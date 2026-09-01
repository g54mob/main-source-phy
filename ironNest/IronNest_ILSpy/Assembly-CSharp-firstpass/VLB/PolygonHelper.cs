using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class PolygonHelper : MonoBehaviour
{
	public struct Plane2D
	{
		public Vector2 normal;

		public float distance;

		public float Distance(Vector2 point)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.PolygonHelper+Plane2D)+4]");
			object obj2 = default(object);
			object obj = obj2 * 0;
			object obj3 = point * normal;
			object obj4 = obj + obj3;
			return (float)obj4 + distance;
		}

		public Vector2 ClosestPoint(Vector2 pt)
		{
			Vector2 result = default(Vector2);
			return result;
		}

		public Vector2 Intersect(Vector2 p1, Vector2 p2)
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			object obj2 = default(object);
			object obj3 = default(object);
			object obj = obj2 - obj3;
			object obj4 = p1 - p2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.PolygonHelper+Plane2D)+4]");
			object obj5 = obj * 0;
			object obj6 = obj4 * (object)normal;
			float f = (float)obj5 + (float)obj6;
			if (!Utils.IsAlmostZero(f))
			{
			}
			Vector2 result = default(Vector2);
			return result;
		}

		public bool GetSide(Vector2 point)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Expected O, but got Unknown
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Expected O, but got Unknown
			object obj = point * normal;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.PolygonHelper+Plane2D)+4]");
			object obj3 = default(object);
			object obj2 = obj3 * 0;
			object obj4 = obj2 + obj;
			object obj5 = obj4 + distance;
			bool flag = (nint)obj5 < 0;
			bool flag2 = obj5 == null;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}

		public unsafe static Plane2D FromPoints(Vector3 p1, Vector3 p2)
		{
			//IL_000e: Expected O, but got I4
			//IL_0009: Expected native int or pointer, but got O
			//IL_0017: Expected native int or pointer, but got O
			//IL_004f: Invalid comparison between O and F4
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Expected O, but got Unknown
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Expected O, but got Unknown
			//IL_0095: Expected native int or pointer, but got O
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Expected O, but got Unknown
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_00dd: Expected native int or pointer, but got O
			Plane2D plane2D = default(Plane2D);
			((Plane2D*)(nint)plane2D)->normal = (Vector2)0;
			((Plane2D*)(nint)plane2D)->distance = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			Vector3 vector2 = default(Vector3);
			Vector3 vector = ((System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f)) ? Vector3.zeroVector : vector2);
			object obj = vector * p1.y;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj2 = vector ^ 0;
			Vector2 vector3 = default(Vector2);
			((Plane2D*)(nint)plane2D)->normal = vector3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj3 = vector3 ^ 0;
			object obj4 = obj3 * p1.x;
			float num = (float)obj4 + (float)obj;
			((Plane2D*)(nint)plane2D)->distance = num;
			return plane2D;
		}

		public unsafe static Plane2D FromNormalAndPoint(Vector3 normalizedNormal, Vector3 p1)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Expected O, but got Unknown
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_005c: Expected O, but got F4
			//IL_0057: Expected native int or pointer, but got O
			//IL_0073: Expected native int or pointer, but got O
			float x = normalizedNormal.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj = x ^ 0;
			object obj2 = obj * p1.x;
			float num = normalizedNormal.y * p1.y;
			Plane2D plane2D = default(Plane2D);
			((Plane2D*)(nint)plane2D)->normal = (Vector2)normalizedNormal.x;
			float num2 = (float)obj2 - num;
			((Plane2D*)(nint)plane2D)->distance = num2;
			return plane2D;
		}

		public void Flip()
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Expected O, but got Unknown
			Vector2 vector = (Vector2)(normal ^ -0f);
			normal = vector;
			float num = distance ^ -0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.PolygonHelper+Plane2D)+4]");
			object obj = 0 ^ -0f;
			distance = num;
		}

		public unsafe Vector2[] CutConvex(Vector2[] poly)
		{
			//IL_0024: Expected O, but got I4
			//IL_0034: Expected O, but got I4
			//IL_0065: Expected O, but got I
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0083: Expected O, but got I
			//IL_008c: Expected O, but got I4
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Expected O, but got Unknown
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Expected O, but got Unknown
			//IL_013d: Expected O, but got I
			//IL_0160: Expected O, but got I4
			//IL_0177: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Expected O, but got Unknown
			//IL_01bd: Expected O, but got I4
			//IL_01e4: Expected O, but got I4
			//IL_0331: Expected O, but got Ref
			//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d9: Expected O, but got Unknown
			//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e7: Expected O, but got Unknown
			//IL_02f7: Expected O, but got I
			//IL_025d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0262: Expected O, but got Unknown
			//IL_02b0: Expected O, but got Ref
			List<Vector2> list = new List<Vector2>(poly.Length);
			object obj = poly.Length;
			object obj2 = poly.Length - 1;
			if ((nint)obj2 < poly.Length)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [poly @ rdx (UnityEngine.Vector2[])+18+v196 @ rcx_v6*8]");
				Vector2 vector = (Vector2)0;
				object obj3 = poly + 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [poly @ rdx (UnityEngine.Vector2[])+1C+v196 @ rcx_v6*8]");
				Vector2 vector2 = (Vector2)0;
				object obj4 = 0;
				Vector2 vector5 = default(Vector2);
				Vector2 vector6 = default(Vector2);
				while (true)
				{
					if ((nint)obj4 < poly.Length)
					{
						if ((nint)obj4 >= poly.Length)
						{
							break;
						}
						Vector2 vector3 = vector2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.PolygonHelper+Plane2D)+4]");
						object obj5 = vector3 * 0;
						object obj6 = vector * normal;
						object obj7 = obj5 + obj6;
						object obj8 = obj3 * (object)normal;
						object obj9 = obj7 + distance;
						bool flag = (nint)obj9 < 0;
						bool flag2 = obj9 == null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ r14_v4+4]");
						nint num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.PolygonHelper+Plane2D)+4]");
						object obj10 = num * 0;
						bool flag3 = !flag;
						bool flag4 = !flag2;
						object obj11 = flag4 & flag3;
						object obj12 = obj10 + obj8;
						object obj13 = obj12 + distance;
						bool flag5 = (nint)obj13 < 0;
						bool flag6 = obj13 == null;
						bool flag7 = !flag5;
						bool flag8 = !flag6;
						object obj14 = flag8 & flag7;
						object obj15 = obj11 & obj14;
						bool flag9 = obj15 == null;
						object obj16 = !flag9;
						if (obj16 == null)
						{
							if (obj11 != null && obj14 == null)
							{
								Vector2 vector4 = Intersect(vector5, vector5);
								vector6 = vector4;
							}
							else
							{
								object obj17 = obj11 ^ 1;
								object obj18 = obj14 & obj17;
								if (obj18 == null)
								{
									goto IL_02cb;
								}
								Vector2 vector7 = Intersect(vector5, vector5);
								list.Add((Vector2)(&vector6));
								vector6 = vector5;
							}
						}
						else
						{
							vector6 = vector5;
						}
						list.Add((Vector2)(&vector6));
						goto IL_02cb;
					}
					return list.ToArray();
					IL_02cb:
					obj4++;
					obj3 += 8;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ r14_v4+4]");
					vector2 = (Vector2)0;
					vector = (Vector2)obj3;
				}
			}
			return (Vector2[])(object)new IndexOutOfRangeException();
		}

		public override string ToString()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39CEB]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			object arg3 = default(object);
			return $"{arg} x {arg2} + {arg3}";
		}
	}
}
