namespace vcvj.Models.Grammatical_Subcomponents
{
    /// <summary>
    /// An optional extension used to specify transparency settings and control animations.
    /// </summary>
    public class GraphicControlExtension
    {
        public const int TotalBlockLength = 8;
        public byte[] Bytes = new byte[TotalBlockLength];
        public int BlockSize { get; set; }

        public string PackedField { get; set; }
        public int DisposalMethod { get; set; }
        public bool UserInputFlag { get; set; }
        public bool TransparentColorFlag { get; set; }
        public int DelayTime { get; set; }
        public int TransparentColorIndex { get; set; }
    }
}
