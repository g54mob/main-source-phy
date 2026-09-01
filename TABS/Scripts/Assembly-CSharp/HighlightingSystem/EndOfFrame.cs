using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

namespace HighlightingSystem
{
	[ExcludeFromDocs]
	public class EndOfFrame : MonoBehaviour
	{
		[ExcludeFromDocs]
		public delegate void OnEndOfFrame();

		private static EndOfFrame _singleton;

		private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

		private Coroutine coroutine;

		private List<OnEndOfFrame> listeners = new List<OnEndOfFrame>();

		private static EndOfFrame singleton
		{
			get
			{
				if (_singleton == null)
				{
					_singleton = new GameObject("EndOfFrameHelper")
					{
						hideFlags = HideFlags.HideAndDontSave
					}.AddComponent<EndOfFrame>();
				}
				return _singleton;
			}
		}

		private void OnEnable()
		{
			coroutine = StartCoroutine(EndOfFrameRoutine());
		}

		private void OnDisable()
		{
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
			}
		}

		public static void AddListener(OnEndOfFrame listener)
		{
			if (listener != null)
			{
				singleton.listeners.Add(listener);
			}
		}

		public static void RemoveListener(OnEndOfFrame listener)
		{
			if (listener != null && !(_singleton == null))
			{
				singleton.listeners.Remove(listener);
			}
		}

		private IEnumerator EndOfFrameRoutine()
		{
			while (true)
			{
				yield return waitForEndOfFrame;
				for (int num = listeners.Count - 1; num >= 0; num--)
				{
					OnEndOfFrame onEndOfFrame = listeners[num];
					if (onEndOfFrame != null)
					{
						onEndOfFrame();
					}
					else
					{
						listeners.RemoveAt(num);
					}
				}
			}
		}
	}
}
