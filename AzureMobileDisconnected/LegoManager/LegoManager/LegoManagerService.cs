using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoManager
{
    public class LegoManagerService
    {
        static readonly string LEGO_SETS_QUERY = "LegoSetsFull";
        static MobileServiceClient client;
        static IMobileServiceSyncTable<LegoSet> legoSyncTable;
        static IMobileServiceTable<LegoSet> legoTable;
        static bool isConnected = true;
        static bool hasLoadedInitialData = false;


        static LegoManagerService()
        {
            client = new MobileServiceClient("https://[YOUR MOBILE SERVICE NAME].azure-mobile.net/", "[YOUR MOBILE SERVICE APPLICATION KEY]");
            legoSyncTable = client.GetSyncTable<LegoSet>();
            legoTable = client.GetTable<LegoSet>();
           // var sets = from l in legoTable
           //            where l.Owned == true
           //            select l;
           //var mysets = await  legoTable.ReadAsync(sets);
               
        }

        public async Task InitializeOfflineAsync(IMobileServiceLocalStore store, IMobileServiceSyncHandler conflictHandler)
        {
           
            if(client.SyncContext.IsInitialized)
            {
                throw new InvalidOperationException("The mobile service client has already been initialized");
            }
            if (conflictHandler == null)
            {
                await client.SyncContext.InitializeAsync(store, new SimpleUserConflictResolveHandler());
            }
            else
            {
                await client.SyncContext.InitializeAsync(store, conflictHandler);
            }

        }

        public async Task RemoveSetAsync(LegoSet set)
        {
            //TODO: remove item from azure service
            await legoSyncTable.DeleteAsync(set);
            if (isConnected)
            {
                try
                {
                    await client.SyncContext.PushAsync();
                }
                catch(MobileServicePushFailedException pfex)
                {
                    //TODO: handle push exception
                    System.Diagnostics.Debug.WriteLine(pfex.Message);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        public async Task UpdateSetAsync(LegoSet set)
        {
            //TODO: update item in Azure Mobile
            await legoSyncTable.UpdateAsync(set);
            if (isConnected)
            {
                try {
                    await client.SyncContext.PushAsync();
                }
                catch(MobileServicePushFailedException pfex)
                {
                    //TODO: handle push exception
                    System.Diagnostics.Debug.WriteLine(pfex.Message);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        public async Task<IList<LegoSet>> PurgeLocalChangesAsync()
        {
            await legoSyncTable.PurgeAsync(true);
            await legoSyncTable.PullAsync("", legoSyncTable.CreateQuery());
            return await legoSyncTable.ToListAsync();
        }

        public async Task AddNewSetAsync(LegoSet newSet)
        {
            await legoSyncTable.InsertAsync(newSet);
            if (isConnected)
            {
                try
                {
                    await client.SyncContext.PushAsync();
                }
                catch(MobileServicePreconditionFailedException prex)
                {
                    System.Diagnostics.Debug.WriteLine(prex.Message);
                }
                catch(MobileServicePushFailedException pfex)
                {
                    //TODO: handle conflicts
                    System.Diagnostics.Debug.WriteLine(pfex.PushResult.Errors.Count);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return;
        }

        public async Task<IList<LegoSet>> GetLegoSetsAsync()
        {
            if(!hasLoadedInitialData)
            {
                if(isConnected)
                {
                    try {
                        await legoSyncTable.PullAsync(LEGO_SETS_QUERY, legoSyncTable.CreateQuery());
                        hasLoadedInitialData = true;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    
                }
                else
                {
                    //TODO: notify not connected
                }
            }
            return await legoSyncTable.ToListAsync();
        }

        public async Task<IList<LegoSet>> PullAndGetLegoSetsAsync()
        {
           
            var query = legoSyncTable.CreateQuery();
            
            await legoSyncTable.PullAsync(LEGO_SETS_QUERY, query);

            return await legoSyncTable.ToListAsync();
        }

        public async Task PushChangesToAzure()
        {
            try
            {
                await client.SyncContext.PushAsync();
            }
            catch(MobileServicePushFailedException pfex)
            {
                //TODO: handle push failed
            }
            
        }
    }
}
