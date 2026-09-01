namespace Mono.CSharp
{
	public class LocatedToken
	{
		public int row;

		public int column;

		public string value;

		public SourceFile file;

		public Location Location
		{
			get
			{
				return new Location(file, row, column);
			}
		}

		public string Value
		{
			get
			{
				return value;
			}
		}

		public LocatedToken()
		{
		}

		public LocatedToken(string value, Location loc)
		{
			this.value = value;
			file = loc.SourceFile;
			row = loc.Row;
			column = loc.Column;
		}

		public override string ToString()
		{
			return string.Format("Token '{0}' at {1},{2}", Value, row, column);
		}
	}
}
