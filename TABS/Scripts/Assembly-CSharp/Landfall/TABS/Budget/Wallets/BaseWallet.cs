namespace Landfall.TABS.Budget.Wallets
{
	public abstract class BaseWallet
	{
		private Team m_team;

		protected Team OwningTeam => m_team;

		public void SetTeamOwner(Team team)
		{
			m_team = team;
		}

		public abstract int GetBudget();

		public abstract int GetMaxBudget();

		public abstract void SetBudget(int budget);

		public abstract void ResetBudget();

		public abstract bool CanAfford(int value);

		public abstract void SpendAmount(int value);

		public abstract void ReturnAmount(int value);

		public abstract bool IsInfinite();
	}
}
