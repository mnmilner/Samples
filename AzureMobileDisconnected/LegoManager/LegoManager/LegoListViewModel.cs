using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LegoManager
{
    public class LegoListViewModel : INotifyPropertyChanged
    {
        private INavigation navigator;

        public LegoListViewModel(Xamarin.Forms.INavigation navigation)
        {
            navigator = navigation;
            RefreshData = new DelegateCommand(RefreshLegoSets, CanRefreshLegoSets);
            AddLegoSet = new DelegateCommand(AddLegoSetToCollection, CanAddLegoSet);
            PurgeLocalData = new DelegateCommand(PurgeLocalChanges, CanPurgeLocalChanges);
        }
        private bool loading;

        public bool IsLoading
        {
            get { return loading; }
            private set {
                if(loading != value)
                {
                    loading = value;
                    NotifyPropertyChanged("IsLoading");
                }

            }
        }

        public ObservableCollection<LegoSet> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, 
                    new PropertyChangedEventArgs(propertyName));

            }
        }

        public ICommand AddLegoSet { get; set; }

        public ICommand RefreshData { get; set; }

        public ICommand PurgeLocalData { get; set; }

        public async Task LoadAsync()
        {
            IsLoading = true;
            var sets = await LegoManager.App.LegoService.GetLegoSetsAsync();
            Items = new ObservableCollection<LegoSet>(sets);
            NotifyPropertyChanged("Items");
            IsLoading = false;
        }

        public void LegoSet_Selected(LegoSet set)
        {
            var view = new EditLegoSetView(new EditLegoSetViewModel{ SetToEdit = set });
            navigator.PushAsync(view);
        }


        public async void RefreshLegoSets(object parameter) {
            IsLoading = true;
            var sets = await LegoManager.App.LegoService.PullAndGetLegoSetsAsync();
            Items = new ObservableCollection<LegoSet>(sets);
            NotifyPropertyChanged("Items");
            IsLoading = false;
        }
        public bool CanRefreshLegoSets(object parameter)
        {
            return !IsLoading;
        }
        public void AddLegoSetToCollection(object parameter)
        {
            navigator.PushAsync(new AddLegoSetView());
        }
        public bool CanAddLegoSet(object parameter)
        {
            return !IsLoading;
        }

        public async void PurgeLocalChanges(object parameter)
        {
            IsLoading = true;

            var refreshedSetsData = await App.LegoService.PurgeLocalChangesAsync();
         
            Items = new ObservableCollection<LegoSet>(refreshedSetsData);
            NotifyPropertyChanged("Items");
            IsLoading = false;
        }

        public bool CanPurgeLocalChanges(object parameter)
        {
            return !IsLoading;
        }
    }
}
