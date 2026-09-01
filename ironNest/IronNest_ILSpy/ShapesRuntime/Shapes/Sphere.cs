using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Sphere : ShapeRenderer
{
	private float radius;

	private ThicknessSpace radiusSpace;

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

	internal override bool HasDetailLevels => true;

	internal override bool HasScaleModes => false;

	private protected override void SetAllMaterialProperties()
	{
		SetFloat(ShapesMaterialUtils.propRadius, radius);
		SetInt(ShapesMaterialUtils.propRadiusSpace, (int)radiusSpace);
	}

	private protected override void ShapeClampRanges()
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_001d: Expected F4, but got I4
		bool flag = !(0f < radius);
		float num = 0f;
		if (!flag)
		{
			num = radius;
		}
		radius = num;
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_000f: Expected O, but got I4
		//IL_0037: Expected I, but got O
		//IL_006a: Expected I, but got O
		//IL_007a: Expected O, but got I
		//IL_0098: Expected I, but got O
		ShapesMaterials matSphere = ShapesMaterialUtils.matSphere;
		bool flag = ShapesMaterialUtils.matSphere == null;
		Material[] array = mats;
		if (!flag)
		{
			array = (Material[])base.blendMode;
			ShapesMaterials shapesMaterials = (ShapesMaterials)(object)ShapesMaterialUtils.matSphere.get_Item(base.blendMode);
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
					matSphere = shapesMaterials;
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
		Mesh[] sphereMesh = ShapesMeshUtils.SphereMesh;
		DetailLevel detailLevel = base.detailLevel;
		if ((int)base.detailLevel < sphereMesh.Length)
		{
			return sphereMesh[(int)detailLevel];
		}
		return (Mesh)(object)new IndexOutOfRangeException();
	}

	private protected unsafe override Bounds GetUnpaddedLocalBounds_Internal()
	{
		//IL_0043: Expected F4, but got I4
		//IL_005b: Expected I, but got O
		//IL_0074: Expected native int or pointer, but got O
		//IL_00c8: Expected O, but got F4
		//IL_00c3: Expected native int or pointer, but got O
		float num = ((radiusSpace != ThicknessSpace.Meters) ? 0f : (radius + radius));
		nint num2 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num3 = 0;
		Bounds bounds = default(Bounds);
		((Bounds*)(nint)bounds)->m_Center = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rcx_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		float num4 = num * 0.5f;
		float num5 = num * 0.5f;
		float num6 = num * 0.5f;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num5;
		return bounds;
	}

	public Sphere()
	{
		//IL_0012: Expected O, but got I
		//IL_004d: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		radius = 1f;
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
