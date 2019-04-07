namespace vcvj.Models.Grammatical_Subcomponents
{
    /// <summary>
    /// The block which contains data regarding the canvas dimensions, colors used, resolution, and pixel aspect ratios.
    /// </summary>
    public class LogicalScreenDescriptor
    {
        public const int TotalBlockLength = 7;
        public byte[] Bytes = new byte[LogicalScreenDescriptor.TotalBlockLength];

        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }
        public string PackedField { get; set; }
        public int BackgroundColorIndex { get; set; }
        public int PixelAspectRatio { get; set; }

        public int ColorResolution { get; set; }
        public bool SortFlag { get; set; }

        public bool HasGlobalColorTable { get; set; }
        public int GlobalColorTableSize { get; set; }
        public int GlobalColorTableLength { get; set; }
    }
}
