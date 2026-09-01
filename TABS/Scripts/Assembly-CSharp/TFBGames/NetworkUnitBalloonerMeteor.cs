using Landfall.TABS;
using Photon.Bolt;

namespace TFBGames
{
	public class NetworkUnitBalloonerMeteor : NetworkUnitSpecialAttack
	{
		private BalloonBackPack balloonBackPack;

		protected override void SubscribeToUnitEvents(Unit unit)
		{
			if (!(unit == null))
			{
				balloonBackPack = unit.GetComponentInChildren<BalloonBackPack>();
				if (balloonBackPack != null)
				{
					balloonBackPack.networkMeteorEvent += OnNetworkMeteorEvent;
				}
			}
		}

		protected override void UnsubscribeFromUnitEvents(Unit unit)
		{
			if (balloonBackPack != null)
			{
				balloonBackPack.networkMeteorEvent -= OnNetworkMeteorEvent;
			}
		}

		protected override void OnReceivedSpecialAttack(NetworkUnit networkUnit, UnitSpecialAttackEvent attackEvent)
		{
			if (!(balloonBackPack == null) && attackEvent.AttackType == 3)
			{
				balloonBackPack.MeteorAttack();
			}
		}

		private void OnNetworkMeteorEvent()
		{
			BalloonerMeteorAttackToken attackToken = new BalloonerMeteorAttackToken();
			SendSpecialAttackEvent(NetworkUnitSpecialAttackType.BalloonerBackPackMeteor, attackToken);
		}
	}
}
