#!/bin/bash

if [[ ! -d certs ]]
then
    mkdir certs
    cd certs/
    if [[ ! -f localhost.pfx ]]
    then
        dotnet dev-certs https -v -ep localhost.pfx -p 633becf0-5c18-4b35-a63c-cd9c47da6c6b -t
    fi
    cd ../
fi

docker-compose up -d
