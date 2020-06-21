# LevelUp.Permission

A simple permission solution

## Install

### Package Manager

    Install-Package LevelUp.Permission --version 1.0.0

### .NET CLI

    dotnet add package LevelUp.Permission --version 1.0.0

### PackageReference

    <PackageReference Include="LevelUp.Permission" Version="1.0.0" />

### Packet CLI

    paket add LevelUp.Permission --version 1.0.0


## Tutorial

### Getting started

```C#
//var permission = new Permission<Policy>();
var permission = (Permission<Policy>)0;
Console.WriteLine("所有權限: {0} ({1})", Permission<Policy>.All, Permission<Policy>.All.Value);
Console.WriteLine("當前權限: {0} ({1})", permission, permission.Value);
Console.WriteLine("加入權限 {0}...", Policy.Create);
permission.Add(Policy.Create);
Console.WriteLine("當前權限: {0} ({1})", permission, (BigInteger)permission);
Console.WriteLine("加入權限 {0}...", Policy.Read);
permission.Add(Policy.Read);
Console.WriteLine("當前權限: {0} ({1})", permission, permission.Value);
Console.WriteLine("是否含權限 {0}: {1}", Policy.Read, permission.Contains(Policy.Read));
Console.WriteLine("移除權限 {0}...", Policy.Read);
permission.Remove(Policy.Read);
Console.WriteLine("當前權限: {0} ({1})", permission, permission.Value);
Console.WriteLine("是否含權限 {0}: {1}", Policy.Read, permission.Contains(Policy.Read));
Console.WriteLine();

var permissionA = new Permission<Policy>(Policy.Create);
var permissionB = new Permission<Policy>(Policy.Read);
var permissionC = permissionA + permissionB;
var permissionD = permissionC - permissionB;
var permissionE = new Permission<Policy>(permissionC);
Console.WriteLine("permissionC: {0} ({1})", permissionC, permissionC.Value);
Console.WriteLine("permissionD: {0} ({1})", permissionD, permissionD.Value);
Console.WriteLine("permissionE: {0} ({1})", permissionE, permissionE.Value);
Console.WriteLine("permissionC.Equals(permissionE): {0}", permissionC.Equals(permissionE));
Console.WriteLine("permissionC == permissionE: {0}", permissionC == permissionE);
Console.WriteLine("permissionC != permissionE: {0}", permissionC != permissionE);
```

### Instance permission

```C#
...
var permission = new Permission<Policy>();
//var permission = (Permission<Policy>)0;
//var permission = new Permission<Policy>(Policy.Create);
...
```


### Add policy

```C#
...
permission.Add(Policy.Create);
...
```


### Remove policy

```C#
...
permission.Remove(Policy.Create);
...
```


### Check policy

```C#
...
var hasPolicy = permission.Contains(Policy.Create);
...
```


### Get permission value

```C#
...
var permissionValue = permission.Value;
...
```