#!/bin/bash

# TUSMO Project Setup Script
# This script creates necessary project files for running Tusmo in Unity

set -e

echo "=== Tusmo Project Setup ==="
echo ""

TUSMO_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd "$TUSMO_DIR"

# Check if Unity is installed
if ! command -v unity &> /dev/null; then
    echo "⚠️  Unity Editor not found in PATH"
    echo "Please install Unity 2021.3 LTS or later"
    echo ""
    echo "Manual setup steps:"
    echo "1. Open Unity Hub"
    echo "2. Create new project with Unity 2021.3 LTS"
    echo "3. Copy the Assets/Scripts folder to your project"
    echo "4. Follow TESTING.md for scene setup"
    exit 1
fi

echo "✅ Unity found at: $(which unity)"
echo ""
echo "Creating project structure..."

# Create necessary directories
mkdir -p Assets/Scenes
mkdir -p Assets/Prefabs
mkdir -p Assets/Audio
mkdir -p Assets/Materials
mkdir -p Logs

echo "✅ Directories created"
echo ""
echo "To run the project:"
echo "1. Open this folder in Unity Editor"
echo "2. Follow instructions in TESTING.md to set up the GameScene"
echo "3. Press Play in the Editor"
echo ""
echo "Project is ready!"
