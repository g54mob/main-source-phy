using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Kamgam.SettingsGenerator
{
	public static class FindComponentUtils
	{
		public static T FindComponentInAllLoadedScenes<T>(bool includeInactive, Predicate<Scene> scenePredicate = null)
		{
			return default(T);
		}

		public static List<T> FindComponentsInAllLoadedScenes<T>(bool includeInactive, Predicate<Scene> scenePredicate = null)
		{
			return null;
		}

		public static List<T> FindComponentsInScenes<T>(bool includeInactive, params Scene[] scenes)
		{
			return null;
		}
	}
}
