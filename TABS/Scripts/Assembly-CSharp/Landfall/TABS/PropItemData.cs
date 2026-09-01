using System;
using UnityEngine;

namespace Landfall.TABS
{
	[Serializable]
	public class PropItemData
	{
		public Vector3 m_positionOffset = Vector3.zero;

		public Vector3 m_scale = Vector3.one;

		public UnitRig.EquipType m_equip;

		public int[] m_colors;

		public bool[] m_isTeamColor;

		public PropItemData()
		{
			m_colors = new int[0];
			m_isTeamColor = new bool[0];
		}

		public PropItemData(PropItemData data)
		{
			m_positionOffset = data.m_positionOffset;
			m_scale = data.m_scale;
			m_equip = data.m_equip;
			m_colors = (int[])data.m_colors.Clone();
			m_isTeamColor = (bool[])data.m_isTeamColor.Clone();
		}
	}
}
