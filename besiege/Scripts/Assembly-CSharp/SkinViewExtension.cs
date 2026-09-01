public class SkinViewExtension : IFileBrowserViewExtension
{
	private SkinFileBrowserController skinController;

	public void Initialize(FileBrowserView view, FileBrowserController controller)
	{
		skinController = (SkinFileBrowserController)controller;
	}

	public void OnPageViewCreated(FileBrowserPageView pageView)
	{
		SkinFileBrowserPageView skinFileBrowserPageView = (SkinFileBrowserPageView)pageView;
		skinFileBrowserPageView.SkinModifiedClicked = OnSkinModifiedClicked;
		skinFileBrowserPageView.SkinSelectClicked = OnSkinSelectedClicked;
		skinFileBrowserPageView.SkinPaintClicked = OnSkinPaintClicked;
	}

	private void OnSkinPaintClicked(BlockSkinLoader.SkinPack skinPack)
	{
		skinController.PaintWithSkinPack(skinPack);
	}

	private void OnSkinSelectedClicked(BlockSkinLoader.SkinPack skinPack)
	{
		skinController.SelectSkinPack(skinPack);
	}

	private void OnSkinModifiedClicked(BlockSkinLoader.SkinPack skinPack)
	{
		skinController.ModifySkinPack(skinPack);
	}
}
