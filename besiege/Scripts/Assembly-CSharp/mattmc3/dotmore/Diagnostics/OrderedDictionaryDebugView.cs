using System.Collections.Specialized;
using System.Diagnostics;

namespace mattmc3.dotmore.Diagnostics
{
	internal class OrderedDictionaryDebugView
	{
		private IOrderedDictionary _dict;

		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public IndexedKeyValuePairs[] IndexedKeyValuePairs
		{
			get
			{
				IndexedKeyValuePairs[] array = new IndexedKeyValuePairs[_dict.Count];
				int num = 0;
				foreach (object key in _dict.Keys)
				{
					array[num] = new IndexedKeyValuePairs(_dict, num, key, _dict[key]);
					num++;
				}
				return array;
			}
		}

		public OrderedDictionaryDebugView(IOrderedDictionary dict)
		{
			_dict = dict;
		}
	}
}
