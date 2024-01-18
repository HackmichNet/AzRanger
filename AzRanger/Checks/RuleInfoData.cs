using System;
using Tomlyn;
using Tomlyn.Model;

namespace AzRanger.Checks
{
    internal static class RuleInfoData
    {
        private readonly static TomlTable _data = Toml.ToModel(Properties.Resource.RuleInfoData);

        internal static TomlInfoWrapper GetSectionOrNull(string name)
        {
            if (!RuleInfoData._data.ContainsKey(name))
            {
                return null;
            }
            return new TomlInfoWrapper(
                    (TomlTable)RuleInfoData._data[name]
                );
        }

        internal static TomlInfoWrapper GetSectionOrThrow(string name)
        {
            if (RuleInfoData._data.ContainsKey(name))
            {
                return new TomlInfoWrapper(
                    (TomlTable)RuleInfoData._data[name]
                );
            }
            throw new ArgumentException($"The section {name} could not be found in TOML data");
        }
    }
}
