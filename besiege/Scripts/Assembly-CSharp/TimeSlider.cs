using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlider
{
	public static TimeSlider Instance;

	public float deltaTime;

	public float time;

	public Action<float> onScaleChanged;

	public float percentagey;

	public float percSendInterval = 0.1f;

	public bool paused;

	public bool wasSimulating;

	public bool startingSimulation;

	protected float timeAtCurrentFrame;

	protected float timeAtLastFrame;

	protected float _delegateTimeScale = 1f;

	protected float timeSinceSimulate;

	protected float lastTimeScale = 1f;

	private float lastSentPercentage;

	private float lastSentAutoPercentage;

	private float lastSentTime;

	private bool autoModeActive = true;

	private float lerpDelta = 0.015f;

	private float duration = 0.5f;

	private Queue<float> frameQueue = new Queue<float>();

	private float queueTime;

	private int currentLock = -1;

	public float delegateTimeScale
	{
		get
		{
			return _delegateTimeScale;
		}
		set
		{
			SetDelegateTimeScale(value);
		}
	}

	public TimeSlider(float defaultScale)
	{
		Instance = this;
		percentagey = defaultScale;
		lastSentTime = Time.time;
		ResetScale(percentagey);
		startingSimulation = false;
		timeAtLastFrame = Time.realtimeSinceStartup;
	}

	private void SetDelegateTimeScale(float val)
	{
		float num = _delegateTimeScale;
		_delegateTimeScale = val;
		if (StatMaster.levelSimulating && AddPiece.canSimulate && StatMaster.useSmartInterpolation)
		{
			if (num >= 0.6f && _delegateTimeScale < 0.6f)
			{
				ReferenceMaster.SetInterpolationForAllRigidbodies(RigidbodyInterpolation.Interpolate);
			}
			else if (num < 0.6f && _delegateTimeScale >= 0.6f)
			{
				ReferenceMaster.SetInterpolationForAllRigidbodies(RigidbodyInterpolation.None);
			}
		}
	}

	public float DeltaTime()
	{
		return deltaTime * Instance.delegateTimeScale;
	}

	public void SendTimeScale(bool isAuto)
	{
		if (!StatMaster.isMP)
		{
			return;
		}
		NetworkAddPiece instance = NetworkAddPiece.Instance;
		NetworkAuxAddPiece instance2 = NetworkAuxAddPiece.Instance;
		if (!isAuto)
		{
			if (lastSentPercentage != percentagey)
			{
				if (StatMaster.isHosting && StatMaster.Mode.LevelEditor.clientGlobalSim)
				{
					instance.SaveTimeScale(percentagey, false);
					instance2.SendNetworkMessage(RPCMessageType.TimeScale, instance.GetTimeScale(percentagey));
				}
				else
				{
					instance.lastLocalTimeScale = percentagey;
				}
				lastSentPercentage = (lastSentAutoPercentage = percentagey);
			}
		}
		else if (StatMaster.isHosting && lastSentAutoPercentage != percentagey)
		{
			instance.SaveTimeScale(percentagey, true);
			instance2.SendNetworkMessage(RPCMessageType.AutoTimeScale, instance.GetTimeScale(percentagey));
			lastSentAutoPercentage = percentagey;
		}
		lastSentTime = 0f;
	}

	public void ResetScale(float defaultTimeScale)
	{
		SetPercentage(defaultTimeScale);
		lastSentPercentage = (lastSentAutoPercentage = defaultTimeScale);
	}

	public void Update()
	{
		if (startingSimulation)
		{
			GetDeltaTime();
			return;
		}
		if (OptionsMaster.BesiegeConfig.AutoTimeScale && (((!StatMaster.isMP || StatMaster.isHosting || StatMaster.IsLevelEditorOnly) && StatMaster.levelSimulating) || StatMaster.isLocalSim))
		{
			AutoAssignTimescale();
		}
		if (StatMaster.levelSimulating && AddPiece.canSimulate)
		{
			timeSinceSimulate += deltaTime;
			if (lastTimeScale != delegateTimeScale || !wasSimulating)
			{
				Time.timeScale = delegateTimeScale;
				lastTimeScale = delegateTimeScale;
			}
			wasSimulating = true;
		}
		else if (wasSimulating)
		{
			Time.timeScale = 1f;
			timeSinceSimulate = 0f;
			wasSimulating = false;
		}
		GetDeltaTime();
		time += deltaTime;
		if (StatMaster.isMP && StatMaster.isHosting)
		{
			if (lastSentTime > percSendInterval)
			{
				SendTimeScale(autoModeActive);
			}
			lastSentTime += deltaTime;
		}
	}

	public void SetAuto(bool b)
	{
		if (!b)
		{
			ResetRollingDelta();
		}
	}

	public void AutoAssignTimescale()
	{
		float b = RollingDelta();
		BesiegeConfig besiegeConfig = OptionsMaster.BesiegeConfig;
		lerpDelta = Mathf.Lerp(lerpDelta, b, Time.unscaledDeltaTime);
		float lockDelta = FrameRate.GetLockDelta(besiegeConfig);
		float min = Mathf.Lerp(0.0333333f, 0.016f, (Time.timeScale - 0.1f) * 1.12f);
		lockDelta = Mathf.Clamp(lockDelta, min, 0.5f);
		float num = lockDelta - lerpDelta;
		float num2 = lockDelta * 0.05f;
		float num3 = besiegeConfig.MaxTimeScale * 0.01f;
		if (delegateTimeScale > num3)
		{
			num = 0f - num2 - 0.1f;
		}
		if (num < 0f - num2)
		{
			float num4 = besiegeConfig.MinTimeScale * 0.01f;
			if (delegateTimeScale + num * 0.5f > num4)
			{
				TimeSliderView.Instance.ChangePercentage(num * 0.5f);
			}
			else
			{
				TimeSliderView.Instance.SetPercentage(num4);
			}
		}
		else if (num > 0f)
		{
			ChangePct(num * 0.5f, num3);
		}
	}

	protected void ChangePct(float inc, float max)
	{
		if (delegateTimeScale < max)
		{
			if (delegateTimeScale + inc > max)
			{
				TimeSliderView.Instance.SetPercentage(max);
			}
			else
			{
				TimeSliderView.Instance.ChangePercentage(inc);
			}
		}
	}

	public void SetPercentage(float percent)
	{
		percentagey = percent;
		delegateTimeScale = percentagey * 2f;
		if (onScaleChanged != null)
		{
			onScaleChanged(percent);
		}
	}

	private void GetDeltaTime()
	{
		timeAtCurrentFrame = Time.realtimeSinceStartup;
		deltaTime = timeAtCurrentFrame - timeAtLastFrame;
		timeAtLastFrame = timeAtCurrentFrame;
	}

	protected float RollingDelta()
	{
		int fPSLock = OptionsMaster.GetFPSLock();
		if (fPSLock != currentLock)
		{
			currentLock = fPSLock;
			ResetRollingDelta();
		}
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		frameQueue.Enqueue(unscaledDeltaTime);
		queueTime += unscaledDeltaTime;
		if (queueTime > duration)
		{
			queueTime -= frameQueue.Dequeue();
		}
		if (frameQueue.Count == 0)
		{
			return Time.unscaledDeltaTime;
		}
		return queueTime / (1f * (float)frameQueue.Count);
	}

	public void ResetRollingDelta()
	{
		frameQueue.Clear();
		queueTime = 0f;
	}
}
