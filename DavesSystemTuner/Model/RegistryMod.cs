using System.IO;
using System.Linq;

namespace DavesSystemTuner.Model
{
    // RegistryMod is a Mod which can be applied to the Windows Registry
    internal class RegistryMod : Mod
    {
        public string KeyPath { get; set; }
        public string KeyName { get; set; }
        public string EnableValue { get; set; }
        public string DisableValue { get; set; }
        public Microsoft.Win32.RegistryValueKind? KeyType { get; set; }
        public RegistryMod() : base()
        {
            KeyPath = string.Empty;
            KeyName = string.Empty;
            EnableValue = string.Empty;
            DisableValue = string.Empty;
        }
        public Microsoft.Win32.RegistryHive? GetHive()
        {
            string[] pathArray = KeyPath.Split(Path.DirectorySeparatorChar);
            if (pathArray.Length == 0) { return null; }

            switch (pathArray[0])
            {
                case "HKEY_CURRENT_CONFIG":
                    return Microsoft.Win32.RegistryHive.CurrentConfig;
                case "HKEY_CURRENT_USER":
                    return Microsoft.Win32.RegistryHive.CurrentUser;
                case "HKEY_LOCAL_MACHINE":
                    return Microsoft.Win32.RegistryHive.LocalMachine;
            };

            return null;
        }

        // GetSubPath() returns KeyPath without the RegistryHive prefix
        // Ex: "HKEY_CURRENT_USER\\Test\\Key\\Path" becomes "Test\\Key\\Path"
        public string GetSubPath()
        {
            string[] keyPathArray = KeyPath.Split(Path.DirectorySeparatorChar);
            if (keyPathArray.Length == 0) { return string.Empty; }

            string? subPath = string.Join("\\", keyPathArray.Skip(1));
            if (subPath == null) { return string.Empty; }

            return subPath;
        }
    }
}
