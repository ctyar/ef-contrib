# ef-contrib

[![Build Status](https://dev.azure.com/ctyar/ef-contrib/_apis/build/status/ctyar.ef-contrib?branchName=master)](https://dev.azure.com/ctyar/ef-contrib/_build/latest?definitionId=3&branchName=master)

A simple tool that helps with common scenarios while using Entity Framework Core command-line tool (dotnet-ef). `ef-contrib` is designed to keep the database and migrations in sync.
For example, when you add or remove a new migration you don't need to manually update the database.

__Sample scenario:__

With `dotnet-ef` if you want to recreate the current migration that you are working on you have to follow these steps:

1. Update the database to previous migration
2. Remove the current migration
3. Recreate the current migration
4. Update the database

But with `ef-contrib`, you just run `ef-contrib recreate` and everything is taken care of.

## Get started
No release yet but soon :)

## Usage

```
  ef-contrib [options] [command]

Options:
  --version         Show version information
  -?, -h, --help    Show help and usage information

Commands:
  recreate               Recreates the last migration
  squash                 Merges last two migrations
  add <migrationName>    Adds a new migration
  remove                 Removes the last migration
  ef <arguments>         Call ef command directly with your specified configs
  config                 Adds a config file with default project info
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
