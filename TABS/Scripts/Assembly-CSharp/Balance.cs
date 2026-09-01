using Landfall.MonoBatch;
using Landfall.TABS.Services;
using TFBGames;
using UnityEngine;

public class Balance : BatchedMonobehaviour
{
	public float forceMultiplier = 1f;

	public BalanceData balanceData;

	public float allowedLegAngle = 140f;

	public float allowedTorsoAngle = 130f;

	public float allowedFootDistance = 1.3f;

	private Rigidbody kneeRight;

	private Rigidbody kneeLeft;

	private Rigidbody hip;

	private Rigidbody torso;

	private Rigidbody head;

	private Transform kneeLeftPos;

	private Transform kneeRightPos;

	private DataHandler data;

	private AnimationHandler anim;

	private FixedTimeStepService fixedTimeStepService;

	private ITimeService timeService;

	private float cappedTimeNumber;

	protected override void Start()
	{
		base.Start();
		fixedTimeStepService = ServiceLocator.GetService<FixedTimeStepService>();
		timeService = ServiceLocator.GetService<ITimeService>();
		anim = GetComponent<AnimationHandler>();
		data = GetComponent<DataHandler>();
		hip = GetComponentInChildren<Hip>().GetComponent<Rigidbody>();
		torso = GetComponentInChildren<Torso>().GetComponent<Rigidbody>();
		head = GetComponentInChildren<Head>().GetComponent<Rigidbody>();
		kneeLeft = GetComponentInChildren<KneeLeft>().GetComponent<Rigidbody>();
		kneeRight = GetComponentInChildren<KneeRight>().GetComponent<Rigidbody>();
		ForcePoint componentInChildren = kneeLeft.GetComponentInChildren<ForcePoint>();
		if ((bool)componentInChildren)
		{
			kneeLeftPos = componentInChildren.transform;
		}
		else
		{
			kneeLeftPos = kneeLeft.transform;
		}
		ForcePoint componentInChildren2 = kneeRight.GetComponentInChildren<ForcePoint>();
		if ((bool)componentInChildren2)
		{
			kneeRightPos = componentInChildren2.transform;
		}
		else
		{
			kneeRightPos = kneeRight.transform;
		}
	}

	public override void BatchedUpdate()
	{
		if (data.Dead)
		{
			return;
		}
		if (data.immunityForSeconds < 0f)
		{
			CheckBalance();
		}
		if (!data.isGrounded)
		{
			return;
		}
		for (int i = 0; i < balanceData.balanceInstances.Length; i++)
		{
			if (balanceData.balanceInstances[i].ID == anim.currentState)
			{
				DoBalance(balanceData.balanceInstances[i]);
			}
		}
	}

	private void CheckBalance()
	{
		if (!(kneeLeft == null) && !(kneeRight == null) && !(torso == null))
		{
			Vector3 vector = -kneeLeft.transform.up + -kneeRight.transform.up;
			if (Vector3.Angle(torso.transform.up, Vector3.up) > 120f)
			{
				Fall();
			}
			if (Vector3.Angle(vector, Vector3.down) > allowedLegAngle || Vector3.Angle(torso.transform.up, Vector3.up) > allowedTorsoAngle)
			{
				Fall();
			}
		}
	}

	public bool Fall()
	{
		if (data == null)
		{
			return false;
		}
		if (!data.canFall || data.cantFallForSeconds > 0f)
		{
			return false;
		}
		if (data.fallTime < -2f && data.immunityForSeconds <= 0f)
		{
			data.fallTime = 1f;
			return true;
		}
		return false;
	}

	private void DoBalance(BalanceDataInstance balanceInstance)
	{
		if (!timeService.IsPaused() && !(data.ragdollControl < 0.5f))
		{
			if (fixedTimeStepService.CurrentFixedTimeStep == FixedTimeStepService.FixedTimeStep.SixtyUpdates)
			{
				cappedTimeNumber = Mathf.Clamp(Time.deltaTime, 0f, 0.05f) * 100f;
			}
			else
			{
				cappedTimeNumber = Mathf.Clamp(0.0167f, 0f, 0.05f) * 100f;
			}
			Vector3 vector = (torso.worldCenterOfMass + head.worldCenterOfMass) / 2f;
			Vector3 vector2 = (kneeLeftPos.position + kneeRightPos.position) / 2f;
			Vector3 vector3 = vector - vector2;
			vector3.y = 0f;
			vector.y = 0f;
			Rigidbody rigidbody = kneeLeft;
			Transform transform = kneeLeftPos;
			Rigidbody rigidbody2 = kneeRight;
			Transform transform2 = kneeRightPos;
			if (vector3.magnitude > allowedFootDistance)
			{
				Fall();
			}
			if (Vector3.Distance(kneeRightPos.position, vector) < Vector3.Distance(kneeLeftPos.position, vector))
			{
				rigidbody = kneeRight;
				transform = kneeRightPos;
				rigidbody2 = kneeLeft;
				transform2 = kneeLeftPos;
			}
			Vector3 position = transform.position;
			Vector3 position2 = transform2.position;
			position.y = 0f;
			position2.y = 0f;
			float num = cappedTimeNumber * forceMultiplier;
			float num2 = balanceInstance.centerForce * num;
			if (transform.transform.position.y + 0.2f < hip.transform.position.y)
			{
				rigidbody.AddForceAtPosition(FixedTimeStepService.LargeForceCoefficient * num * balanceInstance.force * data.torsoAngleMultiplier * vector3, transform.position, ForceMode.Acceleration);
			}
			rigidbody.AddForceAtPosition(num2 * (vector - position), transform.position, ForceMode.Acceleration);
			rigidbody2.AddForceAtPosition(num2 * (vector - position2), transform2.position, ForceMode.Acceleration);
		}
	}
}
