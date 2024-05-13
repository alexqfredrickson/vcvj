using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Parsers
{
    public static partial class VcvjParser
    {
        public static ImageDescriptor ParseImageDescriptor(byte[] bytes)
        {
            string packedField = Convert.ToString(bytes[9], 2).PadLeft(8, '0');

            ImageDescriptor id = new ImageDescriptor()
            {
                Bytes = (byte[])bytes.Clone(),
                ImageLeft = BitConverter.ToUInt16(new byte[2] { (byte)bytes[1], (byte)bytes[2] }, 0),
                ImageTop = BitConverter.ToUInt16(new byte[2] { (byte)bytes[3], (byte)bytes[4] }, 0),
                ImageWidth = BitConverter.ToUInt16(new byte[2] { (byte)bytes[5], (byte)bytes[6] }, 0),
                ImageHeight = BitConverter.ToUInt16(new byte[2] { (byte)bytes[7], (byte)bytes[8] }, 0),
                PackedField = packedField,
                LocalColorTableFlag = packedField.Substring(0, 1) == "1",
                InterlaceFlag = packedField.Substring(1, 1) == "1",
                SortFlag = packedField.Substring(2, 1) == "1",
                LocalColorTableLength = 3 * (int)(Math.Pow(2, Convert.ToInt32(packedField.Substring(5, 3), 2) + 1))
            };

            return id;
        }
    }
}
