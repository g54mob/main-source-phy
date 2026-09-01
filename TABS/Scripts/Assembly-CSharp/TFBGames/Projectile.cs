using System;
using UnityEngine;

namespace TFBGames
{
	public class Projectile : MonoBehaviour, IRemotelyControllable
	{
		[SerializeField]
		[HideInInspector]
		protected int m_prefabId;

		[SerializeField]
		[HideInInspector]
		protected bool m_shouldEffectsWaitForMarsHit;

		private PooledProjectile m_pooledProjectile;

		private bool m_isAlive;

		public int PrefabId => m_prefabId;

		public bool IsRemotelyControlled { get; private set; }

		public ushort? NetworkId { get; set; }

		public bool ShouldEffectsWaitForRemoteHit => m_shouldEffectsWaitForMarsHit;

		public bool ShouldSendHitUnitEvent => ShouldEffectsWaitForRemoteHit;

		public byte? RandomSeed { get; set; }

		public event Action<Projectile> DestroyedOrReturnedToPool;

		private void Awake()
		{
			m_pooledProjectile = GetComponentInChildren<PooledProjectile>();
			if (m_pooledProjectile != null)
			{
				m_pooledProjectile.SpawnedFromPool += OnSpawnedFromPool;
				m_pooledProjectile.ReturnedToPool += OnReturnedToPool;
			}
		}

		private void OnEnable()
		{
			m_isAlive = true;
		}

		private void OnDestroy()
		{
			if (m_pooledProjectile != null)
			{
				m_pooledProjectile.SpawnedFromPool -= OnSpawnedFromPool;
				m_pooledProjectile.ReturnedToPool -= OnReturnedToPool;
			}
			if (m_isAlive)
			{
				m_isAlive = false;
				this.DestroyedOrReturnedToPool?.Invoke(this);
			}
		}

		private void OnSpawnedFromPool(PooledProjectile pooledProjectile)
		{
			m_isAlive = true;
		}

		private void OnReturnedToPool(PooledProjectile pooledProjectile)
		{
			if (m_isAlive)
			{
				m_isAlive = false;
				this.DestroyedOrReturnedToPool?.Invoke(this);
			}
		}

		public void SetIsRemotelyControlled(bool isRemotelyControlled)
		{
			IsRemotelyControlled = isRemotelyControlled;
		}
	}
}
