using System;
using System.Collections.Generic;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DraggableUIElement : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler, ISubmitHandler
{
	private Transform myHome;

	private Transform hoveredHome;

	private int myHomeSiblingID;

	private int hoveredSiblingID;

	private Transform whileDrag;

	private GraphicRaycaster m_Raycaster;

	private PointerEventData m_PointerEventData;

	private EventSystem m_EventSystem;

	[SerializeField]
	private float m_selectedJiggleScale = 1.15f;

	protected bool m_draggingWithController;

	protected PlayerActions m_playerActions;

	private ScaleJiggle m_jiggle;

	private RectTransform m_rectTransform;

	private Button m_button;

	protected DraggableDropZone hoveredZone;

	public event Action<bool> OnControllerSubmit;

	private void Start()
	{
		whileDrag = base.transform.root.GetComponentInChildren<WhileDragTransform>(includeInactive: true).transform;
		m_Raycaster = GetComponentInParent<GraphicRaycaster>();
		m_EventSystem = EventSystem.current;
		m_playerActions = PlayerActions.Instance;
		m_jiggle = GetComponent<ScaleJiggle>();
		m_button = GetComponent<Button>();
	}

	private void Update()
	{
		if (m_draggingWithController)
		{
			DragWithController();
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		StartMovement();
	}

	private void StartMovement()
	{
		myHome = base.transform.parent;
		myHomeSiblingID = base.transform.GetSiblingIndex();
		base.transform.SetParent(whileDrag, worldPositionStays: true);
		base.transform.SetSiblingIndex(whileDrag.childCount - 1);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		EndMovement();
	}

	public void EndMovement()
	{
		Land();
		m_jiggle.targetScale = 1f;
		m_draggingWithController = false;
	}

	private void Land()
	{
		if ((bool)hoveredZone)
		{
			base.transform.SetParent(hoveredHome, worldPositionStays: true);
			base.transform.SetSiblingIndex(hoveredSiblingID);
			hoveredZone.EndHover();
		}
		else
		{
			base.transform.SetParent(myHome, worldPositionStays: true);
			base.transform.SetSiblingIndex(myHomeSiblingID);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		(base.transform as RectTransform).position = Input.mousePosition;
		Drag(Input.mousePosition);
	}

	private void Drag(Vector3 pos)
	{
		DraggableDropZone draggableDropZone = FindDraggableDropZone(pos);
		if (draggableDropZone == null && hoveredZone != null)
		{
			hoveredZone.EndHover();
			hoveredZone = null;
		}
		else if (draggableDropZone != null)
		{
			HoveringOverDropZone(draggableDropZone);
		}
	}

	protected virtual DraggableDropZone FindDraggableDropZone(Vector3 position)
	{
		m_PointerEventData = new PointerEventData(m_EventSystem);
		m_PointerEventData.position = position;
		List<RaycastResult> list = new List<RaycastResult>();
		m_Raycaster.Raycast(m_PointerEventData, list);
		foreach (RaycastResult item in list)
		{
			DraggableDropZone componentInParent = item.gameObject.GetComponentInParent<DraggableDropZone>();
			if ((bool)componentInParent)
			{
				return componentInParent;
			}
		}
		return null;
	}

	protected abstract void DragWithController();

	protected void HoveringOverDropZone(DraggableDropZone zone)
	{
		hoveredZone = zone;
		hoveredSiblingID = zone.OnHoveredOver(base.gameObject);
		hoveredHome = zone.transform;
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (m_draggingWithController)
		{
			EndMovement();
		}
		else
		{
			m_draggingWithController = true;
			m_jiggle.targetScale = m_selectedJiggleScale;
			Navigation navigation = m_button.navigation;
			navigation.mode = Navigation.Mode.None;
			m_button.navigation = navigation;
			StartMovement();
			Drag(base.transform.position);
		}
		this.OnControllerSubmit?.Invoke(m_draggingWithController);
	}
}
