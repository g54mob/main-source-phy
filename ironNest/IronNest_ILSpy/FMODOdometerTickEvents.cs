using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class FMODOdometerTickEvents : MonoBehaviour
{
	[Serializable]
	public class DrumTickEvent : UnityEvent<int>
	{
	}

	private OdometerDisplay odometer;

	private DrumTickEvent onLowest0Tick;

	private DrumTickEvent onLowest1Tick;

	private DrumTickEvent onLowest2Tick;

	private DrumTickEvent onLowest3Tick;

	private int digitsOnDrum;

	private int[] inspectorWatchedIndices;

	private int[] inspectorWatchedDigitIndex;

	private DrumTickEvent[] tickEvents;

	private Transform[] watchedDrums;

	private int[] lastDigitIndex;

	private float DegreesPerDigit
	{
		get
		{
			int num = digitsOnDrum;
			if (digitsOnDrum < 1)
			{
				num = 1;
			}
			return 360f / (float)num;
		}
	}

	private void OnValidate()
	{
		if (digitsOnDrum < 2)
		{
			digitsOnDrum = 2;
		}
		if (inspectorWatchedIndices == null)
		{
			int[] array = new int[4];
			inspectorWatchedIndices = array;
		}
		if (inspectorWatchedDigitIndex == null)
		{
			int[] array2 = new int[4];
			inspectorWatchedDigitIndex = array2;
		}
	}

	private void Awake()
	{
		//IL_0025: Expected I, but got O
		//IL_008c: Expected I, but got O
		//IL_00ee: Expected I, but got O
		//IL_00fe: Expected O, but got I
		//IL_016a: Expected I, but got O
		//IL_017a: Expected O, but got I
		//IL_01e6: Expected I, but got O
		//IL_01f6: Expected O, but got I
		bool flag = odometer == null;
		bool flag2 = !flag;
		nint num = unchecked((nint)null);
		if (!flag2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			OdometerDisplay odometerDisplay = default(OdometerDisplay);
			odometer = odometerDisplay;
			num = 0;
		}
		DrumTickEvent[] array = new DrumTickEvent[4];
		if (onLowest0Tick != null)
		{
			nint num2 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj = default(object);
			if (obj == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				DrumTickEvent drumTickEvent = default(DrumTickEvent);
				throw drumTickEvent;
			}
		}
		array[0] = onLowest0Tick;
		if (onLowest1Tick != null)
		{
			nint num3 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ rdx_v28 (Il2CppClass<DrumTickEvent[]>)+40]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj3 = default(object);
			bool flag3 = obj3 == null;
			DrumTickEvent drumTickEvent2 = onLowest1Tick;
			if (flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				DrumTickEvent drumTickEvent3 = default(DrumTickEvent);
				throw drumTickEvent3;
			}
		}
		array[1] = onLowest1Tick;
		if (onLowest2Tick != null)
		{
			nint num4 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ rdx_v26 (Il2CppClass<DrumTickEvent[]>)+40]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj5 = default(object);
			bool flag4 = obj5 == null;
			DrumTickEvent drumTickEvent4 = onLowest2Tick;
			if (flag4)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				DrumTickEvent drumTickEvent5 = default(DrumTickEvent);
				throw drumTickEvent5;
			}
		}
		array[2] = onLowest2Tick;
		if (onLowest3Tick != null)
		{
			nint num5 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v388 @ rdx_v24 (Il2CppClass<DrumTickEvent[]>)+40]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj7 = default(object);
			bool flag5 = obj7 == null;
			DrumTickEvent drumTickEvent6 = onLowest3Tick;
			if (flag5)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj8 = default(object);
				throw obj8;
			}
		}
		array[3] = onLowest3Tick;
		tickEvents = array;
		BindDrums();
		PrimeDigitState();
	}

	private void Update()
	{
		//IL_00bd: Expected O, but got I4
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		if (!(odometer != null))
		{
			return;
		}
		if (!(odometer != null))
		{
			goto IL_013e;
		}
		OdometerDisplay odometerDisplay = odometer;
		bool flag;
		if (odometerDisplay.drums != null)
		{
			Transform[] drums = odometerDisplay.drums;
			if (drums.Length != 0)
			{
				int[] array = inspectorWatchedIndices;
				object obj = drums.Length - 1;
				object obj2 = array[0] - obj;
				flag = obj2 == null;
				goto IL_0154;
			}
		}
		Transform[] array2 = watchedDrums;
		bool flag2 = array2[0] != null;
		flag = !flag2;
		goto IL_0154;
		IL_0154:
		if (!flag)
		{
			BindDrums();
			PrimeDigitState();
		}
		goto IL_013e;
		IL_013e:
		UpdateTicks();
	}

	private void BindDrums()
	{
		//IL_02fb: Expected O, but got I4
		//IL_0304: Expected O, but got I4
		//IL_030d: Expected O, but got I4
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_00fb: Expected O, but got I4
		//IL_010b: Expected O, but got I4
		//IL_0114: Expected O, but got I4
		//IL_011d: Expected O, but got I4
		//IL_0126: Expected O, but got I4
		//IL_012f: Expected O, but got I4
		//IL_0188: Expected O, but got I8
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Expected O, but got Unknown
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected O, but got Unknown
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Expected O, but got Unknown
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_0240: Expected I, but got O
		object obj = 32;
		object obj2 = 32;
		object obj3 = 0;
		do
		{
			Transform[] array = watchedDrums;
			_ = 0;
			int[] array2 = inspectorWatchedIndices;
			_ = 4294967295L;
			obj3++;
			obj2 += 4;
			obj += 8;
		}
		while ((nint)obj3 < 4);
		if (!(odometer != null))
		{
			return;
		}
		OdometerDisplay odometerDisplay = odometer;
		if (odometerDisplay.drums == null)
		{
			return;
		}
		Transform[] drums = odometerDisplay.drums;
		if (drums.Length == 0)
		{
			return;
		}
		object obj4 = drums.Length - 1;
		object obj5 = drums.Length - 1;
		object obj6 = 32;
		object obj7 = 0;
		object obj8 = 32;
		object obj9 = 0;
		object obj12 = default(object);
		while (true)
		{
			int[] array3 = inspectorWatchedIndices;
			object obj10 = obj4 - obj7;
			object obj11;
			if ((nint)obj5 >= 0)
			{
				OdometerDisplay odometerDisplay2 = odometer;
				Transform[] drums2 = odometerDisplay2.drums;
				bool flag = (nint)obj5 < drums2.Length;
				obj11 = obj5;
				if (flag)
				{
					goto IL_018d;
				}
			}
			obj11 = 4294967295L;
			goto IL_018d;
			IL_021b:
			Transform transform;
			Transform[] array4;
			if ((object)transform != null)
			{
				nint num = (nint)array4;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (obj12 == null)
				{
					break;
				}
			}
			obj9++;
			obj5--;
			obj7++;
			obj8 += 4;
			obj6 += 8;
			if ((nint)obj9 < 4)
			{
				continue;
			}
			return;
			IL_018d:
			array4 = watchedDrums;
			if ((nint)obj5 >= 0)
			{
				OdometerDisplay odometerDisplay3 = odometer;
				Transform[] drums3 = odometerDisplay3.drums;
				if ((nint)obj5 < drums3.Length)
				{
					transform = drums3[obj10];
					goto IL_021b;
				}
			}
			transform = null;
			goto IL_021b;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
		object obj13 = default(object);
		throw obj13;
	}

	private void BindDrumsIfNeeded()
	{
		//IL_009a: Expected O, but got I4
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		if (!(odometer != null))
		{
			return;
		}
		OdometerDisplay odometerDisplay = odometer;
		bool flag;
		if (odometerDisplay.drums != null)
		{
			Transform[] drums = odometerDisplay.drums;
			if (drums.Length != 0)
			{
				int[] array = inspectorWatchedIndices;
				object obj = drums.Length - 1;
				object obj2 = array[0] - obj;
				flag = obj2 == null;
				goto IL_0121;
			}
		}
		Transform[] array2 = watchedDrums;
		bool flag2 = array2[0] != null;
		flag = !flag2;
		goto IL_0121;
		IL_0121:
		if (!flag)
		{
			BindDrums();
			PrimeDigitState();
		}
	}

	private void PrimeDigitState()
	{
		//IL_00b9: Expected O, but got I4
		//IL_00c2: Expected O, but got I4
		//IL_001c: Expected O, but got I
		//IL_004f: Expected O, but got I
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		object obj = 32;
		object obj2 = 32;
		do
		{
			Transform[] array = watchedDrums;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r14_v2+v79 @ rax_v3 (UnityEngine.Transform[])]");
			if ((UnityEngine.Object)0 != null)
			{
				Transform[] array2 = watchedDrums;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r14_v2+v102 @ rdx_v6 (UnityEngine.Transform[])]");
				int num = ComputeDigitIndexFromTransform((Transform)0);
				int[] array3 = lastDigitIndex;
				int[] array4 = inspectorWatchedDigitIndex;
			}
			else
			{
				int[] array5 = lastDigitIndex;
				_ = 0;
				int[] array6 = inspectorWatchedDigitIndex;
				_ = 0;
			}
			obj2 += 8;
			obj += 4;
		}
		while ((nint)obj2 < 64);
	}

	private unsafe void UpdateTicks()
	{
		//IL_0066: Expected O, but got I
		//IL_0083: Expected O, but got I
		//IL_00a6: Invalid comparison between F4 and O
		//IL_00c3: Expected O, but got I4
		//IL_00cd: Expected O, but got I4
		//IL_00e9: Expected O, but got I
		//IL_010d: Expected O, but got I
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Expected O, but got Unknown
		//IL_0168: Expected O, but got I4
		//IL_01ed: Expected O, but got I4
		//IL_026e: Expected O, but got I
		if (!(odometer != null))
		{
			return;
		}
		OdometerDisplay odometerDisplay = odometer;
		object obj = (object)odometerDisplay.rotationAxis * (object)odometerDisplay.rotationAxis;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v5 (OdometerDisplay)+3C]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v5 (OdometerDisplay)+3C]");
		object obj2 = num * 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v5 (OdometerDisplay)+40]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v5 (OdometerDisplay)+40]");
		object obj3 = num2 * 0;
		object obj4 = obj + obj2;
		object obj5 = obj4 + obj3;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
		{
			return;
		}
		object obj6 = 32;
		object obj7 = 32;
		object obj10 = default(object);
		do
		{
			Transform[] array = watchedDrums;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r14_v7+v175 @ rax_v12 (UnityEngine.Transform[])]");
			if ((UnityEngine.Object)0 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r14_v7+v175 @ rax_v12 (UnityEngine.Transform[])]");
				int num3 = ComputeDigitIndexFromTransform((Transform)0);
				int[] array2 = inspectorWatchedDigitIndex;
				int[] array3 = lastDigitIndex;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rdi_v8+v177 @ rax_v18 (System.Int32[])]");
				if ((nint)num3 != 0)
				{
					object obj8 = digitsOnDrum - 1;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rdi_v8+v177 @ rax_v18 (System.Int32[])]");
					if (0 != (nint)obj8 || num3 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rdi_v8+v177 @ rax_v18 (System.Int32[])]");
						if ((nint)0 == 0)
						{
							object obj9 = digitsOnDrum - 1;
							if (num3 == (nint)obj9)
							{
								goto IL_02b3;
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rdi_v8+v177 @ rax_v18 (System.Int32[])]");
						if ((nint)num3 > (nint)0)
						{
						}
					}
					goto IL_02b3;
				}
			}
			goto IL_0273;
			IL_02b3:
			int[] array4 = lastDigitIndex;
			DrumTickEvent[] array5 = tickEvents;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r14_v7+v178 @ rax_v22 (DrumTickEvent[])]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r14_v7+v178 @ rax_v22 (DrumTickEvent[])]");
				((UnityEvent<int>)0).Invoke((int)(&obj10));
			}
			goto IL_0273;
			IL_0273:
			obj6 += 4;
			obj7 += 8;
		}
		while ((nint)obj6 < 48);
	}

	private unsafe int ComputeDigitIndexFromTransform(Transform drum)
	{
		//IL_0183: Expected I4, but got O
		//IL_0056: Expected O, but got Ref
		//IL_0056: Expected O, but got Ref
		//IL_00a4: Invalid comparison between I4 and F4
		//IL_01bd: Invalid comparison between F8 and I4
		//IL_0170: Expected F8, but got I4
		//IL_01d6: Expected I4, but got F8
		//IL_014b: Invalid comparison between F8 and I4
		if ((object)drum != null)
		{
			Quaternion localRotation = drum.localRotation;
			if ((object)odometer != null)
			{
				object obj = default(object);
				object obj2 = default(object);
				float signedAngleProjected = GetSignedAngleProjected((Quaternion)(&obj), (Vector3)(&obj2));
				float x = signedAngleProjected / 360f;
				float num = MathF.Floor(x);
				float num2 = num * 360f;
				float num3 = signedAngleProjected - num2;
				if (0f > num3 || num3 > 360f)
				{
				}
				if (digitsOnDrum < 1)
				{
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
				double num4 = Math.Floor(0.0);
				int num5 = digitsOnDrum;
				if (digitsOnDrum < 1)
				{
					num5 = 1;
				}
				double num6 = num4 % (double)num5;
				if (!(num6 < 0.0))
				{
					int num7 = digitsOnDrum - 1;
					if (num6 > (double)num7)
					{
						return num7;
					}
				}
				else
				{
					num6 = 0.0;
				}
				return (int)num6;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	private static int ComputeSingleStepDirection(int prev, int curr, int baseN)
	{
		//IL_000e: Expected O, but got I4
		//IL_0081: Expected O, but got I4
		//IL_00cf: Expected I4, but got I8
		object obj = baseN - 1;
		if (prev == (nint)obj && curr == 0)
		{
			return 1;
		}
		if (prev == 0)
		{
			object obj2 = baseN - 1;
			if (curr == (nint)obj2)
			{
				goto IL_00c2;
			}
		}
		bool flag = curr > prev;
		int result = 1;
		if (!flag)
		{
			goto IL_00c2;
		}
		goto IL_00d4;
		IL_00d4:
		return result;
		IL_00c2:
		result = -1;
		goto IL_00d4;
	}

	private static float GetSignedAngleProjected(Quaternion localRotation, Vector3 axis)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0250: Expected I, but got O
		//IL_0259: Invalid comparison between O and F4
		//IL_007f: Expected F4, but got I
		//IL_028d: Expected O, but got I
		//IL_02aa: Expected O, but got I
		//IL_0233: Expected F4, but got I4
		//IL_0098: Expected I, but got O
		//IL_031b: Expected F4, but got I
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Expected O, but got Unknown
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Expected O, but got Unknown
		//IL_03a7: Invalid comparison between O and F4
		//IL_062c: Expected O, but got I
		//IL_0649: Expected O, but got I
		//IL_017a: Expected F4, but got O
		//IL_03e7: Expected F4, but got I
		//IL_00bf: Expected O, but got I
		//IL_00dc: Expected O, but got I
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Expected O, but got Unknown
		//IL_01c8: Expected I, but got O
		//IL_01f1: Expected F4, but got I
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Expected O, but got Unknown
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Expected O, but got Unknown
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Expected O, but got Unknown
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Expected O, but got Unknown
		//IL_04a3: Expected O, but got I
		//IL_04c0: Expected O, but got I
		//IL_04dd: Expected O, but got I
		//IL_04fa: Expected O, but got I
		//IL_0517: Expected O, but got I
		//IL_0541: Expected O, but got I
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Expected O, but got Unknown
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Expected O, but got Unknown
		//IL_05b9: Invalid comparison between F4 and I4
		//IL_01ad: Expected O, but got F4
		object obj2 = default(object);
		object obj = obj2 - 95;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		nint num = (nint)typeof(Vector3);
		object obj3 = default(object);
		float num4 = default(float);
		float num5;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			float num2 = axis.z / (float)obj3;
			float num3 = num4;
			num5 = num2;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rax_v24 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			num5 = 0f;
			_ = Vector3.zeroVector;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
		nint num7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
		object obj4 = num7 * 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
		object obj5 = num8 * 0;
		object obj6 = obj5 + obj4;
		float num9 = num5 * num5;
		float num10 = (float)obj6 + num9;
		if (!(0.0001f > num10))
		{
			nint num11 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v255 @ rcx_v17 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num12 = 0;
			Vector3 vector = Vector3.rightVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ rax_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
			float num13 = 0f;
			float num14 = num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
			float num15 = num14 * 0f;
			Vector3 rightVector = Vector3.rightVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
			object obj7 = rightVector * 0;
			_ = Vector3.rightVector;
			_ = Vector3.rightVector;
			float num16 = (float)obj7 + num15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ rax_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
			float num17 = 0f * num5;
			float num18 = num16 + num17;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj8 = num18 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.9f))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v255 @ rcx_v17 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num19 = 0;
				vector = Vector3.upVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ rax_v21 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
				num13 = 0f;
				_ = Vector3.upVector;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
			nint num20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
			object obj9 = num20 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
			nint num21 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
			object obj10 = num21 * 0;
			float num22 = num5 * num5;
			object obj11 = obj10 + obj9;
			float num23 = Mathf.Epsilon;
			float num24 = (float)obj11 + num22;
			if (!(Mathf.Epsilon > num24))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
				nint num25 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
				object obj12 = num25 * 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
				nint num26 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
				object obj13 = num26 * 0;
				object obj14 = obj13 + obj12;
				float num27 = num13 * num5;
				float num28 = (float)obj14 + num27;
				float num29 = num28 / num24;
				float num30 = num29;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
				num22 = num30 * 0f;
				float num3 = num29 * num5;
				float num31 = num13 - num3;
				num23 = num4;
				float num32 = num4;
				num13 = num31;
			}
			else
			{
				float num32 = (float)vector;
			}
			object obj15 = obj - 57;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			if (num23 > 1E-05f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
				num22 = 0f / num23;
				float num32 = num13 / num23;
				Vector3 vector2 = (Vector3)num4;
				float num33 = num32;
			}
			else
			{
				nint num34 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v401 @ rax_v17 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num35 = 0;
				Vector3 vector2 = Vector3.zeroVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v402 @ rcx_v14 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				float num33 = 0f;
			}
			Vector3 vector3 = (Vector3)(obj - 41);
			Quaternion quaternion = (Quaternion)(obj - 25);
			_ = localRotation.x;
			Vector3 vector4 = quaternion * vector3;
			object obj16 = obj - 41;
			object obj17 = obj - 57;
			_ = vector4.x;
			_ = vector4.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F3260");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
			nint num36 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-31]");
			object obj18 = num36 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-21]");
			nint num37 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-35]");
			object obj19 = num37 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
			nint num38 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-31]");
			object obj20 = num38 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
			nint num39 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-39]");
			object obj21 = num39 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
			nint num40 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-35]");
			object obj22 = num40 * 0;
			object obj23 = obj19 - obj20;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-21]");
			nint num41 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-39]");
			object obj24 = num41 * 0;
			object obj25 = obj21 - obj22;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
			object obj26 = obj23 * 0;
			object obj27 = obj18 - obj24;
			float num42 = (float)obj25 * num5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
			object obj28 = obj27 * 0;
			object obj29 = obj28 + obj26;
			float num43 = (float)obj29 + num42;
			if (!(num43 < 0f))
			{
				return 1f * vector4.x;
			}
			return -1f * vector4.x;
		}
		return 0f;
	}

	public FMODOdometerTickEvents()
	{
		DrumTickEvent drumTickEvent = new DrumTickEvent();
		onLowest0Tick = drumTickEvent;
		DrumTickEvent drumTickEvent2 = new DrumTickEvent();
		onLowest1Tick = drumTickEvent2;
		DrumTickEvent drumTickEvent3 = new DrumTickEvent();
		onLowest2Tick = drumTickEvent3;
		DrumTickEvent drumTickEvent4 = new DrumTickEvent();
		onLowest3Tick = drumTickEvent4;
		digitsOnDrum = 10;
		int[] array = new int[4];
		inspectorWatchedIndices = array;
		inspectorWatchedDigitIndex = new int[4];
		watchedDrums = new Transform[4];
		lastDigitIndex = new int[4];
		base._002Ector();
	}
}
