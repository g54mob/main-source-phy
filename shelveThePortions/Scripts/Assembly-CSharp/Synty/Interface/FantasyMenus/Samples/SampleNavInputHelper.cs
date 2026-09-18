using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleNavInputHelper : MonoBehaviour
	{
		public Toggle previous;

		public Toggle next;

		private Toggle selectionToggle;

		private bool seelctPrevious;

		private bool selectNext;

		public void Awake()
		{
			selectionToggle = GetComponent<Toggle>();
		}

		public void Update()
		{
			if (selectionToggle != null && selectionToggle.isOn)
			{
				if (Input.GetKeyDown(KeyCode.Q))
				{
					seelctPrevious = true;
				}
				else if (Input.GetKeyDown(KeyCode.E))
				{
					selectNext = true;
				}
			}
		}

		public void LateUpdate()
		{
			if (seelctPrevious)
			{
				SelectPrevious();
			}
			else if (selectNext)
			{
				SelectNext();
			}
			seelctPrevious = false;
			selectNext = false;
		}

		private void SelectPrevious()
		{
			if (previous != null)
			{
				previous.isOn = true;
			}
		}

		private void SelectNext()
		{
			if (next != null)
			{
				next.isOn = true;
			}
		}
	}
}
