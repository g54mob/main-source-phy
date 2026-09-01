using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Triangle : ShapeRenderer, IDashable
{
	public enum TriangleColorMode
	{
		Single,
		PerCorner
	}

	private TriangleColorMode colorMode;

	private Vector3 a;

	private Vector3 b;

	private Vector3 c;

	private bool border;

	private float thickness;

	private ThicknessSpace thicknessSpace;

	private float roundness;

	private Color colorB;

	private Color colorC;

	private bool matchDashSpacingToSize;

	private bool dashed;

	private DashStyle dashStyle;

	// C# has no syntax for parameterized property 'Item'.
	public unsafe Vector3 get_Item(int index)
	{
		//IL_00ba: Expected F4, but got O
		//IL_00b5: Expected native int or pointer, but got O
		//IL_00cf: Expected F4, but got I
		//IL_00ca: Expected native int or pointer, but got O
		//IL_002b: Expected O, but got I4
		//IL_0091: Expected F4, but got O
		//IL_008c: Expected native int or pointer, but got O
		//IL_00a6: Expected F4, but got I
		//IL_00a1: Expected native int or pointer, but got O
		//IL_0068: Expected F4, but got O
		//IL_0063: Expected native int or pointer, but got O
		//IL_007d: Expected F4, but got I
		//IL_0078: Expected native int or pointer, but got O
		bool flag = index == 0;
		Vector3 vector = default(Vector3);
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				if ((nint)obj == 1)
				{
					((Vector3*)(nint)vector)->x = (float)c;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+C4]");
					((Vector3*)(nint)vector)->z = 0f;
					return vector;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"Triangle only has four vertices, 0 to 2, you tried to access element {arg}";
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			((Vector3*)(nint)vector)->x = (float)b;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+B8]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		((Vector3*)(nint)vector)->x = (float)a;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+AC]");
		((Vector3*)(nint)vector)->z = 0f;
		return vector;
	}

	public unsafe void set_Item(int index, Vector3 value)
	{
		//IL_002b: Expected O, but got I4
		//IL_0144: Expected O, but got F4
		//IL_00d3: Expected O, but got Ref
		//IL_011d: Expected O, but got F4
		//IL_00f6: Expected O, but got F4
		bool flag = index == 0;
		int prop;
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string message = $"Triangle only has four vertices, 0 to 2, you tried to set element {arg}";
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				prop = ShapesMaterialUtils.propC;
				c = (Vector3)value.x;
				_ = value.z;
			}
			else
			{
				prop = ShapesMaterialUtils.propB;
				b = (Vector3)value.x;
				_ = value.z;
			}
		}
		else
		{
			prop = ShapesMaterialUtils.propA;
			a = (Vector3)value.x;
			_ = value.z;
		}
		float num = default(float);
		SetVector3Now(prop, (Vector3)(&num));
	}

	public TriangleColorMode ColorMode
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+AC]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0032: Expected O, but got Ref
			a = (Vector3)value.x;
			_ = value.z;
			object obj = default(object);
			SetVector3Now(ShapesMaterialUtils.propA, (Vector3)(&obj));
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+B8]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0032: Expected O, but got Ref
			b = (Vector3)value.x;
			_ = value.z;
			object obj = default(object);
			SetVector3Now(ShapesMaterialUtils.propB, (Vector3)(&obj));
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+C4]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		set
		{
			//IL_0019: Expected O, but got F4
			//IL_0032: Expected O, but got Ref
			c = (Vector3)value.x;
			_ = value.z;
			object obj = default(object);
			SetVector3Now(ShapesMaterialUtils.propC, (Vector3)(&obj));
		}
	}

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
			//IL_0020: Expected O, but got F4
			//IL_002f: Expected O, but got Ref
			//IL_003e: Expected O, but got F4
			//IL_004d: Expected O, but got Ref
			//IL_005c: Expected O, but got F4
			//IL_006b: Expected O, but got Ref
			color = (Color)value.r;
			float num = default(float);
			SetColor(ShapesMaterialUtils.propColor, (Color)(&num));
			colorB = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorB, (Color)(&num));
			colorC = (Color)value.r;
			SetColor(ShapesMaterialUtils.propColorC, (Color)(&num));
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Triangle)+108]");
			return 0f;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_0076: Expected O, but got Ref
			DashStyle dashStyle = (DashStyle)(this + 252);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Triangle)+108]");
				return 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Triangle)+10C]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Triangle)+110]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Triangle)+100]");
			return DashSpace.Meters;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			SetInt(ShapesMaterialUtils.propDashSpace, (int)value);
			DashStyle dashStyle = (DashStyle)(this + 252);
			float netAbsoluteSize = ((DashStyle*)dashStyle)->GetNetAbsoluteSize(dashed, thickness);
			SetFloatNow(ShapesMaterialUtils.propDashSize, netAbsoluteSize);
		}
	}

	public DashSnapping DashSnap
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Triangle)+104]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.Triangle)+114]");
			return 0f;
		}
		set
		{
			SetFloatNow(ShapesMaterialUtils.propDashShapeModifier, value);
		}
	}

	public unsafe Vector3 GetTriangleVertex(int index)
	{
		//IL_00ba: Expected F4, but got O
		//IL_00b5: Expected native int or pointer, but got O
		//IL_00cf: Expected F4, but got I
		//IL_00ca: Expected native int or pointer, but got O
		//IL_002b: Expected O, but got I4
		//IL_0091: Expected F4, but got O
		//IL_008c: Expected native int or pointer, but got O
		//IL_00a6: Expected F4, but got I
		//IL_00a1: Expected native int or pointer, but got O
		//IL_0068: Expected F4, but got O
		//IL_0063: Expected native int or pointer, but got O
		//IL_007d: Expected F4, but got I
		//IL_0078: Expected native int or pointer, but got O
		bool flag = index == 0;
		Vector3 vector = default(Vector3);
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				if ((nint)obj == 1)
				{
					((Vector3*)(nint)vector)->x = (float)c;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+C4]");
					((Vector3*)(nint)vector)->z = 0f;
					return vector;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"Triangle only has four vertices, 0 to 2, you tried to access element {arg}";
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			((Vector3*)(nint)vector)->x = (float)b;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+B8]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		((Vector3*)(nint)vector)->x = (float)a;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+AC]");
		((Vector3*)(nint)vector)->z = 0f;
		return vector;
	}

	public unsafe Vector3 SetTriangleVertex(int index, Vector3 value)
	{
		//IL_000d: Expected native int or pointer, but got O
		//IL_001f: Expected native int or pointer, but got O
		//IL_004f: Expected O, but got I4
		//IL_0170: Expected O, but got F4
		//IL_00fb: Expected O, but got Ref
		//IL_0149: Expected O, but got F4
		//IL_0122: Expected O, but got F4
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = value.x;
		((Vector3*)(nint)vector)->z = value.z;
		bool flag = index == 0;
		int prop;
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string message = $"Triangle only has four vertices, 0 to 2, you tried to set element {arg}";
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				prop = ShapesMaterialUtils.propC;
				c = (Vector3)value.x;
				_ = value.z;
			}
			else
			{
				prop = ShapesMaterialUtils.propB;
				b = (Vector3)value.x;
				_ = value.z;
			}
		}
		else
		{
			prop = ShapesMaterialUtils.propA;
			a = (Vector3)value.x;
			_ = value.z;
		}
		float num = default(float);
		SetVector3Now(prop, (Vector3)(&num));
		return vector;
	}

	public unsafe Color GetTriangleColor(int index)
	{
		//IL_0098: Expected native int or pointer, but got O
		//IL_002b: Expected O, but got I4
		//IL_007c: Expected F4, but got O
		//IL_0077: Expected native int or pointer, but got O
		//IL_0068: Expected F4, but got O
		//IL_0063: Expected native int or pointer, but got O
		bool flag = index == 0;
		Color color = default(Color);
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				if ((nint)obj == 1)
				{
					((Color*)(nint)color)->r = (float)colorC;
					return color;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"Triangle only has four vertices, 0 to 2, you tried to access element {arg}";
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			((Color*)(nint)color)->r = (float)colorB;
			return color;
		}
		((Color*)(nint)color)->r = Color.r;
		return color;
	}

	public unsafe void SetTriangleColor(int index, Color color)
	{
		//IL_006d: Expected O, but got Ref
		//IL_002b: Expected O, but got I4
		//IL_011a: Expected O, but got F4
		//IL_00d9: Expected O, but got Ref
		//IL_00fd: Expected O, but got F4
		bool flag = index == 0;
		float num = default(float);
		if (!flag)
		{
			object obj = index - 1;
			int prop;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string message = $"Triangle only has four vertices, 0 to 3, you tried to set element {arg}";
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				prop = ShapesMaterialUtils.propColorC;
				colorC = (Color)color.r;
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

	private protected unsafe override void SetAllMaterialProperties()
	{
		//IL_011f: Expected O, but got Ref
		//IL_012f: Expected O, but got Ref
		//IL_013f: Expected O, but got Ref
		//IL_00be: Expected O, but got Ref
		//IL_00a0: Expected O, but got Ref
		//IL_0083: Expected O, but got Ref
		//IL_010a: Expected F4, but got I4
		//IL_006e: Expected O, but got Ref
		Vector3 vector = default(Vector3);
		SetVector3(ShapesMaterialUtils.propA, (Vector3)(&vector));
		SetVector3(ShapesMaterialUtils.propB, (Vector3)(&vector));
		SetVector3(ShapesMaterialUtils.propC, (Vector3)(&vector));
		int propColorC;
		if (colorMode != TriangleColorMode.Single)
		{
			SetColor(ShapesMaterialUtils.propColorB, (Color)(&vector));
			propColorC = ShapesMaterialUtils.propColorC;
		}
		else
		{
			Color color = Color;
			SetColor(ShapesMaterialUtils.propColorB, (Color)(&vector));
			Color color2 = Color;
			propColorC = ShapesMaterialUtils.propColorC;
		}
		float num = default(float);
		SetColor(propColorC, (Color)(&num));
		SetFloat(ShapesMaterialUtils.propRoundness, roundness);
		SetFloat(ShapesMaterialUtils.propThickness, thickness);
		SetFloat(ShapesMaterialUtils.propThicknessSpace, (float)thicknessSpace);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D51B20");
		float value = default(float);
		SetFloat(ShapesMaterialUtils.propBorder, value);
		float num2 = default(float);
		bool setType = default(bool);
		bool now = default(bool);
		SetAllDashValues((DashStyle)(&num), dashed, matchDashSpacingToSize, num2, setType, now);
	}

	private protected override Mesh GetInitialMeshAsset()
	{
		Mesh[] triangleMesh = ShapesMeshUtils.TriangleMesh;
		if (triangleMesh.Length > 0)
		{
			return triangleMesh[0];
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
		ShapesMaterials matTriangle = ShapesMaterialUtils.matTriangle;
		bool flag = ShapesMaterialUtils.matTriangle == null;
		Material[] array = mats;
		if (!flag)
		{
			array = (Material[])base.blendMode;
			ShapesMaterials shapesMaterials = (ShapesMaterials)(object)ShapesMaterialUtils.matTriangle.get_Item(base.blendMode);
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
					matTriangle = shapesMaterials;
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
		//IL_0214: Expected O, but got I
		//IL_0093: Expected O, but got I
		//IL_00a8: Expected O, but got I
		//IL_02a7: Expected O, but got I
		//IL_0356: Expected O, but got F4
		//IL_0351: Expected native int or pointer, but got O
		//IL_038d: Expected O, but got F4
		//IL_0388: Expected native int or pointer, but got O
		//IL_00f5: Expected O, but got I
		//IL_010a: Expected O, but got I
		Vector3 vector = a;
		Vector3 vector2 = b;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector) <= System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector2);
		Vector3 vector3 = a;
		if (!flag)
		{
			vector3 = b;
		}
		Vector3 vector4 = vector3;
		Vector3 vector5 = c;
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector4) > System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector5))
		{
			vector3 = c;
		}
		object obj = default(object);
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		object obj2 = obj;
		if (!flag2)
		{
			obj2 = obj;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			obj2 = obj;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+AC]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+AC]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+B8]");
		if (num > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+B8]");
			obj3 = 0;
		}
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+C4]");
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+C4]");
			obj3 = 0;
		}
		Vector3 vector6 = a;
		Vector3 vector7 = b;
		bool flag3 = System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector6) >= System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector7);
		Vector3 vector8 = a;
		if (!flag3)
		{
			vector8 = b;
		}
		Vector3 vector9 = vector8;
		Vector3 vector10 = c;
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector9) < System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector10))
		{
			vector8 = c;
		}
		bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		object obj5 = obj;
		if (!flag4)
		{
			obj5 = obj;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			obj5 = obj;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+AC]");
		object obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+AC]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+B8]");
		if (num2 < 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+B8]");
			obj6 = 0;
		}
		object obj7 = obj6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+C4]");
		if ((nint)obj7 < 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.Triangle)+C4]");
			obj6 = 0;
		}
		object obj8 = obj6 - obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181061150");
		object obj9 = vector8 + vector3;
		object obj10 = obj5 + obj2;
		object obj11 = obj6 + obj3;
		object obj12 = default(object);
		float num3 = (float)obj12 * 0.5f;
		float num4 = (float)obj9 * 0.5f;
		float num5 = (float)obj10 * 0.5f;
		Bounds bounds = default(Bounds);
		((Bounds*)(nint)bounds)->m_Center = (Vector3)num4;
		float num6 = (float)obj11 * 0.5f;
		float num7 = (float)obj * 0.5f;
		((Bounds*)(nint)bounds)->m_Extents = (Vector3)num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v254 @ rax_v2+8]");
		float num8 = 0f * 0.5f;
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

	public Triangle()
	{
		//IL_0108: Expected I, but got O
		//IL_0018: Expected I, but got O
		//IL_0143: Expected I, but got O
		//IL_0188: Expected O, but got I
		//IL_01a5: Expected O, but got I
		//IL_01b3: Expected I, but got O
		//IL_00a2: Expected O, but got I
		//IL_00bc: Expected I4, but got I8
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		a = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v6 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		b = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		nint num5 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v9 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num6 = 0;
		c = Vector3.rightVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rcx_v6 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
		_ = 0;
		thickness = 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		colorB = (Color)0;
		matchDashSpacingToSize = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		colorC = (Color)0;
		nint num7 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v101 @ rax_v12 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num8 = 0;
		dashStyle = DashStyle.defaultDashStyleRing;
		meshOutOfDate = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rax_v13 (Il2CppStaticFields<Shapes.DashStyle>)+2C]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rax_v13 (Il2CppStaticFields<Shapes.DashStyle>)+34]");
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
