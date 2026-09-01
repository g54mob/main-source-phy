using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Torus : ShapeRenderer
{
	private float radius;

	private float thickness;

	private ThicknessSpace thicknessSpace;

	private ThicknessSpace radiusSpace;

	private AngularUnit angUnitInput;

	private float angRadiansStart;

	private float angRadiansEnd;

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

	public ThicknessSpace RadiusSpace
	{
		get
		{
			return radiusSpace;
		}
		set
		{
			radiusSpace = value;
			SetIntNow(ShapesMaterialUtils.propThicknessSpace, (int)value);
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

	internal override bool HasDetailLevels => true;

	private protected override void SetAllMaterialProperties()
	{
		SetFloat(ShapesMaterialUtils.propRadius, radius);
		SetFloat(ShapesMaterialUtils.propThickness, thickness);
		SetInt(ShapesMaterialUtils.propThicknessSpace, (int)thicknessSpace);
		SetInt(ShapesMaterialUtils.propRadiusSpace, (int)radiusSpace);
		SetFloat(ShapesMaterialUtils.propAngStart, angRadiansStart);
		SetFloat(ShapesMaterialUtils.propAngEnd, angRadiansEnd);
	}

	private protected override void ShapeClampRanges()
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_001d: Expected F4, but got I4
		//IL_005e: Invalid comparison between I4 and F4
		//IL_0070: Expected F4, but got I4
		bool flag = !(0f < radius);
		float num = 0f;
		if (!flag)
		{
			num = radius;
		}
		radius = num;
		bool flag2 = !(0f < thickness);
		float num2 = 0f;
		if (!flag2)
		{
			num2 = thickness;
		}
		thickness = num2;
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_000f: Expected O, but got I4
		//IL_0037: Expected I, but got O
		//IL_006a: Expected I, but got O
		//IL_007a: Expected O, but got I
		//IL_0098: Expected I, but got O
		ShapesMaterials matTorus = ShapesMaterialUtils.matTorus;
		bool flag = ShapesMaterialUtils.matTorus == null;
		Material[] array = mats;
		if (!flag)
		{
			array = (Material[])base.blendMode;
			ShapesMaterials shapesMaterials = (ShapesMaterials)(object)ShapesMaterialUtils.matTorus.get_Item(base.blendMode);
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
					matTorus = shapesMaterials;
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

	private protected override Mesh GetInitialMeshAsset()
	{
		Mesh[] torusMesh = ShapesMeshUtils.TorusMesh;
		DetailLevel detailLevel = base.detailLevel;
		if ((int)base.detailLevel < torusMesh.Length)
		{
			return torusMesh[(int)detailLevel];
		}
		return (Mesh)(object)new IndexOutOfRangeException();
	}

	private protected unsafe override Bounds GetUnpaddedLocalBounds_Internal()
	{
		//IL_0043: Expected F4, but got I4
		//IL_0074: Expected F4, but got I4
		//IL_0090: Expected I, but got O
		//IL_00b9: Expected native int or pointer, but got O
		//IL_00fd: Expected O, but got F4
		//IL_00f8: Expected native int or pointer, but got O
		float num = ((radiusSpace != ThicknessSpace.Meters) ? 0f : (radius + radius));
		bool flag = thicknessSpace != ThicknessSpace.Meters;
		float num2 = 0f;
		if (!flag)
		{
			num2 = thickness;
		}
		float num3 = num2 + num;
		nint num4 = (nint)typeof(Vector3);
		float num5 = num2 * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num6 = 0;
		Bounds bounds = default(Bounds);
		((Bounds*)(nint)bounds)->m_Center = Vector3.zeroVector;
		float num7 = num3 * 0.5f;
		float num8 = num3 * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rcx_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num8;
		return bounds;
	}

	public Torus()
	{
		//IL_0012: Expected O, but got I
		//IL_006e: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		radius = 1f;
		thickness = 0.5f;
		angUnitInput = AngularUnit.Degrees;
		angRadiansEnd = (float)Math.PI * 2f;
		meshOutOfDate = true;
		base.blendMode = ShapesBlendMode.Transparent;
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
