using System;
using System.Collections.Generic;
using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Models.Grammatical_Components
{
    public class DataStream
    {
        #region Fields

        public Header Header = new Header();
        public LogicalScreen LogicalScreen = new LogicalScreen();
        public List<DataBlock> DataBlocks = new List<DataBlock>();
        public Trailer Trailer = new Trailer();
        public int DataBlocksStartingIndex
        {
            get
            {
                return Header.TotalBlockLength + LogicalScreenDescriptor.TotalBlockLength + 1;
            }
        }
        public byte[] Bytes { get; set; }

        #endregion

        #region Methods/Transformers

        /// <summary>
        /// Returns the length of the parsed data stream byte[].
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            int length = 0;
            length += Header.TotalBlockLength + LogicalScreenDescriptor.TotalBlockLength + Trailer.TotalBlockLength;

            if (LogicalScreen.GlobalColorTable != null)
            {
                length += LogicalScreen.GlobalColorTable.TotalBlockLength;
            }

            foreach (DataBlock db in DataBlocks)
            {
                if (db is GraphicBlock)
                {
                    GraphicBlock gb = (GraphicBlock)db;

                    if (gb.GraphicControlExtension != null)
                    {
                        length += GraphicControlExtension.TotalBlockLength;
                    }

                    if (gb.GraphicRenderingBlock is PlainTextExtension)
                    {
                        PlainTextExtension pte = (PlainTextExtension)gb.GraphicRenderingBlock;

                        length += pte.TotalBlockLength;
                    }
                    else if (gb.GraphicRenderingBlock is TableBasedImage)
                    {
                        TableBasedImage tbi = (TableBasedImage)gb.GraphicRenderingBlock;

                        length += tbi.ImageData.TotalBlockLength + ImageDescriptor.TotalBlockLength;

                        if (tbi.LocalColorTable != null)
                        {
                            length += tbi.LocalColorTable.TotalBlockLength;
                        }
                    }
                }
                else if (db is SpecialPurposeBlock)
                {
                    SpecialPurposeBlock spb = (SpecialPurposeBlock)db;

                    if (spb is ApplicationExtension)
                    {
                        ApplicationExtension ae = (ApplicationExtension)spb;

                        length += ae.TotalBlockLength;
                    }
                    else if (spb is CommentExtension)
                    {
                        CommentExtension ce = (CommentExtension)spb;

                        length += ce.TotalBlockLength;
                    }
                }
            }

            return length;
        }

        /// <summary>
        /// Returns a list of all GraphicBlocks in the data stream.
        /// </summary>
        /// <returns></returns>
        public List<GraphicBlock> GetGraphicBlocks()
        {
            List<GraphicBlock> blocks = new List<GraphicBlock>();

            foreach (DataBlock b in DataBlocks)
            {
                if (b.IsGraphicBlock)
                {
                    blocks.Add(b.ToGraphicBlock());
                }
            }

            return blocks;
        }

        /// <summary>
        /// Returns a list of TableBasedImages in the data stream.
        /// </summary>
        /// <returns></returns>
        public List<TableBasedImage> GetTableBasedImages()
        {
            List<TableBasedImage> tableBasedImages = new List<TableBasedImage>();

            foreach (DataBlock b in DataBlocks)
            {
                if (b.HasTableBasedImage)
                {
                    tableBasedImages.Add(b.GetTableBasedImage());
                }
            }

            return tableBasedImages;
        }

        /// <summary>
        /// Returns a list of every grammatical component in the data stream except for GraphicBlocks.
        /// </summary>
        /// <returns></returns>
        public List<DataBlock> GetNonGraphicBlocks()
        {
            List<DataBlock> blocks = new List<DataBlock>();

            foreach (DataBlock b in DataBlocks)
            {
                if (b.IsGraphicBlock == false)
                {
                    blocks.Add(b);
                }
            }

            return blocks;
        }

        /// <summary>
        /// Removes all image frames from the data stream.
        /// </summary>
        /// <returns></returns>
        public DataStream RemoveAllTableBasedImages()
        {
            List<DataBlock> tempDataBlocks = new List<DataBlock>();
            DataBlocks.ForEach(x => tempDataBlocks.Add(x));

            tempDataBlocks.RemoveAll(x => (x is GraphicBlock) == false);

            DataBlocks = tempDataBlocks;
            return this;
        }
      
        #endregion
    }
}
