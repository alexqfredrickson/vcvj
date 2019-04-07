using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Models.Grammatical_Components
{
    /// <summary>
    /// A block containing an image descriptor, image data, and a local color table (optional).
    /// </summary>
    public class TableBasedImage : GraphicRenderingBlock
    {
        public ImageDescriptor ImageDescriptor { get; set; }
        public LocalColorTable LocalColorTable { get; set; }
        public ImageData ImageData { get; set; }
    }
}
