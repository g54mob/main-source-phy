using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public static class ShapesMeshGen
{
	private enum ReflexState
	{
		Unknown,
		Reflex,
		Convex
	}

	private class EarClipPoint
	{
		public int vertIndex;

		public Vector2 pt;

		private ReflexState reflex;

		public EarClipPoint prev;

		public EarClipPoint next;

		public ReflexState ReflexState
		{
			get
			{
				//IL_01f8: Expected I4, but got O
				//IL_004b: Expected O, but got I
				//IL_00b6: Expected O, but got I4
				//IL_0146: Expected O, but got I
				//IL_019d: Expected O, but got I4
				//IL_0250: Expected O, but got I4
				//IL_0288: Invalid comparison between O and F4
				//IL_01e5: Expected O, but got I8
				if (reflex == ReflexState.Unknown)
				{
					EarClipPoint earClipPoint = next;
					if (next != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rax_v4 (Shapes.ShapesMeshGen+EarClipPoint)+18]");
						nint num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v26 @ rcx_v1 (Shapes.ShapesMeshGen+EarClipPoint)+18]");
						object obj = num - 0;
						object obj2 = earClipPoint.pt - pt;
						object obj3 = obj * obj;
						object obj4 = obj2 * obj2;
						object obj5 = obj4 + obj3;
						object obj6;
						if (0 <= (nint)obj5)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm1\"");
							obj6 = 0;
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
							obj6 = obj5;
						}
						EarClipPoint earClipPoint2 = prev;
						object obj7 = obj2 / obj6;
						object obj8 = obj / obj6;
						if (prev != null)
						{
							object obj9 = pt - earClipPoint2.pt;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v26 @ rcx_v1 (Shapes.ShapesMeshGen+EarClipPoint)+18]");
							nint num2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rax_v8 (Shapes.ShapesMeshGen+EarClipPoint)+18]");
							object obj10 = num2 - 0;
							object obj11 = obj9 * obj9;
							object obj12 = obj10 * obj10;
							object obj13 = obj11 + obj12;
							object obj14;
							if (0 <= (nint)obj13)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm1\"");
								obj14 = 0;
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
								obj14 = obj13;
							}
							object obj15 = obj9 / obj14;
							object obj16 = obj10 / obj14;
							object obj17 = obj15 * obj8;
							object obj18 = obj16 * obj7;
							bool flag = generatingClockwisePolygon;
							object obj19 = 1;
							if (!flag)
							{
								obj19 = 4294967295L;
							}
							object obj20 = obj17 - obj18;
							object obj21 = obj20 * obj19;
							bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj21) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)(-0.001f));
							ReflexState reflexState = (ReflexState)((flag2 ? 1 : 0) + 1);
							reflex = reflexState;
							goto IL_025e;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (ReflexState)ex;
				}
				goto IL_025e;
				IL_025e:
				return reflex;
			}
		}

		public EarClipPoint(int vertIndex, Vector2 pt)
		{
			this.vertIndex = vertIndex;
			this.pt = pt;
		}

		public void MarkReflexUnknown()
		{
			reflex = ReflexState.Unknown;
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<EarClipPoint, string> _003C_003E9__12_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003CGenPolygonMesh_003Eb__12_0(EarClipPoint p)
		{
			//IL_002a: Expected I4, but got O
			if (p != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				ReflexState reflexState = p.ReflexState;
				object obj = default(object);
				object arg = (ReflexState)obj;
				object arg2 = default(object);
				return $"[{arg2}]: {arg}";
			}
			return (string)(object)new NullReferenceException();
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass17_0
	{
		public int[] triIndices;

		public int tri;
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass8_0
	{
		public int triId;
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass8_1
	{
		public Vector3 prevPos;

		public Vector3 nextPos;
	}

	private static readonly ExpandoList<Color> meshColors;

	private static readonly ExpandoList<Vector3> meshVertices;

	private static readonly ExpandoList<Vector4> meshUv0;

	private static readonly ExpandoList<Vector3> meshUv1Prevs;

	private static readonly ExpandoList<Vector3> meshUv2Nexts;

	private static readonly ExpandoList<int> meshTriangles;

	private static readonly ExpandoList<int> meshJoinsTriangles;

	private static bool generatingClockwisePolygon;

	private static bool SamePosition(Vector3 a, Vector3 b)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00eb: Invalid comparison between F4 and O
		//IL_010a: Invalid comparison between F4 and I4
		float num = b.x - a.x;
		float num2 = b.y - a.y;
		float num3 = b.z - a.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num2 & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
		{
			obj = obj2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj3 = num3 & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
		{
			obj = obj3;
		}
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		float num4 = 1E-05f - (float)obj;
		bool flag2 = num4 == 0f;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		return flag4 & flag3;
	}

	public unsafe static void GenPolylineMesh(Mesh mesh, IList<PolylinePoint> path, bool closed, PolylineJoins joins, bool flattenZ, bool useColors)
	{
		//IL_0008: Expected O, but got Ref
		//IL_21da: Expected F4, but got I4
		//IL_0030: Expected F4, but got I4
		//IL_0036: Expected O, but got I
		//IL_006f: Expected F4, but got I4
		//IL_0075: Expected O, but got I
		//IL_00ae: Expected F4, but got I4
		//IL_00b4: Expected O, but got I
		//IL_00ed: Expected F4, but got I4
		//IL_00f3: Expected O, but got I
		//IL_012c: Expected F4, but got I4
		//IL_0132: Expected O, but got I
		//IL_016b: Expected F4, but got I4
		//IL_0171: Expected O, but got I
		//IL_01a9: Expected F4, but got I4
		//IL_01af: Expected O, but got I
		//IL_1c8e: Expected F4, but got I4
		//IL_1c96: Expected I4, but got O
		//IL_01fa: Expected O, but got Ref
		//IL_2227: Expected O, but got Ref
		//IL_024d: Expected F4, but got I
		//IL_0255: Expected F4, but got O
		//IL_02d5: Expected F4, but got I
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected F4, but got Unknown
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Expected F4, but got Unknown
		//IL_028a: Expected I4, but got O
		//IL_02a8: Expected I4, but got O
		//IL_2259: Expected I4, but got O
		//IL_2277: Expected I4, but got O
		//IL_0400: Expected O, but got I4
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Expected O, but got Unknown
		//IL_1dc2: Expected O, but got I4
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Expected I4, but got Unknown
		//IL_0396: Expected O, but got Ref
		//IL_22aa: Expected O, but got I4
		//IL_22b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_22b8: Expected O, but got Unknown
		//IL_22c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_22c6: Expected O, but got Unknown
		//IL_22ef: Expected O, but got I4
		//IL_03b5: Expected F4, but got I
		//IL_03bd: Expected F4, but got O
		//IL_03d2: Expected I4, but got O
		//IL_03e8: Expected I4, but got O
		//IL_04f1: Expected O, but got I4
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Expected O, but got Unknown
		//IL_1aba: Expected O, but got I
		//IL_2320: Expected O, but got Ref
		//IL_233d: Expected O, but got Ref
		//IL_236e: Expected F4, but got I
		//IL_06a3: Expected F4, but got O
		//IL_06c7: Expected F4, but got O
		//IL_05cd: Expected O, but got Ref
		//IL_0607: Expected F4, but got I4
		//IL_0610: Expected F4, but got I4
		//IL_1af9: Expected O, but got I
		//IL_0696: Expected I4, but got I8
		//IL_077f: Expected O, but got I4
		//IL_1b75: Expected O, but got I
		//IL_2384: Unknown result type (might be due to invalid IL or missing references)
		//IL_2389: Expected I4, but got Unknown
		//IL_06da: Expected O, but got Ref
		//IL_244b: Expected I4, but got O
		//IL_2455: Expected I4, but got O
		//IL_0999: Expected O, but got Ref
		//IL_09c8: Expected O, but got Ref
		//IL_0797: Expected O, but got Ref
		//IL_1e63: Expected O, but got I
		//IL_0730: Expected F4, but got I
		//IL_1b9d: Expected O, but got I
		//IL_1ba6: Expected I4, but got O
		//IL_1bb0: Expected I4, but got O
		//IL_1b35: Expected O, but got I
		//IL_1b4e: Expected O, but got I
		//IL_0a05: Expected O, but got Ref
		//IL_07c6: Expected O, but got Ref
		//IL_0a34: Expected O, but got Ref
		//IL_1be5: Expected O, but got I
		//IL_1bee: Expected I4, but got O
		//IL_1bf8: Expected I4, but got O
		//IL_1c09: Expected O, but got I4
		//IL_0a63: Expected O, but got Ref
		//IL_0838: Expected O, but got Ref
		//IL_1c31: Expected O, but got I
		//IL_1035: Expected O, but got Ref
		//IL_08e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e7: Expected O, but got Unknown
		//IL_08f5: Expected O, but got Ref
		//IL_0862: Expected O, but got Ref
		//IL_0ab7: Expected O, but got Ref
		//IL_1faf: Expected O, but got F4
		//IL_101a: Expected F4, but got I
		//IL_0efc: Expected F4, but got I
		//IL_1efe: Expected O, but got I
		//IL_1f0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f13: Expected I4, but got Unknown
		//IL_0ae1: Expected O, but got Ref
		//IL_1e8e: Expected O, but got Ref
		//IL_1ea0: Expected F4, but got O
		//IL_1eb0: Expected F4, but got I
		//IL_0f5b: Expected O, but got Ref
		//IL_0f73: Expected O, but got Ref
		//IL_0923: Expected F4, but got O
		//IL_0933: Expected F4, but got I
		//IL_0bf9: Expected O, but got Ref
		//IL_0b19: Expected O, but got Ref
		//IL_0f48: Expected F4, but got O
		//IL_0c28: Expected O, but got Ref
		//IL_0b43: Expected O, but got Ref
		//IL_1c70: Expected O, but got I
		//IL_0c57: Expected O, but got Ref
		//IL_0b6d: Expected O, but got Ref
		//IL_0cab: Expected O, but got Ref
		//IL_14e6: Expected O, but got Ref
		//IL_0d8e: Expected O, but got Ref
		//IL_0cd5: Expected O, but got Ref
		//IL_1529: Expected O, but got Ref
		//IL_154f: Expected F4, but got I4
		//IL_0dbd: Expected O, but got Ref
		//IL_0cff: Expected O, but got Ref
		//IL_2049: Expected I4, but got O
		//IL_11ab: Expected O, but got Ref
		//IL_0e51: Expected O, but got Ref
		//IL_11e0: Expected O, but got Ref
		//IL_0e7b: Expected O, but got Ref
		//IL_1a82: Expected O, but got I
		//IL_1231: Expected O, but got Ref
		//IL_1270: Expected O, but got Ref
		//IL_166e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1673: Expected I4, but got Unknown
		//IL_12af: Expected O, but got Ref
		//IL_15e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e9: Expected I4, but got Unknown
		//IL_1308: Expected F4, but got I4
		//IL_1303: Expected native int or pointer, but got O
		//IL_1310: Expected native int or pointer, but got O
		//IL_131e: Expected native int or pointer, but got O
		//IL_1335: Expected O, but got Ref
		//IL_2089: Expected O, but got I4
		//IL_1468: Expected O, but got Ref
		//IL_1732: Expected O, but got I4
		//IL_14b0: Expected O, but got Ref
		//IL_1368: Expected O, but got Ref
		//IL_13a7: Expected O, but got Ref
		//IL_17aa: Expected O, but got I4
		//IL_13ea: Expected O, but got Ref
		//IL_1432: Expected O, but got Ref
		//IL_20bd: Expected O, but got I4
		//IL_185c: Expected O, but got I4
		//IL_18a2: Expected O, but got I4
		//IL_1904: Expected O, but got I4
		//IL_194a: Expected O, but got I4
		//IL_19ba: Expected O, but got I4
		object obj = default(object);
		Vector4 vector = (Vector4)(&obj);
		_ = 0;
		_ = 0;
		bool flag = meshColors == null;
		float num = 0f;
		IList<PolylinePoint> list = path;
		PolylineJoins polylineJoins2 = default(PolylineJoins);
		PolylineJoins polylineJoins = polylineJoins2;
		bool flag12;
		float num2;
		object obj6;
		object obj7;
		object obj8 = default(object);
		float num6 = default(float);
		bool flag11 = default(bool);
		if (!flag)
		{
			meshColors.Clear();
			bool flag2 = meshVertices == null;
			num = 0f;
			list = (IList<PolylinePoint>)0;
			polylineJoins = polylineJoins2;
			if (!flag2)
			{
				meshVertices.Clear();
				bool flag3 = meshUv0 == null;
				num = 0f;
				list = (IList<PolylinePoint>)0;
				polylineJoins = polylineJoins2;
				if (!flag3)
				{
					meshUv0.Clear();
					bool flag4 = meshUv1Prevs == null;
					num = 0f;
					list = (IList<PolylinePoint>)0;
					polylineJoins = polylineJoins2;
					if (!flag4)
					{
						meshUv1Prevs.Clear();
						bool flag5 = meshUv2Nexts == null;
						num = 0f;
						list = (IList<PolylinePoint>)0;
						polylineJoins = polylineJoins2;
						if (!flag5)
						{
							meshUv2Nexts.Clear();
							bool flag6 = meshTriangles == null;
							num = 0f;
							list = (IList<PolylinePoint>)0;
							polylineJoins = polylineJoins2;
							if (!flag6)
							{
								meshTriangles.Clear();
								bool flag7 = meshJoinsTriangles == null;
								num = 0f;
								list = (IList<PolylinePoint>)0;
								polylineJoins = polylineJoins2;
								if (!flag7)
								{
									meshJoinsTriangles.Clear();
									bool flag8 = path == null;
									num = 0f;
									list = (IList<PolylinePoint>)0;
									polylineJoins = polylineJoins2;
									if (!flag8)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
										object obj2 = default(object);
										if ((nint)obj2 >= 2)
										{
											object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 192));
											bool flag9 = (nint)obj2 != 2;
											bool flag10 = false;
											if (!flag9)
											{
												flag10 = flag11;
											}
											flag12 = false;
											if (!flag10)
											{
												flag12 = flag11;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1343 @ rax_v77+10]");
											_ = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
											object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 576));
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1578 @ rax_v80+10]");
											num = 0f;
											object obj5 = default(object);
											num2 = (float)obj5;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1578 @ rax_v80+10]");
											_ = 0;
											PolylineJoins polylineJoins3;
											if (!flag12)
											{
												bool flag13 = (nint)obj2 != 2;
												polylineJoins3 = (PolylineJoins)path;
												obj6 = obj2;
												obj7 = obj2;
												flag11 = (byte)(int)typeof(IList<PolylinePoint>) != 0;
												list = null;
												if (flag13)
												{
													goto IL_1d2f;
												}
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
											float num3 = 0f;
											float num4 = num2 - (float)obj8;
											float num5 = num6 - num6;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
											float num7 = num4 & 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
											float num8 = num5 & 0;
											if (!(num7 > num8))
											{
												num7 = num8;
											}
											float num9 = num6 - num6;
											float num10 = num9 & num3;
											if (!(num7 > num10))
											{
												num7 = num10;
											}
											bool flag14 = !(1E-05f > num7);
											num = 1E-05f;
											polylineJoins3 = (PolylineJoins)path;
											obj6 = obj2;
											obj7 = obj2;
											flag11 = (byte)(int)typeof(IList<PolylinePoint>) != 0;
											list = null;
											if (!flag14)
											{
												obj6 = obj2 - 1;
												if ((nint)obj6 < 2)
												{
													return;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
												object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 192));
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1874 @ rax_v400+10]");
												num = 0f;
												object obj10 = default(object);
												num2 = (float)obj10;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1874 @ rax_v400+10]");
												_ = 0;
												polylineJoins3 = (PolylineJoins)path;
												obj7 = obj6;
												flag11 = (byte)(int)typeof(IList<PolylinePoint>) != 0;
												list = null;
											}
											goto IL_1d2f;
										}
										bool flag15 = (object)mesh == null;
										num = 0f;
										flag11 = (byte)(int)path != 0;
										list = (IList<PolylinePoint>)typeof(ICollection<PolylinePoint>);
										if (!flag15)
										{
											mesh.Clear();
											return;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_1cc0;
		IL_1dd0:
		PolylineJoins polylineJoins5;
		PolylineJoins polylineJoins4 = polylineJoins5;
		goto IL_1da7;
		IL_1da7:
		bool flag16 = polylineJoins != PolylineJoins.Simple;
		IList<PolylinePoint> list2 = (IList<PolylinePoint>)5;
		if (!flag16)
		{
			list2 = (IList<PolylinePoint>)2;
		}
		object obj11 = (object)list2 * obj6;
		object obj12 = polylineJoins5 ^ PolylineJoins.Miter;
		object obj13 = obj12 * 2;
		object obj14 = obj13 + 3;
		bool flag17 = (nint)obj6 <= 0;
		polylineJoins2 = PolylineJoins.Simple;
		list = (IList<PolylinePoint>)2;
		Mesh mesh2 = mesh;
		IList<PolylinePoint> list3;
		PolylineJoins polylineJoins15;
		bool flag19;
		if (!flag17)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
			list3 = (IList<PolylinePoint>)(obj6 - 1);
			PolylineJoins polylineJoins6 = PolylineJoins.Simple;
			PolylineJoins polylineJoins7 = PolylineJoins.Simple;
			PolylineJoins polylineJoins8 = PolylineJoins.Simple;
			PolylineJoins polylineJoins9 = PolylineJoins.Simple;
			PolylineJoins polylineJoins10 = PolylineJoins.Simple;
			PolylineJoins polylineJoins11 = PolylineJoins.Miter;
			PolylineJoins polylineJoins12 = PolylineJoins.Simple;
			PolylineJoins polylineJoins13 = PolylineJoins.Simple;
			PolylineJoins polylineJoins14 = PolylineJoins.Simple;
			polylineJoins15 = polylineJoins;
			num = num2;
			PolylineJoins polylineJoins16 = PolylineJoins.Simple;
			bool flag18 = false;
			IList<PolylinePoint> list4 = list3;
			PolylineJoins polylineJoins17 = PolylineJoins.Simple;
			object obj18 = default(object);
			object obj21 = default(object);
			object obj24 = default(object);
			ref _003C_003Ec__DisplayClass8_0 reference = default(ref _003C_003Ec__DisplayClass8_0);
			int num25 = default(int);
			PolylineJoins polylineJoins19 = default(PolylineJoins);
			PolylineJoins polylineJoins20 = default(PolylineJoins);
			object obj28 = default(object);
			while (true)
			{
				bool flag20;
				PolylineJoins polylineJoins18;
				if (flag12)
				{
					polylineJoins15 = polylineJoins;
					flag19 = flag11;
					flag20 = true;
				}
				else
				{
					if ((nint)polylineJoins17 == (nint)list4)
					{
						flag20 = false;
					}
					else
					{
						bool flag21 = polylineJoins17 == PolylineJoins.Simple;
						flag20 = !flag21;
					}
					if (polylineJoins17 != PolylineJoins.Simple)
					{
						bool flag22 = polylineJoins17 != PolylineJoins.Simple;
						polylineJoins18 = polylineJoins11;
						if (!flag22)
						{
							polylineJoins18 = (PolylineJoins)(-1);
						}
						goto IL_2312;
					}
				}
				polylineJoins18 = polylineJoins16;
				goto IL_2312;
				IL_1f68:
				_003C_003Ec__DisplayClass8_1 obj17;
				float num7;
				if (polylineJoins17 != PolylineJoins.Simple)
				{
					object obj15 = obj7 - 1;
					object obj16 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 112));
					if ((nint)polylineJoins17 == (nint)obj15)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
						bool flag23 = !flag12;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3992 @ rax_v280+10]");
						num = 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3992 @ rax_v280+10]");
						_ = 0;
						if (!flag23)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)-38]");
							_ = 0;
							obj17 = (_003C_003Ec__DisplayClass8_1)obj18;
							num7 = (float)obj8;
						}
						else
						{
							object obj19 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 112));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
							object obj20 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 112));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4201 @ rax_v282+10]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4277 @ rax_v285+10]");
							_ = 0;
							float num11 = num6 + num6;
							float num12 = num11 - num6;
							float num13 = num6;
							obj17 = (_003C_003Ec__DisplayClass8_1)obj18;
							num7 = num6;
							float num3 = num6;
							num = num6;
						}
						goto IL_108f;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
					float num14 = (float)obj21;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3991 @ rax_v288+10]");
					float num15 = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3991 @ rax_v288+10]");
					_ = 0;
					obj17 = (_003C_003Ec__DisplayClass8_1)obj21;
				}
				else
				{
					float num14;
					if (flag12)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)-34]");
						float num16 = 0f;
						num14 = num2;
					}
					else
					{
						object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 112));
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4000 @ rax_v275+10]");
						_ = 0;
						float num17 = num6 + num6;
						float num3 = num17 - num6;
						float num13 = num6;
						num14 = num6;
						float num15 = num6;
						float num16 = num3;
					}
					obj17 = (_003C_003Ec__DisplayClass8_1)num14;
				}
				object obj23 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 112));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
				num7 = (float)obj24;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4192 @ rax_v273+10]");
				num = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4192 @ rax_v273+10]");
				_ = 0;
				goto IL_108f;
				IL_155d:
				bool flag24 = (nint)polylineJoins17 == (nint)list3;
				bool flag25 = flag12;
				if (!flag24)
				{
					flag25 = true;
				}
				bool flag26 = !flag25;
				bool flag27 = true;
				nint num18;
				polylineJoins2 = (PolylineJoins)num18;
				Vector4 vector2;
				flag11 = (byte)(int)vector2 != 0;
				int num20;
				int num22;
				int num24;
				int num30;
				if (!flag26)
				{
					if (polylineJoins15 == PolylineJoins.Simple)
					{
						int num19 = (((nint)polylineJoins17 != (nint)list3) ? (list2 + num20) : 0);
						int num21 = num19 + 1;
						int a = num20 + 1;
						_003CGenPolylineMesh_003Eg__AddQuad_007C8_2(a, num20, num19, num21, ref reference);
						polylineJoins2 = (PolylineJoins)num21;
						flag11 = (byte)num19 != 0;
					}
					else
					{
						polylineJoins = (((nint)polylineJoins17 != (nint)list3) ? ((PolylineJoins)(list2 + num20)) : PolylineJoins.Simple);
						int c = (int)(polylineJoins + 1);
						_003CGenPolylineMesh_003Eg__AddQuad_007C8_2(num20, num22, c, (int)polylineJoins, ref reference);
						int num23 = (int)(polylineJoins + 3);
						_003CGenPolylineMesh_003Eg__AddQuad_007C8_2((int)polylineJoins, num23, num24, num20, ref reference);
						bool flag28 = !flag20;
						polylineJoins2 = (PolylineJoins)num20;
						flag11 = (byte)num24 != 0;
						if (!flag28)
						{
							bool flag29 = meshJoinsTriangles == null;
							polylineJoins2 = (PolylineJoins)num20;
							flag11 = (byte)num24 != 0;
							list = (IList<PolylinePoint>)num23;
							if (flag29)
							{
								break;
							}
							meshJoinsTriangles.set_Item((int)polylineJoins10, (int)(&num25));
							bool flag30 = meshJoinsTriangles == null;
							polylineJoins2 = PolylineJoins.Simple;
							flag11 = (byte)(&num25) != 0;
							list = (IList<PolylinePoint>)polylineJoins10;
							polylineJoins = polylineJoins10;
							if (flag30)
							{
								break;
							}
							int i = (int)(polylineJoins10 + 1);
							meshJoinsTriangles.set_Item(i, (int)(&num25));
							int num26 = (int)(polylineJoins10 + 2);
							int num27 = num26 + 1;
							bool flag31 = meshJoinsTriangles == null;
							polylineJoins2 = PolylineJoins.Simple;
							flag11 = (byte)(&num25) != 0;
							list = (IList<PolylinePoint>)num26;
							polylineJoins = polylineJoins10;
							if (flag31)
							{
								break;
							}
							meshJoinsTriangles.set_Item(num26, (int)(&num25));
							bool flag32 = polylineJoins4 != PolylineJoins.Simple;
							polylineJoins10 = (PolylineJoins)num27;
							num25 = (int)polylineJoins8;
							polylineJoins2 = PolylineJoins.Simple;
							flag11 = (byte)(&num25) != 0;
							if (!flag32)
							{
								bool flag33 = meshJoinsTriangles == null;
								polylineJoins2 = PolylineJoins.Simple;
								flag11 = (byte)(&num25) != 0;
								list = (IList<PolylinePoint>)num26;
								polylineJoins = polylineJoins8;
								if (flag33)
								{
									break;
								}
								meshJoinsTriangles.set_Item(num27, (int)(&polylineJoins19));
								polylineJoins = (PolylineJoins)(num27 + 1);
								bool flag34 = meshJoinsTriangles == null;
								polylineJoins2 = PolylineJoins.Simple;
								flag11 = (byte)(&polylineJoins19) != 0;
								list = (IList<PolylinePoint>)num27;
								if (flag34)
								{
									break;
								}
								meshJoinsTriangles.set_Item((int)polylineJoins, (int)(&polylineJoins20));
								bool flag35 = meshJoinsTriangles == null;
								polylineJoins2 = PolylineJoins.Simple;
								flag11 = (byte)(&polylineJoins20) != 0;
								list = (IList<PolylinePoint>)polylineJoins;
								if (flag35)
								{
									break;
								}
								int num28 = (int)(polylineJoins + 1);
								meshJoinsTriangles.set_Item(num28, (int)(&num25));
								polylineJoins += 2;
								bool flag36 = meshJoinsTriangles == null;
								polylineJoins2 = PolylineJoins.Simple;
								flag11 = (byte)(&num25) != 0;
								list = (IList<PolylinePoint>)num28;
								if (flag36)
								{
									break;
								}
								meshJoinsTriangles.set_Item((int)polylineJoins, (int)(&num25));
								bool flag37 = meshJoinsTriangles == null;
								polylineJoins2 = PolylineJoins.Simple;
								flag11 = (byte)(&num25) != 0;
								list = (IList<PolylinePoint>)polylineJoins;
								if (flag37)
								{
									break;
								}
								int i2 = (int)(polylineJoins + 1);
								meshJoinsTriangles.set_Item(i2, (int)(&num25));
								int num29 = (int)(polylineJoins + 2);
								PolylineJoins polylineJoins21 = (PolylineJoins)(num29 + 1);
								bool flag38 = meshJoinsTriangles == null;
								polylineJoins2 = PolylineJoins.Simple;
								flag11 = (byte)(&num25) != 0;
								list = (IList<PolylinePoint>)num29;
								if (flag38)
								{
									break;
								}
								meshJoinsTriangles.set_Item(num29, (int)(&num25));
								polylineJoins10 = polylineJoins21;
								num25 = num30;
								polylineJoins2 = PolylineJoins.Simple;
								flag11 = (byte)(&num25) != 0;
							}
						}
					}
					flag27 = true;
				}
				polylineJoins17++;
				int num31;
				int num32;
				int num33;
				int num34;
				if ((nint)polylineJoins17 < (nint)obj7)
				{
					polylineJoins6 = (PolylineJoins)num30;
					polylineJoins7 = (PolylineJoins)num31;
					polylineJoins9 = (PolylineJoins)num32;
					polylineJoins11 = (flag27 ? PolylineJoins.Miter : PolylineJoins.Simple);
					polylineJoins12 = (PolylineJoins)num24;
					polylineJoins13 = (PolylineJoins)num33;
					polylineJoins14 = (PolylineJoins)num22;
					polylineJoins16 = PolylineJoins.Simple;
					flag18 = (byte)num34 != 0;
					flag11 = flag19;
					list4 = list3;
					polylineJoins = polylineJoins15;
					continue;
				}
				goto IL_1a72;
				IL_2312:
				object obj25 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 608));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
				object obj26 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 112));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)+370]");
				bool flag39 = (nint)0 == 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2805 @ rax_v130+10]");
				float num35 = 0f;
				if (!flag39)
				{
					object obj27 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 112));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2805 @ rax_v130+10]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2917 @ rax_v385+10]");
					_ = 0;
					float num36 = num6;
					num35 = 0f;
					float num37 = 0f;
				}
				else
				{
					float num36 = (float)obj28;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2805 @ rax_v130+10]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
					float num37 = (float)obj28;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)+378]");
				if ((nint)0 != 0)
				{
					object obj29 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 112));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1801BA220");
					ColorSpace activeColorSpace = QualitySettings.activeColorSpace;
					if (activeColorSpace == ColorSpace.Linear)
					{
						float num38 = Mathf.GammaToLinearSpace(num6);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3051 @ rax_v383+10]");
						float num39 = Mathf.GammaToLinearSpace(0f);
						float num40 = Mathf.GammaToLinearSpace(num6);
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3051 @ rax_v383+10]");
						_ = 0;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)-50]");
					object obj30 = 0;
				}
				else
				{
					object obj30 = 0;
				}
				num20 = list2 * polylineJoins17;
				int num45;
				if (polylineJoins15 == PolylineJoins.Simple)
				{
					Vector3 value = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 224));
					meshVertices.set_Item(num20, value);
					Vector3 value2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 240));
					int i3 = num20 + 1;
					meshVertices.set_Item(i3, value2);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)+378]");
					bool flag40 = (nint)0 == 0;
					num22 = (int)polylineJoins14;
					float num15 = num6;
					if (!flag40)
					{
						Color value3 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 416));
						meshColors.set_Item(num20, value3);
						Color value4 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 432));
						int i4 = num20 + 1;
						meshColors.set_Item(i4, value4);
						num22 = (int)polylineJoins14;
						num15 = num6;
					}
				}
				else
				{
					int num41 = num20 + 2;
					int num42 = num20 + 3;
					PolylineJoins polylineJoins22 = (PolylineJoins)(num20 + 4);
					Vector3 value5 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 384));
					meshVertices.set_Item(num20, value5);
					Vector3 value6 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 256));
					int i5 = num20 + 1;
					meshVertices.set_Item(i5, value6);
					Vector3 value7 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 272));
					meshVertices.set_Item(num41, value7);
					Vector3 value8 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 288));
					meshVertices.set_Item(num42, value8);
					Vector3 value9 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 304));
					meshVertices.set_Item((int)polylineJoins22, value9);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)+378]");
					if ((nint)0 != 0)
					{
						Color value10 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 448));
						meshColors.set_Item(num20, value10);
						Color value11 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 464));
						int i6 = num20 + 1;
						meshColors.set_Item(i6, value11);
						Color value12 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 480));
						meshColors.set_Item(num41, value12);
						Color value13 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 496));
						meshColors.set_Item(num42, value13);
						Color value14 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 560));
						meshColors.set_Item((int)polylineJoins22, value14);
					}
					bool flag41 = !flag20;
					polylineJoins12 = polylineJoins22;
					polylineJoins13 = (PolylineJoins)num42;
					num22 = num41;
					float num15 = num6;
					if (!flag41)
					{
						PolylineJoins polylineJoins23 = polylineJoins17;
						if (!flag12)
						{
							polylineJoins23 = polylineJoins17 - 1;
						}
						PolylineJoins num43 = polylineJoins23;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)-40]");
						object obj31 = (nint)num43 * (nint)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)-3C]");
						num34 = 0 + obj31;
						int num44 = num34 + 1;
						num45 = num34 + 2;
						int num46 = num34 + 3;
						int num47 = num34 + 4;
						Vector3 value15 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 400));
						meshVertices.set_Item(num34, value15);
						Vector3 value16 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 352));
						meshVertices.set_Item(num44, value16);
						Vector3 value17 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 368));
						meshVertices.set_Item(num45, value17);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)+378]");
						if ((nint)0 != 0)
						{
							Color value18 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 512));
							meshColors.set_Item(num34, value18);
							Color value19 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 528));
							meshColors.set_Item(num44, value19);
							Color value20 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 544));
							meshColors.set_Item(num45, value20);
						}
						bool flag42 = polylineJoins4 != PolylineJoins.Simple;
						num30 = num47;
						num31 = num46;
						polylineJoins8 = (PolylineJoins)num45;
						num32 = num44;
						num24 = (int)polylineJoins22;
						num33 = num42;
						num22 = num41;
						num15 = num6;
						if (!flag42)
						{
							Vector3 value21 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 336));
							meshVertices.set_Item(num46, value21);
							Vector3 value22 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 320));
							meshVertices.set_Item(num47, value22);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)+378]");
							bool flag43 = (nint)0 == 0;
							num30 = num47;
							num31 = num46;
							polylineJoins8 = (PolylineJoins)num45;
							num32 = num44;
							num24 = (int)polylineJoins22;
							num33 = num42;
							num22 = num41;
							num15 = num6;
							if (!flag43)
							{
								Color value23 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 576));
								meshColors.set_Item(num46, value23);
								Color value24 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 192));
								meshColors.set_Item(num47, value24);
								num30 = num47;
								num31 = num46;
								polylineJoins8 = (PolylineJoins)num45;
								num32 = num44;
								num24 = (int)polylineJoins22;
								num33 = num42;
								num22 = num41;
								num15 = num6;
							}
						}
						goto IL_1f68;
					}
				}
				num30 = (int)polylineJoins6;
				num31 = (int)polylineJoins7;
				num32 = (int)polylineJoins9;
				num24 = (int)polylineJoins12;
				num33 = (int)polylineJoins13;
				num34 = (flag18 ? 1 : 0);
				num45 = (int)polylineJoins8;
				goto IL_1f68;
				IL_108f:
				_003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(num20, ref obj17);
				int atIndex = num20 + 1;
				_003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(atIndex, ref obj17);
				ExpandoList<Vector4> expandoList;
				int i8;
				if (polylineJoins15 != PolylineJoins.Simple)
				{
					_003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(num22, ref obj17);
					_003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(num33, ref obj17);
					_003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(num24, ref obj17);
					if (flag20)
					{
						_003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(num34, ref obj17);
						_003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(num32, ref obj17);
						_003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(num45, ref obj17);
						if (polylineJoins4 == PolylineJoins.Simple)
						{
							_003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(num31, ref obj17);
							_003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(num30, ref obj17);
						}
					}
					Vector4 value25 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 96));
					_ = 0;
					meshUv0.set_Item(num20, value25);
					Vector4 value26 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 112));
					int i7 = num20 + 1;
					_ = 3212836864L;
					_ = 3212836864L;
					meshUv0.set_Item(i7, value26);
					Vector4 value27 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 128));
					_ = 3212836864L;
					_ = 1065353216;
					meshUv0.set_Item(num22, value27);
					Vector4 value28 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 144));
					_ = 1065353216;
					_ = 3212836864L;
					meshUv0.set_Item(num33, value28);
					vector2 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 160));
					_ = 1065353216;
					_ = 1065353216;
					meshUv0.set_Item(num24, vector2);
					bool flag44 = !flag20;
					num18 = 0;
					if (flag44)
					{
						goto IL_155d;
					}
					((Vector4*)(nint)vector)->z = (float)polylineJoins18;
					((Vector4*)(nint)vector)->w = num6;
					((Vector4*)(nint)vector)->x = 0f;
					meshUv0.set_Item(num34, (Vector4)(&obj));
					if (polylineJoins4 == PolylineJoins.Simple)
					{
						Vector4 value29 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 176));
						_ = 1065353216;
						_ = 3212836864L;
						meshUv0.set_Item(num32, value29);
						Vector4 value30 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 32));
						_ = 3212836864L;
						_ = 3212836864L;
						meshUv0.set_Item((int)polylineJoins8, value30);
						Vector4 value31 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 16));
						_ = 3212836864L;
						_ = 1065353216;
						meshUv0.set_Item(num31, value31);
						expandoList = meshUv0;
						vector2 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 16));
						_ = 1065353216;
						_ = 1065353216;
						i8 = num30;
					}
					else
					{
						Vector4 value32 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 32));
						_ = 1065353216;
						_ = 3212836864L;
						meshUv0.set_Item(num32, value32);
						expandoList = meshUv0;
						vector2 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 48));
						_ = 1065353216;
						_ = 1065353216;
						i8 = (int)polylineJoins8;
					}
				}
				else
				{
					Vector4 value33 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 80));
					_ = 3212836864L;
					meshUv0.set_Item(num20, value33);
					vector2 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 64));
					i8 = num20 + 1;
					_ = 1065353216;
					num = (float)polylineJoins17;
					expandoList = meshUv0;
				}
				expandoList.set_Item(i8, vector2);
				num18 = 0;
				goto IL_155d;
			}
			goto IL_1cc0;
		}
		goto IL_1dea;
		IL_1cc0:
		throw new NullReferenceException();
		IL_1dea:
		if ((object)mesh2 != null)
		{
			mesh2.Clear();
			list = (IList<PolylinePoint>)meshVertices;
			if (meshVertices != null)
			{
				Mesh mesh3 = mesh2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v729 @ rdx_v23 (System.Collections.Generic.IList`1<Shapes.PolylinePoint>)+10]");
				mesh3.SetVertices((List<Vector3>)0);
				int subMeshCount = (flag19 ? 1 : 0) + 1;
				mesh2.subMeshCount = subMeshCount;
				list = (IList<PolylinePoint>)meshTriangles;
				bool flag45 = meshTriangles == null;
				flag11 = false;
				if (!flag45)
				{
					Mesh mesh4 = mesh2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v729 @ rdx_v23 (System.Collections.Generic.IList`1<Shapes.PolylinePoint>)+10]");
					mesh4.SetTriangles((List<int>)0, 0);
					if (polylineJoins == PolylineJoins.Simple)
					{
						polylineJoins2 = PolylineJoins.Simple;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v729 @ rdx_v23 (System.Collections.Generic.IList`1<Shapes.PolylinePoint>)+10]");
						list = (IList<PolylinePoint>)0;
						polylineJoins = PolylineJoins.Miter;
					}
					else
					{
						list = (IList<PolylinePoint>)meshJoinsTriangles;
						bool flag46 = meshJoinsTriangles == null;
						polylineJoins2 = PolylineJoins.Simple;
						flag11 = false;
						if (flag46)
						{
							goto IL_1cc0;
						}
						Mesh mesh5 = mesh2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v729 @ rdx_v23 (System.Collections.Generic.IList`1<Shapes.PolylinePoint>)+10]");
						mesh5.SetTriangles((List<int>)0, 1);
						polylineJoins2 = PolylineJoins.Simple;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v729 @ rdx_v23 (System.Collections.Generic.IList`1<Shapes.PolylinePoint>)+10]");
						list = (IList<PolylinePoint>)0;
						polylineJoins = PolylineJoins.Miter;
					}
					flag11 = (byte)(int)meshUv0 != 0;
					if ((int)(~meshUv0) == 0)
					{
						Mesh mesh6 = mesh2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v727 @ r8_v23 (System.Boolean)+10]");
						mesh6.SetUVs(0, (List<Vector4>)0);
						flag11 = (byte)(int)meshUv1Prevs != 0;
						bool flag47 = (byte)(int)(~meshUv1Prevs) != 0;
						polylineJoins2 = PolylineJoins.Simple;
						list = null;
						if (!flag47)
						{
							Mesh mesh7 = mesh2;
							PolylineJoins channel = polylineJoins;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v727 @ r8_v23 (System.Boolean)+10]");
							mesh7.SetUVs((int)channel, (List<Vector3>)0);
							flag11 = (byte)(int)meshUv2Nexts != 0;
							bool flag48 = (byte)(int)(~meshUv2Nexts) != 0;
							polylineJoins2 = PolylineJoins.Simple;
							list = (IList<PolylinePoint>)polylineJoins;
							if (!flag48)
							{
								Mesh mesh8 = mesh2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v727 @ r8_v23 (System.Boolean)+10]");
								mesh8.SetUVs(2, (List<Vector3>)0);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)+378]");
								if ((nint)0 == 0)
								{
									return;
								}
								list = (IList<PolylinePoint>)meshColors;
								bool flag49 = meshColors == null;
								polylineJoins2 = PolylineJoins.Simple;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v727 @ r8_v23 (System.Boolean)+10]");
								flag11 = false;
								if (!flag49)
								{
									Mesh mesh9 = mesh2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v729 @ rdx_v23 (System.Collections.Generic.IList`1<Shapes.PolylinePoint>)+10]");
									mesh9.SetColors((List<Color>)0);
									return;
								}
							}
						}
					}
				}
			}
		}
		goto IL_1cc0;
		IL_1a72:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1 (UnityEngine.Vector4)+350]");
		mesh2 = (Mesh)0;
		list = list3;
		polylineJoins = polylineJoins15;
		goto IL_1dea;
		IL_1d2f:
		bool flag50 = polylineJoins2 == PolylineJoins.Simple;
		if (!flag50)
		{
			object obj32 = polylineJoins2 - 1;
			if (!flag50)
			{
				PolylineJoins polylineJoins24 = (PolylineJoins)(obj32 - 1);
				if (!flag50)
				{
					bool flag51 = polylineJoins24 != PolylineJoins.Miter;
					polylineJoins = polylineJoins2;
					if (!flag51)
					{
						flag19 = (byte)polylineJoins24 != 0;
						polylineJoins4 = polylineJoins24;
						flag11 = (byte)polylineJoins24 != 0;
						polylineJoins5 = polylineJoins24;
						polylineJoins = polylineJoins24;
						goto IL_1da7;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					object actualValue = default(object);
					ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("join", actualValue, null);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				flag19 = true;
				flag11 = true;
				polylineJoins5 = PolylineJoins.Simple;
				polylineJoins = PolylineJoins.Miter;
				goto IL_1dd0;
			}
			flag11 = false;
			polylineJoins5 = PolylineJoins.Simple;
			polylineJoins = PolylineJoins.Simple;
		}
		else
		{
			flag11 = false;
			polylineJoins5 = PolylineJoins.Simple;
			polylineJoins = PolylineJoins.Simple;
		}
		flag19 = flag11;
		goto IL_1dd0;
	}

	public unsafe static void GenPolygonMesh(Mesh mesh, List<Vector2> path, PolygonTriangulation triangulation)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0039: Expected O, but got I4
		//IL_0044: Expected O, but got I4
		//IL_006a: Expected O, but got I4
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0163: Expected O, but got I
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0c57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5c: Expected O, but got Unknown
		//IL_0c65: Expected O, but got I4
		//IL_1034: Expected O, but got Ref
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c82: Expected O, but got Unknown
		//IL_0e4a: Expected O, but got Ref
		//IL_0d39: Expected O, but got Ref
		//IL_0c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca3: Expected O, but got Unknown
		//IL_01e3: Expected O, but got Ref
		//IL_0cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc4: Expected O, but got Unknown
		//IL_0cd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd8: Expected O, but got Unknown
		//IL_0d80: Expected O, but got I
		//IL_0d96: Expected O, but got I
		//IL_0db5: Expected O, but got I
		//IL_0e73: Expected O, but got I
		//IL_0e7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e80: Expected O, but got Unknown
		//IL_0e90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e95: Expected I4, but got Unknown
		//IL_0267: Expected O, but got I4
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Expected I4, but got Unknown
		//IL_029e: Expected O, but got I4
		//IL_02bc: Expected I4, but got O
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Expected O, but got Unknown
		//IL_0313: Expected O, but got I4
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Expected O, but got Unknown
		//IL_0b99: Expected O, but got I4
		//IL_0f98: Expected O, but got Ref
		//IL_0fb2: Expected O, but got I
		//IL_038e: Expected O, but got I
		//IL_0bd4: Expected O, but got I4
		//IL_03c5: Expected O, but got I4
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Expected O, but got Unknown
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Expected I4, but got Unknown
		//IL_03f2: Expected O, but got I4
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Expected I4, but got Unknown
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Expected O, but got Unknown
		//IL_0914: Expected O, but got I4
		//IL_091d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Expected O, but got Unknown
		//IL_0942: Expected O, but got I4
		//IL_0567: Expected O, but got Ref
		//IL_0588: Expected O, but got I
		//IL_0665: Expected O, but got I
		//IL_068f: Expected O, but got I
		//IL_06b1: Expected O, but got I
		//IL_06fe: Expected O, but got I4
		//IL_072f: Expected O, but got I4
		//IL_0738: Expected O, but got I4
		//IL_0762: Expected O, but got I4
		//IL_0793: Expected O, but got I4
		//IL_079c: Expected O, but got I4
		//IL_07b4: Expected O, but got I
		//IL_07c4: Expected O, but got I
		//IL_07e1: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
		bool flag = (nint)0 <= (nint)0;
		int num = 0;
		Vector2 vector = (Vector2)0;
		Vector2 vector2 = (Vector2)0;
		if (!flag)
		{
			object obj6 = default(object);
			object obj7 = default(object);
			object obj9 = default(object);
			object obj10 = default(object);
			bool flag2;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj3 = num + 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
				object obj4 = obj3 % 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				num++;
				object obj5 = obj6 + obj7;
				object obj8 = obj9 - obj10;
				object obj11 = obj5 * obj8;
				vector = (Vector2)((object)vector + obj11);
				int num2 = num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
				flag2 = (nint)num2 < (nint)0;
				vector2 = vector;
			}
			while (flag2);
		}
		bool flag3 = (nint)vector2 < 0;
		bool flag4 = (object)vector2 == null;
		bool flag5 = !flag3;
		bool flag6 = !flag4;
		bool flag7 = flag6 & flag5;
		generatingClockwisePolygon = flag7;
		float num3 = ((!generatingClockwisePolygon) ? (-1f) : 1f);
		mesh.Clear();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
		if ((nint)0 < (nint)2)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
		object obj12 = -2;
		object obj13 = obj12 * 2;
		object obj14 = obj12 + obj13;
		int[] array = new int[obj14];
		int[] array2;
		int num10;
		List<Vector3> list2;
		if (triangulation != PolygonTriangulation.FastConvexOnly)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
			List<EarClipPoint> list = new List<EarClipPoint>(0);
			int num4 = 0;
			object obj15 = default(object);
			Vector2 pt = default(Vector2);
			int num5;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				EarClipPoint earClipPoint = new EarClipPoint(0, (Vector2)(&obj15));
				earClipPoint._002Ector(0, (Vector2)(&obj15));
				earClipPoint.pt = pt;
				earClipPoint.vertIndex = num4;
				list.Add(earClipPoint);
				num4++;
				num5 = num4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
			}
			while ((nint)num5 < (nint)0);
			int num6 = 0;
			bool flag8;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
				object obj16 = -1;
				object obj17 = obj16 + num6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
				int num7 = obj17 % 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj18 = num6 + 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
				int num8 = obj18 % 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				EarClipPoint earClipPoint2 = (EarClipPoint)(num6 + 1);
				EarClipPoint earClipPoint3 = earClipPoint2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
				flag8 = (nint)earClipPoint3 < 0;
				num6 = (int)earClipPoint2;
			}
			while (flag8);
			object obj19 = array + 36;
			int num9 = 0;
			obj15 = obj19;
			array2 = array;
			Vector2 vector3 = (Vector2)obj19;
			Vector2 vector5 = default(Vector2);
			Vector2 vector4 = vector5;
			num10 = 0;
			object obj20 = 1000000;
			object obj26 = default(object);
			float num15 = default(float);
			object obj28 = default(object);
			object obj30 = default(object);
			object obj32 = default(object);
			object obj34 = default(object);
			object obj36 = default(object);
			object obj38 = default(object);
			Vector2 vector8 = default(Vector2);
			object obj40 = default(object);
			object obj42 = default(object);
			Vector2 vector10 = default(Vector2);
			object obj44 = default(object);
			Vector2 vector12 = default(Vector2);
			Vector2 vector16 = default(Vector2);
			float aMargin = default(float);
			float bMargin = default(float);
			float cMargin = default(float);
			object obj45 = default(object);
			object obj46 = default(object);
			object obj47 = default(object);
			object obj48 = default(object);
			while (list._size >= 3)
			{
				object obj21 = obj20 - 1;
				bool flag9 = (nint)obj20 <= 0;
				obj20 = obj21;
				if (flag9)
				{
					break;
				}
				if (list._size != 3)
				{
					int num11 = num10;
					EarClipPoint earClipPoint4;
					object obj25;
					float num14;
					object obj27;
					object obj29;
					object obj31;
					object obj33;
					object obj35;
					object obj37;
					Vector2 vector7;
					object obj39;
					object obj41;
					Vector2 vector9;
					object obj43;
					Vector2 vector11;
					Vector2 vector13;
					Vector2 vector14;
					Vector2 vector15;
					EarClipPoint earClipPoint2;
					while (true)
					{
						Vector2 vector6 = (Vector2)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-80]");
						earClipPoint4 = (EarClipPoint)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-80]");
						ReflexState reflexState = ((EarClipPoint)0).ReflexState;
						if (reflexState == ReflexState.Convex)
						{
							object obj22 = list._size - 1;
							object obj23 = obj22 + num11;
							int num12 = obj23 % list._size;
							object obj24 = num11 + 1;
							int num13 = obj24 % list._size;
							earClipPoint2 = null;
							while (true)
							{
								bool flag10 = (nint)earClipPoint2 == num11;
								obj25 = obj26;
								num14 = num15;
								obj27 = obj28;
								obj29 = obj30;
								obj31 = obj32;
								obj33 = obj34;
								obj35 = obj36;
								obj37 = obj38;
								vector7 = vector8;
								obj39 = obj40;
								obj41 = obj42;
								vector9 = vector10;
								obj43 = obj44;
								vector11 = vector12;
								vector13 = vector3;
								vector14 = vector4;
								vector15 = vector2;
								if (!flag10)
								{
									bool flag11 = (nint)earClipPoint2 == num12;
									obj25 = obj26;
									num14 = num15;
									obj27 = obj28;
									obj29 = obj30;
									obj31 = obj32;
									obj33 = obj34;
									obj35 = obj36;
									obj37 = obj38;
									vector7 = vector8;
									obj39 = obj40;
									obj41 = obj42;
									vector9 = vector10;
									obj43 = obj44;
									vector11 = vector12;
									vector13 = vector3;
									vector14 = vector4;
									vector15 = vector2;
									if (!flag11)
									{
										bool flag12 = (nint)earClipPoint2 == num13;
										obj25 = obj26;
										num14 = num15;
										obj27 = obj28;
										obj29 = obj30;
										obj31 = obj32;
										obj33 = obj34;
										obj35 = obj36;
										obj37 = obj38;
										vector7 = vector8;
										obj39 = obj40;
										obj41 = obj42;
										vector9 = vector10;
										obj43 = obj44;
										vector11 = vector12;
										vector13 = vector3;
										vector14 = vector4;
										vector15 = vector2;
										if (!flag12)
										{
											vector6 = (Vector2)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 120));
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-78]");
											ReflexState reflexState2 = ((EarClipPoint)0).ReflexState;
											bool flag13 = reflexState2 != ReflexState.Reflex;
											obj25 = obj26;
											num14 = num15;
											obj27 = obj28;
											obj29 = obj30;
											obj31 = obj32;
											obj33 = obj34;
											obj35 = obj36;
											obj37 = obj38;
											vector7 = vector8;
											obj39 = obj40;
											obj41 = obj42;
											vector9 = vector10;
											obj43 = obj44;
											vector11 = vector12;
											vector13 = vector3;
											vector14 = vector4;
											vector15 = vector2;
											if (!flag13)
											{
												EarClipPoint next = earClipPoint4.next;
												vector11 = next.pt;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v351 @ rax_v115 (Shapes.ShapesMeshGen+EarClipPoint)+18]");
												obj43 = 0;
												EarClipPoint prev = earClipPoint4.prev;
												vector9 = earClipPoint4.pt;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ rdi_v19 (Shapes.ShapesMeshGen+EarClipPoint)+18]");
												obj41 = 0;
												vector15 = prev.pt;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v116 (Shapes.ShapesMeshGen+EarClipPoint)+18]");
												obj39 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
												float num16 = num3 * -0.0001f;
												bool flag14 = ShapesMath.PointInsideTriangle(vector16, vector16, vector16, vector16, aMargin, bMargin, cMargin);
												obj25 = 0;
												num14 = num16;
												obj27 = obj45;
												obj29 = obj46;
												obj31 = obj47;
												obj33 = obj48;
												obj35 = 0;
												obj37 = 0;
												vector7 = vector16;
												vector13 = vector16;
												vector14 = vector16;
												vector6 = vector16;
												obj26 = 0;
												num15 = num16;
												obj28 = obj45;
												obj30 = obj46;
												obj32 = obj47;
												obj34 = obj48;
												obj36 = 0;
												obj38 = 0;
												vector8 = vector16;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v116 (Shapes.ShapesMeshGen+EarClipPoint)+18]");
												obj40 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ rdi_v19 (Shapes.ShapesMeshGen+EarClipPoint)+18]");
												obj42 = 0;
												vector10 = earClipPoint4.pt;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v351 @ rax_v115 (Shapes.ShapesMeshGen+EarClipPoint)+18]");
												obj44 = 0;
												vector12 = next.pt;
												vector3 = vector16;
												vector4 = vector16;
												vector2 = prev.pt;
												num10 = num12;
												if (flag14)
												{
													break;
												}
											}
										}
									}
								}
								earClipPoint2 = (EarClipPoint)(earClipPoint2 + 1);
								bool flag15 = (nint)earClipPoint2 < list._size;
								obj26 = obj25;
								num15 = num14;
								obj28 = obj27;
								obj30 = obj29;
								obj32 = obj31;
								obj34 = obj33;
								obj36 = obj35;
								obj38 = obj37;
								vector8 = vector7;
								obj40 = obj39;
								obj42 = obj41;
								vector10 = vector9;
								obj44 = obj43;
								vector12 = vector11;
								vector3 = vector13;
								vector4 = vector14;
								vector2 = vector15;
								if (!flag15)
								{
									goto end_IL_0f8a;
								}
							}
						}
						num11++;
						if (num11 < list._size)
						{
							continue;
						}
						goto IL_0ad2;
						continue;
						end_IL_0f8a:
						break;
					}
					EarClipPoint next2 = earClipPoint4.next;
					_ = next2.vertIndex;
					int num17 = num9 + 3;
					obj15 = earClipPoint4.vertIndex;
					object obj49 = obj15 + 12;
					EarClipPoint prev2 = earClipPoint4.prev;
					object obj50 = num9 + 2;
					array[obj50] = prev2.vertIndex;
					EarClipPoint next3 = earClipPoint4.next;
					next3.reflex = ReflexState.Unknown;
					EarClipPoint prev3 = earClipPoint4.prev;
					prev3.reflex = ReflexState.Unknown;
					EarClipPoint next4 = earClipPoint4.next;
					earClipPoint2 = earClipPoint4.prev;
					next4.prev = earClipPoint4.prev;
					earClipPoint2.next = next4;
					list.RemoveAt(num11);
					obj26 = obj25;
					num15 = num14;
					obj28 = obj27;
					obj30 = obj29;
					obj32 = obj31;
					obj34 = obj33;
					obj36 = obj35;
					obj38 = obj37;
					vector8 = vector7;
					obj40 = obj39;
					obj42 = obj41;
					vector10 = vector9;
					obj44 = obj43;
					vector12 = vector11;
					num9 = num17;
					obj15 = obj49;
					array2 = array;
					vector3 = vector13;
					vector4 = vector14;
					vector2 = vector15;
					num10 = 0;
					obj20 = obj21;
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				int[] array3 = array2;
				int num18 = num9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ stack_-118 (UnityEngine.Vector2)+10]");
				array3[num18] = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj51 = num9 + 1;
				int[] array4 = array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ stack_-118 (UnityEngine.Vector2)+10]");
				array4[obj51] = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj52 = num9 + 2;
				int[] array5 = array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ stack_-118 (UnityEngine.Vector2)+10]");
				array5[obj52] = 0;
				obj20 = obj21;
				break;
				IL_0ad2:
				string text = "Invalid polygon triangulation - no convex edges found. Your polygon is likely self-intersecting.\n" + "Failed point set:\n";
				Func<EarClipPoint, string> selector = _003C_003Ec._003C_003E9__12_0;
				if (_003C_003Ec._003C_003E9__12_0 == null)
				{
					selector = (_003C_003Ec._003C_003E9__12_0 = delegate(EarClipPoint p)
					{
						//IL_002a: Expected I4, but got O
						if (p != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							ReflexState reflexState3 = p.ReflexState;
							object obj58 = default(object);
							object arg = (ReflexState)obj58;
							object arg2 = default(object);
							return $"[{arg2}]: {arg}";
						}
						return (string)(object)new NullReferenceException();
					});
				}
				IEnumerable<string> values = Enumerable.Select(list, selector);
				string text2 = string.Join("\n", values);
				string message = text + text2;
				Debug.LogError(message);
				array2 = array;
				num10 = 0;
				obj20 = obj21;
				break;
			}
			if ((nint)obj20 < 1)
			{
				Debug.LogError("Polygon triangulation failed, please report a bug (Shapes/Report Bug) with this exact case included");
				list2 = null;
				goto IL_0d12;
			}
		}
		else
		{
			bool flag16 = (nint)obj12 <= 0;
			array2 = array;
			num10 = 0;
			if (!flag16)
			{
				object obj53 = array + 36;
				object obj54 = 1;
				int num19 = 0;
				bool flag17;
				do
				{
					object obj55 = obj54 + 1;
					obj53 = obj54;
					obj53 += 12;
					int num20 = num19 + 3;
					obj54++;
					_ = 0;
					object obj56 = obj54 - 1;
					flag17 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj56) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12);
					num19 = num20;
				}
				while (flag17);
				array2 = array;
				num10 = 0;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
		list2 = new List<Vector3>(0);
		goto IL_0d12;
		IL_0d12:
		List<Vector3> list3 = list2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
		list3._002Ector(0);
		Vector2 vector17 = default(Vector2);
		int num21;
		do
		{
			object obj57 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 104));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			list2.Add((Vector3)(&vector17));
			num10++;
			num21 = num10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [path @ rdx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
		}
		while ((nint)num21 < (nint)0);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+60]");
		((Mesh)0).SetVertices(list2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+60]");
		((Mesh)0).subMeshCount = 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+60]");
		((Mesh)0).SetTriangles(array2, 0);
	}

	public static void CreateDisc(Mesh mesh, int segmentsPerFullTurn, float radius)
	{
		float radius2 = default(float);
		float radiusInner = default(float);
		float angRadiansStart = default(float);
		float angRadiansEnd = default(float);
		GenerateDiscMesh(mesh, segmentsPerFullTurn, hasSector: false, hasInnerRadius: false, radius2, radiusInner, angRadiansStart, angRadiansEnd);
	}

	public static void CreateCircleSector(Mesh mesh, int segmentsPerFullTurn, float radius, float angRadiansStart, float angRadiansEnd)
	{
		float radius2 = default(float);
		float radiusInner = default(float);
		float angRadiansStart2 = default(float);
		float angRadiansEnd2 = default(float);
		GenerateDiscMesh(mesh, segmentsPerFullTurn, hasSector: true, hasInnerRadius: false, radius2, radiusInner, angRadiansStart2, angRadiansEnd2);
	}

	public static void CreateAnnulus(Mesh mesh, int segmentsPerFullTurn, float radius, float radiusInner)
	{
		float radius2 = default(float);
		float radiusInner2 = default(float);
		float angRadiansStart = default(float);
		float angRadiansEnd = default(float);
		GenerateDiscMesh(mesh, segmentsPerFullTurn, hasSector: true, hasInnerRadius: false, radius2, radiusInner2, angRadiansStart, angRadiansEnd);
	}

	public static void CreateAnnulusSector(Mesh mesh, int segmentsPerFullTurn, float radius, float radiusInner, float angRadiansStart, float angRadiansEnd)
	{
		float radius2 = default(float);
		float radiusInner2 = default(float);
		float angRadiansStart2 = default(float);
		float angRadiansEnd2 = default(float);
		GenerateDiscMesh(mesh, segmentsPerFullTurn, hasSector: true, hasInnerRadius: false, radius2, radiusInner2, angRadiansStart2, angRadiansEnd2);
	}

	private static void GenerateDiscMesh(Mesh mesh, int segmentsPerFullTurn, bool hasSector, bool hasInnerRadius, float radius, float radiusInner, float angRadiansStart, float angRadiansEnd)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0069: Expected O, but got I4
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Expected O, but got Unknown
		//IL_004b: Expected F4, but got I
		//IL_005b: Expected O, but got I
		//IL_060d: Expected O, but got I
		//IL_00aa: Expected O, but got I
		//IL_0080: Expected O, but got I4
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Expected O, but got Unknown
		//IL_0095: Expected O, but got I
		//IL_00f1: Expected O, but got I4
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Expected O, but got Unknown
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Expected O, but got Unknown
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_00e3: Expected O, but got I
		//IL_0524: Expected O, but got I
		//IL_0539: Expected O, but got I
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_017e: Expected F4, but got I4
		//IL_0187: Expected O, but got I4
		//IL_0195: Expected I, but got O
		//IL_0553: Expected O, but got I
		//IL_0564: Expected O, but got I
		//IL_02a3: Expected O, but got I4
		//IL_02ac: Expected O, but got I4
		//IL_02b5: Expected O, but got I4
		//IL_07fd: Invalid comparison between I4 and F4
		//IL_01d2: Expected F4, but got I4
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_081f: Expected O, but got Unknown
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_02d9: Expected I4, but got O
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Expected O, but got Unknown
		//IL_02fd: Expected I4, but got O
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Expected O, but got Unknown
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected O, but got Unknown
		//IL_032f: Expected I4, but got O
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Expected O, but got Unknown
		//IL_07ad: Expected I, but got O
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Expected O, but got Unknown
		//IL_0361: Expected I4, but got O
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Expected O, but got Unknown
		//IL_0385: Expected I4, but got O
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Expected O, but got Unknown
		//IL_07d0: Expected I, but got O
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Expected O, but got Unknown
		//IL_03b7: Expected I4, but got O
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Expected O, but got Unknown
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Expected O, but got Unknown
		//IL_03e9: Expected I4, but got O
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Expected O, but got Unknown
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Expected O, but got Unknown
		//IL_041b: Expected I4, but got O
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Expected O, but got Unknown
		//IL_028d: Expected O, but got I
		//IL_043f: Expected I4, but got O
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Expected O, but got Unknown
		//IL_0463: Expected I4, but got O
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Expected O, but got Unknown
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Expected O, but got Unknown
		//IL_0495: Expected I4, but got O
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Expected O, but got Unknown
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Expected O, but got Unknown
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Expected O, but got Unknown
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Expected O, but got Unknown
		//IL_04e3: Expected I4, but got O
		object obj2 = default(object);
		object obj = obj2 - 63;
		float num;
		object obj3;
		if (hasSector)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+7F]");
			num = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+77]");
			obj3 = 0;
		}
		else
		{
			obj3 = 0;
			num = (float)Math.PI * 2f;
		}
		float num2 = num - (float)obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj4 = num2 & 0;
		float num3 = (float)obj4 / ((float)Math.PI * 2f);
		float num4 = num3 * (float)segmentsPerFullTurn;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		object obj5 = default(object);
		bool flag = (nint)obj5 >= 1;
		object obj6 = obj5;
		if (!flag)
		{
			obj6 = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+67]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+67]");
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+6F]");
		object obj8;
		if (num5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+67]");
			obj8 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+6F]");
			obj8 = 0;
		}
		float num6 = num - (float)obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm1,r15d\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj9 = num6 & 0;
		object obj10 = obj8 + obj8;
		float num7 = (float)obj9 * 0.5f;
		float num8 = num7 / 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		float num9 = num8 * (float)obj8;
		float num10 = (float)obj10 - num9;
		if (hasInnerRadius)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+6F]");
			if (0 <= (nint)obj7)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+6F]");
				obj7 = 0;
			}
		}
		else
		{
			obj7 = 0;
		}
		object obj11 = obj6 * 2;
		object obj12 = obj6 + obj11;
		object obj13 = obj12 << 2;
		int[] array = new int[obj13];
		object obj14 = obj6 * 2;
		object obj15 = obj14 + 2;
		Vector3[] array2 = new Vector3[obj15];
		Vector3[] array3 = new Vector3[obj15];
		object obj16 = obj6 + 1;
		bool flag2 = (nint)obj16 <= 0;
		Vector3[] vertices = array2;
		if (!flag2)
		{
			object obj17 = array2 + 44;
			float num11 = num - (float)obj3;
			object obj18 = (object)array3 - (object)array2;
			object obj19 = (object)array3 - (object)array2;
			float num12 = 0f;
			object obj20 = 0;
			nint num13 = (nint)typeof(Vector3[]);
			object obj21 = default(object);
			bool flag3;
			do
			{
				float num14 = (float)obj20 / (float)obj6;
				if (!(0f > num14))
				{
					if (num14 > 1f)
					{
						num14 = 1f;
					}
				}
				else
				{
					num14 = 0f;
				}
				float num15 = num11 * num14;
				float num16 = num15 + (float)obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
				num12 = num16 * num10;
				_ = 0;
				float num17 = num16 * (float)obj7;
				obj17 = obj21;
				_ = 0;
				nint num18 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1012 @ rax_v53 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num19 = 0;
				_ = Vector3.forwardVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v675 @ rax_v54 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
				_ = 0;
				nint num20 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1032 @ rax_v57 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num21 = 0;
				obj20++;
				_ = Vector3.forwardVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ rcx_v32 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
				_ = 0;
				obj17 += 24;
				object obj22 = obj20;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+57]");
				flag3 = (nint)obj22 < 0;
				num13 = num21;
			}
			while (flag3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+5F]");
			obj6 = 0;
			vertices = array2;
		}
		int[] array4 = default(int[]);
		if (obj6 != null)
		{
			object obj23 = 0;
			object obj24 = 3;
			object obj25 = 0;
			while (true)
			{
				object obj26 = obj23 + 1;
				object obj27 = obj24 - 3;
				array4[obj23] = (int)obj27;
				object obj28 = obj26 + 1;
				array4[obj26] = (int)obj24;
				object obj29 = obj28 + 1;
				object obj30 = obj24 - 1;
				array4[obj28] = (int)obj30;
				object obj31 = obj29 + 1;
				object obj32 = obj24 - 1;
				array4[obj29] = (int)obj32;
				object obj33 = obj31 + 1;
				array4[obj31] = (int)obj24;
				object obj34 = obj33 + 1;
				object obj35 = obj24 - 3;
				array4[obj33] = (int)obj35;
				object obj36 = obj34 + 1;
				object obj37 = obj24 - 3;
				array4[obj34] = (int)obj37;
				object obj38 = obj36 + 1;
				object obj39 = obj24 - 2;
				array4[obj36] = (int)obj39;
				object obj40 = obj38 + 1;
				array4[obj38] = (int)obj24;
				object obj41 = obj40 + 1;
				array4[obj40] = (int)obj24;
				object obj42 = obj41 + 1;
				object obj43 = obj24 - 2;
				array4[obj41] = (int)obj43;
				object obj44 = obj42 + 1;
				object obj45 = obj24 - 3;
				obj25++;
				obj24 += 2;
				array4[obj42] = (int)obj45;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj25) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
				{
					break;
				}
				obj23 = obj44;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+47]");
		((Mesh)0).vertices = vertices;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+47]");
		((Mesh)0).normals = array3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+47]");
		((Mesh)0).triangles = array4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+47]");
		((Mesh)0).RecalculateBounds();
	}

	static ShapesMeshGen()
	{
		ExpandoList<Color> expandoList = new ExpandoList<Color>();
		meshColors = expandoList;
		ExpandoList<Vector3> expandoList2 = new ExpandoList<Vector3>();
		meshVertices = expandoList2;
		ExpandoList<Vector4> expandoList3 = new ExpandoList<Vector4>();
		meshUv0 = expandoList3;
		ExpandoList<Vector3> expandoList4 = new ExpandoList<Vector3>();
		meshUv1Prevs = expandoList4;
		ExpandoList<Vector3> expandoList5 = new ExpandoList<Vector3>();
		meshUv2Nexts = expandoList5;
		ExpandoList<int> expandoList6 = new ExpandoList<int>();
		meshTriangles = expandoList6;
		ExpandoList<int> expandoList7 = new ExpandoList<int>();
		meshJoinsTriangles = expandoList7;
	}

	internal unsafe static void _003CGenPolylineMesh_003Eg__SetPrevNext_007C8_0(int atIndex, ref _003C_003Ec__DisplayClass8_1 P_1)
	{
		//IL_0017: Expected O, but got Ref
		//IL_002e: Expected O, but got Ref
		object obj = default(object);
		meshUv1Prevs.set_Item(atIndex, (Vector3)(&obj));
		object obj2 = default(object);
		meshUv2Nexts.set_Item(atIndex, (Vector3)(&obj2));
	}

	internal unsafe static void _003CGenPolylineMesh_003Eg__SetUv0_007C8_1(ExpandoList<Vector4> uvArr, float uvEndpointVal, float pathThicc, int id, float x, float y)
	{
		//IL_0016: Expected O, but got Ref
		object obj = default(object);
		uvArr.set_Item(id, (Vector4)(&obj));
	}

	internal unsafe static void _003CGenPolylineMesh_003Eg__AddQuad_007C8_2(int a, int b, int c, int d, ref _003C_003Ec__DisplayClass8_0 P_4)
	{
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_0017: Expected I4, but got O
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_0044: Expected I4, but got O
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_0071: Expected I4, but got O
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_009e: Expected I4, but got O
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00cb: Expected I4, but got O
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00f8: Expected I4, but got O
		object obj2 = default(object);
		object obj = obj2 + 1;
		obj2 = obj;
		IntPtr intPtr = default(IntPtr);
		meshTriangles.set_Item((int)obj2, (int)(&intPtr));
		object obj3 = obj2 + 1;
		obj2 = obj3;
		int num = default(int);
		meshTriangles.set_Item((int)obj2, (int)(&num));
		object obj4 = obj2 + 1;
		obj2 = obj4;
		int num2 = default(int);
		meshTriangles.set_Item((int)obj2, (int)(&num2));
		object obj5 = obj2 + 1;
		obj2 = obj5;
		int num3 = default(int);
		meshTriangles.set_Item((int)obj2, (int)(&num3));
		object obj6 = obj2 + 1;
		obj2 = obj6;
		int num4 = default(int);
		meshTriangles.set_Item((int)obj2, (int)(&num4));
		object obj7 = obj2 + 1;
		obj2 = obj7;
		int num5 = default(int);
		meshTriangles.set_Item((int)obj2, (int)(&num5));
	}

	internal static void _003CGenerateDiscMesh_003Eg__DblTri_007C17_0(int a, int b, int c, ref _003C_003Ec__DisplayClass17_0 P_3)
	{
		//IL_0023: Expected O, but got I
		//IL_0055: Expected O, but got I
		//IL_0087: Expected O, but got I
		//IL_00b9: Expected O, but got I
		//IL_00eb: Expected O, but got I
		//IL_011d: Expected O, but got I
		object obj = P_3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass17_0&)+8]");
		object obj2 = (nint)0 + (nint)1;
		object obj3 = P_3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass17_0&)+8]");
		object obj4 = (nint)0 + (nint)1;
		object obj5 = P_3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass17_0&)+8]");
		object obj6 = (nint)0 + (nint)1;
		object obj7 = P_3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass17_0&)+8]");
		object obj8 = (nint)0 + (nint)1;
		object obj9 = P_3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass17_0&)+8]");
		object obj10 = (nint)0 + (nint)1;
		object obj11 = P_3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass17_0&)+8]");
		object obj12 = (nint)0 + (nint)1;
	}
}
