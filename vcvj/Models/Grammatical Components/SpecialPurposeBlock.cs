using vcvj.Models.Grammatical_Subcomponents;
namespace vcvj.Models.Grammatical_Components
{
    /// <summary>
    /// Represents either an application extension or a comment extension.
    /// </summary>
    public abstract class SpecialPurposeBlock : DataBlock
    {
        public bool IsApplicationExtension
        {
            get
            {
                return this is ApplicationExtension;
            }
        }

        public bool IsCommentExtension
        {
            get
            {
                return this is CommentExtension;
            }
        }

        public ApplicationExtension ToApplicationExtension()
        {
            return (ApplicationExtension)this;
        }

        public CommentExtension ToCommentExtension()
        {
            return (CommentExtension)this;
        }
    }
}
