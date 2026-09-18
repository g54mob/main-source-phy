using UnityEngine;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleURL : MonoBehaviour
	{
		public void OpenURL(string url)
		{
			Application.OpenURL(url);
		}
	}
}
