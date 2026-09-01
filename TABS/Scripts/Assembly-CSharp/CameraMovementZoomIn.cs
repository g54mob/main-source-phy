using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.AI.Systems;
using Unity.Entities;
using UnityEngine;

public class CameraMovementZoomIn : MonoBehaviour
{
	public float force = 1f;

	private CameraMovement move;

	private TeamSystem m_teamSystem;

	private void Start()
	{
		m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
		move = GetComponent<CameraMovement>();
	}

	public void GoToCenter()
	{
		GoToCenterInternal(m_teamSystem.GetTeamUnits(Team.Red), m_teamSystem.GetTeamUnits(Team.Blue));
	}

	private void GoToCenterInternal(List<Unit> unitsRed, List<Unit> unitsBlue)
	{
		if (unitsRed == null || unitsBlue == null)
		{
			return;
		}
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		float num = float.MaxValue;
		float num2 = float.MinValue;
		float num3 = float.MaxValue;
		float num4 = float.MinValue;
		if (unitsRed != null)
		{
			for (int i = 0; i < unitsRed.Count; i++)
			{
				float x = unitsRed[i].data.mainRig.position.x;
				if (x < num)
				{
					num = x;
				}
				if (x > num2)
				{
					num2 = x;
				}
				float z = unitsRed[i].data.mainRig.position.z;
				if (z < num3)
				{
					num3 = z;
				}
				if (z > num4)
				{
					num4 = z;
				}
				zero += unitsRed[i].data.mainRig.position / unitsRed.Count;
			}
		}
		if (unitsBlue != null)
		{
			for (int j = 0; j < unitsBlue.Count; j++)
			{
				float x2 = unitsBlue[j].data.mainRig.position.x;
				if (x2 < num)
				{
					num = x2;
				}
				if (x2 > num2)
				{
					num2 = x2;
				}
				float z2 = unitsBlue[j].data.mainRig.position.z;
				if (z2 < num3)
				{
					num3 = z2;
				}
				if (z2 > num4)
				{
					num4 = z2;
				}
				zero2 += unitsBlue[j].data.mainRig.position / unitsBlue.Count;
			}
		}
		Vector3 vector = (zero + zero2) * 0.5f;
		float num5 = Mathf.Abs(num2 - num) + Mathf.Abs(num4 - num3) * 0.5f;
		StartCoroutine(GoToTargetPos(vector - base.transform.forward * (2f + Mathf.Pow(num5 * 1.1f, 0.9f))));
	}

	private IEnumerator GoToTargetPos(Vector3 targetPos)
	{
		float f = 0f;
		while (f < 0.5f)
		{
			move.Velocity += (targetPos - base.transform.position) * force * Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.02f);
			move.Velocity -= move.Velocity * Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.02f) * 15f;
			f += Time.unscaledDeltaTime;
			yield return null;
		}
	}
}
