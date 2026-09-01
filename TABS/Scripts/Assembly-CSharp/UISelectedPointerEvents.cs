using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectedPointerEvents : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Object to receive the pointer events. Leave empty if it's this game object.")]
	protected GameObject receivingObject;

	private PointerEventData pointerEventData;

	private IPointerEnterHandler enterHandler;

	private IPointerExitHandler exitHandler;

	private bool isSelected;

	private void Start()
	{
		pointerEventData = new PointerEventData(EventSystem.current);
		if (receivingObject == null)
		{
			receivingObject = base.gameObject;
		}
		enterHandler = receivingObject.GetComponent<IPointerEnterHandler>();
		exitHandler = receivingObject.GetComponent<IPointerExitHandler>();
	}

	private void OnDisable()
	{
		OnDeselected();
	}

	private void OnDestroy()
	{
		OnDeselected();
	}

	private void Update()
	{
		EventSystem current = EventSystem.current;
		if (!(current == null) && current.currentSelectedGameObject != base.gameObject)
		{
			OnDeselected();
		}
	}

	private void LateUpdate()
	{
		EventSystem current = EventSystem.current;
		if (!(current == null) && current.currentSelectedGameObject == base.gameObject)
		{
			OnSelected();
		}
	}

	private void OnSelected()
	{
		if (!isSelected)
		{
			isSelected = true;
			enterHandler?.OnPointerEnter(pointerEventData);
		}
	}

	private void OnDeselected()
	{
		if (isSelected)
		{
			isSelected = false;
			exitHandler?.OnPointerExit(pointerEventData);
		}
	}
}
