using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/SuspensionController")]
public class SuspensionController : BlockBehaviour
{
	public float springAmount = 20f;

	private MSlider springSlider;

	private MSlider damperSlider;

	public MSlider SpringSlider
	{
		get
		{
			return springSlider;
		}
	}

	public MSlider DamperSlider
	{
		get
		{
			return damperSlider;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating || SimPhysics)
		{
			springSlider = AddSlider(2470, "spring", 1f, 0.25f, 3f, string.Empty);
			springSlider.logScaling = true;
			damperSlider = AddSlider(4426, "damper", 1f, 0.1f, 3f, string.Empty);
			damperSlider.logScaling = true;
		}
	}

	protected override void Start()
	{
		base.Start();
		if (SimPhysics && isSimulating)
		{
			ConfigurableJoint configurableJoint = blockJoint as ConfigurableJoint;
			JointDrive xDrive = configurableJoint.xDrive;
			float value = springSlider.Value;
			float value2 = damperSlider.Value;
			value2 *= value2;
			xDrive.positionSpring = Mathf.Clamp(springAmount * value, -2.1474836E+09f, 2.1474836E+09f);
			xDrive.positionDamper = Mathf.Clamp(configurableJoint.xDrive.positionDamper * value * value2, -2.1474836E+09f, 2.1474836E+09f);
			configurableJoint.xDrive = xDrive;
		}
	}
}
