using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zip;


namespace ServerBackup {
    public class FileHelper {

        public bool BackupFilesAndFolder(string backupPath, List<BackupItem> backupItems) {

            if (backupItems.Any() == false) {
                return false;
            }

            backupPath = Environment.ExpandEnvironmentVariables(backupPath);

            var directoryPath = Path.Combine(backupPath, "Files");
            Directory.CreateDirectory(directoryPath);

            var zipFileName = string.Format("{0}_Files.zip", DateTime.Now.ToString("yyyyMMdd"));
            zipFileName = Path.Combine(directoryPath, zipFileName);

            using (var zipFile = new ZipFile()) {
                zipFile.AddDirectoryWillTraverseReparsePoints = false;

                foreach (var backupItem in backupItems) {

                    if (backupItem.ItemType == BackupItemType.File) {
                        if (File.Exists(backupItem.ItemPath)) {
                            zipFile.AddFile(backupItem.ItemPath, "");
                        }
                    }

                    if (backupItem.ItemType == BackupItemType.Folder) {
                        if (Directory.Exists(backupItem.ItemPath)) {
                            zipFile.AddDirectory(backupItem.ItemPath, new DirectoryInfo(backupItem.ItemPath).Name);
                        }
                    }
                }

                zipFile.Save(zipFileName);
            }

            return true;
        }


        public static void ZipSingleFile(string zipFileName, string fileName) {

            using (var zipFile = new ZipFile()) {
                if (File.Exists(fileName)) {
                    zipFile.AddFile(fileName);
                    zipFile.Save(zipFileName);
                }
            }
        }

    }
}