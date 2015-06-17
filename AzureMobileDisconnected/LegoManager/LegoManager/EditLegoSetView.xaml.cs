using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LegoManager
{
    public partial class EditLegoSetView : ContentPage
    {
        public EditLegoSetView(EditLegoSetViewModel targetItem)
        {
            InitializeComponent();
            targetItem.Navigation = this.Navigation;
            this.BindingContext = targetItem;
          
        }
    }
}
