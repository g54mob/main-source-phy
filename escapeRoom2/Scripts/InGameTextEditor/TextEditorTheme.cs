using UnityEngine;

[CreateAssetMenu(fileName = "TextEditorTheme", menuName = "InGameTextEditor/TextEditorTheme", order = 1)]
public class TextEditorTheme : ScriptableObject
{
	public string themeName = "Name";

	public Color mainBackgroundColor = Color.black;

	public Color mainFontColor = Color.aliceBlue;

	public Color lineNumberColor = Color.gray;

	public Color lineNumberBackgroundColor = Color.black;

	public Color caretColor = Color.white;

	public Color activeSelectionColor = Color.aquamarine;

	public Color inactiveSelectionColor = Color.gray3;

	public Color commentColor = Color.navajoWhite;

	public Color stringColor = Color.darkOrange;

	public Color numberColor = Color.cornflowerBlue;

	public Color keywordColor = Color.cyan;

	public Color builtInColor = Color.magenta;

	[Header("File Picker")]
	public Color filePickerDefaultButtonColor = Color.gray2;

	public Color filePickerSelectedButtonColor = Color.gray1;

	public Color filePickerButtonTextColor = Color.white;

	public Color filePickerBackgroundColor = Color.gray1;

	public Color buttonsPanelBackgroundColor = Color.gray2;
}
