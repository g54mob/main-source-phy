using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SleepyNodes
{
	public static class NodeDataCache
	{
		[Serializable]
		private class PortDataCache : Dictionary<Type, List<NodePort>>, ISerializationCallbackReceiver
		{
			[SerializeField]
			private List<Type> keys;

			[SerializeField]
			private List<List<NodePort>> values;

			public void OnBeforeSerialize()
			{
			}

			public void OnAfterDeserialize()
			{
			}
		}

		private static PortDataCache portDataCache;

		private static bool Initialized => false;

		public static void UpdatePorts(Node node, Dictionary<string, NodePort> ports)
		{
		}

		private static void BuildCache()
		{
		}

		public static List<FieldInfo> GetNodeFields(Type nodeType)
		{
			return null;
		}

		private static void CachePorts(Type nodeType)
		{
		}
	}
}
