using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Core.Utility
{
    public class alLIDInit
    {
        public static string PaperIDInit()
        {
            string i = "Pa"+ (System.DateTime.Now).ToFileTime().ToString();
            Random rd = new Random();
            int a=rd.Next(1, 100);
            i = i + Convert.ToString(a);
            return i;
        }
        public static string InkIDInit()
        {

            string i = "In" + (System.DateTime.Now).ToFileTime().ToString();
            Random rd = new Random();
            int a = rd.Next(1, 100);
            i = i + Convert.ToString(a);
            return i;
        }
        public static string ProductIDInit()
        {

            string i = "Pro" + (System.DateTime.Now).ToFileTime().ToString();
            Random rd = new Random();
            int a = rd.Next(1, 100);
            i = i + Convert.ToString(a);
            return i;
        }
    }
}
