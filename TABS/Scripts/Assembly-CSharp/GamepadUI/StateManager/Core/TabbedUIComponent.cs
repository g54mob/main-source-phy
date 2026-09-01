using Landfall.TABS_Input;
using UnityEngine;

namespace GamepadUI.StateManager.Core
{
	public class TabbedUIComponent : UIComponentMainMenu
	{
		[SerializeField]
		protected TabbedUIPage[] TABS = new TabbedUIPage[0];

		protected int currentPage = -1;

		protected PlayerActions playerActions;

		protected override void Awake()
		{
			base.Awake();
			playerActions = PlayerActions.Instance;
			for (int i = 0; i < TABS.Length; i++)
			{
				_ = TABS[i];
			}
		}

		protected override void Start()
		{
			base.Start();
			SwitchTab(0, force: true);
		}

		protected override void Update()
		{
			base.Update();
			if (!base.IsActive || !playerActions.m_cycleTabs.WasPressed)
			{
				return;
			}
			int num = Mathf.RoundToInt(playerActions.m_cycleTabs.Value);
			int i = 1;
			for (int num2 = TABS.Length; i < num2; i++)
			{
				int wrappedPageIndex = GetWrappedPageIndex(num * i);
				if (IsTabValid(wrappedPageIndex))
				{
					SwitchTab(wrappedPageIndex);
					break;
				}
			}
		}

		public virtual void SwitchTab(int index)
		{
			SwitchTab(index, force: false);
		}

		public void SwitchTab(int index, bool force, bool setToggleValue = true)
		{
			int num = TABS.Length;
			bool flag = index >= 0 && index < num;
			if (!force && (!flag || index == currentPage))
			{
				return;
			}
			currentPage = index;
			for (int i = 0; i < num; i++)
			{
				TabbedUIPage tabbedUIPage = TABS[i];
				if (i == currentPage)
				{
					OpenSubMenu(tabbedUIPage.tabSubMenu);
					continue;
				}
				CloseSubMenu(tabbedUIPage.tabSubMenu);
				tabbedUIPage.tabSubMenu.gameObject.SetActive(value: false);
			}
			if (flag && setToggleValue)
			{
				TABS[index].toggleTab.isOn = true;
			}
		}

		public void TabClicked(int index)
		{
			SwitchTab(index, force: false, setToggleValue: false);
		}

		protected virtual bool IsTabValid(int index)
		{
			if (index >= 0)
			{
				return index < TABS.Length;
			}
			return false;
		}

		private int GetWrappedPageIndex(int tabIncrement)
		{
			int num = TABS.Length;
			int num2 = (currentPage + tabIncrement) % num;
			if (num2 >= 0)
			{
				return num2;
			}
			return num2 + num;
		}
	}
}
