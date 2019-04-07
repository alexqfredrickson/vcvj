using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vcvj.Manglers;
using vcvj.Models;

namespace vcvj.Tests
{
    [TestClass]
    public class VcvjImageManglerTests
    {
        private VcvjImage vcvj { get; set; }
        private VcvjImageMangler VcvjImageMangler { get; set; }

        [TestInitialize]
        public void Init()
        {
            vcvj = new VcvjImage(Config.SourceImagePath);
            VcvjImageMangler = new VcvjImageMangler(vcvj);
        }

        [TestMethod]
        public void ColorTableXORSuccessful()
        {
            VcvjImageMangler.XorGlobalColorTable(50);
            vcvj.CompileAndSave(" - colors inverted (XOR 50)", true);

            VcvjImageMangler.GlobalColorTableMangler.XOR(100);
            vcvj.CompileAndSave(" - colors inverted (XOR 100)", true);
        }

        [TestMethod]
        public void ColorTableRandomizationSuccessful()
        {
            VcvjImageMangler.RandomizeGlobalColorTable();
            vcvj.CompileAndSave(" - colors randomized", true);
        }

        [TestMethod]
        public void ColorTable50PercentRandomizationSuccessful()
        {
            VcvjImageMangler.RandomizeHalfGlobalColorTable();
            vcvj.CompileAndSave(" - colors partially randomized", true);
        }

        [TestMethod]
        public void DataStreamAbridgementSuccessful()
        {
            VcvjImageMangler.AbridgeStream(90);
            vcvj.CompileAndSave(" - 90% abridged", true);
        }

        [TestMethod]
        public void DataStreamRandomizationSuccessful()
        {
            VcvjImageMangler.RandomizeFrames();
            vcvj.CompileAndSave(" - shuffled", true);
        }

        [TestMethod]
        public void DataStreamReversalSuccessful()
        {
            vcvj.CompileByteArray();
            VcvjImageMangler.ReverseFrames();
            vcvj.CompileAndSaveToPath(" - reversed", true);
        }

        [TestMethod]
        public void DataStreamRandomByteDeletionSuccessful()
        {
            vcvj.CompileByteArray();

            for (int i = 1; i <= 5; i++)
            {
                int count = 25000 * i;
                VcvjImageMangler.DeleteBytes(count);
                vcvj.Save(string.Format(" - {0} liberated", count.ToString() + " bytes"), true);
            }
        }
    }
}
