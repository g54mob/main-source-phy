using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InternalModding.Blocks
{
	public static class TabCreator
	{
		public static readonly int MaxTabButtons = 6;

		public static int MaxBlocksPerTab = -1;

		public static List<BlockTabButton> ModTabButtons;

		public static List<GameObject> ModTabs;

		public static BlockMenuControl[] BMC;

		public static string bottombarPath = "HUD/BottomBar/Align (Bottom Left)";

		private static int originalButtonsLength;

		private static int originalTabsLength;

		public static void CreateTabs()
		{
			if (GameObject.Find(bottombarPath) == null)
			{
				bottombarPath = "HUD/BottomBar/AlignBottomLeft";
			}
			GameObject gameObject = GameObject.Find(bottombarPath + "/MOD TAB BUTTONS/SLIDER");
			if (gameObject == null)
			{
				Debug.LogError("[TabCreator] Couldn't find tab buttons!");
				ModTabButtons = new List<BlockTabButton>(0);
				ModTabs = new List<GameObject>(0);
				return;
			}
			Transform transform = gameObject.transform;
			Transform transform2 = GameObject.Find(bottombarPath + "/BLOCK BUTTONS").transform;
			float num = SingleInstanceFindOnly<AddPiece>.Instance.hudCam.orthographicSize * 2f / (float)Screen.height;
			float num2 = SingleInstanceFindOnly<BlockLoader>.Instance.BlockButtonTemplate.GetComponent<BoxCollider>().size.x * SingleInstanceFindOnly<BlockLoader>.Instance.BlockButtonTemplate.transform.lossyScale.x;
			float num3 = num2 / num;
			Vector3 position = transform2.FindChild("StartPosition").position;
			Vector3 vector = SingleInstanceFindOnly<AddPiece>.Instance.hudCam.WorldToScreenPoint(position);
			MaxBlocksPerTab = Mathf.FloorToInt(((float)Screen.width - vector.x) / num3);
			int num4 = Mathf.CeilToInt((float)SingleInstanceFindOnly<BlockLoader>.Instance.VisibleBlocksCount / (float)MaxBlocksPerTab);
			ModTabButtons = new List<BlockTabButton>(num4);
			ModTabs = new List<GameObject>(num4);
			for (int i = 0; i < num4; i++)
			{
				if (num4 > MaxTabButtons)
				{
					break;
				}
				CreateTabButton(i, num4, transform);
				CreateTab(i, transform2);
			}
			RegisterTabs();
		}

		public static void DestroyTabs()
		{
			if (ModTabButtons != null && ModTabButtons.Count != 0)
			{
				for (int i = 0; i < ModTabButtons.Count; i++)
				{
					DestroyTabButton(i);
					DestroyTab(i);
				}
				ModTabButtons.Clear();
				ModTabs.Clear();
				UnregisterBlocksFromTabs();
				UnregisterTabs();
			}
		}

		private static void CreateTabButton(int index, int count, Transform parent)
		{
			GameObject gameObject = Object.Instantiate(SingleInstanceFindOnly<BlockLoader>.Instance.TabButtonTemplate);
			gameObject.name = "MODDED BLOCKS";
			gameObject.transform.parent = parent;
			Vector3 localPosition = new Vector3(index, 0f, 0f);
			gameObject.transform.localPosition = localPosition;
			gameObject.SetActive(true);
			ModTabButtons.Add(gameObject.GetComponent<BlockTabButton>());
			Transform transform = GameObject.Find(bottombarPath + "/MOD TAB BUTTONS/SLIDER/BGS/BG").transform;
			transform.localScale = new Vector3(count, 1f, 1f);
		}

		private static void DestroyTabButton(int index)
		{
			Object.Destroy(ModTabButtons[index]);
		}

		private static void CreateTab(int index, Transform parent)
		{
			GameObject gameObject = Object.Instantiate(SingleInstanceFindOnly<BlockLoader>.Instance.TabTemplate);
			gameObject.name = "t_Modded" + index;
			gameObject.transform.parent = parent;
			ModTabs.Add(gameObject);
		}

		private static void DestroyTab(int index)
		{
			Object.Destroy(ModTabs[index]);
		}

		private static void RegisterTabs()
		{
			BlockTabController component = GameObject.Find(bottombarPath + "/TAB BUTTONS").GetComponent<BlockTabController>();
			BlockTabButton[] buttons = component.buttons;
			originalButtonsLength = buttons.Length;
			Transform[] tabs = component.tabs;
			originalTabsLength = tabs.Length;
			component.buttons = new BlockTabButton[buttons.Length + ModTabButtons.Count];
			component.tabs = new Transform[tabs.Length + ModTabs.Count];
			for (int i = 0; i < buttons.Length; i++)
			{
				component.buttons[i] = buttons[i];
				component.tabs[i] = tabs[i];
			}
			for (int j = 0; j < ModTabButtons.Count; j++)
			{
				BlockTabButton blockTabButton = ModTabButtons[j];
				blockTabButton.controller = component;
				blockTabButton.myIndex = buttons.Length + j;
				component.buttons[buttons.Length + j] = blockTabButton;
				component.tabs[buttons.Length + j] = ModTabs[j].transform;
				blockTabButton.controller = component;
				blockTabButton.myIndex = buttons.Length + j;
			}
			component.RefreshTabs();
		}

		public static void UnregisterTabs()
		{
			BlockTabController blockTabController = Object.FindObjectOfType<BlockTabController>();
			blockTabController.buttons = blockTabController.buttons.Take(originalButtonsLength).ToArray();
			blockTabController.tabs = blockTabController.tabs.Take(originalTabsLength).ToArray();
			blockTabController.RefreshTabs();
		}

		public static void RegisterBlocksToTabs()
		{
			if (ModTabs == null)
			{
				Debug.LogWarning("[TabCreator] ModTabs are null!");
				return;
			}
			BMC = new BlockMenuControl[ModTabs.Count];
			for (int i = 0; i < ModTabs.Count; i++)
			{
				GameObject gameObject = ModTabs[i];
				int childCount = gameObject.transform.childCount;
				BMC[i] = gameObject.GetComponent<BlockMenuControl>();
				BMC[i].buttons = new BlockButtonControl[childCount];
				for (int j = 0; j < childCount; j++)
				{
					BMC[i].buttons[j] = gameObject.transform.GetChild(j).GetComponent<BlockButtonControl>();
				}
			}
			BlockMenuItemsInitiator instance = SingleInstanceFindOnly<BlockMenuItemsInitiator>.Instance;
			BlockMenuControl[] menus = instance.Menus;
			instance.Menus = new BlockMenuControl[menus.Length + ModTabs.Count];
			for (int k = 0; k < menus.Length; k++)
			{
				instance.Menus[k] = menus[k];
			}
			for (int l = 0; l < ModTabs.Count; l++)
			{
				BlockMenuControl component = ModTabs[l].GetComponent<BlockMenuControl>();
				instance.Menus[menus.Length + l] = component;
				component.Setup();
				BlockButtonControl[] buttons = component.buttons;
				foreach (BlockButtonControl blockButtonControl in buttons)
				{
					blockButtonControl.StartDisregardInactive();
				}
			}
			Object.FindObjectOfType<BlockTabController>().RefreshTabs();
			instance.UpdateMenuButtons(BlockType.DoubleWoodenBlock);
		}

		private static void UnregisterBlocksFromTabs()
		{
			BlockMenuItemsInitiator instance = SingleInstanceFindOnly<BlockMenuItemsInitiator>.Instance;
			instance.Menus = instance.Menus.Take(originalTabsLength - 1).ToArray();
			Object.FindObjectOfType<BlockTabController>().RefreshTabs();
			instance.UpdateMenuButtons(BlockType.DoubleWoodenBlock);
		}
	}
}
