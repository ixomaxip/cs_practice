main: build
	dotnet ./bin/Debug/netcoreapp2.2/cs.dll -i input.txt -o output.txt
build:
	dotnet build