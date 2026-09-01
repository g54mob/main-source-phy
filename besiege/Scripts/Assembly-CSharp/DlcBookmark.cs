using System.Collections.Generic;
using BesiegeDlc;
using UnityEngine;

public class DlcBookmark : MonoBehaviour
{
	private const int MaxDlcCount = 3;

	[SerializeField]
	private GameObject bookmarkBaseObject;

	[SerializeField]
	private GameObject altContentObject;

	[SerializeField]
	private AlignGUIObject altContentAlign;

	[SerializeField]
	private AlignGUIObject altBookmarkStartAlign;

	[SerializeField]
	private AlignGUIObject altBookmarkEndAlign;

	[SerializeField]
	private AlignGUIObject bookmarkEndAlign;

	[SerializeField]
	private DlcBookmarkItem dlcIconTemplate;

	[SerializeField]
	private GameObject warningIconObject;

	[SerializeField]
	private float dlcIconWidth = 0.5f;

	[SerializeField]
	private float dlcMargin = 0.2f;

	[SerializeField]
	private float altDlcMargin;

	private DlcManager dlcManager;

	private bool hasMissingDlc;

	private bool hasInstalledDlc;

	public void SetUp(List<uint> installedDlcTypes, List<uint> missingDlcTypes)
	{
		dlcManager = DlcManager.Instance;
		hasMissingDlc = missingDlcTypes.Count > 0;
		hasInstalledDlc = installedDlcTypes.Count > 0;
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
		int iconOffsetCount = missingDlcTypes.Count + 1;
		ResizeBookmarkToIcons(bookmarkBaseObject, iconOffsetCount, dlcMargin);
		bookmarkEndAlign.RealignObject();
		SetupDlcIcons(bookmarkBaseObject.transform, missingDlcTypes, missingDlcTypes.Count, true, dlcMargin);
		if (installedDlcTypes.Count > 0)
		{
			ResizeBookmarkToIcons(altContentAlign.gameObject, installedDlcTypes.Count, altDlcMargin);
			altBookmarkStartAlign.RealignObject();
			altContentAlign.RealignObject();
			altBookmarkEndAlign.RealignObject();
			SetupDlcIcons(altContentAlign.transform, installedDlcTypes, installedDlcTypes.Count, false, altDlcMargin, altContentObject.transform);
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
		int num = Mathf.Min(installedDlcTypes.Count, 3);
		ResizeBookmarkToIcons(bookmarkBaseObject, num, dlcMargin);
		bookmarkEndAlign.RealignObject();
		SetupDlcIcons(bookmarkBaseObject.transform, installedDlcTypes, num, false, dlcMargin);
	}

	private void SetupDlcIcons(Transform relativeParent, List<uint> dlcTypes, int dlcCount, bool addWarningIconMargin, float iconMargin, Transform parentOverride = null)
	{
		dlcIconTemplate.gameObject.SetActive(false);
		float num = ((!addWarningIconMargin) ? iconMargin : (iconMargin + dlcIconWidth));
		float num2 = dlcIconWidth * 0.5f;
		float num3 = num + num2;
		Transform parent = dlcIconTemplate.transform.parent;
		Vector3 vector = parent.InverseTransformPoint(relativeParent.position);
		for (int i = 0; i < dlcCount; i++)
		{
			DlcManager.DlcType dlcType = (DlcManager.DlcType)dlcTypes[i];
			DlcBookmarkItem dlcBookmarkItem = (DlcBookmarkItem)Object.Instantiate(dlcIconTemplate, (!(parentOverride == null)) ? parentOverride : dlcIconTemplate.transform.parent);
			dlcBookmarkItem.gameObject.SetActive(true);
			dlcBookmarkItem.transform.localPosition = vector + new Vector3(num3 + dlcIconWidth * (float)i, 0f, dlcIconTemplate.transform.localPosition.z);
			Texture dlcTexture = dlcManager.GetDlcTexture(dlcType);
			string dlcName = dlcManager.GetDlcName(dlcType);
			dlcBookmarkItem.Setup(dlcName, dlcTexture);
		}
	}

	private void ResizeBookmarkToIcons(GameObject bookmarkObject, int iconOffsetCount, float iconMargin)
	{
		Vector3 localScale = new Vector3(dlcIconWidth * (float)iconOffsetCount + iconMargin, 1f, 1f);
		bookmarkObject.transform.localScale = localScale;
	}
}
