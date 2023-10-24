using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Output
{
    public class AffectedItem
    {
        public AffectedItem(string id, string name)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; }
        public String Id { get; }
    }
}
