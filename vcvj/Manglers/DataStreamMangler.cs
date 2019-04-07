using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vcvj.Models;
using vcvj.Models.Grammatical_Components;
using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Manglers
{
    public class DataStreamMangler
    {
        private VcvjImage VcvjImage { get; set; }
        private DataStream DataStream { get; set; }

        public DataStreamMangler(ref VcvjImage vcvjImage)
        {
            VcvjImage = vcvjImage;
            DataStream = VcvjImage.DataStream;
        }

        /// <summary>
        /// Randomizes the frames of the data stream.
        /// </summary>
        /// <returns></returns>
        public void RandomizeFrames()
        {
            Random rng = new Random();
            List<DataBlock> tempDataBlocks = new List<DataBlock>();
            DataStream.DataBlocks.ForEach(x => tempDataBlocks.Add(x));

            int n = tempDataBlocks.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);

                DataBlock block = tempDataBlocks[k];

                if (tempDataBlocks[k].HasTableBasedImage)
                {
                    tempDataBlocks[k] = tempDataBlocks[n];
                    tempDataBlocks[n] = block;
                }
            }

            DataStream.DataBlocks = tempDataBlocks;
        }

        /// <summary>
        /// Reverses the order of DataBlocks in the data stream.
        /// </summary>
        /// <returns></returns>
        public void ReverseDataBlocks()
        {
            DataStream.DataBlocks.Reverse();
        }

        /// <summary>
        /// Reverses the order of the frames in the data stream.
        /// </summary>
        /// <returns></returns>
        public void ReverseFrames()
        {
            List<DataBlock> tempFrames = new List<DataBlock>();

            foreach (DataBlock db in DataStream.DataBlocks)
            {
                if (db.HasTableBasedImage)
                {
                    tempFrames.Add(db);
                }
            }

            tempFrames.Reverse();

            int index = 0;

            for (int i = 0; i < DataStream.DataBlocks.Count; i++)
            {
                if (DataStream.DataBlocks[i].HasTableBasedImage)
                {
                    DataStream.DataBlocks[i] = tempFrames[index];
                    index++;
                }
            }
        }

        /// <summary>
        /// Deletes random data from the compiled byte array.
        /// </summary>
        /// <param name="chunkLength"></param>
        /// <returns></returns>
        public void DeleteRandomCompiledBytes(int chunkLength)
        {
            if (DataStream.Bytes == null)
            {
                throw new NullReferenceException("The DataStream byte array is null.");
            }

            Random r = new Random();

            int startIndex = DataStream.DataBlocksStartingIndex;
            int endIndex = DataStream.Bytes.Length - (chunkLength + Trailer.TotalBlockLength);
            int randomIndex = r.Next(startIndex, endIndex);

            byte[] startArray = GlobalUtils.SubArray(DataStream.Bytes, 0, randomIndex);
            byte[] endArray = GlobalUtils.SubArray(DataStream.Bytes, randomIndex + chunkLength, DataStream.Bytes.Length - (randomIndex + chunkLength));

            var newBytes = new byte[startArray.Length + endArray.Length];
            startArray.CopyTo(newBytes, 0);
            endArray.CopyTo(newBytes, startArray.Length);

            DataStream.Bytes = newBytes;
        }

        /// <summary>
        /// Abridges the length of a GIF by removing a specified percentage (in the range of 0 - 100, as percentage) of the data stream's images.
        /// </summary>
        /// <param name="abridgementPercentage">The percentage of the data stream's images to remove (from 0 - 100, i.e. '25' = 25%).</param>
        /// <returns></returns>
        public void Abridge(int abridgementPercentage)
        {
            if (abridgementPercentage < 0 || abridgementPercentage > 100)
            {
                throw new FormatException("The A value of 0 to 100 must be specified.");
            }
            else
            {
                // initialize the new stream by adding every bock that isn't a graphic block
                List<DataBlock> newStream = DataStream.GetNonGraphicBlocks();

                // grab just the images from the data stream
                List<GraphicBlock> graphicBlocks = DataStream.GetGraphicBlocks();

                decimal percentage = (decimal)abridgementPercentage / 100;
                int imagesCount = graphicBlocks.Count;
                int imagesToRemoveCount = (int)Math.Floor(imagesCount * percentage);

                graphicBlocks.RemoveRange(imagesCount - imagesToRemoveCount, imagesToRemoveCount);

                newStream.AddRange(graphicBlocks);
                DataStream.DataBlocks = newStream;
            }
        }
    }
}
