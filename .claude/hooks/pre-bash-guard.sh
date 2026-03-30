#!/bin/bash
# PreToolUse hook: blocks dangerous bash commands

INPUT=$(cat)

# Only check Bash tool calls
TOOL_NAME=$(echo "$INPUT" | jq -r '.tool_name // empty' 2>/dev/null)
if [[ "$TOOL_NAME" != "Bash" ]]; then
  exit 0
fi

COMMAND=$(echo "$INPUT" | jq -r '.tool_input.command // empty' 2>/dev/null)

if [[ -z "$COMMAND" ]]; then
  exit 0
fi

# List of dangerous patterns to block
DANGEROUS_PATTERNS=(
  "rm -rf /"
  "rm -rf ~"
  "rm -rf \."
  "rm -rf \*"
  "rm -rf --no-preserve-root"
  "rmdir /s"
  "del /s /q"
  "format c:"
  "format d:"
  ":(){ :|:& };:"
  "mkfs\."
  "dd if=/dev/zero"
  "dd if=/dev/random"
  "> /dev/sda"
  "chmod -R 777 /"
  "chown -R .* /"
  "git clean -fdx /"
  "git push.*--force.*main"
  "git push.*--force.*master"
  "DROP DATABASE"
  "DROP TABLE"
  "TRUNCATE TABLE"
)

COMMAND_LOWER=$(echo "$COMMAND" | tr '[:upper:]' '[:lower:]')

for PATTERN in "${DANGEROUS_PATTERNS[@]}"; do
  PATTERN_LOWER=$(echo "$PATTERN" | tr '[:upper:]' '[:lower:]')
  if echo "$COMMAND_LOWER" | grep -qiE "$PATTERN_LOWER"; then
    echo "BLOCKED: Dangerous command detected matching pattern: $PATTERN"
    echo "Command was: $COMMAND"
    exit 2
  fi
done

exit 0
