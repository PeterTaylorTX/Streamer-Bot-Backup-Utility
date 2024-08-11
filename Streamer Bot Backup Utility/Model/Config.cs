using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamer_Bot_Backup_Utility.Model
{
    public class Config
    {
        /// <summary>
        /// The directory containing the Streamer Bot exe file
        /// </summary>
        public string StreamerBotLocation { get; set; } = Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.not_located;

    }
}
