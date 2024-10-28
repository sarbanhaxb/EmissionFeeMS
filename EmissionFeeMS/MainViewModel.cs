using EmissionFeeMS.SettingsMVVM;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmissionFeeMS
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CalcResult> CalcResults { get; set; }
        public Settings MainSettings { get; set; }

        private CalcResult _selectedItem;

        public CalcResult SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public MainViewModel()
        {
            MainSettings = Settings.GetSettings();
            CalcResults = [];
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      CalcResult phone = new CalcResult();
                      CalcResults.Add(phone);
                  }));
            }
        }

        private RelayCommand removeCommand;

        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj => Delete(obj as Collection<object>)));
            }
        }

        private void Delete(Collection<object> o)
        {
            List<CalcResult> list = o.Cast<CalcResult>().ToList();
            list.ForEach(obj => CalcResults.Remove(obj));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }



    }
}
