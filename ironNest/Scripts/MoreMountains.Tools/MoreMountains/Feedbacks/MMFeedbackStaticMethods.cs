using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public static class MMFeedbackStaticMethods
	{
		private static List<Component> m_ComponentCache;

		public static Component GetComponentNoAlloc(this GameObject @this, Type componentType)
		{
			return null;
		}

		public static Type MMFGetTypeByName(string name)
		{
			return null;
		}

		public static T MMFGetComponentNoAlloc<T>(this GameObject @this) where T : Component
		{
			return null;
		}
	}
}
