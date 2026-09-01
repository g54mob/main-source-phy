using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMSerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField]
		protected List<TKey> _keys;

		[SerializeField]
		protected List<TValue> _values;

		public MMSerializableDictionary()
		{
		}

		public MMSerializableDictionary(SerializationInfo info, StreamingContext context)
		{
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
		}
	}
}
