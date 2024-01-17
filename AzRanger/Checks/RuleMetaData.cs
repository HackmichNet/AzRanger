namespace AzRanger.Checks
{
    using System;
    using Tomlyn;
    using Tomlyn.Model;

    namespace AzRanger.Checks
    {
        internal static class RuleMetaData
        {
            private readonly static TomlTable _data = Toml.ToModel(Properties.Resource.RuleMetaData);

            internal static TomlInfoWrapper GetSectionOrNull(string name)
            {
                if (!RuleMetaData._data.ContainsKey(name))
                {
                    return null;
                }
                return new TomlInfoWrapper(
                        (TomlTable)RuleMetaData._data[name]
                    );
            }

            internal static TomlInfoWrapper GetSectionOrThrow(string name)
            {
                if (RuleMetaData._data.ContainsKey(name))
                {
                    return new TomlInfoWrapper(
                        (TomlTable)RuleMetaData._data[name]
                    );
                }
                throw new ArgumentException($"The section {name} could not be found in TOML data");
            }
        }
    }

}
