using System;
using System.Collections.Generic;

public abstract class UndoAction
{
	protected Guid guid;

	protected BlockInfo info;

	protected Machine machine;

	public bool isMultiAction;

	public bool changesOBM;

	public bool changesTransform;

	public bool changesCount;

	public bool changesParameters;

	public abstract bool Undo();

	public abstract bool Redo();

	public BlockInfo GetInfo()
	{
		return info;
	}

	public Guid GetGuid()
	{
		return guid;
	}

	public void OverwriteInfo(Machine newMachine, BlockInfo newInfo)
	{
		machine = newMachine;
		info = newInfo;
	}

	protected void ApplyInfo(Guid blockGuid, BlockInfo newInfo)
	{
		BlockBehaviour block;
		if (!machine.GetBlock(blockGuid, out block))
		{
			return;
		}
		BlockSkinLoader.SkinPack.Skin selectedSkin = block.VisualController.selectedSkin;
		bool flag = selectedSkin != newInfo.Skin;
		block.isBMAction = true;
		NetworkEditFieldHandler networkEditFieldHandler = EditFieldHandler.Instance as NetworkEditFieldHandler;
		if ((bool)networkEditFieldHandler)
		{
			networkEditFieldHandler.OnEditBlockState(block, true, newInfo.BlockData, (!flag) ? null : newInfo.Skin);
		}
		else
		{
			block.OnLoad(newInfo.BlockData);
			if (flag)
			{
				block.VisualController.ReplaceSkin(newInfo.Skin);
			}
			OpenBlockMapper(block);
		}
		block.isBMAction = false;
	}

	protected void OpenBlockMapper(BlockBehaviour block)
	{
		if (!isMultiAction)
		{
			AdvancedBlockEditor.Instance.SetActiveTool(StatMaster.Tool.Modify, false);
			BlockMapper currentInstance = BlockMapper.CurrentInstance;
			if (currentInstance == null || currentInstance.Current != block)
			{
				BlockMapper.Open(block);
			}
		}
	}

	public virtual List<BlockBehaviour> GetBlocks()
	{
		List<BlockBehaviour> list = new List<BlockBehaviour>();
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			list.Add(block);
		}
		return list;
	}
}
