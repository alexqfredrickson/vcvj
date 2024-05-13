using LzwGifTools;
using vcvj.Models;

namespace vcvj.Tests
{

    [TestClass]
    public class ImageDataTests
    {
        private VcvjImage vcvj { get; set; }

        [TestInitialize]
        public void Init()
        {
            vcvj = new VcvjImage(Config.SourceImagePath);
        }

        [TestMethod, Ignore]
        public void SaveImageData()
        {
            File.WriteAllBytes(Config.DestinationImagePath, vcvj.DataStream.GetTableBasedImages().First().ImageData.CompressedLzwEncodedIndexStream);
            Assert.Inconclusive();
        }

        [TestMethod, Ignore]
        public void LzwDecodingAndVariableWidthDecompressionSuccessful()
        {
            byte[] imageData = vcvj.DataStream.GetTableBasedImages().First().ImageData.Bytes;
            int lzwSize = vcvj.DataStream.GetTableBasedImages().First().ImageData.LzwMinimumCodeSize;

            LzwDecompressor decompressor = new((byte)lzwSize);
            Decoder decoder = new((byte)lzwSize);

            List<int> decompressedStream = decompressor.Decompress(imageData.Select((b) => (int)b).ToList<int>());
            List<int> decodedStream = decoder.Decode(decompressedStream.Select(x => (byte)x).ToList<byte>());

            // assuming no error is thrown, we're... probably good. yeah this is kind of 
            // an awkward test until we can derive a lzw code table from the global 
            // color index and re-encode everything to match the original bytestream
            Assert.IsTrue(decodedStream.Count > imageData.Length);
        }
    }
}
