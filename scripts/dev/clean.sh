#! /usr/bin/env bash

find . -type d -name obj -o -name bin | grep -v ^./bin | xargs rm -r
rm -rf ./build