using vcvj.Models.Enums;
using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Parsers
{
    public static partial class VcvjParser
    {
        /// <summary>
        /// Parses the ImageData block from a byte stream.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static ImageData ParseImageData(byte[] bytes)
        {
            ImageData id = new ImageData()
            {
                TotalBlockLength = bytes.Length,
                LzwMinimumCodeSize = bytes[0],
                Bytes = (byte[])bytes.Clone()
            };

            id.PopulateCompressedLzwEncodedIndexStream();

            //LzwDecompressor decompressor = new LzwDecompressor((byte)id.LzwMinimumCodeSize);
            //LzwTools.Decoder decoder = new LzwTools.Decoder((byte)id.LzwMinimumCodeSize);

            //List<int> decompressedStream = decompressor.Decompress(id.CompressedLzwEncodedIndexStream.Select((b) => (int)b).ToList<int>());
            //id.CodeStream = decoder.Decode(decompressedStream.Select(x => (byte)x).ToList<byte>()).ToArray();

            return id;
        }

        /// <summary>
        /// Determines the length of an ImageData block.
        /// </summary>
        /// <param name="currentIndex">The index of the data stream.</param>
        /// <param name="bytes">The data stream.</param>
        /// <returns></returns>
        public static int GetImageDataBlockLength(long currentIndex, ref byte[] bytes)
        {
            int len = 0;

            len += 1;

            while (bytes[currentIndex + len] != (byte)Global.BlockTerminator)
            {
                len += bytes[currentIndex + len] + 1;
            }

            len += 1;

            return len;
        }

        /// <summary>
        /// Determines the length of an ApplicationExtension block.
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int GetApplicationExtensionBlockLength(long currentIndex, ref byte[] bytes)
        {
            // get main block offset
            int len = 3;
            len += bytes[currentIndex + 2];

            // get sub block offset
            while (bytes[currentIndex + len] != (byte)Global.BlockTerminator)
            {
                len += bytes[currentIndex + len] + 1;
            }

            len += 1;

            return len;
        }
    }
}
