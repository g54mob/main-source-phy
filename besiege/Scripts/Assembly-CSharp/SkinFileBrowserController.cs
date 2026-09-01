using System;
using System.Linq;

public class SkinFileBrowserController : FileBrowserController
{
	protected SetupSkinPackWindow setupSkinPackWindow;

	public SkinFileBrowserController(FileBrowserView browserView)
		: base(browserView)
	{
		setupSkinPackWindow = GameObjectHelper.FindObjectsOfTypeAll<SetupSkinPackWindow>().FirstOrDefault();
	}

	public void PaintWithSkinPack(BlockSkinLoader.SkinPack skinPack)
	{
		Machine machine = null;
		bool flag = false;
		if (StatMaster.isMP)
		{
			if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
			{
				machine = PlayerData.localPlayer.machine;
				flag = true;
			}
		}
		else
		{
			machine = Machine.Active();
			flag = machine != null;
		}
		if (flag && skinPack != null)
		{
			BlockSkinLoader.SetAllBlocksToPack(skinPack, machine);
		}
		view.Close();
	}

	public void SelectSkinPack(BlockSkinLoader.SkinPack skinPack)
	{
		if (skinPack != null)
		{
			BlockSkinLoader.SetAllPrefabsToPack(skinPack);
		}
		view.Close();
	}

	public void ModifySkinPack(BlockSkinLoader.SkinPack skinPack)
	{
		if (skinPack != null)
		{
			setupSkinPackWindow.SetupWindow(skinPack);
		}
	}

	protected override void LoadFile(IVirtualObject virtualObject, OpenMode mode)
	{
		LocalSkinFile localSkinFile = (LocalSkinFile)virtualObject;
		if (localSkinFile.SkinPack.type != PackType.Official)
		{
			ModifySkinPack(localSkinFile.SkinPack);
		}
	}

	protected override void SaveFile(IVirtualObject virtualObject, OpenMode mode)
	{
	}

	protected override void UploadFile(IVirtualObject virtualObject)
	{
		LocalSkinFile localSkinFile = (LocalSkinFile)virtualObject;
		if (localSkinFile.SkinPack != null)
		{
			view.UploadSkin(localSkinFile.SkinPack);
		}
	}

	protected override void UploadFolder(IVirtualObject virtualObject)
	{
		throw new NotImplementedException();
	}
}
