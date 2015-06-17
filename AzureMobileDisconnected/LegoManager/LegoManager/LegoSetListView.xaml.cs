using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LegoManager
{
    public partial class LegoSetListView : ContentPage
    {
        private LegoListViewModel model;
       

        public LegoSetListView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            LoadAsync(); 
        }

        public async Task LoadAsync()
        {
            model = new LegoListViewModel(this.Navigation);
            this.BindingContext = model;
            await model.LoadAsync();
            
        }

        public void LegoSet_Selected(object sender, ItemTappedEventArgs e)
        {
            LegoSet set = (LegoSet)e.Item;
            model.LegoSet_Selected(set);
        }
    }
}
