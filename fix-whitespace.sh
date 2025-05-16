#!/bin/bash

echo "Fixar trailing whitespace och extra mellanslag före '{' i alla .cs-filer..."

find . -type f -name "*.cs" | while read -r file; do
  sed -i 's/[ \t]*$//' "$file"
  sed -i 's/  \+{ / { /g' "$file"
  sed -i 's/  \+{/{/g' "$file"
  sed -i 's/ *{/{/g' "$file"
done

echo "Klart!"
