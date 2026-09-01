using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitCreatorRiderButton : MonoBehaviour
	{
		public UnitEditorUIManager m_uiManager;

		public void Click()
		{
			m_uiManager.ShowRiders();
		}
	}
}
