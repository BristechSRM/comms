#!/bin/sh

set -e

cd code
cp -R /code/.nuget/ .
cp -R /code/packages/ .
mono packages/FAKE/tools/FAKE.exe build.fsx
cd ..
cp code/build/output/* binaries/
