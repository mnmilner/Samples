using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LegoManager
{
    public class EditLegoSetViewModel : INotifyPropertyChanged
    {
        


        public EditLegoSetViewModel()
        {
            
            SaveLegoSet = new DelegateCommand(
                SaveSetToStorage, CanSaveSet);

            RemoveLegoSet = new DelegateCommand(
                RemoveFromStorage, CanRemoveSet);
        }

        private Xamarin.Forms.INavigation navigator;

        public Xamarin.Forms.INavigation Navigation
        {
            get { return navigator; }
            set { navigator = value; }
        }

        private LegoSet set;

        public LegoSet SetToEdit
        {
            get { return set; }

            set {
                set = value;
                NotifyPropertyChanged("SetToEdit");
            }
        }
        public ICommand RemoveLegoSet { get; set; }

        public async void RemoveFromStorage(object parameter)
        {
            try {
                await LegoManager.App.LegoService.RemoveSetAsync(this.set);
                await navigator.PopAsync();
            }
            catch(Exception ex)
            {

            }
        }

        public bool CanRemoveSet(object parameter)
        {
            //TODO: validate inputs
            return true;
        }
        public ICommand SaveLegoSet { get; set; }

        public async void SaveSetToStorage(object parameter)
        {
            try {
                await LegoManager.App.LegoService.UpdateSetAsync(this.set);
                await navigator.PopAsync();
            }
            catch(Exception ex)
            {

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
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
