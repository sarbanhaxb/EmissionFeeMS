namespace EmissionFeeMS.SettingsMVVM
{
    public class MainSettingsViewModel
    {
        public Settings MainSettings { get; set; }


        public MainSettingsViewModel()
        {
            MainSettings = Settings.GetSettings();
        }
    }
}
