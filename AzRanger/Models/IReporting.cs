using AzRanger.Output;

namespace AzRanger.Models
{
    public interface IReporting
    {
        string PrintConsole();
        string PrintCSV();
        AffectedItem GetAffectedItem();
    }
}