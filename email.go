package main

import (
	"encoding/json"
	"fmt"
	"github.com/gorilla/mux"
	"log"
	"net/http"
	"strings"
)

var lastContactedMap map[string]string

const URL_PATH = "/email"
const EMAIL_ADDRESS = "email-address"

// main is the program's entry point.
func main() {
	router := createRouter()
	log.Fatal(http.ListenAndServe(":8080", router))
}

// createRouter creates a gorilla router that maps different paths/request types to
// functions to handle those request.
func createRouter() *mux.Router {
	//TODO Currently populating hardcoded map. This is a side effect of this method that will be removed once we add database integration
	populateLastContactedMap()
	router := mux.NewRouter().StrictSlash(true)
	router.HandleFunc(URL_PATH, getAllLastContactedTimes).Methods("GET")
	router.HandleFunc(fmt.Sprintf("%s/{%s}", URL_PATH, EMAIL_ADDRESS), getLastContactedTime).Methods("GET")

	return router
}

// populateLastContactedMap creates a map of email addresses mapped
// to a date in string format
func populateLastContactedMap() {
	lastContactedMap = make(map[string]string)
	lastContactedMap["david.wybourn@superawesomegoodcode.co.uk"] = "2016-03-07T12:45:04Z"
	lastContactedMap["chris.smith@leaddeveloper.com"] = "2016-02-17T15:51:15Z"
	lastContactedMap["bob.builder@cartoonconstructionslimited.tv"] = "2004-01-30T05:00:01"
}

// getLastContactedTime gets the last contacted time for one or more email addresses.
// The response is in json format of the form {emailAddress:date, ...}
func getLastContactedTime(writer http.ResponseWriter, request *http.Request) {
	vars := mux.Vars(request)
	emailAddressesStr := vars[EMAIL_ADDRESS]

	emailAddresses := strings.Split(emailAddressesStr, ",")

	result := make(map[string]string)

	for _, emailAddress := range emailAddresses {
		result[emailAddress] = lastContactedMap[emailAddress]
	}

	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(result)
}

// getAllLastContactedTimes gets the last contacted times for all of the email addresses.
// The response is in json format of the form {emailAddress:date, ...}
func getAllLastContactedTimes(writer http.ResponseWriter, request *http.Request) {
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(lastContactedMap)
}
