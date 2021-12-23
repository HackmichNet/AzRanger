using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class Signinactivity
    {
        public DateTime lastSignInDateTime { get; set; }
        public string lastSignInRequestId { get; set; }
        public DateTime lastNonInteractiveSignInDateTime { get; set; }
        public string lastNonInteractiveSignInRequestId { get; set; }
    }
}
