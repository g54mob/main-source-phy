using System;
using UnityEngine;

public class BlockMenuItemsInitiator : SingleInstanceFindOnly<BlockMenuItemsInitiator>
{
	public const int t_Basic = 0;

	public const int t_Blocks = 1;

	public const int t_Locomotion = 2;

	public const int t_Mechanical = 3;

	public const int t_Weaponry = 4;

	public const int t_Flight = 5;

	public const int t_Armour = 6;

	public const int t_Logic = 7;

	public const int t_Water = 8;

	public const int t_AI = 8;

	public BlockMenuControl[] Menus;

	private readonly string[] order = new string[9] { "t_BASIC", "t_BLOCKS", "t_LOCOMOTION", "t_MECHANICAL", "t_WEAPONRY", "t_FLIGHT", "t_ARMOUR", "t_LOGIC", "t_WATER" };

	public override string Name
	{
		get
		{
			return "BlockMenuItemsInitiator";
		}
	}

	public void CreateMenuList()
	{
		Menus = base.transform.parent.GetComponentsInChildren<BlockMenuControl>(true);
		SortMenuList();
	}

	public void SortMenuList()
	{
		BlockMenuControl[] array = new BlockMenuControl[Menus.Length];
		int num = 0;
		for (int i = 0; i < Menus.Length; i++)
		{
			BlockMenuControl blockMenuControl = Menus[i];
			int num2 = Array.IndexOf(order, blockMenuControl.name);
			if (num2 >= 0)
			{
				array[num2] = blockMenuControl;
				continue;
			}
			int num3 = order.Length + num;
			if (num3 >= Menus.Length)
			{
				Debug.LogError("Tried sorting non matching object: " + blockMenuControl.name);
				continue;
			}
			array[num3] = blockMenuControl;
			num++;
		}
		Menus = array;
	}

	protected override void Awake()
	{
		base.Awake();
		StatMaster.SelectedBlockChanged += UpdateMenuButtons;
		if (Menus == null)
		{
			CreateMenuList();
			return;
		}
		BlockMenuControl[] menus = Menus;
		foreach (BlockMenuControl blockMenuControl in menus)
		{
			if (blockMenuControl == null)
			{
				CreateMenuList();
				return;
			}
		}
		SortMenuList();
	}

	protected void Start()
	{
		BlockMenuControl[] menus = Menus;
		foreach (BlockMenuControl blockMenuControl in menus)
		{
			if (!(blockMenuControl == null))
			{
				blockMenuControl.Setup();
				BlockButtonControl[] buttons = blockMenuControl.buttons;
				foreach (BlockButtonControl blockButtonControl in buttons)
				{
					blockButtonControl.StartDisregardInactive();
				}
			}
		}
		UnityEngine.Object.FindObjectOfType<BlockTabController>().RefreshTabs();
		UpdateMenuButtons(BlockType.DoubleWoodenBlock);
		if (!StatMaster.isMP)
		{
			SingleInstanceFindOnly<PrefabVisualUI>.Instance.SetUIBasedOnID(BlockType.DoubleWoodenBlock);
		}
	}

	public void UpdateMenuButtons(BlockType id)
	{
		BlockMenuControl[] menus = Menus;
		foreach (BlockMenuControl blockMenuControl in menus)
		{
			blockMenuControl.UpdateButtons();
		}
	}

	private void OnDestroy()
	{
		StatMaster.SelectedBlockChanged -= UpdateMenuButtons;
		BlockMenuControl[] menus = Menus;
		foreach (BlockMenuControl blockMenuControl in menus)
		{
			BlockButtonControl[] buttons = blockMenuControl.buttons;
			foreach (BlockButtonControl blockButtonControl in buttons)
			{
				blockButtonControl.OnDestroyDisregardInactive();
			}
		}
	}
}
