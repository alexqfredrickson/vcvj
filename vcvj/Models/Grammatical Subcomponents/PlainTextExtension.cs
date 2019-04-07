using vcvj.Models.Grammatical_Components;

namespace vcvj.Models.Grammatical_Subcomponents
{
    /// <summary>
    /// An extension containing captions.
    /// </summary>
    public class PlainTextExtension : GraphicRenderingBlock
    {
        public byte[] Bytes { get; set; }
        public int TotalBlockLength {get;set;}

        public int BlockSize { get; set; }
        public string Caption { get; set; }

        public PlainTextExtension() { }

        /// <summary>
        /// Populates the Plain Text Extension byte[] from the source image, and performs lexical parsing on the array.
        /// </summary>
        /// <param name="bytes"></param>
        public PlainTextExtension(byte[] bytes)
        {
            BlockSize = bytes[2];

            for (int i = BlockSize + 2; i < Bytes.Length - 1; i++)
            {
                if (Bytes[i] != (byte) Enums.Global.BlockTerminator)
                {
                    Caption += (char) Bytes[i];
                }
                else
                {
                    break;
                }
            }

            TotalBlockLength = Caption.Length + BlockSize + 3;
            Bytes = new byte[TotalBlockLength];

            for (int i = 0; i < Bytes.Length; i++)
            {
                Bytes[i] = bytes[i];
            }
        }
    }
}
