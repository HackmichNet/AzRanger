using System;

namespace AzRanger.Checks
{
    internal class CISAzInfo
    {
        public string Section { get; private set; }
        public CISLevel Level { get; private set; }
        public string Version { get; private set; }

        public static bool TryGet(string identifier, out CISAzInfo info)
        {
            if (identifier == null || identifier.Length == 0)
            {
                info = null;
                return false;
            }

            var section = RuleInfoData.GetSectionOrNull(identifier);
            if (section == null)
            {
                info = null;
                return false;
            }

            info = new CISAzInfo();
            info.Section = section.GetStringOrNull("CISAZSection");
            info.Version = section.GetStringOrNull("CISAZVersion");

            if (!Enum.TryParse(section.GetStringOrNull("CISAZLevel"), out CISLevel level))
            {
                info = null;
                return false;
            }

            info.Level = level;
            return true;
        }
    }
}
