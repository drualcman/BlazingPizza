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
    public partial class Myorders
    {
        [Inject]
        public HttpClient Client { get; set; }

        List<OrderWithStatus> MyOrdersWithStatus;

        CancellationTokenSource PollingCancellationToken;

        private async void PollForUpdates()
        {
            PollingCancellationToken = new CancellationTokenSource();

            while (!PollingCancellationToken.IsCancellationRequested)
            {
                try
                {
                    MyOrdersWithStatus = await Client.GetFromJsonAsync<List<OrderWithStatus>>("orders");
                    StateHasChanged();
                    await Task.Delay(5000);                    
                }
                catch (Exception ex)
                {
                    PollingCancellationToken.Cancel();
                    //send to log...
                    Console.Error.WriteLine(ex.Message);
                    StateHasChanged();
                }
            }
        }

        protected override void OnInitialized()
        {
            PollingCancellationToken?.Cancel();     //if already enter some times and it's looking then cancel to check the new order
            PollForUpdates();
        }
    }
}
