using Landfall.TABS.UI.WinConditions;
using UnityEngine;
using UnityEngine.Events;

namespace Landfall.TABS.WinConditions
{
	public class ToggleGroupCallback : MonoBehaviour
	{
		[SerializeField]
		private PanelSlider m_winConditionToggler;

		[SerializeField]
		public UnityEvent TeamToggleCallback;

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void OnRedTeamToggle(bool value)
		{
			if (value)
			{
				m_winConditionToggler.TweenInLeftPanel();
				TeamToggleCallback?.Invoke();
			}
		}

		public void OnBlueTeamToggle(bool value)
		{
			if (value)
			{
				m_winConditionToggler.TweenInRightPanel();
				TeamToggleCallback?.Invoke();
			}
		}
	}
}
