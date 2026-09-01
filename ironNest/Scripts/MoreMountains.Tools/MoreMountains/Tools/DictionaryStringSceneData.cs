using System;
using System.Runtime.Serialization;

namespace MoreMountains.Tools
{
	[Serializable]
	public class DictionaryStringSceneData : MMSerializableDictionary<string, MMPersistenceSceneData>
	{
		public DictionaryStringSceneData()
		{
		}

		protected DictionaryStringSceneData(SerializationInfo info, StreamingContext context)
		{
		}
	}
}
