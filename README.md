# ef-contrib

A simple tool that facilitates the common scenarios with Entity Framework Core. `ef-contrib` is designed to keep the database in sync with your migrations.
For example, when you add or remove a new migration you don't need to manually update the database.

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

## Build
Install [.NET Core SDK](https://get.dot.net/).

Run:
```
$ dotnet tool restore
$ dotnet-cake
```
