using UnityEngine;

public class POVCam : ClickBehaviour
{
	public static float distanceToMachine = 100f;

	public Transform headPosition;

	public bool activey;

	public bool isDead;

	private void Start()
	{
		releaseOnlyOver = true;
		if (activey)
		{
			CamChange();
		}
	}

	public override void OnClickReleased()
	{
		CamChange();
	}

	private void CamChange()
	{
		STATLORD.activeHumanPOV = this;
		STATLORD.povMode = true;
		activey = true;
	}
}
