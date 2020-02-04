using ASPNETCoreSignalR.Hubs;
using ASPNETCoreSignalR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ServiceInterface;
using System.Diagnostics;
using ViewModel;

namespace ASPNETCoreSignalR.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly IPollManager _pollManager;

		private readonly IHubContext<PollHub> _hubContext;


		public HomeController(ILogger<HomeController> logger, IPollManager pollManager, IHubContext<PollHub> hubContext)
		{
			_logger = logger;
			_pollManager = pollManager;
			_hubContext = hubContext;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult AddPoll()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddPoll(AddPollViewModel poll)
		{
			if (ModelState.IsValid)
			{
				if (_pollManager.AddPoll(poll))
				{
					ViewBag.Message = "Poll added successfully!";

					_hubContext.Clients.All.SendAsync("PollItem");
					_hubContext.Clients.All.SendAsync("GetVoteResults");
				}
					
			}
			return View(poll);
		}

		public IActionResult Result(int pollID = 0)
		{
			ViewBag.PollID = pollID;
			return View();
		}
	}
}
