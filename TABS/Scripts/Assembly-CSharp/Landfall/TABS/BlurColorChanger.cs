using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class BlurColorChanger : MonoBehaviour
	{
		public bool m_UseNewColor;

		public Image m_Image;

		public Color m_NewColor = Color.white;

		private Color m_ogColor;

		private void Awake()
		{
			m_ogColor = UIStyleManager.GetStyle().m_BackgroundColor;
		}

		private void Update()
		{
			Color b = m_ogColor;
			if (m_UseNewColor)
			{
				b = m_NewColor;
			}
			m_Image.color = Color.Lerp(m_Image.color, b, 2f * Time.unscaledDeltaTime);
		}
	}
}
