using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class SetUIOutline : MonoBehaviour
	{
		public Image m_Image;

		private void Start()
		{
			m_Image.color = UIStyleManager.GetStyle().m_OutlineColor;
		}
	}
}
