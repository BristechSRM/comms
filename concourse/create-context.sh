#!/bin/sh

set -e

cp code/concourse/Dockerfile context/
echo "Copied Dockerfile"
cp binaries/ context/
echo "Copied binary"
