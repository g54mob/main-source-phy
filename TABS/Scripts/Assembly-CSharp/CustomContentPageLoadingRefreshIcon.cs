using UnityEngine;

public class CustomContentPageLoadingRefreshIcon : MonoBehaviour
{
	public enum LoadingIconState
	{
		Loading = 0,
		NoContent = 1,
		HaveContent = 2
	}

	[SerializeField]
	protected GameObject loadingIcon;

	[SerializeField]
	protected GameObject noCustomContentText;

	private void ShowLoadingIcon(bool show)
	{
		if (loadingIcon != null)
		{
			loadingIcon.SetActive(show);
		}
	}

	private void ShowNoCustomContentText(bool show)
	{
		if (noCustomContentText != null)
		{
			noCustomContentText.SetActive(show);
		}
	}

	public void UpdateLoadingScreenState(LoadingIconState newState)
	{
		switch (newState)
		{
		case LoadingIconState.Loading:
			ShowLoadingIcon(show: true);
			ShowNoCustomContentText(show: false);
			break;
		case LoadingIconState.NoContent:
			ShowLoadingIcon(show: false);
			ShowNoCustomContentText(show: true);
			break;
		case LoadingIconState.HaveContent:
			ShowLoadingIcon(show: false);
			ShowNoCustomContentText(show: false);
			break;
		}
	}
}
