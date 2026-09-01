using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.AI.Systems;
using Unity.Entities;
using UnityEngine;

public class MainMenuCameraMove : MonoBehaviour
{
	private TeamSystem m_teamSystem;

	private float spring = 50f;

	private float drag = 5f;

	private Vector3 vel;

	private bool chosenFastest;

	private Unit followedUnit;

	private int frameCount;

	private void Update()
	{
		if (m_teamSystem == null)
		{
			m_teamSystem = World.Active.GetExistingManager<TeamSystem>();
			return;
		}
		frameCount++;
		if (frameCount >= 10)
		{
			List<Unit> teamUnits = m_teamSystem.GetTeamUnits(Team.Red);
			List<Unit> teamUnits2 = m_teamSystem.GetTeamUnits(Team.Blue);
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			for (int i = 0; i < teamUnits?.Count; i++)
			{
			}
			for (int j = 0; j < teamUnits2?.Count; j++)
			{
				zero2 += teamUnits2[j].data.mainRig.transform.position / teamUnits2.Count;
			}
			Vector3 vector = (zero2 + zero) * 0.5f;
			if (followedUnit != null)
			{
				vector = followedUnit.data.mainRig.transform.position;
			}
			if (base.transform.position == Vector3.zero)
			{
				base.transform.position = vector;
			}
			vel += spring * Time.deltaTime * (vector + Vector3.up * 0.5f - base.transform.position);
			vel -= drag * Time.deltaTime * vel;
			base.transform.position += vel * Time.deltaTime;
		}
	}
}
