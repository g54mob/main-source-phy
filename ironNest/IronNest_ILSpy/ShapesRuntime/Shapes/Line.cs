using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Line : ShapeRenderer, IDashable
{
	public enum LineColorMode
	{
		Single,
		Double
	}

	private LineGeometry geometry;

	private LineColorMode colorMode;

	private Color colorEnd;

	private Vector3 start;

	private Vector3 end;

	private float thickness;

	private ThicknessSpace thicknessSpace;

	private LineEndCap endCaps;

	private bool matchDashSpacingToSize;

	private bool dashed;

	private DashStyle dashStyle;

	// C# has no syntax for parameterized property 'Item'.
	public unsafe Vector3 get_Item(int i)
	{
		//IL_0058: Expected F4, but got O
		//IL_0053: Expected native int or pointer, but got O
		//IL_006d: Expected F4, but got I
		//IL_0068: Expected native int or pointer, but got O
		//IL_002f: Expected F4, but got O
		//IL_002a: Expected native int or pointer, but got O
		//IL_0044: Expected F4, but got I
		//IL_003f: Expected native int or pointer, but got O
		Vector3 vector = default(Vector3);
		if (i > 0)
		{
			((Vector3*)(nint)vector)->x = (float)end;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Line)+CC]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		((Vector3*)(nint)vector)->x = (float)start;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Line)+C0]");
		((Vector3*)(nint)vector)->z = 0f;
		return vector;
	}

	public unsafe void set_Item(int i, Vector3 value)
	{
		//IL_0082: Expected O, but got F4
		//IL_003d: Expected O, but got Ref
		//IL_005b: Expected O, but got F4
		int prop;
		if (i > 0)
		{
			prop = ShapesMaterialUtils.propPointEnd;
			end = (Vector3)value.x;
			_ = value.z;
		}
		else
		{
			prop = ShapesMaterialUtils.propPointStart;
			start = (Vector3)value.x;
			_ = value.z;
		}
		float num = default(float);
		SetVector3Now(prop, (Vector3)(&num));
	}

	public LineGeometry Geometry
	{
		get
		{
			return geometry;
		}
		set
		{
			geometry = value;
			SetIntNow(ShapesMaterialUtils.propAlignment, (int)geometry);
			UpdateMesh(force: true);
			UpdateMaterial();
			ApplyProperties();
		}
	}

	public unsafe LineColorMode ColorMode
	{
		get
		{
			return colorMode;
		}
		set
		{
			//IL_0025: Expected O, but got Ref
			colorMode = value;
			if (value == LineColorMode.Double)
			{
			}
			Color color = default(Color);
			SetColor(ShapesMaterialUtils.propColorEnd, (Color)(&color));
			ApplyProperties();
		}
	}

	public unsafe override Color Color
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)base.color;
			return color;
		}
		set
		{
			//IL_001a: Expected O, but got F4
			//IL_0029: Expected O, but got Ref
			//IL_0038: Expected O, but got F4
			//IL_0047: Expected O, but got Ref
			color = (Color)value.r;
			float num = default(float);
			SetColor(ShapesMaterialUtils.propColor, (Color)(&num));
			colorEnd = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorEnd, (Color)(&num));
			ApplyProperties();
		}
	}

	public unsafe Color ColorStart
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)base.color;
			return color;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0028: Expected O, but got Ref
			color = (Color)value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColor, (Color)(&obj));
			ApplyProperties();
		}
	}

	public unsafe Color ColorEnd
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorEnd;
			return color;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0028: Expected O, but got Ref
			colorEnd = (Color)value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColorEnd, (Color)(&obj));
			ApplyProperties();
		}
	}

	public unsafe Vector3 Start
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)start;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Line)+C0]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0032: Expected O, but got Ref
			start = (Vector3)value.x;
			_ = value.z;
			object obj = default(object);
			SetVector3Now(ShapesMaterialUtils.propPointStart, (Vector3)(&obj));
		}
	}

	public unsafe Vector3 End
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)end;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Line)+CC]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0032: Expected O, but got Ref
			end = (Vector3)value.x;
			_ = value.z;
			object obj = default(object);
			SetVector3Now(ShapesMaterialUtils.propPointEnd, (Vector3)(&obj));
		}
	}

	public float Thickness
	{
		get
		{
			return thickness;
		}
		set
		{
			thickness = value;
			SetFloatNow(ShapesMaterialUtils.propThickness, value);
			if (dashed)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Line)+E4]");
				if ((nint)0 == -1)
				{
					SetAllDashValues(now: true);
				}
			}
		}
	}

	public ThicknessSpace ThicknessSpace
	{
		get
		{
			return thicknessSpace;
		}
		set
		{
			thicknessSpace = value;
			SetIntNow(ShapesMaterialUtils.propThicknessSpace, (int)value);
		}
	}

	public LineEndCap EndCaps
	{
		get
		{
			return endCaps;
		}
		set
		{
			endCaps = value;
			UpdateMaterial();
		}
	}

	internal override bool HasDetailLevels => true;

	public unsafe bool MatchDashSpacingToSize
	{
		get
		{
			return matchDashSpacingToSize;
		}
		set
		{
			//IL_002b: Expected O, but got Ref
			matchDashSpacingToSize = value;
			DashStyle dashStyle = default(DashStyle);
			float num = default(float);
			bool setType = default(bool);
			bool now = default(bool);
			SetAllDashValues((DashStyle)(&dashStyle), dashed, value, num, setType, now);
		}
	}

	public unsafe bool Dashed
	{
		get
		{
			return dashed;
		}
		set
		{
			//IL_002b: Expected O, but got Ref
			dashed = value;
			DashStyle dashStyle = default(DashStyle);
			float num = default(float);
			bool setType = default(bool);
			bool now = default(bool);
			SetAllDashValues((DashStyle)(&dashStyle), value, matchDashSpacingToSize, num, setType, now);
		}
	}

	public unsafe float DashSize
	{
		get
		{
			//IL_000d: Expected F4, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Line)+EC]");
			return 0f;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_0076: Expected O, but got Ref
			DashStyle dashStyle = (DashStyle)(this + 224);
			float netAbsoluteSize = ((DashStyle*)dashStyle)->GetNetAbsoluteSize(dashed, thickness);
			if (matchDashSpacingToSize)
			{
				object obj = default(object);
				float num = default(float);
				float netDashSpacing = GetNetDashSpacing((DashStyle)(&obj), dashed, matchDashSpacingToSize, num);
				SetFloat(ShapesMaterialUtils.propDashSpacing, netDashSpacing);
			}
			SetFloatNow(ShapesMaterialUtils.propDashSize, netAbsoluteSize);
		}
	}

	public unsafe float DashSpacing
	{
		get
		{
			//IL_0039: Expected F4, but got I
			//IL_002c: Expected F4, but got I
			if (matchDashSpacingToSize)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Line)+EC]");
				return 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Line)+F0]");
			return 0f;
		}
		set
		{
			//IL_0029: Expected O, but got Ref
			object obj = default(object);
			float num = default(float);
			float netDashSpacing = GetNetDashSpacing((DashStyle)(&obj), dashed, matchDashSpacingToSize, num);
			SetFloatNow(ShapesMaterialUtils.propDashSpacing, netDashSpacing);
		}
	}

	public float DashOffset
	{
		get
		{
			//IL_000d: Expected F4, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Line)+F4]");
			return 0f;
		}
		set
		{
			SetFloatNow(ShapesMaterialUtils.propDashOffset, value);
		}
	}

	public unsafe DashSpace DashSpace
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Line)+E4]");
			return DashSpace.Meters;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			SetInt(ShapesMaterialUtils.propDashSpace, (int)value);
			DashStyle dashStyle = (DashStyle)(this + 224);
			float netAbsoluteSize = ((DashStyle*)dashStyle)->GetNetAbsoluteSize(dashed, thickness);
			SetFloatNow(ShapesMaterialUtils.propDashSize, netAbsoluteSize);
		}
	}

	public DashSnapping DashSnap
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Line)+E8]");
			return DashSnapping.Off;
		}
		set
		{
			SetIntNow(ShapesMaterialUtils.propDashSnap, (int)value);
		}
	}

	public DashType DashType
	{
		get
		{
			//IL_0007: Expected I4, but got O
			return (DashType)dashStyle;
		}
		set
		{
			//IL_0014: Expected O, but got I4
			dashStyle = (DashStyle)value;
			SetIntNow(ShapesMaterialUtils.propDashType, (int)value);
		}
	}

	public float DashShapeModifier
	{
		get
		{
			//IL_000d: Expected F4, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Line)+F8]");
			return 0f;
		}
		set
		{
			SetFloatNow(ShapesMaterialUtils.propDashShapeModifier, value);
		}
	}

	private protected unsafe override void SetAllMaterialProperties()
	{
		//IL_00a1: Expected O, but got Ref
		//IL_00b1: Expected O, but got Ref
		//IL_006e: Expected O, but got Ref
		//IL_0090: Expected O, but got Ref
		Vector3 vector = default(Vector3);
		SetVector3(ShapesMaterialUtils.propPointStart, (Vector3)(&vector));
		SetVector3(ShapesMaterialUtils.propPointEnd, (Vector3)(&vector));
		SetFloat(ShapesMaterialUtils.propThickness, thickness);
		SetInt(ShapesMaterialUtils.propThicknessSpace, (int)thicknessSpace);
		SetInt(ShapesMaterialUtils.propAlignment, (int)geometry);
		if (colorMode == LineColorMode.Double)
		{
		}
		SetColor(ShapesMaterialUtils.propColorEnd, (Color)(&vector));
		Color color = default(Color);
		float num = default(float);
		bool setType = default(bool);
		bool now = default(bool);
		SetAllDashValues((DashStyle)(&color), dashed, matchDashSpacingToSize, num, setType, now);
	}

	private protected unsafe override Bounds GetUnpaddedLocalBounds_Internal()
	{
		//IL_003a: Expected F4, but got I4
		//IL_0077: Expected O, but got F4
		//IL_0072: Expected native int or pointer, but got O
		//IL_00b6: Expected O, but got I
		//IL_00f7: Expected O, but got F4
		//IL_00f2: Expected native int or pointer, but got O
		float num = ((thicknessSpace != ThicknessSpace.Meters) ? 0f : thickness);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181061150");
		object obj = end + start;
		float num2 = (float)obj * 0.5f;
		Bounds bounds = default(Bounds);
		((Bounds*)(nint)bounds)->m_Center = (Vector3)num2;
		object obj3 = default(object);
		object obj2 = obj3 + obj3;
		float num3 = (float)obj2 * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Line)+CC]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Line)+C0]");
		object obj4 = num4 + 0;
		object obj5 = default(object);
		float num5 = (float)obj5 + num;
		float num6 = (float)obj4 * 0.5f;
		float num7 = num5 * 0.5f;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num7;
		float num8 = (float)obj3 + num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rax_v1+8]");
		float num9 = 0f + num;
		float num10 = num8 * 0.5f;
		float num11 = num9 * 0.5f;
		return bounds;
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_003c: Expected O, but got I4
		//IL_0071: Expected I4, but got O
		//IL_0089: Expected O, but got I4
		//IL_0091: Expected I4, but got O
		//IL_011e: Expected O, but got I4
		//IL_00c4: Expected I, but got O
		//IL_00f6: Expected O, but got I4
		ShapesMaterials lineMat = ShapesMaterialUtils.GetLineMat(geometry, endCaps);
		bool flag = lineMat == null;
		LineEndCap lineEndCap = endCaps;
		object obj = 0;
		LineGeometry lineGeometry = geometry;
		if (!flag)
		{
			lineEndCap = (LineEndCap)base.blendMode;
			LineGeometry lineGeometry2 = (LineGeometry)lineMat.get_Item(base.blendMode);
			bool flag2 = mats == null;
			obj = 0;
			lineGeometry = (LineGeometry)lineMat;
			if (!flag2)
			{
				if (lineGeometry2 != LineGeometry.Flat2D)
				{
					nint num = (nint)mats;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rdx_v9 (Il2CppClass<UnityEngine.Material[]>)+40]");
					lineEndCap = LineEndCap.None;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj2 = default(object);
					bool flag3 = obj2 == null;
					obj = 0;
					lineGeometry = lineGeometry2;
					if (flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj3 = default(object);
						throw obj3;
					}
				}
				mats[0] = (Material)lineGeometry2;
				return;
			}
		}
		throw new NullReferenceException();
	}

	private protected override Mesh GetInitialMeshAsset()
	{
		return ShapesMeshUtils.GetLineMesh(geometry, endCaps, detailLevel);
	}

	private protected unsafe override void ShapeClampRanges()
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_001d: Expected F4, but got I4
		//IL_00a6: Expected O, but got I4
		//IL_0168: Expected F4, but got I4
		//IL_0043: Expected O, but got I4
		//IL_00bf: Expected O, but got I4
		//IL_011b: Expected F4, but got I
		//IL_00d4: Expected F4, but got I
		//IL_0098: Expected F4, but got I4
		//IL_005c: Expected O, but got I4
		//IL_0190: Expected O, but got Ref
		bool flag = !(0f < thickness);
		float num = 0f;
		if (!flag)
		{
			num = thickness;
		}
		thickness = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Line)+E4]");
		if ((nint)0 == -2)
		{
			object obj = 236;
			if (!matchDashSpacingToSize)
			{
				obj = 240;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v14+this @ rcx (Shapes.Line)]");
			float num2 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v14+this @ rcx (Shapes.Line)]");
			if ((nint)0 <= (nint)0)
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
		}
		else
		{
			object obj2 = 236;
			if (!matchDashSpacingToSize)
			{
				obj2 = 240;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v12+this @ rcx (Shapes.Line)]");
			bool flag2 = (nint)0 >= (nint)0;
			float num2 = 0f;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v12+this @ rcx (Shapes.Line)]");
				num2 = 0f;
			}
		}
		object obj3 = default(object);
		float num3 = default(float);
		float netDashSpacing = GetNetDashSpacing((DashStyle)(&obj3), dashed, matchDashSpacingToSize, num3);
		SetFloatNow(ShapesMaterialUtils.propDashSpacing, netDashSpacing);
	}

	private unsafe void SetAllDashValues(bool now)
	{
		//IL_0022: Expected O, but got Ref
		object obj = default(object);
		float num = default(float);
		bool setType = default(bool);
		bool now2 = default(bool);
		SetAllDashValues((DashStyle)(&obj), dashed, matchDashSpacingToSize, num, setType, now2);
	}

	private unsafe float GetNetDashSpacing()
	{
		//IL_001a: Expected O, but got Ref
		object obj = default(object);
		float num = default(float);
		return GetNetDashSpacing((DashStyle)(&obj), dashed, matchDashSpacingToSize, num);
	}

	public Line()
	{
		//IL_0017: Expected O, but got I
		//IL_009f: Expected I, but got O
		//IL_0035: Expected I, but got O
		//IL_008c: Expected I, but got O
		//IL_0129: Expected O, but got I
		//IL_0143: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		colorEnd = (Color)0;
		geometry = LineGeometry.Billboard;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		start = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v6 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		end = Vector3.rightVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rcx_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
		_ = 0;
		thickness = 0.125f;
		endCaps = LineEndCap.Round;
		matchDashSpacingToSize = true;
		nint num5 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rax_v9 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num6 = 0;
		dashStyle = DashStyle.defaultDashStyleLine;
		meshOutOfDate = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rax_v10 (Il2CppStaticFields<Shapes.DashStyle>)+48]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rax_v10 (Il2CppStaticFields<Shapes.DashStyle>)+50]");
		_ = 0;
		base.blendMode = ShapesBlendMode.Transparent;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		detailLevel = DetailLevel.Medium;
		base.renderQueue = -1;
		base.zTest = CompareFunction.LessEqual;
		base.colorMask = ColorWriteMask.All;
		base.stencilComp = CompareFunction.Always;
		base.stencilReadMask = 255;
		base.shouldUpdateMaterialPropertiesInEditor = true;
		((MonoBehaviour)this)._002Ector();
	}
}
