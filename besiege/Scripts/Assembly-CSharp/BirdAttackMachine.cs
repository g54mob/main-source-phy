using System.Collections;
using UnityEngine;

public class BirdAttackMachine : MonoBehaviour
{
	public Transform myTarget;

	public float speed;

	public Rigidbody myRigidbody;

	public bool canAttack;

	public float randomAmount = 100f;

	public float smoothRand = 10f;

	private Vector3 randForce;

	private IEnumerator Start()
	{
		canAttack = false;
		if (StatMaster.levelSimulating)
		{
			yield return null;
			GetNewTarget();
			myRigidbody.isKinematic = false;
			canAttack = true;
		}
	}

	private void FixedUpdate()
	{
		if (canAttack)
		{
			if (myTarget != null)
			{
				myRigidbody.AddForce((myTarget.position - myRigidbody.position).normalized * speed);
				randForce = Vector3.Lerp(randForce, Random.insideUnitSphere, Time.deltaTime * smoothRand);
				myRigidbody.AddForce(randForce * randomAmount);
			}
			else
			{
				GetNewTarget();
			}
		}
	}

	private void GetNewTarget()
	{
		if (!(Machine.Active() == null))
		{
			myTarget = Machine.Active().GetRandomBlock().transform;
		}
	}

	private void Explodey()
	{
	}
}
