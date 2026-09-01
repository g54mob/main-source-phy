using UnityEngine;

public class RandomBirdFlight : SimBehaviour
{
	public Vector3 startPos;

	public Vector3 targetPos;

	public Vector3 smoothPos;

	public float movementMaxRadius = 6f;

	public float movementMaxHeight = 2f;

	public float randomDirectionRate = 0.8f;

	public float movementSpeed = 5f;

	public float lerpSmooth = 10f;

	protected override void Start()
	{
		base.Start();
		startPos = base.transform.localPosition;
		GetRandomPosition();
		smoothPos = startPos;
	}

	private void Update()
	{
		if (base.SimPhysics && (!StatMaster.isMP || base.isSimulating))
		{
			if (CompareVectors(targetPos, base.transform.localPosition))
			{
				GetRandomPosition();
			}
			smoothPos = Vector3.MoveTowards(smoothPos, targetPos, Time.deltaTime * movementSpeed);
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, smoothPos, Time.deltaTime * lerpSmooth);
		}
	}

	private void GetRandomPosition()
	{
		Vector3 vector = startPos + Random.insideUnitSphere * movementMaxRadius;
		targetPos = new Vector3(vector.x, startPos.y + Random.value * movementMaxHeight, vector.z);
	}

	private bool CompareVectors(Vector3 vectorOne, Vector3 vectorTwo)
	{
		return (vectorOne - vectorTwo).sqrMagnitude <= (vectorOne * 0.1f).sqrMagnitude;
	}
}
