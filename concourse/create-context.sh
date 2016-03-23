#!/bin/sh

set -e

cp code/concourse/Dockerfile context/
echo "Copied Dockerfile"
cp -R binaries/ context/
echo "Copied binary"
