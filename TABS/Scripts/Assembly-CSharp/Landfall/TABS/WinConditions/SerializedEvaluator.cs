using System;
using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABS.WinConditions
{
	[Serializable]
	public class SerializedEvaluator
	{
		[SerializeField]
		private string m_team;

		[SerializeField]
		private List<SerializedWinCondition> m_winConditions;

		public SerializedEvaluator(WinConditionEvaluator evaluator)
		{
			m_winConditions = new List<SerializedWinCondition>();
			m_team = evaluator.OwningTeam.ToString();
			SerializedWinCondition[] serializedWinConditions = evaluator.GetSerializedWinConditions();
			m_winConditions.AddRange(serializedWinConditions);
		}

		public WinConditionEvaluator ToEvaluator()
		{
			WinConditionEvaluator winConditionEvaluator = new WinConditionEvaluator((Team)Enum.Parse(typeof(Team), m_team));
			foreach (SerializedWinCondition winCondition in m_winConditions)
			{
				winConditionEvaluator.AddWinCondition(winCondition.ToWinCondition());
			}
			return winConditionEvaluator;
		}
	}
}
