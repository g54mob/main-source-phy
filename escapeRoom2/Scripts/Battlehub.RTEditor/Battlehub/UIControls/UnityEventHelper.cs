using System;
using UnityEngine;
using UnityEngine.Events;

namespace Battlehub.UIControls
{
	public static class UnityEventHelper
	{
		public static void AddListener<T>(T control, Func<T, UnityEvent> evt, UnityAction action) where T : MonoBehaviour
		{
			if (!(control == null))
			{
				evt(control).AddListener(action);
			}
		}

		public static void RemoveListener<T>(T control, Func<T, UnityEvent> evt, UnityAction action) where T : MonoBehaviour
		{
			if (!(control == null))
			{
				evt(control).RemoveListener(action);
			}
		}

		public static void RemoveAllListeners<T>(T control, Func<T, UnityEvent> evt) where T : MonoBehaviour
		{
			if (!(control == null))
			{
				evt(control).RemoveAllListeners();
			}
		}

		public static void AddListener<T, V>(T control, Func<T, UnityEvent<V>> evt, UnityAction<V> action) where T : MonoBehaviour
		{
			if (!(control == null))
			{
				evt(control).AddListener(action);
			}
		}

		public static void RemoveListener<T, V>(T control, Func<T, UnityEvent<V>> evt, UnityAction<V> action) where T : MonoBehaviour
		{
			if (!(control == null))
			{
				evt(control).RemoveListener(action);
			}
		}

		public static void RemoveAllListeners<T, V>(T control, Func<T, UnityEvent<V>> evt) where T : MonoBehaviour
		{
			if (!(control == null))
			{
				evt(control).RemoveAllListeners();
			}
		}
	}
}
