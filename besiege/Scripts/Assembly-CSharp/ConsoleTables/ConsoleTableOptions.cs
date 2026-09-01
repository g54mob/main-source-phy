using System.Collections.Generic;

namespace ConsoleTables
{
	public class ConsoleTableOptions
	{
		public IEnumerable<string> Columns { get; set; }

		public bool EnableCount { get; set; }

		public ConsoleTableOptions()
		{
			Columns = new List<string>();
			EnableCount = true;
		}
	}
}
