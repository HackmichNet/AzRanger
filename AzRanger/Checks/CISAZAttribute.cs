using System;

namespace AzRanger.Checks
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class CISAZAttribute : Attribute
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public String Section { get; private set; }
        public CISLevel Level { get; set; }
        public String Version { get; set; }


        public CISAZAttribute(string section, string title, CISLevel level, string version)
        {
            this.Id = 0;
            this.Title = title;
            this.Section = section;
            this.Level = Level;
            Version = version;
        }
    }
}