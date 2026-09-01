using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Cuboid : ShapeRenderer
{
	private Vector3 size;

	private ThicknessSpace sizeSpace;

	public unsafe Vector3 Size
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)size;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Cuboid)+A8]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0032: Expected O, but got Ref
			size = (Vector3)value.x;
			_ = value.z;
			object obj = default(object);
			SetVector3Now(ShapesMaterialUtils.propSize, (Vector3)(&obj));
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

	internal override bool HasDetailLevels => false;

	internal override bool HasScaleModes => false;

	private protected unsafe override void SetAllMaterialProperties()
	{
		//IL_0019: Expected O, but got Ref
		object obj = default(object);
		SetVector3(ShapesMaterialUtils.propSize, (Vector3)(&obj));
		SetInt(ShapesMaterialUtils.propSizeSpace, (int)sizeSpace);
	}

	private protected override void ShapeClampRanges()
	{
		//IL_002d: Expected O, but got I
		//IL_007f: Expected O, but got I4
		if (0 < (nint)size)
		{
			goto IL_001d;
		}
		object obj = default(object);
		Vector3 vector = default(Vector3);
		object obj2;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) >= System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Cuboid)+A8]");
			bool flag = (nint)0 >= (nint)0;
			obj2 = 0;
			if (!flag)
			{
				goto IL_001d;
			}
		}
		goto IL_004c;
		IL_001d:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Cuboid)+A8]");
		obj2 = 0;
		goto IL_004c;
		IL_004c:
		size = vector;
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_000f: Expected O, but got I4
		//IL_0037: Expected I, but got O
		//IL_006a: Expected I, but got O
		//IL_007a: Expected O, but got I
		//IL_0098: Expected I, but got O
		ShapesMaterials matCuboid = ShapesMaterialUtils.matCuboid;
		bool flag = ShapesMaterialUtils.matCuboid == null;
		Material[] array = mats;
		if (!flag)
		{
			array = (Material[])base.blendMode;
			ShapesMaterials shapesMaterials = (ShapesMaterials)(object)ShapesMaterialUtils.matCuboid.get_Item(base.blendMode);
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
					matCuboid = shapesMaterials;
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
		Mesh[] cuboidMesh = ShapesMeshUtils.CuboidMesh;
		if (cuboidMesh.Length > 0)
		{
			return cuboidMesh[0];
		}
		return (Mesh)(object)new IndexOutOfRangeException();
	}

	private protected unsafe override Bounds GetUnpaddedLocalBounds_Internal()
	{
		//IL_00b9: Expected I, but got O
		//IL_00e2: Expected O, but got I
		//IL_00f0: Expected O, but got I4
		//IL_00eb: Expected native int or pointer, but got O
		//IL_003c: Expected O, but got I
		//IL_004a: Expected O, but got I4
		//IL_0045: Expected native int or pointer, but got O
		//IL_008c: Expected O, but got F4
		//IL_0087: Expected native int or pointer, but got O
		Vector3 zeroVector;
		object obj;
		Bounds bounds = default(Bounds);
		if (sizeSpace == ThicknessSpace.Meters)
		{
			zeroVector = size;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Cuboid)+A8]");
			obj = 0;
			((Bounds*)(nint)bounds)->m_Center = (Vector3)0;
			_ = 0;
		}
		else
		{
			nint num = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rax_v4 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num2 = 0;
			zeroVector = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			obj = 0;
			((Bounds*)(nint)bounds)->m_Center = (Vector3)0;
			_ = 0;
		}
		object obj2 = default(object);
		float num3 = (float)obj2 * 0.5f;
		float num4 = (float)zeroVector * 0.5f;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num4;
		float num5 = (float)obj * 0.5f;
		return bounds;
	}

	public Cuboid()
	{
		//IL_0013: Expected I, but got O
		//IL_0040: Expected O, but got I
		//IL_007d: Expected I4, but got I8
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		size = Vector3.oneVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		_ = 0;
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
