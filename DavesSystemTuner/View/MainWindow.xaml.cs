using DavesSystemTuner.Model;
using DavesSystemTuner.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace DavesSystemTuner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel
            {
                // todo: decouple data from the view code-behind class
                Mods = new List<RegistryMod>()
                {
                    new RegistryMod()
                    {
                        Name = "Disable Windows Telemetry",
                        Category = "Telemetry",
                        Description = "",
                        KeyPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection",
                        KeyName = "Allow Telemetry",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "0",
                        DisableValue = "1",
                    },
                    
                    new RegistryMod()
                    {
                        Name = "Disable Taskbar Weather",
                        Category = "Taskbar",
                        Description = "",
                        KeyPath = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Feeds",
                        KeyName = "ShellFeedsTaskbarViewMode",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "2",
                        DisableValue = "0",
                    },
                    
                    new RegistryMod()
                    {
                        Name = "Disable Live Tiles",
                        Category = "Start Menu",
                        Description = "",
                        KeyPath = "HKEY_CURRENT_USER\\SOFTWARE\\Policies\\Microsoft\\Windows\\CurrentVersion\\PushNotifications",
                        KeyName = "NoTileApplicationNotification",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "1",
                        DisableValue = "0",
                    },
                    new RegistryMod()
                    {
                        Name = "Disable Cortana",
                        Category = "Search",
                        Description = "",
                        KeyPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search",
                        KeyName = "AllowCortana",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "0",
                        DisableValue = "1",
                    },
                    new RegistryMod()
                    {
                        Name = "Bing Search",
                        Category = "Start Menu",
                        Description = "",
                        KeyPath = "HKEY_CURRENT_USER\\SOFTWARE\\Policies\\Microsoft\\Windows\\Explorer",
                        KeyName = "DisableSearchBoxSuggestions",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "1",
                        DisableValue = "0",
                    },
                    new RegistryMod()
                    {
                        Name = "Disable My People",
                        Category = "Taskbar",
                        Description = "",
                        KeyPath = "HKEY_CURRENT_USER\\Software\\Policies\\Microsoft\\Windows\\Explorer",
                        KeyName = "HidePeopleBar",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "1",
                        DisableValue = "0",
                    },
                    new RegistryMod()
                    {
                        Name = "Disable Your Phone Integration",
                        Category = "OS Feature",
                        Description = "",
                        KeyPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\System",
                        KeyName = "EnableMmx",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "0",
                        DisableValue = "1",
                    },
                    new RegistryMod()
                    {
                        Name = "Disable Lock Screen Dynamic Content",
                        Category = "Lock Screen",
                        Description = "",
                        KeyPath = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager",
                        KeyName = "ContentDeliveryAllowed",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "0",
                        DisableValue = "1",
                    },
                    new RegistryMod()
                    {
                        Name = "Disable Lock Screen Dynamic Background",
                        Category = "Lock Screen",
                        Description = "",
                        KeyPath = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager",
                        KeyName = "RotatingLockScreenEnabled",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "0",
                        DisableValue = "1",
                    },
                    new RegistryMod()
                    {
                        Name = "Disable Lock Screen Tips and Tricks (1 of 3)",
                        Category = "Lock Screen",
                        Description = "",
                        KeyPath = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager",
                        KeyName = "SubscribedContent-338387Enabled",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "0",
                        DisableValue = "1",
                    },
                    new RegistryMod()
                    {
                        Name = "Disable Lock Screen Tips and Tricks (2 of 3)",
                        Category = "Lock Screen",
                        Description = "",
                        KeyPath = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager",
                        KeyName = "SubscribedContent-338388Enabled",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "0",
                        DisableValue = "1",
                    },
                    new RegistryMod()
                    {
                        Name = "Disable Lock Screen Tips and Tricks (3 of 3)",
                        Category = "Lock Screen",
                        Description = "",
                        KeyPath = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager",
                        KeyName = "SubscribedContent-338389Enabled",
                        KeyType = Microsoft.Win32.RegistryValueKind.DWord,
                        EnableValue = "0",
                        DisableValue = "1",
                    },
                }
            };
            DataContext = ViewModel;
        }
    }
}
