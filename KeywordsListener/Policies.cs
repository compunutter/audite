using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audite
{
    public class Policies
    {
        private const string KEY_ROOT = "HKEY_CURRENT_USER\\Software\\Policies\\Audite";

        private static string get(string valueName)
        {
            return (string)Registry.GetValue(KEY_ROOT, valueName, null);
        }

        public static string[] GetTriggers()
        {
            string result = get("Triggers");
            return result != null ? result.Split(',')
                                          .Select(s => s.Trim().ToUpper())
                                          .Where(s => s != String.Empty)
                                          .ToArray()
                                  : new string[] { "PROXY" };
        }

        public static string[] GetApps()
        {
            string result = get("Apps");
            return result != null ? result.Split(',')
                                          .Select(s => s.Trim())
                                          .Where(s => s != String.Empty)
                                          .ToArray()
                                  : new string[] { "Google Chrome", "Internet Explorer" };
        }

        public static string GetWarningMsg()
        {
            string result = get("WarningMsg");
            return !string.IsNullOrEmpty(result) ? result.Trim() : "This school does not tolerate the use of proxy servers or websites.\r\n\r\nYou have been caught using a proxy site, as result your browser has been closed and you have been reported to the IT department.";
        }

        public static string GetBtnLbl()
        {
            string result = get("BtnLbl");
            return !string.IsNullOrEmpty(result) ? result.Trim() : "I understand";
        }
    }
}
