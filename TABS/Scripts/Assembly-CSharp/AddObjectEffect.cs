using System;
using Landfall.TABS;
using TFBGames;
using UnityEngine;

public class AddObjectEffect : ExplosionEffect, GameObjectPooling.IPoolable
{
	public UnitEffectBase EffectPrefab;

	public bool OnlyOnce;

	public bool forceSkipDeadTests;

	private DataHandler targetData;

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

	public override void DoEffect(GameObject target)
	{
		targetData = target.transform.root.GetComponentInChildren<DataHandler>();
		bool shouldSkipDeadTests = forceSkipDeadTests;
		if ((forceSkipDeadTests || !(targetData != null) || NetworkProjectilesHelper.CanProcessHitUnitEffects(m_networkService, m_projectile, targetData.unit, out shouldSkipDeadTests)) && (!targetData || !targetData.Dead || shouldSkipDeadTests))
		{
			UnitEffectBase unitEffectBase = UnitEffectBase.AddEffectToTarget(target, EffectPrefab);
			if (unitEffectBase == null)
			{
				UnitEffectBase unitEffectBase2 = UnityEngine.Object.Instantiate(EffectPrefab, target.transform.root);
				unitEffectBase2.transform.SetPositionAndRotation(target.transform.root.position, Quaternion.LookRotation(target.transform.root.position - base.transform.position));
				TeamHolder.AddTeamHolder(unitEffectBase2.gameObject, unit, rootTeamHolder);
				unitEffectBase2.ShouldSkipDeadTests = shouldSkipDeadTests;
				unitEffectBase2.DoEffect();
			}
			else if (!OnlyOnce)
			{
				unitEffectBase.ShouldSkipDeadTests = shouldSkipDeadTests;
				unitEffectBase.Ping();
			}
		}
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
	}
}
