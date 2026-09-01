using UnityEngine;

namespace Landfall.TABS
{
	[CreateAssetMenu(fileName = "New Faction Icon", menuName = "TABS/Faction Icon", order = 99999999)]
	public class FactionIcon : ScriptableObject, IDatabaseEntity
	{
		[SerializeField]
		private DatabaseEntity m_entity;

		public DatabaseEntity Entity
		{
			get
			{
				return m_entity;
			}
			set
			{
				m_entity = value;
			}
		}
	}
}
