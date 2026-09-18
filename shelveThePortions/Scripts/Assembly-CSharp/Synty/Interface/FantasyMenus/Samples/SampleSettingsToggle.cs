using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleSettingsToggle : MonoBehaviour
	{
		[Header("References")]
		public Selectable selectable;

		public GameObject toggleGroupParent;

		[Header("Parameters")]
		public bool loop;

		private Toggle[] toggles;

		private int defaultToggle;

		private void Start()
		{
			toggles = toggleGroupParent.GetComponentsInChildren<Toggle>();
			for (int i = 0; i < toggles.Length; i++)
			{
				if (toggles[i].isOn)
				{
					defaultToggle = i;
					break;
				}
			}
		}

		private void Update()
		{
			if (selectable != null && EventSystem.current.currentSelectedGameObject == selectable.gameObject)
			{
				if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
				{
					PreviousToggle();
				}
				else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
				{
					NextToggle();
				}
			}
		}

		private void NextToggle()
		{
			if (toggles.Length == 0)
			{
				return;
			}
			for (int i = 0; i < toggles.Length; i++)
			{
				if (!toggles[i].isOn)
				{
					continue;
				}
				int num = i + 1;
				if (num >= toggles.Length)
				{
					if (!loop)
					{
						break;
					}
					num = 0;
				}
				toggles[num].isOn = true;
				break;
			}
		}

		private void PreviousToggle()
		{
			if (toggles.Length == 0)
			{
				return;
			}
			for (int i = 0; i < toggles.Length; i++)
			{
				if (!toggles[i].isOn)
				{
					continue;
				}
				int num = i - 1;
				if (num < 0)
				{
					if (!loop)
					{
						return;
					}
					num = toggles.Length - 1;
				}
				toggles[num].isOn = true;
				return;
			}
			SelectDefaultToggle();
		}

		private void SelectDefaultToggle()
		{
			if (toggles.Length == 0)
			{
				toggles[defaultToggle].isOn = true;
			}
		}
	}
}
