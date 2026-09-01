using System.Collections.Generic;
using BesiegeDlc;
using Localisation;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Dlc Mismatch UI")]
public class DlcMismatchUI : MonoBehaviour
{
	public Button CloseButton;

	public Button ExpandButton;

	public GameObject ScrollView;

	public Text TitleText;

	public GameObject EntryTemplate;

	private static DlcMismatchUI _instance;

	private List<DlcManager.DlcStatus> dlcIssues;

	private List<DlcMismatchEntry> entries;

	internal static void Show(List<DlcManager.DlcStatus> dlcIssues, int titleLocId)
	{
		if ((bool)_instance)
		{
			_instance.gameObject.SetActive(true);
			_instance.TitleText.text = LocalisationManager.GetTranslation(titleLocId);
			_instance.dlcIssues = dlcIssues;
			_instance.RebuildList();
		}
	}

	public static void Hide()
	{
		if ((bool)_instance)
		{
			_instance.gameObject.SetActive(false);
		}
	}

	public void Awake()
	{
		_instance = this;
		CloseButton.onClick.AddListener(delegate
		{
			Hide();
		});
		ExpandButton.onClick.AddListener(delegate
		{
			ToggleScrollView(true);
		});
		ToggleScrollView(true);
		EntryTemplate.SetActive(false);
		base.gameObject.SetActive(false);
	}

	private void ToggleScrollView(bool toggle)
	{
		CloseButton.gameObject.SetActive(toggle);
		ExpandButton.gameObject.SetActive(!toggle);
		ScrollView.SetActive(toggle);
	}

	private void RebuildList()
	{
		if (entries != null)
		{
			foreach (DlcMismatchEntry entry in entries)
			{
				Object.Destroy(entry.gameObject);
			}
		}
		entries = new List<DlcMismatchEntry>();
		foreach (DlcManager.DlcStatus dlcIssue in dlcIssues)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(EntryTemplate, EntryTemplate.transform.parent);
			DlcMismatchEntry component = gameObject.GetComponent<DlcMismatchEntry>();
			component.Init(dlcIssue);
			gameObject.SetActive(true);
			entries.Add(component);
		}
	}
}
