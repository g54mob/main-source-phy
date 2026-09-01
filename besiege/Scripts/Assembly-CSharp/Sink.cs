using UnityEngine;

public class Sink : MonoBehaviour
{
	[SerializeField]
	protected BasicInfo bInfo;

	[SerializeField]
	protected float sinkDensity = 5f;

	[SerializeField]
	protected bool useSubmergeTimer;

	[SerializeField]
	protected float timeSubmerged = 1f;

	private float submergedTimer;

	[SerializeField]
	protected bool useSinkAngle;

	[SerializeField]
	protected float maxDotAngle;

	private bool sunken;

	public Behaviour[] disableComponents;

	public int victoryPoints;

	private void Update()
	{
		if (!bInfo.SimPhysics || !bInfo._inWater || sunken)
		{
			return;
		}
		if (bInfo.submergedPercent >= 1f)
		{
			if (submergedTimer >= timeSubmerged)
			{
				SinkObject();
			}
			else
			{
				submergedTimer += Time.deltaTime;
			}
		}
		else if (submergedTimer != 0f)
		{
			submergedTimer = 0f;
		}
		if (useSinkAngle)
		{
			float num = Vector3.Dot(Vector3.up, bInfo.Rigidbody.transform.up);
			if (num < maxDotAngle)
			{
				SinkObject();
			}
		}
	}

	public void SinkObject()
	{
		sunken = true;
		bInfo.density = sinkDensity;
		for (int i = 0; i < disableComponents.Length; i++)
		{
			disableComponents[i].enabled = false;
		}
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted += victoryPoints;
			base.gameObject.tag = "Untagged";
		}
	}
}
