using UnityEngine;

public class BlockMenuControl : MonoBehaviour
{
	public BlockButtonControl[] buttons;

	private AudioSource audioSource;

	private BlockTabController _tabController;

	private BlockButtonControl[] _blockButtons;

	public static BlockMenuControl[] Menus
	{
		get
		{
			return SingleInstanceFindOnly<BlockMenuItemsInitiator>.Instance.Menus;
		}
	}

	public BlockTabController TabController
	{
		get
		{
			if (_tabController == null)
			{
				_tabController = Object.FindObjectOfType<BlockTabController>();
			}
			return _tabController;
		}
	}

	public BlockButtonControl[] BlockButtons
	{
		get
		{
			if (_blockButtons == null)
			{
				_blockButtons = GetComponentsInChildren<BlockButtonControl>(true);
			}
			return _blockButtons;
		}
	}

	public static bool GetMenu(string menuName, out BlockMenuControl menu)
	{
		for (int i = 0; i < Menus.Length; i++)
		{
			if (Menus[i].gameObject.name.Equals(menuName))
			{
				menu = Menus[i];
				return true;
			}
		}
		menu = null;
		return false;
	}

	public void Setup()
	{
		audioSource = GetComponent<AudioSource>();
		OnEnable();
	}

	private void OnEnable()
	{
		CheckIfActive(false);
	}

	public void CheckIfActive(bool playSound)
	{
		if (playSound)
		{
			if (audioSource == null)
			{
				audioSource = GetComponent<AudioSource>();
			}
			if (audioSource.gameObject.activeInHierarchy)
			{
				audioSource.Play();
			}
		}
	}

	public void UpdateButtons()
	{
		BlockType blockType = SingleInstanceFindOnly<AddPiece>.Instance.CurrentType;
		BlockType currentType = SingleInstanceFindOnly<AddPiece>.Instance.CurrentType;
		if (currentType == BlockType.BuildNode || currentType == BlockType.BuildEdge)
		{
			blockType = BlockType.BuildSurface;
		}
		for (int i = 0; i < buttons.Length; i++)
		{
			BlockButtonControl blockButtonControl = buttons[i];
			if (!(blockButtonControl == null))
			{
				BlockType myIndex = (BlockType)blockButtonControl.myIndex;
				if (!AddPiece.isEditingLevel && SingleInstanceFindOnly<AddPiece>.Instance != null && myIndex == blockType)
				{
					blockButtonControl.Activate();
				}
				else
				{
					blockButtonControl.Deactivate();
				}
			}
		}
	}
}
