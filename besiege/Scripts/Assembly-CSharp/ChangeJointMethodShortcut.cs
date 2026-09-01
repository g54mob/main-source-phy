using UnityEngine;

public class ChangeJointMethodShortcut : MonoBehaviour
{
	private void Update()
	{
		if (StatMaster.isMP && StatMaster.isClient)
		{
			base.enabled = false;
		}
		else
		{
			if (!Input.GetKey(KeyCode.RightShift) || !Input.GetKey(KeyCode.LeftShift) || !Input.GetKeyDown(KeyCode.Joystick1Button0))
			{
				return;
			}
			StatMaster.UseJointParenting = !StatMaster.UseJointParenting;
			if (StatMaster.UseJointParenting)
			{
				ParentingJointWarning parentingJointWarning = Object.FindObjectOfType(typeof(ParentingJointWarning)) as ParentingJointWarning;
				if (!object.ReferenceEquals(parentingJointWarning, null))
				{
					parentingJointWarning.ParentJointsEnabled();
				}
			}
		}
	}
}
