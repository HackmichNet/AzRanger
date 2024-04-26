using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.ExchangeOnline
{
    public class ExternalInOutlook
    {
        public string Identity { get; set; }
        public bool Enabled { get; set; }
        public string AllowListodatatype { get; set; }
        public object[] AllowList { get; set; }
    }

}
