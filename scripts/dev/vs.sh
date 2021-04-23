#! /usr/bin/env bash

if [ -f ~/.bash_aliases ]
then
  shopt -s expand_aliases
  source ~/.bash_aliases
fi

vsCommunity2019Location="/mnt/c/Program Files \(x86\)/Microsoft Visual Studio/2019/Community/Common7/IDE/devenv.exe"

if alias vs &>/dev/null
then
  vs woosh.sln
elif [ -f "$vsCommunity2019Location" ]
then
  "$vsCommunity2019Location" woosh.sln
else
  echo "No path to Visual Studio found, please set a 'vs' alias in ~/.bash_aliases pointing to your devenv.exe" >&2
  exit 1
fi