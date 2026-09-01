using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/BallastWeightController")]
public class BallastWeightController : BlockBehaviour
{
	private MSlider massSlider;

	public MSlider MassSlider
	{
		get
		{
			return massSlider;
		}
	}

	public override float WaterDrag
	{
		get
		{
			return _waterDrag;
		}
		set
		{
			if (!noRigidbody && (calcDragInWater || value == 0f))
			{
				if (waterDragMulti != 0f)
				{
					value *= waterDragMulti;
				}
				if (_inWater && value > 0f)
				{
					value += 0.2f;
				}
				Rigidbody.drag += value - _waterDrag;
				_waterDrag = value;
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating || SimPhysics)
		{
			massSlider = AddSlider(2420, "mass", 0.5f, 0.2f, 3f, string.Empty);
			massSlider.ValueChanged += MassSliderChanged;
			ChangeMass(massSlider.Value);
			SetOriginalDensity();
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
			SetOriginalDensity();
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	private void SetOriginalDensity()
	{
		density += 0.133935f;
		originalDensity = density;
	}
}
