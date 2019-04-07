using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Parsers
{
    public static partial class VcvjParser
    {
        /// <summary>
        /// Populates the global color table byte[] from the source image, and performs lexical parsing on the array.
        /// </summary>
        /// <param name="bytes"></param>
        public static GlobalColorTable ParseGlobalColorTable(byte[] bytes)
        {
            GlobalColorTable gct = new GlobalColorTable()
            {
                TotalBlockLength = bytes.Length,
                Bytes = (byte[]) bytes.Clone(),
                DistinctColorCount = bytes.Length / 3
            };

            for (int i = 0; i <= bytes.Length - 3; i += 3)
            {
                gct.Colors.Add(new byte[3] { bytes[i], bytes[i + 1], bytes[i + 2] });
            }

            return gct;
        }
    }
}
