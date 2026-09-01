using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class IMPanelSample : ImmediateModePanel
{
	public float fillAmount;

	public Gradient colorGradient;

	public string title;

	public unsafe override void DrawPanelShapes(Rect rect, ImCanvasContext ctx)
	{
		//IL_0046: Expected I, but got O
		//IL_0081: Expected O, but got Ref
		//IL_0081: Expected O, but got Ref
		//IL_0099: Expected I, but got O
		//IL_00d0: Expected O, but got Ref
		//IL_00d0: Expected O, but got Ref
		//IL_0120: Expected I, but got O
		if (colorGradient != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106D480");
			Color color = colorGradient.Evaluate(fillAmount);
			nint num = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v366 @ rax_v14 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v367 @ rcx_v13 (Il2CppStaticFields<Shapes.Draw>)+108]");
			float num3 = default(float);
			object obj = default(object);
			float thickness = default(float);
			Vector4 cornerRadii = default(Vector4);
			Draw.Rectangle_Internal(ShapesBlendMode.Opaque, false, (Rect)(&num3), (Color)(&obj), thickness, cornerRadii);
			nint num4 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v467 @ rax_v24 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v474 @ rcx_v21 (Il2CppStaticFields<Shapes.Draw>)+108]");
			object obj2 = default(object);
			Draw.Rectangle_Internal(ShapesBlendMode.Opaque, true, (Rect)(&num3), (Color)(&obj2), thickness, cornerRadii);
			nint num6 = (nint)typeof(Draw);
			float num7 = rect.m_Height + rect.m_YMin;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v497 @ rax_v29 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num8 = 0;
			float num9 = num7 + 6f;
			_ = 1131413504;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106DCC0");
		}
	}

	private unsafe Rect Inset(Rect r, float amount)
	{
		//IL_0053: Expected native int or pointer, but got O
		//IL_0074: Expected native int or pointer, but got O
		//IL_0081: Expected native int or pointer, but got O
		//IL_00a2: Expected native int or pointer, but got O
		float xMin = amount + r.m_XMin;
		float num = amount + amount;
		float yMin = amount + r.m_YMin;
		float num2 = amount + amount;
		Rect rect = default(Rect);
		((Rect*)(nint)rect)->m_XMin = xMin;
		float width = r.m_Width - num;
		((Rect*)(nint)rect)->m_YMin = yMin;
		((Rect*)(nint)rect)->m_Width = width;
		float height = r.m_Height - num2;
		((Rect*)(nint)rect)->m_Height = height;
		return rect;
	}

	public IMPanelSample()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B426D7]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		fillAmount = 1f;
		title = "Title";
		base._002Ector();
	}
}
