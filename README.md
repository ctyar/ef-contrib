# ef-contrib

A simple tool that facilitates the common scenarios with Entity Framework Core. `ef-contrib` is designed to keep the database in sync with your migrations.
For example, when you add or remove a new migration you don't need to manually update the database.

## Get started

Download the [.NET Core SDK](https://get.dot.net/).

Once installed, run this command:

```
$ dotnet tool install --global ctyar.ef-contirb
```

And run the tool with:
```
$ ef-contrib
```

## Build
Install [.NET Core SDK](https://get.dot.net/).

Run:
```
$ dotnet tool restore
$ dotnet-cake
```
