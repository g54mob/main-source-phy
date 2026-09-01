using System;
using Landfall.TABS;
using Photon.Bolt;
using TFBGames;
using UnityEngine;

public class ProjectileHitAddEffect : ProjectileHitEffect, GameObjectPooling.IPoolable
{
	public UnitEffectBase EffectPrefab;

	public bool addToDead;

	[HideInInspector]
	public Unit unitThatSpawnedMe;

	public bool syncEffectsRemotely;

	private SyncProjectileEffect syncProjectileEffect;

	private Unit unit;

	private TeamHolder rootTeamHolder;

	private INetworkService m_networkService;

	private Projectile m_projectile;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		m_networkService = ServiceLocator.GetService<INetworkService>();
		m_projectile = GetComponentInParent<Projectile>();
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	public override bool DoEffect(HitData hit)
	{
		DataHandler componentInChildren = hit.transform.root.GetComponentInChildren<DataHandler>();
		bool shouldSkipDeadTests = false;
		if (!componentInChildren)
		{
			return false;
		}
		if (BoltNetwork.IsClient && syncEffectsRemotely)
		{
			return false;
		}
		if (componentInChildren != null && !NetworkProjectilesHelper.CanProcessHitUnitEffects(m_networkService, m_projectile, componentInChildren.unit, out shouldSkipDeadTests) && BoltNetwork.IsRunning)
		{
			return false;
		}
		if (componentInChildren != null && componentInChildren.Dead && !shouldSkipDeadTests)
		{
			return false;
		}
		if (componentInChildren != null && componentInChildren.Dead && !addToDead)
		{
			return false;
		}
		if (EffectPrefab == null)
		{
			return false;
		}
		UnitEffectBase unitEffectBase = UnitEffectBase.AddEffectToTarget(hit.transform.root.gameObject, EffectPrefab);
		if (unitEffectBase == null)
		{
			UnitEffectBase unitEffectBase2 = UnityEngine.Object.Instantiate(EffectPrefab, hit.transform.root);
			unitEffectBase2.transform.position = hit.transform.root.position;
			unitEffectBase2.transform.rotation = base.transform.rotation;
			unitEffectBase2.ShouldSkipDeadTests = shouldSkipDeadTests;
			unitEffectBase2.DoEffect();
			TeamHolder.AddTeamHolder(unitEffectBase2.gameObject, unit, rootTeamHolder);
		}
		else
		{
			unitEffectBase.ShouldSkipDeadTests = shouldSkipDeadTests;
			unitEffectBase.Ping();
		}
		if (syncEffectsRemotely && hit.transform.root.GetComponent<Unit>() != null && syncProjectileEffect != null)
		{
			syncProjectileEffect.SyncEffect(hit);
		}
		return false;
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
	}

	public void Release()
	{
	}

	private void InitializeOnSpawn()
	{
		TeamHolder.GetTeamRelevantComponents(base.transform.root, ref unit, ref rootTeamHolder);
		if (syncEffectsRemotely && unitThatSpawnedMe != null)
		{
			syncProjectileEffect = unitThatSpawnedMe.GetComponentInChildren<SyncProjectileEffect>();
		}
	}
}
