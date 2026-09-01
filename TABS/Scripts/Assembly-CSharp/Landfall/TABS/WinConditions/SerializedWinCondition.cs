using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Landfall.TABS.WinConditions
{
	[Serializable]
	public class SerializedWinCondition
	{
		[SerializeField]
		private string m_winConditionTypeString;

		private Type m_winConditionType;

		[SerializeField]
		private string m_jsonData;

		public Type WinConditionType => m_winConditionType;

		public string JsonData => m_jsonData;

		public SerializedWinCondition(WinCondition condition)
		{
			m_winConditionType = condition.GetType();
			m_winConditionTypeString = m_winConditionType.FullName;
			m_jsonData = JsonConvert.SerializeObject(condition, Formatting.Indented);
		}

		public WinCondition ToWinCondition()
		{
			m_winConditionType = Type.GetType(m_winConditionTypeString);
			WinCondition obj = (WinCondition)JsonConvert.DeserializeObject(m_jsonData, m_winConditionType);
			obj.Guid = Guid.NewGuid();
			return obj;
		}
	}
}
