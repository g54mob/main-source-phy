using Landfall.TABS;
using Photon.Bolt;
using TFBGames.Units;

namespace TFBGames
{
	public class NetworkUnitPikeDropSync : NetworkUnitSpecialAttack
	{
		private PikeDropSync pikeDropSync;

		protected override void SubscribeToUnitEvents(Unit unit)
		{
			if (!(unit == null))
			{
				pikeDropSync = unit.GetComponentInChildren<PikeDropSync>();
				if (pikeDropSync != null)
				{
					pikeDropSync.dropEvent += OnDropEvent;
				}
			}
		}

		protected override void UnsubscribeFromUnitEvents(Unit unit)
		{
			if (pikeDropSync != null)
			{
				pikeDropSync.dropEvent -= OnDropEvent;
			}
		}

		protected override void OnReceivedSpecialAttack(NetworkUnit networkUnit, UnitSpecialAttackEvent attackEvent)
		{
			if (!(pikeDropSync == null) && attackEvent.AttackType == 5)
			{
				pikeDropSync.Drop();
			}
		}

		private void OnDropEvent()
		{
			PikeDropAttackToken attackToken = new PikeDropAttackToken();
			SendSpecialAttackEvent(NetworkUnitSpecialAttackType.PikeDrop, attackToken);
		}
	}
}
