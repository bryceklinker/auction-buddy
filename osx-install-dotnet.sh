#!/usr/bin/env bash

INSTALLER_URL="https://download.visualstudio.microsoft.com/download/pr/787e81f1-f0da-4e3b-a989-8a199132ed8c/61a8dba81fbf2b3d533562d7b96443ec/dotnet-sdk-3.1.100-osx-x64.pkg"

curl -o ./dotnet-installer.pkg ${INSTALLER_URL}

sudo installer -pkg ./dotnet-installer.pkg -target /