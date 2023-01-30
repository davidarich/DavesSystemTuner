using CommunityToolkit.Mvvm.Input;
using DavesSystemTuner.Model;
using DavesSystemTuner.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DavesSystemTuner.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private const string LAST_SCAN_DEFAULT = "Never";
        RegistryModService RegistryModService { get; set; }

        #region INotifyPropertyChanged Boilerplate
        // todo: look into mvvm toolkit ObservableBase, rather than INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        // used in setters to notify view bindings that a property has changed value
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Overview Tab - Bindings
        private string _lastScan;
        public string LastScan
        {
            get { return _lastScan; }
            set
            {
                if(_lastScan!= value)
                {
                    _lastScan = value;
                    NotifyPropertyChanged(nameof(LastScan));
                }
            }
        }

        private int _totalMods;
        public int TotalMods
        {
            get { return _totalMods; }
            set
            {
                if (_totalMods != value)
                {
                    _totalMods = value;
                    NotifyPropertyChanged(nameof(TotalMods));
                }
            }
        }

        private int _modsPending;
        public int ModsPending
        {
            get { return _modsPending; }
            set
            {
                if (_modsPending != value)
                {
                    _modsPending = value;
                    NotifyPropertyChanged(nameof(ModsPending));
                }
            }
        }

        private int _modsApplied;
        public int ModsApplied
        {
            get { return _modsApplied; }
            set
            {
                if (_modsApplied != value)
                {
                    _modsApplied = value;
                    NotifyPropertyChanged(nameof(ModsApplied));
                }
            }
        }

        #endregion

        #region Registry Mods Tab - Bindings
        private List<RegistryMod> _mods;
        public List<RegistryMod> Mods
        {
            get { return _mods; }
            set
            {
                if (_mods != value)
                {
                    _mods = value;
                    NotifyPropertyChanged(nameof(Mods));
                    // also update related bound properites
                    TotalMods = _mods.Count;
                    ModsPending = _mods.Count - ModsApplied;
                }
            }
        }

        private RegistryMod? _selectedMod;
        public RegistryMod? SelectedMod
        {
            get { return _selectedMod; }
            set
            {
                if (_selectedMod != value)
                {
                    _selectedMod = value;
                    NotifyPropertyChanged(nameof(SelectedMod));
                }
            }
        }
        #endregion

        #region Overview Tab - Command Bindings
        public RelayCommand ScanCommand { get; set; }
        public RelayCommand ApplyAllModsCommand { get; set; }
        public RelayCommand RemoveAllModsCommand { get; set; }
        #endregion

        #region Overview Tab - Command Implementations
        private void ApplyAllMods()
        {
            foreach (RegistryMod mod in Mods)
            {
                if (mod.IsEnabled)
                {
                    RegistryModService.ApplyMod(mod);
                }
            }
            Scan();
        }
        private bool CanExecuteApplyAllMods()
        {
            return (LastScan != LAST_SCAN_DEFAULT) && ModsPending > 0;
        }

        private void RemoveAllMods()
        {
            foreach (RegistryMod mod in Mods)
            {
                RegistryModService.RemoveMod(mod);
            }
            Scan();
        }
        private void Scan()
        {
            int applied = 0;
            int index = 0;
            List<RegistryMod> updatedMods = new();
            foreach (RegistryMod mod in Mods)
            {
                if (RegistryModService.IsModApplied(mod))
                {
                    mod.IsApplied = true;
                    applied++;
                }
                index++;
                updatedMods.Add(mod);
            }
            // update bound properties
            Mods = updatedMods;
            ModsApplied = applied;
            ModsPending = TotalMods - applied;
            LastScan = DateTime.Now.ToString("G");
            // update Command which will update bound button state in UI
            ApplyAllModsCommand.NotifyCanExecuteChanged();
        }
        #endregion

        public MainWindowViewModel()
        {
            RegistryModService = new();

            // set default values for internal class properties of bindings
            _lastScan = LAST_SCAN_DEFAULT;
            _totalMods = 0;
            _modsPending = 0;
            _modsApplied = 0;
            _mods = new List<RegistryMod>();

            // instantiate Commands with implementation logic
            ScanCommand = new RelayCommand(Scan);
            ApplyAllModsCommand = new RelayCommand(ApplyAllMods, CanExecuteApplyAllMods);
            RemoveAllModsCommand = new RelayCommand(RemoveAllMods);
        }
    }
}
