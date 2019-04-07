using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vcvj.Models;
using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Manglers
{
    /// <summary>
    /// A wrapper for VcvjImage-mangling utilities.
    /// </summary>
    public class VcvjImageMangler
    {
        private static VcvjImage VcvjImage { get; set; }
        public GlobalColorTableMangler GlobalColorTableMangler { get; set; }
        public DataStreamMangler DataStreamMangler { get; set; }

        public void XorGlobalColorTable(byte b)
        {
            GlobalColorTableMangler.XOR(b);
        }

        public void RandomizeGlobalColorTable()
        {
            GlobalColorTableMangler.RandomizeColors();
        }

        public void RandomizeHalfGlobalColorTable()
        {
            GlobalColorTableMangler.RandomizeHalfColors();
        }

        public void RandomizeFrames()
        {
            DataStreamMangler.RandomizeFrames();
        }

        public void ReverseFrames()
        {
            DataStreamMangler.ReverseFrames();
        }

        public void ReverseDataBlocks()
        {
            DataStreamMangler.ReverseDataBlocks();
        }

        public void AbridgeStream(int abridgementPercentage)
        {
            DataStreamMangler.Abridge(abridgementPercentage);

        }

        public void DeleteBytes(int chunkLength)
        {
            DataStreamMangler.DeleteRandomCompiledBytes(chunkLength);
        }

        public VcvjImage ToVcvjImage()
        {
            return VcvjImage;
        }

        public VcvjImageMangler(VcvjImage vcvjImage)
        {
            VcvjImage = vcvjImage;
            GlobalColorTableMangler = new GlobalColorTableMangler(ref vcvjImage);
            DataStreamMangler = new DataStreamMangler(ref vcvjImage);
        }
    }
}
