#! /usr/bin/env bash

# Modern Instructions for installation on Ubuntu, e.g. 20.04
# https://docs.microsoft.com/en-us/dotnet/core/install/linux#ubuntu

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update;
sudo apt-get install -y apt-transport-https && sudo apt-get update && sudo apt-get install -y dotnet-sdk-5.0