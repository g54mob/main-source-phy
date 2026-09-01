using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/ScalingBlock")]
public class ScalingBlock : BlockBehaviour
{
	private bool defaultMassSizeToggleValue;

	private MToggle massFromSize;

	private MSlider massSlider;

	private MSlider densitySlider;

	public MToggle MassFromSizeToggle
	{
		get
		{
			return massFromSize;
		}
	}

	public MSlider MassSlider
	{
		get
		{
			return massSlider;
		}
	}

	public MSlider DensitySlider
	{
		get
		{
			return densitySlider;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating || SimPhysics)
		{
			massSlider = AddSlider(2420, "mass", 0.5f, 0.2f, 2f, string.Empty);
			densitySlider = AddSlider(4594, "density", density, 0f, 5f, string.Empty);
			massFromSize = AddToggle(2471, "mass-from-size", defaultMassSizeToggleValue);
			massFromSize.Toggled += MassTypeToggled;
			massSlider.ValueChanged += UpdateMass;
			densitySlider.ValueChanged += UpdateDensity;
			UpdateMass(massSlider.Value);
		}
	}

	protected void MassTypeToggled(bool b)
	{
		UpdateMass(massSlider.Value);
	}

	protected void UpdateMass(float m)
	{
		if (!noRigidbody)
		{
			Vector3 lossyScale = base.transform.lossyScale;
			Rigidbody.mass = massSlider.Value * ((!massFromSize.IsActive) ? 1f : (lossyScale.x * lossyScale.y * lossyScale.z));
		}
	}

	protected void UpdateDensity(float d)
	{
		density = d;
	}
}
