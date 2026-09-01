using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public static class MeshGenerator
{
	public enum CapMode
	{
		None,
		OneVertexPerCap_1Cap,
		OneVertexPerCap_2Caps,
		SpecificVerticesPerCap_1Cap,
		SpecificVerticesPerCap_2Caps
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public int numSides;

		public int vertCountSides;

		public Func<int, int> vertSidesStartFromSlide;

		public Func<int, int> vertCenterFromSlide;

		internal int _003CGenerateConeZ_Radii_DoubleCaps_003Eb__0(int slideID)
		{
			return slideID * numSides;
		}

		internal int _003CGenerateConeZ_Radii_DoubleCaps_003Eb__1(int slideID)
		{
			return vertCountSides + slideID;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_1
	{
		public int[] indices;

		public int ind;

		public _003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals1;

		internal void _003CGenerateConeZ_Radii_DoubleCaps_003Eb__2(int slideID, bool invert)
		{
			//IL_003e: Unsupported input type for neg.
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			//IL_0054: Expected O, but got I4
			//IL_02a5: Expected O, but got I4
			//IL_01ac: Expected I4, but got O
			//IL_01c4: Expected O, but got I4
			//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d2: Expected O, but got Unknown
			//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e6: Expected O, but got Unknown
			//IL_0093: Expected I4, but got O
			//IL_00ab: Expected O, but got I4
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Expected O, but got Unknown
			//IL_01fc: Expected I4, but got O
			//IL_0214: Expected O, but got I4
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Expected O, but got Unknown
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00db: Expected O, but got Unknown
			//IL_00ec: Expected I4, but got O
			//IL_0104: Expected O, but got I4
			//IL_0113: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Expected O, but got Unknown
			//IL_0129: Expected I4, but got O
			//IL_0132: Unknown result type (might be due to invalid IL or missing references)
			//IL_0137: Expected O, but got Unknown
			//IL_023b: Expected O, but got I4
			//IL_0252: Unknown result type (might be due to invalid IL or missing references)
			//IL_0257: Expected O, but got Unknown
			//IL_026d: Expected I4, but got O
			_003C_003Ec__DisplayClass6_0 obj = CS_0024_003C_003E8__locals1;
			Func<int, int> vertSidesStartFromSlide = obj.vertSidesStartFromSlide;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v33 @ rcx_v3 (System.Func`2<System.Int32, System.Int32>)+18] (should have been resolved before IL gen)");
			_003C_003Ec__DisplayClass6_0 obj2 = CS_0024_003C_003E8__locals1;
			object obj4 = default(object);
			object obj3 = 0 - obj4;
			object obj5 = obj4;
			object obj6 = 0;
			int num = slideID;
			int[] array;
			int num2;
			_003C_003Ec__DisplayClass6_0 obj8;
			while (true)
			{
				array = indices;
				object obj7 = obj2.numSides - 1;
				num2 = ind;
				obj8 = CS_0024_003C_003E8__locals1;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
				{
					break;
				}
				Func<int, int> vertCenterFromSlide = obj8.vertCenterFromSlide;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v271 @ rcx_v14 (System.Func`2<System.Int32, System.Int32>)+18] (should have been resolved before IL gen)");
				array[num2] = (int)obj4;
				int[] array2 = indices;
				object obj9 = (invert ? 1 : 0) ^ 1;
				object obj10 = obj9 + 1;
				object obj11 = obj10 + ind;
				object obj12 = obj5 + 1;
				array2[obj11] = (int)obj12;
				int[] array3 = indices;
				object obj13 = (invert ? 1 : 0) + 1;
				object obj14 = obj13 + ind;
				array3[obj14] = (int)obj5;
				obj5++;
				int num3 = ind + 3;
				ind = num3;
				obj2 = CS_0024_003C_003E8__locals1;
				obj6 = obj3 + obj5;
				num = slideID;
			}
			Func<int, int> vertCenterFromSlide2 = obj8.vertCenterFromSlide;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v276 @ rcx_v8 (System.Func`2<System.Int32, System.Int32>)+18] (should have been resolved before IL gen)");
			array[num2] = (int)obj4;
			int[] array4 = indices;
			object obj15 = (invert ? 1 : 0) ^ 1;
			object obj16 = obj15 + 1;
			object obj17 = ind + obj16;
			array4[obj17] = (int)obj4;
			_003C_003Ec__DisplayClass6_0 obj18 = CS_0024_003C_003E8__locals1;
			object obj19 = (invert ? 1 : 0) + 1;
			int[] array5 = indices;
			object obj20 = obj18.numSides - 1;
			object obj21 = obj20 + obj4;
			object obj22 = ind + obj19;
			array5[obj22] = (int)obj21;
			int num4 = ind + 3;
			ind = num4;
		}
	}

	private const float kMinTruncatedRadius = 0.001f;

	private static float GetAngleOffset(int numSides)
	{
		//IL_002c: Expected F4, but got I4
		if (numSides == 4)
		{
			return (float)Math.PI / 4f;
		}
		return 0f;
	}

	private static float GetRadiiScale(int numSides)
	{
		//IL_002a: Invalid comparison between I4 and F4
		//IL_004e: Expected F4, but got I4
		if (numSides == 4)
		{
			if (!(0f > 2f))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm1\"");
				return 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
			return 2f;
		}
		return 1f;
	}

	public static Mesh GenerateConeZ_RadiusAndAngle(float lengthZ, float radiusStart, float coneAngle, int numSides, int numSegments, bool cap, bool doubleSided)
	{
		float num = coneAngle * ((float)Math.PI / 180f);
		float num2 = num * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
		float radiusEnd = num2 * lengthZ;
		return GenerateConeZ_Radii(lengthZ, radiusStart, radiusEnd, numSides, numSegments, cap, doubleSided);
	}

	public static Mesh GenerateConeZ_Angle(float lengthZ, float coneAngle, int numSides, int numSegments, bool cap, bool doubleSided)
	{
		float num = coneAngle * ((float)Math.PI / 180f);
		float num2 = num * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
		float radiusEnd = num2 * lengthZ;
		int numSegments2 = default(int);
		bool cap2 = default(bool);
		bool doubleSided2 = default(bool);
		return GenerateConeZ_Radii(lengthZ, 0f, radiusEnd, numSides, numSegments2, cap2, doubleSided2);
	}

	public unsafe static Mesh GenerateConeZ_Radii(float lengthZ, float radiusStart, float radiusEnd, int numSides, int numSegments, bool cap, bool doubleSided)
	{
		//IL_0043: Invalid comparison between F4 and I4
		//IL_0052: Invalid comparison between F4 and I4
		//IL_007b: Expected O, but got I4
		//IL_002c: Expected O, but got I4
		//IL_0035: Expected O, but got I4
		//IL_0f7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f83: Expected O, but got Unknown
		//IL_0f9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9f: Expected O, but got Unknown
		//IL_00a0: Invalid comparison between I4 and F4
		//IL_0183: Expected O, but got I4
		//IL_0194: Expected F4, but got I4
		//IL_01a5: Expected F4, but got I4
		//IL_0115: Expected O, but got I4
		//IL_00c7: Expected F4, but got I4
		//IL_0145: Expected O, but got I4
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Expected O, but got Unknown
		//IL_01d7: Expected I, but got O
		//IL_10c1: Expected O, but got I4
		//IL_11a7: Expected O, but got I4
		//IL_048b: Expected O, but got I
		//IL_10de: Expected I, but got O
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Expected O, but got Unknown
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_0202: Expected O, but got I4
		//IL_0511: Expected O, but got I4
		//IL_051a: Expected O, but got I4
		//IL_0523: Expected O, but got I4
		//IL_1050: Invalid comparison between I4 and F4
		//IL_1218: Unknown result type (might be due to invalid IL or missing references)
		//IL_121d: Expected O, but got Unknown
		//IL_1226: Unknown result type (might be due to invalid IL or missing references)
		//IL_122b: Expected O, but got Unknown
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Expected O, but got Unknown
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Expected O, but got Unknown
		//IL_0250: Expected F4, but got I4
		//IL_1267: Unknown result type (might be due to invalid IL or missing references)
		//IL_126c: Expected O, but got Unknown
		//IL_0680: Expected O, but got I4
		//IL_11e9: Expected I, but got O
		//IL_120a: Expected F4, but got I
		//IL_12ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bf: Expected O, but got Unknown
		//IL_12c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12cd: Expected O, but got Unknown
		//IL_06c9: Expected O, but got I4
		//IL_06d2: Expected O, but got I4
		//IL_05e3: Expected O, but got I4
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Expected O, but got Unknown
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Expected O, but got Unknown
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Expected O, but got Unknown
		//IL_03fe: Expected O, but got I4
		//IL_1479: Expected O, but got I4
		//IL_1482: Expected O, but got I4
		//IL_075a: Expected O, but got I4
		//IL_1239: Unknown result type (might be due to invalid IL or missing references)
		//IL_123e: Expected O, but got Unknown
		//IL_1247: Unknown result type (might be due to invalid IL or missing references)
		//IL_124c: Expected O, but got Unknown
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_076c: Expected O, but got Unknown
		//IL_077a: Expected O, but got I4
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Expected O, but got Unknown
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_02b2: Expected I, but got O
		//IL_0795: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Expected O, but got Unknown
		//IL_07a3: Expected O, but got I4
		//IL_07ac: Expected O, but got I4
		//IL_07b5: Expected O, but got I4
		//IL_07c6: Expected O, but got I4
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Expected I4, but got Unknown
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Expected O, but got Unknown
		//IL_0421: Expected O, but got F4
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Expected O, but got Unknown
		//IL_0aad: Expected O, but got I4
		//IL_134a: Unknown result type (might be due to invalid IL or missing references)
		//IL_134f: Expected O, but got Unknown
		//IL_1366: Expected O, but got I4
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Expected O, but got Unknown
		//IL_0d14: Expected O, but got I4
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Expected O, but got Unknown
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Expected O, but got Unknown
		//IL_13ef: Expected O, but got Ref
		//IL_0d41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d46: Expected O, but got Unknown
		//IL_0d4f: Expected O, but got I4
		//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adb: Expected O, but got Unknown
		//IL_0aed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Expected O, but got Unknown
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b00: Expected O, but got Unknown
		//IL_0b09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0e: Expected O, but got Unknown
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Expected O, but got Unknown
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Expected O, but got Unknown
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0801: Expected O, but got Unknown
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Expected O, but got Unknown
		//IL_0816: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Expected O, but got Unknown
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Expected O, but got Unknown
		//IL_083f: Expected O, but got I4
		//IL_0c5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c60: Expected O, but got Unknown
		//IL_0c71: Expected I4, but got O
		//IL_0b52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Expected O, but got Unknown
		//IL_0d7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d84: Expected O, but got Unknown
		//IL_0c9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca0: Expected O, but got Unknown
		//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cae: Expected O, but got Unknown
		//IL_0cbf: Expected I4, but got O
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0882: Expected O, but got Unknown
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Expected O, but got Unknown
		//IL_0b86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8b: Expected O, but got Unknown
		//IL_0b94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b99: Expected O, but got Unknown
		//IL_0baa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baf: Expected O, but got Unknown
		//IL_0bb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbd: Expected O, but got Unknown
		//IL_0bc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcb: Expected O, but got Unknown
		//IL_0db5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dba: Expected O, but got Unknown
		//IL_0dcb: Expected I4, but got O
		//IL_0dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd9: Expected O, but got Unknown
		//IL_0ce8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ced: Expected O, but got Unknown
		//IL_0cfe: Expected I4, but got O
		//IL_0bfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bff: Expected O, but got Unknown
		//IL_0e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e09: Expected O, but got Unknown
		//IL_0e12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e17: Expected O, but got Unknown
		//IL_08fd: Expected I4, but got O
		//IL_0906: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Expected O, but got Unknown
		//IL_0e41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e46: Expected O, but got Unknown
		//IL_0e64: Expected I4, but got O
		//IL_0947: Unknown result type (might be due to invalid IL or missing references)
		//IL_094c: Expected O, but got Unknown
		//IL_0e8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e94: Expected O, but got Unknown
		//IL_0e9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea2: Expected O, but got Unknown
		//IL_0976: Unknown result type (might be due to invalid IL or missing references)
		//IL_097b: Expected O, but got Unknown
		//IL_0984: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Expected O, but got Unknown
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Expected O, but got Unknown
		//IL_09a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Expected O, but got Unknown
		//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b8: Expected O, but got Unknown
		//IL_0ecc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed1: Expected O, but got Unknown
		//IL_0ee1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee6: Expected O, but got Unknown
		//IL_0ef7: Expected I4, but got O
		//IL_0f00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f05: Expected O, but got Unknown
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Expected O, but got Unknown
		//IL_0a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a07: Expected O, but got Unknown
		//IL_0a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a14: Expected O, but got Unknown
		Mesh mesh = new Mesh();
		object obj = default(object);
		object obj2;
		object obj3;
		float num = default(float);
		if (obj == null)
		{
			obj2 = 0;
			obj3 = 0;
		}
		else
		{
			bool flag = num < 0f;
			bool flag2 = num == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			obj3 = flag4 & flag3;
			obj2 = obj3;
		}
		bool flag5 = num > 0.001f;
		float num2 = num;
		if (!flag5)
		{
			num2 = 0.001f;
		}
		float num3;
		if (numSides == 4)
		{
			if (!(0f > 2f))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm11,xmm1\"");
				num3 = 0f;
				num = 2f;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
				num3 = 2f;
				num = 2f;
			}
		}
		else
		{
			num3 = 1f;
		}
		float num5 = default(float);
		float num4 = num5 * num3;
		object obj5 = default(object);
		object obj4 = obj5 + 2;
		float num6 = num2 * num3;
		object obj6 = obj4 * numSides;
		bool flag6 = obj3 == null;
		object obj7 = obj6;
		object obj8 = obj6;
		if (!flag6)
		{
			object obj9 = numSides + 1;
			obj8 = obj9 + obj6;
			obj7 = obj8;
		}
		object obj10;
		Array array2;
		float num7;
		Array array4;
		float num8;
		if (numSides == 4)
		{
			Vector3[] array = new Vector3[obj8];
			obj10 = 0;
			array2 = array;
			num7 = (float)Math.PI / 4f;
		}
		else
		{
			Vector3[] array3 = new Vector3[obj8];
			bool flag7 = numSides <= 0;
			obj10 = 0;
			array2 = array3;
			num7 = 0f;
			array4 = array3;
			num8 = 0f;
			if (flag7)
			{
				goto IL_0fcc;
			}
		}
		object obj11 = obj5 + 2;
		object obj12 = obj8;
		nint num9 = (nint)typeof(Vector3[]);
		while (true)
		{
			float num10 = (float)obj10 * ((float)Math.PI * 2f);
			float num11 = num10 / (float)numSides;
			float num12 = num11 + num7;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
			bool flag8 = (nint)obj11 <= 0;
			object obj13 = 0;
			if (!flag8)
			{
				object obj14 = obj5 + 1;
				float num13 = num4 - num6;
				object obj15 = 0;
				object obj16 = obj10;
				while (true)
				{
					float num14 = (float)obj15 / (float)obj14;
					float num15 = ((0f > num14) ? 0f : ((num14 > 1f) ? 1f : num14));
					float num16 = num15 * num13;
					num5 = num14 * lengthZ;
					float num17 = num16 + num6;
					num = num17 * num12;
					object obj17 = obj16;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v241 @ rbx_v8 (System.Array)+18]");
					if ((nint)obj17 >= 0)
					{
						break;
					}
					obj13 = obj15 + 1;
					obj12 = obj16 + numSides;
					object obj18 = obj16 * 2;
					num9 = (nint)(obj16 + obj18);
					bool flag9 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11);
					obj15 = obj13;
					obj16 = obj12;
					if (flag9)
					{
						continue;
					}
					goto IL_02e9;
				}
				break;
			}
			goto IL_02e9;
			IL_02e9:
			obj10++;
			if ((nint)obj10 < numSides)
			{
				continue;
			}
			goto IL_0313;
		}
		goto IL_102a;
		IL_1374:
		int[] array5;
		object obj21;
		object obj19;
		if ((nint)obj19 < array5.Length)
		{
			object obj20 = obj19 + 1;
			array5[obj19] = (int)obj21;
			if ((nint)obj20 < array5.Length)
			{
				object obj22 = obj21 + 1;
				obj19 += 2;
				array5[obj20] = (int)obj22;
				if ((nint)obj19 < array5.Length)
				{
					object obj23 = obj21 + numSides;
					array5[obj19] = (int)obj23;
					goto IL_1498;
				}
			}
		}
		goto IL_102a;
		IL_1395:
		Array triangles;
		mesh.triangles = (int[])triangles;
		if (num6 > num4)
		{
			object obj24 = default(object);
			mesh.bounds = (Bounds)(&obj24);
			return mesh;
		}
		goto IL_13b9;
		IL_102a:
		return (Mesh)(object)new IndexOutOfRangeException();
		IL_0596:
		object obj25 = obj5;
		object obj27 = default(object);
		object obj26 = obj27;
		goto IL_11bd;
		IL_0a8a:
		object obj28 = obj2;
		obj26 = obj27;
		goto IL_1304;
		IL_0c38:
		obj21 = obj6;
		goto IL_1374;
		IL_11bd:
		object obj31;
		Vector2[] array6;
		if (obj2 != null)
		{
			int num18 = numSides + 1;
			if (num18 > 0)
			{
				object obj29 = 0;
				object obj30 = obj31;
				while (true)
				{
					object obj32 = obj31 + 1;
					object obj33 = obj30 + 1;
					if ((nint)obj31 >= array6.Length)
					{
						break;
					}
					obj29++;
					_ = 1065353216;
					bool flag10 = (nint)obj29 < num18;
					obj31 = obj32;
					obj30 = obj33;
					if (flag10)
					{
						continue;
					}
					goto IL_0652;
				}
				goto IL_102a;
			}
		}
		goto IL_0652;
		IL_13b9:
		object obj34 = default(object);
		int[] array7 = default(int[]);
		object obj42 = default(object);
		while (true)
		{
			bool flag11 = (nint)obj34 >= array5.Length;
			triangles = array7;
			if (flag11)
			{
				break;
			}
			if ((nint)obj34 < array5.Length)
			{
				object obj35 = array5.Length + obj34;
				if ((nint)obj35 < array7.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v626 @ rdx_v26-8]");
					object obj36 = 0 + obj7;
					array7[obj35] = (int)obj36;
					object obj37 = obj34 + 2;
					if ((nint)obj37 < array5.Length)
					{
						object obj38 = array5.Length + obj34;
						object obj39 = obj38 + 1;
						if ((nint)obj39 < array7.Length)
						{
							object obj40 = obj34 + 1;
							object obj41 = obj42 + obj7;
							array7[obj39] = (int)obj41;
							if ((nint)obj40 < array5.Length)
							{
								object obj43 = array5.Length + obj34;
								object obj44 = obj43 + 2;
								if ((nint)obj44 < array7.Length)
								{
									obj34 += 3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v626 @ rdx_v26-4]");
									object obj45 = 0 + obj7;
									array7[obj44] = (int)obj45;
									obj42 += 12;
									continue;
								}
							}
						}
					}
				}
			}
			goto IL_102a;
		}
		goto IL_1395;
		IL_1498:
		if (obj26 != null)
		{
			object obj46 = array5.Length + array5.Length;
			array7 = new int[obj46];
			array5.CopyTo(array7, 0);
			obj42 = array5 + 40;
			obj34 = 0;
			goto IL_13b9;
		}
		triangles = array5;
		goto IL_1395;
		IL_1304:
		object obj53;
		if (obj28 != null)
		{
			object obj47 = numSides - 1;
			if ((nint)obj47 > 0)
			{
				object obj48 = obj19 + 2;
				object obj49 = ~obj6;
				object obj50 = obj6 + 1;
				object obj51 = obj19 + 9;
				object obj52 = obj51 * 4;
				obj53 = (object)array5 + obj52;
				object obj54 = obj19;
				while ((nint)obj19 < array5.Length)
				{
					object obj55 = obj48 - 1;
					if ((nint)obj55 >= array5.Length)
					{
						break;
					}
					object obj56 = obj50 + 1;
					obj19 += 3;
					obj53 = obj56;
					object obj57 = obj54 + 3;
					obj53 += 12;
					object obj58 = obj48 + 3;
					if ((nint)obj48 >= array5.Length)
					{
						break;
					}
					obj50++;
					object obj59 = obj49 + obj50;
					bool flag12 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj59) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj47);
					obj54 = obj57;
					obj48 = obj58;
					if (flag12)
					{
						continue;
					}
					goto IL_0c38;
				}
				goto IL_102a;
			}
			obj21 = obj6;
			goto IL_1374;
		}
		goto IL_1498;
		IL_0fcc:
		if (obj3 != null)
		{
			nint num19 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v457 @ rax_v84 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rbx_v2 (System.Array)+18]");
			if ((nint)obj6 < 0)
			{
				object obj60 = obj6 + 1;
				object obj61 = obj6 * 2;
				object obj62 = obj6 + obj61;
				_ = Vector3.zeroVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v412 @ rdx_v53 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				_ = 0;
				if (numSides <= 0)
				{
					goto IL_0461;
				}
				object obj63 = obj60 + 4;
				object obj64 = obj63 * 2;
				object obj65 = obj60 + obj64;
				object obj66 = obj65 * 4;
				object obj67 = (object)array4 + obj66;
				object obj68 = 0;
				float num21 = num;
				float num25 = default(float);
				while (true)
				{
					float num22 = (float)obj68 * ((float)Math.PI * 2f);
					float num23 = num22 / (float)numSides;
					float num24 = num23 + num8;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
					object obj69 = obj60 + obj68;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rbx_v2 (System.Array)+18]");
					if ((nint)obj69 >= 0)
					{
						break;
					}
					obj68++;
					obj67 = num25;
					_ = 0;
					obj67 += 12;
					bool flag13 = (nint)obj68 < numSides;
					num = num25;
					num21 = num25;
					if (flag13)
					{
						continue;
					}
					goto IL_0461;
				}
			}
			goto IL_102a;
		}
		goto IL_1434;
		IL_0313:
		array4 = array2;
		num8 = num7;
		goto IL_0fcc;
		IL_1434:
		Array vertices;
		Mesh mesh2;
		if (obj27 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rbx_v2 (System.Array)+18]");
			nint num26 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rbx_v2 (System.Array)+18]");
			object obj70 = num26 + 0;
			Vector3[] array8 = new Vector3[obj70];
			array4.CopyTo(array8, 0);
			Array array9 = array4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rbx_v2 (System.Array)+18]");
			array9.CopyTo(array8, 0);
			int num18 = 0;
			vertices = array8;
			mesh2 = mesh;
		}
		else
		{
			int num18 = numSides;
			vertices = array4;
			mesh2 = mesh;
		}
		mesh2.vertices = (Vector3[])vertices;
		array6 = new Vector2[obj8];
		bool flag14 = (nint)obj6 <= 0;
		obj25 = obj5;
		obj31 = 0;
		obj26 = obj27;
		if (!flag14)
		{
			object obj71 = 0;
			object obj72 = 0;
			object obj73 = 0;
			while (true)
			{
				obj31 = obj72 + 1;
				object obj74 = obj71 + 1;
				nint num27 = (nint)typeof(Vector2);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v784 @ rax_v78 (Il2CppClass<UnityEngine.Vector2>)+B8]");
				nint num28 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v640 @ rcx_v56 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
				num = 0f;
				if ((nint)obj72 >= array6.Length)
				{
					break;
				}
				obj73++;
				_ = Vector2.zeroVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v640 @ rcx_v56 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
				_ = 0;
				bool flag15 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj73) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6);
				obj71 = obj74;
				obj72 = obj31;
				if (flag15)
				{
					continue;
				}
				goto IL_0596;
			}
			goto IL_102a;
		}
		goto IL_11bd;
		IL_0461:
		obj8 = obj7;
		goto IL_1434;
		IL_0652:
		Array uv;
		if (obj26 != null)
		{
			object obj75 = array6.Length + array6.Length;
			Vector2[] array10 = new Vector2[obj75];
			array6.CopyTo(array10, 0);
			array6.CopyTo(array10, array6.Length);
			int num18 = 0;
			object obj76 = 0;
			object obj77 = 0;
			while (true)
			{
				bool flag16 = (nint)obj77 >= array6.Length;
				uv = array10;
				if (flag16)
				{
					break;
				}
				num18 = obj76 + array6.Length;
				if (num18 < array10.Length)
				{
					object obj78 = obj76 + array6.Length;
					obj76++;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1591 @ rax_v65 (UnityEngine.Vector2[])+20+v1592 @ r9_v4 (System.Int32)*8]");
					_ = 0;
					_ = 1065353216;
					obj77 = obj76;
					continue;
				}
				goto IL_102a;
			}
		}
		else
		{
			uv = array6;
		}
		mesh.uv = (Vector2[])uv;
		object obj79 = obj25 + 1;
		if ((nint)obj79 <= 1)
		{
			obj79 = 1;
		}
		object obj80 = obj79 * numSides;
		object obj81 = obj80 * 2;
		object obj82 = obj80 + obj81;
		object obj83 = obj82 + obj82;
		if (obj2 != null)
		{
			object obj84 = obj83 + numSides;
			object obj85 = numSides * 2;
			obj83 = obj84 + obj85;
		}
		array5 = new int[obj83];
		bool flag17 = numSides <= 0;
		obj19 = 0;
		obj53 = 0;
		obj28 = obj2;
		if (!flag17)
		{
			object obj86 = obj5 + 1;
			object obj87 = 0;
			obj19 = 0;
			obj53 = 0;
			object obj88 = obj2;
			object obj89 = 0;
			while (true)
			{
				object obj90 = obj87 + 1;
				bool flag18 = (nint)obj90 == numSides;
				object obj91 = 0;
				if (!flag18)
				{
					obj91 = obj90;
				}
				if ((nint)obj86 > 0)
				{
					object obj92 = obj91 - obj87;
					object obj93 = obj89 + 9;
					object obj94 = obj19 + 2;
					obj88 = obj92 - numSides;
					object obj95 = obj87 + numSides;
					object obj96 = obj93 * 4;
					object obj97 = (object)array5 + obj96;
					object obj98 = 0;
					object obj99 = obj19;
					object obj100 = obj89;
					while ((nint)obj99 < array5.Length)
					{
						object obj101 = obj95 - numSides;
						object obj102 = obj94 - 1;
						if ((nint)obj102 >= array5.Length)
						{
							break;
						}
						object obj103 = obj88 + obj95;
						obj97 = obj103;
						if ((nint)obj94 >= array5.Length)
						{
							break;
						}
						array5[obj94] = (int)obj95;
						object obj104 = obj94 + 1;
						if ((nint)obj104 >= array5.Length)
						{
							break;
						}
						object obj105 = obj92 + obj95;
						object obj106 = obj94 + 2;
						if ((nint)obj106 >= array5.Length)
						{
							break;
						}
						obj19 = obj99 + 6;
						obj89 = obj100 + 6;
						object obj107 = obj97 + 24;
						object obj108 = obj94 + 3;
						obj83 = obj94 + 6;
						if ((nint)obj108 >= array5.Length)
						{
							break;
						}
						object obj109 = obj88 + obj95;
						obj86 = obj5 + 1;
						obj98++;
						obj53 = obj95 + numSides;
						bool flag19 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj98) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj86);
						obj26 = obj100;
						obj97 = obj107;
						obj99 = obj19;
						obj95 = obj53;
						obj100 = obj89;
						obj94 = obj83;
						if (flag19)
						{
							continue;
						}
						goto IL_0a60;
					}
					break;
				}
				goto IL_0a60;
				IL_0a60:
				obj87++;
				if ((nint)obj87 < numSides)
				{
					continue;
				}
				goto IL_0a8a;
			}
			goto IL_102a;
		}
		goto IL_1304;
	}

	public unsafe static Mesh GenerateConeZ_Radii_DoubleCaps(float lengthZ, float radiusStart, float radiusEnd, int numSides, bool inverted)
	{
		//IL_0008: Expected O, but got Ref
		//IL_00be: Expected F4, but got I4
		//IL_098d: Expected O, but got I4
		//IL_09a0: Expected O, but got I4
		//IL_09ae: Expected I, but got O
		//IL_0277: Expected O, but got Ref
		//IL_0103: Expected O, but got I
		//IL_0114: Expected O, but got I
		//IL_0a2a: Expected I, but got O
		//IL_012e: Expected F4, but got I4
		//IL_09e4: Invalid comparison between I4 and F4
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Expected O, but got Unknown
		//IL_02fb: Expected O, but got Ref
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Expected O, but got Unknown
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Expected O, but got Unknown
		//IL_01f4: Expected I, but got O
		//IL_0207: Invalid comparison between F4 and I4
		//IL_0233: Expected F4, but got I
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Expected O, but got Unknown
		//IL_03a9: Expected O, but got I4
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Expected O, but got Unknown
		//IL_03ff: Expected O, but got I4
		//IL_0408: Expected O, but got I4
		//IL_0890: Expected O, but got Ref
		//IL_08b5: Expected O, but got Ref
		//IL_08cb: Expected O, but got I
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Expected O, but got Unknown
		//IL_045b: Expected O, but got I4
		//IL_0a48: Expected O, but got I4
		//IL_0a95: Expected O, but got Ref
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Expected O, but got Unknown
		//IL_04eb: Expected I4, but got O
		//IL_050e: Expected O, but got I
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Expected O, but got Unknown
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Expected O, but got Unknown
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Expected O, but got Unknown
		//IL_0584: Expected I4, but got O
		//IL_05a7: Expected O, but got I
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Expected O, but got Unknown
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Expected O, but got Unknown
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Expected O, but got Unknown
		//IL_0633: Expected I4, but got O
		//IL_066a: Expected O, but got I4
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Expected O, but got Unknown
		//IL_06bb: Expected I4, but got O
		//IL_06de: Expected O, but got I
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Expected O, but got Unknown
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Expected O, but got Unknown
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Expected O, but got Unknown
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Expected O, but got Unknown
		//IL_0778: Expected I4, but got O
		//IL_079b: Expected O, but got I
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b2: Expected O, but got Unknown
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Expected O, but got Unknown
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Expected O, but got Unknown
		//IL_0811: Expected I4, but got O
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals45 = new _003C_003Ec__DisplayClass6_0();
		CS_0024_003C_003E8__locals45.numSides = numSides;
		Mesh mesh = new Mesh();
		float num = default(float);
		bool flag = num > 0.001f;
		float num2 = num;
		if (!flag)
		{
			num2 = 0.001f;
		}
		int num3 = (CS_0024_003C_003E8__locals45.vertCountSides = CS_0024_003C_003E8__locals45.numSides + CS_0024_003C_003E8__locals45.numSides);
		Func<int, int> vertSidesStartFromSlide = (int slideID) => slideID * CS_0024_003C_003E8__locals45.numSides;
		CS_0024_003C_003E8__locals45.vertSidesStartFromSlide = vertSidesStartFromSlide;
		Func<int, int> vertCenterFromSlide = (int slideID) => CS_0024_003C_003E8__locals45.vertCountSides + slideID;
		CS_0024_003C_003E8__locals45.vertCenterFromSlide = vertCenterFromSlide;
		float num4 = ((CS_0024_003C_003E8__locals45.numSides != 4) ? 0f : ((float)Math.PI / 4f));
		int num5 = num3 + 2;
		Vector3[] array = new Vector3[num5];
		object obj3 = 0;
		float num6 = 0.001f;
		object obj4 = 0;
		nint num7 = (nint)typeof(Vector3[]);
		float num9 = default(float);
		float num8 = num9;
		object obj6 = default(object);
		float num17 = default(float);
		object obj56 = default(object);
		while (true)
		{
			if ((nint)obj4 < CS_0024_003C_003E8__locals45.numSides)
			{
				float num10 = (float)obj3 * ((float)Math.PI * 2f);
				float num11 = num10 / (float)CS_0024_003C_003E8__locals45.numSides;
				float num12 = num11 + num4;
				int num13 = ((_003C_003Ec__DisplayClass6_0)num7)._003CGenerateConeZ_Radii_DoubleCaps_003Eb__1(num5);
				int num14 = ((_003C_003Ec__DisplayClass6_0)num7)._003CGenerateConeZ_Radii_DoubleCaps_003Eb__1(num5);
				float num15 = num12;
				float num16 = 0f;
				while (true)
				{
					if (!(0f > num16))
					{
						if (!(num16 > 1f))
						{
							num15 = num16;
						}
						else
						{
							num15 = num16;
						}
					}
					Func<int, int> vertSidesStartFromSlide2 = CS_0024_003C_003E8__locals45.vertSidesStartFromSlide;
					num5 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v256 @ rcx_v57 (System.Func`2<System.Int32, System.Int32>)+18] (should have been resolved before IL gen)");
					num9 = num16 * lengthZ;
					object obj5 = obj6 + obj3;
					if ((nint)obj5 >= array.Length)
					{
						break;
					}
					num16++;
					object obj7 = obj5 * 2;
					num7 = (nint)(obj5 + obj7);
					bool flag2 = num16 < 2f;
					num15 = num17;
					if (!flag2)
					{
						goto IL_0223;
					}
				}
				break;
			}
			Func<int, int> vertCenterFromSlide2 = CS_0024_003C_003E8__locals45.vertCenterFromSlide;
			object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v242 @ rcx_v16 (System.Func`2<System.Int32, System.Int32>)+18] (should have been resolved before IL gen)");
			nint num18 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v239 @ rax_v23 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num19 = 0;
			if ((nint)obj6 >= array.Length)
			{
				break;
			}
			object obj9 = obj6 * 2;
			object obj10 = obj6 + obj9;
			_ = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ rdx_v12 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			_ = 0;
			Func<int, int> vertCenterFromSlide3 = CS_0024_003C_003E8__locals45.vertCenterFromSlide;
			object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
			_ = 1;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v243 @ rcx_v20 (System.Func`2<System.Int32, System.Int32>)+18] (should have been resolved before IL gen)");
			if ((nint)obj6 >= array.Length)
			{
				break;
			}
			object obj12 = obj6 * 2;
			object obj13 = obj6 + obj12;
			mesh.vertices = array;
			_003C_003Ec__DisplayClass6_1 CS_0024_003C_003E8__locals64 = new _003C_003Ec__DisplayClass6_1();
			CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1 = CS_0024_003C_003E8__locals45;
			_003C_003Ec__DisplayClass6_0 obj14 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
			object obj15 = obj14.numSides * 2;
			object obj16 = obj14.numSides + obj15;
			object obj17 = obj16 << 2;
			int[] indices = new int[obj17];
			CS_0024_003C_003E8__locals64.indices = indices;
			CS_0024_003C_003E8__locals64.ind = 0;
			object obj18 = 0;
			object obj19 = 0;
			while (true)
			{
				_003C_003Ec__DisplayClass6_0 obj20 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
				if ((nint)obj19 < obj20.numSides)
				{
					object obj21 = obj18 + 1;
					bool flag3 = (nint)obj21 == obj20.numSides;
					object obj22 = 0;
					if (!flag3)
					{
						obj22 = obj21;
					}
					object obj23 = 0;
					while (true)
					{
						_003C_003Ec__DisplayClass6_0 obj24 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
						int[] indices2 = CS_0024_003C_003E8__locals64.indices;
						int ind = CS_0024_003C_003E8__locals64.ind;
						if (CS_0024_003C_003E8__locals64.ind >= indices2.Length)
						{
							break;
						}
						object obj25 = obj23 * obj24.numSides;
						object obj26 = obj25 + obj18;
						indices2[ind] = (int)obj26;
						int[] indices3 = CS_0024_003C_003E8__locals64.indices;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+77]");
						object obj27 = (nint)0 ^ (nint)1;
						object obj28 = obj27 + 1;
						object obj29 = obj28 + CS_0024_003C_003E8__locals64.ind;
						if ((nint)obj29 >= indices3.Length)
						{
							break;
						}
						object obj30 = CS_0024_003C_003E8__locals64.ind + obj28;
						object obj31 = obj22 + obj25;
						indices3[obj30] = (int)obj31;
						_003C_003Ec__DisplayClass6_0 obj32 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+77]");
						object obj33 = (nint)0 + (nint)1;
						int[] indices4 = CS_0024_003C_003E8__locals64.indices;
						object obj34 = obj33 + CS_0024_003C_003E8__locals64.ind;
						if ((nint)obj34 >= indices4.Length)
						{
							break;
						}
						object obj35 = obj33 + CS_0024_003C_003E8__locals64.ind;
						object obj36 = obj32.numSides + obj25;
						object obj37 = obj36 + obj18;
						indices4[obj35] = (int)obj37;
						_003C_003Ec__DisplayClass6_0 obj38 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
						int[] indices5 = CS_0024_003C_003E8__locals64.indices;
						object obj39 = CS_0024_003C_003E8__locals64.ind + 3;
						if ((nint)obj39 >= indices5.Length)
						{
							break;
						}
						object obj40 = obj38.numSides + obj22;
						object obj41 = obj40 + obj25;
						indices5[obj39] = (int)obj41;
						_003C_003Ec__DisplayClass6_0 obj42 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+77]");
						object obj43 = (nint)0 ^ (nint)1;
						object obj44 = obj43 + 4;
						int[] indices6 = CS_0024_003C_003E8__locals64.indices;
						object obj45 = obj44 + CS_0024_003C_003E8__locals64.ind;
						if ((nint)obj45 >= indices6.Length)
						{
							break;
						}
						object obj46 = obj44 + CS_0024_003C_003E8__locals64.ind;
						object obj47 = obj42.numSides + obj25;
						object obj48 = obj47 + obj18;
						indices6[obj46] = (int)obj48;
						int[] indices7 = CS_0024_003C_003E8__locals64.indices;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+77]");
						object obj49 = (nint)0 + (nint)4;
						object obj50 = obj49 + CS_0024_003C_003E8__locals64.ind;
						if ((nint)obj50 >= indices7.Length)
						{
							break;
						}
						object obj51 = obj49 + CS_0024_003C_003E8__locals64.ind;
						obj23++;
						object obj52 = obj22 + obj25;
						indices7[obj51] = (int)obj52;
						int ind2 = CS_0024_003C_003E8__locals64.ind + 6;
						CS_0024_003C_003E8__locals64.ind = ind2;
						if ((nint)obj23 >= 1)
						{
							goto IL_084e;
						}
					}
					break;
				}
				Action<int, bool> action = delegate(int slideID, bool invert)
				{
					//IL_003e: Unsupported input type for neg.
					//IL_003e: Unknown result type (might be due to invalid IL or missing references)
					//IL_0043: Expected O, but got Unknown
					//IL_0054: Expected O, but got I4
					//IL_02a5: Expected O, but got I4
					//IL_01ac: Expected I4, but got O
					//IL_01c4: Expected O, but got I4
					//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
					//IL_01d2: Expected O, but got Unknown
					//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
					//IL_01e6: Expected O, but got Unknown
					//IL_0093: Expected I4, but got O
					//IL_00ab: Expected O, but got I4
					//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
					//IL_00b9: Expected O, but got Unknown
					//IL_01fc: Expected I4, but got O
					//IL_0214: Expected O, but got I4
					//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
					//IL_00cd: Expected O, but got Unknown
					//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
					//IL_00db: Expected O, but got Unknown
					//IL_00ec: Expected I4, but got O
					//IL_0104: Expected O, but got I4
					//IL_0113: Unknown result type (might be due to invalid IL or missing references)
					//IL_0118: Expected O, but got Unknown
					//IL_0129: Expected I4, but got O
					//IL_0132: Unknown result type (might be due to invalid IL or missing references)
					//IL_0137: Expected O, but got Unknown
					//IL_023b: Expected O, but got I4
					//IL_0252: Unknown result type (might be due to invalid IL or missing references)
					//IL_0257: Expected O, but got Unknown
					//IL_026d: Expected I4, but got O
					_003C_003Ec__DisplayClass6_0 obj57 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
					Func<int, int> vertSidesStartFromSlide3 = obj57.vertSidesStartFromSlide;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v33 @ rcx_v3 (System.Func`2<System.Int32, System.Int32>)+18] (should have been resolved before IL gen)");
					_003C_003Ec__DisplayClass6_0 obj58 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
					object obj60 = default(object);
					object obj59 = 0 - obj60;
					object obj61 = obj60;
					object obj62 = 0;
					int num20 = slideID;
					int[] indices8;
					int ind3;
					_003C_003Ec__DisplayClass6_0 obj64;
					while (true)
					{
						indices8 = CS_0024_003C_003E8__locals64.indices;
						object obj63 = obj58.numSides - 1;
						ind3 = CS_0024_003C_003E8__locals64.ind;
						obj64 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj62) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj63))
						{
							break;
						}
						Func<int, int> vertCenterFromSlide4 = obj64.vertCenterFromSlide;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v271 @ rcx_v14 (System.Func`2<System.Int32, System.Int32>)+18] (should have been resolved before IL gen)");
						indices8[ind3] = (int)obj60;
						int[] indices9 = CS_0024_003C_003E8__locals64.indices;
						object obj65 = (invert ? 1 : 0) ^ 1;
						object obj66 = obj65 + 1;
						object obj67 = obj66 + CS_0024_003C_003E8__locals64.ind;
						object obj68 = obj61 + 1;
						indices9[obj67] = (int)obj68;
						int[] indices10 = CS_0024_003C_003E8__locals64.indices;
						object obj69 = (invert ? 1 : 0) + 1;
						object obj70 = obj69 + CS_0024_003C_003E8__locals64.ind;
						indices10[obj70] = (int)obj61;
						obj61++;
						int ind4 = CS_0024_003C_003E8__locals64.ind + 3;
						CS_0024_003C_003E8__locals64.ind = ind4;
						obj58 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
						obj62 = obj59 + obj61;
						num20 = slideID;
					}
					Func<int, int> vertCenterFromSlide5 = obj64.vertCenterFromSlide;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v276 @ rcx_v8 (System.Func`2<System.Int32, System.Int32>)+18] (should have been resolved before IL gen)");
					indices8[ind3] = (int)obj60;
					int[] indices11 = CS_0024_003C_003E8__locals64.indices;
					object obj71 = (invert ? 1 : 0) ^ 1;
					object obj72 = obj71 + 1;
					object obj73 = CS_0024_003C_003E8__locals64.ind + obj72;
					indices11[obj73] = (int)obj60;
					_003C_003Ec__DisplayClass6_0 obj74 = CS_0024_003C_003E8__locals64.CS_0024_003C_003E8__locals1;
					object obj75 = (invert ? 1 : 0) + 1;
					int[] indices12 = CS_0024_003C_003E8__locals64.indices;
					object obj76 = obj74.numSides - 1;
					object obj77 = obj76 + obj60;
					object obj78 = CS_0024_003C_003E8__locals64.ind + obj75;
					indices12[obj78] = (int)obj77;
					int ind5 = CS_0024_003C_003E8__locals64.ind + 3;
					CS_0024_003C_003E8__locals64.ind = ind5;
				};
				object obj53 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+77]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1006 @ rax_v39 (System.Action`2<System.Int32, System.Boolean>)+18] (should have been resolved before IL gen)");
				object obj54 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+77]");
				object obj55 = (nint)0 ^ (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1006 @ rax_v39 (System.Action`2<System.Int32, System.Boolean>)+18] (should have been resolved before IL gen)");
				mesh.triangles = CS_0024_003C_003E8__locals64.indices;
				if (num2 > num8)
				{
				}
				if (!(num2 > num8))
				{
				}
				mesh.bounds = (Bounds)(&obj56);
				return mesh;
				IL_084e:
				obj18++;
				obj19 = obj18;
			}
			break;
			IL_0223:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+67]");
			num8 = 0f;
			obj3++;
			num6 = num17;
			obj4 = obj3;
		}
		return (Mesh)(object)new IndexOutOfRangeException();
	}

	public unsafe static Bounds ComputeBounds(float lengthZ, float radiusStart, float radiusEnd)
	{
		//IL_0037: Expected O, but got I4
		//IL_0032: Expected native int or pointer, but got O
		//IL_0089: Expected O, but got F4
		//IL_0084: Expected native int or pointer, but got O
		float num = default(float);
		if (num < radiusEnd)
		{
		}
		float num2 = radiusEnd + radiusEnd;
		Bounds bounds = default(Bounds);
		((Bounds*)(nint)bounds)->m_Center = (Vector3)0;
		float num3 = lengthZ * 0.5f;
		float num4 = lengthZ * 0.5f;
		float num5 = num2 * 0.5f;
		float num6 = num2 * 0.5f;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num5;
		return bounds;
	}

	private static int GetCapAdditionalVerticesCount(CapMode capMode, int numSides)
	{
		//IL_002b: Expected O, but got I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_009f: Expected O, but got I4
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected I4, but got Unknown
		bool flag = capMode == CapMode.None;
		if (!flag)
		{
			object obj = capMode - 1;
			if (flag)
			{
				return 1;
			}
			object obj2 = obj - 1;
			if (flag)
			{
				return 2;
			}
			object obj3 = obj2 - 1;
			if (flag)
			{
				return numSides + 1;
			}
			if ((nint)obj3 == 1)
			{
				object obj4 = numSides * 2;
				return obj4 + 2;
			}
		}
		return 0;
	}

	private static int GetCapAdditionalIndicesCount(CapMode capMode, int numSides)
	{
		//IL_002b: Expected O, but got I4
		//IL_00cc: Expected O, but got I4
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected I4, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_009f: Expected O, but got I4
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00b9: Expected I4, but got O
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		bool flag = capMode == CapMode.None;
		if (!flag)
		{
			object obj = capMode - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					object obj3 = obj2 - 1;
					if (flag)
					{
						goto IL_00be;
					}
					if ((nint)obj3 != 1)
					{
						goto IL_00de;
					}
				}
				object obj4 = numSides * 2;
				object obj5 = numSides + obj4;
				return (int)(obj5 + obj5);
			}
			goto IL_00be;
		}
		goto IL_00de;
		IL_00de:
		return 0;
		IL_00be:
		object obj6 = numSides * 2;
		return numSides + obj6;
	}

	public static int GetVertexCount(int numSides, int numSegments, CapMode capMode, bool doubleSided)
	{
		//IL_0134: Expected O, but got I4
		//IL_013d: Expected O, but got I4
		//IL_0190: Expected O, but got I4
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_01aa: Expected I4, but got O
		//IL_002b: Expected O, but got I4
		//IL_011d: Expected O, but got I4
		//IL_0126: Expected O, but got I4
		//IL_0150: Expected O, but got I4
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_0178: Expected I4, but got O
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0106: Expected O, but got I4
		//IL_010f: Expected O, but got I4
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_00dc: Expected O, but got I4
		//IL_00ea: Expected O, but got I4
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_009f: Expected O, but got I4
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		//IL_00bb: Expected O, but got I4
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		bool flag = capMode == CapMode.None;
		if (flag)
		{
			goto IL_012b;
		}
		object obj = capMode - 1;
		object obj5;
		object obj7;
		if (!flag)
		{
			object obj2 = obj - 1;
			if (!flag)
			{
				object obj3 = obj2 - 1;
				if (!flag)
				{
					if ((nint)obj3 != 1)
					{
						goto IL_012b;
					}
					object obj4 = numSides * 2;
					obj5 = obj4 + 2;
					object obj6 = numSides * 4;
					obj7 = obj6 + 4;
				}
				else
				{
					obj5 = numSides + 1;
					object obj8 = numSides * 2;
					obj7 = obj8 + 2;
				}
			}
			else
			{
				obj7 = 4;
				obj5 = 2;
			}
		}
		else
		{
			obj7 = 2;
			obj5 = 1;
		}
		goto IL_0182;
		IL_012b:
		obj7 = 0;
		obj5 = 0;
		goto IL_0182;
		IL_0182:
		object obj9 = numSegments + 2;
		object obj10 = obj9 * numSides;
		int result = (int)(obj10 + obj5);
		if (doubleSided)
		{
			object obj11 = numSegments + 2;
			object obj12 = obj11 * numSides;
			object obj13 = obj12 * 2;
			return (int)(obj7 + obj13);
		}
		return result;
	}

	public static int GetIndicesCount(int numSides, int numSegments, CapMode capMode, bool doubleSided)
	{
		//IL_000e: Expected O, but got I4
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected I4, but got Unknown
		//IL_0061: Expected O, but got I4
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected I4, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		object obj = numSegments + 1;
		object obj2 = obj * 2;
		object obj3 = obj + obj2;
		object obj4 = obj3 + obj3;
		bool flag = capMode == CapMode.None;
		if (!flag)
		{
			object obj5 = capMode - 1;
			if (!flag)
			{
				object obj6 = obj5 - 1;
				if (!flag)
				{
					object obj7 = obj6 - 1;
					if (flag)
					{
						goto IL_010a;
					}
					if ((nint)obj7 != 1)
					{
						goto IL_012a;
					}
				}
				object obj8 = obj4 + 6;
				int num = obj8 * numSides;
				int result = num + num;
				if (!doubleSided)
				{
					result = num;
				}
				return result;
			}
			goto IL_010a;
		}
		goto IL_012a;
		IL_012a:
		int num2 = obj4 * numSides;
		int result2 = num2 + num2;
		if (!doubleSided)
		{
			result2 = num2;
		}
		return result2;
		IL_010a:
		obj4 += 3;
		goto IL_012a;
	}

	public static int GetSharedMeshVertexCount()
	{
		//IL_0200: Expected I4, but got O
		//IL_019b: Expected O, but got I4
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected I4, but got Unknown
		//IL_0156: Expected O, but got I4
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		//IL_0183: Expected I4, but got O
		//IL_00df: Expected O, but got I4
		Config instance = Config.GetInstance(true);
		Config instance2;
		Config instance3;
		if ((object)instance != null)
		{
			instance2 = Config.GetInstance(true);
			if ((object)instance2 != null)
			{
				instance3 = Config.GetInstance(true);
				if ((object)instance3 != null)
				{
					if (instance3.m_RenderingMode != RenderingMode.SRPBatcher)
					{
						goto IL_00fc;
					}
					if (instance3.m_RenderPipeline != RenderPipeline.BuiltIn)
					{
						RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
						object obj = projectRenderPipeline - 1;
						if ((nint)obj <= 1)
						{
							goto IL_00fc;
						}
					}
					goto IL_0143;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
		IL_0143:
		object obj2 = instance2.sharedMeshSegments + 3;
		object obj3 = obj2 * instance.sharedMeshSides;
		object obj4 = obj3 + 1;
		return (int)(obj4 + obj4);
		IL_00fc:
		if (instance3.m_RenderPipeline != RenderPipeline.BuiltIn && instance3.m_RenderingMode == RenderingMode.MultiPass)
		{
			goto IL_0143;
		}
		object obj5 = instance2.sharedMeshSegments + 3;
		object obj6 = obj5 * instance.sharedMeshSides;
		int num = obj6 + 1;
		if (instance3.m_RenderingMode > RenderingMode.MultiPass)
		{
			num += num;
		}
		return num;
	}

	public static int GetSharedMeshIndicesCount()
	{
		//IL_021e: Expected I4, but got O
		//IL_0153: Expected O, but got I4
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected I4, but got Unknown
		//IL_01cb: Expected O, but got I4
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected I4, but got Unknown
		//IL_00df: Expected O, but got I4
		Config instance = Config.GetInstance(true);
		Config instance2;
		Config instance3;
		if ((object)instance != null)
		{
			instance2 = Config.GetInstance(true);
			if ((object)instance2 != null)
			{
				instance3 = Config.GetInstance(true);
				if ((object)instance3 != null)
				{
					if (instance3.m_RenderingMode != RenderingMode.SRPBatcher)
					{
						goto IL_00fc;
					}
					if (instance3.m_RenderPipeline != RenderPipeline.BuiltIn)
					{
						RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
						object obj = projectRenderPipeline - 1;
						if ((nint)obj <= 1)
						{
							goto IL_00fc;
						}
					}
					goto IL_01b8;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
		IL_021e:
		int result;
		return result;
		IL_01b8:
		object obj2 = instance2.sharedMeshSegments * 2;
		object obj3 = instance2.sharedMeshSegments + obj2;
		object obj4 = obj3 * 4;
		object obj5 = obj4 + 18;
		result = obj5 * instance.sharedMeshSides;
		goto IL_021e;
		IL_00fc:
		if (instance3.m_RenderPipeline == RenderPipeline.BuiltIn || instance3.m_RenderingMode != RenderingMode.MultiPass)
		{
			object obj6 = instance2.sharedMeshSegments * 2;
			object obj7 = instance2.sharedMeshSegments + obj6;
			object obj8 = obj7 * 2;
			object obj9 = obj8 + 9;
			result = obj9 * instance.sharedMeshSides;
			if (instance3.m_RenderingMode <= RenderingMode.MultiPass)
			{
				goto IL_021e;
			}
		}
		goto IL_01b8;
	}

	public static int GetSharedMeshHDVertexCount()
	{
		//IL_005f: Expected I4, but got O
		//IL_003e: Expected O, but got I4
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected I4, but got Unknown
		Config instance = Config.GetInstance(true);
		if ((object)instance != null)
		{
			object obj = instance.sharedMeshSides * 2;
			return obj + 2;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public static int GetSharedMeshHDIndicesCount()
	{
		//IL_0071: Expected I4, but got O
		//IL_003e: Expected O, but got I4
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_005e: Expected I4, but got O
		Config instance = Config.GetInstance(true);
		if ((object)instance != null)
		{
			object obj = instance.sharedMeshSides * 2;
			object obj2 = instance.sharedMeshSides + obj;
			return obj2 << 2;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}
}
