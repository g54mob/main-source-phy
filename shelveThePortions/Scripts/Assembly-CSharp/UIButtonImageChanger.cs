using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIEventTriggers))]
public class UIButtonImageChanger : MonoBehaviour
{
	private Image buttonImage;

	[SerializeField]
	private Sprite hoveredImage;

	[SerializeField]
	private Sprite unhoveredImage;

	private void Awake()
	{
		buttonImage = GetComponent<Image>();
		SubscribeToEvents();
	}

	private void OnEnable()
	{
		buttonImage.sprite = unhoveredImage;
	}

	private void SubscribeToEvents()
	{
		UIEventTriggers component = GetComponent<UIEventTriggers>();
		component.OnPointerClickEvent.AddListener(OnPointerClick);
		component.OnPointerEnterEvent.AddListener(OnPointerEnter);
		component.OnPointerExitEvent.AddListener(OnPointerExit);
	}

	public void OnPointerClick()
	{
		buttonImage.sprite = unhoveredImage;
	}

	public void OnPointerEnter()
	{
		buttonImage.sprite = hoveredImage;
	}

	public void OnPointerExit()
	{
		buttonImage.sprite = unhoveredImage;
	}
}
