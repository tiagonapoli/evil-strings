using System;
using System.Buffers;
using System.IO;
using EvilStrings.LineParser;

namespace EvilStrings.FileConsumer
{
    public class FileConsumerV2 : IFileConsumer
    {
        public void ConsumeFile(string filename, ILineParser lineParser)
        {
            byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);
            char[] line = ArrayPool<char>.Shared.Rent(256);
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
                        lineParser.ParseLineSpan(line.AsSpan(0, lineCnt));
                        lineCnt = 0;
                    }
                }
            }
            
            ArrayPool<byte>.Shared.Return(buffer);
            ArrayPool<char>.Shared.Return(line);
        }
    }
}