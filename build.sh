#!/bin/bash
if [ "$OS" = "Windows_NT" ]; then
    #Use .NET
    .paket/paket.bootstrapper.exe
    .paket/paket.exe restore

    packages/build/FAKE/tools/FAKE.exe $@ --fsiargs -d:MONO build.fsx
else
    #Use Mono
    mono .paket/paket.bootstrapper.exe
    mono .paket/paket.exe restore

    mono packages/build/FAKE/tools/FAKE.exe $@ --fsiargs -d:MONO build.fsx
fi