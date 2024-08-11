using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamer_Bot_Backup_Utility.Data
{
    public class Config : Model.Config
    {
        /// <summary>
        /// Load the Config
        /// </summary>
        public static Config Load()
        {
            string strConfig = Preferences.Get("config", string.Empty);
            Config? tmpConfig = null;
            if (!string.IsNullOrWhiteSpace(strConfig)) { tmpConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(strConfig); }
            if (tmpConfig == null)
            {
                return new();
            }
            else
            {
                return tmpConfig;
            }
        }

        /// <summary>
        /// Save the config
        /// </summary>
        public void Save()
        {
            Preferences.Set("config", Newtonsoft.Json.JsonConvert.SerializeObject(this));
        }
    }
}
