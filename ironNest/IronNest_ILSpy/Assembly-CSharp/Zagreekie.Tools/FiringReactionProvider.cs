using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Zagreekie.Tools;

public sealed class FiringReactionProvider : MonoBehaviour
{
	private ArmedFireRelayOneShot _relay;

	private SliderEnergyMomentumSpinner _spinner;

	private GunController _gunLeft;

	private GunController _gunRight;

	private float _valueOnArmed = 0.15f;

	private float _valueAtFullSpinner = 0.25f;

	private bool _invertOutput;

	private float _armedRampUpTau = 1f;

	private float _disarmReleaseTau = 1.5f;

	private AnimationCurve _inFlightDecayCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	private float _curvePeakScale = 1f;

	private float _fallbackFlightDuration = 5f;

	private float _postImpactTailDuration = 2f;

	private float _debugArmedContribution;

	private float _debugGunLeftContribution;

	private float _debugGunLeftLatchedTime;

	private float _debugGunRightContribution;

	private float _debugGunRightLatchedTime;

	private float _debugOutput;

	private float _003CValue_003Ek__BackingField;

	private float _armedContribution;

	private float _gunLeftLatchedTravelTime;

	private float _gunRightLatchedTravelTime;

	private float _gunLeftContribution;

	private float _gunLeftFlightTotal;

	private float _gunLeftFlightRemaining;

	private float _gunRightContribution;

	private float _gunRightFlightTotal;

	private float _gunRightFlightRemaining;

	public float Value
	{
		get
		{
			return _003CValue_003Ek__BackingField;
		}
		private set
		{
			_003CValue_003Ek__BackingField = value;
		}
	}

	private void OnEnable()
	{
		//IL_009a: Invalid comparison between I4 and F4
		//IL_00ac: Expected F4, but got I4
		//IL_0161: Invalid comparison between I4 and F4
		//IL_0173: Expected F4, but got I4
		if (_gunLeft != null)
		{
			Action value = OnGunLeftFired;
			_gunLeft.OnGunFired += value;
			Action<float> value2 = OnGunLeftImpactTimeChanged;
			_gunLeft.OnPredictedImpactTimeChanged += value2;
			GunController gunLeft = _gunLeft;
			bool flag = !(0f < gunLeft._003CPredictedImpactTime_003Ek__BackingField);
			float gunLeftLatchedTravelTime = 0f;
			if (!flag)
			{
				gunLeftLatchedTravelTime = gunLeft._003CPredictedImpactTime_003Ek__BackingField;
			}
			_gunLeftLatchedTravelTime = gunLeftLatchedTravelTime;
		}
		if (_gunRight != null)
		{
			Action value3 = OnGunRightFired;
			_gunRight.OnGunFired += value3;
			Action<float> value4 = OnGunRightImpactTimeChanged;
			_gunRight.OnPredictedImpactTimeChanged += value4;
			GunController gunRight = _gunRight;
			bool flag2 = !(0f < gunRight._003CPredictedImpactTime_003Ek__BackingField);
			float gunRightLatchedTravelTime = 0f;
			if (!flag2)
			{
				gunRightLatchedTravelTime = gunRight._003CPredictedImpactTime_003Ek__BackingField;
			}
			_gunRightLatchedTravelTime = gunRightLatchedTravelTime;
		}
	}

	private void OnDisable()
	{
		if (_gunLeft != null)
		{
			Action value = OnGunLeftFired;
			_gunLeft.OnGunFired -= value;
			Action<float> value2 = OnGunLeftImpactTimeChanged;
			_gunLeft.OnPredictedImpactTimeChanged -= value2;
		}
		if (_gunRight != null)
		{
			Action value3 = OnGunRightFired;
			_gunRight.OnGunFired -= value3;
			Action<float> value4 = OnGunRightImpactTimeChanged;
			_gunRight.OnPredictedImpactTimeChanged -= value4;
		}
	}

