using System;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class ToolToggleEntry : ToolControlUIEntry
	{
		[Space]
		public bool m_initialState;

		public OnStateChanged m_onStateChanged;
	}
}
