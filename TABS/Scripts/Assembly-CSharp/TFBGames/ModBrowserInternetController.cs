using ModIO.UI;
using UnityEngine;

namespace TFBGames
{
	public class ModBrowserInternetController : MonoBehaviour
	{
		private IInternetStatusService m_InternetStatus;

		private void Start()
		{
			m_InternetStatus = ServiceLocator.GetService<IInternetStatusService>();
			if (ModBrowser.instance != null)
			{
				ModBrowser.instance.Initialize(IsConnectedToInternet);
			}
			else
			{
				Debug.LogError("Failed to find the ModBrowser.");
			}
		}

		private bool IsConnectedToInternet()
		{
			if (m_InternetStatus != null)
			{
				return m_InternetStatus.IsConnectedWithCache(connectIfNotConnected: false);
			}
			return true;
		}
	}
}
