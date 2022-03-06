using System;
using EvilStrings.ValueHolder;

namespace EvilStrings.LineParser
{
    public class LineParserV0 : ILineParser
    {
        public void ParseLine(string line)
        {
            var parts = line.Split(',');

            if (parts[0] == "MNO")
            {
                var valueHolder = new ValueHolderAsClassV1(line);
            }
        }

        public void ParseLineSpan(ReadOnlySpan<char> line)
        {
            throw new NotImplementedException();
        }
    }
}