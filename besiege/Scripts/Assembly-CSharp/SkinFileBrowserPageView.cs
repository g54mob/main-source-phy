using System;

public class SkinFileBrowserPageView : FileBrowserPageView
{
	public Action<BlockSkinLoader.SkinPack> SkinModifiedClicked;

	public Action<BlockSkinLoader.SkinPack> SkinSelectClicked;

	public Action<BlockSkinLoader.SkinPack> SkinPaintClicked;

	protected override void AssignSlotDelegates(FileBrowserSlot slot)
	{
		base.AssignSlotDelegates(slot);
		SkinFileBrowserSlot skinFileBrowserSlot = (SkinFileBrowserSlot)slot;
		skinFileBrowserSlot.PaintClicked = OnSkinSlotPaintClicked;
		skinFileBrowserSlot.ModifyClicked = OnSkinSlotModifyClicked;
		skinFileBrowserSlot.SelectClicked = OnSkinSlotSelectClicked;
	}

	protected override void RemoveSlotDelegates(FileBrowserSlot slot)
	{
		base.RemoveSlotDelegates(slot);
		SkinFileBrowserSlot skinFileBrowserSlot = (SkinFileBrowserSlot)slot;
		skinFileBrowserSlot.PaintClicked = null;
		skinFileBrowserSlot.ModifyClicked = null;
		skinFileBrowserSlot.SelectClicked = null;
	}

	private void OnSkinSlotSelectClicked(SkinFileBrowserSlot slot)
	{
		if (SkinSelectClicked != null)
		{
			SkinSelectClicked(slot.SkinPack);
		}
	}

	private void OnSkinSlotModifyClicked(SkinFileBrowserSlot slot)
	{
		if (SkinModifiedClicked != null)
		{
			SkinModifiedClicked(slot.SkinPack);
		}
	}

	private void OnSkinSlotPaintClicked(SkinFileBrowserSlot slot)
	{
		if (SkinPaintClicked != null)
		{
			SkinPaintClicked(slot.SkinPack);
		}
	}
}
