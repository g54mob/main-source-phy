using UnityEngine;

[AddComponentMenu("LevelEditor/Entities/JointEntity")]
public class JointEntity : GenericEntity
{
	public LevelObjectJoint joint;

	private MSlider breakSlider;

	private MToggle isHinge;

	public override void Init()
	{
		if (!isInitialized)
		{
			base.Init();
			breakSlider = AddSliderUnclamped(2421, GenericEntity.LOGIC_PREFIX + "breakForce", joint.breakForce, 0f, 50000f, string.Empty, string.Empty, true);
			breakSlider.logScaling = true;
			breakSlider.ValueChanged += OnBreakChanged;
			isHinge = AddToggle(4875, GenericEntity.LOGIC_PREFIX + "isHinge", joint.hinge);
			isHinge.Toggled += OnTypeChanged;
		}
	}

	private void OnBreakChanged(float newValue)
	{
		joint.breakForce = newValue;
	}

	private void OnTypeChanged(bool newValue)
	{
		joint.hinge = newValue;
	}

	public override bool TriggerEvaluate()
	{
		return false;
	}
}
