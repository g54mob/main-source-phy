using Landfall.TABS;
using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public class NetworkSyncProjectileEffect : NetworkUnitSpecialAttack
	{
		private INetworkUnitsManager m_unitsManager;

		private SyncProjectileEffect m_syncProjectileEffect;

		protected override void SubscribeToUnitEvents(Unit unit)
		{
			m_unitsManager = ServiceLocator.GetService<INetworkUnitsManager>();
			m_syncProjectileEffect = unit.GetComponentInChildren<SyncProjectileEffect>();
			if (m_syncProjectileEffect != null)
			{
				m_syncProjectileEffect.AddedProjectileHitEffect += OnProjectileHitEffect;
			}
		}

		protected override void UnsubscribeFromUnitEvents(Unit unit)
		{
			if (m_syncProjectileEffect != null)
			{
				m_syncProjectileEffect.AddedProjectileHitEffect -= OnProjectileHitEffect;
			}
		}

		protected override void OnReceivedSpecialAttack(NetworkUnit networkUnit, UnitSpecialAttackEvent attackEvent)
		{
			if (!(m_syncProjectileEffect == null) && m_unitsManager != null && attackEvent.AttackType == 6)
			{
				SyncProjectileEffectToken syncProjectileEffectToken = (SyncProjectileEffectToken)attackEvent.AttackToken;
				GameObject target = m_unitsManager.GetUnitBySmallNetworkId(syncProjectileEffectToken.TargetSmallNetworkId).transform.root.gameObject;
				m_syncProjectileEffect.DoRemoteEffect(target);
			}
		}

		private void OnProjectileHitEffect(HitData hit)
		{
			SyncProjectileEffectToken attackToken = new SyncProjectileEffectToken(hit.transform.root.GetComponent<Unit>().SmallNetworkId);
			SendSpecialAttackEvent(NetworkUnitSpecialAttackType.SyncProjectileEffect, attackToken);
		}
	}
}
