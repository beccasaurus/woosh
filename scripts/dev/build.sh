#! /usr/bin/env bash

rm -rf bin/
mkdir bin
./dev clean
dotnet build "$@"
cd woosh
dotnet publish -c Release -r linux-x64 /p:PublishSingleFile=true
cp bin/Release/net5.0/linux-x64/publish/Woosh ../bin/woosh
