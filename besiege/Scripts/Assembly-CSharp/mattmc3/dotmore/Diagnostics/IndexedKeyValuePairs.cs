using System.Collections;
using System.Diagnostics;

namespace mattmc3.dotmore.Diagnostics
{
	[DebuggerDisplay("{Value}", Name = "[{Index}]: {Key}")]
	internal class IndexedKeyValuePairs
	{
		public IDictionary Dictionary { get; private set; }

		public int Index { get; private set; }

		public object Key { get; private set; }

		public object Value { get; private set; }

		public IndexedKeyValuePairs(IDictionary dictionary, int index, object key, object value)
		{
			Index = index;
			Value = value;
			Key = key;
			Dictionary = dictionary;
		}
	}
}
