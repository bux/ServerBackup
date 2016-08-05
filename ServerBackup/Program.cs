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
                Application.Run(new Form1(_settings));
            } else {

                if (args[0] == "-q") {
                    var dbHelper = new DatabaseHelper(_settings.Server, _settings.User, _settings.Password);
                    var lstDatabases = dbHelper.GetDatabases();
                    Console.Write(lstDatabases.Aggregate((db1, db2) => db1 + Environment.NewLine + db2));

                    dbHelper.DumpToFiles(lstDatabases, _settings.BackupPath);
                }
            }
        }

        private static void LoadConfig() {
            var cfgHelper = new ConfigHelper();
            _settings = cfgHelper.GetSettings();
        }
    }
}
