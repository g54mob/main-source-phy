using System;

public class SelectSkinPaintButton : SelectSkinButton
{
	public SkinPaintTool tool;

	public void Setup(int ID, BlockSkinLoader.SkinPack.Skin skin, SkinPaintTool tool)
	{
		this.tool = tool;
		Setup(ID, skin);
	}

	public override void OnClicked()
	{
		audioSource.Play();
		if (mySkin != null)
		{
			tool.SetIconDisplay(mySkin);
			OptionsMaster.BesiegeConfig.SkinsLastUsedTimes.AddOrReplace(mySkin.pack.id, DateTime.UtcNow);
			tool.Sort(false);
		}
	}
}
