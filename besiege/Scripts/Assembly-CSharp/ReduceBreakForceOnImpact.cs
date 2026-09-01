using UnityEngine;

public class ReduceBreakForceOnImpact : SimBehaviour, IExplosionEffect
{
	public static bool OnlyInMP = true;

	public ConfigurableJoint joint;

	public float impactThreshold = 2f;

	public float maxImpactSpeed = 4f;

	public float firstBreakForce = 50000f;

	public float reduceMultiplier = 1200f;

	private bool hasStartedBreaking;

	private BlockBehaviour block;

	public static bool Used
	{
		get
		{
			return StatMaster.isMP || !OnlyInMP;
		}
	}

	protected override void Start()
	{
		base.Start();
		if (!Used)
		{
			Object.Destroy(this);
			return;
		}
		block = GetComponent<BlockBehaviour>();
		hasStartedBreaking = false;
	}

	public void ReduceJointBreakForce(float impactSqrSpeed)
	{
		if (base.isSimulating && (!(joint == null) || block.isParented) && (!block.HasParentMachine || !block.ParentMachine.UnbreakableMode))
		{
			float value;
			if (!hasStartedBreaking)
			{
				value = firstBreakForce;
				hasStartedBreaking = true;
			}
			else
			{
				float value2 = Mathf.Sqrt(impactSqrSpeed);
				float num = Mathf.Clamp(value2, 0f, maxImpactSpeed) * reduceMultiplier;
				value = ((!block.isParented) ? (joint.breakForce - num) : (block.jointBreakForce - num));
			}
			if (block.isParented)
			{
				block.jointBreakForce = Mathf.Clamp(value, 0f, firstBreakForce);
				block.VirtualJointBreakCollision();
			}
			else
			{
				joint.breakForce = Mathf.Clamp(value, 0f, firstBreakForce);
			}
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.isSimulating)
		{
			return false;
		}
		if ((float)(mask & 8) != 0f)
		{
			explosionPos = new Vector3(explosionPos.x, explosionPos.y - upPower, explosionPos.z);
			Vector3 vector = block.transform.TransformPoint(block.originalCOM) - explosionPos;
			float magnitude = vector.magnitude;
			Vector3 vector2 = (1f - magnitude / radius) * power * vector.normalized;
			ReduceJointBreakForce((vector2 / block.Rigidbody.mass * Time.fixedDeltaTime).sqrMagnitude);
			return true;
		}
		return false;
	}
}
