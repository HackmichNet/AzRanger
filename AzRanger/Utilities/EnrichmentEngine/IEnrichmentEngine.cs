using AzRanger.Models;

namespace AzRanger.Utilities.EnrichmentEngine
{
    internal interface IEnrichmentEngine
    {
        // Do some data enrichment
        void Enrich(Tenant tenant);
    }
}
