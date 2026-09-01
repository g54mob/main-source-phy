using System;
using UnityEngine;

public class RandomZRotation : MonoBehaviour, GameObjectPooling.IPoolable
{
	public float min;

	public float max = 360f;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	public void RandomizeRotation()
	{
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, UnityEngine.Random.Range(min, max));
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
	}

	public void Release()
	{
	}

	private void InitializeOnSpawn()
	{
		RandomizeRotation();
	}
}
