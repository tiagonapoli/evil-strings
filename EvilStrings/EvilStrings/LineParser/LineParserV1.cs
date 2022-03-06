using System;
using EvilStrings.ValueHolder;

namespace EvilStrings.LineParser
{
    public class LineParserV1 : ILineParser
    {
        public void ParseLine(string line)
        {
            var parts = line.Split(',');

            if (parts[0] == "MNO")
            {
                var valueHolder = new ValueHolderAsClassV2(
                    int.Parse(parts[1]),
                    int.Parse(parts[2]),
                    int.Parse(parts[3]),
                    int.Parse(parts[4]),
                    decimal.Parse(parts[5]));
            }
        }

        public void ParseLineSpan(ReadOnlySpan<char> line)
        {
            throw new NotImplementedException();
        }
    }
}