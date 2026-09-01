using System.Collections.Generic;

public class BlockParentTabButton : BlockTabButton
{
	public List<BlockTabButton> subTabs = new List<BlockTabButton>();

	private bool state;

	private bool checkState;

	public override void SetVis(bool state)
	{
		checkState = true;
		if (state)
		{
			this.state = state;
		}
	}

	public virtual void SetOff()
	{
		checkState = true;
		state = false;
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (checkState)
		{
			base.SetVis(state);
			state = false;
			checkState = false;
		}
	}
}
