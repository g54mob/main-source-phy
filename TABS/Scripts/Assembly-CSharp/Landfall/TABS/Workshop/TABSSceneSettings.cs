using System;
using Landfall.TABS.WinConditions;
using UnityEngine;

namespace Landfall.TABS.Workshop
{
	[Serializable]
	public class TABSSceneSettings
	{
		public float TeamLineCenterX;

		public float TeamLineCenterY;

		public float TeamLineCenterZ;

		public float TeamLineRotation;

		public float TeamLineRadius;

		public int TeamLineType;

		public bool TeamSwap;

		public SerializedEvaluator[] WinEvaluators;

		public void SetTeamLineCenter(Vector3 value)
		{
			TeamLineCenterX = value.x;
			TeamLineCenterY = value.y;
			TeamLineCenterZ = value.z;
		}

		public Vector3 GetTeamLineCenter()
		{
			return new Vector3(TeamLineCenterX, TeamLineCenterY, TeamLineCenterZ);
		}

		public TABSSceneSettings(Vector3 center, float rot, float radius, int type, bool teamSwap, SerializedEvaluator[] winEvaluators)
		{
			SetTeamLineCenter(center);
			TeamLineRotation = rot;
			TeamLineRadius = radius;
			TeamLineType = type;
			TeamSwap = teamSwap;
			WinEvaluators = winEvaluators;
		}
	}
}
