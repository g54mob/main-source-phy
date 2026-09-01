using UnityEngine;

namespace Landfall.TABS
{
	public class UnitButtonManager : MonoBehaviour
	{
		private UILayoutGroup m_layoutGroup;

		private void Awake()
		{
			m_layoutGroup = GetComponent<UILayoutGroup>();
			m_layoutGroup.OnButtonSpawned += OnSpawnedButton;
		}

		private void OnSpawnedButton(UnitButton button)
		{
			Debug.Log("Spawned Button: " + button.UnitID);
		}

		public void UpdateWhitelistUI()
		{
		}
	}
}
