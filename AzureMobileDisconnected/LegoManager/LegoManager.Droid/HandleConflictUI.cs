using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
[assembly: Xamarin.Forms.Dependency(typeof (LegoManager.Droid.HandleConflictUI))]
namespace LegoManager.Droid
{
    public class HandleConflictUI : LegoManager.IHandleConflictUI
    {   
        public async Task<ConflictResolutionDecision> HandleConflictAsync(string message)
        {
        var context = Xamarin.Forms.Forms.Context;
        var builder = new AlertDialog.Builder(context);
        builder.SetTitle("Conflict between local and server versions");
        builder.SetMessage(message);

        var clickTask = new TaskCompletionSource<int>();

        builder.SetPositiveButton("Local", (which, e) =>
        {
            clickTask.SetResult(1);
        });
        builder.SetNegativeButton("Server", (which, e) =>
        {
            clickTask.SetResult(2);
        });
            
        builder.Create().Show();

        int command = await clickTask.Task;
        if (command == 1)
        {
            return ConflictResolutionDecision.Local;
        }
        else
        {
            return ConflictResolutionDecision.Server;
        }
    }
    }
}