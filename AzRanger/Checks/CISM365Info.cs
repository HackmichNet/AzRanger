using System;

namespace AzRanger.Checks
{
    internal enum CISLevel
    {
        L1,
        L2,
    }

    internal class CISM365Info
    {
        public string Section { get; private set; }
        public CISLevel Level { get; private set; }
        public string Version { get; private set; }

        public static bool TryGet(string identifier, out CISM365Info info)
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

            info = new CISM365Info();
            info.Section = section.GetStringOrNull("CISM365Section");
            info.Version = section.GetStringOrNull("CISM365Level");

            if (!Enum.TryParse(section.GetStringOrNull("CISM365version"), out CISLevel level))
            {
                info = null;
                return false;
            }

            info.Level = level;
            return true;
        }
    }
}
