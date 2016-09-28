#!/bin/sh -eu
DIR=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd)
echo $DIR

rm -rf $DIR/source
rm -rf $DIR/binaries
rm -rf $DIR/context
