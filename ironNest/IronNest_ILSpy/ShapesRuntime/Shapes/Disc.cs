using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Disc : ShapeRenderer, IDashable
{
	public enum DiscColorMode
	{
		Single,
		Radial,
		Angular,
		Bilinear
	}

	private DiscType type;

	private DiscColorMode colorMode;

	private Color colorOuterStart;

	private Color colorInnerEnd;

	private Color colorOuterEnd;

	private DiscGeometry geometry;

	private AngularUnit angUnitInput;

	private float angRadiansStart;

	private float angRadiansEnd;

	private float radius;

	private ThicknessSpace radiusSpace;

	private float thickness;

	private ThicknessSpace thicknessSpace;

	private ArcEndCap arcEndCaps;

	private bool matchDashSpacingToSize;

	private bool dashed;

	private DashStyle dashStyle;

	public bool HasThickness
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E980D0");
			bool result = default(bool);
			return result;
		}
	}

	public bool HasSector
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181056A80");
			bool result = default(bool);
			return result;
		}
	}

	public DiscType Type
	{
		get
		{
			return type;
		}
		set
		{
			type = value;
			UpdateMaterial();
			ApplyProperties();
		}
	}

	public DiscColorMode ColorMode
	{
		get
		{
			return colorMode;
		}
		set
		{
			colorMode = value;
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
			//IL_0043: Expected O, but got F4
			//IL_0052: Expected O, but got Ref
			//IL_0061: Expected O, but got F4
			//IL_0070: Expected O, but got Ref
			//IL_007f: Expected O, but got F4
			//IL_008e: Expected O, but got Ref
			//IL_0014: Expected O, but got F4
			//IL_0023: Expected O, but got Ref
			color = (Color)value.r;
			float num = default(float);
			SetColor(ShapesMaterialUtils.propColor, (Color)(&num));
			colorOuterStart = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorOuterStart, (Color)(&num));
			colorInnerEnd = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorInnerEnd, (Color)(&num));
			colorOuterEnd = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorOuterEnd, (Color)(&num));
			ApplyProperties();
		}
	}

	public unsafe Color ColorInnerStart
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

	public unsafe Color ColorOuterStart
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorOuterStart;
			return color;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0028: Expected O, but got Ref
			colorOuterStart = (Color)value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColorOuterStart, (Color)(&obj));
			ApplyProperties();
		}
	}

	public unsafe Color ColorInnerEnd
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorInnerEnd;
			return color;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0028: Expected O, but got Ref
			colorInnerEnd = (Color)value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColorInnerEnd, (Color)(&obj));
			ApplyProperties();
		}
	}

	public unsafe Color ColorOuterEnd
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorOuterEnd;
			return color;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0028: Expected O, but got Ref
			colorOuterEnd = (Color)value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColorOuterEnd, (Color)(&obj));
			ApplyProperties();
		}
	}

	public unsafe Color ColorOuter
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorOuterStart;
			return color;
		}
		set
		{
			//IL_001a: Expected O, but got F4
			//IL_0029: Expected O, but got Ref
			//IL_0038: Expected O, but got F4
			//IL_0047: Expected O, but got Ref
			colorOuterStart = (Color)value.r;
			float num = default(float);
			SetColor(ShapesMaterialUtils.propColorOuterStart, (Color)(&num));
			colorOuterEnd = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorOuterEnd, (Color)(&num));
			ApplyProperties();
		}
	}

	public unsafe Color ColorInner
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
			colorInnerEnd = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorInnerEnd, (Color)(&num));
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
			//IL_001a: Expected O, but got F4
			//IL_0029: Expected O, but got Ref
			//IL_0038: Expected O, but got F4
			//IL_0047: Expected O, but got Ref
			color = (Color)value.r;
			float num = default(float);
			SetColor(ShapesMaterialUtils.propColor, (Color)(&num));
			colorOuterStart = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorOuterStart, (Color)(&num));
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
			((Color*)(nint)color)->r = (float)colorInnerEnd;
			return color;
		}
		set
		{
			//IL_001a: Expected O, but got F4
			//IL_0029: Expected O, but got Ref
			//IL_0038: Expected O, but got F4
			//IL_0047: Expected O, but got Ref
			colorInnerEnd = (Color)value.r;
			float num = default(float);
			SetColor(ShapesMaterialUtils.propColorInnerEnd, (Color)(&num));
			colorOuterEnd = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorOuterEnd, (Color)(&num));
			ApplyProperties();
		}
	}

	public DiscGeometry Geometry
	{
		get
		{
			return geometry;
		}
		set
		{
			geometry = value;
			SetIntNow(ShapesMaterialUtils.propAlignment, (int)value);
		}
	}

	public float AngRadiansStart
	{
		get
		{
			return angRadiansStart;
		}
		set
		{
			angRadiansStart = value;
			SetFloatNow(ShapesMaterialUtils.propAngStart, value);
		}
	}

	public float AngRadiansEnd
	{
		get
		{
			return angRadiansEnd;
		}
		set
		{
			angRadiansEnd = value;
			SetFloatNow(ShapesMaterialUtils.propAngEnd, value);
		}
	}

	public float Radius
	{
		get
		{
			return radius;
		}
		set
		{
			//IL_003a: Invalid comparison between I4 and F4
			//IL_004c: Expected F4, but got I4
			bool flag = !(0f < value);
			float value2 = 0f;
			if (!flag)
			{
				value2 = value;
			}
			radius = value2;
			SetFloatNow(ShapesMaterialUtils.propRadius, value2);
		}
	}

	public ThicknessSpace RadiusSpace
	{
		get
		{
			return radiusSpace;
		}
		set
		{
			radiusSpace = value;
			SetIntNow(ShapesMaterialUtils.propRadiusSpace, (int)value);
		}
	}

	public float RadiusInner
	{
		get
		{
			return thickness;
		}
		set
		{
			//IL_00b8: Invalid comparison between I4 and F4
			//IL_00ca: Expected F4, but got I4
			bool flag = !(0f < value);
			float value2 = 0f;
			if (!flag)
			{
				value2 = value;
			}
			thickness = value2;
			SetFloatNow(ShapesMaterialUtils.propThickness, value2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E980D0");
			object obj = default(object);
			if (obj != null && dashed)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+104]");
				if ((nint)0 == -1)
				{
					SetAllDashValues(now: true);
				}
			}
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
			//IL_00b8: Invalid comparison between I4 and F4
			//IL_00ca: Expected F4, but got I4
			bool flag = !(0f < value);
			float value2 = 0f;
			if (!flag)
			{
				value2 = value;
			}
			thickness = value2;
			SetFloatNow(ShapesMaterialUtils.propThickness, value2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E980D0");
			object obj = default(object);
			if (obj != null && dashed)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+104]");
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

	public ArcEndCap ArcEndCaps
	{
		get
		{
			return arcEndCaps;
		}
		set
		{
			arcEndCaps = value;
			SetIntNow(ShapesMaterialUtils.propRoundCaps, (int)value);
		}
	}

	internal override bool HasDetailLevels => false;

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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+10C]");
			return 0f;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_0076: Expected O, but got Ref
			DashStyle dashStyle = (DashStyle)(this + 256);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+10C]");
				return 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+110]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+114]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+104]");
			return DashSpace.Meters;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			SetInt(ShapesMaterialUtils.propDashSpace, (int)value);
			DashStyle dashStyle = (DashStyle)(this + 256);
			float netAbsoluteSize = ((DashStyle*)dashStyle)->GetNetAbsoluteSize(dashed, thickness);
			SetFloatNow(ShapesMaterialUtils.propDashSize, netAbsoluteSize);
		}
	}

	public DashSnapping DashSnap
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+108]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+118]");
			return 0f;
		}
		set
		{
			SetFloatNow(ShapesMaterialUtils.propDashShapeModifier, value);
		}
	}

	private protected override void SetAllMaterialProperties()
	{
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Expected O, but got Unknown
		//IL_0093: Expected O, but got I4
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		SetInt(ShapesMaterialUtils.propAlignment, (int)geometry);
		SetFloat(ShapesMaterialUtils.propRadius, radius);
		SetInt(ShapesMaterialUtils.propRadiusSpace, (int)radiusSpace);
		SetFloat(ShapesMaterialUtils.propThickness, thickness);
		SetInt(ShapesMaterialUtils.propThicknessSpace, (int)thicknessSpace);
		SetInt(ShapesMaterialUtils.propRoundCaps, (int)arcEndCaps);
		SetFloat(ShapesMaterialUtils.propAngStart, angRadiansStart);
		SetFloat(ShapesMaterialUtils.propAngEnd, angRadiansEnd);
		bool flag = colorMode == DiscColorMode.Single;
		object obj3 = default(object);
		if (!flag)
		{
			object obj = colorMode - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						goto IL_00f1;
					}
					Color value = (Color)(obj3 - 32);
					_ = colorOuterStart;
					SetColor(ShapesMaterialUtils.propColorOuterStart, value);
					Color value2 = (Color)(obj3 - 32);
					_ = colorInnerEnd;
					SetColor(ShapesMaterialUtils.propColorInnerEnd, value2);
					Color color = colorOuterEnd;
				}
				else
				{
					Color value3 = (Color)(obj3 - 32);
					_ = base.color;
					SetColor(ShapesMaterialUtils.propColorOuterStart, value3);
					Color value4 = (Color)(obj3 - 32);
					_ = colorInnerEnd;
					SetColor(ShapesMaterialUtils.propColorInnerEnd, value4);
					Color color = colorInnerEnd;
				}
			}
			else
			{
				Color value5 = (Color)(obj3 - 32);
				_ = colorOuterStart;
				SetColor(ShapesMaterialUtils.propColorOuterStart, value5);
				Color value6 = (Color)(obj3 - 32);
				_ = base.color;
				SetColor(ShapesMaterialUtils.propColorInnerEnd, value6);
				Color color = colorOuterStart;
			}
		}
		else
		{
			Color value7 = (Color)(obj3 - 32);
			_ = base.color;
			SetColor(ShapesMaterialUtils.propColorOuterStart, value7);
			Color value8 = (Color)(obj3 - 32);
			_ = base.color;
			SetColor(ShapesMaterialUtils.propColorInnerEnd, value8);
			Color color = base.color;
		}
		Color value9 = (Color)(obj3 - 32);
		SetColor(ShapesMaterialUtils.propColorOuterEnd, value9);
		goto IL_00f1;
		IL_00f1:
		DashStyle style = (DashStyle)(obj3 - 32);
		_ = dashStyle;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+110]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Disc)+118]");
		_ = 0;
		float num = default(float);
		bool setType = default(bool);
		bool now = default(bool);
		SetAllDashValues(style, dashed, matchDashSpacingToSize, num, setType, now);
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_0061: Expected I4, but got O
		//IL_0075: Expected I, but got O
		//IL_007d: Expected I4, but got O
		//IL_0106: Expected O, but got I4
		//IL_00b0: Expected I, but got O
		//IL_00de: Expected I, but got O
		ShapesMaterials discMaterial = ShapesMaterialUtils.GetDiscMaterial(type);
		bool flag = discMaterial == null;
		ShapesBlendMode shapesBlendMode = ShapesBlendMode.Opaque;
		DiscType discType = type;
		if (!flag)
		{
			shapesBlendMode = base.blendMode;
			DiscType discType2 = (DiscType)discMaterial.get_Item(base.blendMode);
			bool flag2 = mats == null;
			nint num = unchecked((nint)null);
			discType = (DiscType)discMaterial;
			if (!flag2)
			{
				if (discType2 != DiscType.Disc)
				{
					nint num2 = (nint)mats;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rdx_v9 (Il2CppClass<UnityEngine.Material[]>)+40]");
					shapesBlendMode = ShapesBlendMode.Opaque;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj = default(object);
					bool flag3 = obj == null;
					num = unchecked((nint)null);
					discType = discType2;
					if (flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj2 = default(object);
						throw obj2;
					}
				}
				mats[0] = (Material)discType2;
				return;
			}
		}
		throw new NullReferenceException();
	}

	private protected unsafe override Bounds GetUnpaddedLocalBounds_Internal()
	{
		//IL_0043: Expected F4, but got I4
		//IL_0082: Expected F4, but got I4
		//IL_0149: Expected I, but got O
		//IL_0176: Expected F4, but got I4
		//IL_00d8: Expected native int or pointer, but got O
		//IL_012c: Expected O, but got F4
		//IL_0127: Expected native int or pointer, but got O
		float num = ((radiusSpace != ThicknessSpace.Meters) ? 0f : (radius + radius));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E980D0");
		object obj = default(object);
		float num2 = ((obj == null || thicknessSpace != ThicknessSpace.Meters) ? 0f : thickness);
		float num3 = num2 + num;
		nint num4 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num5 = 0;
		bool flag = geometry != DiscGeometry.Billboard;
		float num6 = 0f;
		if (!flag)
		{
			num6 = num3;
		}
		Bounds bounds = default(Bounds);
		((Bounds*)(nint)bounds)->m_Center = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		float num7 = num3 * 0.5f;
		float num8 = num6 * 0.5f;
		float num9 = num3 * 0.5f;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num9;
		return bounds;
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

	public Disc()
	{
		//IL_0017: Expected O, but got I
		//IL_0034: Expected O, but got I
		//IL_0051: Expected O, but got I
		//IL_0080: Expected I, but got O
		//IL_00e2: Expected O, but got I
		//IL_00fc: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		colorOuterStart = (Color)0;
		angUnitInput = AngularUnit.Degrees;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		colorInnerEnd = (Color)0;
		angRadiansEnd = (float)Math.PI * 3f / 4f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		colorOuterEnd = (Color)0;
		radius = 1f;
		thickness = 0.5f;
		matchDashSpacingToSize = true;
		nint num = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v3 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num2 = 0;
		dashStyle = DashStyle.defaultDashStyleRing;
		meshOutOfDate = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+2C]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+34]");
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
