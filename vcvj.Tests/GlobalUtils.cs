using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vcvj.Tests
{
    public static class GlobalUtils
    {
        public static void ToClipboard(this byte[] arr)
        {
            string result = String.Empty;

            for (int i = 0; i < arr.Length; i++)
            {
                result += arr[i].ToString();
                if (i != arr.Length - 1)
                {
                    result += ", ";
                }
            }

            Clipboard.SetText(result);
        }
    }
}
