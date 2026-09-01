using System;
using UnityEngine;

namespace Landfall.TABC
{
	public class RoundHandler : MonoBehaviour
	{
		public enum RoundState
		{
			Planning = 0,
			Battle = 1,
			WaitingForOtherBattles = 2,
			PostRound = 3,
			PickingChallange = 4
		}

		public int currentRound = 1;

		public SpecialRound[] specialRounds;

		public static RoundHandler instance;

		public RoundState roundState;

		public Action EnterBattleAction;

		public Action EnterBattleActionLate;

		public Action EnterPlanningAction;

		public Action EnterPostRoundAction;

		public Action EnterPostRoundActionLate;

		public Action<RoundState> NewStateAction;

		public bool CanPlaceUnits()
		{
			if (roundState != RoundState.Planning && roundState != RoundState.PostRound)
			{
				return roundState == RoundState.PickingChallange;
			}
			return true;
		}

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
		}

		public int IsItChallangeTime()
		{
			int result = -1;
			for (int i = 0; i < specialRounds.Length; i++)
			{
				if (specialRounds[i].roundNumber == currentRound)
				{
					result = i;
				}
			}
			return result;
		}

		public void IncrementRound()
		{
			currentRound++;
		}

		internal void SetRoundState(RoundState newRoundState)
		{
			if (newRoundState != roundState)
			{
				NewStateAction?.Invoke(newRoundState);
			}
			roundState = newRoundState;
			if (roundState == RoundState.Battle)
			{
				EnterBattleAction?.Invoke();
				EnterBattleActionLate?.Invoke();
			}
			if (roundState == RoundState.PostRound)
			{
				EnterPostRoundAction?.Invoke();
				EnterPostRoundActionLate?.Invoke();
			}
			if (roundState == RoundState.Planning)
			{
				EnterPlanningAction?.Invoke();
			}
		}
	}
}
