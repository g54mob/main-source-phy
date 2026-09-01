using System;
using UnityEngine;

public class FileBrowserPagination : MonoBehaviour
{
	public Action<int, int> PageChanged;

	[SerializeField]
	[ReadOnly]
	private int currentPageNumber;

	[ReadOnly]
	[SerializeField]
	private int totalPagesNumber;

	[SerializeField]
	private FileBrowserPaginationButton[] pageButtons;

	[SerializeField]
	private SimpleUIButton nextPageButton;

	[SerializeField]
	private SimpleUIButton previousPageButton;

	public void Initialize()
	{
		FileBrowserPaginationButton[] array = pageButtons;
		foreach (FileBrowserPaginationButton fileBrowserPaginationButton in array)
		{
			fileBrowserPaginationButton.Initialize();
			fileBrowserPaginationButton.ButtonClicked = OnPaginationButtonClicked;
		}
		nextPageButton.Click += NextPageButtonClick;
		previousPageButton.Click += PreviousPageButtonClick;
	}

	private void PreviousPageButtonClick()
	{
		int num = currentPageNumber - 1;
		if (num != 0)
		{
			GoToPage(num - 1);
		}
	}

	private void NextPageButtonClick()
	{
		int num = currentPageNumber - 1;
		if (num != totalPagesNumber - 1)
		{
			GoToPage(num + 1);
		}
	}

	private void OnPaginationButtonClicked(int pageButtonNumber)
	{
		GoToPage(pageButtonNumber - 1);
	}

	public void Generate(int totalPages, int startPageIndex)
	{
		totalPagesNumber = totalPages;
		currentPageNumber = startPageIndex + 1;
		if (totalPages <= 1)
		{
			DisablePageButtons();
			return;
		}
		EnableNextPreviousButton();
		SetCounter(startPageIndex);
	}

	public void GoToPage(int pageIndex)
	{
		if (pageIndex >= 0 && pageIndex <= totalPagesNumber)
		{
			int num = currentPageNumber - 1;
			if (num == -1)
			{
				num = pageIndex;
			}
			SetCounter(pageIndex);
			if (PageChanged != null)
			{
				PageChanged(num, pageIndex);
			}
		}
	}

	private void SetCounter(int currentPageIndex)
	{
		currentPageNumber = currentPageIndex + 1;
		int num = 1;
		int num2 = Mathf.FloorToInt((float)pageButtons.Length / 2f);
		if (currentPageNumber > num2)
		{
			num = Mathf.Max(Mathf.Min(currentPageNumber - num2, totalPagesNumber - pageButtons.Length + 1), 1);
		}
		for (int i = 0; i < pageButtons.Length; i++)
		{
			int num3 = num + i;
			if (num3 > totalPagesNumber)
			{
				break;
			}
			pageButtons[i].gameObject.SetActive(true);
			pageButtons[i].SetButtonNumber(num3, num3 == currentPageNumber);
		}
	}

	public void DisablePageButtons()
	{
		for (int i = 0; i < pageButtons.Length; i++)
		{
			pageButtons[i].gameObject.SetActive(false);
		}
		previousPageButton.gameObject.SetActive(false);
		nextPageButton.gameObject.SetActive(false);
	}

	private void EnableNextPreviousButton()
	{
		previousPageButton.gameObject.SetActive(true);
		nextPageButton.gameObject.SetActive(true);
	}
}
