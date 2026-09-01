using Landfall.TABS;
using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public abstract class NetworkUnitSpecialAttack : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The attached NetworkUnit.")]
		protected NetworkUnit m_networkUnit;

		protected virtual void Awake()
		{
			m_networkUnit.InitializedUnit += OnInitializedUnit;
			m_networkUnit.NetworkUnitDetached += OnNetworkUnitDetached;
			m_networkUnit.ReceivedSpecialAttack += OnReceivedSpecialAttack;
		}

		protected virtual void OnDestroy()
		{
			if (m_networkUnit != null)
			{
				m_networkUnit.InitializedUnit -= OnInitializedUnit;
				m_networkUnit.NetworkUnitDetached -= OnNetworkUnitDetached;
				m_networkUnit.ReceivedSpecialAttack -= OnReceivedSpecialAttack;
			}
		}

		protected virtual void OnInitializedUnit(NetworkUnit networkUnit)
		{
			if (networkUnit != null)
			{
				SubscribeToUnitEvents(networkUnit.Unit);
			}
		}

		protected virtual void OnNetworkUnitDetached(NetworkUnit networkUnit)
		{
			if (networkUnit != null)
			{
				UnsubscribeFromUnitEvents(networkUnit.Unit);
			}
		}

		protected void SendSpecialAttackEvent(NetworkUnitSpecialAttackType attackType, IProtocolToken attackToken)
		{
			if (m_networkUnit != null)
			{
				m_networkUnit.SendSpecialAttackEvent(attackType, attackToken);
			}
		}

		protected abstract void SubscribeToUnitEvents(Unit unit);

		protected abstract void UnsubscribeFromUnitEvents(Unit unit);

		protected abstract void OnReceivedSpecialAttack(NetworkUnit networkUnit, UnitSpecialAttackEvent attackEvent);
	}
}
