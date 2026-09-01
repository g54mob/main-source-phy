using Landfall.TABS;

namespace TFBGames
{
	public static class NetworkProjectilesHelper
	{
		public static bool CanProcessHitUnitEffects(INetworkService networkService, Projectile projectile, Unit unit, out bool shouldSkipDeadTests)
		{
			shouldSkipDeadTests = false;
			if (networkService == null || projectile == null || unit == null || unit.data == null || networkService.IsServer || !projectile.ShouldEffectsWaitForRemoteHit)
			{
				return true;
			}
			if (unit.data.healthHandler != null && unit.data.healthHandler.DiedLocally)
			{
				return false;
			}
			if (unit.WasHitByProjectileRemotely(projectile))
			{
				shouldSkipDeadTests = unit.data.Dead;
				return true;
			}
			return false;
		}
	}
}
