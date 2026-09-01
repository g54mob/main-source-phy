using System;
using UnityEngine;

public class BlockTabController : MonoBehaviour
{
	public const int SearchTabIndex = 999;

	public Action OnCategoryChange;

	private int _activeTab = 4;

	public Transform[] tabs;

	public BlockTabButton[] buttons;

	private static MachineToolController _toolControllerCode;

	public int activeTab
	{
		get
		{
			return _activeTab;
		}
		set
		{
			_activeTab = value;
		}
	}

	public static MachineToolController toolControllerCode
	{
		get
		{
			if (_toolControllerCode == null)
			{
				_toolControllerCode = UnityEngine.Object.FindObjectOfType<MachineToolController>();
			}
			return _toolControllerCode;
		}
	}

	private void Start()
	{
		RefreshTabs();
	}

	private void Update()
	{
		if (StatMaster.isMP && PlayerData.localPlayer != null && PlayerData.localPlayer.isSpectator)
		{
			return;
		}
		bool isSimulating;
		if (StatMaster.isMP)
		{
			if (PlayerData.localPlayer == null)
			{
				goto IL_0064;
			}
			isSimulating = PlayerData.localPlayer.machine.isSimulating;
		}
		else
		{
			isSimulating = Machine.Active().isSimulating;
		}
		if (isSimulating)
		{
			return;
		}
		goto IL_0064;
		IL_0064:
		if (activeTab != 999 && InputManager.SearchKeys())
		{
			OpenTab(999);
		}
	}

	public void OpenTab(int index)
	{
		activeTab = index;
		RefreshTabs();
		if (OnCategoryChange != null)
		{
			OnCategoryChange();
		}
	}

	public void OpenTabWithBlock(int blockIndex)
	{
		int tabIndex;
		BlockButtonControl blockButton;
		if (FindTabIndex(blockIndex, out tabIndex, out blockButton))
		{
			OpenTab(tabIndex);
		}
	}

	public bool SelectBlock(int blockIndex)
	{
		int tabIndex;
		BlockButtonControl blockButton;
		if (!FindTabIndex(blockIndex, out tabIndex, out blockButton))
		{
			return false;
		}
		OpenTab(tabIndex);
		blockButton.Set();
		return true;
	}

	public void RefreshTabs()
	{
		toolControllerCode.DisableAll();
		for (int i = 0; i < tabs.Length; i++)
		{
			if (!(buttons[i] == null))
			{
				bool flag = buttons[i].myIndex == activeTab;
				tabs[i].gameObject.SetActive(flag);
				buttons[i].SetVis(flag);
			}
		}
	}

	public int GetTabIndex(int blockIndex)
	{
		int tabIndex;
		BlockButtonControl blockButton;
		FindTabIndex(blockIndex, out tabIndex, out blockButton);
		return tabIndex;
	}

	private bool FindTabIndex(int blockIndex, out int tabIndex, out BlockButtonControl blockButton)
	{
		tabIndex = -1;
		blockButton = null;
		BlockMenuControl[] menus = BlockMenuControl.Menus;
		foreach (BlockMenuControl blockMenuControl in menus)
		{
			BlockButtonControl[] blockButtons = blockMenuControl.BlockButtons;
			foreach (BlockButtonControl blockButtonControl in blockButtons)
			{
				if (blockButtonControl.myIndex == blockIndex)
				{
					int num = Array.IndexOf(tabs, blockMenuControl.transform);
					if (num != -1)
					{
						blockButton = blockButtonControl;
						tabIndex = num;
						return true;
					}
				}
			}
		}
		return false;
	}
}
