using UnityEngine;

namespace Zagreekie.Utility
{
	public sealed class OpenWebPageOnEvent : MonoBehaviour
	{
		[Header("URL")]
		[SerializeField]
		[Tooltip("The URL to open when triggered.\nNotes:\n- Use a full absolute URL including scheme, e.g. 'https://example.com'.\n- 'http://' and 'https://' are recommended.\n- You can also use 'mailto:' or other OS-supported schemes, but test per platform.\nExamples:\n- https://unity.com\n- https://example.com/support")]
		private string url;

		[Header("Safety / Behavior")]
		[SerializeField]
		[Tooltip("If true, logs a warning instead of attempting to open when the URL is empty or whitespace.\nRecommended to keep enabled for safer prefab defaults.")]
		private bool guardAgainstEmptyUrl;

		[SerializeField]
		[Tooltip("If true, logs a message when an attempt to open the URL is made.\nUseful for verifying that your UnityEvent is wired correctly.")]
		private bool logWhenOpened;

		public void Open()
		{
		}

		public void OpenUrl(string urlToOpen)
		{
		}
	}
}
