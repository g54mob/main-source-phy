using Landfall.MonoBatch;
using TFBGames;
using UnityEngine;

public class RotationHandler : BatchedMonobehaviour
{
	public float rotationForce;

	public bool ignoreY;

	public bool lockYToZero;

	public bool rotateTowardsMovement;

	[HideInInspector]
	public float rotationMultiplier = 1f;

	private Rigidbody torso;

	private Rigidbody hip;

	private DataHandler data;

	protected override void Start()
	{
		base.Start();
		data = GetComponent<DataHandler>();
		torso = GetComponentInChildren<Torso>().GetComponent<Rigidbody>();
		hip = GetComponentInChildren<Hip>().GetComponent<Rigidbody>();
	}

	public override void BatchedFixedUpdate()
	{
		if (!data.Dead && data.isGrounded && base.enabled)
		{
			Vector3 forward = hip.transform.forward;
			Vector3 forward2 = torso.transform.forward;
			Vector3 forward3 = data.characterForwardObject.forward;
			if (ignoreY)
			{
				forward.y = 0f;
				forward2.y = 0f;
				forward3.y = 0f;
			}
			if (lockYToZero)
			{
				forward3.y = 0f;
			}
			if (rotateTowardsMovement)
			{
				forward3 = data.groundedMovementDirectionObject.forward;
			}
			float num = (0f - rotationMultiplier) * data.muscleControl * rotationForce * data.torsoAngleMultiplier;
			float num2 = Mathf.Clamp(Vector3.Angle(forward3, forward), 0f, 15f) * num;
			float num3 = Mathf.Clamp(Vector3.Angle(forward3, forward2), 0f, 15f) * num;
			hip.AddTorque(FixedTimeStepService.TorqueCoefficient * num2 * Vector3.Cross(forward3, forward).normalized, ForceMode.Acceleration);
			torso.AddTorque(FixedTimeStepService.TorqueCoefficient * num3 * Vector3.Cross(forward3, forward2).normalized, ForceMode.Acceleration);
		}
	}
}
