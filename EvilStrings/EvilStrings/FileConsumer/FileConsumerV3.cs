using System;
using System.Buffers;
using System.IO;
using EvilStrings.LineParser;

namespace EvilStrings.FileConsumer
{
    public class FileConsumerV3 : IFileConsumer
    {
        public void ConsumeFile(string filename, ILineParser lineParser)
        {
            Span<byte> buffer = stackalloc byte[4096];
            Span<char> line = stackalloc char[256];
            int lineCnt = 0;
            
            using var fs = File.OpenRead(filename);
        
            int readBytes;
            while ((readBytes = fs.Read(buffer)) > 0)
            {
                for (int i = 0; i < readBytes; i++)
                {
                    line[lineCnt++] = (char) buffer[i];
                    if (buffer[i] == '\n')
                    {
                        lineParser.ParseLineSpan(line.Slice(0, lineCnt));
                        lineCnt = 0;
                    }
                }
            }
        }
    }
}