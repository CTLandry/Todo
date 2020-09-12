using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Todo.Infrastructure.Globals
{
    /// <summary>
    /// Handles global events that need to be listened for such as a theme change or internet connectivity loss
    /// </summary>
    public class AppModel : IAppModel
    {
        public AppModel()
        {
            Task.Run(async () => await InitAsync()); 
        }

        #region DarkMode - LightMode

       

        async Task InitAsync()
        {
            var useDarkModeValue = await SecureStorage.GetAsync(nameof(UseDarkMode)).ConfigureAwait(false);
            UseDarkMode = Convert.ToBoolean(useDarkModeValue);

            var useDeviceThemeValue = await SecureStorage.GetAsync(nameof(UseDeviceThemeSettings)).ConfigureAwait(false);
            UseDeviceThemeSettings = Convert.ToBoolean(useDeviceThemeValue);
        }

        private bool useDeviceThemeSettings;
        public bool UseDeviceThemeSettings
        {
            get
            {
                return useDeviceThemeSettings;
            }

            set
            {
                SetProperty(ref useDeviceThemeSettings, value);
            }
        }

        private bool useDarkMode;
        public bool UseDarkMode
        {
            get
            {
                return useDarkMode;
            }

            set
            {
                SecureStorage.SetAsync(nameof(UseDarkMode), value.ToString());
                SetProperty(ref useDarkMode, value);
            }
        }

        #endregion

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
