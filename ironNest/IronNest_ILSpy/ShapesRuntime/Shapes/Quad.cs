using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Quad : ShapeRenderer
{
	public enum QuadColorMode
	{
		Single,
		Horizontal,
		Vertical,
		PerCorner
	}

	private QuadColorMode colorMode;

	private Vector3 a;

	private Vector3 b;

	private Vector3 c;

	private Vector3 d;

	private bool autoSetD;

	private Color colorB;

	private Color colorC;

	private Color colorD;

	// C# has no syntax for parameterized property 'Item'.
	public unsafe Vector3 get_Item(int index)
	{
		//IL_00ff: Expected F4, but got O
		//IL_00fa: Expected native int or pointer, but got O
		//IL_0114: Expected F4, but got I
		//IL_010f: Expected native int or pointer, but got O
		//IL_002b: Expected O, but got I4
		//IL_00d6: Expected F4, but got O
		//IL_00d1: Expected native int or pointer, but got O
		//IL_00eb: Expected F4, but got I
		//IL_00e6: Expected native int or pointer, but got O
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_00ad: Expected F4, but got O
		//IL_00a8: Expected native int or pointer, but got O
		//IL_00c2: Expected F4, but got I
		//IL_00bd: Expected native int or pointer, but got O
		//IL_0084: Expected F4, but got O
		//IL_007f: Expected native int or pointer, but got O
		//IL_0099: Expected F4, but got I
		//IL_0094: Expected native int or pointer, but got O
		bool flag = index == 0;
		Vector3 vector = default(Vector3);
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 == 1)
					{
						((Vector3*)(nint)vector)->x = (float)d;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+D0]");
						((Vector3*)(nint)vector)->z = 0f;
						return vector;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string message = $"Quad only has four vertices, 0 to 3, you tried to access element {arg}";
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				((Vector3*)(nint)vector)->x = (float)c;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+C4]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
			((Vector3*)(nint)vector)->x = (float)b;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+B8]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		((Vector3*)(nint)vector)->x = (float)a;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+AC]");
		((Vector3*)(nint)vector)->z = 0f;
		return vector;
	}

	public unsafe void set_Item(int index, Vector3 value)
	{
		//IL_002b: Expected O, but got I4
		//IL_0221: Expected O, but got F4
		//IL_0174: Expected O, but got Ref
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_01fa: Expected O, but got F4
		//IL_01a7: Expected O, but got F4
		//IL_01c0: Expected O, but got Ref
		//IL_0142: Expected O, but got F4
		//IL_015b: Expected O, but got Ref
		bool flag = index == 0;
		float num = default(float);
		int prop;
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string message = $"Quad only has four vertices, 0 to 3, you tried to set element {arg}";
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex;
					}
					if (autoSetD)
					{
						GameObject context = base.gameObject;
						Debug.LogWarning("tried to set D when auto-set is enabled, you might want to turn off auto-set on this object", context);
					}
					else
					{
						d = (Vector3)value.x;
						_ = value.z;
						SetVector3Now(ShapesMaterialUtils.propD, (Vector3)(&num));
					}
					return;
				}
				c = (Vector3)value.x;
				_ = value.z;
				SetVector3Now(ShapesMaterialUtils.propC, (Vector3)(&num));
				if (!autoSetD)
				{
					return;
				}
				goto IL_00ad;
			}
			prop = ShapesMaterialUtils.propB;
			b = (Vector3)value.x;
			_ = value.z;
		}
		else
		{
			prop = ShapesMaterialUtils.propA;
			a = (Vector3)value.x;
			_ = value.z;
		}
		SetVector3Now(prop, (Vector3)(&num));
		if (!autoSetD)
		{
			return;
		}
		goto IL_00ad;
		IL_00ad:
		AutoSetD();
	}

	public QuadColorMode ColorMode
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

	public unsafe Vector3 A
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)a;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+AC]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_0025: Expected O, but got F4
			//IL_003e: Expected O, but got Ref
			a = (Vector3)value.x;
			_ = value.z;
			object obj = default(object);
			SetVector3Now(ShapesMaterialUtils.propA, (Vector3)(&obj));
			if (autoSetD)
			{
				AutoSetD();
			}
		}
	}

	public unsafe Vector3 B
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)b;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+B8]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_0025: Expected O, but got F4
			//IL_003e: Expected O, but got Ref
			b = (Vector3)value.x;
			_ = value.z;
			object obj = default(object);
			SetVector3Now(ShapesMaterialUtils.propB, (Vector3)(&obj));
			if (autoSetD)
			{
				AutoSetD();
			}
		}
	}

	public unsafe Vector3 C
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)c;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+C4]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_0025: Expected O, but got F4
			//IL_003e: Expected O, but got Ref
			c = (Vector3)value.x;
			_ = value.z;
			object obj = default(object);
			SetVector3Now(ShapesMaterialUtils.propC, (Vector3)(&obj));
			if (autoSetD)
			{
				AutoSetD();
			}
		}
	}

	public unsafe Vector3 D
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)d;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+D0]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_0059: Expected O, but got F4
			//IL_0072: Expected O, but got Ref
			if (autoSetD)
			{
				GameObject context = base.gameObject;
				Debug.LogWarning("tried to set D when auto-set is enabled, you might want to turn off auto-set on this object", context);
			}
			else
			{
				d = (Vector3)value.x;
				_ = value.z;
				object obj = default(object);
				SetVector3Now(ShapesMaterialUtils.propD, (Vector3)(&obj));
			}
		}
	}

	public bool IsUsingAutoD
	{
		get
		{
			return autoSetD;
		}
		set
		{
			autoSetD = value;
			AutoSetD();
		}
	}

	public unsafe Vector3 DAuto
	{
		get
		{
			//IL_001d: Expected O, but got I
			//IL_003c: Expected native int or pointer, but got O
			//IL_0049: Expected native int or pointer, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+C4]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+B8]");
			object obj = num - 0;
			float num2 = (float)obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+AC]");
			float z = num2 + 0f;
			Vector3 vector = default(Vector3);
			float x = default(float);
			((Vector3*)(nint)vector)->x = x;
			((Vector3*)(nint)vector)->z = z;
			return vector;
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
			colorB = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorB, (Color)(&num));
			colorC = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorC, (Color)(&num));
			colorD = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorD, (Color)(&num));
			ApplyProperties();
		}
	}

	public unsafe Color ColorLeft
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
			colorB = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorB, (Color)(&num));
			ApplyProperties();
		}
	}

	public unsafe Color ColorTop
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorB;
			return color;
		}
		set
		{
			//IL_001a: Expected O, but got F4
			//IL_0029: Expected O, but got Ref
			//IL_0038: Expected O, but got F4
			//IL_0047: Expected O, but got Ref
			colorB = (Color)value.r;
			float num = default(float);
			SetColor(ShapesMaterialUtils.propColorB, (Color)(&num));
			colorC = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorC, (Color)(&num));
			ApplyProperties();
		}
	}

	public unsafe Color ColorRight
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorC;
			return color;
		}
		set
		{
			//IL_001a: Expected O, but got F4
			//IL_0029: Expected O, but got Ref
			//IL_0038: Expected O, but got F4
			//IL_0047: Expected O, but got Ref
			colorC = (Color)value.r;
			float num = default(float);
			SetColor(ShapesMaterialUtils.propColorC, (Color)(&num));
			colorD = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorD, (Color)(&num));
			ApplyProperties();
		}
	}

	public unsafe Color ColorBottom
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorD;
			return color;
		}
		set
		{
			//IL_001a: Expected O, but got F4
			//IL_0029: Expected O, but got Ref
			//IL_0038: Expected O, but got F4
			//IL_0047: Expected O, but got Ref
			colorD = (Color)value.r;
			float num = default(float);
			SetColor(ShapesMaterialUtils.propColorD, (Color)(&num));
			color = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColor, (Color)(&num));
			ApplyProperties();
		}
	}

	public unsafe Color ColorA
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

	public unsafe Color ColorB
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorB;
			return color;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0028: Expected O, but got Ref
			colorB = (Color)value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColorB, (Color)(&obj));
			ApplyProperties();
		}
	}

	public unsafe Color ColorC
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorC;
			return color;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0028: Expected O, but got Ref
			colorC = (Color)value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColorC, (Color)(&obj));
			ApplyProperties();
		}
	}

	public unsafe Color ColorD
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)colorD;
			return color;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0028: Expected O, but got Ref
			colorD = (Color)value.r;
			object obj = default(object);
			SetColor(ShapesMaterialUtils.propColorD, (Color)(&obj));
			ApplyProperties();
		}
	}

	internal override bool HasDetailLevels => false;

	internal override bool HasScaleModes => false;

	public unsafe Vector3 GetQuadVertex(int index)
	{
		//IL_00ff: Expected F4, but got O
		//IL_00fa: Expected native int or pointer, but got O
		//IL_0114: Expected F4, but got I
		//IL_010f: Expected native int or pointer, but got O
		//IL_002b: Expected O, but got I4
		//IL_00d6: Expected F4, but got O
		//IL_00d1: Expected native int or pointer, but got O
		//IL_00eb: Expected F4, but got I
		//IL_00e6: Expected native int or pointer, but got O
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_00ad: Expected F4, but got O
		//IL_00a8: Expected native int or pointer, but got O
		//IL_00c2: Expected F4, but got I
		//IL_00bd: Expected native int or pointer, but got O
		//IL_0084: Expected F4, but got O
		//IL_007f: Expected native int or pointer, but got O
		//IL_0099: Expected F4, but got I
		//IL_0094: Expected native int or pointer, but got O
		bool flag = index == 0;
		Vector3 vector = default(Vector3);
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 == 1)
					{
						((Vector3*)(nint)vector)->x = (float)d;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+D0]");
						((Vector3*)(nint)vector)->z = 0f;
						return vector;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string message = $"Quad only has four vertices, 0 to 3, you tried to access element {arg}";
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				((Vector3*)(nint)vector)->x = (float)c;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+C4]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
			((Vector3*)(nint)vector)->x = (float)b;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+B8]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		((Vector3*)(nint)vector)->x = (float)a;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+AC]");
		((Vector3*)(nint)vector)->z = 0f;
		return vector;
	}

	public unsafe Vector3 SetQuadVertex(int index, Vector3 value)
	{
		//IL_000d: Expected native int or pointer, but got O
		//IL_001f: Expected native int or pointer, but got O
		//IL_0032: Expected O, but got Ref
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = value.x;
		((Vector3*)(nint)vector)->z = value.z;
		object obj = default(object);
		this.set_Item(index, (Vector3)(&obj));
		return vector;
	}

	public unsafe Color GetQuadColor(int index)
	{
		//IL_00c8: Expected native int or pointer, but got O
		//IL_002b: Expected O, but got I4
		//IL_00ac: Expected F4, but got O
		//IL_00a7: Expected native int or pointer, but got O
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0098: Expected F4, but got O
		//IL_0093: Expected native int or pointer, but got O
		//IL_0084: Expected F4, but got O
		//IL_007f: Expected native int or pointer, but got O
		bool flag = index == 0;
		Color color = default(Color);
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 == 1)
					{
						((Color*)(nint)color)->r = (float)colorD;
						return color;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string message = $"Quad only has four vertices, 0 to 3, you tried to access element {arg}";
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				((Color*)(nint)color)->r = (float)colorC;
				return color;
			}
			((Color*)(nint)color)->r = (float)colorB;
			return color;
		}
		((Color*)(nint)color)->r = Color.r;
		return color;
	}

	public unsafe void SetQuadColor(int index, Color color)
	{
		//IL_008e: Expected O, but got Ref
		//IL_002b: Expected O, but got I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_015d: Expected O, but got F4
		//IL_00fa: Expected O, but got Ref
		//IL_0140: Expected O, but got F4
		//IL_0123: Expected O, but got F4
		bool flag = index == 0;
		float num = default(float);
		if (!flag)
		{
			object obj = index - 1;
			int prop;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string message = $"Quad only has four vertices, 0 to 3, you tried to set element {arg}";
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex;
					}
					prop = ShapesMaterialUtils.propColorD;
					colorD = (Color)color.r;
				}
				else
				{
					prop = ShapesMaterialUtils.propColorC;
					colorC = (Color)color.r;
				}
			}
			else
			{
				prop = ShapesMaterialUtils.propColorB;
				colorB = (Color)color.r;
			}
			SetColor(prop, (Color)(&num));
			ApplyProperties();
		}
		else
		{
			Color = (Color)(&num);
		}
	}

	private unsafe void AutoSetD()
	{
		//IL_0019: Expected O, but got Ref
		object obj = default(object);
		SetVector3(ShapesMaterialUtils.propD, (Vector3)(&obj));
	}

	private void CheckAutoSetD()
	{
		if (autoSetD)
		{
			AutoSetD();
		}
	}

	private protected override void SetAllMaterialProperties()
	{
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Expected O, but got Unknown
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Expected O, but got Unknown
		//IL_003b: Expected O, but got I4
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0156: Expected O, but got I4
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Expected O, but got Unknown
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Expected O, but got Unknown
		//IL_0078: Expected O, but got I4
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Expected O, but got Unknown
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected O, but got Unknown
		//IL_0297: Expected F4, but got O
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Expected O, but got Unknown
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_0233: Expected F4, but got O
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		object obj = default(object);
		Vector3 value = (Vector3)(obj - 16);
		_ = a;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Quad)+AC]");
		_ = 0;
		SetVector3(ShapesMaterialUtils.propA, value);
		Vector3 value2 = (Vector3)(obj - 16);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Quad)+B8]");
		_ = 0;
		_ = b;
		SetVector3(ShapesMaterialUtils.propB, value2);
		Vector3 value3 = (Vector3)(obj - 16);
		Vector3 vector = c;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Quad)+C4]");
		_ = 0;
		_ = c;
		SetVector3(ShapesMaterialUtils.propC, value3);
		if (autoSetD)
		{
			AutoSetD();
			object obj2 = 0;
			int num = 0;
		}
		else
		{
			value3 = (Vector3)(obj - 16);
			vector = d;
			_ = d;
			int num = ShapesMaterialUtils.propD;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Quad)+D0]");
			_ = 0;
			SetVector3(ShapesMaterialUtils.propD, value3);
			object obj2 = 0;
		}
		bool flag = colorMode == QuadColorMode.Single;
		int propColorD;
		if (!flag)
		{
			object obj3 = colorMode - 1;
			if (!flag)
			{
				object obj4 = obj3 - 1;
				if (!flag)
				{
					if ((nint)obj4 != 1)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException();
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex;
					}
					Color value4 = (Color)(obj - 16);
					_ = colorB;
					SetColor(ShapesMaterialUtils.propColorB, value4);
					Color color = colorC;
				}
				else
				{
					Color value5 = (Color)(obj - 16);
					_ = colorD;
					SetColor(ShapesMaterialUtils.propColor, value5);
					Color value6 = (Color)(obj - 16);
					_ = colorB;
					SetColor(ShapesMaterialUtils.propColorB, value6);
					Color color = colorB;
				}
				Color value7 = (Color)(obj - 16);
				SetColor(ShapesMaterialUtils.propColorC, value7);
				float num2 = (float)colorD;
			}
			else
			{
				Color color2 = Color;
				Color value8 = (Color)(obj - 16);
				_ = color2.r;
				SetColor(ShapesMaterialUtils.propColorB, value8);
				Color value9 = (Color)(obj - 16);
				_ = colorC;
				SetColor(ShapesMaterialUtils.propColorC, value9);
				float num2 = (float)colorC;
			}
			propColorD = ShapesMaterialUtils.propColorD;
		}
		else
		{
			Color color3 = Color;
			Color value10 = (Color)(obj - 16);
			_ = color3.r;
			SetColor(ShapesMaterialUtils.propColorB, value10);
			Color color4 = Color;
			Color value11 = (Color)(obj - 16);
			_ = color4.r;
			SetColor(ShapesMaterialUtils.propColorC, value11);
			float num2 = Color.r;
			propColorD = ShapesMaterialUtils.propColorD;
		}
		Color value12 = (Color)(obj - 16);
		SetColor(propColorD, value12);
	}

	private protected override Mesh GetInitialMeshAsset()
	{
		Mesh[] quadMesh = ShapesMeshUtils.QuadMesh;
		if (quadMesh.Length > 0)
		{
			return quadMesh[0];
		}
		return (Mesh)(object)new IndexOutOfRangeException();
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_000f: Expected O, but got I4
		//IL_0037: Expected I, but got O
		//IL_006a: Expected I, but got O
		//IL_007a: Expected O, but got I
		//IL_0098: Expected I, but got O
		ShapesMaterials matQuad = ShapesMaterialUtils.matQuad;
		bool flag = ShapesMaterialUtils.matQuad == null;
		Material[] array = mats;
		if (!flag)
		{
			array = (Material[])base.blendMode;
			ShapesMaterials shapesMaterials = (ShapesMaterials)(object)ShapesMaterialUtils.matQuad.get_Item(base.blendMode);
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
					matQuad = shapesMaterials;
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
		//IL_0094: Expected O, but got I
		//IL_005a: Expected O, but got I
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0230: Expected O, but got I
		//IL_0105: Expected O, but got I
		//IL_011a: Expected O, but got I
		//IL_04e1: Expected O, but got I
		//IL_018e: Expected O, but got I
		//IL_037a: Expected O, but got F4
		//IL_0375: Expected native int or pointer, but got O
		//IL_03b1: Expected O, but got F4
		//IL_03ac: Expected native int or pointer, but got O
		//IL_01a3: Expected O, but got I
		Vector3 vector;
		Vector3 vector2 = default(Vector3);
		object obj3;
		if (autoSetD)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+C4]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+B8]");
			object obj = num - 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+AC]");
			object obj2 = obj + 0;
			vector = vector2;
			obj3 = obj2;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+D0]");
			obj3 = 0;
			vector = d;
		}
		Vector3 vector3 = a;
		Vector3 vector4 = b;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector3) <= System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector4);
		Vector3 vector5 = a;
		if (!flag)
		{
			vector5 = b;
		}
		Vector3 vector6 = vector5;
		Vector3 vector7 = c;
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector6) > System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector7))
		{
			vector5 = c;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector5) > System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector))
		{
			vector5 = vector;
		}
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector2) <= System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector2);
		Vector3 vector8 = vector2;
		if (!flag2)
		{
			vector8 = vector2;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector8) > System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector2))
		{
			vector8 = vector2;
		}
		Vector3 vector9 = default(Vector3);
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector8) > System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector9))
		{
			vector8 = vector9;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+AC]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+AC]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+B8]");
		if (num2 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+B8]");
			obj4 = 0;
		}
		object obj5 = obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+C4]");
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+C4]");
			obj4 = 0;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
		{
			obj4 = obj3;
		}
		Vector3 vector10 = a;
		Vector3 vector11 = b;
		bool flag3 = System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector10) >= System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector11);
		Vector3 vector12 = a;
		if (!flag3)
		{
			vector12 = b;
		}
		Vector3 vector13 = vector12;
		Vector3 vector14 = c;
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector13) < System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector14))
		{
			vector12 = c;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector12) < System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector))
		{
			vector12 = vector;
		}
		bool flag4 = System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector2) >= System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector2);
		Vector3 vector15 = vector2;
		if (!flag4)
		{
			vector15 = vector2;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector15) < System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector2))
		{
			vector15 = vector2;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector15) < System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector9))
		{
			vector15 = vector9;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+AC]");
		object obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+AC]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+B8]");
		if (num3 < 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+B8]");
			obj6 = 0;
		}
		object obj7 = obj6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+C4]");
		if ((nint)obj7 < 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Quad)+C4]");
			obj6 = 0;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
		{
			obj6 = obj3;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181061150");
		object obj8 = vector12 + vector5;
		object obj9 = vector15 + vector8;
		object obj10 = obj6 + obj4;
		object obj11 = default(object);
		float num4 = (float)obj11 * 0.5f;
		float num5 = (float)obj8 * 0.5f;
		float num6 = (float)obj9 * 0.5f;
		Bounds bounds = default(Bounds);
		((Bounds*)(nint)bounds)->m_Center = (Vector3)num5;
		float num7 = (float)obj10 * 0.5f;
		float num8 = (float)vector2 * 0.5f;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ rax_v3+8]");
		float num9 = 0f * 0.5f;
		return bounds;
	}

	public Quad()
	{
		//IL_003a: Expected O, but got I
		//IL_0057: Expected O, but got I
		//IL_0074: Expected O, but got I
		//IL_0091: Expected O, but got I
		//IL_00a0: Expected I4, but got I8
		Vector3 vector = default(Vector3);
		a = vector;
		b = vector;
		c = vector;
		d = vector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		colorB = (Color)0;
		meshOutOfDate = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		colorC = (Color)0;
		base.blendMode = ShapesBlendMode.Transparent;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		colorD = (Color)0;
		detailLevel = DetailLevel.Medium;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		base.renderQueue = -1;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		base.zTest = CompareFunction.LessEqual;
		base.colorMask = ColorWriteMask.All;
		base.stencilComp = CompareFunction.Always;
		base.stencilReadMask = 255;
		base.shouldUpdateMaterialPropertiesInEditor = true;
		((MonoBehaviour)this)._002Ector();
	}
}
