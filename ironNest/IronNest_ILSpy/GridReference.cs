using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

[Serializable]
public class GridReference
{
	public GridLocations Location;

	public int X;

	public int Y;

	public override string ToString()
	{
		//IL_002d: Expected I4, but got O
		object obj = default(object);
		object arg = (GridLocations)obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg2 = default(object);
		object arg3 = default(object);
		return $"{arg} {arg2}:{arg3}";
	}

	public void RandomiseSubGrid()
	{
		int x = UnityEngine.Random.Range(0, 10);
		X = x;
		int y = UnityEngine.Random.Range(0, 10);
		Y = y;
	}

	public unsafe Vector3 GetLocation(Vector3[] gridBounds, bool fuzzyLocation = false)
	{
		//IL_009c: Expected O, but got I
		//IL_00c8: Expected O, but got I
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_02cb: Expected native int or pointer, but got O
		//IL_02d8: Expected native int or pointer, but got O
		if (gridBounds.Length > 0 && gridBounds.Length > 1 && gridBounds.Length > 3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+4C]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
			object obj = num - 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+34]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
			object obj2 = num2 - 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			object obj3 = (object)this >> 2;
			float num3 = (float)Y * 0.01f;
			object obj4 = obj3 >> 31;
			object obj5 = obj3 + obj4;
			object obj6 = obj5 * 4;
			object obj7 = obj5 + obj6;
			object obj8 = obj7 + obj7;
			object obj9 = Location - obj8;
			float num4 = (float)X * 0.005f;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			object obj10 = obj8 >> 2;
			float num5 = (float)obj9 * 0.1f;
			object obj11 = obj10 >> 31;
			object obj12 = obj10 + obj11;
			float num6 = (float)obj12 * 0.05f;
			float num7 = num4 + num6;
			float num10;
			float num12;
			if (fuzzyLocation)
			{
				float num8 = UnityEngine.Random.Range(0f, 1f);
				float num9 = num8 * 0.005f;
				num10 = num9 + num7;
				float num11 = UnityEngine.Random.Range(0f, 1f);
				num12 = num11;
			}
			else
			{
				num10 = num7 + 0.0025f;
				num12 = 0.5f;
			}
			float num13 = num5 + num3;
			float num14 = num12 * 0.01f;
			float num15 = num14 + num13;
			float num16 = (float)obj * num10;
			float num17 = num16;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
			float num18 = num17 + 0f;
			float num19 = (float)obj2 * num15;
			float z = num18 + num19;
			Vector3 vector = default(Vector3);
			float x = default(float);
			((Vector3*)(nint)vector)->x = x;
			((Vector3*)(nint)vector)->z = z;
			return vector;
		}
		return (Vector3)new IndexOutOfRangeException();
	}

	public unsafe Vector3 GetLocationBottomLeft(Vector3[] gridBounds)
	{
		//IL_0083: Expected O, but got I
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_01c2: Expected O, but got I
		//IL_01d9: Expected native int or pointer, but got O
		//IL_01f5: Expected native int or pointer, but got O
		if (gridBounds.Length > 0 && gridBounds.Length > 1 && gridBounds.Length > 3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+4C]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
			object obj = num - 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			object obj2 = (object)this >> 2;
			object obj3 = obj2 >> 31;
			float num2 = (float)X * 0.005f;
			object obj4 = obj2 + obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			float num3 = (float)obj4 * 0.05f;
			float num4 = num2 + num3;
			object obj5 = obj4 >> 2;
			object obj6 = obj5 >> 31;
			object obj7 = obj5 + obj6;
			float num5 = (float)Y * 0.01f;
			object obj8 = obj7 * 4;
			object obj9 = obj7 + obj8;
			float num6 = (float)obj * num4;
			object obj10 = obj9 + obj9;
			object obj11 = Location - obj10;
			float num7 = num6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
			float num8 = num7 + 0f;
			float num9 = (float)obj11 * 0.1f;
			float num10 = num9 + num5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+34]");
			nint num11 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
			object obj12 = num11 - 0;
			float num12 = (float)obj12 * num10;
			Vector3 vector = default(Vector3);
			float x = default(float);
			((Vector3*)(nint)vector)->x = x;
			float z = num8 + num12;
			((Vector3*)(nint)vector)->z = z;
			return vector;
		}
		return (Vector3)new IndexOutOfRangeException();
	}

	public unsafe static GridReference FromLocalSpace(Vector2 localSpace, float cellWidth, float cellHeight, bool yIncreasesUp)
	{
		//IL_0015: Expected O, but got Ref
		object obj = default(object);
		return FromLocalSpace((Vector3)(&obj), cellWidth, cellHeight, yIncreasesUp);
	}

	public static GridReference FromLocalSpace(Vector3 localSpace, float cellWidth, float cellHeight, bool yIncreasesUp)
	{
		//IL_0303: Invalid comparison between I4 and F4
		//IL_000e: Invalid comparison between I4 and F4
		//IL_0351: Invalid comparison between F8 and I4
		//IL_007d: Expected F8, but got I4
		//IL_022f: Invalid comparison between F8 and I4
		//IL_0047: Invalid comparison between F8 and I4
		//IL_00c1: Expected F8, but got I4
		//IL_008b: Invalid comparison between F8 and I4
		//IL_006f: Expected F8, but got I4
		//IL_02b5: Invalid comparison between F8 and I4
		//IL_00b3: Expected F8, but got I4
		//IL_0105: Expected F8, but got I4
		//IL_00cf: Invalid comparison between F8 and I4
		//IL_012f: Invalid comparison between F8 and I4
		//IL_00f7: Expected F8, but got I4
		//IL_0182: Expected F8, but got I4
		//IL_014c: Invalid comparison between F8 and I4
		//IL_01b3: Expected I4, but got F8
		//IL_01df: Expected I4, but got F8
		//IL_01ec: Expected I4, but got F8
		//IL_0174: Expected F8, but got I4
		GridReference gridReference;
		float num = default(float);
		if (yIncreasesUp)
		{
			if (!(0f < cellWidth) || !(0f < cellHeight))
			{
				gridReference = new GridReference();
				goto IL_02f5;
			}
			num = localSpace.x / cellWidth;
		}
		double num2 = Math.Floor(num);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		double num3 = Math.Floor(0.0);
		double num4;
		if (!(num2 < 0.0))
		{
			bool flag = !(num2 > 19.0);
			num4 = num2;
			if (!flag)
			{
				num4 = 19.0;
			}
		}
		else
		{
			num4 = 0.0;
		}
		double num5;
		if (!(num3 < 0.0))
		{
			bool flag2 = !(num3 > 9.0);
			num5 = num3;
			if (!flag2)
			{
				num5 = 9.0;
			}
		}
		else
		{
			num5 = 0.0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,r14d\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,esi\"");
		float num6 = 0f * cellWidth;
		float num7 = localSpace.x - num6;
		float num8 = num7 / cellWidth;
		float num9 = num8 / 0.1f;
		double num10 = Math.Floor(num9);
		double num11;
		if (!(num10 < 0.0))
		{
			bool flag3 = !(num10 > 9.0);
			num11 = num10;
			if (!flag3)
			{
				num11 = 9.0;
			}
		}
		else
		{
			num11 = 0.0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		double num12 = Math.Floor(0.0);
		double num13;
		if (!(num12 < 0.0))
		{
			bool flag4 = !(num12 > 9.0);
			num13 = num12;
			if (!flag4)
			{
				num13 = 9.0;
			}
		}
		else
		{
			num13 = 0.0;
		}
		gridReference = new GridReference();
		if (gridReference != null)
		{
			double num14 = num4 * 4.0;
			double num15 = num4 + num14;
			gridReference.X = (int)num11;
			double num16 = num15 * 2.0;
			double num17 = num5 + num16;
			gridReference.Location = (GridLocations)num17;
			gridReference.Y = (int)num13;
			goto IL_02f5;
		}
		return (GridReference)(object)new NullReferenceException();
		IL_02f5:
		return gridReference;
	}

	public void Move(Vector2Int delta)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Expected I4, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected I4, but got Unknown
		//IL_038c: Expected I, but got O
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0136: Expected I4, but got O
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_0221: Expected I4, but got O
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected O, but got Unknown
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Expected O, but got Unknown
		//IL_0338: Expected F8, but got I4
		//IL_03b1: Invalid comparison between F8 and I4
		//IL_030a: Invalid comparison between F8 and I4
		//IL_0374: Expected F8, but got I4
		//IL_040d: Expected I4, but got F8
		//IL_0346: Invalid comparison between F8 and I4
		//IL_032a: Expected F8, but got I4
		//IL_0366: Expected F8, but got I4
		int num = (int)(X + delta);
		object obj = (object)delta >> 32;
		int y = Y + obj;
		Y = y;
		X = num;
		float num2 = (float)num / 10f;
		double num3 = Math.Floor(num2);
		float num4 = (float)Y / 10f;
		nint num5 = (nint)typeof(Math);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rcx_v4 (Il2CppClass<System.Math>)+E4]");
		bool flag = (nint)0 < (nint)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
		object obj2 = obj >> 2;
		object obj3 = obj2 >> 31;
		object obj4 = obj2 + obj3;
		object obj5 = 1 - obj4;
		object obj6 = obj5 * 4;
		object obj7 = obj5 + obj6;
		object obj8 = obj7 * 2;
		object obj9 = X + obj8;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
		object obj10 = obj4 >> 2;
		object obj11 = obj10 >> 31;
		object obj12 = obj10 + obj11;
		object obj13 = obj12 * 4;
		object obj14 = obj12 + obj13;
		object obj15 = obj14 + obj14;
		int x = obj9 - obj15;
		X = x;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
		object obj16 = obj12 >> 2;
		object obj17 = obj16 >> 31;
		object obj18 = obj16 + obj17;
		object obj19 = 1 - obj18;
		object obj20 = obj19 * 4;
		object obj21 = obj19 + obj20;
		object obj22 = obj21 * 2;
		object obj23 = Y + obj22;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
		object obj24 = obj18 >> 2;
		object obj25 = obj24 >> 31;
		object obj26 = obj24 + obj25;
		object obj27 = obj26 * 4;
		object obj28 = obj26 + obj27;
		object obj29 = obj28 + obj28;
		int y2 = obj23 - obj29;
		Y = y2;
		double num6 = Math.Floor(num4);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ebx\"");
		object obj30 = obj26 >> 2;
		object obj31 = obj30 >> 31;
		object obj32 = obj30 + obj31;
		object obj33 = obj32 * 4;
		object obj34 = obj32 + obj33;
		object obj35 = obj34 + obj34;
		object obj36 = Location - obj35;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ebx\"");
		double num7 = num6 + (double)obj36;
		object obj37 = obj35 >> 2;
		object obj38 = obj37 >> 31;
		object obj39 = obj38 + obj37;
		double num8 = (double)obj39 + num3;
		if (!flag)
		{
			if (num8 > 19.0)
			{
				num8 = 19.0;
			}
		}
		else
		{
			num8 = 0.0;
		}
		if (!(num7 < 0.0))
		{
			if (num7 > 9.0)
			{
				num7 = 9.0;
			}
		}
		else
		{
			num7 = 0.0;
		}
		double num9 = num8 * 4.0;
		double num10 = num8 + num9;
		double num11 = num10 * 2.0;
		double num12 = num7 + num11;
		Location = (GridLocations)num12;
	}

	public unsafe static Vector3 ClampToGridBounds(Vector3 targetPos, Vector3[] gridBounds)
	{
		//IL_00ca: Expected O, but got I
		//IL_00ec: Expected O, but got I
		//IL_0113: Expected O, but got I
		//IL_0144: Expected O, but got I
		//IL_02ad: Invalid comparison between I4 and F4
		//IL_02f8: Expected F4, but got I4
		//IL_0350: Invalid comparison between I4 and F4
		//IL_0334: Expected F4, but got I4
		//IL_03b0: Expected native int or pointer, but got O
		//IL_03bd: Expected native int or pointer, but got O
		if (gridBounds.Length > 0 && gridBounds.Length > 1 && gridBounds.Length > 3)
		{
			float num = targetPos.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+20]");
			float num2 = num - 0f;
			float num3 = targetPos.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
			float num4 = num3 - 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+44]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+20]");
			object obj = num5 - 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+4C]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
			object obj2 = num6 - 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+2C]");
			nint num7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+20]");
			object obj3 = num7 - 0;
			float num9 = default(float);
			float num8 = num9 - num9;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+34]");
			nint num10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
			object obj4 = num10 - 0;
			float num11 = num9 - num9;
			float num12 = num9 - num9;
			float num13 = num4 * (float)obj2;
			float num14 = num4 * (float)obj4;
			float num15 = num2 * (float)obj3;
			float num16 = num11 * num11;
			float num17 = num12 * num11;
			float num18 = num12 * num8;
			float num19 = num17 + num15;
			float num20 = num2 * (float)obj;
			object obj5 = obj3 * obj3;
			float num21 = num19 + num14;
			object obj6 = obj4 * obj4;
			float num22 = num18 + num20;
			float num23 = num16 + (float)obj5;
			object obj7 = obj * obj;
			float num24 = num22 + num13;
			float num25 = num23 + (float)obj6;
			object obj8 = obj2 * obj2;
			float num26 = num21 / num25;
			float num27 = num8 * num8;
			float num28 = num27 + (float)obj7;
			float num29 = num28 + (float)obj8;
			float num30 = num24 / num29;
			if (!(0f > num30))
			{
				if (num30 > 1f)
				{
					num30 = 1f;
				}
			}
			else
			{
				num30 = 0f;
			}
			if (!(0f > num26))
			{
				if (num26 > 1f)
				{
					num26 = 1f;
				}
			}
			else
			{
				num26 = 0f;
			}
			float num31 = (float)obj2 * num30;
			float num32 = num31;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
			float num33 = num32 + 0f;
			float num34 = num26 * (float)obj4;
			float z = num33 + num34;
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = num9;
			((Vector3*)(nint)vector)->z = z;
			return vector;
		}
		return (Vector3)new IndexOutOfRangeException();
	}

	public unsafe static Vector3 ClampToGridBoundsLocal(Vector3 localTargetPos, Transform localSpace, Vector3[] worldBounds)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0047: Expected O, but got I4
		//IL_0078: Expected O, but got Ref
		//IL_0161: Expected F4, but got I
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00bd: Expected O, but got F4
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00e5: Expected O, but got I
		//IL_01bf: Expected F4, but got I
		//IL_021d: Expected F4, but got I
		//IL_027b: Expected F4, but got I
		//IL_035a: Expected F4, but got I4
		//IL_039b: Expected F4, but got I
		//IL_02fa: Expected O, but got I4
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_0311: Expected O, but got I4
		//IL_031a: Expected O, but got I4
		//IL_0327: Invalid comparison between F4 and O
		//IL_03f9: Expected F4, but got I
		//IL_0b12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b17: Expected O, but got Unknown
		//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b25: Expected O, but got Unknown
		//IL_0b2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b33: Expected O, but got Unknown
		//IL_0346: Expected F4, but got O
		//IL_0457: Expected F4, but got I
		//IL_04b6: Expected F4, but got I
		//IL_0596: Expected F4, but got I4
		//IL_05d7: Expected F4, but got I
		//IL_0535: Expected O, but got I4
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Expected O, but got Unknown
		//IL_054c: Expected O, but got I4
		//IL_0555: Expected O, but got I4
		//IL_0563: Invalid comparison between O and F4
		//IL_0635: Expected F4, but got I
		//IL_0b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8f: Expected O, but got Unknown
		//IL_0b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9d: Expected O, but got Unknown
		//IL_0ba6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bab: Expected O, but got Unknown
		//IL_0582: Expected F4, but got O
		//IL_0693: Expected F4, but got I
		//IL_06f1: Expected F4, but got I
		//IL_07d0: Expected F4, but got I4
		//IL_0811: Expected F4, but got I
		//IL_0770: Expected O, but got I4
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Expected O, but got Unknown
		//IL_0787: Expected O, but got I4
		//IL_0790: Expected O, but got I4
		//IL_079d: Invalid comparison between F4 and O
		//IL_086f: Expected F4, but got I
		//IL_0c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c07: Expected O, but got Unknown
		//IL_0c10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c15: Expected O, but got Unknown
		//IL_0c1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c23: Expected O, but got Unknown
		//IL_07bc: Expected F4, but got O
		//IL_08cd: Expected F4, but got I
		//IL_092b: Expected F4, but got I
		//IL_0945: Expected F4, but got I4
		//IL_0ccc: Expected native int or pointer, but got O
		//IL_0cd9: Expected native int or pointer, but got O
		//IL_0ce6: Expected native int or pointer, but got O
		//IL_09b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Expected O, but got Unknown
		//IL_09d2: Expected O, but got I4
		//IL_09db: Expected O, but got I4
		//IL_09e8: Invalid comparison between O and F4
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Expected O, but got Unknown
		//IL_0c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5d: Expected O, but got Unknown
		//IL_0c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6b: Expected O, but got Unknown
		//IL_0a07: Expected F4, but got O
		Vector3[] array2 = default(Vector3[]);
		Vector3[] array = new Vector3[array2.Length];
		object obj = array2 + 24;
		object obj2 = (object)array2 - (object)array;
		object obj3 = array + 32;
		object obj4 = 0;
		object obj5 = default(object);
		float num3;
		float num5;
		float num6;
		while (true)
		{
			float num;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
				{
					Vector3 vector = localSpace.InverseTransformPoint((Vector3)(&obj5));
					if ((nint)obj4 < array.Length)
					{
						obj4++;
						obj3 = vector.x;
						_ = vector.z;
						obj3 += 12;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r14_v4+v103 @ rsi_v4]");
						obj5 = 0;
						array2 = null;
						continue;
					}
				}
			}
			else
			{
				float[] array3 = new float[4];
				if (array.Length > 0 && array3.Length > 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+20]");
					array3[0] = 0f;
					if (array.Length > 1 && array3.Length > 1)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+2C]");
						array3[1] = 0f;
						if (array.Length > 2 && array3.Length > 2)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+38]");
							array3[2] = 0f;
							if (array.Length > 3 && array3.Length > 3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+44]");
								array3[3] = 0f;
								if (array3.Length == 0)
								{
									num = 0f;
									goto IL_0ad9;
								}
								if (array3.Length > 0)
								{
									num = array3[0];
									if (array3.Length <= 1)
									{
										goto IL_0ad9;
									}
									array2 = (Vector3[])array3.Length;
									object obj6 = array3 + 36;
									object obj7 = 1;
									object obj8 = 1;
									while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) < System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref array2))
									{
										if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
										{
											num = (float)obj6;
										}
										obj8++;
										obj7++;
										obj6 += 4;
										if ((nint)obj7 < array3.Length)
										{
											continue;
										}
										goto IL_0ad9;
									}
								}
							}
						}
					}
				}
			}
			goto IL_0aac;
			IL_0aac:
			return (Vector3)new IndexOutOfRangeException();
			IL_0ad9:
			float[] array4 = new float[4];
			float num2;
			if (array.Length > 0 && array4.Length > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+20]");
				array4[0] = 0f;
				if (array.Length > 1 && array4.Length > 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+2C]");
					array4[1] = 0f;
					if (array.Length > 2 && array4.Length > 2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+38]");
						array4[2] = 0f;
						if (array.Length > 3 && array4.Length > 3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+44]");
							array4[3] = 0f;
							if (array4.Length == 0)
							{
								num2 = 0f;
								goto IL_0b51;
							}
							if (array4.Length > 0)
							{
								num2 = array4[0];
								if (array4.Length <= 1)
								{
									goto IL_0b51;
								}
								array2 = (Vector3[])array4.Length;
								object obj9 = array4 + 36;
								object obj10 = 1;
								object obj11 = 1;
								while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11) < System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref array2))
								{
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
									{
										num2 = (float)obj9;
									}
									obj11++;
									obj10++;
									obj9 += 4;
									if ((nint)obj10 < array4.Length)
									{
										continue;
									}
									goto IL_0b51;
								}
							}
						}
					}
				}
			}
			goto IL_0aac;
			IL_0bc9:
			float[] array5 = new float[4];
			if (array.Length > 0 && array5.Length > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+24]");
				array5[0] = 0f;
				if (array.Length > 1 && array5.Length > 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+30]");
					array5[1] = 0f;
					if (array.Length > 2 && array5.Length > 2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+3C]");
						array5[2] = 0f;
						if (array.Length > 3 && array5.Length > 3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+48]");
							array5[3] = 0f;
							bool flag = array5.Length == 0;
							num3 = 0f;
							if (flag)
							{
								goto IL_0a0c;
							}
							if (array5.Length > 0)
							{
								num3 = array5[0];
								if (array5.Length <= 1)
								{
									goto IL_0a0c;
								}
								object obj12 = array5 + 36;
								float num4 = array5[0];
								object obj13 = 1;
								object obj14 = 1;
								while ((nint)obj13 < array5.Length)
								{
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4))
									{
										num4 = (float)obj12;
									}
									obj13++;
									obj14++;
									obj12 += 4;
									bool flag2 = (nint)obj14 < array5.Length;
									num3 = num4;
									if (flag2)
									{
										continue;
									}
									goto IL_0a0c;
								}
							}
						}
					}
				}
			}
			goto IL_0aac;
			IL_0b51:
			float[] array6 = new float[4];
			if (array.Length > 0 && array6.Length > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+24]");
				array6[0] = 0f;
				if (array.Length > 1 && array6.Length > 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+30]");
					array6[1] = 0f;
					if (array.Length > 2 && array6.Length > 2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+3C]");
						array6[2] = 0f;
						if (array.Length > 3 && array6.Length > 3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (UnityEngine.Vector3[])+48]");
							array6[3] = 0f;
							if (array6.Length == 0)
							{
								num5 = 0f;
								goto IL_0bc9;
							}
							if (array6.Length > 0)
							{
								num5 = array6[0];
								if (array6.Length <= 1)
								{
									goto IL_0bc9;
								}
								array2 = (Vector3[])array6.Length;
								object obj15 = array6 + 36;
								object obj16 = 1;
								object obj17 = 1;
								while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17) < System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref array2))
								{
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj15))
									{
										num5 = (float)obj15;
									}
									obj17++;
									obj16++;
									obj15 += 4;
									if ((nint)obj16 < array6.Length)
									{
										continue;
									}
									goto IL_0bc9;
								}
							}
						}
					}
				}
			}
			goto IL_0aac;
			IL_0a0c:
			num6 = localTargetPos.x;
			if (!(num > localTargetPos.x))
			{
				if (num6 > num2)
				{
					num6 = num2;
				}
			}
			else
			{
				num6 = num;
			}
			break;
		}
		float num7 = localTargetPos.y;
		if (!(num5 > localTargetPos.y))
		{
			if (num7 > num3)
			{
				num7 = num3;
			}
		}
		else
		{
			num7 = num5;
		}
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->z = localTargetPos.z;
		((Vector3*)(nint)vector2)->x = num6;
		((Vector3*)(nint)vector2)->y = num7;
		return vector2;
	}
}
