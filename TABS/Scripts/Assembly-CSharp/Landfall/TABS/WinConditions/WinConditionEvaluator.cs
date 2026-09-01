using System;
using System.Collections.Generic;

namespace Landfall.TABS.WinConditions
{
	public class WinConditionEvaluator
	{
		private List<WinCondition> m_winConditions = new List<WinCondition>();

		private Team m_owningTeam;

		public Team OwningTeam => m_owningTeam;

		public WinConditionEvaluator(Team owningTeam)
		{
			m_owningTeam = owningTeam;
		}

		public void AddWinCondition(WinCondition condition)
		{
			if (condition != null)
			{
				condition.OwningTeam = m_owningTeam;
				m_winConditions.Add(condition);
			}
		}

		public void RemoveWinCondition(WinCondition condition)
		{
			m_winConditions.Remove(condition);
		}

		public void RemoveWinCondition(Guid guid)
		{
			for (int i = 0; i < m_winConditions.Count; i++)
			{
				WinCondition winCondition = m_winConditions[i];
				if (winCondition.Guid.Equals(guid))
				{
					m_winConditions.Remove(winCondition);
				}
			}
		}

		public WinCondition GetWinCondition(Guid guid)
		{
			for (int i = 0; i < m_winConditions.Count; i++)
			{
				WinCondition winCondition = m_winConditions[i];
				if (winCondition.Guid.Equals(guid))
				{
					return winCondition;
				}
			}
			return null;
		}

		public WinCondition[] GetWinConditions()
		{
			return m_winConditions.ToArray();
		}

		public void ClearWinConditions()
		{
			m_winConditions.Clear();
		}

		public SerializedWinCondition[] GetSerializedWinConditions()
		{
			List<SerializedWinCondition> list = new List<SerializedWinCondition>();
			for (int i = 0; i < m_winConditions.Count; i++)
			{
				list.Add(new SerializedWinCondition(m_winConditions[i]));
			}
			return list.ToArray();
		}

		public void OnUnitDied(Unit unit)
		{
			foreach (WinCondition winCondition in m_winConditions)
			{
				winCondition.OnUnitDied(unit);
			}
		}

		public bool IsUnitMustKill(Unit unit, out WinCondition mustKillUnitWinCondition)
		{
			mustKillUnitWinCondition = null;
			if (unit == null || unit.RuntimeReference == null)
			{
				return false;
			}
			foreach (WinCondition winCondition in m_winConditions)
			{
				if (winCondition is MustKillUnitWinCondition mustKillUnitWinCondition2 && mustKillUnitWinCondition2.UnitToKill != null && mustKillUnitWinCondition2.UnitToKill.Guid == unit.RuntimeReference.Guid)
				{
					mustKillUnitWinCondition = mustKillUnitWinCondition2;
					return true;
				}
			}
			return false;
		}

		public void OnUpdate()
		{
			foreach (WinCondition winCondition in m_winConditions)
			{
				winCondition.OnUpdate();
			}
		}

		public void OnResetConditions()
		{
			foreach (WinCondition winCondition in m_winConditions)
			{
				winCondition.Reset();
			}
		}

		public void OnMatchOver()
		{
			foreach (WinCondition winCondition in m_winConditions)
			{
				winCondition.OnMatchOver();
			}
		}
	}
}
