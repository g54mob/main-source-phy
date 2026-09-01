using System;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

namespace Shapes;

public class IMColorPickerRenderer : ImmediateModeShapeDrawer
{
	public float hue;

	public float saturation = 1f;

	public float value = 1f;

	public float hueStripThickness;

	public float outline;

	public float quadMargin;

	public float hueDotScale;

	public Vector2 labelSize;

	private PolylinePath hueStripPath;

	public unsafe Color CurrentPureColor
	{
		get
		{
			//IL_002a: Expected native int or pointer, but got O
			Color color = default(Color);
			bool hdr = default(bool);
			((Color*)(nint)color)->r = Color.HSVToRGB(hue, 1f, 1f, hdr).r;
			return color;
		}
	}

	public unsafe Color CurrentColor
	{
		get
		{
			//IL_002c: Expected native int or pointer, but got O
			Color color = default(Color);
			bool hdr = default(bool);
			((Color*)(nint)color)->r = Color.HSVToRGB(hue, saturation, value, hdr).r;
			return color;
		}
	}

	public float QuadScale
	{
		get
		{
			//IL_003d: Invalid comparison between I4 and F4
			float num = hueStripThickness * 0.5f;
			float num2 = 1f - num;
			float num3 = num2 - quadMargin;
			if (!(0f > 2f))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm1\"");
				return num3 / 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
			return num3 / 2f;
		}
	}

	public unsafe Rect QuadRect
	{
		get
		{
			//IL_010f: Expected native int or pointer, but got O
			//IL_0139: Expected I, but got O
			//IL_0174: Invalid comparison between I4 and F4
			//IL_0018: Expected F4, but got I4
			//IL_0096: Expected native int or pointer, but got O
			//IL_00b3: Expected native int or pointer, but got O
			//IL_00e0: Expected native int or pointer, but got O
			//IL_00fc: Expected native int or pointer, but got O
			Rect rect = default(Rect);
			((Rect*)(nint)rect)->m_XMin = 0f;
			float num = hueStripThickness * 0.5f;
			nint num2 = (nint)typeof(Vector2);
			float num3 = 1f - num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rax_v3 (Il2CppClass<UnityEngine.Vector2>)+B8]");
			nint num4 = 0;
			float num5 = num3 - quadMargin;
			float num6;
			if (!(0f > 2f))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm1\"");
				num6 = 0f;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
				num6 = 2f;
			}
			float num7 = num5 / num6;
			float num8 = num7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rsi_v1 (Il2CppStaticFields<UnityEngine.Vector2>)+C]");
			float num9 = num8 * 0f;
			float num10 = num7 * (float)Vector2.oneVector;
			float num11 = num9 + num9;
			float num12 = (((Rect*)(nint)rect)->m_Width = num10 + num10) * 0.5f;
			((Rect*)(nint)rect)->m_Height = num11;
			float num13 = num11 * 0.5f;
			float xMin = 0f - num12;
			((Rect*)(nint)rect)->m_XMin = xMin;
			object obj = default(object);
			float yMin = (float)obj - num13;
			((Rect*)(nint)rect)->m_YMin = yMin;
			return rect;
		}
	}

	public float HueStripRadiusOuter
	{
		get
		{
			float num = hueStripThickness * 0.5f;
			float num2 = num + 1f;
			return num2 + outline;
		}
	}

	public float HueStripRadiusInner
	{
		get
		{
			float num = hueStripThickness * 0.5f;
			float num2 = 1f - num;
			return num2 - outline;
		}
	}

	public static Vector2 HueToVector(float hue)
	{
		float num = hue * ((float)Math.PI * 2f);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		Vector2 result = default(Vector2);
		return result;
	}

	public static float VectorToHue(Vector2 v)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
		object obj = default(object);
		float num = (float)obj / ((float)Math.PI * 2f);
		float num2 = MathF.Floor(num);
		return num - num2;
	}

	public unsafe override void OnEnable()
	{
		//IL_004b: Expected O, but got I4
		//IL_0115: Expected O, but got Ref
		//IL_0122: Expected O, but got Ref
		//IL_0089: Expected O, but got Ref
		//IL_0089: Expected O, but got Ref
		//IL_0098: Expected O, but got Ref
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00be: Expected O, but got I4
		//IL_00c7: Expected O, but got I4
		//IL_00cf: Expected O, but got Ref
		base.OnEnable();
		PolylinePath polylinePath = (hueStripPath = new PolylinePath());
		object obj = 0;
		PolylinePath p = polylinePath;
		bool hdr = default(bool);
		object obj2 = default(object);
		object obj3 = default(object);
		float num = default(float);
		PolylinePoint polylinePoint2 = default(PolylinePoint);
		bool flag;
		do
		{
			float h = (float)obj / 100f;
			Color color = Color.HSVToRGB(h, 1f, 1f, hdr);
			((PointPath<PolylinePoint>)(&obj2)).AddPoint((PolylinePoint)p);
			((PointPath<PolylinePoint>)(&obj2)).AddPoint((PolylinePoint)p);
			PolylinePoint polylinePoint = new PolylinePoint((Vector3)(&obj3), (Color)(&num));
			hueStripPath.AddPoint((PolylinePoint)(&polylinePoint2));
			obj++;
			flag = (nint)obj < 100;
			polylinePoint = (PolylinePoint)0;
			polylinePoint2 = (PolylinePoint)0;
			p = (PolylinePath)(&polylinePoint2);
		}
		while (flag);
	}

	public override void OnDisable()
	{
		base.OnDisable();
		hueStripPath.Dispose();
	}

	public unsafe override void DrawShapes(Camera cam)
	{
		//IL_009c: Expected I, but got O
		//IL_00ba: Expected O, but got F4
		//IL_05cd: Expected O, but got Ref
		//IL_0602: Expected I, but got O
		//IL_0615: Expected I, but got O
		//IL_0122: Expected I, but got O
		//IL_0145: Expected I, but got O
		//IL_019d: Invalid comparison between I4 and F4
		//IL_01c4: Expected F4, but got I4
		//IL_064c: Expected I, but got O
		//IL_07da: Expected O, but got I4
		//IL_07da: Expected O, but got Ref
		//IL_07da: Expected O, but got Ref
		//IL_022f: Expected I, but got O
		//IL_0324: Expected I4, but got F4
		//IL_0352: Expected O, but got I4
		//IL_0352: Expected O, but got F4
		//IL_0352: Expected O, but got Ref
		//IL_0352: Expected O, but got Ref
		//IL_0352: Expected O, but got Ref
		//IL_0352: Expected O, but got Ref
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Expected O, but got Unknown
		//IL_038a: Expected I4, but got F4
		//IL_069f: Expected I, but got O
		//IL_06b2: Expected I, but got O
		//IL_03cd: Expected I, but got O
		//IL_0412: Expected O, but got I
		//IL_0412: Expected O, but got Ref
		//IL_0469: Expected O, but got Ref
		//IL_04b7: Expected I4, but got F4
		//IL_04ca: Expected O, but got Ref
		//IL_04f8: Invalid comparison between I4 and F4
		//IL_06e0: Invalid comparison between I4 and F4
		//IL_06fd: Expected O, but got Ref
		//IL_0748: Expected I4, but got F4
		//IL_0564: Expected O, but got Ref
		DrawCommand drawCommand = Draw.Command(cam);
		Transform transform = base.transform;
		if ((object)transform != null)
		{
			Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
			nint num = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ rax_v13 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num2 = 0;
			Draw.matrix = (Matrix4x4)localToWorldMatrix.m00;
			_ = localToWorldMatrix.m01;
			_ = localToWorldMatrix.m02;
			_ = localToWorldMatrix.m03;
			object obj = default(object);
			DiscColors discColors = (Color)(&obj);
			float num3 = outline + hueStripThickness;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9B590");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9BD60");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9B6A0");
			MatrixStack.Pop();
			nint num4 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v459 @ rax_v27 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num5 = 0;
			_ = 0;
			nint num6 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v492 @ rax_v31 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num7 = 0;
			_ = 0;
			nint num8 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v555 @ rax_v37 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num9 = 0;
			nint num10 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v587 @ rax_v40 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num11 = 0;
			PolylinePath path = hueStripPath;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v556 @ rcx_v28 (Il2CppStaticFields<Shapes.Draw>)+1A8]");
			nint num12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v588 @ rcx_v31 (Il2CppStaticFields<Shapes.Draw>)+1AC]");
			float num13 = default(float);
			ThicknessSpace thicknessSpace = default(ThicknessSpace);
			Color color = default(Color);
			Draw.Polyline_Internal(path, true, (PolylineGeometry)num12, PolylineJoins.Simple, num13, thicknessSpace, color);
			if (!(0f > 2f))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm1\"");
				float num14 = 0f;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
				float num14 = 2f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9B590");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9BD60");
			nint num15 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v804 @ rax_v60 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num16 = 0;
			Vector2 size = default(Vector2);
			Rect rect = RectPivotExtensions.GetRect(RectPivot.Center, size);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v806 @ rax_v61 (Il2CppStaticFields<Shapes.Draw>)+108]");
			float num17 = default(float);
			Vector2 vector = default(Vector2);
			Draw.Rectangle_Internal(ShapesBlendMode.Opaque, false, (Rect)(&num17), (Color)(&vector), num13, (Vector4)thicknessSpace);
			MatrixStack.Pop();
			MatrixStack matrixScope = Draw.MatrixScope;
			nint num18 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v948 @ rax_v76 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
			Draw.matrix = Draw.matrix;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v949 @ rcx_v60 (Il2CppStaticFields<Shapes.Draw>)+8C]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v949 @ rcx_v60 (Il2CppStaticFields<Shapes.Draw>)+90]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v949 @ rcx_v60 (Il2CppStaticFields<Shapes.Draw>)+98]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v949 @ rcx_v60 (Il2CppStaticFields<Shapes.Draw>)+9C]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v949 @ rcx_v60 (Il2CppStaticFields<Shapes.Draw>)+A0]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm5\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v949 @ rcx_v60 (Il2CppStaticFields<Shapes.Draw>)+A8]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm5\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v949 @ rcx_v60 (Il2CppStaticFields<Shapes.Draw>)+AC]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm5\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v949 @ rcx_v60 (Il2CppStaticFields<Shapes.Draw>)+B0]");
			_ = 0;
			Color color2 = Color.HSVToRGB(hue, 1f, 1f, (byte)(int)num13 != 0);
			Vector2 vector2 = default(Vector2);
			Vector2 vector3 = default(Vector2);
			Vector2 vector4 = default(Vector2);
			Color colorD = default(Color);
			Draw.Quad_Internal((Vector3)(&vector), (Vector3)(&vector2), (Vector3)(&vector3), (Vector3)(&vector4), (Color)num13, (Color)thicknessSpace, color, colorD);
			MatrixStack matrixStack = default(MatrixStack);
			matrixStack.Dispose();
			Vector2 vector5 = labelSize;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj2 = vector5 ^ 0;
			float num20 = (float)obj2 * 0.5f;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106D480");
			Color color3 = Color.HSVToRGB(hue, saturation, value, (byte)(int)num13 != 0);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DA8430");
			string text = default(string);
			string content = "#" + text;
			nint num21 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMColorPickerRenderer)+48]");
			float num22 = 0f * 8.5f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1161 @ rax_v94 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num23 = 0;
			nint num24 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1195 @ rax_v98 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num25 = 0;
			_ = 4;
			nint num26 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1258 @ rax_v104 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num27 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1259 @ rcx_v80 (Il2CppStaticFields<Shapes.Draw>)+1C0]");
			float num28 = default(float);
			Draw.TextRect_Internal(content, (TextElement)null, (Rect)(&num28), (TMP_FontAsset)0, num13, (TextAlign)thicknessSpace, color);
			float num29 = hueStripThickness * 0.5f;
			float num30 = num29 * hueDotScale;
			float num31 = hue * ((float)Math.PI * 2f);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
			float num32 = default(float);
			DiscColors discColors2 = (Color)(&num32);
			float num33 = outline * 0.5f;
			float num34 = num33 + num30;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CFF0");
			Color color4 = Color.HSVToRGB(hue, 1f, 1f, (byte)(int)num13 != 0);
			DiscColors discColors3 = (Color)(&num32);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CFF0");
			Rect quadRect = QuadRect;
			if (0f > saturation || saturation > 1f)
			{
			}
			if (0f > value || value > 1f)
			{
			}
			DiscColors discColors4 = (Color)(&num32);
			float num35 = outline * 0.5f;
			float num36 = num35 + num30;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CFF0");
			Color color5 = Color.HSVToRGB(hue, saturation, value, (byte)(int)num13 != 0);
			DiscColors discColors5 = (Color)(&num32);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CFF0");
			object obj3 = default(object);
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			return;
		}
		throw new NullReferenceException();
	}

	private unsafe void ConstructHueStripPolyline()
	{
		//IL_0045: Expected O, but got I4
		//IL_010f: Expected O, but got Ref
		//IL_011c: Expected O, but got Ref
		//IL_0083: Expected O, but got Ref
		//IL_0083: Expected O, but got Ref
		//IL_0092: Expected O, but got Ref
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00b8: Expected O, but got I4
		//IL_00c1: Expected O, but got I4
		//IL_00c9: Expected O, but got Ref
		PolylinePath polylinePath = (hueStripPath = new PolylinePath());
		object obj = 0;
		PolylinePath p = polylinePath;
		bool hdr = default(bool);
		object obj2 = default(object);
		object obj3 = default(object);
		float num = default(float);
		PolylinePoint polylinePoint2 = default(PolylinePoint);
		bool flag;
		do
		{
			float h = (float)obj / 100f;
			Color color = Color.HSVToRGB(h, 1f, 1f, hdr);
			((PointPath<PolylinePoint>)(&obj2)).AddPoint((PolylinePoint)p);
			((PointPath<PolylinePoint>)(&obj2)).AddPoint((PolylinePoint)p);
			PolylinePoint polylinePoint = new PolylinePoint((Vector3)(&obj3), (Color)(&num));
			hueStripPath.AddPoint((PolylinePoint)(&polylinePoint2));
			obj++;
			flag = (nint)obj < 100;
			polylinePoint = (PolylinePoint)0;
			polylinePoint2 = (PolylinePoint)0;
			p = (PolylinePath)(&polylinePoint2);
		}
		while (flag);
	}
}
