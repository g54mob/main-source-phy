using Landfall.MonoBatch;
using UnityEngine;

public class AnimationJoint : BatchedMonobehaviour
{
	public AnimationJointData animationData;

	private AnimationHandler anim;

	private DataHandler data;

	private ConfigurableJoint joint;

	private int internalAnimationState;

	public AnimationJointInstance defaultJoint;

	protected override void Start()
	{
		base.Start();
		joint = GetComponent<ConfigurableJoint>();
		anim = GetComponentInParent<AnimationHandler>();
		data = anim.GetComponent<DataHandler>();
		defaultJoint.name = "Default";
		defaultJoint.ID = -1;
		defaultJoint.useTwist = true;
		defaultJoint.highTwist = joint.highAngularXLimit.limit;
		defaultJoint.lowTwist = joint.lowAngularXLimit.limit;
		defaultJoint.useAngularY = true;
		defaultJoint.angularY = joint.angularYLimit.limit;
		defaultJoint.useAngularZ = true;
		defaultJoint.angularZ = joint.angularZLimit.limit;
	}

	public override void BatchedFixedUpdate()
	{
		if (anim.currentState == internalAnimationState)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < animationData.joints.Length; i++)
		{
			if (animationData.joints[i].ID == anim.currentState)
			{
				Set(animationData.joints[i]);
				flag = true;
			}
		}
		if (!flag)
		{
			Set(defaultJoint);
		}
	}

	private void Set(AnimationJointInstance currentAnimation)
	{
		if (!data || !data.Dead)
		{
			internalAnimationState = anim.currentState;
			if (currentAnimation.useTwist)
			{
				SoftJointLimit lowAngularXLimit = joint.lowAngularXLimit;
				lowAngularXLimit.limit = currentAnimation.lowTwist;
				joint.lowAngularXLimit = lowAngularXLimit;
				lowAngularXLimit = joint.highAngularXLimit;
				lowAngularXLimit.limit = currentAnimation.highTwist;
				joint.highAngularXLimit = lowAngularXLimit;
			}
			if (currentAnimation.useAngularY)
			{
				SoftJointLimit angularYLimit = joint.angularYLimit;
				angularYLimit.limit = currentAnimation.angularY;
				joint.angularYLimit = angularYLimit;
			}
			if (currentAnimation.useAngularZ)
			{
				SoftJointLimit angularZLimit = joint.angularZLimit;
				angularZLimit.limit = currentAnimation.angularZ;
				joint.angularZLimit = angularZLimit;
			}
		}
	}
}
