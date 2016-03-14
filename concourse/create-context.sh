#!/bin/sh

set -e

cp code/concourse/Dockerfile context/
echo "Copied Dockerfile"
cp binaries/speaker-comms context/
echo "Copied binary"
