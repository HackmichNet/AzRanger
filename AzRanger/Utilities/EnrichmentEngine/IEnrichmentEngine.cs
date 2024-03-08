using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Utilities.EnrichmentEngine
{
    internal interface IEnrichmentEngine
    {
        // Do some data enrichment
        void Enrich(Tenant tenant);
    }
}
