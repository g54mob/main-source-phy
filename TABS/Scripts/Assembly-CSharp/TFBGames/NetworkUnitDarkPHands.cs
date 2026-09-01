using Landfall.TABS;
using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public class NetworkUnitDarkPHands : NetworkUnitSpecialAttack
	{
		private INetworkUnitsManager m_unitsManager;

		private DarkPHands m_darkPHands;

		protected override void SubscribeToUnitEvents(Unit unit)
		{
			if (!(unit == null))
			{
				m_unitsManager = ServiceLocator.GetService<INetworkUnitsManager>();
				m_darkPHands = unit.GetComponentInChildren<DarkPHands>();
				if (m_darkPHands != null)
				{
					m_darkPHands.Targeted += OnTargeted;
				}
			}
		}

		protected override void UnsubscribeFromUnitEvents(Unit unit)
		{
			if (m_darkPHands != null)
			{
				m_darkPHands.Targeted -= OnTargeted;
			}
		}

		protected override void OnReceivedSpecialAttack(NetworkUnit networkUnit, UnitSpecialAttackEvent attackEvent)
		{
			if (!(m_darkPHands == null) && m_unitsManager != null && attackEvent.AttackType == 1)
			{
				DarkPHandsTargetToken darkPHandsTargetToken = (DarkPHandsTargetToken)attackEvent.AttackToken;
				Unit unitBySmallNetworkId = m_unitsManager.GetUnitBySmallNetworkId(darkPHandsTargetToken.PrimeTargetSmallNetworkId);
				Unit unitBySmallNetworkId2 = m_unitsManager.GetUnitBySmallNetworkId(darkPHandsTargetToken.TargetSmallNetworkId);
				m_darkPHands.Target(unitBySmallNetworkId, unitBySmallNetworkId2, darkPHandsTargetToken.PositionOrDirection);
			}
		}

		private void OnTargeted(Unit primeTarget, Unit target, Vector3 positionOrDirection)
		{
			if (!(m_networkUnit == null) && !(m_networkUnit.Unit == null) && !m_networkUnit.Unit.IsRemotelyControlled)
			{
				int primeTargetSmallNetworkId = ((primeTarget != null) ? primeTarget.SmallNetworkId : 0);
				ushort targetSmallNetworkId = (ushort)((target != null) ? target.SmallNetworkId : 0);
				DarkPHandsTargetToken attackToken = new DarkPHandsTargetToken((ushort)primeTargetSmallNetworkId, targetSmallNetworkId, positionOrDirection);
				SendSpecialAttackEvent(NetworkUnitSpecialAttackType.DarkPHandsTarget, attackToken);
			}
		}
	}
}
