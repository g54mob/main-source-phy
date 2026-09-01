using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public sealed class CamshaftRocker : MonoBehaviour
{
	public EnginePowerController EnginePower;

	public Transform Target;

	public Vector3 LocalAxis;

	public AnimationCurve AngleCurve;

	public float CycleDurationAtMinPower;

	public float CycleDurationAtMaxPower;

	public float MinPowerForMotion;

	public float BaseAngleDegrees;

	public Vector3 PostEulerOffset;

	public float StartCyclePosition;

	public bool PreserveCycleOnEnable;

	private float _cyclePos;

	private Quaternion _initialLocalRotation;

	private void Awake()
	{
		//IL_0065: Expected O, but got F4
		//IL_0093: Expected O, but got I
		//IL_00b0: Expected O, but got I
		//IL_00d3: Invalid comparison between F4 and O
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected O, but got Unknown
		//IL_012d: Expected I, but got O
		//IL_016e: Expected I, but got O
		//IL_0187: Expected F4, but got O
		if (Target == null)
		{
			Transform target = base.transform;
			Target = target;
		}
		_initialLocalRotation = (Quaternion)Target.localRotation.x;
		object obj = (object)LocalAxis * (object)LocalAxis;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CamshaftRocker)+38]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CamshaftRocker)+38]");
		object obj2 = num * 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CamshaftRocker)+34]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CamshaftRocker)+34]");
		object obj3 = num2 * 0;
		object obj4 = obj + obj3;
		object obj5 = obj4 + obj2;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5);
		float num3 = 1E-06f;
		if (!flag)
		{
			nint num4 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ rax_v15 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num5 = 0;
			num3 = (float)Vector3.rightVector;
			LocalAxis = Vector3.rightVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rdx_v6 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
			_ = 0;
		}
		object obj6 = this + 48;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		if (num3 > 1E-05f)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CamshaftRocker)+38]");
			float num6 = 0f / num3;
			Vector3 localAxis = default(Vector3);
			LocalAxis = localAxis;
			return;
		}
		nint num7 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ rax_v10 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num8 = 0;
		LocalAxis = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ rcx_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
	}

	private void OnEnable()
	{
		//IL_004b: Invalid comparison between I4 and F4
		//IL_0094: Expected F4, but got I4
		if (PreserveCycleOnEnable)
		{
			return;
		}
		float num = MathF.Floor(StartCyclePosition);
		float num2 = StartCyclePosition - num;
		if (!(0f > num2))
		{
			if (num2 > 1f)
			{
				_cyclePos = 1f;
				return;
			}
		}
		else
		{
			num2 = 0f;
		}
		_cyclePos = num2;
	}

	private void Start()
	{
		//IL_002a: Invalid comparison between F4 and I4
		//IL_0043: Expected O, but got I4
		//IL_00ad: Expected F4, but got I
		//IL_00d1: Invalid comparison between I4 and F4
		//IL_0096: Expected F4, but got I4
		//IL_005a: Expected O, but got I4
		if (!PreserveCycleOnEnable)
		{
			return;
		}
		bool flag = _cyclePos == 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018052DBEDh\"");
		object obj = 100;
		if (!flag)
		{
			obj = 108;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v3+this @ rcx (CamshaftRocker)]");
		float num = MathF.Floor(0f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v3+this @ rcx (CamshaftRocker)]");
		float num2 = 0f - num;
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
		_cyclePos = num2;
	}

	private unsafe void Update()
	{
		//IL_01be: Expected F4, but got I4
		//IL_019e: Expected O, but got Ref
		//IL_0062: Invalid comparison between F4 and I4
		//IL_01d5: Invalid comparison between I4 and F4
		//IL_00e2: Expected F4, but got I4
		//IL_01f2: Invalid comparison between I4 and F4
		//IL_011e: Expected F4, but got I4
		//IL_031b: Invalid comparison between I4 and F4
		//IL_0168: Expected F4, but got I4
		if (EnginePower != null)
		{
			EnginePowerController enginePower = EnginePower;
			float num = enginePower._003CPower_003Ek__BackingField;
			if (enginePower._003CPower_003Ek__BackingField > 0f)
			{
				if (!(enginePower._003CPower_003Ek__BackingField > MinPowerForMotion))
				{
					num = MinPowerForMotion;
				}
				if (!(0f > num))
				{
					if (num > 1f)
					{
						num = 1f;
					}
				}
				else
				{
					num = 0f;
				}
				if (!(0f > num))
				{
					if (num > 1f)
					{
						num = 1f;
					}
				}
				else
				{
					num = 0f;
				}
				float num2 = CycleDurationAtMaxPower - CycleDurationAtMinPower;
				float num3 = num2 * num;
				float num4 = num3 + CycleDurationAtMinPower;
				if (!(num4 > 1E-06f))
				{
					num4 = 1E-06f;
				}
				float deltaTime = Time.deltaTime;
				float num5 = 1f / num4;
				float num6 = deltaTime * num5;
				float num7 = num6 + _cyclePos;
				float num8 = MathF.Floor(num7);
				float num9 = num7 - num8;
				if (!(0f > num9))
				{
					if (num9 > 1f)
					{
						num9 = 1f;
					}
				}
				else
				{
					num9 = 0f;
				}
				_cyclePos = num9;
			}
		}
		bool flag = AngleCurve == null;
		float num10 = 0f;
		if (!flag)
		{
			float num11 = AngleCurve.Evaluate(_cyclePos);
			num10 = num11;
		}
		float angle = num10 + BaseAngleDegrees;
		Vector3 axis = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_AngleAxis(angle, ref axis);
		Quaternion quaternion2 = Quaternion.Internal_FromEulerRad(ref axis);
		float num12 = default(float);
		Target.localRotation = (Quaternion)(&num12);
	}

	private unsafe static AnimationCurve DefaultSineLikeCurve()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Expected O, but got Unknown
		//IL_02dc: Expected native int or pointer, but got O
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_00a2: Expected native int or pointer, but got O
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_0127: Expected native int or pointer, but got O
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_01ac: Expected native int or pointer, but got O
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_0231: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = obj2 - 95;
		Keyframe[] array = new Keyframe[5];
		Keyframe keyframe = (Keyframe)(obj - 89);
		_ = 0;
		_ = 0;
		_ = 0;
		float outTangent = default(float);
		*(Keyframe*)(nint)keyframe = new Keyframe(0f, 0f, 0f, outTangent);
		if (array.Length > 0)
		{
			Keyframe keyframe2 = (Keyframe)(obj - 57);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-59]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-41]");
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			*(Keyframe*)(nint)keyframe2 = new Keyframe(0.25f, 30f, 0f, outTangent);
			if (array.Length > 1)
			{
				Keyframe keyframe3 = (Keyframe)(obj - 25);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-39]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-21]");
				_ = 0;
				_ = 0;
				_ = 0;
				_ = 0;
				*(Keyframe*)(nint)keyframe3 = new Keyframe(0.5f, 0f, 0f, outTangent);
				if (array.Length > 2)
				{
					Keyframe keyframe4 = (Keyframe)(obj + 7);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-9]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-1]");
					_ = 0;
					_ = 0;
					_ = 0;
					_ = 0;
					*(Keyframe*)(nint)keyframe4 = new Keyframe(0.75f, -30f, 0f, outTangent);
					if (array.Length > 3)
					{
						Keyframe keyframe5 = (Keyframe)(obj + 39);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+7]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+17]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1F]");
						_ = 0;
						_ = 0;
						_ = 0;
						_ = 0;
						*(Keyframe*)(nint)keyframe5 = new Keyframe(1f, 0f, 0f, outTangent);
						if (array.Length > 4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+27]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+37]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+3F]");
							_ = 0;
							return new AnimationCurve(array);
						}
					}
				}
			}
		}
		return (AnimationCurve)(object)new IndexOutOfRangeException();
	}

	public unsafe CamshaftRocker()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0234: Expected I, but got O
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Expected O, but got Unknown
		//IL_02ee: Expected native int or pointer, but got O
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
		//IL_026f: Expected I, but got O
		object obj2 = default(object);
		object obj = obj2 - 95;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		LocalAxis = Vector3.rightVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
		_ = 0;
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
		*(Keyframe*)(nint)keyframe2 = new Keyframe(0.25f, 30f, 0f, outTangent);
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
		*(Keyframe*)(nint)keyframe3 = new Keyframe(0.5f, 0f, 0f, outTangent);
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
		*(Keyframe*)(nint)keyframe4 = new Keyframe(0.75f, -30f, 0f, outTangent);
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
		*(Keyframe*)(nint)keyframe5 = new Keyframe(1f, 0f, 0f, outTangent);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+27]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+37]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+3F]");
		_ = 0;
		AngleCurve = new AnimationCurve(keys);
		CycleDurationAtMinPower = 2f;
		CycleDurationAtMaxPower = 0.5f;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v282 @ rax_v29 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		PostEulerOffset = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rcx_v14 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		PreserveCycleOnEnable = true;
		base._002Ector();
	}
}
