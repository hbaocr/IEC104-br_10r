using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;
namespace IEC104_dotnet
{
    class MyUltil
    {
        public static string new_line = System.Text.Encoding.ASCII.GetString(new byte[] { (byte)13, (byte)10 });

        
        public static void SetText(TextBox txt, string text)
        {
            if (txt != null)
            {
                txt.Invoke((MethodInvoker)(() => txt.Text = (text)));
            }
           
        }
        public static void AppendText(TextBox txt, string text)
        {
            if (txt != null)
            {
                txt.Invoke((MethodInvoker)(() => txt.AppendText(text + MyUltil.new_line)));
            }

        }

        private static Object thisLock = new Object();  
        static public  void pushlog( string log_str)
        {
            lock (thisLock)
            {
                DateTime dt = DateTime.Now;
                StreamWriter w = File.AppendText("log.txt");
                w.WriteLine(dt.ToString("dd/MM/yy HH:mm:ss") + "   " + log_str);
                w.Close();
            }
        
        }

        public static string byteArrayToHexString(byte[] ba)
        {
            if (ba == null) return "";
            if (ba.Length == 0) return "";
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat(" {0:x2}", b);
            return hex.ToString();
        }
        public static string byteArrayToHexString(byte[] ba,int len)
        {
            if (ba == null) return "";
            if (ba.Length == 0) return "";
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            for (int i = 0; i < len;i++ )
                hex.AppendFormat(" {0:x2}", ba[i]);
            return hex.ToString();
        }
        public static byte[] getBytesFromString(string str)
        {
            if (str == null) return null;
            if (str.Length == 0) return null;
            byte[] array = Encoding.ASCII.GetBytes(str);
            return array;
        }
        static public string byteArray2String(byte[] arr)
        {
            string str = System.Text.Encoding.ASCII.GetString(arr);
            return str;
        }

        public static string Prefix_send_str(int CmdRecCnt)
        {

            // String.Format("{0:D6}", CmdRecCnt);
            return (System.Text.Encoding.ASCII.GetString(new byte[] { (byte)13, (byte)10, (byte)13, (byte)10 }) + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff") + "<----- REQ:       "/*+String.Format("{0:D4} req:       ", CmdRecCnt)*/ + System.Text.Encoding.ASCII.GetString(new byte[] { (byte)13, (byte)10 }) + "        ");
        }
        public static string Prefix_rec_str()
        {
            string tmp = System.Text.Encoding.ASCII.GetString(new byte[] { (byte)13, (byte)10 }) + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff") + "-----> ANS:       " + System.Text.Encoding.ASCII.GetString(new byte[] { (byte)13, (byte)10 }) + "        ";
            return tmp;
        }

    
    
    }
}
