using UnityEngine;

namespace Landfall.TABS
{
	[CreateAssetMenu(fileName = "Standard Turning Data", menuName = "TABS/Turning Data", order = 1)]
	public class TurningData : ScriptableObject, IDatabaseEntity
	{
		[SerializeField]
		private DatabaseEntity m_entity;

		public float TurnSpeed = 18f;

		public AnimationCurve TurnSpeedCurve;

		public DatabaseEntity Entity => m_entity;
	}
}
