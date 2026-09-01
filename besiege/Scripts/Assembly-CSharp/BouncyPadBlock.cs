using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Bouncy Pad Block")]
public class BouncyPadBlock : BlockBehaviour
{
	public ConfigurableJoint myJoint;

	protected MSlider springSlider;

	protected MSlider distanceSlider;

	public MSlider SpringSlider
	{
		get
		{
			return springSlider;
		}
	}

	public MSlider FloppySlider
	{
		get
		{
			return distanceSlider;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!stripped && !isSimulating)
		{
			springSlider = AddSlider(2470, "springiness", 0.5f, 0.5f, 2f, string.Empty);
			distanceSlider = AddSlider(4889, "distance", 0.01f, 0f, 1f, string.Empty);
			springSlider.ValueChanged += SetSpring;
			distanceSlider.ValueChanged += SetDistance;
		}
	}

	protected void SetSpring(float v)
	{
		if (float.IsNaN(v))
		{
			v = 0f;
		}
		SoftJointLimitSpring linearLimitSpring = myJoint.linearLimitSpring;
		linearLimitSpring.spring = Mathf.Max(0f, 30000f + 1313334f * (v - 0.5f));
		linearLimitSpring.damper = Mathf.Lerp(400f, 30000f, (v - 0.5f) * 0.67f);
		myJoint.linearLimitSpring = linearLimitSpring;
	}

	protected void SetDistance(float v)
	{
		if (float.IsNaN(v))
		{
			v = 0f;
		}
		SoftJointLimit linearLimit = myJoint.linearLimit;
		v = Mathf.Clamp01(v);
		linearLimit.limit = v * 0.1f;
		myJoint.linearLimit = linearLimit;
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		if (Prefab.hasMyBounds)
		{
			for (int i = 0; i < myBounds.childColliders.Count; i++)
			{
				myBounds.childColliders[i].contactOffset = 0.05f;
			}
		}
	}
}
