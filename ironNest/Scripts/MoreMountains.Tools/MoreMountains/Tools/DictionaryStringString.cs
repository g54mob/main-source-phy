using System;
using System.Runtime.Serialization;

namespace MoreMountains.Tools
{
	[Serializable]
	public class DictionaryStringString : MMSerializableDictionary<string, string>
	{
		public DictionaryStringString()
		{
		}

		protected DictionaryStringString(SerializationInfo info, StreamingContext context)
		{
		}
	}
}
