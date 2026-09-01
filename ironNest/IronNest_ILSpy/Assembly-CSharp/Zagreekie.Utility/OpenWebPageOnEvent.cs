using Cpp2ILInjected;
using UnityEngine;

namespace Zagreekie.Utility;

public sealed class OpenWebPageOnEvent : MonoBehaviour
{
	private string url;

	private bool guardAgainstEmptyUrl;

	private bool logWhenOpened;

	public void Open()
	{
		if (guardAgainstEmptyUrl && string.IsNullOrWhiteSpace(url))
		{
			string text = base.name;
			string message = "OpenWebPageOnEvent on '" + text + "' was triggered, but the URL is empty.";
			Debug.LogWarning(message, this);
			return;
		}
		if (logWhenOpened)
		{
			string message2 = "OpenWebPageOnEvent opening URL: " + url;
			Debug.Log(message2, this);
		}
		Application.OpenURL(url);
	}

	public void OpenUrl(string urlToOpen)
	{
		if (guardAgainstEmptyUrl && string.IsNullOrWhiteSpace(urlToOpen))
		{
			string text = base.name;
			string message = "OpenWebPageOnEvent on '" + text + "' was triggered, but the URL is empty.";
			Debug.LogWarning(message, this);
			return;
		}
		if (logWhenOpened)
		{
			string message2 = "OpenWebPageOnEvent opening URL: " + urlToOpen;
			Debug.Log(message2, this);
		}
		Application.OpenURL(urlToOpen);
	}

	public OpenWebPageOnEvent()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A82E]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		url = "https://example.com";
		guardAgainstEmptyUrl = true;
		base._002Ector();
	}
}
