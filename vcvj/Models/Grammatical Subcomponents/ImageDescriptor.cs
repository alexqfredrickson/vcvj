namespace vcvj.Models.Grammatical_Subcomponents
{
    public class ImageDescriptor
    {
        public byte[] Bytes = new byte[10];
        public const int TotalBlockLength = 10;

        public int ImageLeft { get; set; }
        public int ImageTop { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        public string PackedField { get; set; }

        public bool LocalColorTableFlag { get; set; }
        public bool InterlaceFlag { get; set; }
        public bool SortFlag { get; set; }
        public int ReservedForFutureUse { get; set; }
        public int LocalColorTableLength { get; set; }
    }
}
