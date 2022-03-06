# Strings Are Evil


This project was based on the [Strings Are Evil](https://indy.codes/strings-are-evil) article. I wanted to implement the memory reduction and performance optimization myself, using the new .NET `Span` APIs. 

Here are the results:

``` ini

BenchmarkDotNet=v0.13.1, OS=ubuntu 20.04
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.405
  [Host]     : .NET 5.0.14 (5.0.1422.5710), X64 RyuJIT
  DefaultJob : .NET 5.0.14 (5.0.1422.5710), X64 RyuJIT


```
|                      Method |     Mean |    Error |   StdDev |   Median | Ratio | RatioSD |      Gen 0 |    Allocated |
|---------------------------- |---------:|---------:|---------:|---------:|------:|--------:|-----------:|-------------:|
| FileConsumerV0_LineParserV0 | 79.49 ms | 1.161 ms | 1.086 ms | 79.24 ms |  1.00 |    0.00 | 23571.4286 | 98,939,242 B |
| FileConsumerV0_LineParserV1 | 62.72 ms | 0.434 ms | 0.339 ms | 62.74 ms |  0.79 |    0.01 | 14500.0000 | 60,727,772 B |
| FileConsumerV0_LineParserV2 | 43.17 ms | 0.818 ms | 1.555 ms | 42.39 ms |  0.55 |    0.03 |  4583.3333 | 19,445,960 B |
| FileConsumerV0_LineParserV3 | 40.71 ms | 0.240 ms | 0.224 ms | 40.71 ms |  0.51 |    0.01 |  3076.9231 | 12,895,590 B |
| FileConsumerV1_LineParserV3 | 48.71 ms | 0.181 ms | 0.142 ms | 48.74 ms |  0.61 |    0.01 |          - |      4,392 B |
| FileConsumerV2_LineParserV3 | 39.01 ms | 0.280 ms | 0.234 ms | 39.01 ms |  0.49 |    0.01 |          - |        252 B |
| FileConsumerV3_LineParserV3 | 37.82 ms | 0.448 ms | 0.397 ms | 37.76 ms |  0.48 |    0.01 |          - |        232 B |



## Optimizations

### LineParser

- **V0 -> V1**: String's `.split` allocate strings for each substring found, and the split was done 2 times unnecessarily. This first optimization removed one of the unnecessaries `.split`.
- **V1 -> V2**: At V1 we still have to use `.split`, which is still responsible for lots of the allocations (we can do memory profiling to attest that - either use PerfView or dotMemory/dotTrace). We can workaround splitting by parsing the numbers between commas using the Span APIs. We create a `ReadOnlySpans` for the whole line and then slice it for each part between commas that are to be parsed. 
- **V2 -> V3**: We can still improve the LineParser by removing the heap allocations of `ValueHolderAsClassV2`. Depending on how the `ValueHolder` is going to be used we can remove allocations by using `struct`. Depending on how they are used they are allocated at the stack, so we don't create GC pressure.

At this point, `FileConsumerV0_LineParserV3` we've removed lots of allocations - \~87% reduction compared to `FileConsumerV0_LineParserV0`) and improved performance by \~50%, because we're using more efficient parsing.

### FileConsumer

When we're consuming each line of the file we're allocating strings, using `reader.ReadLine()`. We'll have to implement consumption of each line of the file using lower level APIs and take control of the buffers used.
- **V0 -> V1**: Now we're allocating only the `line` buffer, and consuming one byte at a time. We've lots allocated probably because when `ArrayPool<char>.Shared.Rent(256)` it allocates in bigger chunks. However, probably because we are consuming one byte at a time from the FileStream, we've worsened time performance, even though memory usage is now pratically irrelevant compred to `FileConsumerV0_LineParserV3`.
- **V1 -> V2**: This recovers the execution time performance. Now instead of reading each byte we're consuming the stream in chunks. Not sure yet why the allocated bytes at V1 is much more than V2.
- **V2 -> V3**: Now instead of using the ArrayPool we're using Spans, which allows safe usage of `stackalloc` (which otherwise would be only allowed under `unsafe` code).

There are some things yet to discover - Why `FileConsumerV1_LineParserV3` uses much more memory than `FileConsumerV2_LineParserV3` and where are the allocated bytes from `FileConsumerV2_LineParserV3`



