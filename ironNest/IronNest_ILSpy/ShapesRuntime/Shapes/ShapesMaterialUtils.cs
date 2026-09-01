using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

internal static class ShapesMaterialUtils
{
	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass80_0
	{
		public DiscType type;
	}

	public static readonly int propZTest;

	public static readonly int propZTestTMP;

	public static readonly int propZOffsetFactor;

	public static readonly int propZOffsetUnits;

	public static readonly int propColorMask;

	public static readonly int propStencilComp;

	public static readonly int propStencilOpPass;

	public static readonly int propStencilID;

	public static readonly int propStencilIDTMP;

	public static readonly int propStencilReadMask;

	public static readonly int propStencilWriteMask;

	public static readonly int propBaseColor;

	public static readonly int propColor;

	public static readonly int propScaleMode;

	public static readonly int propColorEnd;

	public static readonly int propColorOuterStart;

	public static readonly int propColorInnerEnd;

	public static readonly int propColorOuterEnd;

	public static readonly int propColorB;

	public static readonly int propColorC;

	public static readonly int propColorD;

	public static readonly int propPointStart;

	public static readonly int propPointEnd;

	public static readonly int propA;

	public static readonly int propB;

	public static readonly int propC;

	public static readonly int propD;

	public static readonly int propRect;

	public static readonly int propRadius;

	public static readonly int propCornerRadii;

	public static readonly int propLength;

	public static readonly int propBorder;

	public static readonly int propSides;

	public static readonly int propAng;

	public static readonly int propRoundness;

	public static readonly int propAngStart;

	public static readonly int propAngEnd;

	public static readonly int propRoundCaps;

	public static readonly int propThickness;

	public static readonly int propThicknessSpace;

	public static readonly int propRadiusSpace;

	public static readonly int propDashSize;

	public static readonly int propDashOffset;

	public static readonly int propDashSpacing;

	public static readonly int propDashType;

	public static readonly int propDashSpace;

	public static readonly int propDashSnap;

	public static readonly int propDashShapeModifier;

	public static readonly int propSize;

	public static readonly int propSizeSpace;

	public static readonly int propAlignment;

	public static readonly int propFillType;

	public static readonly int propFillStart;

	public static readonly int propFillEnd;

	public static readonly int propFillSpace;

	public static readonly int propMainTex;

	public static readonly int propUvs;

	public static readonly int propScreenParams;

	private static readonly ShapesMaterials matDisc;

	private static readonly ShapesMaterials matCircleSector;

	private static readonly ShapesMaterials matRing;

	private static readonly ShapesMaterials matRingSector;

	private static readonly ShapesMaterials matRectSimple;

	private static readonly ShapesMaterials matRectRounded;

	private static readonly ShapesMaterials matRectBorder;

	private static readonly ShapesMaterials matRectBorderRounded;

	public static readonly ShapesMaterials matTriangle;

	public static readonly ShapesMaterials matQuad;

	public static readonly ShapesMaterials matSphere;

	public static readonly ShapesMaterials matCone;

	public static readonly ShapesMaterials matCuboid;

	public static readonly ShapesMaterials matTorus;

	public static readonly ShapesMaterials matPolygon;

	public static readonly ShapesMaterials matRegularPolygon;

	public static readonly ShapesMaterials matTexture;

	private static readonly ShapesMaterials[] matsLine;

	private static readonly ShapesMaterials[] matsLine3D;

	private static readonly ShapesMaterials[] matsPolyline;

	private static readonly ShapesMaterials[] matsPolylineJoin;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ShapesMaterials GetDiscMaterial(bool hollow, bool sector)
	{
		if (!hollow)
		{
			if (sector)
			{
				return matCircleSector;
			}
			return matDisc;
		}
		if (sector)
		{
			return matRingSector;
		}
		return matRing;
	}

