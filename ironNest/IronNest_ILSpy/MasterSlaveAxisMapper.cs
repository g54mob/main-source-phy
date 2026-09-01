using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class MasterSlaveAxisMapper : MonoBehaviour
{
	public enum AxisType
	{
		Position,
		Rotation
	}

	public enum Axis
	{
		X,
		Y,
		Z
	}

	public Transform masterObject;

	public AxisType masterAxisType;

	public Axis masterAxis;

	public float masterStart;

	public float masterEnd;

	public Transform slaveObject;

	public Vector3 slavePositionStart;

	public Vector3 slavePositionEnd;

	public Vector3 slaveRotationStart;

	public Vector3 slaveRotationEnd;

	public bool mapPosition;

	public bool mapRotation;

	private unsafe void Update()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0427: Expected I, but got O
		//IL_0447: Expected O, but got I
		//IL_0213: Expected I, but got O
		//IL_0172: Expected F4, but got I
		//IL_017b: Expected O, but got I4
		//IL_0188: Expected Ref, but got F4
		//IL_05e6: Expected O, but got I
		//IL_01b1: Expected O, but got I4
		//IL_0200: Expected I, but got O
		//IL_02cf: Expected F4, but got I4
		//IL_0147: Expected F4, but got I
		//IL_0150: Expected O, but got I4
		//IL_015d: Expected Ref, but got F4
		//IL_04e0: Expected O, but got I
		//IL_04fd: Expected O, but got I
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Expected O, but got Unknown
		//IL_05b8: Expected O, but got I
		//IL_0284: Invalid comparison between O and F4
		//IL_0125: Expected O, but got I4
		//IL_0132: Expected Ref, but got F4
		//IL_01ed: Expected I, but got O
		//IL_02f1: Expected O, but got I
		//IL_00f4: Expected F4, but got I4
		//IL_00fd: Expected O, but got I4
		//IL_010a: Expected Ref, but got F4
		//IL_064c: Expected O, but got I4
		//IL_058a: Expected O, but got I
		//IL_038b: Expected O, but got I
		//IL_0331: Expected O, but got Ref
		//IL_03cb: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (!(masterObject != null) || !(slaveObject != null))
		{
			return;
		}
		float num;
		object obj3;
		if (masterAxisType == AxisType.Position)
		{
			Vector3 localPosition = masterObject.localPosition;
			ref Quaternion reference = ref *(Quaternion*)(int)masterAxis;
			_ = localPosition.x;
			bool flag = masterAxis == Axis.X;
			if (!flag)
			{
				reference = ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref reference, 1);
				if (!flag)
				{
					if (System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference) != (void*)1)
					{
						num = 0f;
						obj3 = 0;
						ref Vector3 reference2 = ref *(Vector3*)localPosition.z;
					}
					else
					{
						num = localPosition.z;
						obj3 = 0;
						ref Vector3 reference2 = ref *(Vector3*)localPosition.z;
					}
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-5]");
					num = 0f;
					obj3 = 0;
					ref Vector3 reference2 = ref *(Vector3*)localPosition.z;
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-9]");
				num = 0f;
				obj3 = 0;
				ref Vector3 reference2 = ref *(Vector3*)localPosition.z;
			}
		}
		else
		{
			Quaternion localRotation = masterObject.localRotation;
			nint num2 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ rdx_v11 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ rax_v17 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
			object obj4 = 0;
			_ = Vector3.rightVector;
			bool flag2 = masterAxis == Axis.X;
			if (!flag2)
			{
				object obj5 = masterAxis - 1;
				if (!flag2)
				{
					if ((nint)obj5 == 1)
					{
						nint num4 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v588 @ rdx_v19 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num5 = 0;
						Vector3 forwardVector = Vector3.forwardVector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v589 @ rax_v29 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
						obj4 = 0;
					}
				}
				else
				{
					nint num6 = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v579 @ rdx_v17 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num7 = 0;
					Vector3 forwardVector = Vector3.upVector;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v583 @ rax_v26 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
					obj4 = 0;
				}
			}
			else
			{
				nint num8 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v495 @ rdx_v15 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num9 = 0;
				Vector3 forwardVector = Vector3.rightVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v499 @ rax_v23 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
				obj4 = 0;
			}
			_ = localRotation.x;
			_ = 0;
			ref float angle = ref System.Runtime.CompilerServices.Unsafe.As<object, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
			_ = 0;
			ref Vector3 reference2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			_ = 0;
			Quaternion.Internal_ToAxisAngleRad(ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7)), out reference2, out angle);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-5]");
			nint num10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-15]");
			object obj6 = num10 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-9]");
			nint num11 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
			object obj7 = num11 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+67]");
			float num12 = 0f * 57.29578f;
			object obj8 = obj7 + obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-1]");
			object obj9 = 0 * obj4;
			object obj10 = obj8 + obj9;
			float num13 = (((nint)obj10 < 0) ? (-1f) : 1f);
			if (num12 > 180f)
			{
				num12 += -360f;
			}
			num = num13 * num12;
			obj3 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj11 = default(object);
		float num16;
		if (obj11 == null)
		{
			float num14 = num - masterStart;
			float num15 = masterEnd - masterStart;
			num16 = num14 / num15;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num16))
			{
				if (num16 > 1f)
				{
					num16 = 1f;
				}
				goto IL_060b;
			}
		}
		num16 = 0f;
		goto IL_060b;
		IL_060b:
		if (mapPosition)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MasterSlaveAxisMapper)+54]");
			nint num17 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MasterSlaveAxisMapper)+48]");
			object obj12 = num17 - 0;
			_ = slavePositionStart;
			float num18 = (float)obj12 * num16;
			float num19 = num18;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MasterSlaveAxisMapper)+48]");
			float num20 = num19 + 0f;
			Vector3 localPosition2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			slaveObject.localPosition = localPosition2;
		}
		if (mapRotation)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MasterSlaveAxisMapper)+6C]");
			nint num21 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MasterSlaveAxisMapper)+60]");
			object obj13 = num21 - 0;
			_ = slaveRotationStart;
			float num22 = (float)obj13 * num16;
			float num23 = num22;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MasterSlaveAxisMapper)+60]");
			float num24 = num23 + 0f;
			Vector3 localEulerAngles = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			slaveObject.localEulerAngles = localEulerAngles;
		}
	}

	private float Map01Clamped(float value, float start, float end)
	{
		//IL_0025: Expected F4, but got I4
		//IL_0069: Invalid comparison between I4 and F4
		//IL_00b4: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		bool flag = obj != null;
		float result = 0f;
		if (!flag)
		{
			float num = value - start;
			float num2 = end - start;
			float num3 = num / num2;
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
			result = num3;
		}
		return result;
	}

	private float GetLocalAxis(Vector3 vec, Axis axis)
	{
		//IL_002b: Expected O, but got I4
		//IL_005c: Expected F4, but got I4
		bool flag = axis == Axis.X;
		if (!flag)
		{
			object obj = axis - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					return 0f;
				}
				return vec.z;
			}
			return vec.y;
		}
		return vec.x;
	}

	private float GetLocalRotationSigned(Transform t, Axis axis)
	{
		//IL_00d0: Expected I, but got O
		//IL_00f0: Expected O, but got I
		//IL_008c: Expected I, but got O
		//IL_0213: Expected O, but got I
		//IL_0021: Expected O, but got I4
		//IL_0079: Expected I, but got O
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_01e5: Expected O, but got I
		//IL_0066: Expected I, but got O
		//IL_01b7: Expected O, but got I
		Quaternion localRotation = t.localRotation;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rdx_v1 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
		object obj = 0;
		bool flag = axis == Axis.X;
		Vector3 vector;
		Vector3 vector2;
		if (!flag)
		{
			object obj2 = axis - 1;
			if (!flag)
			{
				bool flag2 = (nint)obj2 != 1;
				vector = Vector3.rightVector;
				if (flag2)
				{
					goto IL_010d;
				}
				nint num3 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ rdx_v9 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num4 = 0;
				vector2 = Vector3.forwardVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ rax_v16 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
				obj = 0;
			}
			else
			{
				nint num5 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rdx_v7 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num6 = 0;
				vector2 = Vector3.upVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rax_v13 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
				obj = 0;
			}
		}
		else
		{
			nint num7 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rdx_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num8 = 0;
			vector2 = Vector3.rightVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ rax_v10 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
			obj = 0;
		}
		vector = vector2;
		goto IL_010d;
		IL_010d:
		Quaternion q = default(Quaternion);
		Quaternion.Internal_ToAxisAngleRad(ref q, out Vector3 axis2, out float _);
		object obj4 = default(object);
		object obj5 = default(object);
		object obj3 = obj4 * obj5;
		object obj6 = (object)axis2 * (object)vector;
		float num9 = 0f * 57.29578f;
		object obj7 = obj6 + obj3;
		object obj8 = 0 * obj;
		object obj9 = obj7 + obj8;
		float num10 = (((nint)obj9 < 0) ? (-1f) : 1f);
		if (num9 > 180f)
		{
			num9 += -360f;
		}
		return num10 * num9;
	}

	private unsafe float GetSignedAngle(Quaternion q, Vector3 axis)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_007c: Invalid comparison between F4 and I4
		Quaternion.Internal_ToAxisAngleRad(ref *(Quaternion*)q, out Vector3 axis2, out float angle);
		float num = angle * 57.29578f;
		float num2 = (float)axis2 * axis.x;
		object obj2 = default(object);
		object obj3 = default(object);
		object obj = obj2 * obj3;
		float num3 = (float)obj + num2;
		object obj4 = 0 * axis.z;
		float num4 = num3 + (float)obj4;
		float num5 = ((num4 < 0f) ? (-1f) : 1f);
		if (num > 180f)
		{
			num += -360f;
		}
		return num5 * num;
	}

	public MasterSlaveAxisMapper()
	{
		//IL_001e: Expected I, but got O
		//IL_0094: Expected I, but got O
		//IL_0059: Expected I, but got O
		//IL_00cf: Expected I, but got O
		masterEnd = 1f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		slavePositionStart = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		slavePositionEnd = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num5 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v8 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num6 = 0;
		slaveRotationStart = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rdx_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num7 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v11 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num8 = 0;
		slaveRotationEnd = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rcx_v6 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		mapPosition = true;
		base._002Ector();
	}
}
