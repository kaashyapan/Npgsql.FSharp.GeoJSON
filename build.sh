#!/usr/bin/env bash

set -eu

cd "$(dirname "$0")"

PAKET_EXE=.paket/paket.exe

run() {
    "$@"
}

echo "Executing Paket..."

FILE='paket.lock'     
if [ -f $FILE ]; then
   echo "paket.lock file found, restoring packages..."
   run paket restore
else
   echo "paket.lock was not found, installing packages..."
   run paket install
fi

run dotnet pack -c Release src