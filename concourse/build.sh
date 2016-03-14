#!/bin/sh

set -e

export GOPATH=`pwd`/code
cd code

# TODO: Automatically go get dependencies
go get github.com/gorilla/mux
echo "Fetched Gorilla Mux"

go build -o speaker-comms
echo "Built binary"
cd ..
cp code/speaker-comms binaries/
echo "Copied binary to output"
