$packageName = "ctyar.ef-contrib"

dotnet-cake

$version = (get-item .\artifacts\*.nupkg).Name -replace "$($packageName).","" -replace ".nupkg",""

dotnet tool uninstall $packageName --global

dotnet tool install --global --add-source .\artifacts $packageName --version $version