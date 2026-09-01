using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UnitEditorColorImage : MonoBehaviour
{
	public Image Image;

	public UnitEditorUIColorDatabase.ColorMode ColorMode;

	[ContextMenu("Get Image")]
	private void Awake()
	{
		if (Image == null)
		{
			Image = GetComponent<Image>();
		}
	}

	public void Color(UnitEditorUIColorDatabase db)
	{
		Image.color = db.GetColor(ColorMode);
	}
}
