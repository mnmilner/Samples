using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LegoManager
{
    public partial class AddLegoSetView : ContentPage
    {
        public AddLegoSetView()
        {
            InitializeComponent();
            
            this.BindingContext = new AddLegoSetViewModel { SetToAdd = new LegoSet(), Navigation=this.Navigation };
        }
    }
}
