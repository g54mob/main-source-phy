using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/GameObject Enable when Lobby")]
	[HelpURL("https://kb.heathen.group/steam/features/lobby")]
	public class SteamLobbyGameObjectEnabler : MonoBehaviour
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
			IsNotSession = 9,
			IsNotFull = 10,
			IsFull = 11
		}

		public enum ConditionMode
		{
			Or = 1,
			And = 2
		}

		[Serializable]
		public struct Condition
		{
			[FormerlySerializedAs("Note")]
			public string note;

			[Tooltip("The rules that will be evaluated together.")]
			public EnableWhenRule[] rules;

			[Tooltip("How the rules are combined.")]
			public ConditionMode mode;
		}

		public SteamLobbyData targetLobby;

		[Tooltip("The conditions that must all be true for this GameObject to be enabled.")]
		public List<Condition> conditions;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleOnChanged(LobbyData arg0)
		{
		}

		private bool EvaluateCondition(Condition condition)
		{
			return false;
		}

		private bool CheckRule(EnableWhenRule rule)
		{
			return false;
		}
	}
}
