using System.Collections;
using System.Collections.Generic;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CustomContentGridBrowser : MonoBehaviour
{
	protected class PendingPopulateInfo
	{
		public bool HasPendingPopulate { get; set; }

		public int Page { get; set; }

		public int NewLayoutGroup { get; set; }
	}

	public int rows = 5;

	public GridLayoutGroup layout01;

	public GridLayoutGroup layout02;

	public GameObject leftPageButton;

	public GameObject rightPageButton;

	[HideInInspector]
	public bool instantClear;

	[SerializeField]
	public CustomContetnManager customContentManager;

	protected int currentLayoutGroup;

	protected int totalPages = 1;

	private CustomContentLoaderModIO customContentLoaderModIO;

	private readonly PendingPopulateInfo pendingPopulateInfo = new PendingPopulateInfo();

	public GameObject newContentGraphic;

	public GameObject newContentGraphicLocal;

	public int CurrentPage { get; protected set; }

	public GridLayoutGroup CurrentLayoutGroup
	{
		get
		{
			if (currentLayoutGroup == 0)
			{
				return layout01;
			}
			return layout02;
		}
	}

	public GridLayoutGroup SecondaryLayoutGroup
	{
		get
		{
			if (currentLayoutGroup == 0)
			{
				return layout02;
			}
			return layout01;
		}
	}

	public int MaxItemsPerPage => GetRowCount() * GetColCount();

	protected virtual void Awake()
	{
		customContentLoaderModIO = ServiceLocator.GetService<CustomContentLoaderModIO>();
	}

	public int GetColCount()
	{
		float num = 0f;
		float width = layout01.GetComponent<RectTransform>().rect.width;
		for (int i = 0; i < 20; i++)
		{
			num += layout01.cellSize.x;
			if (num > width)
			{
				return i;
			}
			num += layout01.spacing.x;
		}
		return 20;
	}

	public int GetRowCount()
	{
		float num = 0f;
		float height = layout01.GetComponent<RectTransform>().rect.height;
		for (int i = 0; i < 20; i++)
		{
			num += layout01.cellSize.y;
			if (num > height)
			{
				return i;
			}
			num += layout01.spacing.y;
		}
		return 20;
	}

	public int GetOtherLayoutGroupIndex(int index)
	{
		if (index == 0)
		{
			return 1;
		}
		return 0;
	}

	public void IncreasePage(int value)
	{
		int num = CurrentPage + value;
		if (num < 0 || num >= totalPages)
		{
			return;
		}
		CodeAnimation component = CurrentLayoutGroup.GetComponent<CodeAnimation>();
		if (component == null)
		{
			return;
		}
		SetAnimationDirection(component, value, positive: true);
		if (component != null)
		{
			component.PlayOut();
		}
		CodeAnimation component2 = SecondaryLayoutGroup.GetComponent<CodeAnimation>();
		if (!(component2 == null))
		{
			SetAnimationDirection(component2, value, positive: false);
			if (component2 != null)
			{
				component2.PlayIn();
			}
			Populate(num, GetOtherLayoutGroupIndex(currentLayoutGroup));
			UpdateChildAlignment();
			SelectCurrentLayoutFirstElement();
		}
	}

	public void SetAnimationDirection(CodeAnimation codeAnimation, int value, bool positive)
	{
		int num = (positive ? 1 : (-1));
		for (int i = 0; i < codeAnimation.animations.Length; i++)
		{
			if (codeAnimation.animations[i] != null)
			{
				codeAnimation.animations[i].direction = new Vector3((float)num * Mathf.Sign(value), 0f, 0f);
			}
		}
	}

	public virtual void Populate(int page = 0, int newLayoutGroup = 0)
	{
		pendingPopulateInfo.HasPendingPopulate = false;
	}

	public void Refresh()
	{
		instantClear = true;
		Populate(0, currentLayoutGroup);
		UpdatePageButtons();
		UpdateChildAlignment();
	}

	public void DestroyDelayed(List<GameObject> objects)
	{
		if (base.gameObject.activeSelf)
		{
			StartCoroutine(DestroyDelayedCortunie(objects));
			return;
		}
		foreach (GameObject @object in objects)
		{
			if (@object != null)
			{
				Object.Destroy(@object);
			}
		}
	}

	protected bool CheckShowLoadingIconOnPopulate(int page, int newLayoutGroup)
	{
		if (customContentManager != null && customContentLoaderModIO != null && customContentLoaderModIO.IsRefreshingOrWaitingToRefresh())
		{
			SetPendingPopulate(page, newLayoutGroup);
			customContentManager.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.Loading);
			return true;
		}
		return false;
	}

	private void SetPendingPopulate(int page, int newLayoutGroup)
	{
		pendingPopulateInfo.HasPendingPopulate = true;
		pendingPopulateInfo.Page = page;
		pendingPopulateInfo.NewLayoutGroup = newLayoutGroup;
	}

	private IEnumerator DestroyDelayedCortunie(List<GameObject> objects)
	{
		float timer = 0f;
		while (timer < 0.25f && !instantClear)
		{
			timer += Time.unscaledDeltaTime;
			yield return null;
		}
		for (int i = 0; i < objects.Count; i++)
		{
			Object.DestroyImmediate(objects[i]);
		}
		instantClear = false;
	}

	public void UpdateChildAlignment()
	{
		if (CurrentLayoutGroup.transform.childCount > GetColCount())
		{
			CurrentLayoutGroup.childAlignment = TextAnchor.UpperCenter;
		}
		else
		{
			CurrentLayoutGroup.childAlignment = TextAnchor.UpperLeft;
		}
	}

	public void UpdatePageButtons()
	{
		bool active = totalPages > 1;
		leftPageButton.SetActive(active);
		rightPageButton.SetActive(active);
	}

	public void Select()
	{
		base.gameObject.SetActive(value: true);
		UpdatePageButtons();
		GetComponent<CodeAnimation>()?.PlayIn();
		SelectCurrentLayoutFirstElement();
	}

	protected void CheckContentArrayLength(int length)
	{
		if (!(customContentManager == null))
		{
			CustomContentPageLoadingRefreshIcon.LoadingIconState newState = ((length > 0) ? CustomContentPageLoadingRefreshIcon.LoadingIconState.HaveContent : CustomContentPageLoadingRefreshIcon.LoadingIconState.Loading);
			customContentManager.UpdateLoadingScreenState(newState);
		}
	}

	public void Deselect()
	{
		GetComponent<CodeAnimation>()?.PlayOut();
	}

	public void SelectCurrentLayoutFirstElement()
	{
		if (base.isActiveAndEnabled)
		{
			StartCoroutine(Delay());
		}
		IEnumerator Delay()
		{
			yield return null;
			bool num = CurrentLayoutGroup.transform.childCount > 0;
			bool flag = PlayerActions.Instance.InputType == InputType.Controller;
			if (num && flag)
			{
				for (int num2 = CurrentLayoutGroup.transform.childCount - 1; num2 >= 0; num2--)
				{
					Transform child = CurrentLayoutGroup.transform.GetChild(num2);
					Selectable component = child.GetComponent<Selectable>();
					bool flag2 = component != null && component.navigation.mode != Navigation.Mode.None;
					if (child.gameObject.activeSelf && flag2)
					{
						component.Select();
						ISelectHandler[] components = component.GetComponents<ISelectHandler>();
						int num3 = 0;
						if (num3 < components.Length)
						{
							components[num3].OnSelect(new BaseEventData(EventSystem.current));
						}
					}
				}
			}
		}
	}

	private void OnEnable()
	{
		DMNewContentManager.onIdAdded.AddListener(UpdateNewContentGraphic);
		DMNewContentManager.onIdRemoved.AddListener(UpdateNewContentGraphic);
		pendingPopulateInfo.HasPendingPopulate = false;
		if (customContentLoaderModIO != null)
		{
			customContentLoaderModIO.ContentQuickRefreshed += OnContentQuickRefreshed;
			OnContentQuickRefreshed();
		}
	}

	private void OnDisable()
	{
		DMNewContentManager.onIdAdded.RemoveListener(UpdateNewContentGraphic);
		DMNewContentManager.onIdRemoved.RemoveListener(UpdateNewContentGraphic);
		if (customContentLoaderModIO != null)
		{
			customContentLoaderModIO.ContentQuickRefreshed -= OnContentQuickRefreshed;
		}
	}

	protected virtual void Update()
	{
		if (pendingPopulateInfo.HasPendingPopulate && (!(customContentLoaderModIO != null) || !customContentLoaderModIO.IsRefreshingOrWaitingToRefresh()))
		{
			Populate(pendingPopulateInfo.Page, pendingPopulateInfo.NewLayoutGroup);
		}
	}

	private void OnContentQuickRefreshed()
	{
		Refresh();
	}

	public void UpdateNewContentGraphic(DMNewContentManager.NewContentID newId, WorkshopContentType contentType)
	{
		if (newContentGraphic != null)
		{
			DMNewContentManager.HasNewContentOfType(contentType, isSavedToLocal: false, delegate(bool hasNewContent)
			{
				if (newContentGraphic != null)
				{
					newContentGraphic.SetActive(hasNewContent);
				}
			});
		}
		if (!(newContentGraphicLocal != null))
		{
			return;
		}
		DMNewContentManager.HasNewContentOfType(contentType, isSavedToLocal: true, delegate(bool hasNewContent)
		{
			if (newContentGraphic != null)
			{
				newContentGraphicLocal.SetActive(hasNewContent);
			}
		});
	}
}
