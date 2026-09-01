using Landfall.MonoBatch;
using UnityEngine;

public class StepHandler : BatchedMonobehaviour
{
	public float multiplier = 1f;

	public StepData stepData;

	private AnimationHandler anim;

	private DataHandler data;

	private Transform kneeLeft;

	private Transform kneeRight;

	private Transform hip;

	private bool lastWasIdle;

	private float stepTimeRandom = 1f;

	private float sinceSwitched;

	private float m_gravityMultiplier = 1f;

	protected override void Start()
	{
		base.Start();
		anim = GetComponent<AnimationHandler>();
		data = GetComponent<DataHandler>();
		kneeLeft = GetComponentInChildren<KneeLeft>().transform;
		kneeRight = GetComponentInChildren<KneeRight>().transform;
		hip = GetComponentInChildren<Hip>().transform;
		stepTimeRandom = NormalRandom.GetRandom(0.5f, 2f);
	}

	public override void BatchedUpdate()
	{
		if (data.Dead)
		{
			return;
		}
		if (anim.currentState == 0)
		{
			bool flag = hip.InverseTransformPoint(kneeRight.position).z > hip.InverseTransformPoint(kneeLeft.position).z;
			data.isRight = !flag;
			data.sinceSwitchedStep = 0f;
			lastWasIdle = true;
			return;
		}
		if (lastWasIdle)
		{
			if (anim.currentState == 3)
			{
				data.isRight = !data.isRight;
			}
			lastWasIdle = false;
		}
		if (data.sinceSwitchedStep < data.currentLowSwitchCap * 0.5f)
		{
			data.sinceSwitchedStep += Time.deltaTime * stepTimeRandom * m_gravityMultiplier / multiplier;
		}
		sinceSwitched += Time.deltaTime * stepTimeRandom * stepTimeRandom * m_gravityMultiplier / multiplier;
		StepInstance stepInstance = null;
		for (int i = 0; i < stepData.steps.Length; i++)
		{
			if (stepData.steps[i].ID == anim.currentState)
			{
				stepInstance = stepData.steps[i];
			}
		}
		if (stepInstance == null && (stepData.steps != null || stepData.steps.Length != 0))
		{
			stepInstance = stepData.steps[stepData.defaultState];
		}
		if (stepInstance == null)
		{
			return;
		}
		bool num = data.groundedMovementDirectionObject.InverseTransformPoint(kneeRight.position).z > data.groundedMovementDirectionObject.InverseTransformPoint(kneeLeft.position).z;
		data.currentLowSwitchCap = stepInstance.minTime;
		if (num == data.isRight || sinceSwitched > stepInstance.maxTime)
		{
			if (data.sinceSwitchedStep >= data.currentLowSwitchCap * 0.5f)
			{
				data.sinceSwitchedStep += Time.deltaTime * stepTimeRandom * m_gravityMultiplier / multiplier;
			}
			if (data.sinceSwitchedStep > stepInstance.minTime || data.sinceSwitchedStep > stepInstance.maxTime)
			{
				data.isRight = !data.isRight;
				data.sinceSwitchedStep = 0f;
				sinceSwitched = 0f;
			}
		}
	}
}
