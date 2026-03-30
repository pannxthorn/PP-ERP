#!/bin/bash
# PostToolUse hook: runs dotnet format and dotnet build after editing .cs files

# Read the tool input from stdin
INPUT=$(cat)

# Check if the edited file is a .cs file
FILE_PATH=$(echo "$INPUT" | jq -r '.tool_input.file_path // .tool_input.command // empty' 2>/dev/null)

if [[ "$FILE_PATH" != *.cs ]]; then
  exit 0
fi

cd "c:/Users/spice/source/repos/PP-ERP" || exit 1

# Run dotnet format
echo "Running dotnet format..."
dotnet format PP-ERP.sln --no-restore --include "$FILE_PATH" 2>&1

# Run dotnet build
echo "Running dotnet build..."
dotnet build PP-ERP.sln --no-restore -consoleloggerparameters:ErrorsOnly 2>&1

BUILD_EXIT=$?
if [ $BUILD_EXIT -ne 0 ]; then
  echo "HOOK_ERROR: dotnet build failed with exit code $BUILD_EXIT"
  exit 1
fi

echo "Format and build completed successfully."
