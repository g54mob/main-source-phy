using UnityEngine;
using UnityEngine.EventSystems;

public class DMSetTextOnHover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
	[SerializeField]
	private LocalizeText m_text;

	[SerializeField]
	[TextArea]
	public string onEnterText;

	[SerializeField]
	[TextArea]
	public string onExitText;

	public void OnPointerEnter(PointerEventData eventData)
	{
		SetText(onEnterText);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		SetText(onExitText);
	}

	public void OnSelect(BaseEventData eventData)
	{
		SetText(onEnterText);
	}

	public void OnDeselect(BaseEventData eventData)
	{
		SetText(onExitText);
	}

	private void SetText(string text)
	{
		if (m_text != null && base.gameObject.activeSelf)
		{
			m_text.LocaleID = text;
		}
	}
}
