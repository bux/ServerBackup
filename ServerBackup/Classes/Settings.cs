using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBackup {
    public class Settings {

        /// <summary>
        /// Gets or sets the Server
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the BackupPath
        /// </summary>
        public string BackupPath { get; set; }

        /// <summary>
        /// Gets or sets the BackupSourceList
        /// </summary>
        public List<string> BackupSourceList { get; set; }
        
    }
}
