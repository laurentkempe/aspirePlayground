#!/bin/bash
# not used at the moment as postCreateCommand doesn't seems to be supported by Rider but works on VSCode

dapr uninstall --all
dapr init

dotnet restore