	private unsafe void Update()
	{
		//IL_01fb: Invalid comparison between I4 and F4
		//IL_0222: Invalid comparison between I4 and F4
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected Ref, but got Unknown
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected Ref, but got Unknown
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected Ref, but got Unknown
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected Ref, but got Unknown
		//IL_0109: Expected F4, but got I4
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_0162: Expected O, but got I4
		//IL_0182: Invalid comparison between O and F4
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Expected O, but got Unknown
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Expected O, but got Unknown
		//IL_01a1: Expected F4, but got O
		float deltaTime = Time.deltaTime;
		bool flag = 0f < _gunLeftFlightRemaining;
		float num = deltaTime;
		if (!flag)
		{
			bool flag2 = _gunLeft != null;
			bool flag3 = !flag2;
			num = deltaTime;
			if (!flag3)
			{
				num = ComputeTravelTime(_gunLeft);
				_gunLeftLatchedTravelTime = num;
			}
		}
		if (!(0f < _gunRightFlightRemaining) && _gunRight != null)
		{
			num = ComputeTravelTime(_gunRight);
			_gunRightLatchedTravelTime = num;
		}
		UpdateArmedRamp(deltaTime);
		float dt = default(float);
		UpdateGunDecay(ref *(float*)(this + 148), ref *(float*)(this + 156), _gunLeftFlightTotal, dt);
		UpdateGunDecay(ref *(float*)(this + 160), ref *(float*)(this + 168), _gunRightFlightTotal, dt);
		float[] array = new float[3] { _armedContribution, _gunLeftContribution, _gunRightContribution };
		bool flag4 = array.Length == 0;
		float num2 = 0f;
		if (!flag4)
		{
			num2 = array[0];
			if (array.Length > 1)
			{
				object obj = array + 36;
				object obj2 = 1;
				float num3 = array[0];
				bool flag5;
				do
				{
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
					{
						num3 = (float)obj;
					}
					obj2++;
					obj += 4;
					flag5 = (nint)obj2 < array.Length;
					num2 = num3;
				}
				while (flag5);
			}
		}
		float num4 = ((!_invertOutput) ? num2 : (1f - num2));
		_003CValue_003Ek__BackingField = num4;
		_debugArmedContribution = _armedContribution;
		_debugGunLeftContribution = _gunLeftContribution;
		_debugGunLeftLatchedTime = _gunLeftLatchedTravelTime;
		_debugGunRightContribution = _gunRightContribution;
		_debugGunRightLatchedTime = _gunRightLatchedTravelTime;
		_debugOutput = _003CValue_003Ek__BackingField;
	}

	private void OnGunLeftImpactTimeChanged(float predictedSeconds)
	{
		//IL_000b: Invalid comparison between F4 and I4
		//IL_0028: Invalid comparison between I4 and F4
		//IL_003a: Expected F4, but got I4
		if (!(_gunLeftFlightRemaining > 0f))
		{
			bool flag = !(0f < predictedSeconds);
			float gunLeftLatchedTravelTime = 0f;
			if (!flag)
			{
				gunLeftLatchedTravelTime = predictedSeconds;
			}
			_gunLeftLatchedTravelTime = gunLeftLatchedTravelTime;
		}
	}

	private void OnGunRightImpactTimeChanged(float predictedSeconds)
	{
		//IL_000b: Invalid comparison between F4 and I4
		//IL_0028: Invalid comparison between I4 and F4
		//IL_003a: Expected F4, but got I4
		if (!(_gunRightFlightRemaining > 0f))
		{
			bool flag = !(0f < predictedSeconds);
			float gunRightLatchedTravelTime = 0f;
			if (!flag)
			{
				gunRightLatchedTravelTime = predictedSeconds;
			}
			_gunRightLatchedTravelTime = gunRightLatchedTravelTime;
		}
	}

