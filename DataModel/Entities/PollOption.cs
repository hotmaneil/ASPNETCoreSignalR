using Dapper;

namespace DataModel.Entities
{
	public class PollOption
	{
		[Key]
		public int PollOptionID { get; set; }

		public int PollID { get; set; }

		public string Answers { get; set; }

		public int Vote { get; set; }
	}
}
