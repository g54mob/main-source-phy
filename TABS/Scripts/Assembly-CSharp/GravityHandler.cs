using Landfall.MonoBatch;
using TFBGames;
using UnityEngine;

public class GravityHandler : BatchedMonobehaviour
{
	public GravityData gravityData;

	private RigidbodyHolder rigidbodyHolder;

	private DataHandler data;

	protected override void Start()
	{
		base.Start();
		rigidbodyHolder = GetComponent<RigidbodyHolder>();
		data = GetComponent<DataHandler>();
	}

	public override void BatchedFixedUpdate()
	{
		if (data.Dead || data.isGrounded || data.muscleControl < 0.5f)
		{
			return;
		}
		if (Bugs.gravityBug)
		{
			data.sinceGrounded = 0f;
			data.lastPos = data.mainRig.position;
			Rigidbody[] allRigs = rigidbodyHolder.AllRigs;
			for (int i = 0; i < allRigs.Length; i++)
			{
				allRigs[i].AddForce(FixedTimeStepService.LargeForceCoefficient * 0.3f * data.sinceGrounded * gravityData.gravity * Vector3.up, ForceMode.Acceleration);
			}
		}
		else
		{
			Rigidbody[] allRigs2 = rigidbodyHolder.AllRigs;
			for (int j = 0; j < allRigs2.Length; j++)
			{
				allRigs2[j].AddForce(FixedTimeStepService.LargeForceCoefficient * data.sinceGrounded * gravityData.gravity * Vector3.down, ForceMode.Acceleration);
			}
		}
	}
}
