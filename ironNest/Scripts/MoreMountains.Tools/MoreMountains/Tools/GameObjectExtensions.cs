using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public static class GameObjectExtensions
	{
		private static List<Component> m_ComponentCache;

		public static Component MMGetComponentNoAlloc(this GameObject @this, Type componentType)
		{
			return null;
		}

		public static T MMGetComponentNoAlloc<T>(this GameObject @this) where T : Component
		{
			return null;
		}

		public static T MMGetComponentAroundOrAdd<T>(this GameObject @this) where T : Component
		{
			return null;
		}

		public static T MMGetOrAddComponent<T>(this GameObject @this) where T : Component
		{
			return null;
		}

		public static (T, bool) MMFindOrCreateObjectOfType<T>(this GameObject @this, string newObjectName, Transform parent, bool forceNewCreation = false) where T : Component
		{
			return default((T, bool));
		}
	}
}
