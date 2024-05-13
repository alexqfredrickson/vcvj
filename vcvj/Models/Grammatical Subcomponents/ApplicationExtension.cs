using System.Text;
using vcvj.Models.Enums;
using vcvj.Models.Grammatical_Components;

namespace vcvj.Models.Grammatical_Subcomponents
{
    /// <summary>
    /// An extension which contains application-specific instructions, including the number of times an animated GIF image should loop.
    /// </summary>
    public partial class ApplicationExtension : SpecialPurposeBlock
    {
        #region Fields
        public byte[] Bytes { get; set; }
        public int TotalBlockLength { get; set; }

        public byte ExtensionIntroducer { get; set; }
        public byte ExtensionLabel { get; set; }

        public int MainBlockSize { get; set; }
        public byte[] MainBlock { get; set; }

        public int SubBlockSize { get; set; }
        public byte[] SubBlock { get; set; }

        public const int ApplicationIdentifierSize = 8;
        public const int ApplicationAuthenticationCodeSize = 3;

        public string ApplicationIdentifier
        {
            get
            {
                return Encoding.UTF8.GetString(
                    MainBlock
                        .Take(ApplicationExtension.ApplicationIdentifierSize)
                        .ToArray()
                );
            }
        }

        public string ApplicationAuthenticationCode
        {
            get
            {
                return Encoding.UTF8.GetString(
                    MainBlock
                        .Skip(ApplicationExtension.ApplicationIdentifierSize)
                        .Take(ApplicationExtension.ApplicationAuthenticationCodeSize)
                        .ToArray()
                );
            }
        }
        #endregion

        #region Methods

        public bool IsNetscapeExtension()
        {
            return ApplicationIdentifier == "NETSCAPE" && ApplicationAuthenticationCode == "2.0";
        }

        public bool IsXmpDataExtension()
        {
            return ApplicationIdentifier == "XMP Data" && ApplicationAuthenticationCode == "XMP";
        }

        /// <summary>
        /// An XMP Data extensions ends with the hideous "magic trailer" '255, 254, 253, ... 2, 1, 0, 0'.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int? GetXmpExtensionBlockTerminatorIndex(byte[] bytes)
        {
            for (int i = 1; i < bytes.Length; i++)
            {
                if (bytes[i - 1] == (byte)Global.BlockTerminator &&
                    bytes[i] == (byte)Global.BlockTerminator)
                {
                    return i;
                }
            }
            return null;
        }
        #endregion
    }
}
