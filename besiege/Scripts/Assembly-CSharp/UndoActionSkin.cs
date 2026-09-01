using System;

public class UndoActionSkin : UndoAction
{
	private readonly BlockSkinLoader.SkinPack.Skin lastSkin;

	private readonly BlockSkinLoader.SkinPack.Skin skin;

	public UndoActionSkin(Machine m, Guid blockGuid, BlockSkinLoader.SkinPack.Skin newSkin, BlockSkinLoader.SkinPack.Skin oldSkin)
	{
		machine = m;
		guid = blockGuid;
		skin = newSkin;
		lastSkin = oldSkin;
	}

	public override bool Redo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			if (StatMaster.isMP)
			{
				NetworkAuxAddPiece.Instance.ChangeBlockSkin(block, skin);
			}
			else
			{
				block.VisualController.ReplaceSkin(skin);
			}
			block.OnUpdateSkin();
		}
		return true;
	}

	public override bool Undo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			if (StatMaster.isMP)
			{
				NetworkAuxAddPiece.Instance.ChangeBlockSkin(block, lastSkin);
			}
			else
			{
				block.VisualController.ReplaceSkin(lastSkin);
			}
			block.OnUpdateSkin();
		}
		return true;
	}
}
