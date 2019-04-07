namespace vcvj.Models.Grammatical_Subcomponents
{
    /// <summary>
    /// The image's trailer marker.
    /// </summary>
    public class Trailer
    {
        public const int TotalBlockLength = 1;
        public byte[] Bytes = new byte[Trailer.TotalBlockLength];

        public byte Marker { get; set; }

        public Trailer() { }

        /// <summary>
        /// Populates the image's trailer, denoting the end of the data stream.
        /// </summary>
        public Trailer(byte b)
        {
            Marker = b;
            Bytes[0] = b;
        }
    }
}
