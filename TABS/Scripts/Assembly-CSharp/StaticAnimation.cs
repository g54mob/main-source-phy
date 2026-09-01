using System;

[Serializable]
public class StaticAnimation
{
	public string animationName;

	public float startMountAnimationDelay;

	public float time = 2f;

	public float startDelay;

	public bool followPosition = true;

	public bool followRotaiton = true;

	public bool animateHead = true;

	public bool animateTorso = true;

	public bool animateHip = true;

	public bool animateLeftArm = true;

	public bool animateLefftHand = true;

	public bool animateRightArm = true;

	public bool animateRightHand = true;

	public bool animateLeftLeg = true;

	public bool animateLeftKnee = true;

	public bool animateRightLeg = true;

	public bool animateRightKnee = true;
}
