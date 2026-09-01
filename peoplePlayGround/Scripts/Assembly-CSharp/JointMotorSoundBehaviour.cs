using System;
using UnityEngine;

public class JointMotorSoundBehaviour : MonoBehaviour
{
	public Joint2D Joint;

	public LoopSoundBehaviour LoopSoundBehaviour;

	public float SpeedThreshold = 0.5f;

	private void FixedUpdate()
	{
		if (Joint == null)
		{
			if (LoopSoundBehaviour.IsPlaying)
			{
				LoopSoundBehaviour.StopLoop();
			}
			return;
		}
		Joint2D joint = Joint;
		JointMotor2D motor;
		if (!(joint is SliderJoint2D sliderJoint2D))
		{
			if (!(joint is HingeJoint2D hingeJoint2D))
			{
				if (!(joint is WheelJoint2D wheelJoint2D))
				{
					throw new Exception("Provided joint without motor");
				}
				motor = wheelJoint2D.motor;
			}
			else
			{
				motor = hingeJoint2D.motor;
			}
		}
		else
		{
			motor = sliderJoint2D.motor;
		}
		bool flag = Mathf.Abs(motor.motorSpeed) > SpeedThreshold;
		if (flag && !LoopSoundBehaviour.IsPlaying)
		{
			LoopSoundBehaviour.StartLoop();
		}
		else if (!flag && LoopSoundBehaviour.IsPlaying)
		{
			LoopSoundBehaviour.StopLoop();
		}
	}
}
