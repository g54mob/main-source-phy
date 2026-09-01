using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class RegularPolygon : ShapeRenderer, IDashable, IFillable
{
	private bool border;

	private int sides;

	private float roundness;

	private float angle;

	private float radius;

	private AngularUnit angUnitInput;

	private RegularPolygonGeometry geometry;

	private ThicknessSpace radiusSpace;

	private float thickness;

	private ThicknessSpace thicknessSpace;

	private bool matchDashSpacingToSize;

	private bool dashed;

	private DashStyle dashStyle;

	private protected GradientFill fill;

	private protected bool useFill;

	public bool Border
	{
		get
		{
			return border;
		}
		set
		{
			border = value;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D51B20");
			int value2 = default(int);
			SetIntNow(ShapesMaterialUtils.propBorder, value2);
		}
	}

	public bool Hollow
	{
		get
		{
			return border;
		}
		set
		{
			border = value;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D51B20");
			int value2 = default(int);
			SetIntNow(ShapesMaterialUtils.propBorder, value2);
		}
	}

	public int Sides
	{
		get
		{
			return sides;
		}
		set
		{
			bool flag = value < 3;
			int value2 = 3;
			if (!flag)
			{
				value2 = value;
			}
			sides = value2;
			SetIntNow(ShapesMaterialUtils.propSides, value2);
		}
	}

	public float Roundness
	{
		get
		{
			return roundness;
		}
		set
		{
			//IL_0071: Invalid comparison between I4 and F4
			//IL_0044: Expected F4, but got I4
			float value2;
			if (!(0f > value))
			{
				bool flag = !(value > 1f);
				value2 = value;
				if (!flag)
				{
					value2 = 1f;
				}
			}
			else
			{
				value2 = 0f;
			}
			roundness = value2;
			SetFloatNow(ShapesMaterialUtils.propRoundness, value2);
		}
	}

	public float Angle
	{
		get
		{
			return angle;
		}
		set
		{
			angle = value;
			SetFloatNow(ShapesMaterialUtils.propAng, value);
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

	public RegularPolygonGeometry Geometry
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.RegularPolygon)+D8]");
			return 0f;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_0076: Expected O, but got Ref
			DashStyle dashStyle = (DashStyle)(this + 204);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.RegularPolygon)+D8]");
				return 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.RegularPolygon)+DC]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.RegularPolygon)+E0]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.RegularPolygon)+D0]");
			return DashSpace.Meters;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			SetInt(ShapesMaterialUtils.propDashSpace, (int)value);
			DashStyle dashStyle = (DashStyle)(this + 204);
			float netAbsoluteSize = ((DashStyle*)dashStyle)->GetNetAbsoluteSize(dashed, thickness);
			SetFloatNow(ShapesMaterialUtils.propDashSize, netAbsoluteSize);
		}
	}

	public DashSnapping DashSnap
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.RegularPolygon)+D4]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.RegularPolygon)+E4]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+F8]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+108]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+118]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+128]");
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
			//IL_003c: Expected I, but got O
			//IL_004c: Expected O, but got I
			//IL_005c: Expected O, but got I
			while (true)
			{
				useFill = value;
				GradientFill gradientFill = (GradientFill)(this + 232);
				int shaderFillTypeInt = ((GradientFill*)gradientFill)->GetShaderFillTypeInt(useFill);
				SetIntNow(ShapesMaterialUtils.propFillType, shaderFillTypeInt);
				nint num = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rdx_v3 (Il2CppClass<Shapes.RegularPolygon>)+1E8]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rdx_v3 (Il2CppClass<Shapes.RegularPolygon>)+1F0]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v78 @ rax_v8 (should have been resolved before IL gen)");
			}
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
			GradientFill gradientFill = (GradientFill)(this + 232);
			int shaderFillTypeInt = ((GradientFill*)gradientFill)->GetShaderFillTypeInt(useFill);
			SetIntNow(ShapesMaterialUtils.propFillType, shaderFillTypeInt);
		}
	}

	public FillSpace FillSpace
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.RegularPolygon)+EC]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+128]");
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+130]");
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
			GradientFill gradientFill = (GradientFill)(this + 232);
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.RegularPolygon)+134]");
			return 0f;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_005b: Expected O, but got Ref
			GradientFill gradientFill = (GradientFill)(this + 232);
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+110]");
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+118]");
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
			GradientFill gradientFill = (GradientFill)(this + 232);
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+11C]");
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+124]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+F0]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.RegularPolygon)+100]");
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

	private protected unsafe override void SetAllMaterialProperties()
	{
		//IL_0075: Expected F4, but got I4
		//IL_00ad: Expected O, but got Ref
		SetFillProperties();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D51B20");
		int value = default(int);
		SetIntNow(ShapesMaterialUtils.propBorder, value);
		SetInt(ShapesMaterialUtils.propAlignment, (int)geometry);
		SetFloat(ShapesMaterialUtils.propRadius, radius);
		SetInt(ShapesMaterialUtils.propRadiusSpace, (int)radiusSpace);
		SetFloat(ShapesMaterialUtils.propThickness, thickness);
		SetInt(ShapesMaterialUtils.propThicknessSpace, (int)thicknessSpace);
		SetFloat(ShapesMaterialUtils.propAng, angle);
		SetFloat(ShapesMaterialUtils.propSides, sides);
		SetFloat(ShapesMaterialUtils.propRoundness, roundness);
		object obj = default(object);
		float num = default(float);
		bool setType = default(bool);
		bool now = default(bool);
		SetAllDashValues((DashStyle)(&obj), dashed, matchDashSpacingToSize, num, setType, now);
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_000f: Expected O, but got I4
		//IL_0037: Expected I, but got O
		//IL_006a: Expected I, but got O
		//IL_007a: Expected O, but got I
		//IL_0098: Expected I, but got O
		ShapesMaterials matRegularPolygon = ShapesMaterialUtils.matRegularPolygon;
		bool flag = ShapesMaterialUtils.matRegularPolygon == null;
		Material[] array = mats;
		if (!flag)
		{
			array = (Material[])base.blendMode;
			ShapesMaterials shapesMaterials = (ShapesMaterials)(object)ShapesMaterialUtils.matRegularPolygon.get_Item(base.blendMode);
			bool flag2 = mats == null;
			nint num = unchecked((nint)null);
			if (!flag2)
			{
				if (shapesMaterials != null)
				{
					nint num2 = (nint)mats;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rdx_v8 (Il2CppClass<UnityEngine.Material[]>)+40]");
					array = (Material[])0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj = default(object);
					bool flag3 = obj == null;
					num = unchecked((nint)null);
					matRegularPolygon = shapesMaterials;
					if (flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj2 = default(object);
						throw obj2;
					}
				}
				mats[0] = (Material)(object)shapesMaterials;
				return;
			}
		}
		throw new NullReferenceException();
	}

	private protected unsafe override Bounds GetUnpaddedLocalBounds_Internal()
	{
		//IL_009b: Expected I, but got O
		//IL_00b9: Expected I, but got O
		//IL_0152: Expected native int or pointer, but got O
		//IL_0197: Expected O, but got F4
		//IL_0192: Expected native int or pointer, but got O
		//IL_006f: Expected F4, but got I4
		//IL_00cc: Expected I, but got O
		//IL_00e5: Expected native int or pointer, but got O
		//IL_0129: Expected O, but got F4
		//IL_0124: Expected native int or pointer, but got O
		Bounds bounds = default(Bounds);
		if (radiusSpace == ThicknessSpace.Meters)
		{
			float num = radius + radius;
			float num2 = ((thicknessSpace != ThicknessSpace.Meters) ? 0f : thickness);
			float num3 = num + num2;
			nint num4 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v9 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num5 = 0;
			((Bounds*)(nint)bounds)->m_Center = Vector3.zeroVector;
			float num6 = num3 * 0.5f;
			float num7 = num3 * 0.5f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rcx_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			_ = 0;
			((Bounds*)(nint)bounds)->m_Extents = (Vector3)num7;
			_ = 0;
			return bounds;
		}
		nint num8 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rdx_v1 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num9 = 0;
		nint num10 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rdx_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num11 = 0;
		((Bounds*)(nint)bounds)->m_Center = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		object obj = default(object);
		float num12 = (float)obj * 0.5f;
		float num13 = (float)Vector3.zeroVector * 0.5f;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num13;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rax_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		float num14 = 0f * 0.5f;
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.RegularPolygon)+EC]");
			SetInt(propFillSpace, 0);
			GradientFill gradientFill = (GradientFill)(this + 232);
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
		GradientFill gradientFill2 = (GradientFill)(this + 232);
		int shaderFillTypeInt = ((GradientFill*)gradientFill2)->GetShaderFillTypeInt(useFill);
		SetInt(ShapesMaterialUtils.propFillType, shaderFillTypeInt);
	}

	public RegularPolygon()
	{
		//IL_0055: Expected I, but got O
		//IL_016c: Expected I, but got O
		//IL_00bf: Expected I4, but got I8
		//IL_0101: Expected O, but got I
		sides = 3;
		angle = (float)Math.PI / 2f;
		radius = 1f;
		angUnitInput = AngularUnit.Degrees;
		thickness = 0.5f;
		matchDashSpacingToSize = true;
		nint num = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rax_v3 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num2 = 0;
		dashStyle = DashStyle.defaultDashStyleRing;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+2C]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+34]");
		_ = 0;
		nint num3 = (nint)typeof(GradientFill);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rax_v6 (Il2CppClass<Shapes.GradientFill>)+B8]");
		nint num4 = 0;
		meshOutOfDate = true;
		fill = GradientFill.defaultFill;
		base.blendMode = ShapesBlendMode.Transparent;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rax_v7 (Il2CppStaticFields<Shapes.GradientFill>)+10]");
		_ = 0;
		detailLevel = DetailLevel.Medium;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rax_v7 (Il2CppStaticFields<Shapes.GradientFill>)+20]");
		_ = 0;
		base.renderQueue = -1;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rax_v7 (Il2CppStaticFields<Shapes.GradientFill>)+30]");
		_ = 0;
		base.zTest = CompareFunction.LessEqual;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rax_v7 (Il2CppStaticFields<Shapes.GradientFill>)+40]");
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
