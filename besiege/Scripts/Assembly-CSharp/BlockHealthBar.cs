using System;
using UnityEngine;

[AddComponentMenu("Blocks/BlockHealthBar")]
public class BlockHealthBar : SimComponent, IExplosionEffect
{
	public float health = 4f;

	private float jointHealth = 4f;

	public bool weakenSecondaryJoints;

	private bool exploded;

	[HideInInspector]
	public float maxHealth;

	protected float startBreakForce;

	protected BlockBehaviour block;

	protected Machine machine;

	public Joint[] joints = new Joint[0];

	private float[] jForces = new float[0];

	private float[] jTorques = new float[0];

	public override void Init(Machine machine, BlockBehaviour block)
	{
		base.Init(machine, block);
		jointHealth = (maxHealth = health);
		this.machine = machine;
		this.block = block;
		if (block.SimPhysics)
		{
			ReferenceMaster.GetIntactBlocks(machine.PlayerID).Add(block);
			if (block.blockJoint != null)
			{
				startBreakForce = block.blockJoint.breakForce;
			}
		}
	}

	public void StartPhysics()
	{
		if (weakenSecondaryJoints)
		{
			if (joints.Length == 0)
			{
				joints = base.gameObject.GetComponentsInChildren<Joint>();
			}
		}
		else
		{
			if (block.blockJoint == null || block.blockJoint.connectedBody == null)
			{
				return;
			}
			joints = new Joint[1] { block.blockJoint };
		}
		jForces = new float[joints.Length];
		jTorques = new float[joints.Length];
		int num = 0;
		for (int i = 0; i < joints.Length; i++)
		{
			if (!(joints[i] == null))
			{
				jForces[num] = joints[i].breakForce;
				jTorques[num] = joints[i].breakTorque;
				joints[num] = joints[i];
				num++;
			}
		}
		if (num < joints.Length)
		{
			Joint[] destinationArray = new Joint[num];
			Array.Copy(joints, destinationArray, num);
			joints = destinationArray;
		}
	}

	public virtual bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!isInitialized || !block.SimPhysics)
		{
			return false;
		}
		if ((mask & 8) != 0)
		{
			exploded = true;
			if (block.Prefab.Type == BlockType.BuildSurface && !machine.UnbreakableMode)
			{
				((BuildSurface)block).BreakSurface(power, upPower, torquePower, explosionPos, radius);
			}
			float amount = 1f;
			if (block.InWater && !StatMaster.GodTools.GravityDisabled)
			{
				float magnitude = (explosionPos - base.transform.position).magnitude;
				amount = ((!(magnitude < radius / 2f)) ? 1 : 3);
			}
			DamageBlock(amount);
			return true;
		}
		return false;
	}

	public virtual void DamageBlock(float amount)
	{
		DamageBlock(amount, true);
	}

	public virtual void DamageBlock(float amount, bool updateVis)
	{
		if (!isInitialized || health <= 0f || !block.isSimulating || machine.UnbreakableMode)
		{
			return;
		}
		float num = health;
		health -= amount;
		if (health < 0f)
		{
			health = 0f;
		}
		else if (health > maxHealth)
		{
			health = maxHealth;
		}
		float num2 = num - health;
		if (StatMaster.isMP && block.SimPhysics && num2 != 0f)
		{
			ServerMachine serverMachine = machine as ServerMachine;
			if (serverMachine.registerDamage)
			{
				serverMachine.ApplyBlockDamage(block, num2);
			}
		}
		if (block.Prefab.hasBVC)
		{
			float num3 = 1f - health / maxHealth;
			if (StatMaster.isMP && block.SimPhysics)
			{
				NetworkBlock netBlock = block.NetBlock;
				if (netBlock != null)
				{
					netBlock.Event(NetworkEntity.EntityEvent.SetDamageLevel, (byte)(Mathf.Clamp01(num3) * 255f));
				}
				else
				{
					Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
				}
			}
			if (updateVis)
			{
				block.VisualController.SetDamageLevel(num3);
			}
		}
		switch (block.Prefab.Type)
		{
		case BlockType.Bomb:
			(block as ExplodeOnCollideBlock).Explodey();
			break;
		case BlockType.Balloon:
			if (health == 0f)
			{
				(block as BalloonController).Pop();
			}
			break;
		case BlockType.SqrBalloon:
			if (health == 0f)
			{
				(block as SqrBalloonController).Pop();
			}
			break;
		case BlockType.Buoyancy:
		case BlockType.BigBarrel:
			WeakenJoints(health);
			if (health == 0f)
			{
				(block as BuoyancyDensityController).Break();
			}
			break;
		case BlockType.Sail:
			WeakenJoints(health);
			if (health == 0f)
			{
				(block as SailBlock).BreakAll();
			}
			break;
		default:
			WeakenJoints(health);
			break;
		}
		if (health <= 0f)
		{
			ReferenceMaster.IntactBlocks[machine.PlayerID].Remove(block);
			if (ReferenceMaster.IntactBlocks[machine.PlayerID].Count == 0)
			{
				block.ParentMachine.hasIntactBlocks = false;
			}
			base.enabled = false;
		}
	}

	public void WeakenJoints(float current)
	{
		SetJointHealth(current / maxHealth);
	}

	public void SetJointHealth(float pct)
	{
		float num = jointHealth / maxHealth;
		if (pct >= num)
		{
			return;
		}
		jointHealth = pct * maxHealth;
		for (int i = 0; i < joints.Length; i++)
		{
			Joint joint = joints[i];
			if ((bool)joint)
			{
				joint.breakForce = jForces[i] * pct;
				joint.breakTorque = jTorques[i] * pct;
			}
		}
	}

	public virtual void RepairBlock(float amount, bool updateVis)
	{
		DamageBlock(0f - amount, updateVis);
	}
}
