using System;

namespace EvilStrings.LineParser
{
    public interface ILineParser
    {
        public void ParseLine(string line);
        public void ParseLineSpan(ReadOnlySpan<char> line);
    }
}