using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ServerBackup {
    public partial class Form1 : Form {

        private readonly Settings _settings;


        public Form1(Settings settings) {
            _settings = settings;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {

            var dbHelper = new DatabaseHelper(_settings.Server, _settings.User, _settings.Password);

            var lstDatabases =  dbHelper.GetDatabases();
            textBox1.Text = lstDatabases.Aggregate((db1, db2) => db1 + Environment.NewLine + db2);

            dbHelper.DumpToFiles(lstDatabases, _settings.BackupPath);
        }
    }
}
