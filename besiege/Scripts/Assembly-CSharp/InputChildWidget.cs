using System.Collections.Generic;
using UnityEngine;

public class InputChildWidget : InputBaseWidget
{
	public int mask = -1;

	[SerializeField]
	private DynamicText keyName;

	[SerializeField]
	private UIButtonExtended hideChild;

	[SerializeField]
	private Texture2D hide;

	[SerializeField]
	private Texture2D unhide;

	protected BlockBehaviour block;

	protected MKey key;

	private bool hovered;

	protected void Awake()
	{
		keySelector.KeyModified += OnKeyModified;
		keySelector.KeysChanged += OnKeysChanged;
		hideChild.Down += ToggleIgnored;
	}

	public override void Init(int i, object parameter)
	{
		base.Init(i, parameter);
		index = i;
		group = (InputGroup)parameter;
		InputGroup.BlockEntry blockEntry = group.blockList[index];
		block = blockEntry.block;
		keySelector.Key = (key = blockEntry.key);
		keySelector.Init();
		BlockType type = block.Prefab.Type;
		string text = ((block.KeyList.Count >= 2) ? blockEntry.key.DisplayName : ReferenceMaster.TranslateBlockName(type));
		ReferenceMaster.SetDynamicText(keyName, text.ToUpper());
		UpdateBlockVis(new List<BlockType> { type });
		ToggleHover(false);
	}

	protected override void UpdateBlockVis(List<BlockType> types)
	{
		base.UpdateBlockVis(types);
		hideChild.icon.material.mainTexture = ((!group.HasEmptyKey() && !key.ignored) ? hide : unhide);
	}

	private void OnKeysChanged()
	{
		if (isEditing)
		{
			BlockMapper.OnEditField(block, key);
			OverviewBlockMapper currentInstance = OverviewBlockMapper.CurrentInstance;
			if (!StatMaster.isMP && currentInstance != null)
			{
				currentInstance.OnEditBlockKey(block, key);
			}
		}
	}

	private void OnKeyModified(int index, KeyCode keyCode)
	{
		if (isEditing)
		{
			key.AddOrReplaceKey(index, keyCode);
			BlockMapper.OnEditField(block, key);
			OverviewBlockMapper currentInstance = OverviewBlockMapper.CurrentInstance;
			if (!StatMaster.isMP && currentInstance != null)
			{
				currentInstance.OnEditBlockKey(block, key);
			}
		}
	}

	private void ToggleIgnored()
	{
		MKey mKey = keySelector.Key;
		mKey.SetIgnored(!mKey.Ignored);
		OnKeysChanged();
	}

	public void ToggleHover(bool toggle)
	{
		if (toggle != hovered)
		{
			hovered = toggle;
			hideChild.icon.enabled = toggle;
		}
	}
}
