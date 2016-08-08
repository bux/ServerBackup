using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;


namespace ServerBackup {
    public class FileHelper {

        public void BackupFilesAndFolder(string backupPath, List<BackupItem> backupItems) {

            var zipFileName = string.Format("{0}_Files.zip", DateTime.Now.ToString("yyyyMMdd"));
            zipFileName = Path.Combine(backupPath, zipFileName);

            zipFileName = Environment.ExpandEnvironmentVariables(zipFileName);

            using (var archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create)) {

                foreach (var backupItem in backupItems) {

                    if (backupItem.ItemType == BackupItemType.File) {
                        if (File.Exists(backupItem.ItemPath)) {
                            archive.CreateEntryFromFile(backupItem.ItemPath, backupItem.ItemPath, CompressionLevel.Optimal);
                        }
                    }

                    if (backupItem.ItemType == BackupItemType.Folder) {
                        if (Directory.Exists(backupItem.ItemPath)) {
                            archive.CreateEntry(backupItem.ItemPath, CompressionLevel.Optimal);
                        }
                    }
                }
            }
        }


        public static void ZipSingleFile(string zipFileName, string fileName) {
            using (var archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create)) {
                archive.CreateEntryFromFile(fileName, Path.GetFileName(fileName));
            }
        }

        
    }

}