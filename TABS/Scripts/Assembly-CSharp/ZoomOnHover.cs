using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomOnHover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[SerializeField]
	private float m_zoom = 1.2f;

	[SerializeField]
	private float m_transitionTime = 0.25f;

	private Vector2 m_startScale;

	private void Start()
	{
		m_startScale = base.transform.localScale;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ZoomIn();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ZoomOut();
	}

	public void ZoomIn()
	{
		base.transform.LeanScale(m_startScale * m_zoom, m_transitionTime).setEaseOutBack();
	}

	public void ZoomOut()
	{
		base.transform.LeanScale(m_startScale, m_transitionTime).setEaseOutBack();
	}
}
