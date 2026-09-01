using System.Collections;
using UnityEngine;

public class DestroyOnSimulate : SimBehaviour
{
	public int waitFrames = 2;

	public int randomFrames;

	public bool physicsFrames = true;

	public Transform[] objsToDestroy;

	public bool destroyEarly;

	public bool destroyThis;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating)
		{
			StartCoroutine(StartDestroy());
		}
	}

	private IEnumerator StartDestroy()
	{
		if (destroyEarly)
		{
			DestroyObjects();
			yield break;
		}
		for (int i = 0; i < Random.Range(waitFrames, waitFrames + randomFrames); i++)
		{
			if (physicsFrames)
			{
				yield return new WaitForFixedUpdate();
			}
			else
			{
				yield return null;
			}
		}
		DestroyObjects();
	}

	private void DestroyObjects()
	{
		for (int i = 0; i < objsToDestroy.Length; i++)
		{
			Object.Destroy(objsToDestroy[i].gameObject);
		}
		if (destroyThis)
		{
			Object.Destroy(this);
		}
	}
}
