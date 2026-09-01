using System.Collections.Generic;
using System.Linq;
using BesiegeDlc;
using SRF;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("DlcBookmarkCanvas")]
public class DlcBookmarkCanvas : MonoBehaviour
{
	private const int MaxDlcCount = 3;

	[SerializeField]
	private Transform bookmarkBaseContainer;

	[SerializeField]
	private GameObject altContentObject;

	[SerializeField]
	private Transform altContentContainer;

	[SerializeField]
	private DlcBookmarkItemCanvas dlcItemTemplate;

	[SerializeField]
	private GameObject warningIconObject;

	[SerializeField]
	private HorizontalLayoutGroup baseContentGroup;

	[SerializeField]
	private int minLeftPadding = 10;

	private DlcManager dlcManager;

	private bool hasMissingDlc;

	private bool hasInstalledDlc;

	public void SetUp(List<uint> installedDlcTypes, List<uint> missingDlcTypes)
	{
		dlcManager = DlcManager.Instance;
		hasMissingDlc = missingDlcTypes.Count > 0;
		hasInstalledDlc = installedDlcTypes.Count > 0;
		Clear();
		if (hasMissingDlc)
		{
			SetupMissingDlcState(installedDlcTypes, missingDlcTypes);
		}
		else
		{
			SetupNormalState(installedDlcTypes);
		}
		ToggleBookmark(hasMissingDlc);
		ToggleAlternativeContent(false);
	}

	private void SetupMissingDlcState(List<uint> installedDlcTypes, List<uint> missingDlcTypes)
	{
		warningIconObject.SetActive(true);
		SetupDlcIcons(missingDlcTypes, missingDlcTypes.Count, bookmarkBaseContainer);
		if (installedDlcTypes.Count > 0)
		{
			SetupDlcIcons(installedDlcTypes, installedDlcTypes.Count, altContentContainer);
		}
		ConfigurePadding(true);
	}

	private void Clear()
	{
		foreach (Transform item in bookmarkBaseContainer.GetChildren().Concat(altContentContainer.GetChildren()))
		{
			if (!(item == dlcItemTemplate.transform))
			{
				Object.Destroy(item.gameObject);
			}
		}
	}

	private void ToggleBookmark(bool showBookmark)
	{
		base.gameObject.SetActive(showBookmark);
	}

	private void ToggleAlternativeContent(bool showContent)
	{
		altContentObject.SetActive(showContent);
	}

	public void OnCursorEntered()
	{
		if (hasMissingDlc)
		{
			if (hasInstalledDlc)
			{
				ToggleAlternativeContent(true);
			}
		}
		else
		{
			ToggleBookmark(true);
		}
	}

	public void OnCursorExited()
	{
		if (hasMissingDlc)
		{
			if (hasInstalledDlc)
			{
				ToggleAlternativeContent(false);
			}
		}
		else
		{
			ToggleBookmark(false);
		}
	}

	private void SetupNormalState(List<uint> installedDlcTypes)
	{
		int dlcCount = Mathf.Min(installedDlcTypes.Count, 3);
		SetupDlcIcons(installedDlcTypes, dlcCount, bookmarkBaseContainer);
		ConfigurePadding(false);
	}

	private void ConfigurePadding(bool hasWarningIcon)
	{
		RectOffset padding = baseContentGroup.padding;
		RectTransform component = warningIconObject.GetComponent<RectTransform>();
		padding.left = minLeftPadding;
		if (hasWarningIcon)
		{
			padding.left += (int)component.sizeDelta.x;
		}
	}

	private void SetupDlcIcons(List<uint> dlcTypes, int dlcCount, Transform parentTransform)
	{
		dlcItemTemplate.gameObject.SetActive(false);
		for (int i = 0; i < dlcCount; i++)
		{
			DlcManager.DlcType dlcType = (DlcManager.DlcType)dlcTypes[i];
			DlcBookmarkItemCanvas dlcBookmarkItemCanvas = (DlcBookmarkItemCanvas)Object.Instantiate(dlcItemTemplate, parentTransform);
			dlcBookmarkItemCanvas.gameObject.SetActive(true);
			Sprite dlcSprite = dlcManager.GetDlcSprite(dlcType);
			string dlcName = dlcManager.GetDlcName(dlcType);
			dlcBookmarkItemCanvas.Setup(dlcType, dlcName, dlcSprite);
		}
	}
}
