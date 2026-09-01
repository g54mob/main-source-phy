using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/AxialDrag")]
public class AxialDrag : BlockBehaviour
{
	public Vector3 AxisDrag;

	public Transform upTransform;

	public Vector3 xyz;

	public Vector3 currentVelocity;

	public float currentVelocitySqr;

	public float dragForceMagnitude;

	public Vector3 dragForceVector;

	public float velocityCap = 100f;

	private float sqrCap;

	private float timeToDry = 2f;

	private float dryingTime;

	private float dryness = 1f;

	private float drynessTime;

	[HideInInspector]
	public float oldMass;

	protected BlockBehaviour parent;

	[Header("Debug - Water Options")]
	public bool disableInWater;

	public bool debugInfo;

	[Header("Debug - Axial Drag Special for water")]
	public bool specialWaterDrag = true;

	public float dragMultiplier = 40f;

	protected override void Awake()
	{
		base.Awake();
		if (isSimulating && SimPhysics)
		{
			sqrCap = velocityCap * velocityCap;
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void FixedUpdateBlock()
	{
		if (noRigidbody)
		{
			return;
		}
		if (disableInWater)
		{
			DisableInWater();
			if (dryness == 0f)
			{
				return;
			}
		}
		currentVelocity = Rigidbody.velocity;
		Vector3 vector = upTransform.InverseTransformDirection(currentVelocity);
		xyz = Vector3.Scale(-vector, AxisDrag);
		if (base.InWater && specialWaterDrag && !StatMaster.GodTools.GravityDisabled)
		{
			currentVelocitySqr = Mathf.Min(currentVelocity.sqrMagnitude * dragMultiplier, sqrCap * 0.5f);
			if (specialWaterDrag)
			{
				currentVelocitySqr *= dragMultiplier;
			}
			xyz = upTransform.TransformDirection(xyz);
			Rigidbody.AddForce(xyz * currentVelocitySqr);
		}
		else
		{
			currentVelocitySqr = Mathf.Min(currentVelocity.sqrMagnitude, sqrCap);
			if (disableInWater)
			{
				xyz *= dryness;
			}
			Rigidbody.AddRelativeForce(xyz * currentVelocitySqr);
			if (StatMaster.aeroCoded)
			{
				xyz = base.transform.TransformDirection(xyz);
			}
		}
		if (StatMaster.aeroCoded)
		{
			originalDrag = Mathf.Max(0f, Vector3.Dot(xyz * currentVelocitySqr * 8E-05f / AxisDrag.magnitude, -currentVelocity.normalized));
		}
	}

	protected virtual void DisableInWater()
	{
		bool flag = base.InWater && !StatMaster.GodTools.GravityDisabled;
		if (blockJoint != null && parent == null && blockJoint.connectedBody != null)
		{
			parent = blockJoint.connectedBody.GetComponent<BlockBehaviour>();
		}
		if (flag || (parent != null && parent.InWater))
		{
			if (WaterController.WingPalenMassChange && Rigidbody.mass != WaterController.WingMass)
			{
				ChangeMass(WaterController.WingMass);
			}
			dryness = 0f;
			if (dryingTime != timeToDry)
			{
				dryingTime = timeToDry;
			}
			return;
		}
		if (dryingTime > 0f)
		{
			dryingTime -= Time.fixedDeltaTime;
			return;
		}
		if (!flag && WaterController.WingPalenMassChange && Rigidbody.mass != originalMass)
		{
			ChangeMass(originalMass);
		}
		if (dryness != 1f)
		{
			drynessTime += Time.fixedDeltaTime;
			dryness = Mathf.Clamp01(Mathf.Lerp(0f, 1f, drynessTime));
		}
		else if (drynessTime != 0f)
		{
			drynessTime = 0f;
		}
	}
}
