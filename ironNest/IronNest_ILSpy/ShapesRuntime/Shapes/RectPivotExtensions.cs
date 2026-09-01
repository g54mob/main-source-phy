using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public static class RectPivotExtensions
{
	public unsafe static Rect GetRect(RectPivot pivot, Vector2 size)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0060: Expected native int or pointer, but got O
		//IL_0029: Expected native int or pointer, but got O
		Rect rect = default(Rect);
		if (pivot == RectPivot.Corner)
		{
			((Rect*)(nint)rect)->m_XMin = 0f;
			return rect;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj = size ^ 0;
		float xMin = (float)obj * 0.5f;
		((Rect*)(nint)rect)->m_XMin = xMin;
		return rect;
	}

	public unsafe static Rect GetRect(RectPivot pivot, float w, float h)
	{
		//IL_0008: Expected native int or pointer, but got O
		//IL_0015: Expected native int or pointer, but got O
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_007a: Expected native int or pointer, but got O
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_00ac: Expected native int or pointer, but got O
		//IL_0043: Expected native int or pointer, but got O
		Rect rect = default(Rect);
		((Rect*)(nint)rect)->m_Width = w;
		((Rect*)(nint)rect)->m_Height = h;
		if (pivot == RectPivot.Corner)
		{
			((Rect*)(nint)rect)->m_XMin = 0f;
			return rect;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj = w ^ 0;
		float xMin = (float)obj * 0.5f;
		((Rect*)(nint)rect)->m_XMin = xMin;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj2 = h ^ 0;
		float yMin = (float)obj2 * 0.5f;
		((Rect*)(nint)rect)->m_YMin = yMin;
		return rect;
	}
}
