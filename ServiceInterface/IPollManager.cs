using System.Collections.Generic;
using ViewModel;

namespace ServiceInterface
{
	public interface IPollManager
	{
		/// <summary>
		/// 加入投票
		/// </summary>
		/// <param name="pollModel"></param>
		/// <returns></returns>
		bool AddPoll(AddPollViewModel pollModel);

		/// <summary>
		/// 取得投票項目
		/// </summary>
		/// <returns></returns>
		IEnumerable<PollDetailsViewModel> GetActivePoll();

		/// <summary>
		/// 取得投票結果
		/// </summary>
		/// <param name="pollID"></param>
		/// <returns></returns>
		IEnumerable<VoteResultViewModel> GetPollVoteResults(int pollID = 0);
	}
}
