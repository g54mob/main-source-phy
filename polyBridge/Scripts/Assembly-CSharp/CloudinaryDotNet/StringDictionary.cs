using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CloudinaryDotNet
{
	public class StringDictionary : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		private List<KeyValuePair<string, string>> m_list = new List<KeyValuePair<string, string>>();

		public bool Sort { get; set; }

		public int Count => m_list.Count;

		public string[] Pairs => m_list.Select((KeyValuePair<string, string> pair) => (pair.Value != null) ? (pair.Key + "=" + pair.Value) : pair.Key).ToArray();

		public string[] SafePairs => m_list.Select((KeyValuePair<string, string> pair) => (!string.IsNullOrEmpty(pair.Value)) ? (EscapeSafePairString(pair.Key) + "=" + EscapeSafePairString(pair.Value)) : EscapeSafePairString(pair.Key)).ToArray();

		public ICollection<string> Keys => m_list.Select((KeyValuePair<string, string> pair) => pair.Key).ToArray();

		public ICollection<string> Values => m_list.Select((KeyValuePair<string, string> pair) => pair.Value).ToArray();

		public bool IsReadOnly => false;

		public string this[string key]
		{
			get
			{
				foreach (KeyValuePair<string, string> item in m_list)
				{
					if (item.Key == key)
					{
						return item.Value;
					}
				}
				return null;
			}
			set
			{
				KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(key, value);
				bool flag = false;
				for (int i = 0; i < m_list.Count; i++)
				{
					if (m_list[i].Key == key)
					{
						m_list[i] = keyValuePair;
						flag = true;
					}
				}
				if (!flag)
				{
					m_list.Add(keyValuePair);
				}
			}
		}

		public StringDictionary()
		{
		}

		public StringDictionary(params string[] keyValuePairs)
		{
			foreach (string text in keyValuePairs)
			{
				int num = text.IndexOf('=');
				if (num == -1)
				{
					Add(text, (string)null);
				}
				else
				{
					Add(text.Substring(0, num), text.Substring(num + 1));
				}
			}
		}

		public void Add(string key, string value)
		{
			KeyValuePair<string, string> item = new KeyValuePair<string, string>(key, value);
			m_list.Add(item);
		}

		public void Add(string key, List<string> value)
		{
			Add(key, JsonConvert.SerializeObject(value));
		}

		public string Remove(string key)
		{
			foreach (KeyValuePair<string, string> item in m_list)
			{
				if (item.Key == key)
				{
					m_list.Remove(item);
					return item.Value;
				}
			}
			return null;
		}

		public void Clear()
		{
			m_list.Clear();
		}

		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			if (Sort)
			{
				return new SortedList<string, string>(this).GetEnumerator();
			}
			return m_list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool ContainsKey(string key)
		{
			foreach (KeyValuePair<string, string> item in m_list)
			{
				if (item.Key == key)
				{
					return true;
				}
			}
			return false;
		}

		bool IDictionary<string, string>.Remove(string key)
		{
			foreach (KeyValuePair<string, string> item in m_list)
			{
				if (item.Key == key)
				{
					m_list.Remove(item);
					return true;
				}
			}
			return false;
		}

		public bool TryGetValue(string key, out string value)
		{
			value = null;
			foreach (KeyValuePair<string, string> item in m_list)
			{
				if (item.Key == key)
				{
					value = item.Value;
					return true;
				}
			}
			return false;
		}

		public void Add(KeyValuePair<string, string> item)
		{
			m_list.Add(item);
		}

		public bool Contains(KeyValuePair<string, string> item)
		{
			return m_list.Contains(item);
		}

		public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
		{
			m_list.CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<string, string> item)
		{
			return m_list.Remove(item);
		}

		private static string EscapeSafePairString(string value)
		{
			return value.Replace("=", "\\=");
		}
	}
}
