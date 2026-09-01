using System;
using UnityEngine;

[Serializable]
public class ObjectToSpawn
{
	public enum SpawnRotation
	{
		normal = 0,
		forward = 1,
		identity = 2,
		random = 3,
		towardsRandomTarget = 4
	}

	public GameObject objectToSpawn;

	public int particleID = -1;

	public SpawnRotation spawnRotation;

	public bool childOfThis;

	public bool removeObjectsToSpawn;

	public float delay;

	public bool destroy;

	public bool spawnOnce;
}
