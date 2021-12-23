using System;

namespace AzRanger.Checks
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class CISRuleAttribute : Attribute
    {
        public int Id { get; private set; }
        public CISRuleCategory Category { get; private set; }
        public string Title { get; private set; }
        public String Section { get; private set; }
        public Level Level { get; set; }


        public CISRuleAttribute(CISRuleCategory CISRuleCategory, string section, string title, Level level)
        {
            this.Id = 0;
            this.Category = CISRuleCategory;
            this.Title = title;
            this.Section = section;
            this.Level = Level;
        }
    }

    enum CISRuleCategory
    {
        CISO365,
        CISOAZ
    }

    enum Level
    {
        L1,
        L2
    }

}