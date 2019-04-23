# This repo is obsolete and therefore archived

* Start tool
* End tool
* Edit config/config.xml
* Start tool and press button
* (or run the tool with `ServerBackup.exe -q`)

Sample config.xml

```xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<config>
  <Server>localhost</Server>
  <User>backup</User>
  <Password>lala</Password>
  <BackupFolder>%UserProfile%\Documents\Backup</BackupFolder>
  <BackupSources>
    <BackupItem ItemType="Folder">C:\Users\dev\Documents\IISExpress\config</BackupItem>
    <BackupItem ItemType="File">C:\Users\dev\Documents\gitignore_global.txt</BackupItem>
  </BackupSources>
</config>
```
