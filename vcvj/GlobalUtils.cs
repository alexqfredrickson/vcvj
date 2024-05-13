using System.Text;
using System.Drawing;

namespace vcvj
{
    public static class GlobalUtils
    {
        /// <summary>
        /// Converts an image to a stream.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Stream ConvertImageToStream(Image image, ImageFormat format)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Returns a subsection of a given array.
        /// </summary>
        /// <typeparam name="T">The type of the original array.</typeparam>
        /// <param name="data">The original array.</param>
        /// <param name="index">The starting index of the original array.</param>
        /// <param name="length">The length of the resulting array.</param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] data, long index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        /// <summary>
        /// Converts a byte array to a binary string.
        /// </summary>
        /// <param name="arr">A byte array.</param>
        /// <returns>A binary string.</returns>
        public static string ToBinaryString(this byte[] arr)
        {
            StringBuilder sb = new StringBuilder();

            for (long i = 0; i < arr.Length; i++)
            {
                sb.Append(Convert.ToString(arr[i], 2).PadLeft(8, '0'));
            }

            return sb.ToString();
        }
    }
}