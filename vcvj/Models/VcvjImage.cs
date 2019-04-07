using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using vcvj.Models.Grammatical_Components;
using vcvj.Models.Grammatical_Subcomponents;
using vcvj.Parsers;

namespace vcvj.Models
{
    public class VcvjImage
    {
        #region Fields
        /// <summary>
        /// The name of the folder that the source image is located in.
        /// </summary>
        public string InputFolderName { get; set; }

        /// <summary>
        /// The name of the source image file (excluding extension).
        /// </summary>
        public string InputFileName { get; set; }

        public string InputFilePath { get { return InputFolderName + InputFileName; } }

        /// <summary>
        /// Represents the tokenized GIF image's constituent tokens.
        /// </summary>
        public DataStream DataStream { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Compiles the parsed tokens/components back into a byte array.
        /// </summary>
        public void CompileByteArray()
        {
            List<byte> output = new List<byte>();

            output.AddRange(DataStream.Header.Bytes);
            output.AddRange(DataStream.LogicalScreen.LogicalScreenDescriptor.Bytes);

            if (DataStream.LogicalScreen.GlobalColorTable != null)
            {
                output.AddRange(DataStream.LogicalScreen.GlobalColorTable.Bytes);
            }

            foreach (DataBlock db in DataStream.DataBlocks)
            {
                if (db is GraphicBlock)
                {
                    GraphicBlock gb = (GraphicBlock)db;

                    if (gb.GraphicControlExtension != null)
                    {
                        output.AddRange(gb.GraphicControlExtension.Bytes);
                    }

                    if (gb.GraphicRenderingBlock is PlainTextExtension)
                    {
                        PlainTextExtension pte = (PlainTextExtension)gb.GraphicRenderingBlock;

                        output.AddRange(pte.Bytes);
                    }
                    else if (gb.GraphicRenderingBlock is TableBasedImage)
                    {
                        TableBasedImage tbi = (TableBasedImage)gb.GraphicRenderingBlock;

                        output.AddRange(tbi.ImageDescriptor.Bytes);

                        if (tbi.LocalColorTable != null)
                        {
                            output.AddRange(tbi.LocalColorTable.Bytes);
                        }

                        output.AddRange(tbi.ImageData.Bytes);
                    }
                }
                else if (db is SpecialPurposeBlock)
                {
                    SpecialPurposeBlock spb = (SpecialPurposeBlock)db;

                    if (spb is ApplicationExtension)
                    {
                        ApplicationExtension ae = (ApplicationExtension)spb;

                        output.AddRange(ae.Bytes);
                    }
                    else if (spb is CommentExtension)
                    {
                        CommentExtension ce = (CommentExtension)spb;

                        output.AddRange(ce.Bytes);
                    }
                }
            }

            output.AddRange(DataStream.Trailer.Bytes);

            DataStream.Bytes = output.ToArray();
        }

        /// <summary>
        /// Saves the image to the source image folder, using the name of the original source image and an optional suffix to append to the output file.
        /// </summary>
        /// <param name="suffix">An optional suffix to append to the end of the new file name (e.g. ' - modified').</param>
        /// <param name="overwriteExistingImage">Overwrites the existing image located at the new file path (default == false).</param>
        public void Save(string suffix = "", bool overwriteExistingImage = false)
        {
            using (MemoryStream memstr = new MemoryStream(DataStream.Bytes))
            {
                Image i = Image.FromStream(memstr);

                string filepath = string.Empty;

                if (!String.IsNullOrEmpty(suffix))
                {
                    if (suffix.EndsWith(".gif"))
                    {
                        suffix = suffix.Replace(".gif", String.Empty);
                    }

                    filepath = InputFilePath.ToLower().Replace(".gif", suffix += ".gif");
                }

                if (File.Exists(filepath))
                {
                    if (overwriteExistingImage)
                    {
                        i.Save(filepath);
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            @"The save operation failed because an image already exists at the specified location.  Images cannot be overwritten unless explicitly specified, as VCVJ is known to cause irreparable harm to GIF images."
                        );
                    }
                }
                else
                {
                    i.Save(filepath);
                }
            }
        }

