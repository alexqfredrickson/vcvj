using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Parsers
{
    public static partial class VcvjParser
    {
        /// <summary>
        /// Populates the Graphics Control Extension byte[] from the source image, and performs lexical parsing on the array.
        /// </summary>
        /// <param name="bytes"></param>
        public static GraphicControlExtension ParseGraphicControlExtension(byte[] bytes)
        {
            GraphicControlExtension gce = new GraphicControlExtension()
            {
                Bytes = (byte[])bytes.Clone(),
                BlockSize = bytes[2],
                PackedField = Convert.ToString(bytes[3], 2).PadLeft(8, '0')
            };

            gce.DisposalMethod = Convert.ToInt32(gce.PackedField.Substring(3, 3), 2);
            gce.UserInputFlag = gce.PackedField.Substring(6, 1) == "1";
            gce.TransparentColorFlag = gce.PackedField.Substring(7, 1) == "1";

            return gce;
        }
    }
}
