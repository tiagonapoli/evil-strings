using BenchmarkDotNet.Attributes;
using EvilStrings.FileConsumer;
using EvilStrings.LineParser;

namespace EvilStrings
{
    [HtmlExporter,MarkdownExporter]
    [MemoryDiagnoser]
    public class Benchmarks
    {
        public static string FileName = "/tmp/input.txt";
        
        [GlobalSetup]
        public void GlobalSetup() => InputGenerator.GenerateInput(FileName);
        
        [Benchmark(Baseline = true)]
        public void FileConsumerV0_LineParserV0()
        {
            new FileConsumerV0().ConsumeFile(FileName, new LineParserV0());
        }
        
        [Benchmark]
        public void FileConsumerV0_LineParserV1()
        {
            new FileConsumerV0().ConsumeFile(FileName, new LineParserV1());
        }
        
        [Benchmark]
        public void FileConsumerV0_LineParserV2()
        {
            new FileConsumerV0().ConsumeFile(FileName, new LineParserV2());
        }
        
        [Benchmark]
        public void FileConsumerV0_LineParserV3()
        {
            new FileConsumerV0().ConsumeFile(FileName, new LineParserV3());
        }
        
        [Benchmark]
        public void FileConsumerV1_LineParserV3()
        {
            new FileConsumerV1().ConsumeFile(FileName, new LineParserV3());
        }
        
        [Benchmark]
        public void FileConsumerV2_LineParserV3()
        {
            new FileConsumerV2().ConsumeFile(FileName, new LineParserV3());
        }
        
        [Benchmark]
        public void FileConsumerV3_LineParserV3()
        {
            new FileConsumerV3().ConsumeFile(FileName, new LineParserV3());
        }
    }
}