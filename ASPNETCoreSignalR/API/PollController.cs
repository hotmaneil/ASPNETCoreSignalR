using Microsoft.AspNetCore.Mvc;
using ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using ViewModel;

namespace ASPNETCoreSignalR.API
{
	[Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
		private readonly IPollManager _pollManager;

		public PollController(IPollManager pollManager)
		{
			_pollManager = pollManager;
		}

		/// <summary>
		/// 取得投票項目清單
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("Get")]
		public IEnumerable<PollDetailsViewModel> Get()
		{
			var res = _pollManager.GetActivePoll();
			return res.ToList();
		}

		/// <summary>
		/// 取得投票結果列表
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetVoteResults")]
		public IEnumerable<VoteResultViewModel> GetVoteResults(int id)
		{
			return _pollManager.GetPollVoteResults(id).ToList();
		}
	}
}