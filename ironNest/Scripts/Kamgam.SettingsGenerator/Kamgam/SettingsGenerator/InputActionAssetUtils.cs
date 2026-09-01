using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Kamgam.SettingsGenerator
{
	public static class InputActionAssetUtils
	{
		public static float _lastCacheTime;

		public static List<InputActionAsset> _cachedResults;

		public static List<InputActionAsset> FindInstancesOf(InputActionAsset baseAsset, List<InputActionAsset> results = null, float cacheDurationInSec = 0f)
		{
			return null;
		}

		private static Guid getFirstBindingGuid(InputActionAsset asset)
		{
			return default(Guid);
		}
	}
}
