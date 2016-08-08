using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace ServerBackup {

    public class ConfigHelper {
        private const string SERVER_NAME = "Server";
        private const string USER_NAME = "User";
        private const string PASSWORD_NAME = "Password";
        private const string BACKUPFOLDER_NAME = "BackupFolder";
        private const string BACKUPSOURCELIST_NAME = "BackupSources";
        private const string BACKUPITEM_NAME = "BackupItem";
        private const string ITEMTYPE_NAME = "ItemType";

        private readonly FileInfo _fiConfig = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config\config.xml"));
        private XDocument _xDocConfig;

        public ConfigHelper() {
            //create dir
            if (_fiConfig.Directory != null && _fiConfig.Directory.Exists == false) {
                _fiConfig.Directory.Create();
            }
        }


        private XDocument CreateOrLoadConfig(bool forceCreation) {
            if (_xDocConfig != null && forceCreation == false) {
                return _xDocConfig;
            }

            if (_fiConfig.Exists && forceCreation == false) {
                _xDocConfig = XDocument.Load(_fiConfig.FullName);
            } else {
                var serverElement = new XElement(SERVER_NAME, "localhost");
                var userElement = new XElement(USER_NAME, "root");
                var passwordElement = new XElement(PASSWORD_NAME, "password");
                var backupFolderElement = new XElement(BACKUPFOLDER_NAME, "%UserProfile%\\Documents\\Backup");
                var backupSourcesListItem = new XElement(BACKUPSOURCELIST_NAME);

                var lstElements = new List<XElement> { serverElement, userElement, passwordElement, backupFolderElement, backupSourcesListItem };

                _xDocConfig = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("config", lstElements.ToArray()));

                SaveConfigXml();

                return _xDocConfig;
            }

            return null;
        }

        /// <summary>
        ///   Read Settings from config file
        /// </summary>
        /// <returns></returns>
        public Settings GetSettings() {
            if (_xDocConfig == null) {
                CreateOrLoadConfig(false);
            }

            if (_xDocConfig == null) {
                return null;
            }

            var loadedSettings = new Settings();

            var serverElement = _xDocConfig.Descendants().FirstOrDefault(d => d.Name == SERVER_NAME);
            if (serverElement != null) {
                loadedSettings.Server = serverElement.Value;
            }

            var userElement = _xDocConfig.Descendants().FirstOrDefault(d => d.Name == USER_NAME);
            if (userElement != null) {
                loadedSettings.User = userElement.Value;
            }

            var passwordElement = _xDocConfig.Descendants().FirstOrDefault(d => d.Name == PASSWORD_NAME);
            if (passwordElement != null) {
                loadedSettings.Password = passwordElement.Value;
            }

            var backupFolderElement = _xDocConfig.Descendants().FirstOrDefault(d => d.Name == BACKUPFOLDER_NAME);
            if (backupFolderElement != null) {
                loadedSettings.BackupPath = backupFolderElement.Value;
            }

            var backupSourcesElement = _xDocConfig.Descendants().FirstOrDefault(d => d.Name == BACKUPSOURCELIST_NAME);
            if (backupSourcesElement != null) {
                try {
                    List<BackupItem> lstBackupItems = backupSourcesElement.Descendants().Where(d => d.Name == BACKUPITEM_NAME).Select(d => new BackupItem() {
                        ItemPath = d.Value.ToString(),
                        ItemType = EnumHelper.ParseEnum<BackupItemType>(d.Attribute(ITEMTYPE_NAME).Value)
                    }).ToList();

                    loadedSettings.BackupItems = lstBackupItems;

                } catch (Exception ex) {
                    loadedSettings.BackupItems = new List<BackupItem>();
                }
            }

            return loadedSettings;
        }

        /// <summary>
        ///   Saves the xml to the file system
        /// </summary>
        private void SaveConfigXml() {
            if (_xDocConfig != null) {
                _xDocConfig.Save(_fiConfig.FullName);
            }
        }
    }

}