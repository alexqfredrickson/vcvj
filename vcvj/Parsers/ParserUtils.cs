using vcvj.Models.Enums;

namespace vcvj.Parsers
{
    public static class ParserUtils
    {
        /// <summary>
        /// Determines whether a block at a given index in the stream represents a graphics control extension.
        /// </summary>
        public static bool IsGraphicControlExtensionBlock(byte currentByte, byte? nextByte)
        {
            if (nextByte == null)
            {
                return false;
            }
            else
            {
                return currentByte == (byte)BlockIntroducer.Extension
                    && nextByte == (byte)BlockLabel.GraphicControlExtension;
            }
        }

        /// <summary>
        /// Determines whether a block at a given index in the stream represents an application extension.
        /// </summary>
        public static bool IsApplicationExtensionBlock(byte currentByte, byte? nextByte)
        {
            if (nextByte == null)
            {
                return false;
            }
            else
            {
                return currentByte == (byte)BlockIntroducer.Extension
                    && nextByte == (byte)BlockLabel.ApplicationExtension;
            }
        }

        /// <summary>
        /// Determines whether a block at a given index in the stream represents a comment extension.
        /// </summary>
        public static bool IsCommentExtensionBlock(byte currentByte, byte? nextByte)
        {
            if (nextByte == null)
            {
                return false;
            }
            else
            {
                return currentByte == (byte)BlockIntroducer.Extension
                    && nextByte == (byte)BlockLabel.CommentExtension;
            }
        }

        /// <summary>
        /// Determines whether a block at a given index in the stream represents an image descriptor.
        /// </summary>
        public static bool IsImageDescriptor(byte currentByte)
        {
            return currentByte == (byte)BlockIntroducer.ImageDescriptor;
        }

        /// <summary>
        /// Determines whether a block at a given index in the stream represents a plain-text extension.
        /// </summary>
        public static bool IsPlaintextExtension(byte currentByte, byte? nextByte)
        {
            if (nextByte == null)
            {
                return false;
            }
            else
            {
                return currentByte == (byte)BlockIntroducer.Extension
                     && nextByte == (byte)BlockLabel.PlainTextExtension;
            }
        }

        /// <summary>
        /// Determines whether the given index represents the image's trailer marker.
        /// </summary>
        public static bool IsTrailerMarker(byte currentByte)
        {
            return currentByte == (byte)BlockIntroducer.Trailer;
        }

        /// <summary>
        /// Determines whether the given index represents the image's header.
        /// </summary>
        public static bool IsHeaderBlock(byte currentByte)
        {
            return currentByte == (byte)BlockIntroducer.Trailer;
        }
    }
}
