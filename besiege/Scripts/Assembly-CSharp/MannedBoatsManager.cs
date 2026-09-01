using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/AI/MannedBoatsManager")]
public class MannedBoatsManager : MonoBehaviour
{
	private List<MannedBoatAI> boats = new List<MannedBoatAI>();

	public float minRange = 200f;

	public float maxRange = 500f;

	public float aggroRange = 30000f;

	public static MannedBoatsManager instance;

	private void Awake()
	{
		if (!StatMaster.levelSimulating)
		{
			instance = this;
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (boats.Count != MannedBoatAI.boats.Count)
		{
			boats = new List<MannedBoatAI>(MannedBoatAI.boats);
		}
		foreach (MannedBoatAI boat in boats)
		{
			float num = float.MaxValue;
			float num2 = float.MaxValue;
			float num3 = float.MaxValue;
			float num4 = 100f;
			float num5 = 100f;
			float num6 = 100f;
			Vector3 center = boat.Center;
			foreach (MannedBoatAI boat2 in boats)
			{
				if (!(boat != boat2))
				{
					continue;
				}
				Vector3 center2 = boat2.Center;
				float sqrMagnitude = (center2 - center).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					float x = boat.looker.InverseTransformPoint(center2).x;
					if (x < 0f)
					{
						num = sqrMagnitude;
						num4 = x;
						continue;
					}
				}
				if (sqrMagnitude < num2)
				{
					float x2 = boat.looker.InverseTransformPoint(center2).x;
					if (x2 > 0f)
					{
						num2 = sqrMagnitude;
						num5 = x2;
					}
				}
				if (sqrMagnitude < num3)
				{
					Vector3 vector = boat.looker.InverseTransformPoint(center2);
					if (vector.z > 0f)
					{
						num3 = sqrMagnitude;
						num6 = vector.z + Mathf.Abs(vector.x) * 0.5f;
					}
				}
			}
			boat.leftDist = num4 * num4;
			boat.rightDist = num5 * num5;
			boat.frontDist = num6 * num6;
			boat.Move();
		}
	}
}
