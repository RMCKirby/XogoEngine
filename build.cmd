@echo off
cls

.paket\paket.bootstrapper.exe
.paket\paket.exe restore

.\packages\build\FAKE\tools\Fake.exe build.fsx