	public static ShapesMaterials GetDiscMaterial(DiscType type)
	{
		//IL_0013: Expected O, but got I4
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		bool flag = type == DiscType.Disc;
		if (!flag)
		{
			object obj = type - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string message = $"Failed to get disc material, invalid enum index of {arg} ";
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex;
					}
					return matRingSector;
				}
				return matRing;
			}
			return matCircleSector;
		}
		return matDisc;
	}

	public static ShapesMaterials GetRectMaterial(bool hollow, bool rounded)
	{
		if (!hollow)
		{
			if (rounded)
			{
				return matRectRounded;
			}
			return matRectSimple;
		}
		if (rounded)
		{
			return matRectBorderRounded;
		}
		return matRectBorder;
	}

	public static ShapesMaterials GetRectMaterial(Rectangle.RectangleType type)
	{
		//IL_0013: Expected O, but got I4
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		bool flag = type == Rectangle.RectangleType.HardSolid;
		if (!flag)
		{
			object obj = type - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						return null;
					}
					return matRectBorderRounded;
				}
				return matRectBorder;
			}
			return matRectRounded;
		}
		return matRectSimple;
	}

	public static ShapesMaterials GetPolylineMat(PolylineJoins join)
	{
		ShapesMaterials[] array = matsPolyline;
		if ((int)join < array.Length)
		{
			return array[(int)join];
		}
		return (ShapesMaterials)(object)new IndexOutOfRangeException();
	}

	public static ShapesMaterials GetPolylineJoinsMat(PolylineJoins join)
	{
		ShapesMaterials[] array = matsPolylineJoin;
		if ((int)join < array.Length)
		{
			return array[(int)join];
		}
		return (ShapesMaterials)(object)new IndexOutOfRangeException();
	}

	public static ShapesMaterials GetLineMat(LineGeometry geometry, LineEndCap cap)
	{
		ShapesMaterials[] array;
		if (geometry > LineGeometry.Billboard)
		{
			if (geometry != LineGeometry.Volumetric3D)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				object actualValue = default(object);
				ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("geometry", actualValue, null);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			if (matsLine3D != null)
			{
				array = matsLine3D;
				goto IL_000a;
			}
		}
		else
		{
			array = matsLine;
			if (matsLine != null)
			{
				goto IL_000a;
			}
		}
		return (ShapesMaterials)(object)new NullReferenceException();
		IL_000a:
		return array[(int)cap];
	}

	static ShapesMaterialUtils()
	{
		//IL_05e8: Expected O, but got I4
		//IL_05f1: Expected O, but got I4
		//IL_05ff: Expected I, but got O
		//IL_0674: Expected O, but got I4
		//IL_067d: Expected O, but got I4
		//IL_068b: Expected I, but got O
		//IL_0700: Expected O, but got I4
		//IL_0709: Expected O, but got I4
		//IL_0717: Expected I, but got O
		//IL_07d3: Expected O, but got I4
		//IL_07dc: Expected O, but got I4
		//IL_07ea: Expected I, but got O
		//IL_085f: Expected O, but got I4
		//IL_0868: Expected O, but got I4
		//IL_0876: Expected I, but got O
		//IL_08eb: Expected O, but got I4
		//IL_08f4: Expected O, but got I4
		//IL_0902: Expected I, but got O
		//IL_0b5c: Expected O, but got I4
		//IL_0b6d: Expected I, but got O
		//IL_0c35: Expected O, but got I4
		//IL_0c3e: Expected O, but got I4
		//IL_0c4c: Expected I, but got O
		//IL_0ba0: Expected I, but got O
		//IL_0bb0: Expected O, but got I
		//IL_0bda: Expected O, but got I4
		//IL_0be2: Expected I, but got O
		//IL_0d3f: Expected O, but got I4
		//IL_0d48: Expected O, but got I4
		//IL_0d56: Expected I, but got O
		//IL_0caa: Expected I, but got O
		//IL_0cba: Expected O, but got I
		//IL_0ce4: Expected O, but got I4
		//IL_0db4: Expected I, but got O
		//IL_0dc4: Expected O, but got I
		//IL_0dee: Expected O, but got I4
		//IL_0e78: Expected O, but got I4
		//IL_0e89: Expected I, but got O
		//IL_0f51: Expected O, but got I4
		//IL_0f5a: Expected O, but got I4
		//IL_0f68: Expected I, but got O
		//IL_0ebc: Expected I, but got O
		//IL_0ecc: Expected O, but got I
		//IL_0ef6: Expected O, but got I4
		//IL_105b: Expected O, but got I4
		//IL_1064: Expected O, but got I4
		//IL_1072: Expected I, but got O
		//IL_0fc6: Expected I, but got O
		//IL_0fd6: Expected O, but got I
		//IL_1000: Expected O, but got I4
		//IL_10d0: Expected I, but got O
		//IL_10e0: Expected O, but got I
		//IL_110a: Expected O, but got I4
		//IL_1194: Expected O, but got I4
		//IL_11a5: Expected I, but got O
		//IL_126d: Expected O, but got I4
		//IL_1276: Expected O, but got I4
		//IL_1284: Expected I, but got O
		//IL_11d8: Expected I, but got O
		//IL_11e8: Expected O, but got I
		//IL_1212: Expected O, but got I4
		//IL_1377: Expected O, but got I4
		//IL_1380: Expected O, but got I4
		//IL_138e: Expected I, but got O
		//IL_12e2: Expected I, but got O
		//IL_12f2: Expected O, but got I
		//IL_131c: Expected O, but got I4
		//IL_1481: Expected O, but got I4
		//IL_148a: Expected O, but got I4
		//IL_1498: Expected I, but got O
		//IL_13ec: Expected I, but got O
		//IL_13fc: Expected O, but got I
		//IL_1426: Expected O, but got I4
		//IL_14f6: Expected I, but got O
		//IL_1506: Expected O, but got I
		//IL_1530: Expected O, but got I4
		//IL_15a7: Expected O, but got I4
		//IL_15b0: Expected O, but got I4
		//IL_15be: Expected I, but got O
		//IL_1617: Expected O, but got I4
		//IL_1628: Expected I, but got O
		//IL_16f0: Expected O, but got I4
		//IL_16f9: Expected O, but got I4
		//IL_1707: Expected I, but got O
		//IL_165b: Expected I, but got O
		//IL_166b: Expected O, but got I
		//IL_1695: Expected O, but got I4
		//IL_1812: Expected O, but got I4
		//IL_181b: Expected O, but got I4
		//IL_1829: Expected I, but got O
		//IL_177d: Expected I, but got O
		//IL_178d: Expected O, but got I
		//IL_17b7: Expected O, but got I4
		//IL_1934: Expected O, but got I4
		//IL_193d: Expected O, but got I4
		//IL_194b: Expected I, but got O
		//IL_189f: Expected I, but got O
		//IL_18af: Expected O, but got I
		//IL_18d9: Expected O, but got I4
		//IL_19c1: Expected I, but got O
		//IL_19d1: Expected O, but got I
		//IL_19fb: Expected O, but got I4
		int num = Shader.PropertyToID("_ZTest");
		propZTest = num;
		int num2 = Shader.PropertyToID("unity_GUIZTestMode");
		propZTestTMP = num2;
		int num3 = Shader.PropertyToID("_ZOffsetFactor");
		propZOffsetFactor = num3;
		int num4 = Shader.PropertyToID("_ZOffsetUnits");
		propZOffsetUnits = num4;
		int num5 = Shader.PropertyToID("_ColorMask");
		propColorMask = num5;
		int num6 = Shader.PropertyToID("_StencilComp");
		propStencilComp = num6;
		int num7 = Shader.PropertyToID("_StencilOpPass");
		propStencilOpPass = num7;
		int num8 = Shader.PropertyToID("_StencilID");
		propStencilID = num8;
		int num9 = Shader.PropertyToID("_Stencil");
		propStencilIDTMP = num9;
		int num10 = Shader.PropertyToID("_StencilReadMask");
		propStencilReadMask = num10;
		int num11 = Shader.PropertyToID("_StencilWriteMask");
		propStencilWriteMask = num11;
		int num12 = Shader.PropertyToID("_BaseColor");
		propBaseColor = num12;
		int num13 = Shader.PropertyToID("_Color");
		propColor = num13;
		int num14 = Shader.PropertyToID("_ScaleMode");
		propScaleMode = num14;
		int num15 = Shader.PropertyToID("_ColorEnd");
		propColorEnd = num15;
		int num16 = Shader.PropertyToID("_ColorOuterStart");
		propColorOuterStart = num16;
		int num17 = Shader.PropertyToID("_ColorInnerEnd");
		propColorInnerEnd = num17;
		int num18 = Shader.PropertyToID("_ColorOuterEnd");
		propColorOuterEnd = num18;
		int num19 = Shader.PropertyToID("_ColorB");
		propColorB = num19;
		int num20 = Shader.PropertyToID("_ColorC");
		propColorC = num20;
		int num21 = Shader.PropertyToID("_ColorD");
		propColorD = num21;
		int num22 = Shader.PropertyToID("_PointStart");
		propPointStart = num22;
		int num23 = Shader.PropertyToID("_PointEnd");
		propPointEnd = num23;
		int num24 = Shader.PropertyToID("_A");
		propA = num24;
		int num25 = Shader.PropertyToID("_B");
		propB = num25;
		int num26 = Shader.PropertyToID("_C");
		propC = num26;
		int num27 = Shader.PropertyToID("_D");
		propD = num27;
		int num28 = Shader.PropertyToID("_Rect");
		propRect = num28;
		int num29 = Shader.PropertyToID("_Radius");
		propRadius = num29;
		int num30 = Shader.PropertyToID("_CornerRadii");
		propCornerRadii = num30;
		int num31 = Shader.PropertyToID("_Length");
		propLength = num31;
		int num32 = Shader.PropertyToID("_Hollow");
		propBorder = num32;
		int num33 = Shader.PropertyToID("_Sides");
		propSides = num33;
		int num34 = Shader.PropertyToID("_Angle");
		propAng = num34;
		int num35 = Shader.PropertyToID("_Roundness");
		propRoundness = num35;
		int num36 = Shader.PropertyToID("_AngleStart");
		propAngStart = num36;
		int num37 = Shader.PropertyToID("_AngleEnd");
		propAngEnd = num37;
		int num38 = Shader.PropertyToID("_RoundCaps");
		propRoundCaps = num38;
		int num39 = Shader.PropertyToID("_Thickness");
		propThickness = num39;
		int num40 = Shader.PropertyToID("_ThicknessSpace");
		propThicknessSpace = num40;
		int num41 = Shader.PropertyToID("_RadiusSpace");
		propRadiusSpace = num41;
		int num42 = Shader.PropertyToID("_DashSize");
		propDashSize = num42;
		int num43 = Shader.PropertyToID("_DashOffset");
		propDashOffset = num43;
		int num44 = Shader.PropertyToID("_DashSpacing");
		propDashSpacing = num44;
		int num45 = Shader.PropertyToID("_DashType");
		propDashType = num45;
		int num46 = Shader.PropertyToID("_DashSpace");
		propDashSpace = num46;
		int num47 = Shader.PropertyToID("_DashSnap");
		propDashSnap = num47;
		int num48 = Shader.PropertyToID("_DashShapeModifier");
		propDashShapeModifier = num48;
		int num49 = Shader.PropertyToID("_Size");
		propSize = num49;
		int num50 = Shader.PropertyToID("_SizeSpace");
		propSizeSpace = num50;
		int num51 = Shader.PropertyToID("_Alignment");
		propAlignment = num51;
		int num52 = Shader.PropertyToID("_FillType");
		propFillType = num52;
		int num53 = Shader.PropertyToID("_FillStart");
		propFillStart = num53;
		int num54 = Shader.PropertyToID("_FillEnd");
		propFillEnd = num54;
		int num55 = Shader.PropertyToID("_FillSpace");
		propFillSpace = num55;
		int num56 = Shader.PropertyToID("_MainTex");
		propMainTex = num56;
		int num57 = Shader.PropertyToID("_Uvs");
		propUvs = num57;
		int num58 = Shader.PropertyToID("_ScreenParams");
		propScreenParams = num58;
		string[] array = Array.Empty<string>();
		ShapesMaterials shapesMaterials = new ShapesMaterials("Disc", array);
		matDisc = shapesMaterials;
		string[] array2 = new string[1];
		bool flag = array2 == null;
		string[] array3 = array;
		object obj = 0;
		string text = (string)1;
		nint num59 = (nint)typeof(string[]);
		if (!flag)
		{
			array2[0] = "SECTOR";
			ShapesMaterials shapesMaterials2 = new ShapesMaterials("Disc", array2);
			matCircleSector = shapesMaterials2;
			string[] array4 = new string[1];
			bool flag2 = array4 == null;
			array3 = array2;
			obj = 0;
			text = (string)1;
			num59 = (nint)typeof(string[]);
			if (!flag2)
			{
				array4[0] = "INNER_RADIUS";
				ShapesMaterials shapesMaterials3 = new ShapesMaterials("Disc", array4);
				matRing = shapesMaterials3;
				string[] array5 = new string[2];
				bool flag3 = array5 == null;
				array3 = array4;
				obj = 0;
				text = (string)2;
				num59 = (nint)typeof(string[]);
				if (!flag3)
				{
					array5[0] = "INNER_RADIUS";
					array5[1] = "SECTOR";
					ShapesMaterials shapesMaterials4 = new ShapesMaterials("Disc", array5);
					matRingSector = shapesMaterials4;
					string[] array6 = Array.Empty<string>();
					ShapesMaterials shapesMaterials5 = new ShapesMaterials("Rect", array6);
					matRectSimple = shapesMaterials5;
					string[] array7 = new string[1];
					bool flag4 = array7 == null;
					array3 = array6;
					obj = 0;
					text = (string)1;
					num59 = (nint)typeof(string[]);
					if (!flag4)
					{
						array7[0] = "CORNER_RADIUS";
						ShapesMaterials shapesMaterials6 = new ShapesMaterials("Rect", array7);
						matRectRounded = shapesMaterials6;
						string[] array8 = new string[1];
						bool flag5 = array8 == null;
						array3 = array7;
						obj = 0;
						text = (string)1;
						num59 = (nint)typeof(string[]);
						if (!flag5)
						{
							array8[0] = "BORDERED";
							ShapesMaterials shapesMaterials7 = new ShapesMaterials("Rect", array8);
							matRectBorder = shapesMaterials7;
							string[] array9 = new string[2];
							bool flag6 = array9 == null;
							array3 = array8;
							obj = 0;
							text = (string)2;
							num59 = (nint)typeof(string[]);
							if (!flag6)
							{
								array9[0] = "CORNER_RADIUS";
								array9[1] = "BORDERED";
								ShapesMaterials shapesMaterials8 = new ShapesMaterials("Rect", array9);
								matRectBorderRounded = shapesMaterials8;
								string[] keywords = Array.Empty<string>();
								ShapesMaterials shapesMaterials9 = new ShapesMaterials("Triangle", keywords);
								matTriangle = shapesMaterials9;
								string[] keywords2 = Array.Empty<string>();
								ShapesMaterials shapesMaterials10 = new ShapesMaterials("Quad", keywords2);
								matQuad = shapesMaterials10;
								string[] keywords3 = Array.Empty<string>();
								ShapesMaterials shapesMaterials11 = new ShapesMaterials("Sphere", keywords3);
								matSphere = shapesMaterials11;
								string[] keywords4 = Array.Empty<string>();
								ShapesMaterials shapesMaterials12 = new ShapesMaterials("Cone", keywords4);
								matCone = shapesMaterials12;
								string[] keywords5 = Array.Empty<string>();
								ShapesMaterials shapesMaterials13 = new ShapesMaterials("Cuboid", keywords5);
								matCuboid = shapesMaterials13;
								string[] keywords6 = Array.Empty<string>();
								ShapesMaterials shapesMaterials14 = new ShapesMaterials("Torus", keywords6);
								matTorus = shapesMaterials14;
								string[] keywords7 = Array.Empty<string>();
								ShapesMaterials shapesMaterials15 = new ShapesMaterials("Polygon", keywords7);
								matPolygon = shapesMaterials15;
								string[] keywords8 = Array.Empty<string>();
								ShapesMaterials shapesMaterials16 = new ShapesMaterials("Regular Polygon", keywords8);
								matRegularPolygon = shapesMaterials16;
								string[] keywords9 = Array.Empty<string>();
								ShapesMaterials shapesMaterials17 = new ShapesMaterials("Texture", keywords9);
								matTexture = shapesMaterials17;
								ShapesMaterials[] array10 = new ShapesMaterials[3];
								string[] array11 = Array.Empty<string>();
								ShapesMaterials shapesMaterials18 = new ShapesMaterials("Line 2D", array11);
								bool flag7 = array10 == null;
								array3 = array11;
								obj = 0;
								text = "Line 2D";
								num59 = (nint)shapesMaterials18;
								if (!flag7)
								{
									if (shapesMaterials18 != null)
									{
										nint num60 = (nint)array10;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2163 @ rdx_v313 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
										text = (string)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj2 = default(object);
										bool flag8 = obj2 == null;
										array3 = array11;
										obj = 0;
										num59 = (nint)shapesMaterials18;
										if (flag8)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											ShapesMaterials shapesMaterials19 = default(ShapesMaterials);
											throw shapesMaterials19;
										}
									}
									array10[0] = shapesMaterials18;
									string[] array12 = new string[1];
									bool flag9 = array12 == null;
									array3 = array11;
									obj = 0;
									text = (string)1;
									num59 = (nint)typeof(string[]);
									if (!flag9)
									{
										array12[0] = "CAP_SQUARE";
										ShapesMaterials shapesMaterials20 = new ShapesMaterials("Line 2D", array12);
										if (shapesMaterials20 != null)
										{
											nint num61 = (nint)array10;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2195 @ rdx_v311 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
											object obj3 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj4 = default(object);
											bool flag10 = obj4 == null;
											array3 = array12;
											obj = 0;
											ShapesMaterials shapesMaterials21 = shapesMaterials20;
											if (flag10)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
												ShapesMaterials shapesMaterials22 = default(ShapesMaterials);
												throw shapesMaterials22;
											}
										}
										array10[1] = shapesMaterials20;
										string[] array13 = new string[1];
										bool flag11 = array13 == null;
										array3 = array12;
										obj = 0;
										text = (string)1;
										num59 = (nint)typeof(string[]);
										if (!flag11)
										{
											array13[0] = "CAP_ROUND";
											ShapesMaterials shapesMaterials23 = new ShapesMaterials("Line 2D", array13);
											if (shapesMaterials23 != null)
											{
												nint num62 = (nint)array10;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2228 @ rdx_v309 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
												object obj5 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
												object obj6 = default(object);
												bool flag12 = obj6 == null;
												array3 = array13;
												obj = 0;
												ShapesMaterials shapesMaterials24 = shapesMaterials23;
												if (flag12)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
													ShapesMaterials shapesMaterials25 = default(ShapesMaterials);
													throw shapesMaterials25;
												}
											}
											array10[2] = shapesMaterials23;
											matsLine = array10;
											ShapesMaterials[] array14 = new ShapesMaterials[3];
											string[] array15 = Array.Empty<string>();
											ShapesMaterials shapesMaterials26 = new ShapesMaterials("Line 3D", array15);
											bool flag13 = array14 == null;
											array3 = array15;
											obj = 0;
											text = "Line 3D";
											num59 = (nint)shapesMaterials26;
											if (!flag13)
											{
												if (shapesMaterials26 != null)
												{
													nint num63 = (nint)array14;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2267 @ rdx_v307 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
													object obj7 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
													object obj8 = default(object);
													bool flag14 = obj8 == null;
													array3 = array15;
													obj = 0;
													ShapesMaterials shapesMaterials27 = shapesMaterials26;
													if (flag14)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
														ShapesMaterials shapesMaterials28 = default(ShapesMaterials);
														throw shapesMaterials28;
													}
												}
												array14[0] = shapesMaterials26;
												string[] array16 = new string[1];
												bool flag15 = array16 == null;
												array3 = array15;
												obj = 0;
												text = (string)1;
												num59 = (nint)typeof(string[]);
												if (!flag15)
												{
													array16[0] = "CAP_SQUARE";
													ShapesMaterials shapesMaterials29 = new ShapesMaterials("Line 3D", array16);
													if (shapesMaterials29 != null)
													{
														nint num64 = (nint)array14;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2299 @ rdx_v305 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
														object obj9 = 0;
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
														object obj10 = default(object);
														bool flag16 = obj10 == null;
														array3 = array16;
														obj = 0;
														ShapesMaterials shapesMaterials30 = shapesMaterials29;
														if (flag16)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
															ShapesMaterials shapesMaterials31 = default(ShapesMaterials);
															throw shapesMaterials31;
														}
													}
													array14[1] = shapesMaterials29;
													string[] array17 = new string[1];
													bool flag17 = array17 == null;
													array3 = array16;
													obj = 0;
													text = (string)1;
													num59 = (nint)typeof(string[]);
													if (!flag17)
													{
														array17[0] = "CAP_ROUND";
														ShapesMaterials shapesMaterials32 = new ShapesMaterials("Line 3D", array17);
														if (shapesMaterials32 != null)
														{
															nint num65 = (nint)array14;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2332 @ rdx_v303 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
															object obj11 = 0;
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
															object obj12 = default(object);
															bool flag18 = obj12 == null;
															array3 = array17;
															obj = 0;
															ShapesMaterials shapesMaterials33 = shapesMaterials32;
															if (flag18)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																ShapesMaterials shapesMaterials34 = default(ShapesMaterials);
																throw shapesMaterials34;
															}
														}
														array14[2] = shapesMaterials32;
														matsLine3D = array14;
														ShapesMaterials[] array18 = new ShapesMaterials[4];
														string[] array19 = Array.Empty<string>();
														ShapesMaterials shapesMaterials35 = new ShapesMaterials("Polyline 2D", array19);
														bool flag19 = array18 == null;
														array3 = array19;
														obj = 0;
														text = "Polyline 2D";
														num59 = (nint)shapesMaterials35;
														if (!flag19)
														{
															if (shapesMaterials35 != null)
															{
																nint num66 = (nint)array18;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2371 @ rdx_v301 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
																object obj13 = 0;
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																object obj14 = default(object);
																bool flag20 = obj14 == null;
																array3 = array19;
																obj = 0;
																ShapesMaterials shapesMaterials36 = shapesMaterials35;
																if (flag20)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																	ShapesMaterials shapesMaterials37 = default(ShapesMaterials);
																	throw shapesMaterials37;
																}
															}
															array18[0] = shapesMaterials35;
															string[] array20 = new string[1];
															bool flag21 = array20 == null;
															array3 = array19;
															obj = 0;
															text = (string)1;
															num59 = (nint)typeof(string[]);
															if (!flag21)
															{
																array20[0] = "JOIN_MITER";
																ShapesMaterials shapesMaterials38 = new ShapesMaterials("Polyline 2D", array20);
																if (shapesMaterials38 != null)
																{
																	nint num67 = (nint)array18;
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2403 @ rdx_v299 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
																	object obj15 = 0;
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																	object obj16 = default(object);
																	bool flag22 = obj16 == null;
																	array3 = array20;
																	obj = 0;
																	ShapesMaterials shapesMaterials39 = shapesMaterials38;
																	if (flag22)
																	{
																		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																		ShapesMaterials shapesMaterials40 = default(ShapesMaterials);
																		throw shapesMaterials40;
																	}
																}
																array18[1] = shapesMaterials38;
																string[] array21 = new string[1];
																bool flag23 = array21 == null;
																array3 = array20;
																obj = 0;
																text = (string)1;
																num59 = (nint)typeof(string[]);
																if (!flag23)
																{
																	array21[0] = "JOIN_ROUND";
																	ShapesMaterials shapesMaterials41 = new ShapesMaterials("Polyline 2D", array21);
																	if (shapesMaterials41 != null)
																	{
																		nint num68 = (nint)array18;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2436 @ rdx_v297 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
																		object obj17 = 0;
																		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																		object obj18 = default(object);
																		bool flag24 = obj18 == null;
																		array3 = array21;
																		obj = 0;
																		ShapesMaterials shapesMaterials42 = shapesMaterials41;
																		if (flag24)
																		{
																			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																			ShapesMaterials shapesMaterials43 = default(ShapesMaterials);
																			throw shapesMaterials43;
																		}
																	}
																	array18[2] = shapesMaterials41;
																	string[] array22 = new string[1];
																	bool flag25 = array22 == null;
																	array3 = array21;
																	obj = 0;
																	text = (string)1;
																	num59 = (nint)typeof(string[]);
																	if (!flag25)
																	{
																		array22[0] = "JOIN_BEVEL";
																		ShapesMaterials shapesMaterials44 = new ShapesMaterials("Polyline 2D", array22);
																		if (shapesMaterials44 != null)
																		{
																			nint num69 = (nint)array18;
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2469 @ rdx_v295 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
																			object obj19 = 0;
																			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																			object obj20 = default(object);
																			bool flag26 = obj20 == null;
																			array3 = array22;
																			obj = 0;
																			ShapesMaterials shapesMaterials45 = shapesMaterials44;
																			if (flag26)
																			{
																				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																				ShapesMaterials shapesMaterials46 = default(ShapesMaterials);
																				throw shapesMaterials46;
																			}
																		}
																		array18[3] = shapesMaterials44;
																		matsPolyline = array18;
																		ShapesMaterials[] array23 = new ShapesMaterials[4];
																		string[] array24 = new string[1];
																		bool flag27 = array24 == null;
																		array3 = array22;
																		obj = 0;
																		text = (string)1;
																		num59 = (nint)typeof(string[]);
																		if (!flag27)
																		{
																			array24[0] = "IS_JOIN_MESH";
																			ShapesMaterials shapesMaterials47 = new ShapesMaterials("Polyline 2D", array24);
																			bool flag28 = array23 == null;
																			array3 = array24;
																			obj = 0;
																			text = "Polyline 2D";
																			num59 = (nint)shapesMaterials47;
																			if (!flag28)
																			{
																				if (shapesMaterials47 != null)
																				{
																					nint num70 = (nint)array23;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2509 @ rdx_v293 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
																					object obj21 = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																					object obj22 = default(object);
																					bool flag29 = obj22 == null;
																					array3 = array24;
																					obj = 0;
																					ShapesMaterials shapesMaterials48 = shapesMaterials47;
																					if (flag29)
																					{
																						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																						ShapesMaterials shapesMaterials49 = default(ShapesMaterials);
																						throw shapesMaterials49;
																					}
																				}
																				array23[0] = shapesMaterials47;
																				string[] array25 = new string[2];
																				bool flag30 = array25 == null;
																				array3 = array24;
																				obj = 0;
																				text = (string)2;
																				num59 = (nint)typeof(string[]);
																				if (!flag30)
																				{
																					array25[0] = "IS_JOIN_MESH";
																					array25[1] = "JOIN_MITER";
																					ShapesMaterials shapesMaterials50 = new ShapesMaterials("Polyline 2D", array25);
																					if (shapesMaterials50 != null)
																					{
																						nint num71 = (nint)array23;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2543 @ rdx_v291 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
																						object obj23 = 0;
																						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																						object obj24 = default(object);
																						bool flag31 = obj24 == null;
																						array3 = array25;
																						obj = 0;
																						ShapesMaterials shapesMaterials51 = shapesMaterials50;
																						if (flag31)
																						{
																							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																							ShapesMaterials shapesMaterials52 = default(ShapesMaterials);
																							throw shapesMaterials52;
																						}
																					}
																					array23[1] = shapesMaterials50;
																					string[] array26 = new string[2];
																					bool flag32 = array26 == null;
																					array3 = array25;
																					obj = 0;
																					text = (string)2;
																					num59 = (nint)typeof(string[]);
																					if (!flag32)
																					{
																						array26[0] = "IS_JOIN_MESH";
																						array26[1] = "JOIN_ROUND";
																						ShapesMaterials shapesMaterials53 = new ShapesMaterials("Polyline 2D", array26);
																						if (shapesMaterials53 != null)
																						{
																							nint num72 = (nint)array23;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2578 @ rdx_v289 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
																							object obj25 = 0;
																							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																							object obj26 = default(object);
																							bool flag33 = obj26 == null;
																							array3 = array26;
																							obj = 0;
																							ShapesMaterials shapesMaterials54 = shapesMaterials53;
																							if (flag33)
																							{
																								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																								ShapesMaterials shapesMaterials55 = default(ShapesMaterials);
																								throw shapesMaterials55;
																							}
																						}
																						array23[2] = shapesMaterials53;
																						string[] array27 = new string[2];
																						bool flag34 = array27 == null;
																						array3 = array26;
																						obj = 0;
																						text = (string)2;
																						num59 = (nint)typeof(string[]);
																						if (!flag34)
																						{
																							array27[0] = "IS_JOIN_MESH";
																							array27[1] = "JOIN_BEVEL";
																							ShapesMaterials shapesMaterials56 = new ShapesMaterials("Polyline 2D", array27);
																							if (shapesMaterials56 != null)
																							{
																								nint num73 = (nint)array23;
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2613 @ rdx_v287 (Il2CppClass<Shapes.ShapesMaterials[]>)+40]");
																								object obj27 = 0;
																								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																								object obj28 = default(object);
																								bool flag35 = obj28 == null;
																								array3 = array27;
																								obj = 0;
																								ShapesMaterials shapesMaterials57 = shapesMaterials56;
																								if (flag35)
																								{
																									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																									object obj29 = default(object);
																									throw obj29;
																								}
																							}
																							array23[3] = shapesMaterials56;
																							matsPolylineJoin = array23;
																							return;
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	internal static ShapesMaterials _003CGetDiscMaterial_003Eg__Load_007C80_0(ref _003C_003Ec__DisplayClass80_0 P_0)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		bool flag = (object)P_0 == null;
		if (!flag)
		{
			object obj = P_0 - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string message = $"Failed to get disc material, invalid enum index of {arg} ";
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						IndexOutOfRangeException ex = new IndexOutOfRangeException(message);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex;
					}
					return matRingSector;
				}
				return matRing;
			}
			return matCircleSector;
		}
		return matDisc;
	}
}
