namespace Landfall.TABS.Budget.Wallets
{
	public class CampaignWallet : BaseWallet
	{
		private bool m_InfBudget;

		private int m_maxBudget;

		private int m_budget;

		public override void ResetBudget()
		{
			m_budget = m_maxBudget;
		}

		public override bool CanAfford(int value)
		{
			if (base.OwningTeam == Team.Blue)
			{
				return true;
			}
			if (m_InfBudget)
			{
				return true;
			}
			if (value <= m_budget)
			{
				return true;
			}
			return false;
		}

		public override void ReturnAmount(int value)
		{
			if (m_InfBudget)
			{
				m_budget -= value;
			}
			else
			{
				m_budget += value;
			}
		}

		public override int GetBudget()
		{
			return m_budget;
		}

		public override int GetMaxBudget()
		{
			return m_maxBudget;
		}

		public override void SetBudget(int budget)
		{
			m_maxBudget = budget;
			m_budget = budget;
			if (budget == 0)
			{
				m_InfBudget = true;
			}
			else
			{
				m_InfBudget = false;
			}
		}

		public override void SpendAmount(int value)
		{
			if (m_InfBudget)
			{
				m_budget += value;
			}
			else
			{
				m_budget -= value;
			}
		}

		public override bool IsInfinite()
		{
			return m_InfBudget;
		}
	}
}
