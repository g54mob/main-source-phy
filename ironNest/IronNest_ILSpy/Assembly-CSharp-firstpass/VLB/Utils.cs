using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public static class Utils
{
	public enum FloatPackingPrecision
	{
		High = 64,
		Low = 8,
		Undef = 0
	}

	private const float kEpsilon = 1E-05f;

	private static FloatPackingPrecision ms_FloatPackingPrecision;

	private const int kFloatPackingHighMinShaderLevel = 35;

	public static float ComputeConeRadiusEnd(float fallOffEnd, float spotAngle)
	{
		float num = spotAngle * ((float)Math.PI / 180f);
		float num2 = num * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
		return num2 * fallOffEnd;
	}

	public static float ComputeSpotAngle(float fallOffEnd, float coneRadiusEnd)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
		float num = coneRadiusEnd * 57.29578f;
		return num + num;
	}

	public unsafe static void Swap<T>(ref T a, ref T b)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0038: Expected O, but got I
		//IL_00d2: Expected O, but got I
		//IL_0111: Expected O, but got I
		//IL_0150: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r9_v1 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r9_v1 (Il2CppClass<T>)+FC]");
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r9_v1 (Il2CppClass<T>)+FC]");
			object obj4 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r9_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj4 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r9_v1 (Il2CppClass<T>)+FC]");
			object obj5 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r9_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj5 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r9_v1 (Il2CppClass<T>)+FC]");
			object obj6 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r9_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj6 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public static void ResizeArray<T>(ref T[] array, int newSize)
	{
		Array.Resize(ref array, newSize);
	}

	public static bool IsValidIndex<T>(T[] array, int idx)
	{
		//IL_00bd: Expected I4, but got O
		//IL_0052: Expected O, but got I4
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected I4, but got Unknown
		if (idx < 0)
		{
			return false;
		}
		if (array != null)
		{
			object obj = idx - array.Length;
			int num = idx ^ array.Length;
			int num2 = idx ^ obj;
			int num3 = num & num2;
			bool flag = num3 < 0;
			bool flag2 = (nint)obj < 0;
			return flag2 != flag;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static string GetPath(Transform current)
	{
		if ((object)current != null)
		{
			Transform parent = current.parent;
			if (parent != null)
			{
				Transform parent2 = current.parent;
				string path = GetPath(parent2);
				string name = current.name;
				return path + "/" + name;
			}
			string name2 = current.name;
			return "/" + name2;
		}
		return (string)(object)new NullReferenceException();
	}

	public static T NewWithComponent<T>(string name)
	{
		//IL_0047: Expected O, but got I
		//IL_0065: Expected O, but got I
		//IL_0098: Expected I, but got O
		//IL_00a8: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		Type[] array = new Type[1];
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)0);
		bool flag = array == null;
		string text = null;
		RuntimeTypeHandle runtimeTypeHandle = (RuntimeTypeHandle)0;
		if (!flag)
		{
			if ((object)typeFromHandle != null)
			{
				nint num = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rdx_v11 (Il2CppClass<System.Type[]>)+40]");
				text = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj = default(object);
				bool flag2 = obj == null;
				runtimeTypeHandle = (RuntimeTypeHandle)typeFromHandle;
				if (flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj2 = default(object);
					throw obj2;
				}
			}
			if (array.Length <= 0)
			{
				return (T)new IndexOutOfRangeException();
			}
			array[0] = typeFromHandle;
			GameObject gameObject = new GameObject(name, array);
			bool flag3 = (object)gameObject == null;
			text = name;
			runtimeTypeHandle = (RuntimeTypeHandle)gameObject;
			if (!flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				T result = default(T);
				return result;
			}
		}
		throw new NullReferenceException();
	}

	public static T GetOrAddComponent<T>(GameObject self)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		if ((object)self != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			UnityEngine.Object obj = default(UnityEngine.Object);
			bool flag = obj == null;
			bool flag2 = !flag;
			UnityEngine.Object result = obj;
			if (!flag2)
			{
				UnityEngine.Object obj2 = (UnityEngine.Object)self.AddComponent<T>();
				result = obj2;
			}
			return (T)result;
		}
		return (T)new NullReferenceException();
	}

	public static T GetOrAddComponent<T>(MonoBehaviour self)
	{
		if ((object)self != null)
		{
			GameObject gameObject = self.gameObject;
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rbx_v3 (Il2CppMethodInfo)+38]");
			if ((nint)0 == 0)
			{
			}
			if ((object)gameObject != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				UnityEngine.Object obj = default(UnityEngine.Object);
				bool flag = obj == null;
				bool flag2 = !flag;
				UnityEngine.Object result = obj;
				if (!flag2)
				{
					UnityEngine.Object obj2 = (UnityEngine.Object)gameObject.AddComponent<T>();
					result = obj2;
				}
				return (T)result;
			}
		}
		return (T)new NullReferenceException();
	}

	public static void ForeachComponentsInAnyChildrenOnly<T>(GameObject self, Action<T> lambda, bool includeInactive = false)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_005a: Expected O, but got I4
		//IL_0063: Expected O, but got I4
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		T[] componentsInChildren = self.GetComponentsInChildren<T>(includeInactive);
		object obj = componentsInChildren + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < componentsInChildren.Length)
		{
			GameObject gameObject = ((Component)obj).gameObject;
			if (gameObject != self)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [lambda @ rdx (System.Action`1<T>)+18] (should have been resolved before IL gen)");
			}
			obj3++;
			obj += 8;
			obj2 = obj3;
		}
	}

	public static void ForeachComponentsInDirectChildrenOnly<T>(GameObject self, Action<T> lambda, bool includeInactive = false)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_005a: Expected O, but got I4
		//IL_0063: Expected O, but got I4
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		T[] componentsInChildren = self.GetComponentsInChildren<T>(includeInactive);
		object obj = componentsInChildren + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < componentsInChildren.Length)
		{
			Transform transform = ((Component)obj).transform;
			Transform parent = transform.parent;
			Transform transform2 = self.transform;
			if (parent == transform2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [lambda @ rdx (System.Action`1<T>)+18] (should have been resolved before IL gen)");
			}
			obj3++;
			obj += 8;
			obj2 = obj3;
		}
	}

	public unsafe static void SetupDepthCamera(Camera depthCamera, float coneApexOffsetZ, float maxGeometryDistance, float coneRadiusStart, float coneRadiusEnd, Vector3 beamLocalForward, Vector3 lossyScale, bool isScalable, Quaternion beamInternalLocalRotation, bool shouldScaleMinNearClipPlane)
	{
		//IL_02de: Invalid comparison between F4 and I4
		//IL_02f8: Invalid comparison between F4 and I4
		//IL_0043: Expected O, but got I4
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0051: Expected F4, but got I4
		//IL_00ba: Expected O, but got Ref
		//IL_0189: Expected O, but got Ref
		//IL_01a6: Expected O, but got I
		//IL_0177: Expected F4, but got I4
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Expected O, but got Unknown
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Expected O, but got Unknown
		//IL_0383: Invalid comparison between F4 and O
		//IL_01b4: Expected O, but got I4
		//IL_01da: Expected F4, but got I4
		//IL_03c3: Expected F4, but got I
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Expected O, but got Unknown
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Expected F4, but got Unknown
		//IL_027b: Expected O, but got I
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Expected O, but got Unknown
		object obj = default(object);
		object obj2 = default(object);
		if (obj == null)
		{
			_ = 1065353216;
			obj2 = 1065353216;
		}
		bool flag = coneApexOffsetZ < 0f;
		bool flag2 = !flag;
		bool flag3 = coneApexOffsetZ > 0f;
		float num = coneApexOffsetZ;
		if (!flag3)
		{
			num = 0f;
		}
		bool orthographic = (byte)((flag2 ? 1u : 0u) ^ 1u) != 0;
		depthCamera.orthographic = orthographic;
		Transform transform = depthCamera.transform;
		float num2 = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj3 = num2 ^ 0;
		object obj4 = default(object);
		float num3 = (float)obj4 * (float)obj3;
		Vector3 euler = default(Vector3);
		transform.localPosition = (Vector3)(&euler);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ stack_38+8]");
		bool flag4 = (nint)0 >= (nint)0;
		object obj5 = obj4;
		float num4 = maxGeometryDistance;
		if (!flag4)
		{
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			object obj7 = default(object);
			object obj6 = obj7 * obj4;
			object obj9 = default(object);
			object obj8 = obj9 * obj4;
			object obj10 = default(object);
			float num5 = (float)obj10 * quaternion.x;
			float num6 = (float)obj6 - num5;
			float num7 = num6 - (float)obj8;
			object obj12 = default(object);
			object obj11 = obj12 * obj4;
			num3 = num7 - (float)obj11;
			obj5 = obj4;
			num4 = (isScalable ? 1 : 0);
		}
		Transform transform2 = depthCamera.transform;
		object obj13 = default(object);
		transform2.localRotation = (Quaternion)(&obj13);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ stack_38+8]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ stack_38+4]");
		object obj14 = num8 * 0;
		object obj15 = 0 - obj14;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj16 = obj14 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj17 = obj15 & 0;
		if ((nint)obj16 <= 0)
		{
			obj16 = 0;
		}
		float num9 = (float)obj16 * 1E-06f;
		float num10 = Mathf.Epsilon * 8f;
		if (!(num9 > num10))
		{
			num9 = num10;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num9) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17))
		{
			bool flag5 = !flag2;
			float num11 = 0f;
			if (!flag5)
			{
				num11 = 0.1f;
			}
			object obj18 = default(object);
			bool flag6 = obj18 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ stack_38+8]");
			nint num12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			float num13 = num12 & 0;
			float num14 = (flag6 ? 1f : num13);
			float num15 = num14 * num11;
			float num16 = num * num13;
			if (!(num16 > num15))
			{
				num16 = num15;
			}
			depthCamera.nearClipPlane = num16;
			bool flag7 = obj != null;
			float num17 = 1f;
			if (!flag7)
			{
				num17 = num13;
				num13 = 1f;
			}
			float num18 = num * num17;
			float num19 = num18 + num4;
			float farClipPlane = num19 * num13;
			depthCamera.farClipPlane = farClipPlane;
			object obj19 = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ stack_38+4]");
			object obj20 = obj19 / 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			float aspect = obj20 & 0;
			depthCamera.aspect = aspect;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ stack_38+4]");
				float orthographicSize = 0f * coneRadiusStart;
				depthCamera.orthographicSize = orthographicSize;
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ stack_38+4]");
			nint num20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj21 = num20 & 0;
			object obj22 = obj21 * shouldScaleMinNearClipPlane;
			float farClipPlane2 = depthCamera.farClipPlane;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
			float num21 = (float)obj22 * 57.29578f;
			float fieldOfView = num21 + num21;
			depthCamera.fieldOfView = fieldOfView;
		}
	}

	public static bool HasFlag(Enum mask, Enum flags)
	{
		//IL_0010: Expected O, but got I
		//IL_0035: Expected I, but got O
		//IL_007e: Expected O, but got I
		//IL_00ab: Expected I, but got O
		//IL_00f4: Expected I, but got O
		//IL_0104: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		object obj = 0;
		Enum obj2 = default(Enum);
		if (obj2 != null)
		{
			nint num = (nint)obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ r8_v2 (Il2CppClass<System.Enum>)+40]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rdx_v3+40]");
			if (num2 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
				obj = 0;
				bool flag = flags == null;
				IntPtr intPtr = num;
				if (flag)
				{
					goto IL_017b;
				}
				nint num3 = (nint)flags;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v142 @ rcx_v4 (Il2CppClass<System.Enum>)+40]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rdx_v3+40]");
				bool flag2 = num4 != 0;
				obj2 = flags;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
					nint num5 = (nint)flags;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
					obj = 0;
					object obj4 = default(object);
					object obj5 = default(object);
					object obj3 = obj4 & obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v191 @ rcx_v6 (Il2CppClass<System.Enum>)+40]");
					nint num6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rdx_v3+40]");
					bool flag3 = num6 != 0;
					obj2 = flags;
					intPtr = num;
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
						object obj7 = default(object);
						object obj6 = obj3 - obj7;
						return obj6 == null;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					num = intPtr;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			bool result = default(bool);
			return result;
		}
		goto IL_017b;
		IL_017b:
		throw new NullReferenceException();
	}

	public unsafe static Vector3 Divide(Vector3 aVector, Vector3 scale)
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_0116: Invalid comparison between F4 and O
		//IL_003b: Expected O, but got I4
		//IL_013d: Expected I, but got O
		//IL_015b: Expected F4, but got O
		//IL_0156: Expected native int or pointer, but got O
		//IL_0170: Expected F4, but got I
		//IL_016b: Expected native int or pointer, but got O
		//IL_0087: Expected native int or pointer, but got O
		//IL_00ad: Expected native int or pointer, but got O
		//IL_00ba: Expected native int or pointer, but got O
		float num = scale.y * scale.x;
		float num2 = num * scale.z;
		float num3 = 0f - num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num2 & 0;
		if ((nint)obj < 0)
		{
			obj = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num3 & 0;
		float num4 = Mathf.Epsilon * 8f;
		float num5 = (float)obj * 1E-06f;
		if (num5 < num4)
		{
			num5 = num4;
		}
		Vector3 vector = default(Vector3);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
		{
			float x = aVector.x / scale.x;
			float y = aVector.y / scale.y;
			((Vector3*)(nint)vector)->x = x;
			float z = aVector.z / scale.z;
			((Vector3*)(nint)vector)->y = y;
			((Vector3*)(nint)vector)->z = z;
		}
		else
		{
			nint num6 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rax_v6 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num7 = 0;
			((Vector3*)(nint)vector)->x = (float)Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ rax_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			((Vector3*)(nint)vector)->z = 0f;
		}
		return vector;
	}

	public static Vector2 xy(Vector3 aVector)
	{
		Vector2 result = default(Vector2);
		return result;
	}

	public static Vector2 xz(Vector3 aVector)
	{
		Vector2 result = default(Vector2);
		return result;
	}

	public static Vector2 yz(Vector3 aVector)
	{
		Vector2 result = default(Vector2);
		return result;
	}

	public static Vector2 yx(Vector3 aVector)
	{
		Vector2 result = default(Vector2);
		return result;
	}

	public static Vector2 zx(Vector3 aVector)
	{
		Vector2 result = default(Vector2);
		return result;
	}

	public static Vector2 zy(Vector3 aVector)
	{
		Vector2 result = default(Vector2);
		return result;
	}

	public static bool Approximately(float a, float b, float epsilon = 1E-05f)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_002c: Invalid comparison between F4 and O
		//IL_004a: Invalid comparison between F4 and I4
		float num = a - b;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num & 0;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)epsilon) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		float num2 = epsilon - (float)obj;
		bool flag2 = num2 == 0f;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		return flag4 & flag3;
	}

	public static bool Approximately(Vector2 a, Vector2 b, float epsilon = 1E-05f)
	{
		//IL_0049: Invalid comparison between F4 and O
		//IL_0067: Invalid comparison between F4 and I4
		object obj2 = default(object);
		object obj3 = default(object);
		object obj = obj2 - obj3;
		object obj4 = a - b;
		object obj5 = obj * obj;
		object obj6 = obj4 * obj4;
		object obj7 = obj5 + obj6;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)epsilon) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7);
		float num = epsilon - (float)obj7;
		bool flag2 = num == 0f;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		return flag4 & flag3;
	}

	public static bool Approximately(Vector3 a, Vector3 b, float epsilon = 1E-05f)
	{
		//IL_00b7: Invalid comparison between F4 and I4
		float num = a.x - b.x;
		float num2 = a.z - b.z;
		object obj = default(object);
		float num3 = a.y - (float)obj;
		float num4 = num * num;
		float num5 = num2 * num2;
		float num6 = num3 * num3;
		float num7 = num6 + num4;
		float num8 = num7 + num5;
		bool flag = epsilon < num8;
		float num9 = epsilon - num8;
		bool flag2 = num9 == 0f;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		return flag4 & flag3;
	}

	public static bool Approximately(Vector4 a, Vector4 b, float epsilon = 1E-05f)
	{
		//IL_00c9: Invalid comparison between F4 and I4
		float num = a.x - b.x;
		object obj2 = default(object);
		object obj = obj2 - obj2;
		object obj3 = obj2 - obj2;
		float num2 = num * num;
		object obj4 = obj * obj;
		object obj5 = obj2 - obj2;
		object obj6 = obj3 * obj3;
		float num3 = (float)obj4 + num2;
		object obj7 = obj5 * obj5;
		float num4 = num3 + (float)obj6;
		float num5 = num4 + (float)obj7;
		bool flag = epsilon < num5;
		float num6 = epsilon - num5;
		bool flag2 = num6 == 0f;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		return flag4 & flag3;
	}

	public unsafe static Vector4 AsVector4(Vector3 vec3, float w)
	{
		//IL_000d: Expected native int or pointer, but got O
		//IL_001f: Expected native int or pointer, but got O
		//IL_0031: Expected native int or pointer, but got O
		//IL_003e: Expected native int or pointer, but got O
		Vector4 vector = default(Vector4);
		((Vector4*)(nint)vector)->x = vec3.x;
		((Vector4*)(nint)vector)->y = vec3.y;
		((Vector4*)(nint)vector)->z = vec3.z;
		((Vector4*)(nint)vector)->w = w;
		return vector;
	}

	public unsafe static Vector4 PlaneEquation(Vector3 normalizedNormal, Vector3 pt)
	{
		//IL_0008: Expected native int or pointer, but got O
		Vector4 vector = default(Vector4);
		float x = default(float);
		((Vector4*)(nint)vector)->x = x;
		return vector;
	}

	public static float GetVolumeCubic(Bounds self)
	{
		//IL_0034: Expected O, but got I
		//IL_0051: Expected O, but got I
		object obj = self.m_Extents + self.m_Extents;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+10]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+10]");
		object obj2 = num + 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+14]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+14]");
		object obj3 = num2 + 0;
		object obj4 = obj2 * obj;
		return (float)obj4 * (float)obj3;
	}

	public static float GetMaxArea2D(Bounds self)
	{
		//IL_0034: Expected O, but got I
		//IL_0051: Expected O, but got I
		//IL_007d: Expected O, but got I
		//IL_009a: Expected O, but got I
		object obj = self.m_Extents + self.m_Extents;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+10]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+10]");
		object obj2 = num + 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+14]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+14]");
		object obj3 = num2 + 0;
		float num3 = (float)obj2 * (float)obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+10]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+10]");
		object obj4 = num4 + 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+14]");
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [self @ rcx (UnityEngine.Bounds)+14]");
		object obj5 = num5 + 0;
		object obj6 = self.m_Extents + self.m_Extents;
		float num6 = (float)obj4 * (float)obj3;
		float num7 = (float)obj5 * (float)obj6;
		if (num3 < num6)
		{
			num3 = num6;
		}
		if (num3 < num7)
		{
			num3 = num7;
		}
		return num3;
	}

	public unsafe static Color Opaque(Color self)
	{
		//IL_000d: Expected native int or pointer, but got O
		//IL_001f: Expected native int or pointer, but got O
		//IL_0031: Expected native int or pointer, but got O
		//IL_003f: Expected native int or pointer, but got O
		Color color = default(Color);
		((Color*)(nint)color)->r = self.r;
		((Color*)(nint)color)->g = self.g;
		((Color*)(nint)color)->b = self.b;
		((Color*)(nint)color)->a = 1f;
		return color;
	}

	public unsafe static Color ComputeComplementaryColor(Color self, bool blackAndWhite)
	{
		//IL_0010: Expected F4, but got I
		//IL_0025: Expected F4, but got I
		//IL_0020: Expected native int or pointer, but got O
		//IL_0041: Expected O, but got I4
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		float num = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		Color color = default(Color);
		((Color*)(nint)color)->r = 0f;
		int num2 = 0;
		while (true)
		{
			bool flag = num2 == 0;
			float num3;
			if (!flag)
			{
				object obj = num2 - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						if ((nint)obj2 != 1)
						{
							break;
						}
						num3 = self.a;
					}
					else
					{
						num3 = self.b;
					}
				}
				else
				{
					num3 = self.g;
				}
			}
			else
			{
				num3 = self.r;
			}
			num = ((!(0.5f > num3)) ? (-0.5f) : 0.5f);
			float value = num3 + num;
			((Color*)color)->set_Item(num2, value);
			num2++;
			if (num2 >= 3)
			{
				return color;
			}
		}
		int num4 = default(int);
		string text = num4.ToString();
		string message = "Invalid Color index(" + text + ")!";
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public unsafe static Plane TranslateCustom(Plane plane, Vector3 translation)
	{
		//IL_0045: Invalid comparison between O and F4
		//IL_006e: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		Vector3 vector = default(Vector3);
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			float num = translation.y / (float)vector;
			float num2 = translation.z / (float)vector;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		Plane plane2 = default(Plane);
		Vector3 normal = default(Vector3);
		((Plane*)(nint)plane2)->m_Normal = normal;
		return plane2;
	}

	public unsafe static Vector3 ClosestPointOnPlaneCustom(Plane plane, Vector3 point)
	{
		//IL_00b1: Expected native int or pointer, but got O
		//IL_00be: Expected native int or pointer, but got O
		float num = point.x * (float)plane.m_Normal;
		float num3 = default(float);
		float num2 = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [plane @ rdx (UnityEngine.Plane)+4]");
		float num4 = num2 * 0f;
		float num5 = num4 + num;
		float num6 = point.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [plane @ rdx (UnityEngine.Plane)+8]");
		float num7 = num6 * 0f;
		float num8 = num5 + num7;
		float num9 = num8 + plane.m_Distance;
		float num10 = num9;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [plane @ rdx (UnityEngine.Plane)+8]");
		float num11 = num10 * 0f;
		float z = point.z - num11;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = num3;
		((Vector3*)(nint)vector)->z = z;
		return vector;
	}

	public static bool IsAlmostZero(float f)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_001e: Invalid comparison between F4 and O
		//IL_003d: Invalid comparison between F4 and I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = f & 0;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		float num = 0.001f - (float)obj;
		bool flag2 = num == 0f;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		return flag4 & flag3;
	}

	public static bool IsValid(Plane plane)
	{
		//IL_0041: Expected O, but got I
		//IL_0064: Invalid comparison between O and F4
		//IL_0083: Invalid comparison between F4 and I4
		object obj2 = default(object);
		object obj = obj2 * obj2;
		object obj3 = (object)plane.m_Normal * (object)plane.m_Normal;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [plane @ rcx (UnityEngine.Plane)+8]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [plane @ rcx (UnityEngine.Plane)+8]");
		object obj4 = num * 0;
		object obj5 = obj + obj3;
		object obj6 = obj5 + obj4;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.5f);
		float num2 = (float)obj6 - 0.5f;
		bool flag2 = num2 == 0f;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		return flag4 & flag3;
	}

	public static void SetKeywordEnabled(Material mat, string name, bool enabled)
	{
		if (!enabled)
		{
			mat.DisableKeyword(name);
		}
		else
		{
			mat.EnableKeyword(name);
		}
	}

	public static void SetShaderKeywordEnabled(string name, bool enabled)
	{
		if (!enabled)
		{
			Shader.DisableKeyword(name);
		}
		else
		{
			Shader.EnableKeyword(name);
		}
	}

	public unsafe static Matrix4x4 SampleInMatrix(Gradient self, int floatPackingPrecision)
	{
		//IL_0026: Expected O, but got I8
		//IL_0039: Expected native int or pointer, but got O
		//IL_0047: Expected native int or pointer, but got O
		//IL_005a: Expected native int or pointer, but got O
		//IL_006d: Expected native int or pointer, but got O
		//IL_025a: Invalid comparison between I4 and F4
		//IL_00b3: Expected F4, but got I4
		//IL_01bb: Expected O, but got I4
		//IL_01d5: Expected O, but got I8
		object obj = 6442450944L;
		Matrix4x4 matrix4x = default(Matrix4x4);
		((Matrix4x4*)(nint)matrix4x)->m00 = 0f;
		((Matrix4x4*)(nint)matrix4x)->m01 = 0f;
		((Matrix4x4*)(nint)matrix4x)->m02 = 0f;
		((Matrix4x4*)(nint)matrix4x)->m03 = 0f;
		float num = 0f / 15f;
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
		if (self != null)
		{
			Color color = self.Evaluate(num);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm6,eax\"");
			float x = color.r * 0f;
			float num2 = MathF.Floor(x);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,ebp\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm1,ebp\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,ebp\"");
			object obj2 = default(object);
			float x2 = (float)obj2 * 0f;
			float num3 = MathF.Floor(x2);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,ebp\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm1,ebp\"");
			float num4 = num3 * 0f;
			float x3 = (float)obj2 * 0f;
			float num5 = num4 * 0f;
			float num6 = MathF.Floor(x3);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm1,ebp\"");
			float x4 = (float)obj2 * 0f;
			float num7 = MathF.Floor(x4);
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ r14_v1+39C940+v151 @ rax_v11*4]");
			object obj4 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v154 @ rcx_v9 (should have been resolved before IL gen)");
		}
		throw new NullReferenceException();
	}

	public static Color[] SampleInArray(Gradient self, int samplesCount)
	{
		//IL_0013: Expected O, but got I4
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_002a: Expected O, but got I4
		//IL_013f: Invalid comparison between I4 and F4
		//IL_0066: Expected F4, but got I4
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00bd: Expected O, but got F4
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		Color[] array = new Color[samplesCount];
		if (samplesCount > 0)
		{
			object obj = samplesCount - 1;
			object obj2 = array + 32;
			object obj3 = 0;
			do
			{
				float num = (float)obj3 / (float)obj;
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
				Color color = self.Evaluate(num);
				if ((nint)obj3 < array.Length)
				{
					obj3++;
					obj2 = color.r;
					obj2 += 16;
					continue;
				}
				return (Color[])(object)new IndexOutOfRangeException();
			}
			while ((nint)obj3 < samplesCount);
		}
		return array;
	}

	private unsafe static Vector4 Vector4_Floor(Vector4 vec)
	{
		//IL_001a: Expected native int or pointer, but got O
		//IL_003e: Expected native int or pointer, but got O
		//IL_005d: Expected native int or pointer, but got O
		//IL_0081: Expected native int or pointer, but got O
		float x = MathF.Floor(vec.x);
		Vector4 vector = default(Vector4);
		((Vector4*)(nint)vector)->x = x;
		float y = MathF.Floor(vec.y);
		((Vector4*)(nint)vector)->y = y;
		float z = MathF.Floor(vec.z);
		((Vector4*)(nint)vector)->z = z;
		float w = MathF.Floor(vec.w);
		((Vector4*)(nint)vector)->w = w;
		return vector;
	}

	public static float PackToFloat(Color color, int floatPackingPrecision)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm7,eax\"");
		float x = 0f * color.r;
		float num = MathF.Floor(x);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,ebx\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm1,ebx\"");
		float num2 = num * 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,ebx\"");
		float num3 = num2 * 0f;
		float num4 = num3 * 0f;
		object obj = default(object);
		float x2 = 0f * (float)obj;
		float num5 = MathF.Floor(x2);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm1,ebx\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm2,ebx\"");
		float num6 = num5 * 0f;
		float num7 = num6 * 0f;
		float num8 = num4 + num7;
		float x3 = 0f * (float)obj;
		float num9 = MathF.Floor(x3);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm1,ebx\"");
		float x4 = 0f * (float)obj;
		float num10 = num9 * 0f;
		float num11 = num8 + num10;
		float num12 = MathF.Floor(x4);
		return num11 + num12;
	}

	public static FloatPackingPrecision GetFloatPackingPrecision()
	{
		if (ms_FloatPackingPrecision == FloatPackingPrecision.Undef)
		{
			int graphicsShaderLevel = SystemInfo.graphicsShaderLevel;
			bool flag = graphicsShaderLevel >= 35;
			FloatPackingPrecision floatPackingPrecision = FloatPackingPrecision.High;
			if (!flag)
			{
				floatPackingPrecision = FloatPackingPrecision.Low;
			}
			ms_FloatPackingPrecision = floatPackingPrecision;
		}
		return ms_FloatPackingPrecision;
	}

	public static bool HasAtLeastOneFlag(Enum mask, Enum flags)
	{
		//IL_0010: Expected O, but got I
		//IL_0035: Expected I, but got O
		//IL_007e: Expected O, but got I
		//IL_00a3: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		object obj = 0;
		Enum obj2 = default(Enum);
		if (obj2 != null)
		{
			nint num = (nint)obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ r8_v2 (Il2CppClass<System.Enum>)+40]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rdx_v2+40]");
			if (num2 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
				obj = 0;
				if (flags == null)
				{
					goto IL_0108;
				}
				nint num3 = (nint)flags;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rcx_v3 (Il2CppClass<System.Enum>)+40]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rdx_v2+40]");
				if (num4 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
					object obj4 = default(object);
					object obj5 = default(object);
					object obj3 = obj4 & obj5;
					bool flag = obj3 == null;
					return !flag;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			bool result = default(bool);
			return result;
		}
		goto IL_0108;
		IL_0108:
		throw new NullReferenceException();
	}

	public static void MarkCurrentSceneDirty()
	{
	}

	public static void MarkObjectDirty(UnityEngine.Object obj)
	{
	}
}
