using System;
using UnityEngine;

namespace Landfall.TABC
{
	public class WalletHandlerClient : MonoBehaviour
	{
		public static WalletHandlerClient instance;

		public int money;

		public Action<bool> moneyWasUpdatedAction;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			UpdateMoney(0);
		}

		public void AddMoney(int moneyToAdd)
		{
			UpdateMoney(moneyToAdd);
		}

		public void RemoveMoney(int moneyToRemove)
		{
			UpdateMoney(-moneyToRemove);
		}

		public void UpdateMoney(int change)
		{
			money += change;
			if (moneyWasUpdatedAction != null)
			{
				moneyWasUpdatedAction(change >= 0);
			}
			GameFlowHandlerClient.instance.ClientToServerUpdateMoney(money);
		}

		public bool Spend(int moneyToSpend)
		{
			if (money >= moneyToSpend)
			{
				RemoveMoney(moneyToSpend);
				return true;
			}
			UIEffects.instance.CantAfford();
			return false;
		}
	}
}
