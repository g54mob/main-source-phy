namespace TFBGames
{
	public struct UnitPoolInfo
	{
		public readonly int PoolIndex;

		public readonly short PoolId;

		public readonly bool HasNetworkError;

		public UnitPoolInfo(int poolIndex, short poolId, bool hasNetworkError = false)
		{
			PoolIndex = poolIndex;
			PoolId = poolId;
			HasNetworkError = hasNetworkError;
		}
	}
}
