using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectedMapCellUI : MonoBehaviour
{
	public void Init(UnityAction onButtonClick, bool selected, bool interactable, bool newLevel)
	{
		Image component = GetComponent<Image>();
		RawImage componentInChildren = GetComponentInChildren<RawImage>();
		if (selected)
		{
			base.gameObject.FetchComponent<UIScaleJiggle>().enabled = false;
			base.gameObject.FetchComponent<UISounds>().enabled = false;
			componentInChildren.material = null;
			componentInChildren.color = Color.white;
			TextMeshProUGUI componentInChildren2 = GetComponentInChildren<TextMeshProUGUI>();
			Color black = Color.black;
			black.a = 0.9411765f;
			componentInChildren2.color = black;
		}
		else if (!interactable)
		{
			float a = 0.11764706f;
			TextMeshProUGUI componentInChildren3 = GetComponentInChildren<TextMeshProUGUI>();
			Color color = componentInChildren3.color;
			color.a = a;
			componentInChildren3.color = color;
			color = component.color;
			color.a = a;
			component.color = color;
			base.gameObject.FetchComponent<UIScaleJiggle>().enabled = false;
			base.gameObject.FetchComponent<UISounds>().enabled = false;
		}
		else
		{
			GetComponent<Button>().onClick.AddListener(onButtonClick);
			if (newLevel)
			{
				GetComponent<ScaleJiggleAnimation>().Play();
			}
		}
	}
}
