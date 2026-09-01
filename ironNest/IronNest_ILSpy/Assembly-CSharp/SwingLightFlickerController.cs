using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public sealed class SwingLightFlickerController : MonoBehaviour
{
	private static readonly List<SwingLightFlicker> Lights;

	private DieselEngineController linkedEngine;

	private bool forceAllOffNow;

	private bool restorePowerAllNow;

	private bool togglePowerAllNow;

	private bool startPoweredOn = true;

	private bool restoreUsesSequence;

	private UnityEvent<bool> onMasterPowerChanged;

	private UnityEvent onPowerOn;

	private UnityEvent onPowerOff;

	private bool _hasPower;

	private bool _switchOn;

	private bool _masterPowerOn;

	private bool _lastEngineRunning;

	private void Start()
	{
		if (Application.isPlaying)
		{
			bool switchOn;
			if (!(linkedEngine != null))
			{
				switchOn = startPoweredOn;
				_hasPower = true;
			}
			else
			{
				DieselEngineController dieselEngineController = linkedEngine;
				_hasPower = dieselEngineController._003CEnginesRunning_003Ek__BackingField;
				DieselEngineController dieselEngineController2 = linkedEngine;
				_lastEngineRunning = dieselEngineController2._003CEnginesRunning_003Ek__BackingField;
				switchOn = true;
			}
			_switchOn = switchOn;
			ApplyEffectivePower(restoreUsesSequence);
		}
	}

	private void Update()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (linkedEngine != null)
		{
			DieselEngineController dieselEngineController = linkedEngine;
			if (dieselEngineController._003CEnginesRunning_003Ek__BackingField != _lastEngineRunning)
			{
				_lastEngineRunning = dieselEngineController._003CEnginesRunning_003Ek__BackingField;
				bool flag = (byte)(~(dieselEngineController._003CEnginesRunning_003Ek__BackingField ? 1u : 0u)) != 0;
				_hasPower = dieselEngineController._003CEnginesRunning_003Ek__BackingField;
				bool flag2 = false;
				if (!flag)
				{
					flag2 = restoreUsesSequence;
				}
				bool flag3 = !flag2;
				bool playRestoreSequence = !flag3;
				ApplyEffectivePower(playRestoreSequence);
			}
		}
		if (forceAllOffNow)
		{
			_switchOn = false;
			ApplyEffectivePower(playRestoreSequence: false);
			forceAllOffNow = false;
		}
		if (restorePowerAllNow)
		{
			_switchOn = true;
			ApplyEffectivePower(restoreUsesSequence);
			restorePowerAllNow = false;
		}
		if (togglePowerAllNow)
		{
			bool switchOn = !_switchOn;
			bool flag4 = false;
			if (!_switchOn)
			{
				flag4 = restoreUsesSequence;
			}
			bool flag5 = !flag4;
			_switchOn = switchOn;
			bool playRestoreSequence2 = !flag5;
			ApplyEffectivePower(playRestoreSequence2);
			togglePowerAllNow = false;
		}
	}

	public void PowerOnAll()
	{
		_switchOn = true;
		ApplyEffectivePower(restoreUsesSequence);
	}

	public void PowerOffAll()
	{
		_switchOn = false;
		ApplyEffectivePower(playRestoreSequence: false);
	}

	public void TogglePowerAll()
	{
		bool switchOn = !_switchOn;
		bool flag = ~(_switchOn ? 1u : 0u) != 0 && restoreUsesSequence;
		bool flag2 = !flag;
		_switchOn = switchOn;
		bool playRestoreSequence = !flag2;
		ApplyEffectivePower(playRestoreSequence);
	}

	public void SetPowerAll(bool powerOn)
	{
		bool flag = powerOn && restoreUsesSequence;
		bool flag2 = !flag;
		_switchOn = powerOn;
		bool playRestoreSequence = !flag2;
		ApplyEffectivePower(playRestoreSequence);
	}

	public void SetMasterPower(bool powerOn, bool playRestoreSequence)
	{
		_switchOn = powerOn;
		ApplyEffectivePower(playRestoreSequence);
	}

	private void SetSwitch(bool switchOn, bool playRestoreSequence)
	{
		_switchOn = switchOn;
		ApplyEffectivePower(playRestoreSequence);
	}

	private unsafe void ApplyEffectivePower(bool playRestoreSequence)
	{
		bool masterPowerOn;
		if (!_hasPower)
		{
			masterPowerOn = false;
		}
		else
		{
			bool flag = !_switchOn;
			masterPowerOn = !flag;
			if (~(_switchOn ? 1u : 0u) == 0 && _masterPowerOn)
			{
				return;
			}
		}
		_masterPowerOn = masterPowerOn;
		Object obj = default(Object);
		onMasterPowerChanged.Invoke((byte)(&obj) != 0);
		UnityEvent unityEvent = (_masterPowerOn ? onPowerOn : onPowerOff);
		unityEvent.Invoke();
		List<SwingLightFlicker> lights = Lights;
		bool flag2 = (nint)Lights < 0;
		int num = lights._size - 1;
		if (flag2)
		{
			return;
		}
		bool flag3;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (!(obj == null))
			{
				flag3 = (nint)obj < 0;
				((SwingLightFlicker)obj).SetMasterPowerState(_masterPowerOn, playRestoreSequence);
			}
			else
			{
				flag3 = (nint)Lights < 0;
				Lights.RemoveAt(num);
			}
			num--;
		}
		while (!flag3);
	}

	public static void Register(SwingLightFlicker light)
	{
		if (light != null && !Lights.Contains(light))
		{
			Lights.Add(light);
		}
	}

	public static void Unregister(SwingLightFlicker light)
	{
		if (light != null)
		{
			bool flag = Lights.Remove(light);
		}
	}

	public SwingLightFlickerController()
	{
		UnityEvent<bool> unityEvent = new UnityEvent<bool>();
		onMasterPowerChanged = unityEvent;
		onPowerOn = new UnityEvent();
		onPowerOff = new UnityEvent();
		base._002Ector();
	}

	static SwingLightFlickerController()
	{
		List<SwingLightFlicker> lights = new List<SwingLightFlicker>(256);
		Lights = lights;
	}
}
