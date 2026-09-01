using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Polygon : ShapeRenderer, IFillable
{
	public List<Vector2> points;

	private PolygonTriangulation triangulation;

	private protected GradientFill fill;

	private protected bool useFill;

	public PolygonTriangulation Triangulation
	{
		get
		{
			return triangulation;
		}
		set
		{
			triangulation = value;
			meshOutOfDate = true;
		}
	}

	public int Count
	{
		get
		{
			//IL_0020: Expected I4, but got O
			List<Vector2> list = points;
			if (points != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
				return 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	// C# has no syntax for parameterized property 'Item'.
	public Vector2 get_Item(int i)
	{
		if (points != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Vector2 result = default(Vector2);
			return result;
		}
		return (Vector2)new NullReferenceException();
	}

	public unsafe void set_Item(int i, Vector2 value)
	{
		//IL_0018: Expected O, but got Ref
		object obj = default(object);
		points.set_Item(i, (Vector2)(&obj));
		meshOutOfDate = true;
	}

	private protected override bool UseCamOnPreCull => true;

	internal override bool HasScaleModes => false;

	internal override bool HasDetailLevels => false;

	private protected override MeshUpdateMode MeshUpdateMode => MeshUpdateMode.SelfGenerated;

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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+BC]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+CC]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+DC]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+EC]");
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
			GradientFill gradientFill = (GradientFill)(this + 172);
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
			GradientFill gradientFill = (GradientFill)(this + 172);
			int shaderFillTypeInt = ((GradientFill*)gradientFill)->GetShaderFillTypeInt(useFill);
			SetIntNow(ShapesMaterialUtils.propFillType, shaderFillTypeInt);
		}
	}

	public FillSpace FillSpace
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Polygon)+B0]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+EC]");
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+F4]");
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
			GradientFill gradientFill = (GradientFill)(this + 172);
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Polygon)+F8]");
			return 0f;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_005b: Expected O, but got Ref
			GradientFill gradientFill = (GradientFill)(this + 172);
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+D4]");
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+DC]");
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
			GradientFill gradientFill = (GradientFill)(this + 172);
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+E0]");
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+E8]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+B4]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Polygon)+C4]");
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

	public unsafe void SetPointPosition(int index, Vector2 position)
	{
		//IL_0042: Expected O, but got Ref
		if (index >= 0)
		{
			List<Vector2> list = points;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v11 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
			if ((nint)index < (nint)0)
			{
				object obj = default(object);
				list.set_Item(index, (Vector2)(&obj));
				meshOutOfDate = true;
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public void SetPoints(IEnumerable<Vector2> points)
	{
		//IL_009d: Expected O, but got I
		List<Vector2> list = this.points;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+1C]");
		_ = (nint)0 + (nint)1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
				Array.Clear((Array)num, 0, 0);
			}
		}
		this.points.AddRange(points);
		meshOutOfDate = true;
	}

	public void AddPoints(IEnumerable<Vector2> points)
	{
		this.points.AddRange(points);
		meshOutOfDate = true;
	}

	public unsafe void AddPoint(Vector2 point)
	{
		//IL_0014: Expected O, but got Ref
		object obj = default(object);
		points.Add((Vector2)(&obj));
		meshOutOfDate = true;
	}

	internal override void CamOnPreCull()
	{
		if (meshOutOfDate)
		{
			meshOutOfDate = false;
			UpdateMesh(force: true);
		}
	}

	private protected override void SetAllMaterialProperties()
	{
		SetFillProperties();
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_000f: Expected O, but got I4
		//IL_0037: Expected I, but got O
		//IL_006a: Expected I, but got O
		//IL_007a: Expected O, but got I
		//IL_0098: Expected I, but got O
		ShapesMaterials matPolygon = ShapesMaterialUtils.matPolygon;
		bool flag = ShapesMaterialUtils.matPolygon == null;
		Material[] array = mats;
		if (!flag)
		{
			array = (Material[])base.blendMode;
			ShapesMaterials shapesMaterials = (ShapesMaterials)(object)ShapesMaterialUtils.matPolygon.get_Item(base.blendMode);
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
					matPolygon = shapesMaterials;
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

	private protected override void GenerateMesh()
	{
		Mesh sharedMesh = base.mf.sharedMesh;
		ShapesMeshGen.GenPolygonMesh(sharedMesh, points, triangulation);
	}

	private protected unsafe override Bounds GetUnpaddedLocalBounds_Internal()
	{
		//IL_00d4: Expected O, but got I4
		//IL_00cf: Expected native int or pointer, but got O
		//IL_0122: Expected I, but got O
		//IL_0169: Expected I, but got O
		//IL_01d4: Expected native int or pointer, but got O
		List<Vector2> list = points;
		if (points != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rax_v4 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
			Bounds bounds = default(Bounds);
			if ((nint)0 < (nint)2)
			{
				((Bounds*)(nint)bounds)->m_Center = (Vector3)0;
				_ = 0;
			}
			else
			{
				nint num = (nint)typeof(Vector2);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v138 @ rdx_v3 (Il2CppClass<UnityEngine.Vector2>)+B8]");
				nint num2 = 0;
				float num3 = (float)Vector2.oneVector * 3.4028235E+38f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rax_v12 (Il2CppStaticFields<UnityEngine.Vector2>)+C]");
				float num4 = 0f * 3.4028235E+38f;
				nint num5 = (nint)typeof(Vector2);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rdx_v4 (Il2CppClass<UnityEngine.Vector2>)+B8]");
				nint num6 = 0;
				float num7 = (float)Vector2.oneVector * -3.4028235E+38f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rax_v14 (Il2CppStaticFields<UnityEngine.Vector2>)+C]");
				float num8 = 0f * -3.4028235E+38f;
				if (points == null)
				{
					goto IL_00df;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<Vector2>.Enumerator enumerator = default(List<Vector2>.Enumerator);
				float num9 = default(float);
				float num10 = default(float);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (!(num9 > num3))
					{
						num3 = num9;
					}
					if (!(num10 > num4))
					{
						num4 = num10;
					}
					if (!(num7 > num9))
					{
						num7 = num9;
					}
					if (!(num8 > num10))
					{
						num8 = num10;
					}
				}
				enumerator.Dispose();
				Vector3 center = default(Vector3);
				((Bounds*)(nint)bounds)->m_Center = center;
			}
			return bounds;
		}
		goto IL_00df;
		IL_00df:
		throw new NullReferenceException();
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Polygon)+B0]");
			SetInt(propFillSpace, 0);
			GradientFill gradientFill = (GradientFill)(this + 172);
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
		GradientFill gradientFill2 = (GradientFill)(this + 172);
		int shaderFillTypeInt = ((GradientFill*)gradientFill2)->GetShaderFillTypeInt(useFill);
		SetInt(ShapesMaterialUtils.propFillType, shaderFillTypeInt);
	}

	public unsafe Polygon()
	{
		//IL_0012: Expected O, but got Ref
		//IL_001f: Expected O, but got Ref
		//IL_0031: Expected O, but got Ref
		//IL_003e: Expected O, but got Ref
		//IL_0050: Expected O, but got Ref
		//IL_005d: Expected O, but got Ref
		//IL_0085: Expected I, but got O
		//IL_00fe: Expected I4, but got I8
		//IL_0140: Expected O, but got I
		object obj = default(object);
		points = new List<Vector2>
		{
			(Vector2)(&obj),
			(Vector2)(&obj),
			(Vector2)(&obj),
			(Vector2)(&obj),
			(Vector2)(&obj),
			(Vector2)(&obj)
		};
		triangulation = PolygonTriangulation.EarClipping;
		nint num = (nint)typeof(GradientFill);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ rax_v13 (Il2CppClass<Shapes.GradientFill>)+B8]");
		nint num2 = 0;
		meshOutOfDate = true;
		fill = GradientFill.defaultFill;
		base.blendMode = ShapesBlendMode.Transparent;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ rax_v14 (Il2CppStaticFields<Shapes.GradientFill>)+10]");
		_ = 0;
		detailLevel = DetailLevel.Medium;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ rax_v14 (Il2CppStaticFields<Shapes.GradientFill>)+20]");
		_ = 0;
		base.renderQueue = -1;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ rax_v14 (Il2CppStaticFields<Shapes.GradientFill>)+30]");
		_ = 0;
		base.zTest = CompareFunction.LessEqual;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ rax_v14 (Il2CppStaticFields<Shapes.GradientFill>)+40]");
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
