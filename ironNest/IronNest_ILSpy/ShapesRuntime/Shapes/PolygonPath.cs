using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class PolygonPath : PointPath<Vector2>
{
	private sealed class _003C_003Ec__DisplayClass10_0
	{
		public PolygonPath _003C_003E4__this;

		public PolygonTriangulation triangulation;

		internal void _003CEnsureMeshIsReadyToRender_003Eb__0()
		{
			//IL_0037: Expected O, but got I
			//IL_0037: Expected O, but got I
			PolygonPath polygonPath = _003C_003E4__this;
			polygonPath.lastUsedTriangulationMode = triangulation;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbx_v1 (Shapes.PolygonPath)+10]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbx_v1 (Shapes.PolygonPath)+28]");
			ShapesMeshGen.GenPolygonMesh((Mesh)num, (List<Vector2>)0, triangulation);
		}
	}

	private PolygonTriangulation lastUsedTriangulationMode = PolygonTriangulation.EarClipping;

	public unsafe void AddPoint(float x, float y)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((Vector2)(&obj));
	}

	public void BezierTo(Vector2 startTangent, Vector2 endTangent, Vector2 end, int pointCount)
	{
		//IL_00c4: Expected I4, but got I8
		if (!CheckCanAddContinuePoint("BezierTo"))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			ShapesMath._003CCubicBezierPointsSkipFirst_003Ed__41 obj = new ShapesMath._003CCubicBezierPointsSkipFirst_003Ed__41(0);
			obj._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			Vector2 vector = default(Vector2);
			obj._003C_003E3__a = vector;
			obj._003C_003E3__b = startTangent;
			obj._003C_003E3__c = endTangent;
			int num = default(int);
			obj._003C_003El__initialThreadId = num;
			obj._003C_003E3__d = end;
			int num2 = default(int);
			obj._003C_003E3__count = num2;
			AddPoints(obj);
		}
	}

	public unsafe void BezierTo(Vector2 startTangent, Vector2 endTangent, Vector2 end, float pointsPerTurn)
	{
		//IL_002a: Expected O, but got Ref
		//IL_002a: Expected O, but got Ref
		//IL_002a: Expected O, but got Ref
		//IL_002a: Expected O, but got Ref
		//IL_015a: Expected I4, but got I8
		ShapesConfig instance = ShapesConfig.Instance;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
		Vector2 vector = default(Vector2);
		object obj = default(object);
		object obj2 = default(object);
		object obj3 = default(object);
		int vertCount = default(int);
		float approximateAngularCurveSumDegrees = ShapesMath.GetApproximateAngularCurveSumDegrees((Vector3)(&vector), (Vector3)(&obj), (Vector3)(&obj2), (Vector3)(&obj3), vertCount);
		float num = approximateAngularCurveSumDegrees / 360f;
		ShapesConfig instance2 = ShapesConfig.Instance;
		float num2 = num * instance2.polylineDefaultPointsPerTurn;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		int num3 = default(int);
		bool flag = num3 >= 2;
		int num4 = num3;
		if (!flag)
		{
			num4 = 2;
		}
		if (!CheckCanAddContinuePoint("BezierTo"))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			ShapesMath._003CCubicBezierPointsSkipFirst_003Ed__41 obj4 = new ShapesMath._003CCubicBezierPointsSkipFirst_003Ed__41(0);
			obj4._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			obj4._003C_003E3__a = vector;
			obj4._003C_003E3__b = startTangent;
			obj4._003C_003E3__c = endTangent;
			obj4._003C_003E3__d = end;
			int num5 = default(int);
			obj4._003C_003El__initialThreadId = num5;
			obj4._003C_003E3__count = num4;
			AddPoints(obj4);
		}
	}

	public void ArcTo(Vector2 corner, Vector2 next, float radius, float pointsPerTurn)
	{
		if (!CheckCanAddContinuePoint("ArcTo"))
		{
			bool useDensity = default(bool);
			int targetPointCount = default(int);
			float pointsPerTurn2 = default(float);
			AddArcPoints(corner, next, radius, useDensity, targetPointCount, pointsPerTurn2);
		}
	}

	public void ArcTo(Vector2 corner, Vector2 next, float radius, int pointCount)
	{
		if (!CheckCanAddContinuePoint("ArcTo"))
		{
			bool useDensity = default(bool);
			int targetPointCount = default(int);
			float pointsPerTurn = default(float);
			AddArcPoints(corner, next, radius, useDensity, targetPointCount, pointsPerTurn);
		}
	}

	public void ArcTo(Vector2 corner, Vector2 next, float radius)
	{
		if (!CheckCanAddContinuePoint("ArcTo"))
		{
			ShapesConfig instance = ShapesConfig.Instance;
			bool useDensity = default(bool);
			int targetPointCount = default(int);
			float pointsPerTurn = default(float);
			AddArcPoints(corner, next, radius, useDensity, targetPointCount, pointsPerTurn);
		}
	}

	public void ArcTo(Vector2 corner, Vector2 next, float radius, float pointsPerTurn, Color color)
	{
		if (!CheckCanAddContinuePoint("ArcTo"))
		{
			bool useDensity = default(bool);
			int targetPointCount = default(int);
			float pointsPerTurn2 = default(float);
			AddArcPoints(corner, next, radius, useDensity, targetPointCount, pointsPerTurn2);
		}
	}

	private unsafe void AddArcPoints(Vector2 corner, Vector2 next, float radius, bool useDensity, int targetPointCount, float pointsPerTurn)
	{
		//IL_02ca: Expected O, but got Ref
		//IL_005e: Invalid comparison between O and F4
		//IL_02bb: Expected O, but got Ref
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Expected O, but got Unknown
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Expected O, but got Unknown
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Expected O, but got Unknown
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Expected O, but got Unknown
		//IL_0452: Expected I4, but got I8
		//IL_0283: Expected O, but got F4
		//IL_0233: Expected F8, but got I4
		//IL_0145: Expected I, but got O
		//IL_01bd: Expected F8, but got I4
		//IL_03aa: Expected F8, but got I4
		//IL_03b8: Expected I, but got O
		object obj2 = default(object);
		object obj3 = default(object);
		object obj6;
		object obj7;
		float num5;
		double num13;
		int num14;
		Vector2 vector4 = default(Vector2);
		if (0.0001f < radius)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			Vector2 value = default(Vector2);
			Vector2 vector = Vector2.Normalize(ref value);
			Vector2 vector2 = Vector2.Normalize(ref value);
			object obj = obj2 * obj3;
			object obj4 = vector2 * vector;
			object obj5 = obj + obj4;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.999f))
			{
				obj6 = vector2 ^ -0f;
				obj7 = vector ^ -0f;
				Vector2 vector3 = Vector2.Normalize(ref value);
				object obj8 = default(object);
				bool flag = obj8 == null;
				object obj9 = (object)vector3 * obj2;
				object obj10 = obj3 * obj6;
				object obj11 = obj9 + obj10;
				float num = radius / (float)obj11;
				float num2 = (float)obj3 * num;
				float num3 = (float)vector3 * num;
				object obj12 = default(object);
				float num4 = num2 + (float)obj12;
				num5 = num3 + (float)corner;
				if (!flag)
				{
					nint num6 = (nint)(&value);
					object obj13 = obj2 * obj2;
					object obj14 = obj6 * obj6;
					object obj15 = obj7 * obj7;
					object obj16 = obj14 + obj13;
					object obj17 = obj3 * obj3;
					double num7 = (double)obj15 + (double)obj17;
					double num8 = (double)obj16 * num7;
					if (!(1.0000000031710769E-30 > num8))
					{
						nint num9 = (nint)typeof(Math);
						object obj18 = obj2 * obj3;
						object obj19 = obj6 * obj7;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm7\"");
						object obj20 = obj19 + obj18;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rcx_v18 (Il2CppClass<System.Math>)+E4]");
						double num10;
						if ((nint)0 <= (nint)0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
							num10 = 0.0;
						}
						else
						{
							num10 = Math.Sqrt(0.0);
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
						double num11 = (double)obj20 / num10;
						if (-1.0 > num11 || !(num11 > 1.0))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
							double num12 = Math.Acos(0.0);
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
							num13 = num12 * 57.295780181884766;
							num7 = 0.0;
							num6 = (nint)typeof(Math);
							goto IL_03bd;
						}
					}
					num13 = 0.0;
					goto IL_03bd;
				}
				int num15 = default(int);
				num14 = num15;
				goto IL_03f3;
			}
			AddPoint((Vector2)(&vector4));
			return;
		}
		AddPoint((Vector2)(&vector4));
		return;
		IL_03bd:
		float num16 = (float)num13 / 360f;
		object obj21 = default(object);
		float num17 = num16 * (float)obj21;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		int num18 = default(int);
		num14 = num18;
		goto IL_03f3;
		IL_03f3:
		Vector2 vector5 = (Vector2)(obj3 ^ -0f);
		object obj22 = obj7 ^ -0f;
		Vector2 vector6 = (Vector2)(obj2 ^ -0f);
		object obj23 = obj6 ^ -0f;
		ShapesMath._003CGetArcPoints_003Ed__37 obj24 = new ShapesMath._003CGetArcPoints_003Ed__37(0);
		obj24._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		int num19 = default(int);
		obj24._003C_003El__initialThreadId = num19;
		obj24._003C_003E3__normA = vector5;
		obj24._003C_003E3__normB = vector6;
		obj24._003C_003E3__center = (Vector2)num5;
		obj24._003C_003E3__radius = radius;
		obj24._003C_003E3__count = num14;
		AddPoints(obj24);
	}

	public bool EnsureMeshIsReadyToRender(PolygonTriangulation triangulation, out Mesh outMesh)
	{
		//IL_0080: Expected I4, but got O
		_003C_003Ec__DisplayClass10_0 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass10_0();
		if (CS_0024_003C_003E8__locals6 != null)
		{
			CS_0024_003C_003E8__locals6._003C_003E4__this = this;
			CS_0024_003C_003E8__locals6.triangulation = triangulation;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PolygonPath)+18]");
			if ((nint)0 == 0 && triangulation != lastUsedTriangulationMode)
			{
				_ = 1;
			}
			Action updateMesh = delegate
			{
				//IL_0037: Expected O, but got I
				//IL_0037: Expected O, but got I
				PolygonPath polygonPath = CS_0024_003C_003E8__locals6._003C_003E4__this;
				polygonPath.lastUsedTriangulationMode = CS_0024_003C_003E8__locals6.triangulation;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbx_v1 (Shapes.PolygonPath)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbx_v1 (Shapes.PolygonPath)+28]");
				ShapesMeshGen.GenPolygonMesh((Mesh)num, (List<Vector2>)0, CS_0024_003C_003E8__locals6.triangulation);
			};
			return EnsureMeshIsReadyToRender(out outMesh, updateMesh);
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void TryUpdateMesh(PolygonTriangulation triangulation)
	{
		//IL_0026: Expected O, but got I
		//IL_0026: Expected O, but got I
		lastUsedTriangulationMode = triangulation;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PolygonPath)+10]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PolygonPath)+28]");
		ShapesMeshGen.GenPolygonMesh((Mesh)num, (List<Vector2>)0, triangulation);
	}
}
