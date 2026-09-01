using UnityEngine;

namespace ModIO.UI
{
	[AddComponentMenu("ModIO/Inspector/Owned Button State")]
	public class InspectorOwnedButtonState : MonoBehaviour, IModViewElement
	{
		public GameObject[] OwnedButtons;

		public GameObject[] NotOwnedButtons;

		private ModView m_view;

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		private void OnEnable()
		{
			SetModView(m_view);
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(UpdateOwnedButtons);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(UpdateOwnedButtons);
					UpdateOwnedButtons(m_view.profile);
				}
				else
				{
					UpdateOwnedButtons(null);
				}
			}
		}

		public void UpdateOwnedButtons(ModProfile modProfile)
		{
			if (modProfile != null)
			{
				bool toggleOn = modProfile.submittedBy.id == LocalUser.UserId;
				ToggleOwnedButtons(toggleOn);
			}
			else
			{
				ToggleButtons(OwnedButtons, false);
				ToggleButtons(NotOwnedButtons, false);
			}
		}

		private void ToggleOwnedButtons(bool toggleOn)
		{
			ToggleButtons(OwnedButtons, toggleOn);
			ToggleButtons(NotOwnedButtons, !toggleOn);
		}

		private void ToggleButtons(GameObject[] buttonsArray, bool toggleOn)
		{
			for (int i = 0; i < buttonsArray.Length; i++)
			{
				buttonsArray[i].SetActive(toggleOn);
			}
		}
	}
}
