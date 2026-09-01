using UnityEngine;

public class AnimationForce : MonoBehaviour
{
	public AnimationForceData animationData;

	public bool isRight = true;

	private RigidbodyHolder rigidbodyHolder;

	private AnimationHandler anim;

	private DataHandler data;

	private GeneralInput input;

	private Rigidbody rig;

	private Transform forcePoint;

	private Transform torso;

	private Transform hip;

	[HideInInspector]
	public bool IAmRight = true;

	[HideInInspector]
	public float counter;

	[HideInInspector]
	public float isRightAmount = 1f;

	private void Start()
	{
		torso = base.transform.root.GetComponentInChildren<Torso>().transform;
		hip = base.transform.root.GetComponentInChildren<Hip>().transform;
		rig = GetComponent<Rigidbody>();
		input = GetComponentInParent<GeneralInput>();
		anim = GetComponentInParent<AnimationHandler>();
		data = GetComponentInParent<DataHandler>();
		ForcePoint componentInChildren = GetComponentInChildren<ForcePoint>();
		if ((bool)componentInChildren)
		{
			forcePoint = componentInChildren.transform;
		}
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < animationData.animations.Length; i++)
		{
			if (animationData.animations[i].ID == anim.currentState)
			{
				Animate(animationData.animations[i]);
			}
		}
	}

	private void Animate(AnimationForceInstance currentAnimation)
	{
		if (data.isRight != IAmRight)
		{
			if (counter >= currentAnimation.switchDelay)
			{
				IAmRight = data.isRight;
				counter = 0f;
			}
			counter += Time.fixedDeltaTime;
		}
		if (IAmRight)
		{
			isRightAmount = Mathf.Clamp(isRightAmount += Time.fixedDeltaTime * 5f / Mathf.Clamp(currentAnimation.smoothing, 0.01f, 100f), 0f, 1f);
		}
		else
		{
			isRightAmount = Mathf.Clamp(isRightAmount -= Time.fixedDeltaTime * 5f / Mathf.Clamp(currentAnimation.smoothing, 0.01f, 100f), 0f, 1f);
		}
		Vector3 zero = Vector3.zero;
		zero = ((!isRight) ? Vector3.Lerp(currentAnimation.forwardForce, currentAnimation.backwardForce, isRightAmount) : Vector3.Lerp(currentAnimation.backwardForce, currentAnimation.forwardForce, isRightAmount));
		if (currentAnimation.forceType == AnimationForceInstance.ForceType.Self)
		{
			zero = base.transform.TransformDirection(zero);
		}
		if (currentAnimation.forceType == AnimationForceInstance.ForceType.Hip)
		{
			zero = hip.TransformDirection(zero);
		}
		if (currentAnimation.forceType == AnimationForceInstance.ForceType.Torso)
		{
			zero = torso.TransformDirection(zero);
		}
		if (currentAnimation.forceType == AnimationForceInstance.ForceType.IputDirection)
		{
			zero = data.groundedMovementDirectionObject.forward * (zero.x + zero.y + zero.z);
		}
		zero *= data.muscleControl;
		if ((bool)forcePoint)
		{
			rig.AddForceAtPosition(zero, forcePoint.position, ForceMode.Acceleration);
		}
		else
		{
			rig.AddForce(zero, ForceMode.Acceleration);
		}
	}
}
