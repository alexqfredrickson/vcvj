using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Models.Grammatical_Components
{
    /// <summary>
    /// A block containing a graphic-rendering block and an optional graphic control extension.
    /// </summary>
    public class GraphicBlock : DataBlock
    {
        public GraphicControlExtension GraphicControlExtension { get; set; }
        public GraphicRenderingBlock GraphicRenderingBlock { get; set; }
    }
}
