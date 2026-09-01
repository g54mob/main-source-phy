using UnityEngine;

public class PinLock : SimBehaviour
{
	public bool hasLock = true;

	public Rigidbody myRigidbody;

	[HideInInspector]
	public PinBlockKinematic pinBlock;

	public void Release()
	{
		if (base.isSimulating)
		{
			hasLock = false;
			if (myRigidbody != null)
			{
				myRigidbody.interpolation = ((!StatMaster.useSmartInterpolation || Time.timeScale < 0.6f) ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None);
				myRigidbody.isKinematic = false;
				myRigidbody.WakeUp();
			}
			Object.Destroy(this);
		}
	}

	private void OnDestroy()
	{
		base.gameObject.tag = "Untagged";
	}
}
