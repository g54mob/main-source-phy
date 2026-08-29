using UnityEngine;

public class DropdownItemTooltip : MonoBehaviour
{
	private void Start()
	{
		DropdownTooltips componentInParent = GetComponentInParent<DropdownTooltips>();
		if (componentInParent == null)
		{
			return;
		}
		int num = base.transform.GetSiblingIndex() - 1;
		if (num >= 0 && num < componentInParent.tooltips.Count)
		{
			string text = componentInParent.tooltips[num];
			if (!string.IsNullOrEmpty(text))
			{
				UITooltip component = base.gameObject.GetComponent<UITooltip>();
				component.text = text;
				component.useSpacingInSizeCalculation = true;
			}
		}
	}
}
