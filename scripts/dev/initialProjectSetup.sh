#! /usr/bin/env bash

# Just some notes for how to setup a project tree

set -e

dotnet new console -n Woosh       -o woosh
dotnet new xunit   -n Woosh.Specs -o spec

cd spec/
dotnet add reference ../woosh/Woosh.csproj

cd ..
dotnet new sln
dotnet sln add woosh/Woosh.csproj
dotnet sln add spec/Woosh.Specs.csproj