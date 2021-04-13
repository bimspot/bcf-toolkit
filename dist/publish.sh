#!/bin/bash

RuntimeIdentifiers=(
  linux-x64
  linux-musl-x64
  linux-arm
  win-x64
  win-x86
  win-arm
  win-arm64
  osx-x64)

printf "Publishing Framework-dependent executable.\n\n"

for id in "${RuntimeIdentifiers[@]}"
do
  printf "[x] Packaging for runtime: $id\n\n"

  dotnet publish ../bcf-converter/bcf-converter.csproj \
    -c Release \
    -r $id \
    --self-contained false \
    --output ../dist/bcf-converter-$id || exit 9

  # zip -r $id.zip $id
  tar -zcvf bcf-converter-$id.tar.gz bcf-converter-$id || exit 9
  rm -rf bcf-converter-$id || exit 9

  printf "\n\n"
done

printf "Publishing completed.\n\n"