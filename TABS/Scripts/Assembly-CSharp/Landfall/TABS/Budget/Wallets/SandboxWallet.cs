namespace Landfall.TABS.Budget.Wallets
{
	public class SandboxWallet : BaseWallet
	{
		private int m_budget;

		public SandboxWallet()
		{
			m_budget = 0;
		}

		public override int GetBudget()
		{
			return m_budget;
		}

		public override int GetMaxBudget()
		{
			return 0;
		}

		public override void SetBudget(int budget)
		{
			m_budget = budget;
		}

		public override void ResetBudget()
		{
			m_budget = 0;
		}

		public override bool CanAfford(int value)
		{
			return true;
		}

		public override void ReturnAmount(int value)
		{
			m_budget -= value;
		}

		public override void SpendAmount(int value)
		{
			m_budget += value;
		}

		public override bool IsInfinite()
		{
			return true;
		}
	}
}
