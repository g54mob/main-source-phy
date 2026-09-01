using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public abstract class ShapeRendererFillable : ShapeRenderer
{
	private const string OBSOLETE = "Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable";

	private protected GradientFill fill;

	private protected bool useFill;

	private int FillTypeShaderInt
	{
		get
		{
			//IL_0030: Expected I4, but got I8
			//IL_0026: Expected I4, but got O
			if (!useFill)
			{
				return -1;
			}
			return (int)fill;
		}
	}

	public bool UseFill
	{
		get
		{
			return useFill;
		}
		set
		{
			//IL_0021: Expected I4, but got I8
			//IL_000f: Expected I4, but got O
			useFill = value;
			SetIntNow(value: (int)((!useFill) ? 4294967295L : ((int)fill)), prop: ShapesMaterialUtils.propFillType);
		}
	}

	public FillType FillType
	{
		get
		{
			return FillType.LinearGradient;
		}
		set
		{
		}
	}

	public FillSpace FillSpace
	{
		get
		{
			return FillSpace.Local;
		}
		set
		{
		}
	}

	public unsafe Vector3 FillRadialOrigin
	{
		get
		{
			//IL_0009: Expected native int or pointer, but got O
			//IL_0017: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
		}
	}

	public float FillRadialRadius
	{
		get
		{
			//IL_0006: Expected F4, but got I4
			return 0f;
		}
		set
		{
		}
	}

	public unsafe Vector3 FillLinearStart
	{
		get
		{
			//IL_0009: Expected native int or pointer, but got O
			//IL_0017: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
		}
	}

	public unsafe Vector3 FillLinearEnd
	{
		get
		{
			//IL_0009: Expected native int or pointer, but got O
			//IL_0017: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = 0f;
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
		}
	}

	public unsafe Color FillColorStart
	{
		get
		{
			//IL_0009: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = 0f;
			return color;
		}
		set
		{
		}
	}

	public unsafe Color FillColorEnd
	{
		get
		{
			//IL_0009: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = 0f;
			return color;
		}
		set
		{
		}
	}

	private protected void SetFillProperties()
	{
	}

	protected ShapeRendererFillable()
	{
		//IL_0012: Expected O, but got I
		//IL_0042: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
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
