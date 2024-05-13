using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Parsers
{
    public static partial class VcvjParser
    {
        /// <summary>
        /// Populates the Logical Screen Descriptor byte[] from the source image, and performs lexical parsing on the array.
        /// </summary>
        /// <param name="bytes"></param>
        public static LogicalScreenDescriptor ParseLogicalScreenDescriptor(byte[] bytes)
        {
            string packedField = Convert.ToString(bytes[4], 2).PadLeft(8, '0');
            int globalColorTableSize = Convert.ToInt32(packedField.Substring(5, 3), 2);

            LogicalScreenDescriptor lsd = new LogicalScreenDescriptor()
            {
                Bytes = (byte[])bytes.Clone(),
                CanvasWidth = BitConverter.ToInt32(new byte[] { bytes[0], bytes[1], 0, 0 }, 0),
                CanvasHeight = BitConverter.ToInt32(new byte[] { bytes[2], bytes[3], 0, 0 }, 0),
                PackedField = packedField,
                HasGlobalColorTable = packedField.Substring(0, 1) == "1",
                ColorResolution = Convert.ToInt32(packedField.Substring(1, 3), 2),
                SortFlag = packedField.Substring(4, 1) == "1",
                GlobalColorTableSize = globalColorTableSize,
                GlobalColorTableLength = 3 * (int)(Math.Pow(2, globalColorTableSize + 1)), // 3 * (2 ^ (n + 1)), where n equals the GlobalColorTableSize
                BackgroundColorIndex = bytes[5],
                PixelAspectRatio = bytes[6]
            };

            return lsd;
        }
    }
}
