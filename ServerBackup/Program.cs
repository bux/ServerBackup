using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ServerBackup {
    static class Program {

        private static Settings _settings;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoadConfig();

            if (args.Length == 0) {
                Application.Run(new ServerBackup(_settings));
            } else {

                if (args[0] == "-d") {
                    var dbHelper = new DatabaseHelper(_settings.Server, _settings.User, _settings.Password);
                    var lstDatabases = dbHelper.GetDatabases();
                    dbHelper.DumpToFiles(lstDatabases, _settings.BackupPath);
                }

                if (args[0] == "-f") {
                    var fileHelper = new FileHelper();
                    fileHelper.BackupFilesAndFolder(_settings.BackupPath, _settings.BackupItems);
                }

            }
        }

        private static void LoadConfig() {
            var cfgHelper = new ConfigHelper();
            _settings = cfgHelper.GetSettings();
        }
    }
}
