using System;
using System.Collections.Generic;
using BesiegeDlc;
using UnityEngine;

[AddComponentMenu("Besiege/Workshop/FileBrowser/SlotDlcOverlay")]
internal class SlotDlcOverlay : MonoBehaviour
{
	[SerializeField]
	private GameObject parentContainer;

	[SerializeField]
	private GameObject notInstalledOverlay;

	[SerializeField]
	private DlcBookmark dlcBookmark;

	private IWorkshopItem workshopObject;

	private FileBrowserSlot slot;

	private void Start()
	{
		slot = GetComponentInParent<FileBrowserSlot>();
		workshopObject = slot.VirtualObject as IWorkshopItem;
		if (workshopObject == null || !HasDlcRequirements())
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		parentContainer.SetActive(true);
		FileBrowserSlot fileBrowserSlot = slot;
		fileBrowserSlot.CursorEntered = (Action)Delegate.Combine(fileBrowserSlot.CursorEntered, new Action(OnCursorEntered));
		FileBrowserSlot fileBrowserSlot2 = slot;
		fileBrowserSlot2.CursorExited = (Action)Delegate.Combine(fileBrowserSlot2.CursorExited, new Action(OnCursorExited));
		bool flag = AreDlcsInstalled();
		ToggleDlcNotInstalledOverlay(!flag);
		SetupBookmark(workshopObject.DlcDependencyMask);
	}

	private void SetupBookmark(uint dlcDependencyMask)
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
		if (slot != null)
		{
			FileBrowserSlot fileBrowserSlot = slot;
			fileBrowserSlot.CursorEntered = (Action)Delegate.Remove(fileBrowserSlot.CursorEntered, new Action(OnCursorEntered));
			FileBrowserSlot fileBrowserSlot2 = slot;
			fileBrowserSlot2.CursorExited = (Action)Delegate.Remove(fileBrowserSlot2.CursorExited, new Action(OnCursorExited));
		}
	}

	private void OnCursorExited()
	{
		dlcBookmark.OnCursorExited();
	}

	private void OnCursorEntered()
	{
		dlcBookmark.OnCursorEntered();
	}

	private void ToggleDlcNotInstalledOverlay(bool showOverlay)
	{
		notInstalledOverlay.SetActive(showOverlay);
	}

	private bool AreDlcsInstalled()
	{
		return workshopObject.AreDlcRequirementsMet;
	}

	private bool HasDlcRequirements()
	{
		return workshopObject.DlcDependencyMask != 0;
	}
}
