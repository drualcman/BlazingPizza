using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class OrderDetails: IDisposable
    {
        [Parameter]
        public int OrderId { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        OrderWithStatus MyOrder;
        bool InvalidOrder;
        CancellationTokenSource PollingCancellationToken;

        private async void PollForUpdates()
        {
            PollingCancellationToken = new CancellationTokenSource();

            while (!PollingCancellationToken.IsCancellationRequested)
            {
                try
                {
                    InvalidOrder = false;
                    MyOrder = await Client.GetFromJsonAsync<OrderWithStatus>($"orders/{OrderId}");
                    StateHasChanged();
                    if (MyOrder.IsDelivered)
                    {
                        //order delivered, cancel the pool
                        PollingCancellationToken.Cancel();
                    }
                    else
                    {
                        await Task.Delay(5000);                        
                    }
                }
                catch (Exception ex)
                {
                    InvalidOrder = true;
                    PollingCancellationToken.Cancel();
                    //send to log...
                    Console.Error.WriteLine(ex.Message);
                    StateHasChanged();
                }
            }
        }

        protected override void OnParametersSet()
        {
            PollingCancellationToken?.Cancel();     //if already enter some times and it's looking then cancel to check the new order
            PollForUpdates();
        }

        public void Dispose()
        {
            PollingCancellationToken?.Cancel();
        }
    }
}
