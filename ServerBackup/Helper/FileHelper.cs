using System.Collections.Generic;
using System.IO;
using System.IO.Compression;


namespace ServerBackup {
    public class FileHelper {

        public void BackupFilesAndFolder(List<string> backupSourceList) {

        }


        public static void ZipSingleFile(string zipFileName, string fileName) {
            using (var archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create)) {
                archive.CreateEntryFromFile(fileName, Path.GetFileName(fileName));
            }
        }
    }

}