using Landfall.TABS;
using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public class PossessedNetworkUnit : EntityEventListener<IPossessedUnitState>
	{
		public void OnEnterPossession(Unit unit)
		{
			SyncTransforms(unit, sync: true);
		}

		public void OnExitPossession(Unit unit)
		{
			SyncTransforms(unit, sync: false);
		}

		private void SyncTransforms(Unit unit, bool sync)
		{
			Transform simulate = ((sync && unit != null) ? unit.GetNetworkSyncedTransform() : null);
			base.state.SetTransforms(base.state.MainTransform, simulate);
		}
	}
}
