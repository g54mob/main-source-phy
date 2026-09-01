using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.AI;
using Landfall.TABS.RuntimeCleanup;
using TFBGames;
using UnityEngine;

public class MeleeWeaponCopySelf : CollisionWeaponEffect, IRemotelyControllable
{
	public float triggerChance = 0.5f;

	public float copyTriggerChance;

	public float copyDuration = 3f;

	public bool isRanged;

	public bool useParentTransform;

	public GameObject poof;

	private float counter = 1f;

	private RuntimeGarbageCollector m_gc;

	private RiderHolder originalUnitRiderholder;

	private bool isACopy;

	private CopySelfLimitedPool copySelfLimitedPool;

	private INetworkService networkService;

	private INetworkUnitsManager networkUnits;

	public CopySelfLimitedPool CopySelfLimitedPool => copySelfLimitedPool;

	public bool IsRemotelyControlled { get; private set; }

	private void Start()
	{
		m_gc = ServiceLocator.GetService<RuntimeGarbageCollector>();
		originalUnitRiderholder = base.transform.root.GetComponent<RiderHolder>();
		if (!isACopy)
		{
			copySelfLimitedPool = GetComponent<CopySelfLimitedPool>();
			Unit component = base.transform.root.GetComponent<Unit>();
			if (copySelfLimitedPool != null && component != null)
			{
				copySelfLimitedPool.CreatePool(component);
			}
		}
		GetNetworkServices();
	}

	private void Update()
	{
		counter -= Time.deltaTime;
	}

	public void RegisterUnitWithPool(CopySelfLimitedPool pool)
	{
		isACopy = true;
		if (pool == null)
		{
			Debug.LogError("Tried to assign unit to a null pool. This is not allowed", this);
		}
		copySelfLimitedPool = pool;
	}

	public override void DoEffect(Transform hitTransform, Collision collision)
	{
		if (!IsAllowedToDoEffectInMultiplayer() || !base.enabled || counter > 0f || Random.value > triggerChance)
		{
			return;
		}
		UnitAPI component = base.transform.root.GetComponent<UnitAPI>();
		if ((bool)component)
		{
			component.forceSupressFromWinCondition = true;
		}
		counter = 0.3f;
		Transform root = base.transform.root;
		Vector3 vector = ((collision != null) ? (collision.GetContact(0).point + root.GetComponentInChildren<DirectionObject>().transform.forward) : ((!useParentTransform) ? hitTransform.position : component.GetComponentInChildren<DataHandler>().mainRig.position));
		Unit component2 = root.GetComponent<Unit>();
		GameObject gameObject = null;
		bool spawnedFromPool = false;
		if (copySelfLimitedPool != null)
		{
			gameObject = copySelfLimitedPool.GetNextUnitFromPool(root.position, root.rotation);
			spawnedFromPool = true;
		}
		else
		{
			gameObject = component2.unitBlueprint.Spawn(root.position, root.rotation, component2.Team)[0];
		}
		if (gameObject == null)
		{
			return;
		}
		Unit component3 = gameObject.GetComponent<Unit>();
		if (component3 != null)
		{
			UnitSpawnSource spawnSource = UnitSpawnSource.MeleeWeaponCopySelf;
			component3.api.SetIsOutOfPool();
			component3.SetSpawnSource(spawnSource);
			ushort copyOfSmallNetworkId = (component3.CopyOfSmallNetworkId = component2.SmallNetworkId);
			component3.CopyOfUnitSpawnPosition = vector;
			GetNetworkServices();
			if (networkService.IsServer && networkService.GetConnectionsCount() > 0)
			{
				networkUnits.ServerSendSpawnUnitFromPoolEvent(component3, spawnSource, vector, copyOfSmallNetworkId);
			}
		}
		OnSpawnedUnit(component2, gameObject, vector, spawnedFromPool);
		m_gc.AddGameObject(gameObject);
	}

