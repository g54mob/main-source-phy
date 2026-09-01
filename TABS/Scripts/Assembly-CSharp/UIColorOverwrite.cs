using UnityEngine;

[CreateAssetMenu(fileName = "NewUIColorOverwrite", menuName = "Landfall/UIColorOverwrite", order = 99999999)]
public class UIColorOverwrite : ScriptableObject
{
	public Color BlurBGColor = new Color(0.6056392f, 0.6056392f, 0.6056392f, 1f);

	public Color RedUIBG = new Color(0.6313726f, 0.1767843f, 0.1767843f, 63f / 85f);

	public Color BlueUIBG = new Color(0.1249412f, 0.2114519f, 0.4627451f, 0.6901961f);
}
