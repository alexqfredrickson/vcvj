using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Models.Grammatical_Components
{
    /// <summary>
    /// A block containing the logical screen descriptor and the optional global color table.
    /// </summary>
    public class LogicalScreen
    {
        public LogicalScreenDescriptor LogicalScreenDescriptor { get; set; }
        public GlobalColorTable GlobalColorTable { get; set; }

        public int TotalBlockLength
        {
            get
            {
                return LogicalScreenDescriptor.TotalBlockLength + (GlobalColorTable != null ? GlobalColorTable.TotalBlockLength : 0);
            }
        }
    }
}
