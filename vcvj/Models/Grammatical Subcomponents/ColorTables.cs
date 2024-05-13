namespace vcvj.Models.Grammatical_Subcomponents
{
    /// <summary>
    /// A block containing information about colors used. Can be global or local (frame-specific).
    /// </summary>
    public abstract class ColorTable
    {
        #region Fields
        public byte[] Bytes { get; set; }
        public int TotalBlockLength { get; set; }

        public bool IsGlobal { get; set; }

        /// <summary>
        /// The number of distinct colors in the global color table.
        /// </summary>
        public int DistinctColorCount { get; set; }

        /// <summary>
        /// A list of the color table's used colors.
        /// </summary>
        public List<byte[]> Colors = new List<byte[]>();
        #endregion

        #region Methods

        /// <summary>
        /// Exports the color table to a text file.
        /// </summary>
        /// <param name="fullPath">The full file path (e.g. 'C:/Desktop/file.text').</param>
        public void ExportToTextFile(string fullPath) {
            if (!File.Exists(fullPath))
            {
                using (StreamWriter sw = File.CreateText(fullPath))
                {
                    int index = 0;

                    foreach (byte[] b in Colors)
                    {
                        string colors = b[0] + " " + b[1] + " " + b[2];
                        sw.WriteLine(string.Format("{0} {1}",  index.ToString(), colors));
                        index++;
                    }
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// A block containing global information about colors used.
    /// </summary>
    public class GlobalColorTable : ColorTable
    {

    }

    /// <summary>
    /// A block containing local information about colors used, relative to a specific frame.
    /// </summary>
    public class LocalColorTable : ColorTable
    {

    }
}