	private void UpdateArmedRamp(float dt)
	{
		//IL_0130: Expected O, but got I4
		//IL_01d7: Expected F4, but got I4
		//IL_00ca: Invalid comparison between I4 and F4
		//IL_00d9: Expected O, but got I4
		//IL_0206: Invalid comparison between O and F4
		//IL_016c: Expected F4, but got I4
		//IL_0102: Expected O, but got I4
		//IL_0119: Expected O, but got I4
		float num;
		object obj;
		if (_relay != null)
		{
			ArmedFireRelayOneShot relay = _relay;
			if (relay._leftArmed || relay._rightArmed)
			{
				if (_spinner != null)
				{
					num = _spinner.EnergyNormalized;
					bool flag = 0f > num;
					obj = 0;
					if (!flag)
					{
						bool flag2 = !(num > 1f);
						obj = 0;
						if (!flag2)
						{
							obj = 0;
							num = 1f;
						}
						goto IL_01dc;
					}
				}
				else
				{
					obj = 0;
				}
				num = 0f;
				goto IL_01dc;
			}
		}
		if (0.0005f > (_armedContribution = SmoothToward(_armedContribution, 0f, _disarmReleaseTau, dt)))
		{
			_armedContribution = 0f;
		}
		return;
		IL_01dc:
		float num2 = num * _valueAtFullSpinner;
		float num3 = num2 + _valueOnArmed;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
		{
			if (num3 > 1f)
			{
				num3 = 1f;
			}
		}
		else
		{
			num3 = 0f;
		}
		float armedContribution = SmoothToward(_armedContribution, num3, _armedRampUpTau, dt);
		_armedContribution = armedContribution;
	}

	private unsafe void UpdateGunDecay(ref float contribution, ref float flightRemaining, float flightTotal, float dt)
	{
		//IL_000e: Invalid comparison between I4 and F4
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_0074: Expected O, but got I4
		//IL_0030: Invalid comparison between I4 and F4
		//IL_016e: Invalid comparison between I4 and F4
		//IL_0203: Expected F4, but got I4
		//IL_00cf: Invalid comparison between I4 and F4
		//IL_02b5: Expected Ref, but got F4
		//IL_02c3: Invalid comparison between I4 and F4
		//IL_01a0: Expected O, but got F4
		//IL_01ad: Expected O, but got F4
		//IL_01d2: Invalid comparison between F4 and I4
		//IL_01e1: Invalid comparison between F4 and I4
		//IL_0281: Expected O, but got I4
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Expected O, but got Unknown
		//IL_0101: Expected O, but got F4
		//IL_010e: Expected O, but got F4
		//IL_0133: Invalid comparison between F4 and I4
		//IL_0142: Invalid comparison between F4 and I4
		if (!(0f < contribution) && !(0f < flightRemaining))
		{
			return;
		}
		object obj2 = default(object);
		object obj = flightRemaining - obj2;
		bool flag = 0 >= (nint)obj;
		object obj3 = 0;
		if (!flag)
		{
			obj3 = obj;
		}
		ref float reference = ref *(float*)obj3;
		bool flag2 = 0 <= (nint)obj;
		float num = 1f;
		if (!flag2)
		{
			float num2 = (float)obj3 / flightTotal;
			num = 1f - num2;
		}
		bool flag3;
		bool flag4;
		bool flag5;
		float num5;
		if (_inFlightDecayCurve != null)
		{
			float num3 = _inFlightDecayCurve.Evaluate(num);
			if (0f > num3)
			{
				goto IL_01fa;
			}
			float num4 = num3 - 1f;
			object obj4 = num3 ^ 1f;
			object obj5 = num3 ^ num4;
			object obj6 = obj4 & obj5;
			flag3 = (nint)obj6 < 0;
			flag4 = num4 < 0f;
			flag5 = num4 == 0f;
			num5 = num3;
		}
		else
		{
			num5 = 1f - num;
			if (0f > num5)
			{
				goto IL_01fa;
			}
			float num6 = num5 - 1f;
			object obj7 = num5 ^ 1f;
			object obj8 = num5 ^ num6;
			object obj9 = obj7 & obj8;
			flag3 = (nint)obj9 < 0;
			flag4 = num6 < 0f;
			flag5 = num6 == 0f;
		}
		bool flag6 = flag4 == flag3;
		object obj10 = !flag6;
		object obj11 = obj10 | flag5;
		if (obj11 == null)
		{
			num5 = 1f;
		}
		goto IL_029c;
		IL_029c:
		float num7 = num5 * _curvePeakScale;
		ref float reference2 = ref *(float*)num7;
		if (!(0f < flightRemaining) && 0.001f > num7)
		{
			reference2 = ref *(float*)null;
		}
		return;
		IL_01fa:
		num5 = 0f;
		goto IL_029c;
	}

