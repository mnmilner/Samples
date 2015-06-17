using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

[assembly: Xamarin.Forms.Dependency(typeof(LegoManager.WinPhone.HandleConflictUI))]
namespace LegoManager.WinPhone
{
    
    public class HandleConflictUI : LegoManager.IHandleConflictUI
    {
        public async Task<ConflictResolutionDecision> HandleConflictAsync(string message)
        {
           var result = MessageBox.Show(message + "\r\n\r\nOK for local, cancel to keep server", "Resolve conflicts", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
                return ConflictResolutionDecision.Local;
            else
                return ConflictResolutionDecision.Server;
        }
    }
}
