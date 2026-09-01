using UnityEngine;
using UnityEngine.UI;

public class GallerySlotDoubleClick : MonoBehaviour
{
	public GallerySlot m_Slot;

	public Button m_Button;

	private float m_LastClickTime;

	private void Awake()
	{
		m_Button.onClick.AddListener(OnButtonClick);
	}

	private void OnButtonClick()
	{
		float num = Time.realtimeSinceStartup - m_LastClickTime;
		m_LastClickTime = Time.realtimeSinceStartup;
		if (num < GameUI.DOUBLE_CLICK_THRESHOLD_SECONDS)
		{
			Gallery.LaunchForCurrentLevel();
		}
	}
}
