using System;
using UnityEngine;
using UnityEngine.UI;

namespace Heathen.SteamworksIntegration
{
	public class SteamLobbySelectableEnabler : MonoBehaviour
	{
		public enum EnableWhenRule
		{
			IsSet = 0,
			IsNotSet = 1,
			AmITheOwner = 2,
			AmINotTheOwner = 3,
			AmIMember = 4,
			AmINotMember = 5,
			IsParty = 6,
			IsSession = 7,
			IsNotParty = 8,
			IsNotSession = 9
		}

		[Flags]
		public enum ConditionMode
		{
			None = 0,
			Or = 1,
			And = 2
		}

		public SteamLobbyData targetLobby;

		[Tooltip("The conditions that will enable this GameObject.")]
		public EnableWhenRule[] conditions;

		[Tooltip("Should conditions be combined with AND or OR logic?")]
		public ConditionMode mode;

		private Selectable[] _selectable;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleOnChanged(LobbyData arg0)
		{
		}

		private bool CheckCondition(EnableWhenRule condition)
		{
			return false;
		}
	}
}
