SETLOCAL
SET TestUser=test
dotnet test ENSEKTestAPIApplication.csproj --logger:"console;verbosity=detailed"  --filter "Category=regression & Category=test"