using UnityEngine;

public class GateDoor : SimBehaviour, IExplosionEffect
{
	public Gate gate;

	private Gate.DamageAmount dmc;

	private Rigidbody rb;

	protected override void Start()
	{
		if (StatMaster.levelSimulating)
		{
			base.Start();
			if (gate == null)
			{
				gate = GetComponentInParent<Gate>();
			}
			if (rb == null)
			{
				rb = GetComponent<Rigidbody>();
			}
			dmc = gate.dmc;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		float sqrMagnitude = other.relativeVelocity.sqrMagnitude;
		BlockBehaviour componentInParent = other.transform.GetComponentInParent<BlockBehaviour>();
		bool flag = sqrMagnitude >= dmc.minimalVelocity;
		if (componentInParent != null)
		{
			if (componentInParent is TimedRocket)
			{
				if (!(sqrMagnitude < dmc.minimalVelocity))
				{
					gate.ApplyDamage(sqrMagnitude * dmc.ProjectileScale, gate.currentEnforcement);
				}
				return;
			}
			if (componentInParent is CogMotorControllerHinge)
			{
				CogMotorControllerHinge cogMotorControllerHinge = componentInParent as CogMotorControllerHinge;
				if (cogMotorControllerHinge.Prefab.Type == BlockType.Drill && !gate.drills.Contains(other.rigidbody))
				{
					gate.drills.Add(other.rigidbody);
				}
				else if (cogMotorControllerHinge.Prefab.Type == BlockType.CircularSaw && !gate.sawDisc.Contains(other.rigidbody))
				{
					gate.sawDisc.Add(other.rigidbody);
				}
			}
			if (flag && componentInParent.Prefab.hasDamageType)
			{
				switch (componentInParent.Prefab.myDamageType)
				{
				case DamageType.Fire:
					gate.ApplyDamage(sqrMagnitude * dmc.FireScale, gate.currentEnforcement);
					return;
				case DamageType.Sharp:
					gate.ApplyDamage(sqrMagnitude * dmc.SharpScale, gate.currentEnforcement);
					return;
				}
			}
		}
		if (flag)
		{
			gate.ApplyDamage(sqrMagnitude * dmc.BluntScale, gate.currentEnforcement);
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if ((bool)other.rigidbody)
		{
			if (gate.drills.Contains(other.rigidbody))
			{
				gate.drills.Remove(other.rigidbody);
			}
			else if (gate.sawDisc.Contains(other.rigidbody))
			{
				gate.sawDisc.Remove(other.rigidbody);
			}
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.isSimulating || !base.SimPhysics)
		{
			return false;
		}
		if ((mask & 1) != 0)
		{
			float sqrMagnitude = rb.GetPointVelocity(explosionPos).sqrMagnitude;
			if (sqrMagnitude >= dmc.minimalVelocity)
			{
				gate.ApplyDamage(sqrMagnitude * dmc.BluntScale, gate.currentEnforcement);
			}
			if ((bool)gate.fireController && (mask & 4) != 0)
			{
				gate.fireController.CatchFire(1f);
			}
			return true;
		}
		return false;
	}
}