	private unsafe void OnGunLeftFired()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected Ref, but got Unknown
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected Ref, but got Unknown
		ref float flightRemaining = default(ref float);
		HandleFire(_gunLeftLatchedTravelTime, ref *(float*)(this + 148), ref *(float*)(this + 152), ref flightRemaining);
	}

	private unsafe void OnGunRightFired()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected Ref, but got Unknown
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected Ref, but got Unknown
		ref float flightRemaining = default(ref float);
		HandleFire(_gunRightLatchedTravelTime, ref *(float*)(this + 160), ref *(float*)(this + 164), ref flightRemaining);
	}

	private unsafe void HandleFire(float latchedTravelTime, ref float contribution, ref float flightTotal, ref float flightRemaining)
	{
		//IL_0009: Invalid comparison between F4 and I4
		//IL_00ae: Expected Ref, but got F4
		//IL_00b6: Expected O, but got F4
		//IL_0035: Invalid comparison between I4 and F4
		//IL_0080: Expected F4, but got I4
		//IL_00ee: Expected Ref, but got F4
		float num = default(float);
		float num3;
		if (num > 0f)
		{
			float num2 = _fallbackFlightDuration + _postImpactTailDuration;
			ref float reference = ref *(float*)num2;
			object obj = num2;
			if (_inFlightDecayCurve == null)
			{
				num3 = 1f;
				goto IL_00d5;
			}
		}
		num3 = _inFlightDecayCurve.Evaluate(0f);
		if (!(0f > num3))
		{
			if (num3 > 1f)
			{
				num3 = 1f;
			}
		}
		else
		{
			num3 = 0f;
		}
		goto IL_00d5;
		IL_00d5:
		float num4 = num3 * _curvePeakScale;
		ref float reference2 = ref *(float*)num4;
	}

	private static float ComputeTravelTime(GunController gun)
	{
		//IL_00a2: Expected F4, but got I4
		//IL_0065: Invalid comparison between I4 and F4
		//IL_00d5: Invalid comparison between I4 and F4
		//IL_00e7: Expected F4, but got I4
		if (gun != null)
		{
			ShellBlueprint chamberedShellBlueprint = gun.ChamberedShellBlueprint;
			if (chamberedShellBlueprint != null)
			{
				float adjustedShellSpeed = chamberedShellBlueprint.GetAdjustedShellSpeed();
				bool flag = 0f < adjustedShellSpeed;
				float num = adjustedShellSpeed;
				if (!flag)
				{
					num = 1f;
				}
				float num2 = gun.MapElevationToRange(gun._003CCurrentElevation_003Ek__BackingField);
				float num3 = num2 / num;
				bool flag2 = !(0f < num3);
				float result = 0f;
				if (!flag2)
				{
					result = num3;
				}
				return result;
			}
		}
		return 0f;
	}

	private static float SmoothToward(float current, float target, float tau, float dt)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_004d: Invalid comparison between F4 and I
		//IL_00da: Invalid comparison between I4 and F4
		//IL_0096: Expected F4, but got I4
		float num = default(float);
		bool flag = !(0f < num);
		float result = target;
		if (flag)
		{
			goto IL_009b;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj = dt ^ 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
		float num4 = default(float);
		if (!(num < 0f))
		{
			float num2 = (float)obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
			float num3 = num2 / 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
			num4 = 1f - num3;
			if (0f > num4)
			{
				num4 = 0f;
				goto IL_00ee;
			}
		}
		if (num4 > 1f)
		{
			num4 = 1f;
		}
		goto IL_00ee;
		IL_00ee:
		float num5 = target - current;
		float num6 = num5 * num4;
		result = num6 + current;
		goto IL_009b;
		IL_009b:
		return result;
	}
}
