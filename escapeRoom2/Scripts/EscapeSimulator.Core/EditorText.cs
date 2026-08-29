using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class EditorText : MonoBehaviour
{
	public TextMeshProUGUI textComponent;

	public Transform gizmo;

	public string text;

	public int fontSize = 12;

	public Color color = Color.white;

	public bool isBold;

	public bool isItalic;

	public Vector2 canvasSize = new Vector2(1f, 1f);

	public void syncVisuals()
	{
		textComponent.text = text;
		textComponent.fontSize = fontSize;
		textComponent.color = color;
		textComponent.rectTransform.sizeDelta = canvasSize * 100f;
		gizmo.localScale = new Vector3(canvasSize.x, canvasSize.y, 0.01f);
		FontStyles fontStyles = FontStyles.Normal;
		if (isBold)
		{
			fontStyles |= FontStyles.Bold;
		}
		if (isItalic)
		{
			fontStyles |= FontStyles.Italic;
		}
		textComponent.fontStyle = fontStyles;
	}
}
