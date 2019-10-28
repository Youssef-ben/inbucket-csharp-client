#!/bin/sh

# Verify Version has not been changed manually
if [[ ! -z $(git diff -G develop:package.version packge.version) ]]; then
     echo "Version in {package.version} has been changed. Failing"
     exit 1
fi

echo "All checks passed"