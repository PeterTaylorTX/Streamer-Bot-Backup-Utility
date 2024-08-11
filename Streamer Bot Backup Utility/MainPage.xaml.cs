
namespace Streamer_Bot_Backup_Utility
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// The configuration for the app
        /// </summary>
        public Model.Config Config { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();

            try
            {
                /// LOAD CONFIG
                string strConfig = Preferences.Get("config", string.Empty);
                Model.Config? tmpConfig = null;
                if (!string.IsNullOrWhiteSpace(strConfig)) { tmpConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Config>(strConfig); }
                if (tmpConfig == null) { this.Config = new(); } else { this.Config = tmpConfig; }
                /// LOAD CONFIG
            }
            catch (Exception ex) { DisplayAlert(Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.Error, ex.Message, Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.OK); }
        }

        /// <summary>
        /// Page Loaded
        /// </summary>
        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            try
            {
                this.BindingContext = this;
            }
            catch (Exception ex) { DisplayAlert(Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.Error, ex.Message, Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.OK); }
        }

        /// <summary>
        /// Page Closing
        /// </summary>
        private void ContentPage_Unloaded(object sender, EventArgs e)
        {
            try
            {
                Preferences.Set("config", Newtonsoft.Json.JsonConvert.SerializeObject(this.Config));
            }
            catch (Exception ex) { DisplayAlert(Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.Error, ex.Message, Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.OK); }
        }

        /// <summary>
        /// Select the Streamer Bot exe file location
        /// </summary>
        private async void btnStreamerBotLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                PickOptions options = new()
                {
                    PickerTitle = "asd",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                        { DevicePlatform.WinUI, new[] {".exe" } }
                    })
                };

                var result = await FilePicker.Default.PickAsync(options);
                if ((result == null)) { return; } // File not selected
                if (!System.IO.File.Exists(result.FullPath)) { return; } // File not found
                this.Config.StreamerBotLocation = Path.GetFullPath(result.FullPath);
                OnPropertyChanged(nameof(Config));
            }
            catch (Exception ex) { await DisplayAlert(Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.Error, ex.Message, Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.OK); }
        }

    }
}
