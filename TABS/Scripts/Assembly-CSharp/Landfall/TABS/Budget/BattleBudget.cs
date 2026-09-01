using System;
using System.Collections.Generic;
using System.Linq;
using Landfall.TABS.Budget.Wallets;

namespace Landfall.TABS.Budget
{
	public class BattleBudget
	{
		public delegate void OnBudgetChangedDelegate(Team team, int newBudget);

		private Dictionary<Team, BaseWallet> m_teamBudgets = new Dictionary<Team, BaseWallet>();

		private int m_originalBudget;

		protected OnBudgetChangedDelegate OnBudgetChanged;

		public void RegisterOnBudgetChangedListener(OnBudgetChangedDelegate callback)
		{
			OnBudgetChanged = (OnBudgetChangedDelegate)Delegate.Combine(OnBudgetChanged, callback);
		}

		public void UnregisterOnBudgetChangedListener(OnBudgetChangedDelegate callback)
		{
			OnBudgetChanged = (OnBudgetChangedDelegate)Delegate.Remove(OnBudgetChanged, callback);
		}

		public void InitializeWithWalletType<T>() where T : BaseWallet
		{
			m_teamBudgets.Clear();
			Team[] array = Enum.GetValues(typeof(Team)).Cast<Team>().ToArray();
			foreach (Team team in array)
			{
				BaseWallet baseWallet = Activator.CreateInstance<T>();
				baseWallet.SetTeamOwner(team);
				AddWallet(team, baseWallet);
				OnBudgetChanged?.Invoke(team, GetBudget(team));
			}
		}

		public void SetBudget(int budget)
		{
			m_originalBudget = budget;
			Team[] array = Enum.GetValues(typeof(Team)).Cast<Team>().ToArray();
			foreach (Team team in array)
			{
				SetBudget(team, budget);
				OnBudgetChanged?.Invoke(team, m_teamBudgets[team].GetBudget());
			}
		}

		public void SetBudget(Team team, int budget)
		{
			m_teamBudgets[team].SetBudget(budget);
			OnBudgetChanged?.Invoke(team, m_teamBudgets[team].GetBudget());
		}

		public void ResetBudget(Team team)
		{
			m_teamBudgets[team].ResetBudget();
			OnBudgetChanged?.Invoke(team, m_teamBudgets[team].GetBudget());
		}

		public void ResetBudgetAll()
		{
			Team[] array = Enum.GetValues(typeof(Team)).Cast<Team>().ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				ResetBudget(array[i]);
			}
		}

		private void AddWallet(Team team, BaseWallet walletInstance)
		{
			m_teamBudgets.Add(team, walletInstance);
		}

		public bool CanAfford(Team team, int price)
		{
			return m_teamBudgets[team].CanAfford(price);
		}

		public int GetBudget(Team team)
		{
			return m_teamBudgets[team].GetBudget();
		}

		public int GetMaxBudget(Team team)
		{
			return m_teamBudgets[team].GetMaxBudget();
		}

		public void ReturnAmount(Team team, int amount)
		{
			GetBudget(team);
			m_teamBudgets[team].ReturnAmount(amount);
			int budget = GetBudget(team);
			OnBudgetChanged?.Invoke(team, budget);
		}

		public void SpendAmount(Team team, int amount)
		{
			GetBudget(team);
			m_teamBudgets[team].SpendAmount(amount);
			int budget = GetBudget(team);
			OnBudgetChanged?.Invoke(team, budget);
		}

		public bool IsInfinite(Team team)
		{
			return m_teamBudgets[team].IsInfinite();
		}
	}
}
