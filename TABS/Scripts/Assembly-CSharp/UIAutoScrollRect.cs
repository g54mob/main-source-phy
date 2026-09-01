using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class UIAutoScrollRect : MonoBehaviour
{
	[SerializeField]
	[Tooltip("An amount of padding to add to the scroll offset.")]
	protected float scrollOffset;

	[SerializeField]
	[Tooltip("Enable to use the selectable element transform instead of finding its parent")]
	protected bool disableParentLookup;

	[SerializeField]
	protected bool isHorizontal;

	protected ScrollRect scrollRect;

	protected Scrollbar verticalScrollBar;

	protected RectTransform scrollRectTransform;

	protected RectTransform viewportRectTransform;

	protected RectTransform contentRectTransform;

	protected GameObject currentlySelectedGameObject;

	protected RectTransform selectedRectTransform;

	protected Vector3[] currentButtonWorldCorners = new Vector3[4];

	protected Vector3[] viewportWorld = new Vector3[4];

	protected bool isChildOfThis;

	protected GameObject overrideSelection;

	protected EventSystem currentEventSystem;

	protected void Awake()
	{
		scrollRect = GetComponent<ScrollRect>();
		scrollRectTransform = GetComponent<RectTransform>();
		currentEventSystem = EventSystem.current;
		if (scrollRect != null)
		{
			contentRectTransform = scrollRect.content;
			viewportRectTransform = scrollRect.viewport;
			verticalScrollBar = scrollRect.verticalScrollbar;
			if (verticalScrollBar != null)
			{
				DisableScrollbarNavigation();
			}
		}
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged += OnInputSourceChanged;
			OnInputSourceChanged(service.CurrentInputType);
		}
	}

	private void OnDestroy()
	{
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged -= OnInputSourceChanged;
		}
	}

	private void OnInputSourceChanged(InputType type)
	{
		base.enabled = type == InputType.Controller;
	}

	public void SetSelectionOverride(GameObject selectionOverride)
	{
		overrideSelection = selectionOverride;
	}

	public void RequestUpdate()
	{
		UpdateScrollPosition();
	}

	protected void DisableScrollbarNavigation()
	{
		if (verticalScrollBar != null)
		{
			Navigation navigation = verticalScrollBar.navigation;
			navigation.mode = Navigation.Mode.None;
			verticalScrollBar.navigation = navigation;
		}
	}

	protected void Update()
	{
		UpdateScrollPosition();
	}

	protected void UpdateScrollPosition()
	{
		if (currentEventSystem == null)
		{
			return;
		}
		currentlySelectedGameObject = ((overrideSelection != null) ? overrideSelection : currentEventSystem.currentSelectedGameObject);
		if (currentlySelectedGameObject == null)
		{
			return;
		}
		isChildOfThis = currentlySelectedGameObject.transform.parent.IsChildOf(contentRectTransform.transform);
		if (!isChildOfThis || !(currentlySelectedGameObject.transform is RectTransform))
		{
			return;
		}
		RectTransform rectTransform = TopLevelParentOf(currentlySelectedGameObject.transform) as RectTransform;
		if (rectTransform == null)
		{
			return;
		}
		rectTransform.GetWorldCorners(currentButtonWorldCorners);
		viewportRectTransform.GetWorldCorners(viewportWorld);
		if (!isHorizontal)
		{
			bool flag = currentButtonWorldCorners[1].y > viewportWorld[1].y;
			if (currentButtonWorldCorners[0].y < viewportWorld[0].y)
			{
				float num = viewportWorld[0].y - currentButtonWorldCorners[0].y + scrollOffset;
				contentRectTransform.position += Vector3.up * num;
			}
			else if (flag)
			{
				float num2 = currentButtonWorldCorners[1].y - viewportWorld[1].y + scrollOffset;
				contentRectTransform.position += Vector3.down * num2;
			}
			return;
		}
		bool num3 = currentButtonWorldCorners[3].x > viewportWorld[3].x;
		bool flag2 = currentButtonWorldCorners[0].x < viewportWorld[0].x;
		if (num3)
		{
			float num4 = viewportWorld[3].x - currentButtonWorldCorners[3].x + scrollOffset;
			contentRectTransform.position += Vector3.right * num4;
		}
		else if (flag2)
		{
			float num5 = currentButtonWorldCorners[0].x - viewportWorld[0].x + scrollOffset;
			contentRectTransform.position += Vector3.left * num5;
		}
	}

	protected Transform TopLevelParentOf(Transform child)
	{
		if (disableParentLookup)
		{
			return child;
		}
		Transform parent = child.parent;
		if (!(parent == contentRectTransform))
		{
			return TopLevelParentOf(parent);
		}
		return child;
	}
}
