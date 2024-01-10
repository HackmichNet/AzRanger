using System;
using Tomlyn;
using Tomlyn.Model;

namespace AzRanger.Checks
{

    internal class RuleInfoDataWrapper
    {
        private readonly TomlTable _table;

        public RuleInfoDataWrapper(TomlTable table)
        {
            this._table = table;
        }

        public bool HasKey(string key)
        {
            return this._table.ContainsKey(key);
        }

        public int GetIntOrThrow(string key)
        {
            return int.Parse(this.GetStringOrThrow(key));
        }

        public string GetStringOrNull(string key)
        {
            return this.HasKey(key) ? this._table[key].ToString() : null;
        }

        public string GetStringOrThrow(string key)
        {
            if (this.HasKey(key))
            {
                return this._table[key].ToString();
            }
            throw new ArgumentException($"The key {key} could not be found in TOML data");
        }
    }

    internal static class RuleInfoData
    {
        private readonly static TomlTable _data = Toml.ToModel(Properties.Resource.RuleInfoData);

        internal static RuleInfoDataWrapper GetSectionOrNull(string name)
        {
            if (!RuleInfoData._data.ContainsKey(name))
            {
                return null;
            }
            return new RuleInfoDataWrapper(
                    (TomlTable)RuleInfoData._data[name]
                );
        }

        internal static RuleInfoDataWrapper GetSectionOrThrow(string name)
        {
            if (RuleInfoData._data.ContainsKey(name))
            {
                return new RuleInfoDataWrapper(
                    (TomlTable)RuleInfoData._data[name]
                );
            }
            throw new ArgumentException($"The section {name} could not be found in TOML data");
        }
    }
}
