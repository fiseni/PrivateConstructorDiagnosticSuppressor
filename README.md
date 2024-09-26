<img align="left" src="pozitronlogo.png" width="120" height="120">

&nbsp; [![NuGet](https://img.shields.io/nuget/v/Pozitron.Analyzers.PrivateConstructorDiagnosticSuppression.svg)](https://www.nuget.org/packages/Pozitron.Analyzers.PrivateConstructorDiagnosticSuppression)

&nbsp; [![Full Build](https://github.com/fiseni/PrivateConstructorDiagnosticSuppressor/actions/workflows/build.yml/badge.svg)](https://github.com/fiseni/PrivateConstructorDiagnosticSuppressor/actions/workflows/build.yml)

&nbsp; [![Coverage](https://privateconstructordiagnosticsuppressor.fiseni.com/badge_combined.svg)](https://privateconstructordiagnosticsuppressor.fiseni.com)

# Private Constructor Nullability Suppression

A library for suppressing the CS8618 warning for private constructors.

```
CS8618 - Non-nullable variable must contain a non-null value when exiting constructor. Consider declaring it as nullable.
```
<strong>Note:</strong> Use it if you're sure the state will be set correctly by other means.
## Getting Started

Get it on [nuget.org](https://www.nuget.org/packages/Pozitron.Analyzers.PrivateConstructorDiagnosticSuppression) or just include with
```csproj
<PackageReference Include="Pozitron.Analyzers.PrivateConstructorDiagnosticSuppression" Version="1.0.0" PrivateAssets="All" />
```

There are no attributes and no configuration. Just include the package and you are good to go.

## Motivation

I find the CS8618 compiler warning very useful. But, not rarely, we do define empty private constructors. It's necessary for EF Core, other ORMs, mapping libraries, etc. Once you have that, then this rule becomes an annoyance. You either have to use pragma, or initialize the state to null! which defies the whole purpose.

![image](https://github.com/fiseni/PrivateConstructorDiagnosticSuppressor/assets/24314310/1e1ffeec-15cf-4aee-84d4-9c8bc0352680)

## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!
