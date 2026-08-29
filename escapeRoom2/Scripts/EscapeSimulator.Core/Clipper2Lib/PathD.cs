using System.Collections.Generic;

namespace Clipper2Lib
{
	public class PathD : List<PointD>
	{
		public PathD()
		{
		}

		public PathD(int capacity = 0)
			: base(capacity)
		{
		}

		public PathD(IEnumerable<PointD> path)
			: base(path)
		{
		}

		public string ToString(int precision = 2)
		{
			string text = "";
			using (Enumerator enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					text = text + enumerator.Current.ToString(precision) + ", ";
				}
			}
			if (text != "")
			{
				text = text.Remove(text.Length - 2);
			}
			return text;
		}
	}
}
