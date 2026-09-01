using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
	private bool freezeRot;

	private Quaternion startRot;

	private void Start()
	{
		startRot = base.transform.rotation;
	}

	private void Update()
	{
		if (freezeRot)
		{
			_ = startRot;
			startRot = base.transform.rotation;
			base.transform.rotation = startRot;
		}
	}

	public void Freeze()
	{
		freezeRot = true;
	}

	public void UnFreeze()
	{
		freezeRot = false;
	}
}
