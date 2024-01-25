using System;
using Tomlyn;
using Tomlyn.Model;

namespace AzRanger.Checks
{
    internal static class CISM365InfoData
    {
        private readonly static TomlTable _data = Toml.ToModel(Properties.Resource.CISM365InfoData);

        internal static TomlInfoWrapper GetSectionOrNull(string name)
        {
            if (!CISM365InfoData._data.ContainsKey(name))
            {
                return null;
            }
            return new TomlInfoWrapper(
                    (TomlTable)CISM365InfoData._data[name]
                );
        }

        internal static TomlInfoWrapper GetSectionOrThrow(string name)
        {
            if (CISM365InfoData._data.ContainsKey(name))
            {
                return new TomlInfoWrapper(
                    (TomlTable)CISM365InfoData._data[name]
                );
            }
            throw new ArgumentException($"The section {name} could not be found in TOML data");
        }
    }
}
