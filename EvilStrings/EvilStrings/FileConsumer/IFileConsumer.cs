using EvilStrings.LineParser;

namespace EvilStrings.FileConsumer
{
    public interface IFileConsumer
    {
        void ConsumeFile(string filename, ILineParser lineParser);
    }
}