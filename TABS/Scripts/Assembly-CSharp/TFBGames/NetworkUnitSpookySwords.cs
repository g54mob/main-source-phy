using Landfall.TABS;
using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public class NetworkUnitSpookySwords : NetworkUnitSpecialAttack
	{
		private INetworkUnitsManager m_unitsManager;

		private SpookySwords m_spookySwords;

		protected override void SubscribeToUnitEvents(Unit unit)
		{
			if (!(unit == null))
			{
				m_unitsManager = ServiceLocator.GetService<INetworkUnitsManager>();
				m_spookySwords = unit.GetComponentInChildren<SpookySwords>();
				if (m_spookySwords != null)
				{
					m_spookySwords.Attacked += OnAttacked;
				}
			}
		}

		protected override void UnsubscribeFromUnitEvents(Unit unit)
		{
			if (m_spookySwords != null)
			{
				m_spookySwords.Attacked -= OnAttacked;
			}
		}

		protected override void OnReceivedSpecialAttack(NetworkUnit networkUnit, UnitSpecialAttackEvent attackEvent)
		{
			if (!(m_spookySwords == null) && m_unitsManager != null && attackEvent.AttackType == 0)
			{
				SpookySwordsAttackToken spookySwordsAttackToken = (SpookySwordsAttackToken)attackEvent.AttackToken;
				Unit unitBySmallNetworkId = m_unitsManager.GetUnitBySmallNetworkId(spookySwordsAttackToken.TargetSmallNetworkId);
				Rigidbody target = ((unitBySmallNetworkId != null && unitBySmallNetworkId.data != null) ? unitBySmallNetworkId.data.mainRig : null);
				m_spookySwords.Attack(target, spookySwordsAttackToken.AttackId);
			}
		}

		private void OnAttacked(Rigidbody target, int useAttackID)
		{
			if (!(m_networkUnit == null) && !(m_networkUnit.Unit == null) && !m_networkUnit.Unit.IsRemotelyControlled)
			{
				Unit unit = ((target != null) ? target.transform.root.GetComponent<Unit>() : null);
				SpookySwordsAttackToken attackToken = new SpookySwordsAttackToken((ushort)((unit != null) ? unit.SmallNetworkId : 0), useAttackID);
				SendSpecialAttackEvent(NetworkUnitSpecialAttackType.SpookySwordsAttack, attackToken);
			}
		}
	}
}
