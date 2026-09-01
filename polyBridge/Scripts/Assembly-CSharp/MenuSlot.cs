using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSlot : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
	public delegate void OnPointerEnterDelegate(MenuSlot slot);

	public Button m_Button;

	public Image m_Background;

	public TextMeshProUGUI m_Text;

	public Image m_Icon;

	private OnPointerEnterDelegate m_OnPointerEnterCallback;

	public void SetPointerEnterCallback(OnPointerEnterDelegate enterCallback)
	{
		m_OnPointerEnterCallback = enterCallback;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_OnPointerEnterCallback?.Invoke(this);
	}
}
