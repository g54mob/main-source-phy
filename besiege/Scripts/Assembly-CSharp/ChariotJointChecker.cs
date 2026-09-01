using UnityEngine;

public class ChariotJointChecker : MonoBehaviour
{
	public EntityAI passanger;

	public Joint[] jointsToKeepTrackOf;

	public float angleToEject = 0.707f;

	public float idleDurationToEject = 5f;

	private float timeSpentIdle;

	private float idleDefiningSpeed = 1f;

	private bool disconnect;

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		for (int i = 0; i < jointsToKeepTrackOf.Length; i++)
		{
			if (jointsToKeepTrackOf[i] == null || jointsToKeepTrackOf[i].connectedBody == null)
			{
				disconnect = true;
				break;
			}
		}
		if (!disconnect)
		{
			float num = Vector3.Dot(base.transform.up, Vector3.up);
			if (num < angleToEject)
			{
				disconnect = true;
			}
		}
		if (!disconnect)
		{
			if (timeSpentIdle > idleDurationToEject)
			{
				disconnect = true;
			}
			else if (idleDefiningSpeed > passanger.movement.VelocitySqr)
			{
				timeSpentIdle += Time.deltaTime;
			}
			else
			{
				timeSpentIdle = 0f;
			}
		}
		if (disconnect)
		{
			if (passanger.groundJoint != null)
			{
				passanger.groundJoint.connectedBody = null;
				passanger.useJointAsGround = false;
				Object.Destroy(passanger.groundJoint);
			}
			base.enabled = false;
		}
	}
}
