using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Rectangle : ShapeRenderer, IDashable, IFillable
{
	public enum RectangleType
	{
		HardSolid,
		RoundedSolid,
		HardBorder,
		RoundedBorder
	}

	public enum RectangleCornerRadiusMode
	{
		Uniform,
		PerCorner
	}

	private RectPivot pivot;

	private float width;

	private float height;

	private RectangleType type;

	private RectangleCornerRadiusMode cornerRadiusMode;

	private Vector4 cornerRadii;

	private float thickness;

	private ThicknessSpace thicknessSpace;

	private bool matchDashSpacingToSize;

	private bool dashed;

	private DashStyle dashStyle;

	private protected GradientFill fill;

	private protected bool useFill;

	public bool IsBorder
	{
		get
		{
			//IL_0038: Expected O, but got I4
			if (type == RectangleType.HardBorder)
			{
				return true;
			}
			object obj = type - 3;
			return obj == null;
		}
	}

	public bool IsHollow
	{
		get
		{
			//IL_0038: Expected O, but got I4
			if (type == RectangleType.HardBorder)
			{
				return true;
			}
			object obj = type - 3;
			return obj == null;
		}
	}

	public bool IsRounded
	{
		get
		{
			//IL_0038: Expected O, but got I4
			if (type == RectangleType.RoundedSolid)
			{
				return true;
			}
			object obj = type - 3;
			return obj == null;
		}
	}

	public RectPivot Pivot
	{
		get
		{
			return pivot;
		}
		set
		{
			pivot = value;
			UpdateRectPositioningNow();
		}
	}

	public float Width
	{
		get
		{
			return width;
		}
		set
		{
			width = value;
			UpdateRectPositioningNow();
		}
	}

	public float Height
	{
		get
		{
			return height;
		}
		set
		{
			height = value;
			UpdateRectPositioningNow();
		}
	}

	public RectangleType Type
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

	public RectangleCornerRadiusMode CornerRadiusMode
	{
		get
		{
			return cornerRadiusMode;
		}
		set
		{
			cornerRadiusMode = value;
		}
	}

	public unsafe float Radius
	{
		get
		{
			//IL_0007: Expected F4, but got O
			return (float)cornerRadii;
		}
		set
		{
			//IL_0067: Invalid comparison between I4 and F4
			//IL_0052: Expected O, but got Ref
			if (!(0f > value))
			{
			}
			Vector4 vector = default(Vector4);
			cornerRadii = vector;
			MaterialPropertyBlock materialPropertyBlock = base.mpb;
			Vector4 vector2;
			if (base.mpb != null)
			{
				vector2 = vector;
			}
			else
			{
				materialPropertyBlock = (base.mpb = new MaterialPropertyBlock());
				vector2 = vector;
			}
			materialPropertyBlock.SetVector(ShapesMaterialUtils.propCornerRadii, (Vector4)(&vector2));
			ApplyProperties();
		}
	}

	public unsafe float CornerRadius
	{
		get
		{
			//IL_0007: Expected F4, but got O
			return (float)cornerRadii;
		}
		set
		{
			//IL_0067: Invalid comparison between I4 and F4
			//IL_0052: Expected O, but got Ref
			if (!(0f > value))
			{
			}
			Vector4 vector = default(Vector4);
			cornerRadii = vector;
			MaterialPropertyBlock materialPropertyBlock = base.mpb;
			Vector4 vector2;
			if (base.mpb != null)
			{
				vector2 = vector;
			}
			else
			{
				materialPropertyBlock = (base.mpb = new MaterialPropertyBlock());
				vector2 = vector;
			}
			materialPropertyBlock.SetVector(ShapesMaterialUtils.propCornerRadii, (Vector4)(&vector2));
			ApplyProperties();
		}
	}

	public unsafe Vector4 CornerRadii
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Vector4 vector = default(Vector4);
			((Vector4*)(nint)vector)->x = (float)cornerRadii;
			return vector;
		}
		set
		{
			//IL_00f2: Invalid comparison between I4 and F4
			//IL_007f: Invalid comparison between I4 and F4
			//IL_0117: Invalid comparison between I4 and F4
			//IL_009f: Invalid comparison between I4 and F4
			//IL_000e: Expected F4, but got I4
			//IL_0133: Expected O, but got F4
			//IL_0060: Expected O, but got Ref
			float num = value.x;
			if (0f > value.x)
			{
				num = 0f;
			}
			if ((!(0f > value.y) && 0f > value.z) || 0f > value.w)
			{
				cornerRadii = (Vector4)num;
			}
			MaterialPropertyBlock materialPropertyBlock = base.mpb;
			float num2;
			if (base.mpb != null)
			{
				num2 = num;
			}
			else
			{
				MaterialPropertyBlock materialPropertyBlock2 = (base.mpb = new MaterialPropertyBlock());
				num2 = num;
				materialPropertyBlock = materialPropertyBlock2;
			}
			materialPropertyBlock.SetVector(ShapesMaterialUtils.propCornerRadii, (Vector4)(&num2));
			ApplyProperties();
		}
	}

	public unsafe Vector4 CornerRadiii
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Vector4 vector = default(Vector4);
			((Vector4*)(nint)vector)->x = (float)cornerRadii;
			return vector;
		}
		set
		{
			//IL_0110: Invalid comparison between I4 and F4
			//IL_0031: Expected F4, but got I4
			//IL_014c: Expected O, but got F4
			//IL_0095: Expected O, but got Ref
			float num = ((!(0f > value.x)) ? value.x : 0f);
			object obj = default(object);
			if ((0 <= (nint)obj && 0 > (nint)obj) || 0 > (nint)obj)
			{
				cornerRadii = (Vector4)num;
			}
			MaterialPropertyBlock materialPropertyBlock = base.mpb;
			float num2;
			if (base.mpb != null)
			{
				num2 = num;
			}
			else
			{
				materialPropertyBlock = (base.mpb = new MaterialPropertyBlock());
				num2 = num;
			}
			materialPropertyBlock.SetVector(ShapesMaterialUtils.propCornerRadii, (Vector4)(&num2));
			ApplyProperties();
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
			//IL_003a: Invalid comparison between I4 and F4
			//IL_004c: Expected F4, but got I4
			bool flag = !(0f < value);
			float value2 = 0f;
			if (!flag)
			{
				value2 = value;
			}
			thickness = value2;
			SetFloatNow(ShapesMaterialUtils.propThickness, value2);
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Rectangle)+DC]");
			return 0f;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_0076: Expected O, but got Ref
			DashStyle dashStyle = (DashStyle)(this + 208);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Rectangle)+DC]");
				return 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Rectangle)+E0]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Rectangle)+E4]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Rectangle)+D4]");
			return DashSpace.Meters;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			SetInt(ShapesMaterialUtils.propDashSpace, (int)value);
			DashStyle dashStyle = (DashStyle)(this + 208);
			float netAbsoluteSize = ((DashStyle*)dashStyle)->GetNetAbsoluteSize(dashed, thickness);
			SetFloatNow(ShapesMaterialUtils.propDashSize, netAbsoluteSize);
		}
	}

	public DashSnapping DashSnap
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Rectangle)+D8]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Rectangle)+E8]");
			return 0f;
		}
		set
		{
			SetFloatNow(ShapesMaterialUtils.propDashShapeModifier, value);
		}
	}

	public unsafe GradientFill Fill
	{
		get
		{
			//IL_000f: Expected I4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_004b: Expected O, but got I
			//IL_0046: Expected native int or pointer, but got O
			GradientFill gradientFill = default(GradientFill);
			((GradientFill*)(nint)gradientFill)->type = (FillType)fill;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+FC]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+10C]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+11C]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+12C]");
			((GradientFill*)(nint)gradientFill)->radialOrigin = (Vector3)0;
			return gradientFill;
		}
		set
		{
			//IL_000f: Expected O, but got I4
			fill = (GradientFill)value.type;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [value @ rdx (Shapes.GradientFill)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [value @ rdx (Shapes.GradientFill)+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [value @ rdx (Shapes.GradientFill)+30]");
			_ = 0;
			_ = value.radialOrigin;
			SetFillProperties();
		}
	}

	public unsafe bool UseFill
	{
		get
		{
			return useFill;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			useFill = value;
			GradientFill gradientFill = (GradientFill)(this + 236);
			int shaderFillTypeInt = ((GradientFill*)gradientFill)->GetShaderFillTypeInt(useFill);
			SetIntNow(ShapesMaterialUtils.propFillType, shaderFillTypeInt);
		}
	}

	public unsafe FillType FillType
	{
		get
		{
			//IL_0007: Expected I4, but got O
			return (FillType)fill;
		}
		set
		{
			//IL_0042: Expected O, but got I4
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			fill = (GradientFill)value;
			GradientFill gradientFill = (GradientFill)(this + 236);
			int shaderFillTypeInt = ((GradientFill*)gradientFill)->GetShaderFillTypeInt(useFill);
			SetIntNow(ShapesMaterialUtils.propFillType, shaderFillTypeInt);
		}
	}

	public FillSpace FillSpace
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Rectangle)+F0]");
			return FillSpace.Local;
		}
		set
		{
			SetIntNow(ShapesMaterialUtils.propFillSpace, (int)value);
		}
	}

	public unsafe Vector3 FillRadialOrigin
	{
		get
		{
			//IL_0015: Expected F4, but got I
			//IL_0010: Expected native int or pointer, but got O
			//IL_002a: Expected F4, but got I
			//IL_0025: Expected native int or pointer, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+12C]");
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+134]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_005b: Expected O, but got Ref
			_ = value.x;
			_ = value.z;
			GradientFill gradientFill = (GradientFill)(this + 236);
			Vector4 shaderStartVector = ((GradientFill*)gradientFill)->GetShaderStartVector();
			MaterialPropertyBlock materialPropertyBlock = base.mpb;
			if (base.mpb == null)
			{
				materialPropertyBlock = (base.mpb = new MaterialPropertyBlock());
			}
			float num = default(float);
			materialPropertyBlock.SetVector(ShapesMaterialUtils.propFillStart, (Vector4)(&num));
			ApplyProperties();
		}
	}

	public unsafe float FillRadialRadius
	{
		get
		{
			//IL_000d: Expected F4, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Rectangle)+138]");
			return 0f;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_005b: Expected O, but got Ref
			GradientFill gradientFill = (GradientFill)(this + 236);
			Vector4 shaderStartVector = ((GradientFill*)gradientFill)->GetShaderStartVector();
			MaterialPropertyBlock materialPropertyBlock = base.mpb;
			if (base.mpb == null)
			{
				materialPropertyBlock = (base.mpb = new MaterialPropertyBlock());
			}
			float num = default(float);
			materialPropertyBlock.SetVector(ShapesMaterialUtils.propFillStart, (Vector4)(&num));
			ApplyProperties();
		}
	}

	public unsafe Vector3 FillLinearStart
	{
		get
		{
			//IL_0015: Expected F4, but got I
			//IL_0010: Expected native int or pointer, but got O
			//IL_002a: Expected F4, but got I
			//IL_0025: Expected native int or pointer, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+114]");
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+11C]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_005b: Expected O, but got Ref
			_ = value.x;
			_ = value.z;
			GradientFill gradientFill = (GradientFill)(this + 236);
			Vector4 shaderStartVector = ((GradientFill*)gradientFill)->GetShaderStartVector();
			MaterialPropertyBlock materialPropertyBlock = base.mpb;
			if (base.mpb == null)
			{
				materialPropertyBlock = (base.mpb = new MaterialPropertyBlock());
			}
			float num = default(float);
			materialPropertyBlock.SetVector(ShapesMaterialUtils.propFillStart, (Vector4)(&num));
			ApplyProperties();
		}
	}

	public unsafe Vector3 FillLinearEnd
	{
		get
		{
			//IL_0015: Expected F4, but got I
			//IL_0010: Expected native int or pointer, but got O
			//IL_002a: Expected F4, but got I
			//IL_0025: Expected native int or pointer, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+120]");
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+128]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_002d: Expected O, but got Ref
			_ = value.x;
			_ = value.z;
			object obj = default(object);
			SetVector3Now(ShapesMaterialUtils.propFillEnd, (Vector3)(&obj));
		}
	}

	public unsafe Color FillColorStart
	{
		get
		{
			//IL_0015: Expected F4, but got I
			//IL_0010: Expected native int or pointer, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+F4]");
			Color color = default(Color);
			((Color*)(nint)color)->r = 0f;
			return color;
		}
		set
		{
			//IL_0023: Expected O, but got Ref
			_ = value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColor, (Color)(&obj));
			ApplyProperties();
		}
	}

	public unsafe Color FillColorEnd
	{
		get
		{
			//IL_0015: Expected F4, but got I
			//IL_0010: Expected native int or pointer, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Rectangle)+104]");
			Color color = default(Color);
			((Color*)(nint)color)->r = 0f;
			return color;
		}
		set
		{
			//IL_0023: Expected O, but got Ref
			_ = value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColorEnd, (Color)(&obj));
			ApplyProperties();
		}
	}

	private unsafe void UpdateRectPositioningNow()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_000e: Expected F4, but got I4
		//IL_0017: Expected F4, but got I4
		//IL_009d: Expected O, but got Ref
		float num;
		float num2;
		if (pivot == RectPivot.Corner)
		{
			num = 0f;
			num2 = 0f;
		}
		else
		{
			float num3 = width;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj = num3 ^ 0;
			num2 = (float)obj * 0.5f;
			num = num2;
		}
		MaterialPropertyBlock materialPropertyBlock = base.mpb;
		float num4;
		if (base.mpb != null)
		{
			num4 = num;
		}
		else
		{
			materialPropertyBlock = (base.mpb = new MaterialPropertyBlock());
			num4 = num2;
		}
		materialPropertyBlock.SetVector(ShapesMaterialUtils.propRect, (Vector4)(&num4));
		ApplyProperties();
	}

	private unsafe void UpdateRectPositioning()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_000e: Expected F4, but got I4
		//IL_0017: Expected F4, but got I4
		//IL_009d: Expected O, but got Ref
		float num;
		float num2;
		if (pivot == RectPivot.Corner)
		{
			num = 0f;
			num2 = 0f;
		}
		else
		{
			float num3 = width;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj = num3 ^ 0;
			num2 = (float)obj * 0.5f;
			num = num2;
		}
		MaterialPropertyBlock materialPropertyBlock = base.mpb;
		float num4;
		if (base.mpb != null)
		{
			num4 = num;
		}
		else
		{
			materialPropertyBlock = (base.mpb = new MaterialPropertyBlock());
			num4 = num2;
		}
		materialPropertyBlock.SetVector(ShapesMaterialUtils.propRect, (Vector4)(&num4));
	}

	private unsafe Vector4 GetPositioningRect()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0096: Expected native int or pointer, but got O
		//IL_00a5: Expected native int or pointer, but got O
		//IL_00b2: Expected native int or pointer, but got O
		//IL_00bf: Expected native int or pointer, but got O
		//IL_002b: Expected F4, but got I4
		//IL_0034: Expected F4, but got I4
		float x;
		float y;
		if (pivot == RectPivot.Corner)
		{
			x = 0f;
			y = 0f;
		}
		else
		{
			float num = width;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj = num ^ 0;
			float num2 = height;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj2 = num2 ^ 0;
			x = (float)obj * 0.5f;
			y = (float)obj2 * 0.5f;
		}
		Vector4 vector = default(Vector4);
		((Vector4*)(nint)vector)->z = width;
		((Vector4*)(nint)vector)->w = height;
		((Vector4*)(nint)vector)->x = x;
		((Vector4*)(nint)vector)->y = y;
		return vector;
	}

	private protected unsafe override void SetAllMaterialProperties()
	{
		//IL_0069: Expected O, but got Ref
		//IL_0161: Expected O, but got Ref
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_007c: Expected F4, but got I4
		//IL_0085: Expected F4, but got I4
		//IL_010b: Expected O, but got Ref
		//IL_0132: Expected O, but got Ref
		Vector4 vector = default(Vector4);
		if (cornerRadiusMode != RectangleCornerRadiusMode.PerCorner)
		{
			if (cornerRadiusMode == RectangleCornerRadiusMode.Uniform)
			{
				SetVector4(ShapesMaterialUtils.propCornerRadii, (Vector4)(&vector));
			}
		}
		else
		{
			MaterialPropertyBlock materialPropertyBlock = base.mpb;
			if (base.mpb == null)
			{
				materialPropertyBlock = (base.mpb = new MaterialPropertyBlock());
			}
			materialPropertyBlock.SetVector(ShapesMaterialUtils.propCornerRadii, (Vector4)(&vector));
		}
		float num;
		float num2;
		if (pivot == RectPivot.Corner)
		{
			num = 0f;
			num2 = 0f;
		}
		else
		{
			float num3 = width;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj = num3 ^ 0;
			num2 = (float)obj * 0.5f;
			num = num2;
		}
		MaterialPropertyBlock materialPropertyBlock2 = base.mpb;
		float num4;
		if (base.mpb != null)
		{
			num4 = num;
		}
		else
		{
			MaterialPropertyBlock materialPropertyBlock3 = (base.mpb = new MaterialPropertyBlock());
			num4 = num2;
			materialPropertyBlock2 = materialPropertyBlock3;
		}
		materialPropertyBlock2.SetVector(ShapesMaterialUtils.propRect, (Vector4)(&num4));
		SetFloat(ShapesMaterialUtils.propThickness, thickness);
		SetIntNow(ShapesMaterialUtils.propThicknessSpace, (int)thicknessSpace);
		SetFillProperties();
		float num5 = default(float);
		float num6 = default(float);
		bool setType = default(bool);
		bool now = default(bool);
		SetAllDashValues((DashStyle)(&num5), dashed, matchDashSpacingToSize, num6, setType, now);
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_0061: Expected I4, but got O
		//IL_0075: Expected I, but got O
		//IL_007d: Expected I4, but got O
		//IL_0106: Expected O, but got I4
		//IL_00b0: Expected I, but got O
		//IL_00de: Expected I, but got O
		ShapesMaterials rectMaterial = ShapesMaterialUtils.GetRectMaterial(type);
		bool flag = rectMaterial == null;
		ShapesBlendMode shapesBlendMode = ShapesBlendMode.Opaque;
		RectangleType rectangleType = type;
		if (!flag)
		{
			shapesBlendMode = base.blendMode;
			RectangleType rectangleType2 = (RectangleType)rectMaterial.get_Item(base.blendMode);
			bool flag2 = mats == null;
			nint num = unchecked((nint)null);
			rectangleType = (RectangleType)rectMaterial;
			if (!flag2)
			{
				if (rectangleType2 != RectangleType.HardSolid)
				{
					nint num2 = (nint)mats;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rdx_v9 (Il2CppClass<UnityEngine.Material[]>)+40]");
					shapesBlendMode = ShapesBlendMode.Opaque;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj = default(object);
					bool flag3 = obj == null;
					num = unchecked((nint)null);
					rectangleType = rectangleType2;
					if (flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj2 = default(object);
						throw obj2;
					}
				}
				mats[0] = (Material)rectangleType2;
				return;
			}
		}
		throw new NullReferenceException();
	}

	private protected unsafe override Bounds GetUnpaddedLocalBounds_Internal()
	{
		//IL_0076: Expected O, but got F4
		//IL_0071: Expected native int or pointer, but got O
		//IL_00b2: Expected O, but got F4
		//IL_00ad: Expected native int or pointer, but got O
		//IL_002b: Expected F4, but got I4
		float num;
		if (pivot == RectPivot.Center)
		{
			num = 0f;
			float num3 = default(float);
			float num2 = num3;
		}
		else
		{
			float num4 = height * 0.5f;
			num = width * 0.5f;
			float num2 = num4;
		}
		Bounds bounds = default(Bounds);
		((Bounds*)(nint)bounds)->m_Center = (Vector3)num;
		_ = 0;
		float num5 = width * 0.5f;
		float num6 = height * 0.5f;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num5;
		_ = 0;
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

	private unsafe void SetFillProperties()
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_0060: Expected O, but got Ref
		//IL_0070: Expected O, but got Ref
		//IL_0085: Expected O, but got Ref
		//IL_0094: Expected O, but got Ref
		if (useFill)
		{
			int propFillSpace = ShapesMaterialUtils.propFillSpace;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Rectangle)+F0]");
			SetInt(propFillSpace, 0);
			GradientFill gradientFill = (GradientFill)(this + 236);
			Vector4 shaderStartVector = ((GradientFill*)gradientFill)->GetShaderStartVector();
			MaterialPropertyBlock materialPropertyBlock = base.mpb;
			if (base.mpb == null)
			{
				materialPropertyBlock = (base.mpb = new MaterialPropertyBlock());
			}
			float num = default(float);
			materialPropertyBlock.SetVector(ShapesMaterialUtils.propFillStart, (Vector4)(&num));
			SetVector3(ShapesMaterialUtils.propFillEnd, (Vector3)(&num));
			SetColor(ShapesMaterialUtils.propColor, (Color)(&num));
			SetColor(ShapesMaterialUtils.propColorEnd, (Color)(&num));
		}
		GradientFill gradientFill2 = (GradientFill)(this + 236);
		int shaderFillTypeInt = ((GradientFill*)gradientFill2)->GetShaderFillTypeInt(useFill);
		SetInt(ShapesMaterialUtils.propFillType, shaderFillTypeInt);
	}

	public Rectangle()
	{
		//IL_0017: Expected O, but got I
		//IL_005c: Expected I, but got O
		//IL_0173: Expected I, but got O
		//IL_00c6: Expected I4, but got I8
		//IL_0108: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [1822E8420]");
		cornerRadii = (Vector4)0;
		pivot = RectPivot.Center;
		width = 1f;
		height = 1f;
		thickness = 0.1f;
		matchDashSpacingToSize = true;
		nint num = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v3 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num2 = 0;
		dashStyle = DashStyle.defaultDashStyleRing;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+2C]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+34]");
		_ = 0;
		nint num3 = (nint)typeof(GradientFill);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v6 (Il2CppClass<Shapes.GradientFill>)+B8]");
		nint num4 = 0;
		meshOutOfDate = true;
		fill = GradientFill.defaultFill;
		base.blendMode = ShapesBlendMode.Transparent;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v7 (Il2CppStaticFields<Shapes.GradientFill>)+10]");
		_ = 0;
		detailLevel = DetailLevel.Medium;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v7 (Il2CppStaticFields<Shapes.GradientFill>)+20]");
		_ = 0;
		base.renderQueue = -1;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v7 (Il2CppStaticFields<Shapes.GradientFill>)+30]");
		_ = 0;
		base.zTest = CompareFunction.LessEqual;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v7 (Il2CppStaticFields<Shapes.GradientFill>)+40]");
		_ = 0;
		base.colorMask = ColorWriteMask.All;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		base.stencilComp = CompareFunction.Always;
		base.stencilReadMask = 255;
		base.shouldUpdateMaterialPropertiesInEditor = true;
		((MonoBehaviour)this)._002Ector();
	}
}
