using System;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

namespace Shapes;

[Serializable]
public struct TextStyle
{
	public static readonly TextStyle defaultTextStyle;

	public TMP_FontAsset font;

	public float size;

	public FontStyles style;

	public TextAlign alignment;

	public float characterSpacing;

	public float wordSpacing;

	public float lineSpacing;

	public float paragraphSpacing;

	public Vector4 margins;

	public TextWrappingModes wrap;

	public TextOverflowModes overflow;

	public float curvature;

	public Vector2 curvaturePivot;

	static TextStyle()
	{
		//IL_007d: Expected I, but got O
		//IL_00a9: Expected O, but got I
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		ShapesAssets instance = ShapesAssets.Instance;
		_ = instance.defaultFont;
		_ = 1065353216;
		_ = 4;
		_ = 0;
		_ = 0;
		_ = 1;
		_ = 0;
		_ = Vector4.zeroVector;
		nint num = (nint)typeof(TextStyle);
		_ = Vector2.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rax_v10 (Il2CppClass<Shapes.TextStyle>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rsp-60]");
		defaultTextStyle = (TextStyle)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rsp-50]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rsp-40]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rsp-30]");
		_ = 0;
	}
}
