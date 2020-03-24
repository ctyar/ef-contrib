dotnet-cake

$version = (get-item .\artifacts\*.nupkg).Name -replace "ef-contrib.","" -replace ".nupkg",""

dotnet tool uninstall ef-contrib --global

dotnet tool install --global --add-source .\artifacts ef-contrib --version $version