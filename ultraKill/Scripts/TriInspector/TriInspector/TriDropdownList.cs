using System.Collections.Generic;

namespace TriInspector
{
	public class TriDropdownList<T> : List<TriDropdownItem<T>>
	{
		public void Add(string text, T value)
		{
			Add(new TriDropdownItem<T>
			{
				Text = text,
				Value = value
			});
		}
	}
}
