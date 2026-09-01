using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ModIO.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace ModIO
{
	[AddComponentMenu("ModIO/Helpers/WebRequest Dispatcher")]
	public class WebRequestDispatcher : MonoBehaviour
	{
		private static WebRequestDispatcher _instance = null;

		private List<UnityWebRequestAsyncOperation> pendingOperations;

		private List<UnityWebRequestAsyncOperation> completeOperations;

		private static readonly Queue<Action> _executionQueue = new Queue<Action>();

		private static Thread MainThread = Thread.CurrentThread;

		public static WebRequestDispatcher instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = UIUtilities.FindComponentInAllScenes<WebRequestDispatcher>(true);
					if (_instance == null)
					{
						GameObject gameObject = new GameObject("WebRequest Dispatcher");
						_instance = gameObject.AddComponent<WebRequestDispatcher>();
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
					}
				}
				return _instance;
			}
		}

		public void Awake()
		{
			pendingOperations = new List<UnityWebRequestAsyncOperation>();
			completeOperations = new List<UnityWebRequestAsyncOperation>();
		}

		public void OnDestroy()
		{
			_executionQueue.Clear();
		}

		public void DispatchAction(Action action)
		{
			lock (_executionQueue)
			{
				_executionQueue.Enqueue(action);
			}
		}

		public static void Dispatch(Action action)
		{
			if (instance == null)
			{
				Debug.LogWarning("Could not dispatch coroutine, instance is null");
			}
			else
			{
				instance.DispatchAction(action);
			}
		}

		public static void Dispatch(IEnumerator coroutine)
		{
			if (instance == null)
			{
				Debug.LogWarning("Could not dispatch coroutine, instance is null");
			}
			else
			{
				instance.StartCoroutine(coroutine);
			}
		}

		public static UnityWebRequestAsyncOperation Dispatch(UnityWebRequest request)
		{
			return instance.DispatchRequest(request);
		}

		private void AssertMainThread()
		{
		}

		public UnityWebRequestAsyncOperation DispatchRequest(UnityWebRequest request)
		{
			AssertMainThread();
			AsyncOperation operation = request.Send();
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = new UnityWebRequestAsyncOperation(request, operation);
			if (unityWebRequestAsyncOperation.isDone)
			{
				completeOperations.Add(unityWebRequestAsyncOperation);
			}
			else
			{
				pendingOperations.Add(unityWebRequestAsyncOperation);
			}
			return unityWebRequestAsyncOperation;
		}

		public void Update()
		{
			ProcessCompleteOperations();
			ProcessOperations();
			ProcessActions();
		}

		private void ProcessActions()
		{
			lock (_executionQueue)
			{
				while (_executionQueue.Count > 0)
				{
					_executionQueue.Dequeue()();
				}
			}
		}

		private void ProcessCompleteOperations()
		{
			for (int i = 0; i < completeOperations.Count; i++)
			{
				completeOperations[i].InvokeCompletionEvent();
			}
			completeOperations.Clear();
		}

		private void ProcessOperations()
		{
			if (pendingOperations.Count == 0)
			{
				return;
			}
			for (int num = pendingOperations.Count - 1; num >= 0; num--)
			{
				if (pendingOperations[num].isDone)
				{
					completeOperations.Add(pendingOperations[num]);
					pendingOperations.RemoveAt(num);
				}
			}
		}
	}
}
