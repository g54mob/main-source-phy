using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverScale : MonoBehaviour
{
	public float m_ScaleMultiplier = 1.05f;

	public float m_ScaleTime = 0.1f;

	private Button m_Button;

	private RectTransform m_RectTransform;

	private PointerEvents m_PointerEvents;

	private Vector3 m_OriginalScale;

	private void Awake()
	{
		m_RectTransform = base.gameObject.GetComponent<RectTransform>();
		m_Button = base.gameObject.GetComponent<Button>();
		m_PointerEvents = base.gameObject.GetComponent<PointerEvents>();
		if (m_PointerEvents == null)
		{
			m_PointerEvents = base.gameObject.AddComponent<PointerEvents>();
		}
		if (m_PointerEvents != null && m_RectTransform != null)
		{
			m_PointerEvents.RegisterOnHoverChangeDelegate(OnHoverChange);
		}
		m_OriginalScale = base.transform.localScale;
	}

	private void OnEnable()
	{
		m_RectTransform.localScale = m_OriginalScale;
	}

	private void OnHoverChange(bool hover)
	{
		if (hover && base.enabled)
		{
			TwoStateButton component = GetComponent<TwoStateButton>();
			if ((!m_Button || m_Button.interactable) && (!component || !(component.m_Image.color.a < 0.5f)))
			{
				m_RectTransform.DOScale(m_OriginalScale * m_ScaleMultiplier, m_ScaleTime).SetEase(Ease.InOutBounce).SetLoops(1, LoopType.Yoyo)
					.SetUpdate(isIndependentUpdate: true);
				InterfaceAudio.Play("ui_menuButton_hover");
			}
		}
		else
		{
			m_RectTransform.DOScale(m_OriginalScale, m_ScaleTime).SetEase(Ease.InOutBounce).SetLoops(1, LoopType.Yoyo)
				.SetUpdate(isIndependentUpdate: true);
		}
	}
}
