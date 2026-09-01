using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Cone : ShapeRenderer
{
	private float radius;

	private float length;

	private ThicknessSpace sizeSpace;

	private bool fillCap;

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

	public float Length
	{
		get
		{
			return length;
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
			length = value2;
			SetFloatNow(ShapesMaterialUtils.propLength, value2);
		}
	}

	public ThicknessSpace RadiusSpace
	{
		get
		{
			return sizeSpace;
		}
		set
		{
			sizeSpace = value;
			SetIntNow(ShapesMaterialUtils.propSizeSpace, (int)value);
		}
	}

	public ThicknessSpace SizeSpace
	{
		get
		{
			return sizeSpace;
		}
		set
		{
			sizeSpace = value;
			SetIntNow(ShapesMaterialUtils.propSizeSpace, (int)value);
		}
	}

	public bool FillCap
	{
		get
		{
			return fillCap;
		}
		set
		{
			fillCap = value;
			UpdateMesh(force: true);
		}
	}

	internal override bool HasDetailLevels => true;

	internal override bool HasScaleModes => false;

	private protected override void SetAllMaterialProperties()
	{
		SetFloat(ShapesMaterialUtils.propRadius, radius);
		SetFloat(ShapesMaterialUtils.propLength, length);
		SetInt(ShapesMaterialUtils.propSizeSpace, (int)sizeSpace);
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
		bool flag2 = !(0f < length);
		float num2 = 0f;
		if (!flag2)
		{
			num2 = length;
		}
		length = num2;
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_000f: Expected O, but got I4
		//IL_0037: Expected I, but got O
		//IL_006a: Expected I, but got O
		//IL_007a: Expected O, but got I
		//IL_0098: Expected I, but got O
		ShapesMaterials matCone = ShapesMaterialUtils.matCone;
		bool flag = ShapesMaterialUtils.matCone == null;
		Material[] array = mats;
		if (!flag)
		{
			array = (Material[])base.blendMode;
			ShapesMaterials shapesMaterials = (ShapesMaterials)(object)ShapesMaterialUtils.matCone.get_Item(base.blendMode);
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
					matCone = shapesMaterials;
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
		DetailLevel detailLevel;
		Mesh[] array;
		if (fillCap)
		{
			Mesh[] coneMesh = ShapesMeshUtils.ConeMesh;
			detailLevel = base.detailLevel;
			bool flag = (int)base.detailLevel >= coneMesh.Length;
			array = coneMesh;
			if (!flag)
			{
				goto IL_0062;
			}
		}
		else
		{
			Mesh[] coneMeshUncapped = ShapesMeshUtils.ConeMeshUncapped;
			detailLevel = base.detailLevel;
			if ((int)base.detailLevel < coneMeshUncapped.Length)
			{
				array = coneMeshUncapped;
				goto IL_0062;
			}
		}
		return (Mesh)(object)new IndexOutOfRangeException();
		IL_0062:
		return array[(int)detailLevel];
	}

	private protected unsafe override Bounds GetUnpaddedLocalBounds_Internal()
	{
		//IL_00ce: Expected I, but got O
		//IL_00ec: Expected I, but got O
		//IL_0056: Expected O, but got I4
		//IL_0051: Expected native int or pointer, but got O
		//IL_009a: Expected O, but got F4
		//IL_0095: Expected native int or pointer, but got O
		//IL_010a: Expected native int or pointer, but got O
		//IL_014f: Expected O, but got F4
		//IL_014a: Expected native int or pointer, but got O
		Bounds bounds = default(Bounds);
		if (sizeSpace == ThicknessSpace.Meters)
		{
			float num = radius + radius;
			float num2 = radius + radius;
			((Bounds*)(nint)bounds)->m_Center = (Vector3)0;
			float num3 = length * 0.5f;
			float num4 = num * 0.5f;
			float num5 = num2 * 0.5f;
			((Bounds*)(nint)bounds)->m_Extents = (Vector3)num4;
			float num6 = length * 0.5f;
			return bounds;
		}
		nint num7 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rdx_v1 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num8 = 0;
		nint num9 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rdx_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num10 = 0;
		((Bounds*)(nint)bounds)->m_Center = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rax_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		object obj = default(object);
		float num11 = (float)obj * 0.5f;
		float num12 = (float)Vector3.zeroVector * 0.5f;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num12;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rax_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		float num13 = 0f * 0.5f;
		return bounds;
	}

	public Cone()
	{
		//IL_0012: Expected O, but got I
		//IL_0063: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		radius = 1f;
		length = 1.5f;
		fillCap = true;
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