	public void OnSpawnedUnit(Unit currentUnit, GameObject newObj, Vector3 spawnPos, bool spawnedFromPool = false)
	{
		Transform transform = currentUnit.transform;
		if (!IsRemotelyControlled || spawnedFromPool)
		{
			newObj.transform.position += spawnPos - transform.GetComponentInChildren<DataHandler>().mainRig.position;
		}
		Dictionary<string, UnitRig.BoneInfo> rigInfo = transform.GetComponent<UnitRig>().RigInfo;
		newObj.GetComponent<UnitRig>().RigInfo = rigInfo;
		newObj.GetComponentInChildren<GeneralInput>().hasControl = false;
		PlacementSpawnEffects componentInChildren = newObj.GetComponentInChildren<PlacementSpawnEffects>();
		if ((bool)componentInChildren)
		{
			Object.Destroy(componentInChildren.gameObject);
		}
		RiderHolder component = newObj.GetComponent<RiderHolder>();
		if ((bool)component)
		{
			for (int i = 0; i < component.riders.Count; i++)
			{
				component.riders[i].GetComponent<UnitRig>().RigInfo = originalUnitRiderholder.riders[i].GetComponent<UnitRig>().RigInfo;
				component.riders[i].GetComponentInChildren<GeneralInput>().hasControl = false;
				PlacementSpawnEffects placementSpawnEffects = null;
				placementSpawnEffects = component.riders[i].GetComponentInChildren<PlacementSpawnEffects>();
				if ((bool)placementSpawnEffects)
				{
					Object.Destroy(placementSpawnEffects.gameObject);
				}
			}
		}
		WeaponHandler weaponHandler = currentUnit.WeaponHandler;
		WeaponHandler weaponHandler2 = newObj.GetComponent<Unit>().WeaponHandler;
		if ((bool)weaponHandler.rightWeapon && (bool)weaponHandler2.rightWeapon)
		{
			Transform obj = weaponHandler2.rightWeapon.transform;
			Transform transform2 = weaponHandler.rightWeapon.transform;
			obj.localPosition = transform2.localPosition;
			obj.localRotation = transform2.localRotation;
			obj.localScale = transform2.localScale;
		}
		if ((bool)weaponHandler.leftWeapon && (bool)weaponHandler2.leftWeapon)
		{
			Transform obj2 = weaponHandler2.leftWeapon.transform;
			Transform transform3 = weaponHandler.leftWeapon.transform;
			obj2.localPosition = transform3.localPosition;
			obj2.localRotation = transform3.localRotation;
			obj2.localScale = transform3.localScale;
		}
		PossesionCamera componentInChildren2 = newObj.GetComponentInChildren<PossesionCamera>();
		if ((bool)componentInChildren2)
		{
			Object.Destroy(componentInChildren2);
		}
		if (newObj.GetComponent<KillAfterSeconds>() == null)
		{
			KillAfterSeconds killAfterSeconds = newObj.AddComponent<KillAfterSeconds>();
			killAfterSeconds.seconds = copyDuration;
			killAfterSeconds.objectToSpawn = poof;
			killAfterSeconds.spawnObjectOnMainRig = true;
			killAfterSeconds.destroyRoot = true;
		}
		MeleeWeaponCopySelf componentInChildren3 = newObj.GetComponentInChildren<MeleeWeaponCopySelf>();
		if ((bool)componentInChildren3)
		{
			if (copyTriggerChance == 0f)
			{
				componentInChildren3.enabled = false;
			}
			else
			{
				componentInChildren3.counter = 0.5f;
				componentInChildren3.triggerChance = copyTriggerChance;
			}
		}
		Object.Instantiate(poof, spawnPos, Quaternion.identity);
	}

	public void RangedDoEffect(Transform hitTransform)
	{
		if (isRanged)
		{
			DoEffect(hitTransform, null);
		}
	}

	private void SetUnitPose(Unit currentUnit, GameObject newObj)
	{
		Transform[] boneTransforms = currentUnit.transform.GetComponent<UnitRig>().boneTransforms;
		Transform[] boneTransforms2 = newObj.GetComponent<UnitRig>().boneTransforms;
		for (int i = 0; i < boneTransforms.Length; i++)
		{
			Transform parent = boneTransforms[i].parent;
			Transform parent2 = boneTransforms2[i].parent;
			if (parent != null && parent2 != null)
			{
				parent2.localPosition = parent.localPosition;
				parent2.localRotation = parent.localRotation;
				parent2.localScale = parent.localScale;
			}
		}
	}

	private void GetNetworkServices()
	{
		if (networkService == null)
		{
			networkService = ServiceLocator.GetService<INetworkService>();
		}
		if (networkUnits == null)
		{
			networkUnits = ServiceLocator.GetService<INetworkUnitsManager>();
		}
	}

	public void SetIsRemotelyControlled(bool isRemotelyControlled)
	{
		IsRemotelyControlled = isRemotelyControlled;
	}

	private bool IsAllowedToDoEffectInMultiplayer()
	{
		return !IsRemotelyControlled;
	}
}
