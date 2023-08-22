#!/bin/bash

# Wait for MySQL to be ready
/app/wait-for-it.sh mysql:3306 -t 60

# Run EF migrations
dotnet ef database update --project EventModuleApi.csproj

# Start the application
dotnet EventModuleApi.dll
