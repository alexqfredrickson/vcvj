using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Parsers
{
    public static partial class VcvjParser
    {
        /// <summary>
        /// Populates the Comment Extension byte[] from the source image, and grabs the comment.
        /// </summary>
        /// <param name="bytes"></param>
        public static CommentExtension ParseCommentExtension(byte[] bytes)
        {
            CommentExtension e = new CommentExtension()
            {
                TotalBlockLength = bytes.Length,
                Bytes = (byte[]) bytes.Clone(),
                CommentLength = bytes.Length - 4
            };
            
            return e;
        }
    }
}
