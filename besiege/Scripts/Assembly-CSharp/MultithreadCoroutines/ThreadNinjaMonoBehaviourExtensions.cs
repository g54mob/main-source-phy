using System.Collections;
using UnityEngine;

namespace MultithreadCoroutines
{
	public static class ThreadNinjaMonoBehaviourExtensions
	{
		public static Coroutine StartCoroutineAsync(this MonoBehaviour behaviour, IEnumerator routine, out Task task)
		{
			task = new Task(routine);
			return behaviour.StartCoroutine(task);
		}

		public static Coroutine StartCoroutineAsync(this MonoBehaviour behaviour, IEnumerator routine)
		{
			Task task;
			return behaviour.StartCoroutineAsync(routine, out task);
		}
	}
}
