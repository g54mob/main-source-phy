using System.Collections.Generic;
using BesiegeDlc;
using ModIO;
using ModIO.UI;
using UnityEngine;

[AddComponentMenu("Besiege/Workshop/FileBrowser/SlotDlcOverlayCanvas")]
internal class SlotDlcOverlayCanvas : MonoBehaviour, IModViewElement
{
	[SerializeField]
	private GameObject parentContainer;

	[SerializeField]
	private GameObject notInstalledOverlay;

	[SerializeField]
	private DlcBookmarkCanvas dlcBookmark;

	private uint dlcDependencyMask;

	private bool areRequirementsMet;

	private ModView modView;

	virtual GameObject IModViewElement.gameObject
	{
		get
		{
			return base.gameObject;
		}
	}

	public void SetModView(ModView view)
	{
		modView = view;
		modView.onProfileChanged.AddListener(OnProfileChanged);
		UpdateViewState();
	}

	private void UpdateViewState()
	{
		ModProfile profile = modView.profile;
		if (profile != null)
		{
			WorkshopManager.ParseItemMetadata(profile.metadataBlob, out dlcDependencyMask);
			areRequirementsMet = DlcManager.Instance.HasPurchasedDlcMask(dlcDependencyMask);
			if (!HasDlcRequirements())
			{
				base.gameObject.SetActive(false);
				return;
			}
			base.gameObject.SetActive(true);
			parentContainer.SetActive(true);
			bool flag = AreDlcInstalled();
			ToggleDlcNotInstalledOverlay(!flag);
			SetupBookmark();
		}
	}

	private void OnProfileChanged(ModProfile newProfile)
	{
		UpdateViewState();
	}

	private void SetupBookmark()
	{
		if (dlcDependencyMask == 0)
		{
			dlcBookmark.gameObject.SetActive(false);
			return;
		}
		List<uint> installedDlcTypes;
		List<uint> missingDlcTypes;
		DlcManager.Instance.GetMissingDlcs(dlcDependencyMask, out installedDlcTypes, out missingDlcTypes);
		dlcBookmark.SetUp(installedDlcTypes, missingDlcTypes);
	}

	private void OnDestroy()
	{
		if (modView != null)
		{
			modView.onProfileChanged.RemoveListener(OnProfileChanged);
		}
	}

	public void OnMouseHoverEnter()
	{
		dlcBookmark.OnCursorEntered();
	}

	public void OnMouseHoverExit()
	{
		dlcBookmark.OnCursorExited();
	}

	private void ToggleDlcNotInstalledOverlay(bool showOverlay)
	{
		notInstalledOverlay.SetActive(showOverlay);
	}

	private bool AreDlcInstalled()
	{
		return areRequirementsMet;
	}

	private bool HasDlcRequirements()
	{
		return dlcDependencyMask != 0;
	}
}
