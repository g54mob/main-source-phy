public class BlockSubTabButton : BlockTabButton
{
	public BlockTabButton parentTab;

	public override void OnClicked()
	{
		parentTab.myIndex = myIndex;
		base.OnClicked();
	}

	public override void SetVis(bool state)
	{
		base.SetVis(state);
		parentTab.SetVis(state);
	}
}
