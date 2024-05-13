using vcvj.Models;
using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Tests
{
    [TestClass]
    public class VcvjImageTests
    {
        [TestMethod, Ignore]
        public void ImageLoadsCorrectly()
        {
            VcvjImage vcvj = new VcvjImage(Config.SourceImagePath);
            Assert.IsTrue(true);
        }

        [TestMethod, Ignore]
        public void ImageCompilationSuccessful()
        {
            VcvjImage vcvj1 = new VcvjImage(Config.SourceImagePath);
            vcvj1.CompileAndSave(" - compiled", true);

            VcvjImage vcvj2 = new VcvjImage(Config.SourceImagePath + " - compiled");
            vcvj2.CompileByteArray();

            Assert.IsTrue(vcvj1.DataStream.Bytes.Length == vcvj2.DataStream.Bytes.Length);
        }
        
        [TestMethod, Ignore]
        public void ExportColorTableToFile()
        {
            VcvjImage vcvj = new VcvjImage(Config.SourceImagePath);
            vcvj.DataStream.LogicalScreen.GlobalColorTable.ExportToTextFile(Config.WorkingDirectory + "\\color-table.txt");
        }

        [TestMethod, Ignore]
        public void ExportIndexStreamToFile()
        {
            VcvjImage vcvj = new VcvjImage(Config.SourceImagePath);
            vcvj.DataStream.GetTableBasedImages().First().ImageData.ExportCodeStreamToTextFile(Config.WorkingDirectory + "\\code-stream.txt");
        }

        [TestMethod, Ignore]
        public void ExportImageDataToFile()
        {
            VcvjImage vcvj = new VcvjImage(Config.SourceImagePath);
            ImageData id = vcvj.DataStream.GetTableBasedImages().First().ImageData;
            id.ExportImageDataAsByteStreamToTextFile(Config.WorkingDirectory + "\\image-bytes.txt");
        }

        [TestMethod, Ignore]
        public void ExportImageDataToFileAsBinaryStringWithLeftPadding()
        {
            VcvjImage vcvj = new VcvjImage(Config.SourceImagePath);
            ImageData id = vcvj.DataStream.GetTableBasedImages().First().ImageData;
            id.ExportImageDataAsBinaryStringToTextFile(Config.WorkingDirectory + "\\lzwcompressedimagedata-binarystring-leftpadding.txt", true);
        }

        [TestMethod, Ignore]
        public void ExportImageDataToFileAsBinaryStringWithRightPadding()
        {
            VcvjImage vcvj = new VcvjImage(Config.SourceImagePath);
            ImageData id = vcvj.DataStream.GetTableBasedImages().First().ImageData;
            id.ExportImageDataAsBinaryStringToTextFile(Config.WorkingDirectory + "\\lzwcompressedimagedata-binarystring-rightpadding.txt", false);
        }
    }
}
