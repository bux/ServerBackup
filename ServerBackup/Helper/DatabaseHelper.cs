using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ServerBackup {
    public class DatabaseHelper {
        private readonly string _password;
        private readonly string _user;
        private readonly string _server;
        
        public DatabaseHelper(string server, string user, string password) {
            _server = server;
            _user = user;
            _password = password;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstDatabases"></param>
        /// <param name="databaseBackupPath"></param>
        public void DumpToFiles(List<string> lstDatabases, string databaseBackupPath) {

            databaseBackupPath = Environment.ExpandEnvironmentVariables(databaseBackupPath);

            var lstFileNames = new List<string>();

            foreach (var databaseName in lstDatabases) {

                var constring = GetConnectionString(databaseName);

                var fileName = string.Format("{0}_{1}.sql", DateTime.Now.ToString("yyyyMMdd"), databaseName);
                fileName = Path.Combine(databaseBackupPath, fileName);

                lstFileNames.Add(fileName);

                using (var conn = new MySqlConnection(constring)) {
                    using (var cmd = new MySqlCommand()) {
                        using (var mb = new MySqlBackup(cmd)) {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(fileName);
                            conn.Close();
                        }
                    }
                }
            }

            // zip files

            foreach (var fileName in lstFileNames) {
                var zipFileName = fileName + ".zip";

                if (File.Exists(zipFileName)) {
                    File.Delete(zipFileName);
                }

                FileHelper.ZipSingleFile(zipFileName, fileName);

                File.Delete(fileName);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        private string GetConnectionString(string databaseName) {

            var connectionString = string.Format("server={0};user={1};pwd={2};Convert Zero Datetime=True", _server, _user, _password);

            if (string.IsNullOrEmpty(databaseName)) {
                return connectionString;
            }
            return string.Format(connectionString + ";database={0}", databaseName);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetDatabases() {

            var lstDatabases = new List<string>();
            var lstIgnoredDatabases = new List<string> { "performance_schema", "information_schema", "mysql", "sys" };

            var constring = GetConnectionString(null);


            using (var connection = new MySqlConnection(constring)) {
                using (var command = new MySqlCommand()) {
                    command.Connection = connection;
                    command.CommandText = "SHOW DATABASES;";
                    connection.Open();
                    var mySqlDataReader = command.ExecuteReader();

                    while (mySqlDataReader.Read()) {
                        for (int i = 0; i < mySqlDataReader.FieldCount; i++) {
                            lstDatabases.Add(mySqlDataReader.GetValue(i).ToString());
                        }
                    }
                    connection.Close();
                }
            }

            lstDatabases = lstDatabases.Except(lstIgnoredDatabases).ToList();
            return lstDatabases;
        }

    }
}
