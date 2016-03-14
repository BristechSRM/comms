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

func init() {
	populateLastContactedMap()
	server = httptest.NewServer(createRouter())
	url = fmt.Sprintf("%s/last-contacted", server.URL)
}

func TestGetAllLastContactedTimes(t *testing.T) {
	response := performHttpRequest()
	checkStatusOK(response.StatusCode, t)

	responseMap := getResponseMap(response)

	if len(responseMap) != len(lastContactedMap) {
		t.Errorf("Expected response size: %d, Actual response size: %d", len(lastContactedMap), len(responseMap))
	}
	if !reflect.DeepEqual(responseMap, lastContactedMap) {
		t.Errorf("Expected last contacted dates: %s, Actual last contacted dates: %s", lastContactedMap, responseMap)
	}
}

func getResponseMap(response *http.Response) map[string]string {
	responseBody, err := ioutil.ReadAll(response.Body)
	panicIfError(err)

	responseMap := make(map[string]string)
	json.Unmarshal(responseBody, &responseMap)

	return responseMap
}

func performHttpRequest() *http.Response {
	request, err := http.NewRequest("GET", fmt.Sprintf("%s", url), strings.NewReader(""))
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
