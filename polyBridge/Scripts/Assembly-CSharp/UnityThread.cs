using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityThread : MonoBehaviour
{
	private static UnityThread instance = null;

	private static List<Action> actionQueuesUpdateFunc = new List<Action>();

	private List<Action> actionCopiedQueueUpdateFunc = new List<Action>();

	private static volatile bool noActionQueueToExecuteUpdateFunc = true;

	private static List<Action> actionQueuesLateUpdateFunc = new List<Action>();

	private List<Action> actionCopiedQueueLateUpdateFunc = new List<Action>();

	private static volatile bool noActionQueueToExecuteLateUpdateFunc = true;

	private static List<Action> actionQueuesFixedUpdateFunc = new List<Action>();

	private List<Action> actionCopiedQueueFixedUpdateFunc = new List<Action>();

	private static volatile bool noActionQueueToExecuteFixedUpdateFunc = true;

	public static void initUnityThread(bool visible = false)
	{
		if (!(instance != null) && Application.isPlaying)
		{
			GameObject gameObject = new GameObject("MainThreadExecuter");
			if (!visible)
			{
				gameObject.hideFlags = HideFlags.HideAndDontSave;
			}
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			instance = gameObject.AddComponent<UnityThread>();
		}
	}

	public void Awake()
	{
	}

	public static void executeCoroutine(IEnumerator action)
	{
		if (instance != null)
		{
			executeInUpdate(delegate
			{
				instance.StartCoroutine(action);
			});
		}
	}

	public static void executeInUpdate(Action action)
	{
		if (action == null)
		{
			throw new ArgumentNullException("action");
		}
		lock (actionQueuesUpdateFunc)
		{
			actionQueuesUpdateFunc.Add(action);
			noActionQueueToExecuteUpdateFunc = false;
		}
	}

	public void Update()
	{
		if (!noActionQueueToExecuteUpdateFunc)
		{
			actionCopiedQueueUpdateFunc.Clear();
			lock (actionQueuesUpdateFunc)
			{
				actionCopiedQueueUpdateFunc.AddRange(actionQueuesUpdateFunc);
				actionQueuesUpdateFunc.Clear();
				noActionQueueToExecuteUpdateFunc = true;
			}
			for (int i = 0; i < actionCopiedQueueUpdateFunc.Count; i++)
			{
				actionCopiedQueueUpdateFunc[i]();
			}
		}
	}

	public static void executeInLateUpdate(Action action)
	{
		if (action == null)
		{
			throw new ArgumentNullException("action");
		}
		lock (actionQueuesLateUpdateFunc)
		{
			actionQueuesLateUpdateFunc.Add(action);
			noActionQueueToExecuteLateUpdateFunc = false;
		}
	}

	public void LateUpdate()
	{
		if (!noActionQueueToExecuteLateUpdateFunc)
		{
			actionCopiedQueueLateUpdateFunc.Clear();
			lock (actionQueuesLateUpdateFunc)
			{
				actionCopiedQueueLateUpdateFunc.AddRange(actionQueuesLateUpdateFunc);
				actionQueuesLateUpdateFunc.Clear();
				noActionQueueToExecuteLateUpdateFunc = true;
			}
			for (int i = 0; i < actionCopiedQueueLateUpdateFunc.Count; i++)
			{
				actionCopiedQueueLateUpdateFunc[i]();
			}
		}
	}

	public static void executeInFixedUpdate(Action action)
	{
		if (action == null)
		{
			throw new ArgumentNullException("action");
		}
		lock (actionQueuesFixedUpdateFunc)
		{
			actionQueuesFixedUpdateFunc.Add(action);
			noActionQueueToExecuteFixedUpdateFunc = false;
		}
	}

	public void FixedUpdate()
	{
		if (!noActionQueueToExecuteFixedUpdateFunc)
		{
			actionCopiedQueueFixedUpdateFunc.Clear();
			lock (actionQueuesFixedUpdateFunc)
			{
				actionCopiedQueueFixedUpdateFunc.AddRange(actionQueuesFixedUpdateFunc);
				actionQueuesFixedUpdateFunc.Clear();
				noActionQueueToExecuteFixedUpdateFunc = true;
			}
			for (int i = 0; i < actionCopiedQueueFixedUpdateFunc.Count; i++)
			{
				actionCopiedQueueFixedUpdateFunc[i]();
			}
		}
	}

	public void OnDisable()
	{
		if (instance == this)
		{
			instance = null;
		}
	}
}
