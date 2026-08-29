using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Executor : MonoBehaviour
{
	private class TaskToExecute
	{
		public Task task;

		public Action onTaskCompleted;

		public TaskToExecute(Task task, Action onTaskCompleted)
		{
			this.task = task;
			this.onTaskCompleted = onTaskCompleted;
		}
	}

	private static Executor instance;

	private readonly List<TaskToExecute> tasksToExecute = new List<TaskToExecute>();

	public static Coroutine executeCoroutine(IEnumerator routine)
	{
		return get().StartCoroutine(routine);
	}

	public static void executeTask(Task task, Action onTaskCompleted = null)
	{
		get().tasksToExecute.Add(new TaskToExecute(task, onTaskCompleted));
	}

	private static Executor get()
	{
		if (instance != null)
		{
			return instance;
		}
		instance = new GameObject("Executor").AddComponent<Executor>();
		UnityEngine.Object.DontDestroyOnLoad(instance);
		return instance;
	}

	private void Update()
	{
		for (int num = tasksToExecute.Count - 1; num >= 0; num--)
		{
			TaskToExecute taskToExecute = tasksToExecute[num];
			if (taskToExecute.task.IsCompleted)
			{
				taskToExecute.onTaskCompleted?.Invoke();
				tasksToExecute.Remove(taskToExecute);
			}
		}
	}
}
