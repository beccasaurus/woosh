#! /usr/bin/env bash

onTrap() {
  echo "Hello, you provided an INT signal"
  exit 1
}

trap onTrap INT

read
read