using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class CarDemoManager : DemoManager
{
	public MMKnob Knob;

	public float MinimumKnobValue = 0.1f;

	public float MaximumPowerDuration;

	public float ChargingSpeed;

	public float CarSpeed;

	public float Power;

	public float StartClickDuration;

	public float DentDuration;

	public List<float> Dents;

	public AudioSource CarEngineAudioSource;

	public Transform LeftWheel;

	public Transform RightWheel;

	public RectTransform CarBody;

	public Vector3 WheelRotationSpeed;

	public GameObject ReloadingPrompt;

	public AnimationCurve StartClickCurve;

	public MMProgressBar PowerBar;

	public List<PowerBarElement> SpeedBars;

	public Color ActiveColor;

	public Color InactiveColor;

	public bool _carStarted;

	public float _carStartedAt;

	public float _lastStartClickAt;

	protected float _knobValueLastFrame;

	protected float _lastDentAt;

	protected float _knobValue;

	protected Vector3 _initialCarPosition;

	protected Vector3 _carPosition;

	protected virtual void Awake()
	{
		//IL_0044: Expected O, but got F4
		Power = MaximumPowerDuration;
		ReloadingPrompt.SetActive(value: false);
		Vector3 localPosition = CarBody.localPosition;
		_initialCarPosition = (Vector3)localPosition.x;
		_ = localPosition.z;
	}

	protected virtual void Update()
	{
		HandlePower();
		UpdateCar();
		UpdateUI();
		MMKnob knob = Knob;
		_knobValueLastFrame = knob.Value;
	}

	protected unsafe virtual void HandlePower()
	{
		//IL_0042: Expected F4, but got I4
		//IL_0323: Invalid comparison between I4 and F4
		//IL_0370: Expected F4, but got I4
		//IL_00e5: Invalid comparison between I4 and F4
		//IL_0132: Expected F4, but got I4
		//IL_0226: Expected O, but got Ref
		//IL_03a9: Expected O, but got Ref
		//IL_015d: Invalid comparison between I4 and F4
		//IL_01b7: Expected O, but got Ref
		MMKnob knob = Knob;
		float num = (_knobValue = ((!knob.Active) ? 0f : knob.Value));
		Vector3 vector = default(Vector3);
		GameObject reloadingPrompt;
		bool active;
		if (_carStarted)
		{
			float time = Time.time;
			float num2 = time - _carStartedAt;
			if (!(num2 > MaximumPowerDuration))
			{
				if (!(_knobValue > MinimumKnobValue))
				{
					_carStarted = false;
					float time2 = Time.time;
					_lastStartClickAt = time2;
				}
				else
				{
					float deltaTime = Time.deltaTime;
					float num3 = Power - deltaTime;
					if (!(0f > num3))
					{
						if (num3 > MaximumPowerDuration)
						{
							num3 = MaximumPowerDuration;
						}
					}
					else
					{
						num3 = 0f;
					}
					Power = num3;
					HapticController.clipLevel = _knobValue;
					HapticController.clipFrequencyShift = _knobValue;
					if (0f < Power)
					{
						return;
					}
					_carStarted = false;
					Knob.SetActive(status: false);
					MMKnob knob2 = Knob;
					knob2._rectTransform.localScale = (Vector3)(&vector);
					ReloadingPrompt.SetActive(value: true);
				}
				HapticController.Stop();
				return;
			}
			_carStarted = false;
			Knob.SetActive(status: false);
			MMKnob knob3 = Knob;
			knob3._rectTransform.localScale = (Vector3)(&vector);
			reloadingPrompt = ReloadingPrompt;
			active = true;
		}
		else
		{
			if (num > MinimumKnobValue && knob.Active)
			{
				_carStarted = true;
				float time3 = Time.time;
				_carStartedAt = time3;
				float time4 = Time.time;
				_lastStartClickAt = time4;
				HapticPatterns.PlayConstant(_knobValue, _knobValue, MaximumPowerDuration);
				CarEngineAudioSource.Play();
				return;
			}
			float deltaTime2 = Time.deltaTime;
			float num4 = deltaTime2 * ChargingSpeed;
			float num5 = num4 + Power;
			if (!(0f > num5))
			{
				if (num5 > MaximumPowerDuration)
				{
					num5 = MaximumPowerDuration;
				}
			}
			else
			{
				num5 = 0f;
			}
			MMKnob knob4 = Knob;
			Power = num5;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp near ptr 0000000180A83321h\"");
			if (num5 != MaximumPowerDuration)
			{
				if (!knob4.Active)
				{
					knob4.SetValue(CarSpeed);
				}
				return;
			}
			knob4.SetActive(status: true);
			MMKnob knob5 = Knob;
			knob5._rectTransform.localScale = (Vector3)(&vector);
			reloadingPrompt = ReloadingPrompt;
			active = false;
		}
		reloadingPrompt.SetActive(active);
	}

	protected unsafe virtual void UpdateCar()
	{
		//IL_007c: Expected O, but got I4
		//IL_0085: Expected F4, but got I4
		//IL_022d: Invalid comparison between O and F4
		//IL_00c1: Expected F4, but got I4
		//IL_006e: Expected O, but got I4
		//IL_0142: Expected O, but got Ref
		//IL_016a: Expected O, but got Ref
		//IL_021b: Expected O, but got Ref
		float num3;
		object obj;
		if (_carStarted)
		{
			MMKnob knob = Knob;
			float num = knob.Value - MinimumKnobValue;
			float num2 = 1f - MinimumKnobValue;
			num3 = num / num2;
			obj = 0;
		}
		else
		{
			obj = 0;
			num3 = 0f;
		}
		float num4 = Time.deltaTime;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4))
		{
			if (num4 > 1f)
			{
				num4 = 1f;
			}
		}
		else
		{
			num4 = 0f;
		}
		float num5 = num3 - CarSpeed;
		float num6 = num5 * num4;
		float volume = (CarSpeed = num6 + CarSpeed);
		CarEngineAudioSource.volume = volume;
		float num7 = CarSpeed - (float)obj;
		float num8 = num7 * 0.75f;
		float pitch = num8 + 0.5f;
		CarEngineAudioSource.pitch = pitch;
		float deltaTime = Time.deltaTime;
		Vector3 vector = default(Vector3);
		LeftWheel.Rotate((Vector3)(&vector), Space.Self);
		float deltaTime2 = Time.deltaTime;
		RightWheel.Rotate((Vector3)(&vector), Space.Self);
		Vector3 carPosition = (Vector3)((object)_initialCarPosition + obj);
		_carPosition = carPosition;
		float time = Time.time;
		float y = CarSpeed * 10f;
		float x = time * 10f;
		float num9 = Mathf.PerlinNoise(x, y);
		float num10 = CarSpeed * 10f;
		float num11 = num9 * num10;
		float num12 = num11;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.CarDemoManager)+F4]");
		float num13 = num12 + 0f;
		_ = 0;
		CarBody.localPosition = (Vector3)(&vector);
	}

	protected unsafe virtual void UpdateUI()
	{
		//IL_038b: Expected O, but got I4
		//IL_0394: Expected F4, but got I4
		//IL_01a5: Expected F4, but got I4
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Expected O, but got Unknown
		//IL_059e: Expected O, but got I4
		//IL_05a6: Expected O, but got Ref
		//IL_01d7: Invalid comparison between F4 and O
		//IL_0213: Invalid comparison between O and F4
		//IL_00f1: Expected O, but got Ref
		//IL_0489: Expected O, but got I4
		//IL_01f5: Invalid comparison between O and F4
		//IL_0109: Invalid comparison between I4 and F4
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Expected O, but got Unknown
		//IL_0464: Expected O, but got I4
		//IL_0232: Invalid comparison between F4 and O
		//IL_015c: Expected F4, but got I4
		//IL_0173: Expected O, but got Ref
		//IL_0185: Expected F4, but got I4
		//IL_02f7: Expected O, but got Ref
		//IL_0367: Expected O, but got Ref
		//IL_0370: Expected O, but got I4
		MMKnob knob = Knob;
		float num12;
		object obj = default(object);
		object obj2;
		List<float>.Enumerator enumerator3 = default(List<float>.Enumerator);
		if (knob.Active)
		{
			float time = Time.time;
			float num = time - _lastStartClickAt;
			float num5 = default(float);
			float num11;
			float num13;
			if (StartClickDuration > num)
			{
				float time2 = Time.time;
				float num2 = time2 - _lastStartClickAt;
				float num3 = 1f / StartClickDuration;
				float time3 = num2 * num3;
				float num4 = StartClickCurve.Evaluate(time3);
				MMKnob knob2 = Knob;
				knob2._rectTransform.localScale = (Vector3)(&num5);
				MMKnob knob3 = Knob;
				float num6;
				if (!(0f > num4))
				{
					bool flag = !(num4 > 1f);
					num6 = num4;
					if (!flag)
					{
						num6 = 1f;
					}
				}
				else
				{
					num6 = 0f;
				}
				float num8 = default(float);
				float num7 = 1f - num8;
				float num9 = num7 * num6;
				float num10 = num9 + num8;
				List<float>.Enumerator enumerator = default(List<float>.Enumerator);
				knob3._image.color = (Color)(&enumerator);
				num11 = 0.05f;
				num12 = 0f;
				num13 = 1f;
			}
			else
			{
				num11 = 0.05f;
				num12 = 0f;
				num13 = 1f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<float>.Enumerator enumerator2 = default(List<float>.Enumerator);
			while (true)
			{
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					float knobValue = _knobValue;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)knobValue) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) || System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)_knobValueLastFrame))
					{
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)_knobValue))
						{
							continue;
						}
						float knobValueLastFrame = _knobValueLastFrame;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)knobValueLastFrame) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
						{
							continue;
						}
					}
					float time4 = Time.time;
					_lastDentAt = time4;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
				}
				else
				{
					enumerator2.Dispose();
				}
				break;
			}
			float time5 = Time.time;
			float num14 = time5 - _lastDentAt;
			bool flag2 = !(DentDuration > num14);
			obj2 = 0;
			enumerator3 = (List<float>.Enumerator)(&enumerator2);
			if (!flag2)
			{
				float time6 = Time.time;
				float num15 = time6 - _lastDentAt;
				float num16 = num13 / DentDuration;
				float time7 = num15 * num16;
				float num17 = StartClickCurve.Evaluate(time7);
				MMKnob knob4 = Knob;
				knob4._rectTransform.localScale = (Vector3)(&num5);
				MMKnob knob5 = Knob;
				float num18 = num17 * num11;
				if (num12 > num18 || num18 > num13)
				{
				}
				float num19 = default(float);
				knob5._image.color = (Color)(&num19);
				obj2 = 0;
				enumerator3 = (List<float>.Enumerator)knob4._rectTransform;
			}
		}
		else
		{
			obj2 = 0;
			num12 = 0f;
		}
		PowerBar.UpdateBar(Power, num12, MaximumPowerDuration);
		List<PowerBarElement> speedBars = SpeedBars;
		object obj6 = default(object);
		if (0.1f < CarSpeed)
		{
			float num20 = CarSpeed * 5f;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si ebx,xmm0\"");
			object obj3 = obj2;
			bool flag3;
			do
			{
				if ((nint)obj3 < speedBars._size)
				{
					object obj5;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<List<float>.Enumerator, UIntPtr>(ref enumerator3))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						object obj4 = 0;
						obj5 = obj6;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						object obj4 = 1;
						obj5 = obj;
					}
					object obj7 = obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1008 @ r8_v10+188] (should have been resolved before IL gen)");
					obj2++;
					speedBars = SpeedBars;
					flag3 = SpeedBars != null;
					obj3 = obj2;
					continue;
				}
				return;
			}
			while (flag3);
			throw new NullReferenceException();
		}
		object obj8 = obj2;
		while ((nint)obj8 < speedBars._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj9 = obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v918 @ r8_v6+188] (should have been resolved before IL gen)");
			obj2++;
			speedBars = SpeedBars;
			obj8 = obj2;
		}
	}

	public CarDemoManager()
	{
		Vector3 wheelRotationSpeed = default(Vector3);
		WheelRotationSpeed = wheelRotationSpeed;
		_ = 50f;
		MaximumPowerDuration = 10f;
		ChargingSpeed = 2f;
		StartClickDuration = 0.2f;
		DentDuration = 0.1f;
		((MonoBehaviour)this)._002Ector();
	}
}