        /// <summary>
        /// Saves the image to a fully qualified path.
        /// </summary>
        /// <param name="filepath">The fully qualified path to the new file (e.g. 'C:\Pictures\Balloons.gif').</param>        
        /// <param name="overwriteExistingImage">Overwrites the existing image located at the new file path (default == false).</param>
        public void SaveToPath(string filepath, bool overwriteExistingImage = false)
        {
            using (MemoryStream memstr = new MemoryStream(DataStream.Bytes))
            {
                Image i = Image.FromStream(memstr);

                if (File.Exists(filepath))
                {
                    if (overwriteExistingImage)
                    {
                        i.Save(filepath);
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            @"The save operation failed because an image already exists at the specified location.  Images cannot be overwritten unless explicitly specified, as VCVJ is known to cause irreparable harm to GIF images."
                        );
                    }
                }
                else
                {
                    i.Save(filepath);
                }
            }
        }

        /// <summary>
        /// Compiles the data stream and saves the image to the input folder with a new file name.
        /// </summary>
        /// <param name="overwriteExistingImage">Overwrites the existing image located at the new file path (default == false).</param>
        /// <param name="suffix">An optional suffix to append to the end of the new file name (e.g. ' - modified').</param>
        public void CompileAndSave(string suffix = "", bool overwriteExistingImage = false)
        {
            CompileByteArray();
            Save(suffix, overwriteExistingImage);
        }

        /// <summary>
        /// Compiles the data stream and saves the image to a fully qualified path.
        /// </summary>
        /// <param name="overwriteExistingImage">Overwrites the existing image located at the new file path (default == false).</param>
        /// <param name="filePath">The fully qualified path to the new file (e.g. 'C:\Pictures\Balloons.gif').</param>
        public void CompileAndSaveToPath(string filePath, bool overwriteExistingImage = false)
        {
            CompileByteArray();
            SaveToPath(filePath, overwriteExistingImage);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new VcVjImage object from a GIF image located at the specified file path.
        /// </summary>
        /// <param name="filename">The fully qualified path to the file.</param>
        public VcvjImage(string inputFilePath)
        {
            // sanitize input
            inputFilePath = inputFilePath.Replace(".gif", "");
            inputFilePath += ".gif";

            InputFolderName = inputFilePath.Substring(0, inputFilePath.LastIndexOf(@"\"));
            InputFileName = inputFilePath.Substring(inputFilePath.LastIndexOf(@"\", inputFilePath.Length));

            Stream stream = GlobalUtils.ConvertImageToStream(Image.FromFile(inputFilePath), ImageFormat.Gif);

            using (MemoryStream ms = new MemoryStream())
            {
                DataStream = new DataStream();
                stream.CopyTo(ms);
                ms.Position = 0;
                DataStream.Bytes = ms.ToArray();
            }

            DataStream = VcvjParser.ParseDataStream(DataStream.Bytes);
        }

        /// <summary>
        /// Creates a new VcVjImage object from a GIF image located at the specified folder with the specified file name.
        /// </summary>
        /// <param name="folderName">The folder containing the GIF file (including trailing backslash, e.g. 'C:\Pictures\').</param>
        /// <param name="fileName">The name of the GIF file (including the extension, e.g. 'Balloons.gif').</param>
        public VcvjImage(string folderName, string fileName)
        {
            if (folderName.EndsWith(@"\") == false)
            {
                folderName += @"\";
            }

            if (fileName.ToLower().EndsWith(".gif") == false)
            {
                fileName += ".gif";
            }

            InputFolderName = folderName;
            InputFileName = fileName;

            Stream stream = GlobalUtils.ConvertImageToStream(Image.FromFile(InputFolderName + InputFileName), ImageFormat.Gif);

            using (MemoryStream ms = new MemoryStream())
            {
                DataStream = new DataStream();
                stream.CopyTo(ms);
                ms.Position = 0;
                DataStream.Bytes = ms.ToArray();
            }

            DataStream = VcvjParser.ParseDataStream(DataStream.Bytes);
        }
        #endregion
    }
}
