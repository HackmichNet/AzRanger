using System;
using Tomlyn.Model;

namespace AzRanger.Checks
{
    internal class TomlInfoWrapper
    {
        private readonly TomlTable _table;

        public TomlInfoWrapper(TomlTable table)
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
}
