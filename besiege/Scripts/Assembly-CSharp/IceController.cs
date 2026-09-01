using UnityEngine;

public class IceController : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		BasicInfo componentInParent = other.GetComponentInParent<BasicInfo>();
		if (componentInParent == null || !componentInParent.isSimulating)
		{
			return;
		}
		IceTag iceTag = null;
		bool flag = componentInParent.infoType == BasicInfo.BasicInfoType.Block;
		if (flag)
		{
			BlockBehaviour blockBehaviour = componentInParent as BlockBehaviour;
			if (blockBehaviour.gotChildBlocks)
			{
				BlockBehaviour childBlockFromCollider = blockBehaviour.GetChildBlockFromCollider(other);
				if (!object.ReferenceEquals(childBlockFromCollider, null))
				{
					blockBehaviour = childBlockFromCollider;
				}
			}
			iceTag = blockBehaviour.iceTag;
		}
		else if (!componentInParent.noRigidbody)
		{
			iceTag = componentInParent.GetComponent<IceTag>();
		}
		if (iceTag != null)
		{
			iceTag.Freeze();
			if (flag && (!StatMaster.isMP || componentInParent.ParentMachine.isLocalMachine))
			{
				AchievementHelper.Increment(35, 1);
			}
		}
	}
}
