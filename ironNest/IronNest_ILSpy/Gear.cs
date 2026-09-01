using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class Gear : MonoBehaviour
{
	public Transform parentGear;

	public float ratio;

	private float lastRatio = -9999f;

	private Quaternion initialParentRotation;

	private Quaternion myInitialRotation;

	private Vector3 myInitialUp;

	private bool _initted;

	private int parentRotations;

	private float lastAngle;

	private bool _003Cdebug_003Ek__BackingField;

	public bool debug
	{
		get
		{
			return _003Cdebug_003Ek__BackingField;
		}
		set
		{
			_003Cdebug_003Ek__BackingField = value;
		}
	}

	private void Awake()
	{
		if (!(parentGear == null))
		{
			InitGear();
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	private unsafe void InitGear()
	{
		//IL_0056: Expected O, but got F4
		//IL_0081: Expected O, but got F4
		//IL_00be: Expected O, but got Ref
		//IL_00dd: Expected O, but got F4
		if (!_initted)
		{
			if (_003Cdebug_003Ek__BackingField)
			{
				Debug.Log("Calling InitGear");
			}
			initialParentRotation = (Quaternion)parentGear.localRotation.x;
			Transform transform = base.transform;
			myInitialRotation = (Quaternion)transform.localRotation.x;
			Transform transform2 = base.transform;
			Transform transform3 = base.transform;
			Vector3 up = transform3.up;
			object obj = default(object);
			Vector3 vector = transform2.InverseTransformDirection((Vector3)(&obj));
			lastRatio = ratio;
			myInitialUp = (Vector3)vector.x;
			parentRotations = 0;
			_ = vector.z;
			_initted = true;
		}
	}

	private unsafe void Update()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0032: Expected F4, but got I
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_0052: Invalid comparison between F4 and O
		//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0c: Expected F4, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected F4, but got Unknown
		//IL_0ed2: Expected O, but got I4
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Expected F4, but got Unknown
		//IL_0bbe: Expected O, but got Ref
		//IL_1194: Expected O, but got Ref
		//IL_0562: Expected O, but got I4
		//IL_0ee5: Expected O, but got Ref
		//IL_0c61: Expected O, but got Ref
		//IL_0ae1: Expected O, but got I4
		//IL_0648: Expected O, but got Ref
		//IL_065f: Expected O, but got I
		//IL_00d5: Expected O, but got Ref
		//IL_00ec: Expected O, but got I
		//IL_0bf9: Expected I, but got O
		//IL_0c09: Expected O, but got I
		//IL_0d3e: Expected O, but got Ref
		//IL_0ccc: Expected I, but got O
		//IL_0cdc: Expected O, but got I
		//IL_0de1: Expected O, but got Ref
		//IL_0d79: Expected I, but got O
		//IL_0d89: Expected O, but got I
		//IL_06b0: Expected I, but got O
		//IL_06c0: Expected O, but got I
		//IL_06f1: Expected O, but got I
		//IL_013d: Expected I, but got O
		//IL_014d: Expected O, but got I
		//IL_0177: Expected O, but got I4
		//IL_0749: Expected O, but got Ref
		//IL_0759: Expected O, but got I
		//IL_01cf: Expected O, but got Ref
		//IL_01df: Expected O, but got I
		//IL_0e1a: Expected I, but got O
		//IL_0e2a: Expected O, but got I
		//IL_07c4: Expected I, but got O
		//IL_07d4: Expected O, but got I
		//IL_0805: Expected O, but got I
		//IL_024a: Expected I, but got O
		//IL_025a: Expected O, but got I
		//IL_02b3: Expected O, but got I
		//IL_0867: Expected O, but got Ref
		//IL_0877: Expected O, but got I
		//IL_0315: Expected O, but got Ref
		//IL_0325: Expected O, but got I
		//IL_08b2: Expected I, but got O
		//IL_08c2: Expected O, but got I
		//IL_08f3: Expected O, but got I
		//IL_0360: Expected I, but got O
		//IL_0370: Expected O, but got I
		//IL_03c9: Expected O, but got I
		//IL_094d: Expected O, but got I
		//IL_095b: Expected O, but got Ref
		//IL_0423: Expected O, but got I
		//IL_0431: Expected O, but got Ref
		//IL_0994: Expected I, but got O
		//IL_09a4: Expected O, but got I
		//IL_09d5: Expected O, but got I
		//IL_046a: Expected I, but got O
		//IL_047a: Expected O, but got I
		//IL_04d3: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		bool flag = ratio == lastRatio;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803FC3C1h\"");
		if (!flag)
		{
			InitGear();
		}
		float angle = GetAngle();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float num = 0f;
		float num2 = ratio;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj3 = num2 & 0;
		float num17;
		ref Vector3 reference;
		float num13;
		float num12;
		float num6;
		float num10;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
		{
			float num3 = ratio;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			float num4 = num3 & 0;
			if (!(num4 > 1f))
			{
				if (_003Cdebug_003Ek__BackingField)
				{
					object[] array = new object[4];
					object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
					_ = parentRotations;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
					object obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if (array != null)
					{
						object obj6 = default(object);
						if (obj6 != null)
						{
							nint num5 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v688 @ rdx_v113 (Il2CppClass<System.Object[]>)+40]");
							obj4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj7 = default(object);
							bool flag2 = obj7 == null;
							obj5 = obj6;
							object obj8 = 0;
							if (flag2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj9 = default(object);
								throw obj9;
							}
						}
						bool flag3 = array.Length <= 0;
						num6 = angle;
						if (!flag3)
						{
							array[0] = obj6;
							obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
							obj5 = 0;
							float num7 = (float)parentRotations * 360f;
							float num8 = num7 + angle;
							num6 = num8 * ratio;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object obj10 = default(object);
							object obj12 = default(object);
							float num11 = default(float);
							ref Vector3 reference2 = default(ref Vector3);
							if (obj10 != null)
							{
								nint num9 = (nint)array;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1442 @ rdx_v111 (Il2CppClass<System.Object[]>)+40]");
								obj4 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj11 = default(object);
								bool flag4 = obj11 == null;
								obj5 = obj10;
								object obj8 = obj12;
								num10 = num11;
								reference = ref reference2;
								num12 = num;
								num13 = num4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1442 @ rdx_v111 (Il2CppClass<System.Object[]>)+40]");
								object obj13 = 0;
								object obj14 = obj10;
								if (flag4)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									object obj15 = default(object);
									throw obj15;
								}
							}
							if (array.Length > 1)
							{
								array[1] = obj10;
								num6 = lastAngle;
								obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 127));
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
								obj5 = 0;
								_ = lastAngle;
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								object obj16 = default(object);
								if (obj16 != null)
								{
									nint num14 = (nint)array;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1775 @ rdx_v109 (Il2CppClass<System.Object[]>)+40]");
									obj4 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj17 = default(object);
									bool flag5 = obj17 == null;
									obj5 = obj16;
									object obj8 = obj12;
									num10 = num11;
									reference = ref reference2;
									num12 = num;
									num13 = num4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1775 @ rdx_v109 (Il2CppClass<System.Object[]>)+40]");
									object obj18 = 0;
									object obj19 = obj16;
									if (flag5)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj20 = default(object);
										throw obj20;
									}
								}
								if (array.Length > 2)
								{
									array[2] = obj16;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
									obj5 = 0;
									obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									object obj21 = default(object);
									if (obj21 != null)
									{
										nint num15 = (nint)array;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1967 @ rdx_v107 (Il2CppClass<System.Object[]>)+40]");
										obj4 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj22 = default(object);
										bool flag6 = obj22 == null;
										obj5 = obj21;
										object obj8 = obj12;
										num10 = num11;
										reference = ref reference2;
										num12 = num;
										num13 = num4;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1967 @ rdx_v107 (Il2CppClass<System.Object[]>)+40]");
										object obj23 = 0;
										object obj24 = obj21;
										if (flag6)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											object obj25 = default(object);
											throw obj25;
										}
									}
									if (array.Length > 3)
									{
										array[3] = obj21;
										string message = string.Format("Parent rotations: {0}. Expected angle: {1}. Last angle: {2} New Angle: {3} - ADDED", array);
										Debug.Log(message);
										goto IL_0547;
									}
								}
							}
						}
						throw new IndexOutOfRangeException();
					}
					throw new NullReferenceException();
				}
				goto IL_0547;
			}
			float num16 = lastAngle - angle;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num17 = num16 & 0;
			bool flag7 = !(num17 > 180f);
			num4 = 180f;
			if (!flag7)
			{
				int num18 = parentRotations + 1;
				float num19 = lastAngle - angle;
				int num20 = parentRotations - 1;
				if (num19 > 180f)
				{
					num20 = num18;
				}
				parentRotations = num20;
				float num21 = MathF.FMod(ratio, 1f);
				num4 = 1f / num21;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				object obj26 = default(object);
				bool flag8 = (nint)obj26 != parentRotations;
				num17 = num4;
				if (!flag8)
				{
					parentRotations = 0;
					num17 = num4;
				}
			}
			if (_003Cdebug_003Ek__BackingField)
			{
				object[] array2 = new object[4];
				object obj27 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
				_ = parentRotations;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
				object obj28 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				if (array2 != null)
				{
					object obj29 = default(object);
					if (obj29 != null)
					{
						nint num22 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1082 @ rdx_v87 (Il2CppClass<System.Object[]>)+40]");
						obj27 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj30 = default(object);
						bool flag9 = obj30 == null;
						obj28 = obj29;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1082 @ rdx_v87 (Il2CppClass<System.Object[]>)+40]");
						object obj4 = 0;
						object obj5 = obj29;
						if (flag9)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj31 = default(object);
							throw obj31;
						}
					}
					if (array2.Length > 0)
					{
						array2[0] = obj29;
						obj27 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
						obj28 = 0;
						float num23 = (float)parentRotations * 360f;
						float num24 = num23 + angle;
						num17 = num24 * ratio;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object obj32 = default(object);
						if (obj32 != null)
						{
							nint num25 = (nint)array2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1592 @ rdx_v85 (Il2CppClass<System.Object[]>)+40]");
							obj27 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj33 = default(object);
							bool flag10 = obj33 == null;
							obj28 = obj32;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1592 @ rdx_v85 (Il2CppClass<System.Object[]>)+40]");
							object obj34 = 0;
							object obj35 = obj32;
							if (flag10)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj36 = default(object);
								throw obj36;
							}
						}
						if (array2.Length > 1)
						{
							array2[1] = obj32;
							num17 = lastAngle;
							obj27 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 127));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
							obj28 = 0;
							_ = lastAngle;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object obj37 = default(object);
							if (obj37 != null)
							{
								nint num26 = (nint)array2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1867 @ rdx_v83 (Il2CppClass<System.Object[]>)+40]");
								obj27 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj38 = default(object);
								bool flag11 = obj38 == null;
								obj28 = obj37;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1867 @ rdx_v83 (Il2CppClass<System.Object[]>)+40]");
								object obj39 = 0;
								object obj40 = obj37;
								if (flag11)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									object obj41 = default(object);
									throw obj41;
								}
							}
							if (array2.Length > 2)
							{
								array2[2] = obj37;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
								obj28 = 0;
								obj27 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								object obj42 = default(object);
								if (obj42 != null)
								{
									nint num27 = (nint)array2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2005 @ rdx_v81 (Il2CppClass<System.Object[]>)+40]");
									obj27 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj43 = default(object);
									bool flag12 = obj43 == null;
									obj28 = obj42;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2005 @ rdx_v81 (Il2CppClass<System.Object[]>)+40]");
									object obj44 = 0;
									object obj45 = obj42;
									if (flag12)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj46 = default(object);
										throw obj46;
									}
								}
								if (array2.Length > 3)
								{
									array2[3] = obj42;
									string message2 = string.Format("Parent rotations: {0}. Expected angle: {1}. Last angle: {2} New Angle: {3} - ADDED", array2);
									Debug.Log(message2);
									ref Vector3 reference2 = ref *(Vector3*)null;
									goto IL_0a4e;
								}
							}
						}
					}
					throw new IndexOutOfRangeException();
				}
				throw new NullReferenceException();
			}
			goto IL_0a4e;
		}
		float num28 = lastAngle - angle;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		num17 = num28 & 0;
		if (num17 > 180f)
		{
			int num29 = parentRotations + 1;
			float num30 = lastAngle - angle;
			int num31 = parentRotations - 1;
			if (num30 > 180f)
			{
				num31 = num29;
			}
			parentRotations = num31;
			float num32 = 1f / ratio;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			object obj47 = default(object);
			bool flag13 = (nint)obj47 != parentRotations;
			num17 = num32;
			if (!flag13)
			{
				parentRotations = 0;
				num17 = num32;
			}
		}
		if (_003Cdebug_003Ek__BackingField)
		{
			object[] array3 = new object[4];
			object obj48 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
			_ = parentRotations;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj49 = default(object);
			if (obj49 != null)
			{
				nint num33 = (nint)array3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v907 @ rdx_v60 (Il2CppClass<System.Object[]>)+40]");
				object obj27 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj50 = default(object);
				bool flag14 = obj50 == null;
				float num4 = 180f;
				object obj28 = obj49;
				if (flag14)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj51 = default(object);
					throw obj51;
				}
			}
			array3[0] = obj49;
			object obj52 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
			float num34 = (float)parentRotations * 360f;
			float num35 = num34 + angle;
			num17 = num35 * ratio;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj53 = default(object);
			if (obj53 != null)
			{
				nint num36 = (nint)array3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1526 @ rdx_v58 (Il2CppClass<System.Object[]>)+40]");
				object obj54 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj55 = default(object);
				bool flag15 = obj55 == null;
				float num4 = 180f;
				object obj56 = obj53;
				if (flag15)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj57 = default(object);
					throw obj57;
				}
			}
			array3[1] = obj53;
			num17 = lastAngle;
			object obj58 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 127));
			_ = lastAngle;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj59 = default(object);
			if (obj59 != null)
			{
				nint num37 = (nint)array3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1849 @ rdx_v56 (Il2CppClass<System.Object[]>)+40]");
				object obj60 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj61 = default(object);
				bool flag16 = obj61 == null;
				float num4 = 180f;
				object obj62 = obj59;
				if (flag16)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj63 = default(object);
					throw obj63;
				}
			}
			array3[2] = obj59;
			object obj64 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj65 = default(object);
			if (obj65 != null)
			{
				nint num38 = (nint)array3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1990 @ rdx_v54 (Il2CppClass<System.Object[]>)+40]");
				object obj66 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj67 = default(object);
				bool flag17 = obj67 == null;
				float num4 = 180f;
				object obj68 = obj65;
				if (flag17)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj69 = default(object);
					throw obj69;
				}
			}
			array3[3] = obj65;
			string message3 = string.Format("Parent rotations: {0}. Expected angle: {1}. Last angle: {2} New Angle: {3} - ADDED", array3);
			Debug.Log(message3);
		}
		Transform transform = base.transform;
		float num39 = (float)parentRotations * 360f;
		float num40 = num39 + angle;
		object obj70 = 0;
		goto IL_100c;
		IL_0547:
		transform = base.transform;
		num40 = angle;
		obj70 = 0;
		goto IL_100c;
		IL_1178:
		reference = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
		object obj71 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		float angle2;
		Quaternion quaternion = Quaternion.Internal_AngleAxis(angle2, ref reference);
		object obj72 = default(object);
		float num41 = (float)obj72 * quaternion.x;
		Quaternion quaternion2;
		object obj73 = (object)quaternion2 * obj72;
		object obj74 = obj72 * obj72;
		float num42 = (float)obj73 + num41;
		object obj75 = obj72 * obj72;
		object obj76 = obj72 * obj72;
		float num43 = num42 + (float)obj76;
		object obj77 = obj72 * obj72;
		float num44 = num43 - (float)obj77;
		object obj78 = obj72 * obj72;
		object obj79 = obj74 + obj78;
		object obj80 = (object)quaternion2 * obj72;
		float num45 = (float)obj72 * quaternion.x;
		object obj81 = obj72 * obj72;
		float num46 = (float)obj79 + num45;
		float num47 = (float)quaternion2 * quaternion.x;
		num13 = (float)quaternion2 * (float)obj72;
		num12 = num46 - (float)obj80;
		object obj82 = obj72 * obj72;
		object obj83 = obj72 * obj72;
		object obj84 = obj75 + obj82;
		object obj85 = obj72 * obj72;
		num6 = (float)obj72 * quaternion.x;
		float num48 = (float)obj83 - num47;
		float num49 = (float)obj84 + num13;
		float num50 = num48 - (float)obj85;
		num10 = num49 - num6;
		float num51 = num50 - (float)obj81;
		Quaternion localRotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-49]");
		_ = 0;
		Transform transform2;
		transform2.localRotation = localRotation;
		lastAngle = angle;
		return;
		IL_0a4e:
		Transform transform3 = base.transform;
		quaternion2 = myInitialRotation;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Gear)+58]");
		_ = 0;
		_ = myInitialUp;
		float num52 = MathF.FMod(ratio, 1f);
		float num53 = angle * ratio;
		float num54 = (float)parentRotations * 360f;
		float num55 = num52 * num54;
		angle2 = num55 + num53;
		transform2 = transform3;
		obj70 = 0;
		goto IL_1178;
		IL_100c:
		angle2 = num40 * ratio;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Gear)+58]");
		_ = 0;
		quaternion2 = myInitialRotation;
		_ = myInitialUp;
		transform2 = transform;
		goto IL_1178;
	}

	public float GetPistonProgress()
	{
		return lastAngle;
	}

	public unsafe float GetAngle()
	{
		//IL_0008: Expected O, but got Ref
		//IL_05c7: Expected I, but got O
		//IL_05d5: Expected O, but got Ref
		//IL_05ea: Expected O, but got Ref
		//IL_0603: Expected F4, but got O
		//IL_06d9: Expected I, but got O
		//IL_06e7: Expected O, but got Ref
		//IL_06f5: Expected O, but got Ref
		//IL_071b: Expected F4, but got I
		//IL_0096: Expected O, but got Ref
		//IL_00ae: Expected O, but got Ref
		//IL_0108: Expected O, but got I
		//IL_0125: Expected O, but got I
		//IL_0142: Expected O, but got I
		//IL_016c: Expected O, but got I
		//IL_0189: Expected O, but got I
		//IL_01a6: Expected O, but got I
		//IL_06b7: Invalid comparison between F4 and I4
		//IL_0413: Expected O, but got Ref
		//IL_042b: Expected O, but got Ref
		//IL_0485: Expected O, but got I
		//IL_04a2: Expected O, but got I
		//IL_04bf: Expected O, but got I
		//IL_04dc: Expected O, but got I
		//IL_0506: Expected O, but got I
		//IL_0523: Expected O, but got I
		//IL_0561: Invalid comparison between F4 and I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj3 = default(object);
		float x;
		float z;
		object obj23;
		object obj24;
		object obj25;
		object obj26;
		object obj27;
		float num16;
		if ((object)parentGear != null)
		{
			Transform parent = parentGear.parent;
			if ((bool)parent)
			{
				if ((object)parentGear != null)
				{
					Transform parent2 = parentGear.parent;
					if ((object)parent2 != null)
					{
						Quaternion rotation = parent2.rotation;
						float num = rotation.x * (float)obj3;
						object obj4 = obj3 * (object)initialParentRotation;
						object obj5 = obj3 * obj3;
						float num2 = (float)obj4 + num;
						object obj6 = obj3 * obj3;
						object obj7 = obj3 * obj3;
						object obj8 = obj3 * obj3;
						float num3 = num2 + (float)obj7;
						object obj9 = obj3 * obj3;
						float num4 = num3 - (float)obj9;
						object obj10 = obj3 * obj3;
						object obj11 = obj8 + obj10;
						float num5 = rotation.x * (float)obj3;
						object obj12 = obj3 * (object)initialParentRotation;
						object obj13 = obj11 + obj12;
						float num6 = rotation.x * (float)initialParentRotation;
						float num7 = rotation.x * (float)obj3;
						float num8 = (float)obj13 - num5;
						object obj14 = obj3 * obj3;
						float num9 = (float)obj6 - num6;
						object obj15 = obj3 * obj3;
						object obj16 = obj5 + obj14;
						object obj17 = obj3 * obj3;
						object obj18 = obj3 * (object)initialParentRotation;
						float num10 = num9 - (float)obj17;
						float num11 = (float)obj16 + num7;
						float num12 = num10 - (float)obj15;
						float num13 = num11 - (float)obj18;
						nint num14 = (nint)typeof(Vector3);
						Vector3 vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
						Quaternion quaternion = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rax_v12 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num15 = 0;
						_ = Vector3.rightVector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
						num16 = 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v457 @ rax_v13 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
						_ = 0;
						Vector3 vector2 = quaternion * vector;
						if ((object)parentGear != null)
						{
							Vector3 right = parentGear.right;
							if ((object)parentGear != null)
							{
								Vector3 up = parentGear.up;
								_ = vector2.x;
								object obj19 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
								_ = vector2.z;
								object obj20 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
								x = up.x;
								z = up.z;
								_ = right.x;
								_ = right.z;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F3260");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-21]");
								nint num17 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-35]");
								object obj21 = num17 * 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-25]");
								nint num18 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-31]");
								object obj22 = num18 * 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-29]");
								nint num19 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-31]");
								obj23 = num19 * 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-21]");
								nint num20 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
								obj24 = num20 * 0;
								obj25 = obj21 - obj22;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-25]");
								nint num21 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
								obj26 = num21 * 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-29]");
								nint num22 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-35]");
								obj27 = num22 * 0;
								goto IL_0646;
							}
						}
					}
				}
			}
			else
			{
				nint num23 = (nint)typeof(Vector3);
				Vector3 vector3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
				_ = initialParentRotation;
				Quaternion quaternion2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v353 @ rax_v21 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num24 = 0;
				num16 = (float)Vector3.rightVector;
				_ = Vector3.rightVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v355 @ rax_v22 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
				_ = 0;
				Vector3 vector4 = quaternion2 * vector3;
				if ((object)parentGear != null)
				{
					Vector3 right2 = parentGear.right;
					if ((object)parentGear != null)
					{
						Vector3 up2 = parentGear.up;
						_ = right2.x;
						object obj28 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
						_ = right2.z;
						object obj29 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
						x = up2.x;
						z = up2.z;
						_ = vector4.x;
						_ = vector4.z;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F3260");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-35]");
						nint num25 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-21]");
						object obj30 = num25 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-31]");
						nint num26 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-25]");
						object obj31 = num26 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
						nint num27 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-21]");
						obj23 = num27 * 0;
						obj25 = obj31 - obj30;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
						nint num28 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-25]");
						obj27 = num28 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-31]");
						nint num29 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-29]");
						obj24 = num29 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-35]");
						nint num30 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-29]");
						obj26 = num30 * 0;
						goto IL_0646;
					}
				}
			}
		}
		goto IL_058f;
		IL_058f:
		throw new NullReferenceException();
		IL_0646:
		object obj32 = obj23 - obj24;
		float num31 = (float)obj25 * x;
		object obj33 = obj26 - obj27;
		object obj34 = obj32 * obj3;
		float num32 = (float)obj33 * z;
		float num33 = num31 + (float)obj34;
		float num34 = num33 + num32;
		float num35 = ((num34 < 0f) ? (-1f) : 1f);
		float num36 = num35 * num16;
		bool flag = (object)parentGear == null;
		num16 = z;
		if (!flag)
		{
			bool flag2 = !(parentGear.lossyScale.x < 0f);
			float num37 = 1f;
			if (!flag2)
			{
				num37 = -1f;
			}
			return num37 * num36;
		}
		goto IL_058f;
	}
}
