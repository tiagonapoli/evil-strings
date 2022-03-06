using System;
using System.Buffers;
using System.IO;
using EvilStrings.LineParser;

namespace EvilStrings.FileConsumer
{
    public class FileConsumerV1 : IFileConsumer
    {
        public void ConsumeFile(string filename, ILineParser lineParser)
        {
            char[] line = ArrayPool<char>.Shared.Rent(256);
            int lineCnt = 0;
            
            using var fs = File.OpenRead(filename);
            int readByte;
            while ((readByte = fs.ReadByte()) > 0)
            {
                line[lineCnt++] = (char) readByte;
                if (readByte == '\n')
                {
                    lineParser.ParseLineSpan(line.AsSpan(0, lineCnt));
                    lineCnt = 0;
                }
            }
            
            ArrayPool<char>.Shared.Return(line);
        }
    }
}