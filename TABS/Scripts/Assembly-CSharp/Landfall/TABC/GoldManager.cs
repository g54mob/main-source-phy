using System;
using UnityEngine;

namespace Landfall.TABC
{
	public class GoldManager : MonoBehaviour
	{
		public static GoldManager instance;

		private int goldSnapshot;

		public int winStreak = 1;

		public int[] winStreakGold;

		private int winStreakReset = 8;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			RoundHandler roundHandler = RoundHandler.instance;
			roundHandler.EnterBattleAction = (Action)Delegate.Combine(roundHandler.EnterBattleAction, new Action(BattleStart));
			BattleManager battleManager = BattleManager.instance;
			battleManager.BattleOverAction = (Action<bool>)Delegate.Combine(battleManager.BattleOverAction, new Action<bool>(BattleOver));
			RoundHandler roundHandler2 = RoundHandler.instance;
			roundHandler2.EnterBattleAction = (Action)Delegate.Combine(roundHandler2.EnterBattleAction, new Action(BattleStart));
		}

		public void BattleOver(bool won)
		{
			if (won)
			{
				winStreak++;
			}
			else
			{
				winStreak = 1;
			}
			GoldManagerUI.instance.ShowGold(5, won ? 1 : 0, GetInterest(goldSnapshot), GetWinStreak());
			if (winStreak == 8)
			{
				winStreak = 1;
			}
		}

		private int GetInterest(int totalGold)
		{
			return Mathf.Clamp(Mathf.FloorToInt((float)totalGold / 10f), 0, 5);
		}

		private int GetWinStreak()
		{
			return winStreakGold[winStreak];
		}

		public void GetMoney()
		{
		}

		public void BattleStart()
		{
			goldSnapshot = WalletHandlerClient.instance.money;
		}
	}
}
