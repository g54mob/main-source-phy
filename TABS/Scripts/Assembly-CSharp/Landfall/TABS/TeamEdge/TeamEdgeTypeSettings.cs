using UnityEngine;

namespace Landfall.TABS.TeamEdge
{
	[CreateAssetMenu(fileName = "TeamEdgeTypeSettings", menuName = "Landfall/TeamEdgeTypeSettings", order = 999999999)]
	public class TeamEdgeTypeSettings : ScriptableObject
	{
		[Header("Colors")]
		public Color m_RedColor = Color.red;

		public Color m_BlueColor = Color.blue;

		[Space(10f)]
		[Header("Line Settings")]
		public float m_LineThickness = 0.5f;

		public float m_PerlinFrequency = 1f;

		public float m_PerlinScrollSpeed = 30f;

		public float m_PerlinStrength = 0.5f;

		[Space(10f)]
		[Header("Deadzone Settings")]
		public float m_DeadzoneLineFrequency = 1f;

		public float m_DeadZoneRotationOffset = 45f;

		public float m_DeadzoneLineWarpingScale = 1f;

		public float m_DeadzoneLineWarpingFreqeuncy = 1f;

		[Header("Gradient Falloff")]
		public float m_GradientFalloffDistance = 10f;

		[Range(0f, 1f)]
		public float m_GradientFalloffStrength = 0.1f;
	}
}
