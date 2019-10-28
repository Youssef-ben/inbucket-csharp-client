#!/bin/sh

# Setup shell
GREP="grep"
case $(uname -s) in
    Darwin)
        #https://apple.stackexchange.com/questions/193288/how-to-install-and-use-gnu-grep-in-osxg
        GREP="ggrep"
    ;;
    Linux)
        # Alpine Linux
        if [ "$(awk -F= '/^NAME/{print $2}' /etc/os-release)" = "\"Alpine Linux"\" ]; then
            echo "Installing dependencies for Alpine Linux."
            apk add --no-cache --upgrade grep
        fi
    ;;
esac

echo "Checking that the version hasn't been changed..."
source ./scripts/get-version.sh

export CURRENT_DATE=$(date +'%Y-%m-%d')

echo "Getting the version..."
export CURRENT_VERSION=$(cat ./package.version)

echo "Upgrading the version..."
NEW_VERSION=$(echo $CURRENT_VERSION | awk -F. '{$NF+=1; OFS="."; print $0}')
export NEW_VERSION=${NEW_VERSION// /.}

echo "Updating the {package.version} file..."
echo $NEW_VERSION > ./package.version

echo
echo "Current Date   : $CURRENT_DATE"
echo "Current Version: v$CURRENT_VERSION"
echo "New Version    : v$NEW_VERSION"