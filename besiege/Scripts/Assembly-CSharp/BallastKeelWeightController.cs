using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/BallastKeelWeightController")]
public class BallastKeelWeightController : BlockBehaviour
{
	private MSlider massSlider;

	public MSlider MassSlider
	{
		get
		{
			return massSlider;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating || SimPhysics)
		{
			massSlider = AddSlider(2420, "mass", 5f, 1f, 10f, string.Empty);
			massSlider.ValueChanged += MassSliderChanged;
		}
	}

	public void SetMass(float m)
	{
		massSlider.SetValue(m);
	}

	public XData SaveMass()
	{
		return massSlider.Serialize();
	}

	protected override void Start()
	{
		base.Start();
		if (!isSimulating || SimPhysics)
		{
			MassSliderChanged(massSlider.Value);
		}
	}

	private void MassSliderChanged(float newMass)
	{
		if (!noRigidbody && Rigidbody.mass != newMass)
		{
			ChangeMass(newMass);
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}
}
