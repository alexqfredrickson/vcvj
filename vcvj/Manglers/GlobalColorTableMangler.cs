using vcvj.Models;
using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Manglers
{
    public class GlobalColorTableMangler
    {
        private VcvjImage VcvjImage { get; set; }
        private GlobalColorTable GlobalColorTable { get; set; }

        /// <summary>
        /// XORs every byte in the color table with the specified byte value.
        /// </summary>
        /// <returns></returns>
        public void XOR(byte b)
        {
            byte[] newBytes = new byte[GlobalColorTable.Bytes.Length];

            for (int i = 0; i < newBytes.Length; i++)
            {
                newBytes[i] = (byte)(GlobalColorTable.Bytes[i] ^ b);
            }

            GlobalColorTable.Bytes = newBytes;
        }

        /// <summary>
        /// Randomizes all colors in the Global Color Table.
        /// </summary>
        /// <param name="vcvjImage"></param>
        /// <returns></returns>
        public void RandomizeColors()
        {
            Random r = new Random();
            byte[] newBytes = new byte[GlobalColorTable.Bytes.Length];
            r.NextBytes(newBytes);
            GlobalColorTable.Bytes = newBytes;
        }

        /// <summary>
        /// Randomizes half of the colors in the Global Color Table.
        /// </summary>
        /// <param name="vcvjImage"></param>
        /// <returns></returns>
        public void RandomizeHalfColors()
        {
            Random r = new Random();
            byte[] newBytes = new byte[GlobalColorTable.Bytes.Length];

            for (int i = 0; i < newBytes.Length - 3; i++)
            {
                if (i % 3 == 0)
                {
                    newBytes[i] = (byte)r.Next(255);
                    newBytes[i + 1] = (byte)r.Next(255);
                    newBytes[i + 2] = (byte)r.Next(255);
                }
                else
                {
                    newBytes[i] = GlobalColorTable.Bytes[i];
                    newBytes[i + 1] = GlobalColorTable.Bytes[i + 1];
                    newBytes[i + 2] = GlobalColorTable.Bytes[i + 2];
                }
            }

            GlobalColorTable.Bytes = newBytes;
        }

        public GlobalColorTableMangler(ref VcvjImage vcvjImage)
        {
            VcvjImage = vcvjImage;
            GlobalColorTable = VcvjImage.DataStream.LogicalScreen.GlobalColorTable;
        }
    }
}
