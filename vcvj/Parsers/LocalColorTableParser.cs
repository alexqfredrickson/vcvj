using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Parsers
{
    public static partial class VcvjParser
    {
        /// <summary>
        /// Populates the local color table byte[] from the source image, and performs lexical parsing on the array.
        /// </summary>
        /// <param name="bytes"></param>
        public static LocalColorTable ParseLocalColorTable(byte[] bytes)
        {
            LocalColorTable lct = new LocalColorTable()
            {
                TotalBlockLength = bytes.Length,
                DistinctColorCount = bytes.Length / 3,
                Bytes = (byte[])bytes.Clone()
            };

            for (int i = 0; i <= bytes.Length - 3; i += 3)
            {
                lct.Colors.Add(new byte[3] { bytes[i], bytes[1 + 1], bytes[i + 2] });
            }

            return lct;
        }
    }
}
