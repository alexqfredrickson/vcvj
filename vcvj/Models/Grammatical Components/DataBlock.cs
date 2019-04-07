using vcvj.Models.Grammatical_Subcomponents;
namespace vcvj.Models.Grammatical_Components
{
    /// <summary>
    /// A generic data block representing either a graphic block or a special purpose block.
    /// </summary>
    public abstract class DataBlock
    {
        /// <summary>
        /// Determines whether or not a given DataBlock represents a GraphicBlock.
        /// </summary>
        public bool IsGraphicBlock
        {
            get
            {
                return this is GraphicBlock;
            }
        }

        /// <summary>
        /// Casts a DataBlock into a GraphicBlock.
        /// </summary>
        public GraphicBlock ToGraphicBlock()
        {
            return (GraphicBlock)this;
        }

        /// <summary>
        /// Determines whether or not a given DataBlock represents a SpecialPurposeBlock.
        /// </summary>
        public bool IsSpecialPurposeBlock
        {
            get
            {
                return this is SpecialPurposeBlock;
            }
        }

        /// <summary>
        /// Casts a DataBlock into a SpecialPurposeBlock.
        /// </summary>
        public SpecialPurposeBlock ToSpecialPurposeBlock()
        {
            return (SpecialPurposeBlock)this;
        }

        /// <summary>
        /// Determines whether or not a given DataBlock represents an ApplicationExtension.
        /// </summary>
        public bool IsApplicationExtension
        {
            get
            {
                if (this.IsSpecialPurposeBlock)
                {
                    return this.ToSpecialPurposeBlock().IsApplicationExtension;
                }

                return false;
            }
        }

        /// <summary>
        /// Casts a DataBlock into an ApplicationExtension.
        /// </summary>
        public ApplicationExtension ToApplicationExtension()
        {
            return (ApplicationExtension)this.ToSpecialPurposeBlock();
        }

        /// <summary>
        /// Determines whether or not a given DataBlock represents a CommentExtension.
        /// </summary>
        public bool IsCommentExtension
        {
            get
            {
                if (this.IsSpecialPurposeBlock)
                {
                    return this.ToSpecialPurposeBlock().IsCommentExtension;
                }

                return false;
            }
        }

        /// <summary>
        /// Casts a DataBlock into a CommentExtension.
        /// </summary>
        public CommentExtension ToCommentExtension()
        {
            return (CommentExtension)this.ToSpecialPurposeBlock();
        }

        /// <summary>
        /// Determines whether or not a given DataBlock represents a GraphicBlock, whose GraphicRenderingBlock represents a TableBasedImage.
        /// </summary>
        public bool HasTableBasedImage
        {
            get
            {
                if (this.IsGraphicBlock)
                {
                    return this.ToGraphicBlock().GraphicRenderingBlock.IsTableBasedImage;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a DataBlock's GraphicRenderingBlock's TableBasedImage.
        /// </summary>
        public TableBasedImage GetTableBasedImage()
        {
            return (TableBasedImage)this.ToGraphicBlock().GraphicRenderingBlock;
        }

        /// <summary>
        /// Determines whether or not a given DataBlock represents a GraphicBlock, whose GraphicRenderingBlock represents a PlainTextExtension.
        /// </summary>
        public bool HasPlainTextExtension
        {
            get
            {
                if (this.IsGraphicBlock)
                {
                    return this.ToGraphicBlock().GraphicRenderingBlock.IsPlainTextExtension;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a DataBlock's GraphicRenderingBlock's PlainTextExtension.
        /// </summary>
        public PlainTextExtension GetPlainTextExtension()
        {
            return (PlainTextExtension)this.ToGraphicBlock().GraphicRenderingBlock;
        }

    }
}
