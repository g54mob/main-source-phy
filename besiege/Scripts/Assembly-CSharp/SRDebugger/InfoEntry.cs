using System;
using SRF;

namespace SRDebugger
{
	public sealed class InfoEntry
	{
		private Func<object> _valueGetter;

		public string Title { get; set; }

		public object Value
		{
			get
			{
				try
				{
					return _valueGetter();
				}
				catch (Exception ex)
				{
					return "Error ({0})".Fmt(ex.GetType().Name);
				}
			}
		}

		public bool IsPrivate { get; private set; }

		public static InfoEntry Create(string name, Func<object> getter, bool isPrivate = false)
		{
			InfoEntry infoEntry = new InfoEntry();
			infoEntry.Title = name;
			infoEntry._valueGetter = getter;
			infoEntry.IsPrivate = isPrivate;
			return infoEntry;
		}

		public static InfoEntry Create(string name, object value, bool isPrivate = false)
		{
			InfoEntry infoEntry = new InfoEntry();
			infoEntry.Title = name;
			infoEntry._valueGetter = () => value;
			infoEntry.IsPrivate = isPrivate;
			return infoEntry;
		}
	}
}
