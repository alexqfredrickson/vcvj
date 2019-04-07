using vcvj.Models.Grammatical_Components;

namespace vcvj.Models.Grammatical_Subcomponents
{
    /// <summary>
    /// An extension containing a comment.
    /// </summary>
    public class CommentExtension : SpecialPurposeBlock
    {
        public byte[] Bytes { get; set; }
        public int TotalBlockLength { get; set; }
        public int CommentLength { get; set; }
        public string Comment { get; set; }
    }
}
