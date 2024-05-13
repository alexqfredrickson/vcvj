namespace vcvj.Models.Grammatical_Subcomponents
{
    public class ImageData
    {
        #region Fields
        /// <summary>
        /// The main data stream.
        /// </summary>
        public byte[] Bytes { get; set; }

        /// <summary>
        /// The length of the main data stream.
        /// </summary>
        public int TotalBlockLength { get; set; }

        /// <summary>
        /// A value used for LZW compression/decompression, representing (roughly) the minimum amount of bits-per-pixel. Ranges from 2 to 12.
        /// </summary>
        public int LzwMinimumCodeSize { get; set; }

        /// <summary>
        /// Represents the compressed, LZW-encoded index stream.
        /// </summary>
        public byte[] CompressedLzwEncodedIndexStream { get; set; }

        /// <summary>
        /// Represents the decompressed, LZW-decoded code stream.
        /// </summary>
        public int[] CodeStream { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Turns pixels white or black depending on whether or not their byte values exceed a given threshold.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public ImageData Threshold(byte r, byte g, byte b)
        {
            byte[] tempBytes = new byte[Bytes.Length];

            for (int i = 1; i < Bytes.Length - 3; i += 3)
            {
                if (Bytes[i] > r)
                {
                    tempBytes[i] = 0;
                }
                else
                {
                    tempBytes[i] = 255;
                }

                if (Bytes[i + 1] > g)
                {
                    tempBytes[i + 1] = 0;
                }
                else
                {
                    tempBytes[i + 1] = 255;
                }

                if (Bytes[i + 2] > b)
                {
                    tempBytes[i + 2] = 0;
                }
                else
                {
                    tempBytes[i + 2] = 255;
                }
            }

            Bytes = tempBytes;
            return this;
        }

        /// <summary>
        /// Populates the compressed, LZW-encoded index stream from the raw data stream.
        /// </summary>
        public void PopulateCompressedLzwEncodedIndexStream()
        {
            List<byte> compressedIndices = new List<byte>();

            int currentSubBlockLength;

            for (int i = 1; i < Bytes.Length; i++)
            {
                currentSubBlockLength = Bytes[i];

                for (int j = 1; j < currentSubBlockLength + 1; j++)
                {
                    compressedIndices.Add(Bytes[i + j]);
                }

                i += currentSubBlockLength;
            }

            CompressedLzwEncodedIndexStream = compressedIndices.ToArray();
        }

        /// <summary>
        /// Exports the code/index stream to a text file.
        /// </summary>
        /// <param name="fullPath">The full file path (e.g. 'C:/Desktop/file.text').</param>
        public void ExportCodeStreamToTextFile(string fullPath)
        {
            using (StreamWriter sw = File.CreateText(fullPath))
            {
                int index = 0;

                foreach (int b in CodeStream)
                {
                    sw.WriteLine(string.Format("{0} {1}", index, b));
                    index++;
                }
            }
        }

        /// <summary>
        /// Exports the image data byte stream to a text file.
        /// </summary>
        /// <param name="fullPath">The full file path (e.g. 'C:/Desktop/file.text').</param>
        public void ExportImageDataAsByteStreamToTextFile(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                File.WriteAllBytes(fullPath, CompressedLzwEncodedIndexStream);
            }
        }

        public void ExportImageDataAsBinaryStringToTextFile(string fullPath, bool padLeft)
        {
            using (StreamWriter sw = File.CreateText(fullPath))
            {
                int index = 0;

                foreach (byte b in CompressedLzwEncodedIndexStream)
                {
                    index++;

                    if (padLeft)
                    {
                        sw.Write(string.Format("{0} ", Convert.ToString(b, 2).PadLeft(8, '0')));
                    }
                    else
                    {
                        sw.Write(string.Format("{0} ", Convert.ToString(b, 2).PadRight(8, '0')));
                    }

                    if (index % 8 == 0)
                    {
                        sw.WriteLine("");
                    }
                }
            }
        }

        #endregion
    }
}
