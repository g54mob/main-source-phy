namespace Landfall.TABS.Budget.Wallets
{
	public class LocalMultiplayerWallet : BaseWallet
	{
		private int budget;

		private int originalBudget;

		public override int GetBudget()
		{
			return budget;
		}

		public override int GetMaxBudget()
		{
			return originalBudget;
		}

		public override void SetBudget(int budget)
		{
			this.budget = budget;
			originalBudget = budget;
		}

		public override void ResetBudget()
		{
			budget = originalBudget;
		}

		public override bool CanAfford(int value)
		{
			return budget - value >= 0;
		}

		public override void SpendAmount(int value)
		{
			budget -= value;
		}

		public override void ReturnAmount(int value)
		{
			budget += value;
		}

		public override bool IsInfinite()
		{
			return true;
		}
	}
}
