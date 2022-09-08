using System;

namespace AzRanger.Checks
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class CISM365Attribute : Attribute
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public String Section { get; private set; }
        public Level Level { get; set; }
        public String Version { get; set; }


        public CISM365Attribute(string section, string title, Level level, String version)
        {
            this.Id = 0;
            this.Title = title;
            this.Section = section;
            this.Level = Level;
            this.Version = version;
        }
    }

    enum Level
    {
        L1,
        L2
    }

}