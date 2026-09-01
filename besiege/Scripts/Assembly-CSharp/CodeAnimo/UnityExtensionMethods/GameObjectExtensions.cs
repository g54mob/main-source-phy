using System;
using UnityEngine;

namespace CodeAnimo.UnityExtensionMethods
{
	public static class GameObjectExtensions
	{
		public static bool Unity4_3_4UndoCrashWorkaroundEnabled = true;

		public static T AddComponentIfMissing<T>(this GameObject gameObject) where T : Component
		{
			T component = gameObject.GetComponent<T>();
			if (component == null)
			{
				return AddComponent<T>(gameObject);
			}
			return (T)null;
		}

		public static U AddComponentIfMissing<T, U>(this GameObject gameObject) where T : Component where U : T
		{
			T component = gameObject.GetComponent<T>();
			if (component == null)
			{
				return AddComponent<U>(gameObject);
			}
			return (U)null;
		}

		public static T AddComponent<T>(this GameObject targetObject) where T : Component
		{
			return targetObject.AddComponent<T>();
		}

		private static int GetCurrentUndoGroup()
		{
			throw new NotImplementedException("Must be called from Unity Editor");
		}

		private static Type GetUndoType()
		{
			throw new NotImplementedException("Must be called from Unity Editor");
		}

		public static T AddComponentIfMissingAndCopySettings<T>(this GameObject gameObject, GameObject standardSettingsPrefab) where T : Component
		{
			T val = gameObject.AddComponentIfMissing<T>();
			if (val != null)
			{
				val.ApplyPrefabSettings(standardSettingsPrefab);
			}
			return val;
		}
	}
}
