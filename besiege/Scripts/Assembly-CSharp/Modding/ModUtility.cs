using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Modding
{
	public static class ModUtility
	{
		private static int windowId = int.MaxValue;

		public static int GetWindowId()
		{
			return windowId--;
		}

		public static T CopyComponent<T>(T original, GameObject destination) where T : Component
		{
			Type type = original.GetType();
			Component component = destination.AddComponent(type);
			return CopyComponentValues(original, (T)component);
		}

		public static T CopyComponentValues<T>(T original, T destination) where T : Component
		{
			Type type = original.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			IEnumerable<FieldInfo> second = from f in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
				where f.IsDefined(typeof(SerializeField), false)
				select f;
			foreach (FieldInfo item in fields.Concat(second))
			{
				item.SetValue(destination, item.GetValue(original));
			}
			return destination;
		}
	}
}
