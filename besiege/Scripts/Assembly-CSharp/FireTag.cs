using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTag : SimBehaviour, IExplosionEffect
{
	public IEnumerable<IFireEffect> fireAffected;

	public FireController fireControllerCode;

	public ParticleSystem slowSmoke;

	public EntityAI ai;

	public bool hasController;

	public bool igniteOnStart;

	public bool canBeDoused = true;

	public bool igniteOnce = true;

	public bool hasBeenBurned;

	public bool onlyIgniteOncePerFrame = true;

	[HideInInspector]
	public bool alreadyIgnitedThisFrame;

	[NonSerialized]
	public bool burning;

	private bool pyroed;

	private bool hasAffected;

	private bool prevUseBehaviour;

	private bool prevAutomaticTargetSystem;

	private int avoidingFire;

	[HideInInspector]
	public float lastIntensity;

	private Coroutine aiFireWait;

	[Obsolete("FireTag::isAI field is obsolete.", false)]
	[HideInInspector]
	public bool isAI;

	[HideInInspector]
	[Obsolete("FireTag::block field is obsolete.", false)]
	public BlockBehaviour block;

	[Obsolete("FireTag::bvc field is obsolete.", false)]
	[HideInInspector]
	public BlockVisualController bvc;

	[Obsolete("FireTag::hasBvc field is obsolete.", false)]
	[HideInInspector]
	public bool hasBvc;

	protected bool WasIgnitedThisFrame
	{
		get
		{
			return alreadyIgnitedThisFrame && onlyIgniteOncePerFrame;
		}
	}

	public void SetAffected(IEnumerable<IFireEffect> a)
	{
		fireAffected = a;
		hasAffected = true;
	}

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating && base.SimPhysics && fireControllerCode == null)
		{
			FireController[] componentsInChildren = GetComponentsInChildren<FireController>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].gameObject.activeInHierarchy)
				{
					fireControllerCode = componentsInChildren[i];
				}
			}
		}
		hasController = fireControllerCode != null;
		if (hasController && fireControllerCode.fireParticles != null)
		{
			WaterFogController.AddEffectMat(fireControllerCode.fireParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial);
		}
		if (base.SimPhysics && igniteOnStart && (!HasBasicInfo || !basicInfo.InWater || !hasController || basicInfo.submergedPercent < fireControllerCode.submergeLimit))
		{
			Ignite(1f);
		}
	}

	private bool CanIgnite()
	{
		if (StatMaster.Rules.DisableFire)
		{
			return false;
		}
		if (!base.isSimulating)
		{
			return false;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return false;
		}
		if ((igniteOnce && burning) || (igniteOnce && hasBeenBurned))
		{
			return false;
		}
		return true;
	}

	public void OnPyro()
	{
		StatMaster.GodTools.HasBeenUsed = true;
		if (HasBasicInfo && basicInfo.infoType == BasicInfo.BasicInfoType.Block && (basicInfo as BlockBehaviour).gotChildBlocks)
		{
			Ray ray = Camera.main.ScreenPointToRay(InputManager.CursorPosition());
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				Ignite(hitInfo.collider, 1f);
				pyroed = true;
				return;
			}
		}
		pyroed = true;
		Ignite(1f);
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.isSimulating || !base.SimPhysics)
		{
			return false;
		}
		if ((mask & 0x10) != 0)
		{
			Ignite();
			return true;
		}
		return false;
	}

	private bool CheckChildBlocks(Collider col)
	{
		if (!HasBasicInfo || basicInfo.infoType != BasicInfo.BasicInfoType.Block)
		{
			return false;
		}
		BlockBehaviour blockBehaviour = basicInfo as BlockBehaviour;
		if (blockBehaviour.gotChildBlocks)
		{
			BlockBehaviour childBlockFromCollider = blockBehaviour.GetChildBlockFromCollider(col);
			if (!object.ReferenceEquals(childBlockFromCollider, null))
			{
				if (!object.ReferenceEquals(childBlockFromCollider.fireTag, null))
				{
					childBlockFromCollider.fireTag.Ignite(col);
				}
				return true;
			}
			if (!burning && base.SimPhysics)
			{
				blockBehaviour.CreateSimLists();
				foreach (BlockBehaviour key in blockBehaviour.parentedColliders.Keys)
				{
					if (!object.ReferenceEquals(key.fireTag, null))
					{
						key.fireTag.Ignite(col);
					}
				}
			}
		}
		return false;
	}

	public void Ignite(float intensity = 0f)
	{
		Ignite(null, intensity);
	}

	public void Ignite(Collider col, float intensity = 0f)
	{
		if (WasIgnitedThisFrame)
		{
			if (intensity > lastIntensity)
			{
				lastIntensity = intensity;
			}
		}
		else
		{
			if (!CanIgnite() || (base.SimPhysics && CheckChildBlocks(col)))
			{
				return;
			}
			lastIntensity = intensity;
			hasBeenBurned = true;
			alreadyIgnitedThisFrame = true;
			byte eventData = 0;
			if (!burning)
			{
				if (base.SimPhysics)
				{
					if (HasBasicInfo && basicInfo.hasAiScript && basicInfo.aiEntity.disposition.AvoidFire && basicInfo.aiEntity.chanceToCatchOnFire < UnityEngine.Random.value && avoidingFire <= 0)
					{
						StartCoroutine(IEIgniteAI(col));
						return;
					}
					float num = 0f;
					if (hasController && !fireControllerCode.hasFullFireDuration)
					{
						num = fireControllerCode.randomAmount;
						float num2 = UnityEngine.Random.Range(0.1f, 1f);
						fireControllerCode.SetFireDuration(num2 * num * 2f - num);
						eventData = (byte)Mathf.RoundToInt(num2 * 255f);
					}
				}
				if (hasController && !fireControllerCode.onFire && fireControllerCode.gameObject.activeInHierarchy)
				{
					fireControllerCode.CatchFire(intensity);
				}
			}
			bool simPhysics = base.SimPhysics;
			if (StatMaster.isMP && !StatMaster.isLocalSim)
			{
				if (simPhysics)
				{
					NetworkBlock netBlock = base.NetBlock;
					if (netBlock != null)
					{
						if (!burning)
						{
							netBlock.Event(NetworkEntity.EntityEvent.Ignite, eventData);
						}
						else
						{
							netBlock.Event(NetworkEntity.EntityEvent.IgniteBurning);
						}
					}
				}
			}
			else if (HasBasicInfo && basicInfo.infoType == BasicInfo.BasicInfoType.Block)
			{
				BlockType type = (basicInfo as BlockBehaviour).Prefab.Type;
				if (type != BlockType.FlameBall && type != BlockType.Torch && type != BlockType.Rocket && hasController)
				{
					AchievementHelper.Increment(9, 1);
				}
			}
			pyroed = false;
			burning = true;
			if (simPhysics)
			{
				if (!hasAffected)
				{
					fireAffected = ReferenceMaster.GetInterfaces<IFireEffect>(base.gameObject);
					hasAffected = true;
				}
				{
					foreach (IFireEffect item in fireAffected)
					{
						if (base.gameObject.activeInHierarchy && item != null)
						{
							item.OnIgnite(this, col, pyroed);
						}
					}
					return;
				}
			}
			VisualiseFireHit(col);
		}
	}

	public IEnumerator IEIgniteAI(Collider col)
	{
		ai = basicInfo.aiEntity;
		if (ai.isDead || !ai.disposition.AvoidFire || object.ReferenceEquals(col, null) || !(col.transform.parent.transform != base.transform))
		{
			yield break;
		}
		if ((double)ai.chanceToCatchOnFire < 0.1)
		{
			ai.chanceToCatchOnFire += 0.01f;
		}
		if (avoidingFire == 0)
		{
			prevUseBehaviour = ai.disposition.useBehaviours;
			prevAutomaticTargetSystem = ai.disposition.AutomaticTargetSystem;
		}
		avoidingFire++;
		ai.disposition.useBehaviours = false;
		ai.disposition.AutomaticTargetSystem = false;
		ai.aiControllerState = EntityAI.EntityState.Strafing;
		ai.TargetBlock.NewTargetBlock(col.transform);
		if ((bool)slowSmoke)
		{
			slowSmoke.Play();
		}
		yield return new WaitForSeconds(ai.disposition.FireAvoidancetime);
		alreadyIgnitedThisFrame = false;
		avoidingFire--;
		if (avoidingFire <= 0 || burning)
		{
			if ((bool)slowSmoke)
			{
				slowSmoke.Stop();
			}
			ai.TargetBlock.Null();
			ai.disposition.useBehaviours = prevUseBehaviour;
			ai.disposition.AutomaticTargetSystem = prevAutomaticTargetSystem;
		}
	}

	public void VisualiseFireHit(Collider col = null)
	{
		if (HasBasicInfo && basicInfo.infoType == BasicInfo.BasicInfoType.Block)
		{
			BlockBehaviour blockBehaviour = basicInfo as BlockBehaviour;
			if (blockBehaviour.Prefab.hasBVC)
			{
				blockBehaviour.VisualController.OnIgnite(this, col, pyroed);
			}
		}
	}

	public void WaterHit()
	{
		if (base.SimPhysics && canBeDoused)
		{
			hasBeenBurned = false;
			if (hasController)
			{
				fireControllerCode.DouseFire();
			}
		}
	}

	private void LateUpdate()
	{
		if (alreadyIgnitedThisFrame)
		{
			alreadyIgnitedThisFrame = false;
		}
	}
}
