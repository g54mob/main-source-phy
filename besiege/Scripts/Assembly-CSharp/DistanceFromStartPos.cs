using UnityEngine;

public class DistanceFromStartPos : MonoBehaviour
{
	public float distance;

	public float distanceCutoff = 3f;

	public bool isDestroyed;

	public float scanFrequency = 0.6f;

	public float lastScan;

	private Vector3 startPos;

	private Transform myTransform;

	private void Start()
	{
		myTransform = base.transform;
		startPos = myTransform.position;
		lastScan = Random.Range(0f, scanFrequency);
	}

	private void Update()
	{
		if (!isDestroyed)
		{
			lastScan += Time.deltaTime;
			if (lastScan > scanFrequency)
			{
				CheckDistance();
				lastScan = 0f;
			}
		}
	}

	private void CheckDistance()
	{
		distance = (startPos - myTransform.position).sqrMagnitude;
		if (distance > distanceCutoff)
		{
			isDestroyed = true;
			WinCondition.currentObjsCompleted++;
		}
	}
}
