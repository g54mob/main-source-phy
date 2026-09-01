using System.Collections.Generic;
using BesiegeDlc;
using ModIO;
using ModIO.UI;
using UnityEngine;

public class DlcModProfileFieldDisplay : MonoBehaviour, IModViewElement
{
	[SerializeField]
	private RectTransform containerParent;

	[SerializeField]
	private DlcBookmarkItemCanvas dlcItemTemplate;

	[SerializeField]
	private GameObject missingDlcRow;

	private ModView m_view;

	virtual GameObject IModViewElement.gameObject
	{
		get
		{
			return base.gameObject;
		}
	}

	public void SetModView(ModView view)
	{
		if (!(m_view == view))
		{
			if (m_view != null)
			{
				m_view.onProfileChanged.RemoveListener(DisplayProfile);
			}
			m_view = view;
			if (m_view != null)
			{
				m_view.onProfileChanged.AddListener(DisplayProfile);
				DisplayProfile(m_view.profile);
			}
			else
			{
				DisplayProfile(null);
			}
		}
	}

	public void DisplayProfile(ModProfile profile)
	{
		ToggleField(false);
		missingDlcRow.SetActive(false);
		if (profile != null)
		{
			uint dlcDependencyMask;
			WorkshopManager.ParseItemMetadata(profile.metadataBlob, out dlcDependencyMask);
			if (dlcDependencyMask != 0)
			{
				ToggleField(true);
				ClearDisplay();
				List<uint> installedDlcTypes;
				List<uint> missingDlcTypes;
				DlcManager.Instance.GetMissingDlcs(dlcDependencyMask, out installedDlcTypes, out missingDlcTypes);
				UpdateDisplay(missingDlcTypes, true);
				UpdateDisplay(installedDlcTypes, false);
				missingDlcRow.SetActive(missingDlcTypes.Count > 0);
			}
		}
	}

	private void ToggleField(bool toggleOn)
	{
		if (base.gameObject.activeSelf != toggleOn)
		{
			base.gameObject.SetActive(toggleOn);
		}
	}

	private void ClearDisplay()
	{
		foreach (Transform item in containerParent)
		{
			if (!(item == dlcItemTemplate.transform))
			{
				Object.Destroy(item.gameObject);
			}
		}
	}

	private void UpdateDisplay(List<uint> dlcTypes, bool markAsMissing)
	{
		DlcManager instance = DlcManager.Instance;
		for (int i = 0; i < dlcTypes.Count; i++)
		{
			DlcManager.DlcType dlcType = (DlcManager.DlcType)dlcTypes[i];
			DlcBookmarkItemCanvas dlcBookmarkItemCanvas = (DlcBookmarkItemCanvas)Object.Instantiate(dlcItemTemplate, containerParent);
			dlcBookmarkItemCanvas.gameObject.SetActive(true);
			Sprite dlcSprite = instance.GetDlcSprite(dlcType);
			string dlcName = instance.GetDlcName(dlcType);
			dlcBookmarkItemCanvas.Setup(dlcType, dlcName, dlcSprite, markAsMissing);
		}
	}
}
