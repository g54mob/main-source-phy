using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CatFloor
{
	public string floorName;

	public List<Transform> spots;

	public Transform defaultSpot;

	public float GetAverageY()
	{
		return 0f;
	}
}
