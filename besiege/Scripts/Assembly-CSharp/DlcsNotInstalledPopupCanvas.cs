using System.Collections.Generic;
using BesiegeDlc;
using UnityEngine;

[AddComponentMenu("DlcsNotInstalledPopupCanvas")]
public class DlcsNotInstalledPopupCanvas : MonoBehaviour
{
	private const int MaxDlcCount = 3;

	[Header("References")]
	[SerializeField]
	private DlcMissingEntryCanvas missingEntryTemplate;

	[SerializeField]
	private Transform entriesContainer;

	[SerializeField]
	[Header("Settings")]
	private float spacing = 0.7f;

	public void Setup(uint dlcDependencyMask, int headerLocId)
	{
		List<uint> dlcTypesFromMask = DlcManager.Instance.GetDlcTypesFromMask(dlcDependencyMask);
		int num = Mathf.Min(dlcTypesFromMask.Count, 3);
		for (int i = 0; i < num; i++)
		{
			CreateMissingDlcEntry(i, dlcTypesFromMask[i]);
		}
	}

	internal void Setup(List<DlcManager.DlcStatus> issues, int headerLocId)
	{
		for (int i = 0; i < issues.Count; i++)
		{
			CreateDlcIssueEntry(i, issues[i]);
		}
	}

	private DlcMissingEntry CreateEntry(int index)
	{
		DlcMissingEntry dlcMissingEntry = (DlcMissingEntry)Object.Instantiate(missingEntryTemplate, entriesContainer);
		dlcMissingEntry.gameObject.SetActive(true);
		dlcMissingEntry.transform.localPosition = new Vector3(0f, (float)index * (0f - spacing), 0f);
		return dlcMissingEntry;
	}

	private void CreateMissingDlcEntry(int index, uint dlcType)
	{
		DlcMissingEntry dlcMissingEntry = CreateEntry(index);
		dlcMissingEntry.Setup(dlcType);
	}

	private void CreateDlcIssueEntry(int index, DlcManager.DlcStatus issue)
	{
		DlcMissingEntry dlcMissingEntry = CreateEntry(index);
		dlcMissingEntry.Setup(issue);
	}

	private void OnDisable()
	{
		DestroyMissingDlcEntries();
	}

	private void DestroyMissingDlcEntries()
	{
		foreach (Transform item in entriesContainer)
		{
			if (!(item == missingEntryTemplate.transform))
			{
				Object.Destroy(item.gameObject);
			}
		}
	}
}
