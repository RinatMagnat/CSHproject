using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TestVersionWorkTestMashin.Controller
{
    class ConnectionString
    {
        public static string connstr {
            get { return ConfigurationManager.ConnectionStrings["TestVersionWorkTestMashin.Properties.Settings.connstr"].ConnectionString; }
        }
    }
}
