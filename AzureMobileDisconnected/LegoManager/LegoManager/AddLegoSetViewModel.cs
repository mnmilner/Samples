using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LegoManager
{
    public class AddLegoSetViewModel : INotifyPropertyChanged
    {
        
        public AddLegoSetViewModel()
        {
            SaveLegoSet = new DelegateCommand(
                SaveSetToStorage, CanSaveSet);
        }
        private LegoSet newSet;

        public LegoSet SetToAdd
        {
            get { return newSet; }
            set { newSet = value; }
        }

        private Xamarin.Forms.INavigation navigator;

        public Xamarin.Forms.INavigation Navigation
        {
            get { return navigator; }
            set { navigator = value; }
        }

        public ICommand SaveLegoSet { get; set; }

        public async void SaveSetToStorage(object parameter)
        {
            try {
                await LegoManager.App.LegoService.AddNewSetAsync(this.newSet);
                await navigator.PopAsync();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public bool CanSaveSet(object parameter)
        {
            //TODO: validate inputs
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
