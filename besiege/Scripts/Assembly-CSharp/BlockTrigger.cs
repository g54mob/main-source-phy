public class BlockTrigger
{
	public TriggerSetJointBase JointTrigger;

	public int Index;

	public bool isOwnLink;

	public bool isDynamic;

	public BlockTrigger(int index, bool isOwn, bool isDyn)
	{
		Index = index;
		isOwnLink = isOwn;
		isDynamic = isDyn;
	}

	public BlockTrigger(TriggerSetJointBase trigger, bool isOwn)
	{
		JointTrigger = trigger;
		Index = trigger.Index;
		isOwnLink = isOwn;
		isDynamic = trigger.isDynamicLink;
	}
}
