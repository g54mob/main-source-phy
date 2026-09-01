using Landfall.MonoBatch;
using UnityEngine;

public class SetAnimationByInput : BatchedMonobehaviour
{
	private AnimationHandler anim;

	private GeneralInput input;

	private DataHandler data;

	protected override void Start()
	{
		base.Start();
		anim = GetComponent<AnimationHandler>();
		input = GetComponent<GeneralInput>();
		data = GetComponent<DataHandler>();
	}

	public override void BatchedUpdate()
	{
		if (data.dontMoveFor > 0f)
		{
			anim.currentState = 0;
		}
		else
		{
			if (data.Dead)
			{
				return;
			}
			if (input.inputDirection.magnitude != 0f)
			{
				if (input.inputDirection.z <= 0f)
				{
					anim.currentState = 3;
				}
				else if (input.shift)
				{
					anim.currentState = 2;
				}
				else
				{
					anim.currentState = 1;
				}
			}
			else if (Mathf.Abs(data.mainRig.angularVelocity.y) > 1f)
			{
				anim.currentState = 3;
			}
			else
			{
				anim.currentState = 0;
			}
		}
	}
}
