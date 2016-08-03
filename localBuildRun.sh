#!/bin/bash -euv
echo -e '\033]2;'Comms'\007'
./build.sh
./copySecrets.sh
./run.sh
