using System;
using System.IO;
using vcvj.Exceptions;
using vcvj.Models.Grammatical_Components;
using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Parsers
{
    public static partial class VcvjParser
    {
        /// <summary>
        /// Tokenizes the GIF image's byte[] into constituent components.
        /// </summary>
        public static DataStream ParseDataStream(byte[] data)
        {
            DataStream ds = new DataStream();

            using (MemoryStream ms = new MemoryStream(data))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    ds.Header = VcvjParser.ParseHeader(br.ReadBytes(Header.TotalBlockLength));

                    ds.LogicalScreen = new LogicalScreen()
                    {
                        LogicalScreenDescriptor = VcvjParser.ParseLogicalScreenDescriptor(br.ReadBytes(LogicalScreenDescriptor.TotalBlockLength))
                    };

                    if (ds.LogicalScreen.LogicalScreenDescriptor.HasGlobalColorTable)
                    {
                        ds.LogicalScreen.GlobalColorTable = VcvjParser.ParseGlobalColorTable(
                            br.ReadBytes(ds.LogicalScreen.LogicalScreenDescriptor.GlobalColorTableLength)
                        );
                    }

                    while (br.PeekChar() != -1)
                    {
                        long currentIndex = br.BaseStream.Position;
                        byte currentByte = data[currentIndex];
                        byte? nextByte = currentIndex + 1 < data.Length ? (byte?)data[currentIndex + 1] : (byte?)null;

                        if (ParserUtils.IsTrailerMarker(currentByte))
                        {
                            ds.Trailer = new Trailer(br.ReadByte());
                            continue;
                        }
                        else if (ParserUtils.IsApplicationExtensionBlock(currentByte, nextByte))
                        {
                            ApplicationExtension ae = VcvjParser.ParseApplicationExtension(
                                br.ReadBytes(VcvjParser.GetApplicationExtensionBlockLength(currentIndex, ref data))
                            );

                            ds.DataBlocks.Add(ae);
                            continue;
                        }
                        else if (ParserUtils.IsCommentExtensionBlock(currentByte, nextByte))
                        {
                            int totalBlockLength = data[currentIndex + 2] + 4;
                            CommentExtension ce = VcvjParser.ParseCommentExtension(br.ReadBytes(totalBlockLength));

                            ds.DataBlocks.Add(ce);
                            continue;
                        }
                        else
                        {
                            GraphicBlock gb = new GraphicBlock();

                            if (ParserUtils.IsGraphicControlExtensionBlock(currentByte, nextByte))
                            {
                                gb.GraphicControlExtension = VcvjParser.ParseGraphicControlExtension(
                                    br.ReadBytes(8)
                                );

                                currentIndex = br.BaseStream.Position;
                                currentByte = data[currentIndex];
                                nextByte = data[currentIndex + 1];
                            }

                            if (ParserUtils.IsImageDescriptor(currentByte))
                            {
                                TableBasedImage tbi = new TableBasedImage()
                                {
                                    ImageDescriptor = VcvjParser.ParseImageDescriptor(
                                        br.ReadBytes(10)
                                    )
                                };

                                if (tbi.ImageDescriptor.LocalColorTableFlag)
                                {
                                    tbi.LocalColorTable = VcvjParser.ParseLocalColorTable(
                                        br.ReadBytes(tbi.ImageDescriptor.LocalColorTableLength)
                                    );
                                }

                                currentIndex = br.BaseStream.Position;
                                currentByte = data[currentIndex];
                                nextByte = data[currentIndex + 1];

                                tbi.ImageData = VcvjParser.ParseImageData(
                                    br.ReadBytes(VcvjParser.GetImageDataBlockLength(currentIndex, ref data))
                                );

                                gb.GraphicRenderingBlock = tbi;

                                ds.DataBlocks.Add(gb);
                                continue;
                            }
                            else if (ParserUtils.IsPlaintextExtension(currentByte, nextByte))
                            {
                                //GraphicBlock gb = new GraphicBlock()
                                //{
                                //};
                            }
                            else if (ParserUtils.IsApplicationExtensionBlock(currentByte, nextByte))
                            {
                                ApplicationExtension ae = VcvjParser.ParseApplicationExtension(
                                    br.ReadBytes(VcvjParser.GetApplicationExtensionBlockLength(currentIndex, ref data))
                                );

                                ds.DataBlocks.Add(ae);
                                continue;
                            }
                            else if (ParserUtils.IsCommentExtensionBlock(currentByte, nextByte))
                            {
                                int totalBlockLength = data[currentIndex + 2] + 4;
                                CommentExtension ce = VcvjParser.ParseCommentExtension(br.ReadBytes(totalBlockLength));

                                ds.DataBlocks.Add(ce);
                                continue;
                            }
                            else
                            {
                                throw new UnidentifiedBlockException(
                                    string.Format(
                                        "The current byte is {0} and the next one is {1}. Not really sure what to make of this.",
                                        currentByte.ToString(),
                                        String.IsNullOrEmpty(nextByte.ToString()) ? "null" : nextByte.ToString()
                                    )
                                );
                            }
                        }
                    }

                }
            }
            return ds;
        }
    }
}
