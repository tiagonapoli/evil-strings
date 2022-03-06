using System.IO;
using EvilStrings.LineParser;

namespace EvilStrings.FileConsumer
{
    public class FileConsumerV0 : IFileConsumer
    {
        public void ConsumeFile(string filename, ILineParser lineParser)
        {
            using StreamReader reader = File.OpenText(filename);
            while (reader.EndOfStream == false)
            {
                lineParser.ParseLine(reader.ReadLine());
            }
        }
    }
}