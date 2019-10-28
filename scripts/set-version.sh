#!/bin/sh
# This Script is used to determine if we need to upgrade the version or not.
# Normaly the version will be upgraded only for the {Develop} branch.
# The other branches should only create a tag with a suffix {-beta} Or nothing for prod.

# Set the Suffix to (alpha) as default and set the branch name using git.
SUFFIX=-alpha
BRANCH_NAME=$(git branch | grep \* | cut -d ' ' -f2)

# Security check.
# If the current branch is not {Develop, Beta or Master} then exits the script.
if [ $BRANCH_NAME != 'develop' ] && [ $BRANCH_NAME != 'beta' ] && [ $BRANCH_NAME != 'master' ]
then
    echo "Nothing to do for this branch {$BRANCH_NAME}."
    exit 1
fi

# Get the current and new Version.
source ./scripts/get-version.sh

echo "Current values for the branch: {$BRANCH_NAME}"
echo "Version : $APP_VERSION"
echo "Date    : $PREVIOUS_DATE"
echo "Suffix  : $SUFFIX"
echo

# Set the Suffix and version for Beta and Master(Production).
# Note: The Develop branch will take the {NEW_VERSION} variable.
if [ $BRANCH_NAME == 'beta' ]
then

    # For the {Beta} branch we only want to increment the Fix version.
    # In order to do that, we check if we have a Fix Version.
    # If it's empty, we will set it to {0}.
    # If not Empty, then we shoud take the {NEW_VERSION} variable whitout setting it.
    FIX_VERSION=$(echo $APP_VERSION | awk -F. '{$NF+=1; OFS="."; print $4}')
    if [ -z "$FIX_VERSION" ]
    then
        echo "Setting the Fixes version..."
        NEW_VERSION=$APP_VERSION.0
    fi

    echo "Setting the Suffix for the {Beta} branch..."
    SUFFIX=-beta

elif [ $BRANCH_NAME == 'master' ]
then

    # Setting the {NEW_VERSION} to the current version means that the fixes always comes from the {Beta} branch.
    # Which means that the Beta stable version is the Master(production) version.
    echo "Setting the Suffix for the {Master} branch..."
    NEW_VERSION=$APP_VERSION
    SUFFIX=

fi

# Depending on the current branch, the script will either update the {appsettings} Date or Date and Version.
echo "Upgrading the version..."
source ./scripts/upgrade-version.sh

# Show the final result 
echo
echo "New values for the branch: {$BRANCH_NAME}"
echo "Version : $NEW_VERSION"
echo "Date    : $CURRENT_DATE"
echo "Suffix  : $SUFFIX"
echo "Tag Name: v$NEW_VERSION$SUFFIX"

# Use git to add and push the new changes
echo
echo "Publishing the changes to Gitlab..."
git add -A
git commit -m "[skip ci] Bumped version number to ${NEW_VERSION}"
git tag -a v$NEW_VERSION$SUFFIX -m "{Released:$CURRENT_DATE}"
git push --follow-tags origin $CI_COMMIT_REF_NAME