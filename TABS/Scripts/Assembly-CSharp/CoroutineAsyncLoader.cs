using System;
using System.Collections;
using UnityEngine;

public class CoroutineAsyncLoader : MonoBehaviour
{
	private Coroutine m_Coroutine;

	public Coroutine DoCoroutine(AsyncOperation op, Action a)
	{
		return StartCoroutine(AsyncCoroutine(op, a));
	}

	private IEnumerator AsyncCoroutine(AsyncOperation op, Action a)
	{
		yield return op;
		Debug.Log("Finished UnLoading GameScene Async: " + Time.frameCount);
		a();
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
