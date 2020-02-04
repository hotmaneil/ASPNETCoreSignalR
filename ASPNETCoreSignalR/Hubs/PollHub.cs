using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ASPNETCoreSignalR.Hubs
{
	public class PollHub : Hub
	{
		/// <summary>
		/// 發送訊息 參考
		/// </summary>
		/// <param name="name"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public async Task Send(string name, string message)
		{
			// Call the broadcastMessage method to update clients.
			await Clients.All.SendAsync("broadcastMessage", name, message);
		}
	}
}
