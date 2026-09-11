namespace TriInspector
{
	public struct TriDropdownItem : ITriDropdownItem
	{
		public string Text { get; set; }

		public object Value { get; set; }
	}
	public struct TriDropdownItem<T> : ITriDropdownItem
	{
		public string Text;

		public T Value;

		string ITriDropdownItem.Text => Text;

		object ITriDropdownItem.Value => Value;
	}
}
