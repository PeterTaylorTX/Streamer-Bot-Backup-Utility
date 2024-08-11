
namespace Streamer_Bot_Backup_Utility
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// The configuration for the app
        /// </summary>
        public Data.Config Config { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();

            try
            {
                this.Config = Data.Config.Load(); // Load the Config
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
                this.Config.Save(); // Save the config
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
                // Set acceptable file types
                PickOptions options = new()
                {
                    PickerTitle = "asd",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                        { DevicePlatform.WinUI, new[] {".exe" } }
                    })
                };
                // Set acceptable file types

                // Select the Streamer Bot exe file
                var result = await FilePicker.Default.PickAsync(options);
                if ((result == null)) { return; } // File not selected
                if (!System.IO.File.Exists(result.FullPath)) { return; } // File not found
                // Select the Streamer Bot exe file

                // Extract the Streamer Bot Directory from file path
                string? directoryName = Path.GetDirectoryName(result.FullPath);
                if (string.IsNullOrWhiteSpace(directoryName)) { return; }
                this.Config.StreamerBotLocation = directoryName;
                // Extract the Streamer Bot Directory from file path

                OnPropertyChanged(nameof(Config)); // UPDATE THE UI
            }
            catch (Exception ex) { await DisplayAlert(Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.Error, ex.Message, Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.OK); }
        }

        /// <summary>
        /// Backup the Datafiles
        /// </summary>
        private async void btnBackup_Clicked(object sender, EventArgs e)
        {
            try
            {
                this.LocalBackup();

                await DisplayAlert(Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.Complete, Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.Backup_Complete, Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.OK);
            }
            catch (Exception ex)
            {
                await DisplayAlert(Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.Error, ex.Message, Streamer_Bot_Backup_Utility.Resources.Strings.Localisation.OK);
            }
        }

        /// <summary>
        /// Backup the files locally (Safety - Always have extra backups)
        /// </summary>
        private void LocalBackup()
        {
            string backupLocation = $"{this.Config.StreamerBotLocation}\\backup_data";
            string dataFiles = $"{this.Config.StreamerBotLocation}\\data";
            if (!System.IO.Directory.Exists(backupLocation)) { System.IO.Directory.CreateDirectory(backupLocation); }

            foreach (var file in System.IO.Directory.GetFiles(dataFiles))
            {
                System.IO.File.Copy(file, $"{backupLocation}\\{Path.GetFileName(file)}", true);
            }
        }
    }
}
