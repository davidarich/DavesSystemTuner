using DavesSystemTuner.Model;
using Microsoft.Win32;

namespace DavesSystemTuner.Service
{
    // RegistryModService is a class which can be used to modify the Windows Registry via RegistryMods
    internal class RegistryModService
    {
        public RegistryModService() { }

        // applies changes to the system according to the provided RegistryMod
        public void ApplyMod(RegistryMod registryMod)
        {
            // get registry key based on hive
            RegistryKey? regKey = GetRegistryKeyFromMod(registryMod);
            // if the registry key didn't exist, create it
            regKey ??= CreateRegistryKeyFromMod(registryMod);
            // make sure a valid key was found  
            if (regKey == null) { return; }
 
            regKey.SetValue(registryMod.KeyName, registryMod.EnableValue, RegistryValueKind.DWord);
            regKey.Close();
            return;
        }

        // creates and returns RegistryKey object for the provided RegistryMod
        // Note: the returned RegistryKey should be closed with Close()
        private static RegistryKey? CreateRegistryKeyFromMod(RegistryMod registryMod)
        {
            RegistryKey? regKey = null;
            switch (registryMod.GetHive())
            {
                case RegistryHive.CurrentConfig:
                    regKey = Registry.CurrentConfig.CreateSubKey(registryMod.GetSubPath(), true);
                    break;
                case RegistryHive.CurrentUser:
                    regKey = Registry.CurrentUser.CreateSubKey(registryMod.GetSubPath(), true);
                    break;
                case RegistryHive.LocalMachine:
                    regKey = Registry.LocalMachine.CreateSubKey(registryMod.GetSubPath(), true);
                    break;
            };

            return regKey;
        }

        // returns a RegistryKey for a RegistryMod if the RegistryKey already exists
        // Note: the returned RegistryKey should be closed with Close()
        private static RegistryKey? GetRegistryKeyFromMod(RegistryMod registryMod)
        {
            // get registry key based on hive
            RegistryKey? regKey = null;
            switch (registryMod.GetHive())
            {
                case RegistryHive.CurrentConfig:
                    regKey = Registry.CurrentConfig.OpenSubKey(registryMod.GetSubPath(), true);
                    break;
                case RegistryHive.CurrentUser:
                    regKey = Registry.CurrentUser.OpenSubKey(registryMod.GetSubPath(), true);
                    break;
                case RegistryHive.LocalMachine:
                    regKey = Registry.LocalMachine.OpenSubKey(registryMod.GetSubPath(), true);
                    break;
            };

            return regKey;
        }

        // returns true when the given mod is currently applied to the system
        public bool IsModApplied(RegistryMod registryMod)
        {
            if (registryMod == null) { return false; }

            // get the registry key
            RegistryKey? registryKey = GetRegistryKeyFromMod(registryMod);
            if (registryKey == null) { return false; }

            // read value from key
            object? keyValue = registryKey.GetValue(registryMod.KeyName);
            if (keyValue == null) { return false; }

            // compare the value of the existing key to the enable value
            if (keyValue.ToString() != registryMod.EnableValue)
            {
                registryKey.Close();
                return false;
            }

            registryKey.Close();
            return true;
        }

        // removes changes to the system for a RegistryMod
        public void RemoveMod(RegistryMod registryMod)
        {
            if (registryMod == null) { return; }

            // get registry key based on hive
            RegistryKey? regKey = GetRegistryKeyFromMod(registryMod);

            // if the registry key didn't exist, there's nothing to do
            if (regKey == null) { return; }

            // if a valid key was found, delete it
            regKey.DeleteSubKey(registryMod.KeyName);
            regKey.Close();
            return;
        }
    }
}
