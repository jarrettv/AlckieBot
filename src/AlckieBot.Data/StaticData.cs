using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Data
{
    public static class StaticData
    {
        public static string ClashCallerCode { get; set; }

        public static void Init()
        {
            ClashCallerCode = ConfigurationManager.AppSettings["CLASH_CALLER_CODE"];
        }        
    }
}
