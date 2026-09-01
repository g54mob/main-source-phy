using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public class NetworkProjectilesManager : GlobalEventListener, IService
	{
		private List<Projectile> m_projectiles = new List<Projectile>();

		private INetworkService m_networkService;

		private ProjectilesSpawnManager m_spawnManager;

		private INetworkUnitsManager m_networkUnits;

		private ushort m_nextProjectileNetworkId;

		private void Awake()
		{
			ServiceLocator.RegisterService(this);
		}

		private void Start()
		{
			m_networkService = ServiceLocator.GetService<INetworkService>();
			m_spawnManager = ServiceLocator.GetService<ProjectilesSpawnManager>();
			m_networkUnits = ServiceLocator.GetService<INetworkUnitsManager>();
			m_spawnManager.SpawnedProjectile += OnSpawnedProjectile;
			m_spawnManager.DestroyedProjectile += OnDestroyedProjectile;
		}

		private void OnDestroy()
		{
			ServiceLocator.UnRegisterSerice<NetworkProjectilesManager>();
			if (m_spawnManager != null)
			{
				m_spawnManager.SpawnedProjectile -= OnSpawnedProjectile;
				m_spawnManager.DestroyedProjectile -= OnDestroyedProjectile;
			}
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}

		public override void OnEvent(SpawnProjectileEvent spawnEvent)
		{
			base.OnEvent(spawnEvent);
			ProjectileSpawnToken projectileSpawnToken = (ProjectileSpawnToken)spawnEvent.SpawnToken;
			Unit unit = ((m_networkUnits != null) ? m_networkUnits.GetUnitBySmallNetworkId(projectileSpawnToken.UnitSmallNetworkId) : null);
			Unit unit2 = ((m_networkUnits != null) ? m_networkUnits.GetUnitBySmallNetworkId(projectileSpawnToken.TargetSmallNetworkId) : null);
			Rigidbody targetRigidbody = ((unit2 != null && unit2.data != null) ? unit2.data.mainRig : null);
			Projectile projectile = m_spawnManager.SpawnProjectile(projectileSpawnToken.PrefabIndex, projectileSpawnToken.SpawnPosition, projectileSpawnToken.SpawnRotation, unit, projectileSpawnToken.WeaponIndex, projectileSpawnToken.SpawnDirection, projectileSpawnToken.DirectionToTarget, targetRigidbody, projectileSpawnToken.ShootPositionForward, projectileSpawnToken.RandomSeed);
			if (projectile == null)
			{
				Debug.LogErrorFormat("{0}: projectile is null", "SpawnProjectileEvent");
			}
			else
			{
				InitializeProjectile(projectile, isRemotelyControlled: true, unit, projectileSpawnToken.WeaponIndex, projectileSpawnToken.SpawnDirection, projectileSpawnToken.DirectionToTarget, unit2, projectileSpawnToken.ShootPositionForward, projectileSpawnToken);
			}
		}

		public override void OnEvent(ProjectileHitUnitEvent hitEvent)
		{
			base.OnEvent(hitEvent);
			Unit unit = ((m_networkUnits != null) ? m_networkUnits.GetUnitBySmallNetworkId((ushort)hitEvent.UnitSmallNetworkId) : null);
			if (unit != null)
			{
				unit.OnHitByProjectileRemotely((ushort)hitEvent.ProjectileNetworkId);
			}
		}

		private void OnSpawnedProjectile(Projectile projectile, Unit unit, byte weaponIndex, Vector3 spawnDirection, Vector3 directionToTarget, Rigidbody targetRigidbody, Vector3 shootPositionForward, bool isSpawnedFromPrefabId)
		{
			if (!m_projectiles.Contains(projectile))
			{
				SubscribeToProjectileEvents(projectile, subscribe: true);
				m_projectiles.Add(projectile);
			}
			if (isSpawnedFromPrefabId)
			{
				return;
			}
			if (projectile.ShouldSendHitUnitEvent)
			{
				m_nextProjectileNetworkId++;
				if (m_nextProjectileNetworkId == 0)
				{
					m_nextProjectileNetworkId = 1;
				}
				projectile.NetworkId = m_nextProjectileNetworkId;
			}
			SendSpawnEvent(projectile, unit, weaponIndex, spawnDirection, directionToTarget, targetRigidbody, shootPositionForward);
		}

		private void OnDestroyedProjectile(Projectile projectile)
		{
			int num = m_projectiles.IndexOf(projectile);
			if (num >= 0)
			{
				SubscribeToProjectileEvents(projectile, subscribe: false);
				m_projectiles.RemoveAt(num);
			}
		}

		private void SendSpawnEvent(Projectile projectile, Unit unit, byte weaponIndex, Vector3 spawnDirection, Vector3 directionToTarget, Rigidbody targetRigidbody, Vector3 shootPositionForward)
		{
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (m_networkService.IsRunning && m_networkService.IsConnected && !(currentGameMode.GetType() != typeof(OnlineMultiplayerGameMode)))
			{
				Transform transform = projectile.transform;
				Unit unit2 = ((targetRigidbody != null) ? targetRigidbody.GetComponentInParent<Unit>() : null);
				ProjectileSpawnToken spawnToken = new ProjectileSpawnToken(m_spawnManager.GetPrefabIndex(projectile.PrefabId), (ushort)((unit != null) ? unit.SmallNetworkId : 0), weaponIndex, transform.position, transform.rotation, spawnDirection, directionToTarget, (ushort)((unit2 != null) ? unit2.SmallNetworkId : 0), shootPositionForward, (targetRigidbody != null) ? targetRigidbody.position : Vector3.zero, (targetRigidbody != null) ? targetRigidbody.velocity : Vector3.zero, projectile.NetworkId, projectile.RandomSeed);
				InitializeProjectile(projectile, isRemotelyControlled: false, unit, weaponIndex, spawnDirection, directionToTarget, unit2, shootPositionForward, spawnToken);
				SpawnProjectileEvent spawnProjectileEvent = SpawnProjectileEvent.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered);
				spawnProjectileEvent.SpawnToken = spawnToken;
				spawnProjectileEvent.Send();
			}
		}

		private void InitializeProjectile(Projectile projectile, bool isRemotelyControlled, Unit unit, int weaponIndex, Vector3 spawnDirection, Vector3 directionToTarget, Unit targetUnit, Vector3 shootPositionForward, ProjectileSpawnToken spawnToken)
		{
			Rigidbody targetRig = ((targetUnit != null && targetUnit.data != null) ? targetUnit.data.mainRig : null);
			projectile.NetworkId = spawnToken.NetworkId;
			projectile.RandomSeed = spawnToken.RandomSeed;
			IRemotelyControllable[] componentsInChildren = projectile.GetComponentsInChildren<IRemotelyControllable>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				componentsInChildren[i].SetIsRemotelyControlled(isRemotelyControlled);
			}
			if (projectile.RandomSeed.HasValue)
			{
				InitializeMoveTransforms(projectile, projectile.RandomSeed);
			}
			RangeWeapon rangeWeapon = ((unit != null && unit.WeaponHandler != null) ? (unit.WeaponHandler.GetWeapon(weaponIndex) as RangeWeapon) : null);
			if (isRemotelyControlled && rangeWeapon != null)
			{
				Vector3 spawnPosition = spawnToken.SpawnPosition;
				rangeWeapon.SetTeamHolder(projectile.gameObject, targetRig);
				rangeWeapon.SetProjectileStats(projectile.gameObject, spawnDirection, directionToTarget, targetRig, shootPositionForward, spawnToken.TargetPosition, spawnToken.TargetVelocity, projectile.RandomSeed);
				rangeWeapon.SetTargetableEffects(projectile.gameObject, spawnPosition, targetRig);
			}
		}

		private void InitializeMoveTransforms(Projectile projectile, byte? randomSeed)
		{
			MoveTransform[] componentsInChildren = projectile.GetComponentsInChildren<MoveTransform>();
			if (componentsInChildren != null && componentsInChildren.Length != 0)
			{
				byte? b = randomSeed;
				int i = 0;
				for (int num = componentsInChildren.Length; i < num; i++)
				{
					componentsInChildren[i].RandomSeed = b;
					b++;
				}
			}
		}

		private void SubscribeToProjectileEvents(Projectile projectile, bool subscribe)
		{
			if (!m_networkService.IsServer || projectile == null)
			{
				return;
			}
			ProjectileHit componentInChildren = projectile.GetComponentInChildren<ProjectileHit>();
			if (!(componentInChildren == null))
			{
				if (subscribe)
				{
					componentInChildren.HitUnit += OnHitUnit;
				}
				else
				{
					componentInChildren.HitUnit -= OnHitUnit;
				}
			}
		}

		private void OnHitUnit(ProjectileHit projectileHit, Unit unit)
		{
			if (m_networkService.IsServer && !(unit == null))
			{
				Projectile projectile = ((projectileHit != null) ? projectileHit.GetComponentInParent<Projectile>() : null);
				if (!(projectile == null) && projectile.ShouldSendHitUnitEvent && projectile.NetworkId.HasValue)
				{
					ProjectileHitUnitEvent projectileHitUnitEvent = ProjectileHitUnitEvent.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered);
					projectileHitUnitEvent.ProjectileNetworkId = projectile.NetworkId.Value;
					projectileHitUnitEvent.UnitSmallNetworkId = unit.SmallNetworkId;
					projectileHitUnitEvent.Send();
				}
			}
		}
	}
}
