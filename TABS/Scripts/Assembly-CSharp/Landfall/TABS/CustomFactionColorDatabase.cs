using System;
using UnityEngine;

namespace Landfall.TABS
{
	[CreateAssetMenu(fileName = "CustomFactionColorDatabase", menuName = "Landfall/TABS/CustomFactionColorDatabase", order = 99999)]
	public class CustomFactionColorDatabase : ScriptableObject
	{
		[Serializable]
		public class CustomFactionColor
		{
			public Color m_Color;

			public DatabaseID m_DatabaseID;

			public CustomFactionColor()
			{
				m_DatabaseID = DatabaseID.NewID();
				m_Color = Color.white;
			}
		}

		[SerializeField]
		private CustomFactionColor[] m_CustomFactionColors;

		public CustomFactionColor[] CustomFacionColors => m_CustomFactionColors;

		public CustomFactionColor GetFactionColor(DatabaseID id)
		{
			for (int i = 0; i < CustomFacionColors.Length; i++)
			{
				if (CustomFacionColors[i].m_DatabaseID == id)
				{
					return CustomFacionColors[i];
				}
			}
			return null;
		}
	}
}
