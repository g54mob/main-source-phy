using System.Collections.Generic;

public class BlockLink
{
	public List<BlockTrigger> Triggers;

	public BlockNode Other;

	public bool isDynamic;

	public bool isOwnLink;

	public BlockLink(BlockNode other)
	{
		Other = other;
		Triggers = new List<BlockTrigger>();
	}

	public void AddTrigger(int index, bool isDyn, bool isOwn)
	{
		Triggers.Add(new BlockTrigger(index, isOwn, isDyn));
		isDynamic = (isDynamic ? isDynamic : isDyn);
		isOwnLink = (isOwnLink ? isOwnLink : isOwn);
	}

	public void AddTrigger(TriggerSetJointBase trigger, bool isOwn)
	{
		Triggers.Add(new BlockTrigger(trigger, isOwn));
		isDynamic = (isDynamic ? isDynamic : trigger.isDynamicLink);
		isOwnLink = (isOwnLink ? isOwnLink : isOwn);
	}
}
