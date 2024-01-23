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
        public string Id { get; }
    }
}
