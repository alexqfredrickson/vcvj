using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Parsers
{
    public static partial class VcvjParser
    {
        /// <summary>
        /// Populates the Header byte[] from the source image.
        /// </summary>
        /// <param name="bytes"></param>
        public static Header ParseHeader(byte[] bytes)
        {
            Header header = new Header()
            {
                Bytes = (byte[]) bytes.Clone()
            };

            return header;
        }
    }
}
