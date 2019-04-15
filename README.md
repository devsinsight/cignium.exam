# CIGNIUM EXAM SEARCH FIGHT

This is the search fight exam solution. You can find the .exe in: cignium.exam/bin/release/netcoreapp1.1/win10-x64/searchfight.exe

# Video Resolution:

https://www.screencast.com/t/5gEpv0QxeEvP

Please note that the google api keys and the bing api keys has some restrictions:

google api: https://developers.google.com/custom-search/json-api/v1/using_rest
bing api: https://azure.microsoft.com/en-us/try/cognitive-services/?api=bing-web-search-api

*Bing Key valid for 7 days (start day: 15/04/2019)

# Commands:
    dotnet clean
    dotnet build
    dotnet build -c Release
    dotnet publish -c Release
    dotnet publish -c Release -r win10-x64

    cd bin/Release/netcoreapp2.2/win10-x64search
    searchfight.exe .net java