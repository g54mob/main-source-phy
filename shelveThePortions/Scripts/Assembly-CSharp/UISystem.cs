using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UISystem : Singleton<UISystem>
{
	public static float animationInfoShowTime = 0.3f;

	private int UILayer;

	private static bool isMouseOverUI = false;

	private void Start()
	{
		UILayer = LayerMask.NameToLayer("UI");
	}

	public void RefreshContentSize(GameObject root, float delay = 0f)
	{
		RefreshContentSize(root.GetComponent<ContentSizeFitter>(), delay);
	}

	public void RefreshContentSize(ContentSizeFitter csf, float delay = 0f)
	{
		StartCoroutine(Routine(csf, delay));
	}

	private IEnumerator Routine(ContentSizeFitter csf, float delay = 0f)
	{
		yield return new WaitForSecondsRealtime(delay);
		if (!(csf == null) && !(csf.gameObject == null) && csf.gameObject.activeSelf)
		{
			bool isVertical = csf.verticalFit == ContentSizeFitter.FitMode.PreferredSize;
			bool isHorzontal = csf.horizontalFit == ContentSizeFitter.FitMode.PreferredSize;
			if (isHorzontal)
			{
				csf.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			}
			if (isVertical)
			{
				csf.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
			}
			yield return new WaitForEndOfFrame();
			if (isHorzontal)
			{
				csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			}
			if (isVertical)
			{
				csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			}
		}
	}

	public static bool IsOverUI()
	{
		return isMouseOverUI;
	}

	private void Update()
	{
		isMouseOverUI = IsPointerOverUIElement();
	}

	private bool IsPointerOverUIElement()
	{
		foreach (RaycastResult eventSystemRaycastResult in GetEventSystemRaycastResults())
		{
			if (eventSystemRaycastResult.gameObject.layer == UILayer)
			{
				return true;
			}
		}
		return false;
	}

	private static List<RaycastResult> GetEventSystemRaycastResults()
	{
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = Mouse.current.position.ReadValue();
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData, list);
		return list;
	}
}
