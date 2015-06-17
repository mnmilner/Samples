using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;

namespace LegoManager
{
    class SimpleUserConflictResolveHandler : Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncHandler
    {
        public async Task<JObject> ExecuteTableOperationAsync(IMobileServiceTableOperation operation)
        {
            MobileServicePreconditionFailedException error;

            do
            {
                error = null;

                try
                {
                    return await operation.ExecuteAsync();
                }
                catch (MobileServicePreconditionFailedException ex)
                {
                    error = ex;
                }

                if (error != null)
                {
                    var localItem = operation.Item.ToObject<LegoSet>();
                    var serverValue = error.Value;
                    
                    var message = "How do you want to resolve this conflict?\n\n" + "Local item: \n" + localItem.Name +
                        "\n\nServer item:\n" + serverValue.ToObject<LegoSet>().Name;

                    IHandleConflictUI handler = Xamarin.Forms.DependencyService.Get<IHandleConflictUI>(Xamarin.Forms.DependencyFetchTarget.NewInstance);
                    var handlerResult = await handler.HandleConflictAsync(message);


                    if (handlerResult ==  ConflictResolutionDecision.Local)
                    {
                        // Overwrite the server version and try the operation again by continuing the loop
                        operation.Item[MobileServiceSystemColumns.Version] = serverValue[MobileServiceSystemColumns.Version];
                        continue;
                    }
                    else
                    {
                        return (JObject)serverValue;
                    }
                }
            } while (error != null);

            return null;

        }

        public Task OnPushCompleteAsync(MobileServicePushCompletionResult result)
        {
            return Task.FromResult(0);
        }
    }
}
