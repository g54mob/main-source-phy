using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	public class RichPresenceSetter : MonoBehaviour
	{
		public bool setOnEnable;

		public bool changeWithAppFocus;

		public StringKeyValuePair[] withFocus;

		public StringKeyValuePair[] withoutFocus;

		private void OnEnable()
		{
		}

		private void DelayUpdate()
		{
		}

		private void OnDisable()
		{
		}

		private void Application_focusChanged(bool focused)
		{
		}

		public void Set(params StringKeyValuePair[] settings)
		{
		}

		public void Set(string key, string value)
		{
		}

		public void Clear()
		{
		}
	}
}
