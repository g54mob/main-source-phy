using UnityEngine;

public class ProgressOnJointBreak : MonoBehaviour
{
	protected int victoryValue = 1;

	private Joint myJoint;

	private bool hasBroken;

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			myJoint = GetComponent<Joint>();
			if (myJoint == null)
			{
				base.enabled = false;
			}
		}
		else
		{
			hasBroken = true;
		}
	}

	public void OnJointBreak(float breakForce)
	{
		Progress();
	}

	private void FixedUpdate()
	{
		if (!hasBroken && StatMaster.levelSimulating && myJoint == null)
		{
			Progress();
		}
	}

	private void OnDisable()
	{
		if (StatMaster.levelSimulating)
		{
			Progress();
		}
	}

	protected void Progress()
	{
		if (!hasBroken)
		{
			Debug.Log("break joint");
			if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
			{
				WinCondition.currentObjsCompleted += victoryValue;
			}
			hasBroken = true;
		}
	}
}
