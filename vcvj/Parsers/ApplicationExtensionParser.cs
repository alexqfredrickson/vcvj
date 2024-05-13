using vcvj.Exceptions;
using vcvj.Models.Enums;
using vcvj.Models.Grammatical_Subcomponents;

namespace vcvj.Parsers
{
    public static partial class VcvjParser
    {
        /// <summary>
        /// Populates the application extension byte[] from the source image, and performs lexical parsing on the array.
        /// </summary>
        /// <param name="bytes"></param>
        public static ApplicationExtension ParseApplicationExtension(byte[] bytes)
        {
            ApplicationExtension ae = new ApplicationExtension()
            {
                Bytes = (byte[])bytes.Clone(),
                TotalBlockLength = bytes.Length,
                ExtensionIntroducer = bytes[0],
                ExtensionLabel = bytes[1],
                MainBlockSize = bytes[2],
                MainBlock = new byte[bytes[2]]
            };

            Array.Copy(bytes, 3, ae.MainBlock, 0, ae.MainBlockSize);

            ae.SubBlockSize = ae.Bytes[ae.MainBlockSize + 3];

            if (ae.IsNetscapeExtension())
            {
                ae.SubBlock = new byte[ae.SubBlockSize];
                Array.Copy(bytes, ae.MainBlockSize + 3, ae.SubBlock, 0, ae.SubBlockSize);
            }
            else if (ae.IsXmpDataExtension())
            {
                int blockTerminatorIndex = ApplicationExtension.GetXmpExtensionBlockTerminatorIndex(bytes).Value;

                //  todo: offer propert support for XMP Data extensions
                //  ae.SubBlockSize = blockTerminatorIndex - (ae.MainBlock.Length + 3);
                //  ae.SubBlock = new byte[ae.SubBlockSize];
                //  Array.Copy(bytes, ae.SubBlock, ae.SubBlockSize);
                //  ae.TotalBlockLength = ApplicationExtension.MainBlockSize + ApplicationExtension.SubBlockSize + 4;
            }

            if (bytes[ae.TotalBlockLength - 1] != (byte)Global.BlockTerminator)
            {
                throw new ApplicationExtensionParserException();
            }
            else
            {
                return ae;
            }
        }
    }
}
