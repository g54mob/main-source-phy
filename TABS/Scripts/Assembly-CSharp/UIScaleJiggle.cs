using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScaleJiggle : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
	private const float DEFAULT_SCALE = 1f;

	[HideInInspector]
	public ScaleJiggle jiggle;

	public float hoverScale = 1.2f;

	public float clickForce = 2f;

	private bool stayEnlarged;

	[HideInInspector]
	public bool isEnabled = true;

	private Button button;

	private bool interactable
	{
		get
		{
			if (!(button != null))
			{
				return isEnabled;
			}
			return button.interactable;
		}
	}

	private void Awake()
	{
		jiggle = GetComponent<ScaleJiggle>();
		button = GetComponent<Button>();
	}

	public void AddClickForce()
	{
		if (jiggle != null)
		{
			jiggle.AddForce(0f - clickForce);
		}
	}

	public void ResetTargetScale()
	{
		if (jiggle != null)
		{
			jiggle.targetScale = 1f;
		}
	}

	private void OnSelect()
	{
		if (base.enabled && jiggle != null)
		{
			jiggle.targetScale = hoverScale;
		}
	}

	private void OnDeselect()
	{
		if (!stayEnlarged)
		{
			ResetTargetScale();
		}
	}

	public void FreezeScaleJiggle()
	{
		stayEnlarged = true;
	}

	public void UnFreezeScaleJiggle()
	{
		stayEnlarged = false;
		ResetTargetScale();
	}

	private void OnPressed()
	{
		if (base.enabled)
		{
			AddClickForce();
		}
	}

	private bool IsCurrentlySelectd()
	{
		if (EventSystem.current == null)
		{
			return false;
		}
		return EventSystem.current.currentSelectedGameObject == base.gameObject;
	}

	public void OnSelect(BaseEventData eventData)
	{
		OnSelect();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (interactable)
		{
			OnPressed();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (interactable)
		{
			OnSelect();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (interactable && !IsCurrentlySelectd())
		{
			OnDeselect();
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		if (interactable)
		{
			OnDeselect();
		}
	}
}
