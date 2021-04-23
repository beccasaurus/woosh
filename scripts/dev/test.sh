#! /usr/bin/env bash

if (( $# > 0 ))
then
  filter=
  while (( $# > 0 ))
  do
    filter+="$1|"
    shift
  done
  filter="${filter%|}"
  cd spec
  dotnet test --filter "$filter"
else
  cd spec
  dotnet test
fi