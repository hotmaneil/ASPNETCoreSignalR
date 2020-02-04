using DataModel.Entities;
using System.Collections.Generic;

namespace ViewModel
{
	public class PollDetailsViewModel
	{
		public int PollID { get; set; }

		public string Question { get; set; }

		public IEnumerable<PollOption> PollOption { get; set; }
	}
}
