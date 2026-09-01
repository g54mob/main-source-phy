using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[ExecuteAlways]
	public static class MMEventManager
	{
		private static Dictionary<Type, List<MMEventListenerBase>> _subscribersList;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void InitializeStatics()
		{
		}

		static MMEventManager()
		{
		}

		public static void AddListener<MMEvent>(MMEventListener<MMEvent> listener) where MMEvent : struct
		{
		}

		public static void RemoveListener<MMEvent>(MMEventListener<MMEvent> listener) where MMEvent : struct
		{
		}

		public static void TriggerEvent<MMEvent>(MMEvent newEvent) where MMEvent : struct
		{
		}

		private static bool SubscriptionExists(Type type, MMEventListenerBase receiver)
		{
			return false;
		}
	}
}
