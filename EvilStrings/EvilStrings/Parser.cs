using System;

namespace EvilStrings
{
    public static class Parser
    {
        public static int ReadNextInt(ReadOnlySpan<char> line, ref int pos)
        {
            int bef = ++pos;
            while (line[pos] != ',') pos++;
            return int.Parse(line.Slice(bef, pos - bef));
        }
        
        public static decimal ReadNextDecimal(ReadOnlySpan<char> line, ref int pos)
        {
            int bef = ++pos;
            while (line[pos] != ',') pos++;
            return decimal.Parse(line.Slice(bef, pos - bef));
        }
    }
}