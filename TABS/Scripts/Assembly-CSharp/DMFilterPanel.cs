using System.Collections;
using ModIO.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DMFilterPanel : MonoBehaviour
{
	[SerializeField]
	private Toggle defaultSortToggle;

	[SerializeField]
	private Toggle defaultTimelineToggle;

	[SerializeField]
	private ExplorerFilterTagsSelector tagDisplay;

	[SerializeField]
	private CanvasGroup explorerView;

	public void ResetFilters()
	{
		defaultSortToggle.isOn = true;
		defaultTimelineToggle.isOn = true;
		tagDisplay.UpdateSelectedTagsDisplay(null);
	}

	public void Open()
	{
		defaultSortToggle.Select();
		EventSystem.current.SetSelectedGameObject(defaultSortToggle.gameObject);
	}

	public void Close()
	{
		StartCoroutine(WaitUntilBrowserIsOpen());
		IEnumerator WaitUntilBrowserIsOpen()
		{
			yield return new WaitUntil(() => explorerView.interactable);
			explorerView.GetComponent<ExplorerView>().Refresh();
		}
	}
}
