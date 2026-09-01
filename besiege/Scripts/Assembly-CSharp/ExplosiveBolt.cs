using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/ExplosiveBolt")]
public class ExplosiveBolt : BlockBehaviour
{
	public float explodePower = 1000f;

	public float explodeTorquePower = 1000f;

	private MKey explodeKey;

	private MSlider powerSlider;

	private ConfigurableJoint myJoint;

	private bool hasJoint;

	public MKey ExplodeKey
	{
		get
		{
			return explodeKey;
		}
	}

	public MSlider PowerSlider
	{
		get
		{
			return powerSlider;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating || SimPhysics)
		{
			myJoint = blockJoint as ConfigurableJoint;
			explodeKey = AddKey(2492, "explode", ControlScheme.BlockControls.Detach, 0, KeyCode.V);
			powerSlider = AddSlider(2427, "epower", 1f, 0f, 3f, string.Empty);
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		hasJoint = true;
		if (!SimPhysics || myJoint == null)
		{
			hasJoint = false;
		}
		else if (myJoint.connectedBody == null)
		{
			Object.Destroy(myJoint);
			hasJoint = false;
		}
	}

	private void OnJointBreak()
	{
		if (!SimPhysics)
		{
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
			return;
		}
		hasJoint = false;
		myJoint = null;
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if ((!isSimulating || SimPhysics) && explodeKey.IsPressed)
		{
			Explode();
		}
	}

	public override void EmulationUpdateBlock()
	{
		if (explodeKey.EmulationPressed())
		{
			Explode();
		}
	}

	public void Explode()
	{
		if (hasJoint)
		{
			float num = explodePower * powerSlider.Value;
			Rigidbody connectedBody = myJoint.connectedBody;
			ConfigurableJoint configurableJoint = myJoint;
			float num2 = 0f;
			myJoint.breakTorque = num2;
			configurableJoint.breakForce = num2;
			Object.DestroyImmediate(myJoint);
			if (!noRigidbody)
			{
				Rigidbody.AddForce(base.transform.forward * num);
				Rigidbody.AddTorque(Random.insideUnitSphere * explodeTorquePower);
			}
			connectedBody.AddForceAtPosition(-base.transform.forward * num, base.transform.position);
			hasJoint = false;
		}
	}
}
