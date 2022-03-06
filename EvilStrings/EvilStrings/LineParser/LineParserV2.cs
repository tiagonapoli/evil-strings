using System;
using EvilStrings.ValueHolder;

namespace EvilStrings.LineParser
{
    public class LineParserV2 : ILineParser
    {
        public void ParseLine(string line)
        {
            ParseLineSpan(line.AsSpan());
        }

        public void ParseLineSpan(ReadOnlySpan<char> lineSpan)
        {
            if (lineSpan.Slice(0, 3).CompareTo("MNO", StringComparison.Ordinal) == 0)
            {
                int pos = 3;
                var valueHolder = new ValueHolderAsClassV2(
                    Parser.ReadNextInt(lineSpan, ref pos),
                    Parser.ReadNextInt(lineSpan, ref pos),
                    Parser.ReadNextInt(lineSpan, ref pos),
                    Parser.ReadNextInt(lineSpan, ref pos),
                    Parser.ReadNextDecimal(lineSpan, ref pos));
            }
        }
    }
}