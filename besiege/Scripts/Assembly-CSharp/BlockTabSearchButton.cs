public class BlockTabSearchButton : BlockTabButton
{
	public SearchField searchField;

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (StatMaster.isMP && PlayerData.localPlayer != null && PlayerData.localPlayer.isSpectator)
		{
			return;
		}
		bool isSimulating;
		if (StatMaster.isMP)
		{
			if (PlayerData.localPlayer == null)
			{
				goto IL_006a;
			}
			isSimulating = PlayerData.localPlayer.machine.isSimulating;
		}
		else
		{
			isSimulating = Machine.Active().isSimulating;
		}
		if (isSimulating)
		{
			return;
		}
		goto IL_006a;
		IL_006a:
		if (InputManager.SearchKeys())
		{
			StatMaster.ChangeSelectedBlock(StatMaster.SelectedBlockId);
			SetVis(true);
		}
	}

	public override void SetVis(bool state)
	{
		base.SetVis(state);
		searchField.Activate(state);
	}
}
