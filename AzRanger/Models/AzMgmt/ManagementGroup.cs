using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
 public class ManagementGroup
{
    public string id { get; set; }
    public string type { get; set; }
    public string name { get; set; }
    public ManagementGroupProperties properties { get; set; }
    public List<Subscription> Subscriptions { get; set; }
}

public class ManagementGroupProperties
    {
    public string tenantId { get; set; }
    public string displayName { get; set; }
    public ManagementGroupDetails details { get; set; }
    public ManagementGroupChild[] children { get; set; }
}

public class ManagementGroupDetails
    {
    public int version { get; set; }
    public DateTime updatedTime { get; set; }
    public string updatedBy { get; set; }
}

public class ManagementGroupChild
    {
    public string id { get; set; }
    public string type { get; set; }
    public string name { get; set; }
    public string displayName { get; set; }
}
}