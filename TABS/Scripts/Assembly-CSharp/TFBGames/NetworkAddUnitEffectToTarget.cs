using Landfall.TABS;
using Photon.Bolt;

namespace TFBGames
{
	public class NetworkAddUnitEffectToTarget : NetworkUnitSpecialAttack
	{
		private INetworkUnitsManager m_unitsManager;

		private AddUnitEffectToTarget m_addUnitEffect;

		protected override void SubscribeToUnitEvents(Unit unit)
		{
			if (!(unit == null))
			{
				m_unitsManager = ServiceLocator.GetService<INetworkUnitsManager>();
				m_addUnitEffect = unit.GetComponentInChildren<AddUnitEffectToTarget>();
				if (m_addUnitEffect != null)
				{
					m_addUnitEffect.AddedEffect += OnAddedEffect;
				}
			}
		}

		protected override void UnsubscribeFromUnitEvents(Unit unit)
		{
			if (m_addUnitEffect != null)
			{
				m_addUnitEffect.AddedEffect -= OnAddedEffect;
			}
		}

		protected override void OnReceivedSpecialAttack(NetworkUnit networkUnit, UnitSpecialAttackEvent attackEvent)
		{
			if (!(m_addUnitEffect == null) && m_unitsManager != null && attackEvent.AttackType == 2)
			{
				AddUnitEffectToTargetAddEffectToken addUnitEffectToTargetAddEffectToken = (AddUnitEffectToTargetAddEffectToken)attackEvent.AttackToken;
				Unit unitBySmallNetworkId = m_unitsManager.GetUnitBySmallNetworkId(addUnitEffectToTargetAddEffectToken.TargetSmallNetworkId);
				DataHandler targetData = ((unitBySmallNetworkId != null) ? unitBySmallNetworkId.data : null);
				m_addUnitEffect.AddEffect(networkUnit.Unit, targetData);
			}
		}

		private void OnAddedEffect(Unit attacker, DataHandler targetData)
		{
			if (!(m_networkUnit == null) && !(m_networkUnit.Unit == null) && !m_networkUnit.Unit.IsRemotelyControlled)
			{
				Unit unit = ((targetData != null) ? targetData.transform.root.GetComponent<Unit>() : null);
				AddUnitEffectToTargetAddEffectToken attackToken = new AddUnitEffectToTargetAddEffectToken((ushort)((unit != null) ? unit.SmallNetworkId : 0));
				SendSpecialAttackEvent(NetworkUnitSpecialAttackType.AddUnitEffectToTargetAddEffect, attackToken);
			}
		}
	}
}
