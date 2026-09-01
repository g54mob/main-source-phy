using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class MetadataKVP
	{
		[JsonProperty("metakey")]
		public string key;

		[JsonProperty("metavalue")]
		public string value;

		public static Dictionary<string, string> ArrayToDictionary(MetadataKVP[] kvpArray)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(kvpArray.Length);
			foreach (MetadataKVP metadataKVP in kvpArray)
			{
				if (!string.IsNullOrEmpty(metadataKVP.key))
				{
					dictionary.Add(metadataKVP.key, metadataKVP.value);
				}
			}
			return dictionary;
		}

		public static MetadataKVP[] DictionaryToArray(Dictionary<string, string> dictionary)
		{
			MetadataKVP[] array = new MetadataKVP[dictionary.Count];
			int num = 0;
			foreach (KeyValuePair<string, string> item in dictionary)
			{
				MetadataKVP metadataKVP = new MetadataKVP();
				metadataKVP.key = item.Key;
				metadataKVP.value = item.Value;
				MetadataKVP metadataKVP2 = metadataKVP;
				array[num++] = metadataKVP2;
			}
			return array;
		}

		public static Dictionary<string, List<string>> ArrayToDictionary_DuplicateKeys(MetadataKVP[] kvpArray)
		{
			List<string> list = null;
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>(kvpArray.Length);
			foreach (MetadataKVP metadataKVP in kvpArray)
			{
				if (!string.IsNullOrEmpty(metadataKVP.key))
				{
					if (!dictionary.TryGetValue(metadataKVP.key, out list))
					{
						list = new List<string>();
						dictionary[metadataKVP.key] = list;
					}
					list.Add(metadataKVP.value);
				}
			}
			return dictionary;
		}

		public static IList<MetadataKVP> DictionaryToArray(Dictionary<string, List<string>> dictionary)
		{
			List<MetadataKVP> list = new List<MetadataKVP>();
			foreach (KeyValuePair<string, List<string>> item3 in dictionary)
			{
				if (item3.Value == null)
				{
					MetadataKVP metadataKVP = new MetadataKVP();
					metadataKVP.key = item3.Key;
					metadataKVP.value = null;
					MetadataKVP item = metadataKVP;
					list.Add(item);
					continue;
				}
				foreach (string item4 in item3.Value)
				{
					MetadataKVP metadataKVP = new MetadataKVP();
					metadataKVP.key = item3.Key;
					metadataKVP.value = item4;
					MetadataKVP item2 = metadataKVP;
					list.Add(item2);
				}
			}
			return list;
		}
	}
}
