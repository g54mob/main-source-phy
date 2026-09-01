using TMPro;
using UnityEngine;

public class DMToggleTMPShadow : MonoBehaviour
{
	private TMP_Text tmpText;

	private Color shadowColor;

	private bool GetTextComponent()
	{
		if (tmpText != null)
		{
			return true;
		}
		tmpText = GetComponent<TMP_Text>();
		if (tmpText != null)
		{
			shadowColor = tmpText.materialForRendering.GetColor(ShaderUtilities.ID_UnderlayColor);
		}
		return tmpText != null;
	}

	public void EnableShadow(bool enable)
	{
		if (GetTextComponent())
		{
			Color value = (enable ? shadowColor : Color.clear);
			Material modifiedMaterial = tmpText.GetModifiedMaterial(tmpText.fontMaterial);
			if (modifiedMaterial != null)
			{
				modifiedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, value);
			}
		}
	}
}
