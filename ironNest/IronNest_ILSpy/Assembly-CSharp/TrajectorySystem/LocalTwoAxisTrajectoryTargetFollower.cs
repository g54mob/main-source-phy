using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace TrajectorySystem;

public sealed class LocalTwoAxisTrajectoryTargetFollower : MonoBehaviour
{
	public enum TwoAxisPlane
	{
		XY,
		XZ,
		YZ
	}

	private string trajectoryTargetTag = "TrajectoryTarget";

	private TwoAxisPlane constrainPlane = TwoAxisPlane.XZ;

	private bool useLateUpdate = true;

	private bool retryAcquireWhenMissing;

	private float retryAcquireIntervalSeconds = 0.25f;

	private UnityEvent onResetRequested;

	private TrajectoryTarget claimedTarget;

	private float nextAcquireTime;

	private void OnEnable()
	{
		nextAcquireTime = 0f;
		TryAcquireNow();
	}

	private void OnDisable()
	{
		Release();
	}

	private void OnDestroy()
	{
		Release();
	}

	private void Update()
	{
		if (!useLateUpdate)
		{
			Tick();
		}
	}

	private void LateUpdate()
	{
		if (useLateUpdate)
		{
			Tick();
		}
	}

	private unsafe void Tick()
	{
		//IL_009e: Expected O, but got Ref
		//IL_022e: Expected O, but got Ref
		//IL_0116: Expected O, but got I4
		if (claimedTarget != null)
		{
			Transform transform = claimedTarget.transform;
			Transform parent = transform.parent;
			Transform transform2 = base.transform;
			Vector3 position = transform2.position;
			float num = ((!(parent != null)) ? position.x : parent.InverseTransformPoint((Vector3)(&num)).x);
			TrajectoryTarget trajectoryTarget = claimedTarget;
			float num2 = (float)trajectoryTarget.followLocalOffset + num;
			Vector3 localPosition = transform.localPosition;
			bool flag = constrainPlane == TwoAxisPlane.XY;
			if (!flag)
			{
				object obj = constrainPlane - 1;
				if (!flag)
				{
					bool flag2 = (nint)obj != 1;
					num = localPosition.x;
					if (!flag2)
					{
						float num3 = default(float);
						num = num3;
					}
				}
				else
				{
					num = num2;
				}
			}
			else
			{
				num = localPosition.x;
			}
			transform.localPosition = (Vector3)(&num);
		}
		else
		{
			if (!retryAcquireWhenMissing)
			{
				return;
			}
			float time = Time.time;
			if (!(nextAcquireTime > time))
			{
				float time2 = Time.time;
				bool flag3 = !(0.01f < retryAcquireIntervalSeconds);
				float num4 = 0.01f;
				if (!flag3)
				{
					num4 = retryAcquireIntervalSeconds;
				}
				float num5 = num4 + time2;
				nextAcquireTime = num5;
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 327 Invalid \"Jump target not found in method: 0x1804BA5F0\"");
				throw new NullReferenceException();
			}
		}
	}

	public void TryAcquireNow()
	{
		if (claimedTarget == null)
		{
			TrajectoryTarget trajectoryTarget = TrajectoryTargetRegistry.TryClaimAnyWithTag(trajectoryTargetTag, this);
			claimedTarget = trajectoryTarget;
		}
	}

	public void Release()
	{
		if (!(claimedTarget != null))
		{
			return;
		}
		TrajectoryTarget trajectoryTarget = claimedTarget;
		if (trajectoryTarget._003CIsClaimed_003Ek__BackingField)
		{
			bool flag = trajectoryTarget._003CCurrentOwner_003Ek__BackingField != this;
			if (!flag)
			{
				trajectoryTarget._003CIsClaimed_003Ek__BackingField = flag;
				trajectoryTarget._003CCurrentOwner_003Ek__BackingField = null;
				if (trajectoryTarget.OnTargetLost != null)
				{
					trajectoryTarget.OnTargetLost.Invoke();
				}
			}
		}
		claimedTarget = null;
	}

	public void InvokeResetEvent()
	{
		if (onResetRequested != null)
		{
			onResetRequested.Invoke();
		}
	}

	public void RequestResetClaimedTarget()
	{
		if (!(claimedTarget != null))
		{
			return;
		}
		TrajectoryTarget trajectoryTarget = claimedTarget;
		if (!trajectoryTarget._003CIsClaimed_003Ek__BackingField)
		{
			trajectoryTarget.isResetting = true;
			if (trajectoryTarget.OnResetRequested != null)
			{
				trajectoryTarget.OnResetRequested.Invoke();
			}
		}
	}

	private unsafe void DriveClaimedTargetLocal2Axis()
	{
		//IL_007b: Expected O, but got Ref
		//IL_0176: Expected O, but got Ref
		//IL_00f3: Expected O, but got I4
		Transform transform = claimedTarget.transform;
		Transform parent = transform.parent;
		Transform transform2 = base.transform;
		Vector3 position = transform2.position;
		float num = ((!(parent != null)) ? position.x : parent.InverseTransformPoint((Vector3)(&num)).x);
		TrajectoryTarget trajectoryTarget = claimedTarget;
		float num2 = (float)trajectoryTarget.followLocalOffset + num;
		Vector3 localPosition = transform.localPosition;
		bool flag = constrainPlane == TwoAxisPlane.XY;
		if (!flag)
		{
			object obj = constrainPlane - 1;
			if (!flag)
			{
				bool flag2 = (nint)obj != 1;
				num = localPosition.x;
				if (!flag2)
				{
					float num3 = default(float);
					num = num3;
				}
			}
			else
			{
				num = num2;
			}
		}
		else
		{
			num = localPosition.x;
		}
		transform.localPosition = (Vector3)(&num);
	}

	public LocalTwoAxisTrajectoryTargetFollower()
	{
		UnityEvent unityEvent = new UnityEvent();
		onResetRequested = unityEvent;
		base._002Ector();
	}
}
