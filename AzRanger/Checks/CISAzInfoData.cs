using System;
using Tomlyn;
using Tomlyn.Model;

namespace AzRanger.Checks
{
    internal static class CISAzInfoData
    {
        private readonly static TomlTable _data = Toml.ToModel(Properties.Resource.CISAzureInfoData);

        internal static TomlInfoWrapper GetSectionOrNull(string name)
        {
            if (!CISAzInfoData._data.ContainsKey(name))
            {
                return null;
            }
            return new TomlInfoWrapper(
                    (TomlTable)CISAzInfoData._data[name]
                );
        }

        internal static TomlInfoWrapper GetSectionOrThrow(string name)
        {
            if (CISAzInfoData._data.ContainsKey(name))
            {
                return new TomlInfoWrapper(
                    (TomlTable)CISAzInfoData._data[name]
                );
            }
            throw new ArgumentException($"The section {name} could not be found in TOML data");
        }
    }
}