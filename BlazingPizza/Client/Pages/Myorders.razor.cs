using BlazingPizza.Client.Services;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class Myorders //: IDisposable
    {
        [Inject]
        public OrdersClient Client { get; set; }        

        private async Task<List<OrderWithStatus>> LoadOrders()
        {
            await Task.Delay(10);
            List<OrderWithStatus> MyOrdersWithStatus = new List<OrderWithStatus>();
            //try
            //{
            //    MyOrdersWithStatus = await Client.GetOrders();
            //}
            //catch (AccessTokenNotAvailableException ex)
            //{
            //    ex.Redirect();
            //}
            return MyOrdersWithStatus;
        }

        //List<OrderWithStatus> MyOrdersWithStatus;

        //CancellationTokenSource PollingCancellationToken;

        //private async void PollForUpdates()
        //{
        //    PollingCancellationToken = new CancellationTokenSource();

        //    while (!PollingCancellationToken.IsCancellationRequested)
        //    {
        //        try
        //        {
        //            MyOrdersWithStatus = await Client.GetOrders();
        //            StateHasChanged();                    
        //            int ordersDeliveres = MyOrdersWithStatus.Where(o => o.IsDelivered == true).ToList().Count();
        //            int totalOrders = MyOrdersWithStatus.Count();
        //            if (ordersDeliveres == totalOrders) PollingCancellationToken.Cancel();
        //            else await Task.Delay(5000);                    
        //        }
        //        catch (AccessTokenNotAvailableException ex)
        //        {
        //            PollingCancellationToken.Cancel();
        //            ex.Redirect();
        //        }
        //        catch (Exception ex)
        //        {
        //            PollingCancellationToken.Cancel();
        //            //send to log...
        //            Console.Error.WriteLine(ex.Message);
        //            StateHasChanged();
        //        }
        //    }
        //}

        //protected override void OnInitialized()
        //{
        //    PollingCancellationToken?.Cancel();     //if already enter some times and it's looking then cancel to check the new order
        //    PollForUpdates();
        //}

        //public void Dispose()
        //{
        //    PollingCancellationToken?.Cancel();
        //}
    }
}
