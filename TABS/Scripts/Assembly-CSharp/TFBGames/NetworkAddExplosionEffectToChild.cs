using Landfall.TABS;
using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public class NetworkAddExplosionEffectToChild : NetworkUnitSpecialAttack
	{
		private INetworkUnitsManager m_unitsManager;

		private AddExplosionEffectToChild m_effectOnParent;

		protected override void SubscribeToUnitEvents(Unit unit)
		{
			m_unitsManager = ServiceLocator.GetService<INetworkUnitsManager>();
			m_effectOnParent = unit.GetComponentInChildren<AddExplosionEffectToChild>();
			if (m_effectOnParent != null)
			{
				m_effectOnParent.AddedObjectEffectToTarget += OnAddedEffectToTarget;
			}
		}

		protected override void UnsubscribeFromUnitEvents(Unit unit)
		{
			if (m_effectOnParent != null)
			{
				m_effectOnParent.AddedObjectEffectToTarget -= OnAddedEffectToTarget;
			}
		}

		protected override void OnReceivedSpecialAttack(NetworkUnit networkUnit, UnitSpecialAttackEvent attackEvent)
		{
			if (!(m_effectOnParent == null) && m_unitsManager != null && attackEvent.AttackType == 4)
			{
				AddExplosionEffectToChildToken addExplosionEffectToChildToken = (AddExplosionEffectToChildToken)attackEvent.AttackToken;
				GameObject target = m_unitsManager.GetUnitBySmallNetworkId(addExplosionEffectToChildToken.TargetSmallNetworkId).gameObject;
				m_effectOnParent.DoRemoteEffect(target);
			}
		}

		private void OnAddedEffectToTarget(GameObject target)
		{
			AddExplosionEffectToChildToken attackToken = new AddExplosionEffectToChildToken(target.gameObject.transform.root.GetComponent<Unit>().SmallNetworkId);
			SendSpecialAttackEvent(NetworkUnitSpecialAttackType.AddExplosionEffectToChild, attackToken);
		}
	}
}
