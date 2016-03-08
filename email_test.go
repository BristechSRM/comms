package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
	"net/http/httptest"
	"reflect"
	"strings"
	"testing"
)

var (
	server *httptest.Server
	url    string
)

// init is called before each test. It sets up the http test server
func init() {
	server = httptest.NewServer(createRouter())
	url = fmt.Sprintf("%s/email", server.URL)
}

// TestGetLastContactedTimeSingleEmailAddress tests getting the last
// contacted date for a single email address.
func TestGetLastContactedTimeSingleEmailAddress(t *testing.T) {
	emailAddress := "david.wybourn@superawesomegoodcode.co.uk"

	response := performHttpRequest(emailAddress)
	checkStatusOK(response.StatusCode, t)

	responseMap := getResponseMap(response)

	if len(responseMap) != 1 {
		t.Errorf("Expected response size: 1, Actual response size: %d", len(responseMap))
	}
	if responseMap[emailAddress] != lastContactedMap[emailAddress] {
		t.Errorf("Expected last contacted date: %s, Actual last contacted date: %s", lastContactedMap[emailAddress], responseMap[emailAddress])
	}
}

// TestGetLastContactedTimeMultipleEmailAddresses tests getting the last
// contacted date for multiple email addresses.
func TestGetLastContactedTimeMultipleEmailAddresses(t *testing.T) {
	emailAddresses := "david.wybourn@superawesomegoodcode.co.uk,chris.smith@leaddeveloper.com"

	response := performHttpRequest(emailAddresses)
	checkStatusOK(response.StatusCode, t)

	responseMap := getResponseMap(response)

	if len(responseMap) != 2 {
		t.Errorf("Expected response size: 2, Actual response size: %d", len(responseMap))
	}
	// check each entry in the response map
	for emailAddress := range responseMap {
		contactedDate := lastContactedMap[emailAddress]
		if contactedDate != responseMap[emailAddress] {
			t.Errorf("Expected last contracted date to be %s, Actual date is: %s", contactedDate, responseMap[emailAddress])
		}
	}

}

// TestGetAllLastContactedTimes tests getting the last contacted date
// for all email addresses.
func TestGetAllLastContactedTimes(t *testing.T) {
	// Passing no email address(es) does a GET all.
	response := performHttpRequest("")
	checkStatusOK(response.StatusCode, t)

	responseMap := getResponseMap(response)

	if len(responseMap) != len(lastContactedMap) {
		t.Errorf("Expected response size: %d, Actual response size: %d", len(lastContactedMap), len(responseMap))
	}
	if !reflect.DeepEqual(responseMap, lastContactedMap) {
		t.Errorf("Expected last contacted dates: %s, Actual last contacted dates: %s", lastContactedMap, responseMap)
	}
}

// getResponseMap creates a map from the http response.
func getResponseMap(response *http.Response) map[string]string {
	responseBody, err := ioutil.ReadAll(response.Body)
	panicIfError(err)

	responseMap := make(map[string]string)
	json.Unmarshal(responseBody, &responseMap)

	return responseMap
}

// performHttpRequest performs a http request to our server with
// the given email address(es) string and returns the response.
func performHttpRequest(emailAddress string) *http.Response {
	request, err := http.NewRequest("GET", fmt.Sprintf("%s/%s", url, emailAddress), strings.NewReader(""))
	panicIfError(err)
	response, err := http.DefaultClient.Do(request)
	panicIfError(err)
	return response
}

// checkStatusOK checks if a status code is HTTP 200 (OK).
func checkStatusOK(statusCode int, t *testing.T) {
	if statusCode != http.StatusOK {
		t.Errorf("Expected status code: %d, actual: %d", http.StatusOK, statusCode)
	}
}

// panicIfError panics if the passed in err is not nil.
func panicIfError(err error) {
	if err != nil {
		panic(err)
	}
}
