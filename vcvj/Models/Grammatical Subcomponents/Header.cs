namespace vcvj.Models.Grammatical_Subcomponents
{
    /// <summary>
    /// The block which contains the image's signature and version encoding.
    /// </summary>
    public class Header
    {
        public const int TotalBlockLength = 6;
        public byte[] Bytes = new byte[Header.TotalBlockLength];

    }
}
