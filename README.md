# Sequence

[![CI](https://github.com/PFalkowski/Sequences/actions/workflows/ci.yml/badge.svg)](https://github.com/PFalkowski/Sequences/actions/workflows/ci.yml)
[![NuGet version](https://img.shields.io/nuget/v/Sequence.svg)](https://www.nuget.org/packages/Sequence/)
[![NuGet downloads](https://img.shields.io/nuget/dt/Sequence.svg)](https://www.nuget.org/packages/Sequence/)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=PFalkowski_Sequences&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=PFalkowski_Sequences)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=PFalkowski_Sequences&metric=coverage)](https://sonarcloud.io/summary/new_code?id=PFalkowski_Sequences)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://choosealicense.com/licenses/mit/)
[![Buy Me a Coffee](https://img.shields.io/badge/Buy%20Me%20a%20Coffee-support-yellow.svg)](https://www.buymeacoffee.com/piotrfalkowski)

Generate and analyze numeric and datetime sequences. `Sequence` computes sum, count, variance, and standard deviation analytically (no enumeration) given `min`, `max`, and `step`. Also provides `Random`/`IEnumerable<T>` extension methods for generating random sequences.

## Sequence (arithmetic range)

```csharp
var seq = new Sequence(0, 10, 1);
var sum = seq.Sum;       // 55
var count = seq.Count;   // 11
var variance = seq.Variance;
var sd = seq.StandardDeviation;

// Lazy enumeration
foreach (var value in seq.GetFullSequence())
    Console.WriteLine(value);
```

## DateTimeRange

```csharp
var range = new DateTimeRange(
    new DateTime(2020, 1, 1),
    new DateTime(2021, 1, 1),
    TimeSpan.FromDays(30));

foreach (var date in range.GetFullSequence())
    Console.WriteLine(date);
```

## Random sequence helpers

```csharp
var rng = new Random();
double[] normals = rng.NormRandoms(1000, mean: 0, sd: 1);
int[]    ints    = rng.RandomInts(100, min: 0, max: 500).ToArray();
string   s       = rng.RandomString(12);
```
