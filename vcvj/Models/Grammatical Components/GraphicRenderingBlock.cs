using vcvj.Models.Grammatical_Subcomponents;
namespace vcvj.Models.Grammatical_Components
{
    /// <summary>
    /// Represents table-based images or plain text extensions.
    /// </summary>
    public abstract class GraphicRenderingBlock
    {
        /// <summary>
        /// Determines whether or not a GraphicRenderingBlock represents a TableBasedImage.
        /// </summary>
        public bool IsTableBasedImage
        {
            get
            {
                return this is TableBasedImage;
            }
        }

        /// <summary>
        /// Determines whether or not a GraphicRenderingBlock represents a PlainTextExtension.
        /// </summary>
        public bool IsPlainTextExtension
        {
            get
            {
                return this is PlainTextExtension;
            }
        }

        /// <summary>
        /// Casts the GraphicRenderingBlock into a TableBasedImage.
        /// </summary>
        public TableBasedImage ToTableBasedImage()
        {
            return (TableBasedImage)this;
        }

        /// <summary>
        /// Casts the GraphicRenderingBlock into a PlainTextExtension.
        /// </summary>
        public PlainTextExtension ToPlainTextExtension()
        {
            return (PlainTextExtension)this;
        }
    }
}
