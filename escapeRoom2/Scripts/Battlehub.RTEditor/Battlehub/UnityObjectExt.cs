using System;
using UnityEngine;

namespace Battlehub
{
	public static class UnityObjectExt
	{
		public static T FindAnyObjectByType<T>() where T : UnityEngine.Object
		{
			return UnityEngine.Object.FindAnyObjectByType<T>();
		}

		public static UnityEngine.Object FindAnyObjectByType(Type type)
		{
			return UnityEngine.Object.FindAnyObjectByType(type);
		}

		public static T[] FindObjectsByType<T>() where T : UnityEngine.Object
		{
			return UnityEngine.Object.FindObjectsByType<T>(FindObjectsSortMode.None);
		}

		public static T[] FindObjectsByType<T>(bool includeInactive) where T : UnityEngine.Object
		{
			return UnityEngine.Object.FindObjectsByType<T>(includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude, FindObjectsSortMode.None);
		}
	}
}
