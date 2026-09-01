using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using Unity.Cinemachine;
using UnityEngine;

public sealed class CinemachineImpulseListenerToSwingBridge : MonoBehaviour
{
	public enum QueryPositionMode
	{
		UseCameraTransformPosition,
		UseManualPosition
	}

	public enum DirectionMode
	{
		RandomXZ,
		FixedXZ,
		FromImpulsePositionXZ
	}

	private CinemachineImpulseListener impulseListener;

	private SwingController swingController;

	private bool mirrorListenerCombinationMode;

	private bool includeReactionSettings;

	private QueryPositionMode queryPositionMode;

	private Vector3 manualQueryPosition;

	private float intensityThreshold;

	private float rotationAngleToIntensity;

	private float intensitySmoothing;

	private AnimationCurve intensityToSwingStrength;

	private float strengthMultiplier;

	private DirectionMode directionMode;

	private Vector2 fixedDirectionWorldXZ;

	private Vector2 randomTwistImpulseWorldYMinMax;

	private float minSecondsBetweenImpulses;

	private bool logDebug;

	private float debugLogInterval;

	private float _smoothedIntensity;

	private float _nextImpulseTime;

	private float _nextLogTime;

	private unsafe void Reset()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected Ref, but got Unknown
		if (impulseListener == null)
		{
			bool flag = TryGetComponent<CinemachineImpulseListener>(out *(CinemachineImpulseListener*)(this + 32));
		}
	}

	private unsafe void Awake()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected Ref, but got Unknown
		if (impulseListener == null)
		{
			bool flag = TryGetComponent<CinemachineImpulseListener>(out *(CinemachineImpulseListener*)(this + 32));
		}
	}

	private unsafe void Update()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0087: Invalid comparison between I4 and F4
		//IL_00f8: Expected O, but got F4
		//IL_0203: Expected O, but got Ref
		//IL_0217: Expected O, but got Ref
		//IL_0a92: Expected O, but got I4
		//IL_018c: Expected O, but got Ref
		//IL_01a0: Expected O, but got Ref
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		//IL_02c0: Expected O, but got Ref
		//IL_02df: Expected O, but got Ref
		//IL_027d: Expected O, but got F4
		//IL_0ac5: Expected I, but got O
		//IL_0b4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b54: Expected F4, but got Unknown
		//IL_0531: Expected O, but got Ref
		//IL_0ba6: Invalid comparison between F4 and I4
		//IL_0573: Expected F4, but got I4
		//IL_05c1: Invalid comparison between I4 and F4
		//IL_0618: Expected F4, but got I4
		//IL_076d: Invalid comparison between F4 and I4
		//IL_0c20: Invalid comparison between I4 and F4
		//IL_07c9: Invalid comparison between I4 and F4
		//IL_06a9: Expected O, but got Ref
		//IL_0654: Expected F4, but got I4
		//IL_06df: Expected O, but got Ref
		//IL_07e9: Expected F4, but got I4
		//IL_0701: Expected O, but got Ref
		//IL_08a3: Expected O, but got I
		//IL_08b3: Expected F4, but got I
		//IL_08d0: Expected O, but got I
		//IL_0816: Expected O, but got I4
		//IL_0880: Expected F4, but got I
		//IL_0daa: Expected I, but got O
		//IL_0951: Expected O, but got I
		//IL_0967: Invalid comparison between F4 and O
		//IL_0d05: Expected I, but got O
		//IL_0d2e: Expected F4, but got I
		//IL_0859: Expected O, but got I
		//IL_09d4: Expected F8, but got I4
		//IL_0d4f: Expected F4, but got I
		//IL_0d4f: Expected F4, but got O
		//IL_0a2f: Expected O, but got F4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		if (!Application.isPlaying || !(impulseListener != null) || !(swingController != null))
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		if (!(0f < deltaTime))
		{
			return;
		}
		Quaternion quaternion;
		if (queryPositionMode == QueryPositionMode.UseManualPosition)
		{
			quaternion = (Quaternion)manualQueryPosition;
		}
		else
		{
			Transform transform = base.transform;
			quaternion = (Quaternion)transform.position.x;
		}
		bool flag;
		bool flag2;
		Quaternion b = default(Quaternion);
		ref Vector3 reference = default(ref Vector3);
		ref Quaternion rot = default(ref Quaternion);
		Vector3 vector;
		int channelMask;
		if (mirrorListenerCombinationMode)
		{
			CinemachineImpulseListener cinemachineImpulseListener = impulseListener;
			if (cinemachineImpulseListener.SignalCombinationMode != CinemachineImpulseListener.SignalCombinationModes.Additive)
			{
				CinemachineImpulseManager instance = CinemachineImpulseManager.Instance;
				CinemachineImpulseListener cinemachineImpulseListener2 = impulseListener;
				flag = cinemachineImpulseListener2.Use2DDistance;
				channelMask = cinemachineImpulseListener2.ChannelMask;
				flag2 = instance.GetStrongestImpulseAt((Vector3)(&b), cinemachineImpulseListener2.Use2DDistance, cinemachineImpulseListener2.ChannelMask, out reference, out rot);
				b = quaternion;
				vector = (Vector3)(&b);
				goto IL_021c;
			}
		}
		CinemachineImpulseManager instance2 = CinemachineImpulseManager.Instance;
		CinemachineImpulseListener cinemachineImpulseListener3 = impulseListener;
		flag = cinemachineImpulseListener3.Use2DDistance;
		channelMask = cinemachineImpulseListener3.ChannelMask;
		flag2 = instance2.GetImpulseAt((Vector3)(&b), cinemachineImpulseListener3.Use2DDistance, cinemachineImpulseListener3.ChannelMask, out reference, out rot);
		b = quaternion;
		vector = (Vector3)(&b);
		goto IL_021c;
		IL_0cac:
		Vector2 vector2;
		object obj3 = vector2 * vector2;
		float num2;
		float num = num2 * num2;
		float num3 = (float)obj3 + num;
		if (1E-06f > num3)
		{
			object obj4 = fixedDirectionWorldXZ * fixedDirectionWorldXZ;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CinemachineImpulseListenerToSwingBridge)+64]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CinemachineImpulseListenerToSwingBridge)+64]");
			object obj5 = num4 * 0;
			object obj6 = obj4 + obj5;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
			{
			}
		}
		nint num5 = (nint)typeof(Math);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1709 @ rcx_v30 (Il2CppClass<System.Math>)+E4]");
		double num6;
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm2\"");
			num6 = 0.0;
		}
		else
		{
			num6 = Math.Sqrt(0.0);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
		if (!(num6 > 9.999999747378752E-06))
		{
		}
		Vector2 vector3 = randomTwistImpulseWorldYMinMax;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CinemachineImpulseListenerToSwingBridge)+6C]");
		float worldTwistImpulse = UnityEngine.Random.Range((float)vector3, 0f);
		float num7 = default(float);
		swingController.TriggerExternalImpulse((Vector2)num7, worldTwistImpulse);
		return;
		IL_0885:
		Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+80]");
		vector2 = (Vector2)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+84]");
		num2 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+80]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+80]");
		object obj7 = num8 * 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+84]");
		float num9 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+84]");
		float num10 = num9 * 0f;
		float num11 = num10 + (float)obj7;
		if (1E-06f > num11)
		{
			nint num12 = (nint)typeof(Vector2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1596 @ rax_v37 (Il2CppClass<UnityEngine.Vector2>)+B8]");
			nint num13 = 0;
			vector2 = Vector2.rightVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1598 @ rcx_v36 (Il2CppStaticFields<UnityEngine.Vector2>)+2C]");
			num2 = 0f;
		}
		goto IL_0cac;
		IL_021c:
		bool flag3 = !flag2;
		ref Quaternion reference2 = ref *(Quaternion*)vector;
		float num14 = default(float);
		if (!flag3)
		{
			CinemachineImpulseListener cinemachineImpulseListener4 = impulseListener;
			reference2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 96));
			_ = Quaternion.identityQuaternion;
			Quaternion quaternion2 = Quaternion.Internal_SlerpUnclamped(ref reference2, ref b, cinemachineImpulseListener4.Gain);
			b = (Quaternion)num14;
			num14 = quaternion2.x;
			flag = (byte)(&b) != 0;
		}
		_ = Quaternion.identityQuaternion;
		bool flag4 = !includeReactionSettings;
		Vector3 vector4 = (Vector3)flag;
		bool flag5 = false;
		float num31 = default(float);
		float num42 = default(float);
		float num56 = default(float);
		if (!flag4)
		{
			CinemachineImpulseListener.ImpulseReaction impulseReaction = (CinemachineImpulseListener.ImpulseReaction)(impulseListener + 72);
			bool reaction = ((CinemachineImpulseListener.ImpulseReaction*)impulseReaction)->GetReaction(deltaTime, (Vector3)(&b), out var pos, out System.Runtime.CompilerServices.Unsafe.As<Vector3, Quaternion>(ref reference));
			bool flag6 = !reaction;
			channelMask = (int)(&pos);
			vector4 = (Vector3)(&b);
			flag5 = reaction;
			if (!flag6)
			{
				float num16 = default(float);
				float num15 = num16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-6C]");
				float num17 = num15 * 0f;
				float num19 = default(float);
				float num18 = num19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-78]");
				float num20 = num18 * 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-74]");
				float num22 = default(float);
				float num21 = 0f * num22;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-70]");
				float num23 = 0f * num22;
				float num24 = num21 + num17;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-6C]");
				float num25 = 0f * num22;
				float num26 = num14;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-70]");
				float num27 = num26 * 0f;
				float num28 = num24 + num20;
				float num29 = num14;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-74]");
				float num30 = num29 * 0f;
				num31 = num28 - num27;
				float num32 = num19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-6C]");
				float num33 = num32 * 0f;
				float num34 = num23 + num33;
				float num35 = num16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-78]");
				float num36 = num35 * 0f;
				float num37 = num34 + num30;
				float num38 = num16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-74]");
				float num39 = num38 * 0f;
				float num40 = num16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-70]");
				float num41 = num40 * 0f;
				num42 = num37 - num36;
				float num43 = num14;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-78]");
				float num44 = num43 * 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-78]");
				float num45 = 0f * num22;
				float num46 = num25 - num44;
				float num47 = num14;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-6C]");
				float num48 = num47 * 0f;
				float num49 = num19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-74]");
				float num50 = num49 * 0f;
				float num51 = num45 + num48;
				float num52 = num46 - num39;
				float num53 = num19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-70]");
				float num54 = num53 * 0f;
				float num55 = num51 + num41;
				num56 = num52 - num54;
				float num57 = num55 - num50;
				num14 = num57;
				channelMask = (int)(&pos);
				vector4 = (Vector3)(&b);
				flag5 = reaction;
			}
		}
		if (!flag2 && !flag5)
		{
			return;
		}
		nint num58 = (nint)typeof(Quaternion);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1129 @ rax_v24 (Il2CppClass<UnityEngine.Quaternion>)+B8]");
		nint num59 = 0;
		float num60 = num14 * (float)Quaternion.identityQuaternion;
		float num61 = num31 * num7;
		float num62 = num42 * num7;
		float num63 = num61 + num60;
		float num64 = num7 * num56;
		float num65 = num63 + num62;
		float num66 = num65 + num64;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float num67 = num66 & 0;
		if (!(1f > num67))
		{
			num67 = 1f;
		}
		float num68;
		float num69;
		if (num67 > 0.999999f)
		{
			num68 = 0f;
			num69 = num7;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EF80");
			float num70 = num67 + num67;
			num68 = num70 * 57.29578f;
			num69 = num67;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num71 = num68 * rotationAngleToIntensity;
		float num72 = num71 + num69;
		if (intensitySmoothing > 0f)
		{
			float num73;
			if (!(0f > intensitySmoothing))
			{
				bool flag7 = !(intensitySmoothing > 1f);
				num73 = intensitySmoothing;
				if (!flag7)
				{
					num73 = 1f;
				}
			}
			else
			{
				num73 = 0f;
			}
			float num74 = deltaTime * 60f;
			float num75 = 1f - num73;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			num60 = 1f - num75;
			if (!(0f > num60))
			{
				if (num60 > 1f)
				{
					num60 = 1f;
				}
			}
			else
			{
				num60 = 0f;
			}
			float num76 = num72 - _smoothedIntensity;
			float num77 = num76 * num60;
			num72 = (_smoothedIntensity = num77 + _smoothedIntensity);
		}
		if (logDebug)
		{
			float time = Time.time;
			if (!(time < _nextLogTime))
			{
				float time2 = Time.time;
				float nextLogTime = time2 + debugLogInterval;
				object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
				_nextLogTime = nextLogTime;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 136));
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				object arg3 = default(object);
				string message = $"[ImpulseListener->Swing] intensity={arg:0.###} posMag={arg2:0.###} rotDeg={arg3:0.###}";
				Debug.Log(message, this);
			}
		}
		if (intensityThreshold > num72)
		{
			return;
		}
		if (minSecondsBetweenImpulses > 0f)
		{
			float time3 = Time.time;
			if (_nextImpulseTime > time3)
			{
				return;
			}
		}
		float time4 = Time.time;
		float num78 = minSecondsBetweenImpulses;
		if (0f > minSecondsBetweenImpulses)
		{
			num78 = 0f;
		}
		bool flag8 = intensityToSwingStrength == null;
		float nextImpulseTime = num78 + time4;
		_nextImpulseTime = nextImpulseTime;
		if (!flag8)
		{
			float num79 = intensityToSwingStrength.Evaluate(num72);
		}
		bool flag9 = directionMode == DirectionMode.RandomXZ;
		if (flag9)
		{
			goto IL_0885;
		}
		object obj11 = directionMode - 1;
		if (!flag9)
		{
			if ((nint)obj11 != 1)
			{
				goto IL_0885;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+80]");
			vector2 = (Vector2)0;
			num2 = num7;
		}
		else
		{
			vector2 = fixedDirectionWorldXZ;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CinemachineImpulseListenerToSwingBridge)+64]");
			num2 = 0f;
		}
		goto IL_0cac;
	}

	private Vector2 GetDirectionXZFromMode(Vector3 impulsePos)
	{
		//IL_00a0: Invalid comparison between F4 and O
		//IL_002f: Expected O, but got I4
		bool flag = directionMode == DirectionMode.RandomXZ;
		Vector2 result = default(Vector2);
		if (!flag)
		{
			object obj = directionMode - 1;
			if (flag)
			{
				return result;
			}
			if ((nint)obj == 1)
			{
				return result;
			}
		}
		Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle;
		object obj2 = insideUnitCircle * insideUnitCircle;
		object obj4 = default(object);
		object obj3 = obj4 * obj4;
		object obj5 = obj3 + obj2;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
		{
		}
		return result;
	}

	public CinemachineImpulseListenerToSwingBridge()
	{
		//IL_001f: Expected I, but got O
		//IL_00a5: Expected O, but got I4
		//IL_00b4: Expected O, but got I8
		mirrorListenerCombinationMode = true;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		manualQueryPosition = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		intensityThreshold = 0.02f;
		rotationAngleToIntensity = 0.01f;
		intensitySmoothing = 0.12f;
		intensityToSwingStrength = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		strengthMultiplier = 1f;
		fixedDirectionWorldXZ = (Vector2)1065353216;
		randomTwistImpulseWorldYMinMax = (Vector2)3184315597L;
		_ = 1036831949;
		minSecondsBetweenImpulses = 0.05f;
		debugLogInterval = 0.5f;
		base._002Ector();
	}
}
