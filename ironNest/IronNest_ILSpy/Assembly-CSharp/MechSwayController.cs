using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class MechSwayController : MonoBehaviour
{
	private struct StepInstance
	{
		public float Time;

		public float RollSign;

		public bool Active;
	}

	private const int PoolSize = 2;

	private TurretMovementLegStepperBridge bridge;

	private AnimationCurve stepCurve;

	private float pitchAmplitude;

	private float rollAmplitude;

	private bool multiplyOnOverlap;

	private float fadeInSeconds;

	private float fadeOutSeconds;

	private bool debugLog;

	public UnityEvent OnSwayImpulse;

	private StepInstance[] _pool;

	private float _nextRollSign;

	private float _swayWeight;

	private bool _subscribed;

	private void Start()
	{
		bool flag = bridge != null;
		MechSwayController context = this;
		if (flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 61 Invalid \"Jump target not found in method: 0x18049BD10\"");
			MechSwayController mechSwayController = default(MechSwayController);
			context = mechSwayController;
		}
		Debug.LogWarning("[MechSwayController] Bridge reference is null. Assign a TurretMovementLegStepperBridge in the Inspector.", context);
	}

	private void OnEnable()
	{
		if (bridge != null && !_subscribed)
		{
			Subscribe();
		}
	}

	private void OnDisable()
	{
		Unsubscribe();
	}

	private void OnDestroy()
	{
		Unsubscribe();
	}

	private void Update()
	{
		//IL_011e: Invalid comparison between F4 and I4
		//IL_01b4: Expected F4, but got I4
		//IL_02d5: Expected O, but got I4
		//IL_02e8: Expected O, but got I4
		//IL_0169: Invalid comparison between I4 and F4
		//IL_006a: Invalid comparison between F4 and I4
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Expected O, but got Unknown
		//IL_00b5: Invalid comparison between I4 and F4
		//IL_0100: Expected F4, but got I4
		//IL_0235: Expected O, but got I4
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_0287: Invalid comparison between I and F4
		float num2;
		if (bridge != null)
		{
			TurretMovementLegStepperBridge turretMovementLegStepperBridge = bridge;
			if (turretMovementLegStepperBridge._003CIsMoving_003Ek__BackingField)
			{
				if (fadeInSeconds > 0f)
				{
					float deltaTime = Time.deltaTime;
					float num = deltaTime / fadeInSeconds;
					num2 = num + _swayWeight;
					if (!(0f > num2))
					{
						if (num2 > 1f)
						{
							num2 = 1f;
						}
					}
					else
					{
						num2 = 0f;
					}
				}
				else
				{
					num2 = 1f;
				}
				goto IL_02b9;
			}
		}
		if (fadeOutSeconds > 0f)
		{
			float deltaTime2 = Time.deltaTime;
			float num3 = deltaTime2 / fadeOutSeconds;
			num2 = _swayWeight - num3;
			if (!(0f > num2))
			{
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				goto IL_02b9;
			}
		}
		num2 = 0f;
		goto IL_02b9;
		IL_02b9:
		_swayWeight = num2;
		float deltaTime3 = Time.deltaTime;
		object obj = 0;
		float num4 = deltaTime3;
		object obj2 = 0;
		do
		{
			StepInstance[] pool = _pool;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rbx_v4+28+v142 @ rax_v8 (StepInstance[])]");
			if ((nint)0 != 0)
			{
				float num5 = deltaTime3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rbx_v4+20+v142 @ rax_v8 (StepInstance[])]");
				float num6 = num5 + 0f;
				Keyframe[] keys = stepCurve.keys;
				int length = stepCurve.length;
				object obj3 = length - 1;
				object obj4 = obj3 * 28;
				object obj5 = obj4 + 32;
				obj2 = obj5 + (object)keys;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				StepInstance[] pool2 = _pool;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rbx_v4+20+v145 @ rax_v14 (StepInstance[])]");
				if (!(0f < num4))
				{
					_ = 0;
				}
			}
			obj += 12;
		}
		while ((nint)obj < 24);
		ApplyRotation();
	}

	private void Subscribe()
	{
		if (!_subscribed)
		{
			TurretMovementLegStepperBridge turretMovementLegStepperBridge = bridge;
			UnityAction call = OnStep;
			turretMovementLegStepperBridge.OnStepTriggered.AddListener(call);
			_subscribed = true;
		}
	}

	private void Unsubscribe()
	{
		if (_subscribed && bridge != null)
		{
			TurretMovementLegStepperBridge turretMovementLegStepperBridge = bridge;
			UnityAction call = OnStep;
			turretMovementLegStepperBridge.OnStepTriggered.RemoveListener(call);
			_subscribed = false;
		}
	}

	private void OnStep()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0071: Expected O, but got I8
		//IL_0083: Expected O, but got I4
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected F4, but got Unknown
		//IL_00bd: Invalid comparison between O and F4
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_00dc: Expected F4, but got O
		if (!(bridge != null))
		{
			return;
		}
		TurretMovementLegStepperBridge turretMovementLegStepperBridge = bridge;
		if (!turretMovementLegStepperBridge._003CIsMoving_003Ek__BackingField)
		{
			return;
		}
		object obj = _pool + 32;
		object obj2 = 4294967295L;
		float num = -1f;
		object obj3 = 0;
		object obj4 = obj;
		bool flag;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ rdx_v6+8]");
			if ((nint)0 != 0)
			{
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num))
				{
					num = (float)obj;
					obj2 = obj3;
				}
				obj4 += 12;
				obj3++;
				flag = (nint)obj3 < 2;
				obj = obj4;
				continue;
			}
			obj2 = obj3;
			break;
		}
		while (flag);
		object obj5 = obj2 * 2;
		object obj6 = obj2 + obj5;
		_ = 1;
		float nextRollSign = _nextRollSign;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		float nextRollSign2 = nextRollSign ^ 0;
		_nextRollSign = nextRollSign2;
		if (OnSwayImpulse != null)
		{
			OnSwayImpulse.Invoke();
		}
	}

	private void TickInstances()
	{
		//IL_0012: Expected O, but got I4
		//IL_0024: Expected O, but got I4
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_00a5: Expected O, but got I4
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00f7: Invalid comparison between I and F4
		float deltaTime = Time.deltaTime;
		object obj = 0;
		float num = deltaTime;
		object obj2 = 0;
		do
		{
			StepInstance[] pool = _pool;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rbx_v2+28+v69 @ rax_v2 (StepInstance[])]");
			if ((nint)0 != 0)
			{
				float num2 = deltaTime;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rbx_v2+20+v69 @ rax_v2 (StepInstance[])]");
				float num3 = num2 + 0f;
				Keyframe[] keys = stepCurve.keys;
				int length = stepCurve.length;
				object obj3 = length - 1;
				object obj4 = obj3 * 28;
				object obj5 = obj4 + 32;
				obj2 = obj5 + (object)keys;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				StepInstance[] pool2 = _pool;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rbx_v2+20+v96 @ rax_v11 (StepInstance[])]");
				if (!(0f < num))
				{
					_ = 0;
				}
			}
			obj += 12;
		}
		while ((nint)obj < 24);
	}

	private unsafe void ApplyRotation()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Expected O, but got Unknown
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Expected O, but got Unknown
		//IL_0508: Invalid comparison between I4 and F4
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Expected O, but got Unknown
		//IL_0553: Expected F4, but got I4
		//IL_092c: Invalid comparison between I4 and F4
		//IL_0432: Expected F4, but got Ref
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Expected O, but got Unknown
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Expected O, but got Unknown
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Expected O, but got Unknown
		//IL_00f7: Expected F4, but got I4
		//IL_0100: Expected F4, but got I4
		//IL_05a4: Expected F4, but got I4
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Expected O, but got Unknown
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Expected O, but got Unknown
		//IL_0768: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Expected O, but got Unknown
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Expected O, but got Unknown
		//IL_0950: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Expected O, but got Unknown
		//IL_095e: Invalid comparison between O and F4
		//IL_027e: Expected F4, but got I
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected F4, but got Unknown
		//IL_02a0: Invalid comparison between F4 and I4
		//IL_0151: Expected F4, but got I
		//IL_0646: Invalid comparison between I4 and F4
		//IL_01a2: Expected O, but got I4
		//IL_06b2: Expected F4, but got I4
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Expected O, but got Unknown
		//IL_05dc: Invalid comparison between O and F4
		//IL_0894: Invalid comparison between I4 and F4
		//IL_02ff: Expected O, but got I4
		//IL_08af: Invalid comparison between I4 and F4
		//IL_083f: Expected O, but got Ref
		StepInstance[] pool = _pool;
		object obj = _pool + 40;
		AnimationCurve animationCurve = null;
		AnimationCurve animationCurve2 = null;
		AnimationCurve animationCurve3;
		bool flag;
		do
		{
			animationCurve3 = (AnimationCurve)(animationCurve2 + 1);
			if (obj == null)
			{
				animationCurve3 = animationCurve2;
			}
			animationCurve = (AnimationCurve)(animationCurve + 1);
			obj += 12;
			flag = (nint)animationCurve < 2;
			animationCurve2 = animationCurve3;
		}
		while (flag);
		Transform transform;
		Vector3 vector = default(Vector3);
		Vector3 vector2 = default(Vector3);
		Transform transform4;
		if (animationCurve3 != null)
		{
			if ((nint)animationCurve3 == 1)
			{
				object obj2 = _pool + 40;
				AnimationCurve animationCurve4 = null;
				AnimationCurve animationCurve5 = null;
				while (obj2 == null)
				{
					animationCurve5 = (AnimationCurve)(animationCurve5 + 1);
					animationCurve4 = (AnimationCurve)(animationCurve4 + 1);
					obj2 += 12;
					if ((nint)animationCurve4 >= 2)
					{
						return;
					}
				}
				float num = stepCurve.Evaluate((nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref pool[(object)animationCurve5]));
				StepInstance[] pool2 = _pool;
				object obj3 = animationCurve5 + 3;
				object obj4 = obj3 * 2;
				object obj5 = obj3 + obj4;
				float num2 = num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v280 @ rcx_v20 (StepInstance[])+v995 @ rax_v22*4]");
				float rollNorm = num2 * 0f;
				WriteFinalRotation(num, rollNorm);
				return;
			}
			if (!multiplyOnOverlap)
			{
				float num3 = 0f;
				float num4 = 0f;
				AnimationCurve animationCurve6 = null;
				StepInstance[] pool3 = _pool;
				do
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v9 (UnityEngine.AnimationCurve)+28+v641 @ rax_v33 (StepInstance[])]");
					if ((nint)0 != 0)
					{
						AnimationCurve animationCurve7 = stepCurve;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v9 (UnityEngine.AnimationCurve)+20+v641 @ rax_v33 (StepInstance[])]");
						float num5 = animationCurve7.Evaluate(0f);
						pool3 = _pool;
						num4 += num5;
						float num6 = num5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v9 (UnityEngine.AnimationCurve)+24+v641 @ rax_v33 (StepInstance[])]");
						float num7 = num6 * 0f;
						num3 += num7;
						obj = 0;
					}
					animationCurve6 = (AnimationCurve)(animationCurve6 + 12);
				}
				while ((nint)animationCurve6 < 24);
				if (-1f > num4 || num4 > 1f)
				{
				}
				if (!(-1f > num3) && !(num3 > 1f))
				{
				}
			}
			else
			{
				float num8 = 1f;
				float num9 = 1f;
				float num10 = 1f;
				float num11 = 1f;
				AnimationCurve animationCurve8 = null;
				do
				{
					StepInstance[] pool4 = _pool;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rsi_v7 (UnityEngine.AnimationCurve)+28+v303 @ rax_v28 (StepInstance[])]");
					if ((nint)0 != 0)
					{
						AnimationCurve animationCurve9 = stepCurve;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rsi_v7 (UnityEngine.AnimationCurve)+20+v303 @ rax_v28 (StepInstance[])]");
						float num12 = animationCurve9.Evaluate(0f);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						float num13 = num12 & 0;
						float num14 = ((num12 < 0f) ? (-1f) : 1f);
						StepInstance[] pool5 = _pool;
						num9 *= num13;
						num11 *= num14;
						num8 *= num13;
						float num15 = num14;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rsi_v7 (UnityEngine.AnimationCurve)+24+v310 @ rax_v30 (StepInstance[])]");
						float num7 = num15 * 0f;
						num10 *= num7;
						obj = 0;
					}
					animationCurve8 = (AnimationCurve)(animationCurve8 + 12);
				}
				while ((nint)animationCurve8 < 24);
				float num16 = num11 * num9;
				if (-1f > num16 || num16 > 1f)
				{
				}
				float num17 = num10 * num8;
				if (!(-1f > num17) && !(num17 > 1f))
				{
				}
			}
			transform = base.transform;
			vector = vector2;
		}
		else
		{
			Transform transform2 = base.transform;
			Vector3 localEulerAngles = transform2.localEulerAngles;
			float x = localEulerAngles.x / 360f;
			float num18 = MathF.Floor(x);
			float num19 = num18 * 360f;
			float num20 = localEulerAngles.x - num19;
			if (!(0f > num20))
			{
				if (num20 > 360f)
				{
					num20 = 360f;
				}
			}
			else
			{
				num20 = 0f;
			}
			if (num20 > 180f)
			{
				num20 -= 360f;
			}
			float x2 = localEulerAngles.z / 360f;
			float num21 = MathF.Floor(x2);
			float num22 = num21 * 360f;
			float num23 = localEulerAngles.z - num22;
			if (!(0f > num23))
			{
				if (num23 > 360f)
				{
					num23 = 360f;
				}
			}
			else
			{
				num23 = 0f;
			}
			if (num23 > 180f)
			{
				num23 -= 360f;
			}
			float num24 = num20;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj6 = num24 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f))
			{
				float num25 = num23;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj7 = num25 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f))
				{
					Transform transform3 = base.transform;
					vector = Vector3.zeroVector;
					transform4 = transform3;
					goto IL_0832;
				}
			}
			float deltaTime = Time.deltaTime;
			float num26 = deltaTime + deltaTime;
			float num27 = 0f - num20;
			if (!(0f > num26))
			{
				if (!(num26 > 1f))
				{
					num27 *= num26;
				}
				else
				{
					num26 = 1f;
				}
			}
			else
			{
				num27 *= 0f;
				num26 = 0f;
			}
			float num28 = num27 + num20;
			float num29 = 0f - num23;
			float num30 = num29 * num26;
			float num31 = num30 + num23;
			transform = base.transform;
			if (0f > num28)
			{
			}
			if (!(0f > num31))
			{
				vector = vector2;
			}
		}
		transform4 = transform;
		goto IL_0832;
		IL_0832:
		transform4.localEulerAngles = (Vector3)(&vector);
	}

	private unsafe void WriteFinalRotation(float pitchNorm, float rollNorm)
	{
		//IL_001c: Expected O, but got Ref
		Transform transform = base.transform;
		object obj = default(object);
		transform.localEulerAngles = (Vector3)(&obj);
	}

	private void UpdateFadeWeight(bool isMoving)
	{
		//IL_00c1: Invalid comparison between F4 and I4
		//IL_0019: Invalid comparison between F4 and I4
		//IL_0182: Expected O, but got I4
		//IL_010c: Invalid comparison between I4 and F4
		//IL_00b1: Expected F4, but got I4
		//IL_0157: Expected F4, but got I4
		//IL_0173: Expected O, but got I4
		//IL_0064: Invalid comparison between I4 and F4
		//IL_00a1: Expected O, but got I4
		if (!isMoving)
		{
			float num2;
			if (fadeOutSeconds > 0f)
			{
				float deltaTime = Time.deltaTime;
				float num = deltaTime / fadeOutSeconds;
				num2 = _swayWeight - num;
				if (!(0f > num2))
				{
					if (num2 > 1f)
					{
						object obj = 92;
						_ = 1f;
						return;
					}
					goto IL_016a;
				}
			}
			num2 = 0f;
			goto IL_016a;
		}
		if (fadeInSeconds > 0f)
		{
			float deltaTime2 = Time.deltaTime;
			float num3 = deltaTime2 / fadeInSeconds;
			float num4 = num3 + _swayWeight;
			if (!(0f > num4))
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
		}
		else
		{
			float num4 = 1f;
		}
		object obj2 = 92;
		return;
		IL_016a:
		object obj3 = 92;
	}

	public unsafe void ResetSway()
	{
		//IL_0009: Expected O, but got I4
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0087: Expected O, but got Ref
		object obj = 0;
		do
		{
			StepInstance[] pool = _pool;
			object obj2 = obj * 2;
			object obj3 = obj + obj2;
			obj++;
			_ = 0;
			_ = 0;
		}
		while ((nint)obj < 2);
		_swayWeight = 0f;
		Transform transform = base.transform;
		object obj4 = default(object);
		transform.localEulerAngles = (Vector3)(&obj4);
	}

	public void FireManualStep(float rollSign = 1f)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_001d: Expected O, but got I8
		//IL_002e: Expected O, but got I4
		//IL_0143: Invalid comparison between F4 and I4
		//IL_0069: Invalid comparison between O and F4
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0088: Expected F4, but got O
		object obj = _pool + 32;
		object obj2 = 4294967295L;
		object obj3 = obj;
		object obj4 = 0;
		float num = -1f;
		bool flag;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r8_v4+8]");
			if ((nint)0 != 0)
			{
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num))
				{
					num = (float)obj;
					obj2 = obj4;
				}
				obj3 += 12;
				obj4++;
				flag = (nint)obj4 < 2;
				obj = obj3;
				continue;
			}
			obj2 = obj4;
			break;
		}
		while (flag);
		if (!(rollSign < 0f))
		{
			/*Error: End of method reached without returning.*/;
		}
		object obj5 = obj2 * 2;
		object obj6 = obj2 + obj5;
		_ = 1;
		if (OnSwayImpulse != null)
		{
			OnSwayImpulse.Invoke();
		}
	}

	public unsafe MechSwayController()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Expected O, but got Unknown
		//IL_02bb: Expected native int or pointer, but got O
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0085: Expected native int or pointer, but got O
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_00ed: Expected native int or pointer, but got O
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0155: Expected native int or pointer, but got O
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_01bd: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = obj2 - 95;
		Keyframe[] keys = new Keyframe[5];
		Keyframe keyframe = (Keyframe)(obj - 89);
		_ = 0;
		_ = 0;
		_ = 0;
		float outTangent = default(float);
		*(Keyframe*)(nint)keyframe = new Keyframe(0f, 0f, 0f, outTangent);
		Keyframe keyframe2 = (Keyframe)(obj - 57);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-59]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-49]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-41]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe2 = new Keyframe(0.85f, 1f, 0f, outTangent);
		Keyframe keyframe3 = (Keyframe)(obj - 25);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-39]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-29]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-21]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe3 = new Keyframe(5.95f, 0.15f, -0.06f, outTangent);
		Keyframe keyframe4 = (Keyframe)(obj + 7);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-19]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-9]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-1]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe4 = new Keyframe(11.9f, -0.08f, -0.02f, outTangent);
		Keyframe keyframe5 = (Keyframe)(obj + 39);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+7]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+17]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+1F]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe5 = new Keyframe(17f, 0f, 0f, outTangent);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+27]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+37]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+3F]");
		_ = 0;
		stepCurve = new AnimationCurve(keys);
		pitchAmplitude = 2f;
		rollAmplitude = 2f;
		multiplyOnOverlap = true;
		fadeInSeconds = 2f;
		fadeOutSeconds = 4f;
		_pool = new StepInstance[2];
		_nextRollSign = 1f;
		base._002Ector();
	}
}
