using UnityEngine;

public class VRHeadsetInterruptHandler
{
	private enum HeadsetTrackingState
	{
		ProperlyTracking = 0,
		PotentialInterruptDetected = 1,
		InterruptDetected = 2,
		ResetRequested = 3
	}

	private readonly VR vr;

	private HeadsetTrackingState headsetTrackingState;

	private Vector3 headsetPositionLastFrame;

	private Vector3 validPositionOnInterrupt;

	private float potentialInterruptTime;

	private float resetRequestTime;

	public VRHeadsetInterruptHandler(VR vr)
	{
		this.vr = vr;
	}

	public void update(Vector3 currentValidPosition)
	{
		if (vr.getActiveState() != VR.State.VR)
		{
			return;
		}
		Vector3 position = vr.rig.headCamera.transform.position;
		float magnitude = (position - headsetPositionLastFrame).magnitude;
		headsetPositionLastFrame = position;
		switch (headsetTrackingState)
		{
		case HeadsetTrackingState.ProperlyTracking:
			if (magnitude == 0f)
			{
				headsetTrackingState = HeadsetTrackingState.PotentialInterruptDetected;
				potentialInterruptTime = Time.time;
			}
			break;
		case HeadsetTrackingState.PotentialInterruptDetected:
			if (magnitude > 0f)
			{
				headsetTrackingState = HeadsetTrackingState.ProperlyTracking;
			}
			else if (Time.time - potentialInterruptTime > 1f)
			{
				headsetTrackingState = HeadsetTrackingState.InterruptDetected;
				validPositionOnInterrupt = currentValidPosition;
			}
			break;
		case HeadsetTrackingState.InterruptDetected:
			if (magnitude > 0f)
			{
				headsetTrackingState = HeadsetTrackingState.ResetRequested;
				resetRequestTime = Time.time;
			}
			break;
		case HeadsetTrackingState.ResetRequested:
			if (Time.time - resetRequestTime > 1f)
			{
				vr.setRigPosition(validPositionOnInterrupt);
				headsetTrackingState = HeadsetTrackingState.ProperlyTracking;
			}
			break;
		default:
			Debug.LogError($"Invalid state for headset tracking state: {headsetTrackingState}");
			break;
		}
	}
}
