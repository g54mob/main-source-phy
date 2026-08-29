using System.Collections.Generic;
using GLTFast.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Scripting;

namespace GLTFast.Newtonsoft.Schema
{
	public class RootExtensions : GLTFast.Schema.RootExtensions, IJsonObject
	{
		[JsonExtensionData]
		private IDictionary<string, JToken> m_JsonExtensionData;

		[Preserve]
		public RootExtensions()
		{
		}

		public bool TryGetValue<T>(string key, out T value)
		{
			if (m_JsonExtensionData != null && m_JsonExtensionData.TryGetValue(key, out var value2))
			{
				value = value2.ToObject<T>();
				return true;
			}
			value = default(T);
			return false;
		}
	}
}
