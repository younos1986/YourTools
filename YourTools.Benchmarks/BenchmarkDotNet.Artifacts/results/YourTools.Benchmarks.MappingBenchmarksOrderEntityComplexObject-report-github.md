```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22621.4317/22H2/2022Update/SunValley2)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.201
  [Host]   : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  ShortRun : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method           | Categories     | Mean        | Error       | StdDev     | Gen0   | Gen1   | Allocated |
|----------------- |--------------- |------------:|------------:|-----------:|-------:|-------:|----------:|
| YourTools_Array  | Array To Array | 1,022.40 ns |   254.50 ns |  13.950 ns | 1.0872 | 0.0229 |    6824 B |
| Mapster_Array    | Array To Array | 1,538.81 ns | 4,336.73 ns | 237.711 ns | 1.2150 | 0.0286 |    7624 B |
| AutoMapper_Array | Array To Array | 1,360.30 ns |   508.25 ns |  27.859 ns | 1.2150 | 0.0286 |    7624 B |
|                  |                |             |             |            |        |        |           |
| YourTools_List   | List to List   | 1,122.48 ns | 1,094.32 ns |  59.983 ns | 1.0929 | 0.0229 |    6856 B |
| Mapster_List     | List to List   | 1,479.53 ns |   543.78 ns |  29.806 ns | 1.2188 | 0.0286 |    7656 B |
| AutoMapper_List  | List to List   | 1,566.10 ns | 2,141.05 ns | 117.358 ns | 1.2741 | 0.0305 |    8008 B |
|                  |                |             |             |            |        |        |           |
| YourTools        | Single         |    56.20 ns |    49.79 ns |   2.729 ns | 0.0421 |      - |     264 B |
| Mapster          | Single         |    85.43 ns |   363.60 ns |  19.930 ns | 0.0471 |      - |     296 B |
| AutoMapper       | Single         |   146.51 ns |   515.39 ns |  28.250 ns | 0.0470 |      - |     296 B |
