using Dapper;
using DataModel;
using DataModel.Entities;
using ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ViewModel;

namespace ServiceImpletment
{
	public class PollManager: IPollManager
	{
		public IDbConnection _connection;

		public PollManager()
		{
			_connection = new SqlConnection("Data Source=localhost;Initial Catalog=ASPNETCoreDemoDB;Connection Timeout=300000;Persist Security Info=True;User ID=admin; Password=hotmaneil");
		}

		/// <summary>
		/// 加入投票
		/// </summary>
		/// <param name="pollModel"></param>
		/// <returns></returns>
		public bool AddPoll(AddPollViewModel pollModel)
		{
			try
			{
				var answers = pollModel.Answer.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
				foreach (var answer in answers)
				{
					PollOption option = new PollOption();

					string sql = "select * from PollOption where PollID=1 and Answers='" + answer + "'";
					var queryCurrentItem = _connection.Query<PollOption>(sql);
					if (queryCurrentItem.Any())
					{
						option = queryCurrentItem.First();
						option.Vote += 1;
						_connection.Update(option);
					}
					else
					{
						option.PollID = 1;//poll.PollID;
						option.Answers = answer;
						option.Vote = 1;
						_connection.Insert(option);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return true;
		}

		/// <summary>
		/// 取得投票項目
		/// </summary>
		/// <returns></returns>
		public IEnumerable<PollDetailsViewModel> GetActivePoll()
		{
			List<PollDetailsViewModel> dataList = new List<PollDetailsViewModel>();

			var polls = _connection.GetList<Poll>().ToList();

			foreach (var item in polls)
			{
				PollDetailsViewModel addModel = new PollDetailsViewModel();
				addModel.PollID = item.PollID;
				addModel.Question = item.Question;
				addModel.PollOption = _connection.GetList<PollOption>().Where(x => x.PollID == item.PollID);

				dataList.Add(addModel);
			}

			return dataList;
		}

		/// <summary>
		/// 取得投票結果
		/// </summary>
		/// <param name="pollID"></param>
		/// <returns></returns>
		public IEnumerable<VoteResultViewModel> GetPollVoteResults(int pollID = 0)
		{
			if (pollID == 0)
			{
				var poll = _connection.GetList<Poll>().Where(o => o.Active.Equals(true));
				if (poll.Any())
					pollID = poll.FirstOrDefault().PollID;
			}

			var pollOption = _connection.GetList<PollOption>().Where(o => o.PollID.Equals(pollID));
			if (pollOption.Any())
			{
				return pollOption.Select(o => new VoteResultViewModel
				{
					Choice = o.Answers,
					Vote = o.Vote
				});
			}
			return Enumerable.Empty<VoteResultViewModel>();
		}
	}
}
