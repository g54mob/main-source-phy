using DarkTonic.MasterAudio;
using UnityEngine;

public class VehicleAudio : MonoBehaviour
{
	[SoundGroup]
	public string StartSound = "[None]";

	[SoundGroup]
	public string EngineLoop = "[None]";

	[SoundGroup]
	public string StopSound = "[None]";

	[SoundGroup]
	public string SirenLoop = "[None]";

	[Range(0f, 5f)]
	public float EngineLoopDelay;

	[Range(0.1f, 5f)]
	public float MinPitch = 1f;

	[Range(0.1f, 5f)]
	public float MaxPitch = 1.5f;

	[Range(-20f, 0f)]
	public float MinGain = -5f;

	[Range(0f, 20f)]
	public float MaxGain = 10f;

	[Range(0f, 1f)]
	public float FrictionVolume = 1f;

	public float MaxRPM;

	private SoundGroupVariation _engineLoop;

	private SoundGroupVariation _frictionLoopN;

	private SoundGroupVariation _frictionLoopS;

	private SoundGroupVariation _sirenLoop;

	private bool _started;

	private Vehicle _vehicle;

	private MFilter _engineFilter;

	private float _engineStartTime;

	public static float S_AddedPitch;

	private void LateUpdate()
	{
		float num = Time.realtimeSinceStartup - _engineStartTime;
		if (_started && _engineLoop == null && num > EngineLoopDelay)
		{
			PlayEngineLoops();
		}
		if (_engineLoop != null)
		{
			UpdateEngineSound();
		}
	}

	private void PlayEngineLoops()
	{
		_engineLoop = MasterAudio.PlaySound3DFollowTransform(EngineLoop, _vehicle.transform)?.ActingVariation;
		if (_engineLoop != null)
		{
			_engineFilter = _engineLoop.VarAudio.gameObject.AddComponent<MFilter>();
			_engineFilter.Frequency = 400.0;
		}
		if (_frictionLoopN == null)
		{
			_frictionLoopN = MasterAudio.PlaySound3DFollowTransform("sfx_vehicle_friction_normal_concrete_lp", _vehicle.transform, 0f)?.ActingVariation;
		}
		if (_frictionLoopS == null)
		{
			_frictionLoopS = MasterAudio.PlaySound3DFollowTransform("sfx_vehicle_friction_struggle_concrete_lp", _vehicle.transform, 0f)?.ActingVariation;
		}
	}

	private void OnDestroy()
	{
		StopEngineSound(playStopSound: false, stopImmediately: true);
	}

	public void StartEngineSound(Vehicle vehicle)
	{
		if (GameStateManager.GetState() != GameState.MAIN_MENU)
		{
			_vehicle = vehicle;
			if (MinPitch > MaxPitch)
			{
				Debug.LogWarning(base.gameObject.name + " VehicleAudio's MinPitch is larger than MaxPitch, please check!");
				MaxPitch = MinPitch + 0.1f;
			}
			if (!_started)
			{
				_started = MasterAudio.PlaySound3DAtTransformAndForget(StartSound, vehicle.transform);
			}
			if (_started && EngineLoopDelay > 0f)
			{
				_engineStartTime = Time.realtimeSinceStartup;
			}
			else
			{
				_engineStartTime = float.MaxValue;
				PlayEngineLoops();
			}
			if (MaxRPM <= 0f)
			{
				MaxRPM = _vehicle.Physics.topSpeed * 500f;
			}
		}
	}

	public void StopEngineSound(bool playStopSound = false, bool stopImmediately = false)
	{
		float num = ((Time.timeScale == 0f) ? 1f : Time.timeScale);
		if (_engineLoop != null)
		{
			if (stopImmediately)
			{
				_engineLoop.Stop();
			}
			else
			{
				_engineLoop.FadeOutNowAndStop(_engineLoop.fadeOutTime / num);
			}
		}
		if (_engineFilter != null)
		{
			Object.Destroy(_engineFilter);
		}
		if (_frictionLoopN != null)
		{
			if (stopImmediately)
			{
				_frictionLoopN.Stop();
			}
			else
			{
				_frictionLoopN.FadeOutNowAndStop(_frictionLoopN.fadeOutTime / num);
			}
		}
		if (_frictionLoopS != null)
		{
			if (stopImmediately)
			{
				_frictionLoopS.Stop();
			}
			else
			{
				_frictionLoopS.FadeOutNowAndStop(_frictionLoopS.fadeOutTime / num);
			}
		}
		if (_sirenLoop != null)
		{
			if (stopImmediately)
			{
				_sirenLoop.Stop();
			}
			else
			{
				_sirenLoop.FadeOutNowAndStop(_sirenLoop.fadeOutTime / num);
			}
		}
		if (playStopSound && _vehicle != null)
		{
			MasterAudio.PlaySound3DAtTransformAndForget(StopSound, _vehicle.transform);
		}
		_started = false;
		_engineLoop = null;
		_frictionLoopN = null;
		_frictionLoopS = null;
		_vehicle = null;
		_sirenLoop = null;
	}

	public void UpdateEngineSound()
	{
		if (!(_vehicle == null) && !(_engineLoop == null) && !(_frictionLoopN == null) && !(_frictionLoopS == null))
		{
			AudioSource varAudio = _engineLoop.VarAudio;
			AudioSource varAudio2 = _frictionLoopN.VarAudio;
			AudioSource varAudio3 = _frictionLoopS.VarAudio;
			if ((bool)_sirenLoop)
			{
				_ = _sirenLoop.VarAudio;
			}
			float num = 0.99f;
			float t = 1f - Mathf.Pow(1f - num, Time.deltaTime);
			float currentEngineRpm = _vehicle.Physics.currentEngineRpm;
			float currentEngineTorqueFraction = _vehicle.Physics.currentEngineTorqueFraction;
			float speed = _vehicle.Speed;
			float num2 = float.MaxValue;
			if (!Mathf.Approximately(speed, 0f))
			{
				num2 = currentEngineTorqueFraction / speed;
			}
			float num3 = Mathf.Clamp01(currentEngineRpm / MaxRPM) * (MaxPitch - MinPitch) + MinPitch + S_AddedPitch;
			if (num3 < 0.0001f)
			{
				num3 = 0f;
			}
			varAudio.pitch = Mathf.Clamp(Mathf.Lerp(varAudio.pitch, num3, t), 0f, float.MaxValue);
			float b = Mathf.Clamp01(currentEngineTorqueFraction / 1f) * (MaxGain - MinGain) + MinGain;
			_engineFilter.Gain = Mathf.Lerp((float)_engineFilter.Gain, b, t);
			float b2 = Mathf.Clamp01(currentEngineRpm / MaxRPM) * FrictionVolume;
			if (currentEngineRpm > 500f && currentEngineTorqueFraction < 0.1f && speed < 1f)
			{
				b2 = 0f;
			}
			varAudio2.volume = Mathf.Lerp(varAudio2.volume, b2, t);
			float b3 = Mathf.Clamp01(num2 / 1.5f) * FrictionVolume;
			varAudio3.volume = Mathf.Lerp(varAudio3.volume, b3, t);
		}
	}
}
