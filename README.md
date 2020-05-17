# ef-contrib

[![Build Status](https://dev.azure.com/ctyar/ef-contrib/_apis/build/status/ctyar.ef-contrib?branchName=master)](https://dev.azure.com/ctyar/ef-contrib/_build/latest?definitionId=3&branchName=master)
[![ctyar.ef-contrib](https://img.shields.io/nuget/v/ctyar.ef-contrib.svg)](https://www.nuget.org/packages/ctyar.ef-contrib/)

A simple tool that helps with common scenarios while using Entity Framework Core command-line tool (dotnet-ef). `ef-contrib` is designed to make everyday job easier.

## Get started

Download the [.NET Core SDK](https://get.dot.net/).

Once installed, run this command:

```
$ dotnet tool install --global ctyar.ef-contrib
```

And run the tool with:
```
$ ef-contrib
```

### recreate

With `dotnet-ef`, if you want to recreate the current migration that you are working on you have to execute multiple commands
to first revert the current changes and then create a fresh migration. With `ef-contrib`, you can do this in a single command.

```
ef-contrib recreate
```

instead of

```
// Revert the changes from current migration
dotnet ef database update MyPreviousMigration
dotnet ef migrations remove

// Create a fresh migration
dotnet ef migrations add MyNewMigration
dotnet ef database update
```

### squash

With `dotnet-ef`, if you want to merge two migrations into one
you have to first remove them and then recreate them again as one.

```
ef-contrib squash
```

instead of

```
// Remove the last two migrations
dotnet ef database update MyMigrationBeforeAnyOfThese
dotnet ef migrations remove
dotnet ef migrations remove

// Add the migration again
dotnet ef migrations add MyNewMigration
dotnet ef database update
```

### add/remove

Adding and removing migrations also comes with another step to keep the database in sync.
`ef-contrib` handles this part automatically.

```
ef-contrib add
```

instead of

```
dotnet ef migrations add MyNewMigration
dotnet ef database update
```

### config

If you happen to have multiple DbContexts or your DbContext is in a different project you have to add a lot of arguments every time you execute a command.
With `ef-contrib config`, you can save all these configs in your project, and `efcotrib ef` takes care of adding those arguments to your calls.

```
// Just once to config
ef-contrib config

ef-contrib ef migrations list
```

instead of

```
dotnet ef migrations list --project MyCompany.Product.EfProject --startup-project MyCompany.Product.MainProject
```

## Pre-release builds
Get the package from [here](https://github.com/ctyar/ef-contrib/packages/164132).

## Build
Install [.NET Core SDK](https://get.dot.net/).

Run:
```
$ dotnet tool restore
$ dotnet-cake
```
