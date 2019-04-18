using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSPA.ViewModels;

namespace WebSPA
{
    public class ChartHub : Hub
    {
        public async Task BroadcastChartData(List<ChartModel> data)
        {
            await Clients.All.SendAsync("broadcastchartdata", data);
        }

        //public async Task BroadcastChartData(string data)
        //{
        //    await Clients.All.SendAsync("broadcastchartdata", DataManager.GetData());
        //}
    }
}
