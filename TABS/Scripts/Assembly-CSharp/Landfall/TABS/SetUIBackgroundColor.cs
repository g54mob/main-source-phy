using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class SetUIBackgroundColor : MonoBehaviour
	{
		public Image m_Image;

		private void Start()
		{
			if (SceneSettings.UseSceneColorOverwrite)
			{
				m_Image.color = SceneSettings.GetBackgroundColor();
			}
			else
			{
				m_Image.color = UIStyleManager.GetStyle().m_BackgroundColor;
			}
		}
	}
}
