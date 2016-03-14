package main

import (
	"encoding/json"
	"github.com/gorilla/mux"
	"log"
	"net/http"
)

var lastContactedMap = map[string]string{
	"david.wybourn@superawesomegoodcode.co.uk": "2016-03-07T12:45:04Z",
	"chris.smith@leaddeveloper.com": "2016-02-17T15:51:15Z",
	"bob.builder@cartoonconstructionslimited.tv": "2004-01-30T05:00:01Z",
}

const URL_PATH = "/last-contacted"

func main() {
	router := createRouter()
	// This log will only be called if the create router method returns (which is an error)
	log.Fatal(http.ListenAndServe(":8080", router))
}

func createRouter() *mux.Router {
	router := mux.NewRouter().StrictSlash(true)
	router.HandleFunc(URL_PATH, getAllLastContactedTimes).Methods("GET")

	return router
}

// getAllLastContactedTimes gets the last contacted times for all of the email addresses.
// The response is in json format of the form {emailAddress:date, ...}
func getAllLastContactedTimes(writer http.ResponseWriter, request *http.Request) {
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(lastContactedMap)
}
