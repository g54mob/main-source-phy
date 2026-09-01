using UnityEngine;
using UnityEngine.UI;

public class MachineInfoHUDItem : MonoBehaviour
{
	[SerializeField]
	private Image iconImage;

	[SerializeField]
	private Text limitText;

	[SerializeField]
	private Text tooltipText;

	[SerializeField]
	private Color bannedColor = Color.red;

	[SerializeField]
	private Color normalColor = Color.white;

	[SerializeField]
	private Color unlimittedColor = Color.cyan;

	public void Setup(MachineInfoHudItemData data)
	{
		iconImage.sprite = data.IconSprite;
		tooltipText.text = data.Name;
		if (data.Limit == -1)
		{
			limitText.text = data.Count.ToString();
		}
		else
		{
			limitText.text = string.Format("{0} / {1}", data.Count, data.Limit);
		}
		if (data.Limit == -1)
		{
			limitText.color = unlimittedColor;
		}
		else if (data.Count > data.Limit)
		{
			limitText.color = bannedColor;
		}
		else
		{
			limitText.color = normalColor;
		}
	}
}
