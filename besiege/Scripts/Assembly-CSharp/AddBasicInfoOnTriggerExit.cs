using UnityEngine;

public class AddBasicInfoOnTriggerExit : SimBehaviour
{
	public GameObject addTo;

	public Rigidbody observed;

	private bool addedInfo;

	private void OnTriggerExit(Collider other)
	{
		if (addedInfo)
		{
			return;
		}
		bool flag = false;
		if (!HasBasicInfo || basicInfo.infoType != BasicInfo.BasicInfoType.Block)
		{
			flag = true;
		}
		else
		{
			Machine parentMachine = base.ParentMachine;
			if (!base.HasParentMachine || parentMachine.isReady)
			{
				flag = true;
			}
		}
		if (flag && other.attachedRigidbody == observed)
		{
			if (addTo != null)
			{
				addTo.AddComponent<BasicInfo>();
			}
			else
			{
				Debug.LogWarning("addTo is null in AddBasicInfoOnTriggerExit!");
			}
			addedInfo = true;
		}
	}
}
