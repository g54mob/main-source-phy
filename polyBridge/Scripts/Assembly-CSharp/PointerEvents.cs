using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEvents : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
	public delegate void OnEventDelegate();

	public delegate void OnHoverEventDelegate(bool hover);

	[NonSerialized]
	public bool m_IsHovering;

	private OnEventDelegate m_OnClickedDelegate;

	private OnEventDelegate m_OnUpDelegate;

	private OnEventDelegate m_OnDownDelegate;

	private OnHoverEventDelegate m_OnHoverChangeDelegate;

	private void OnDisable()
	{
		m_IsHovering = false;
	}

	public void RegisterOnClickedDelegate(OnEventDelegate callback)
	{
		m_OnClickedDelegate = callback;
	}

	public void RegisterOnUpDelegate(OnEventDelegate callback)
	{
		m_OnUpDelegate = callback;
	}

	public void RegisterOnDownDelegate(OnEventDelegate callback)
	{
		m_OnDownDelegate = callback;
	}

	public void RegisterOnHoverChangeDelegate(OnHoverEventDelegate callback)
	{
		m_OnHoverChangeDelegate = callback;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_IsHovering = true;
		if (m_OnHoverChangeDelegate != null)
		{
			m_OnHoverChangeDelegate(hover: true);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_IsHovering = false;
		if (m_OnHoverChangeDelegate != null)
		{
			m_OnHoverChangeDelegate(hover: false);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (m_OnClickedDelegate != null)
		{
			m_OnClickedDelegate();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (m_OnUpDelegate != null)
		{
			m_OnUpDelegate();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (m_OnDownDelegate != null)
		{
			m_OnDownDelegate();
		}
	}
}
