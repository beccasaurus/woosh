#! /usr/bin/env bash

# Just some notes for how to setup a project tree

set -e

root="$PWD"

# Library
mkdir src/
cd src/

dotnet new classlib -o Woosh

# Console
dotnet new console  -o Woosh.CLI
cd Woosh.CLI/
dotnet add reference ../Woosh/Woosh.csproj

# Specs
cd "$root"
dotnet new xunit -o Woosh.Specs
mv Woosh.Specs spec
cd spec/
dotnet add reference ../src/Woosh/Woosh.csproj
dotnet add reference ../src/Woosh.CLI/Woosh.CLI.csproj

# Solution
cd "$root"
dotnet new sln
dotnet sln add src/Woosh/Woosh.csproj
dotnet sln add src/Woosh.CLI/Woosh.CLI.csproj
dotnet sln add spec/Woosh.Specs.csproj