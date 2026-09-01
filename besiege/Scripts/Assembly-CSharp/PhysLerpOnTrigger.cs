using System.Collections;
using UnityEngine;

public class PhysLerpOnTrigger : MonoBehaviour
{
	public Rigidbody objToLerp;

	public Vector3 moveDir;

	public float duration = 0.2f;

	public float delayAfterTrigger;

	public ParticleSystem particleSys;

	private bool isMoving;

	private bool canMove;

	protected void Start()
	{
		delayAfterTrigger *= Random.Range(0.7f, 1.3f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!StatMaster.isClient && StatMaster.levelSimulating && !isMoving && (bool)other.attachedRigidbody)
		{
			StartCoroutine(Lerpy());
		}
	}

	private IEnumerator Lerpy()
	{
		isMoving = true;
		yield return new WaitForSeconds(delayAfterTrigger);
		if (particleSys != null)
		{
			particleSys.Play();
		}
		canMove = true;
		yield return new WaitForSeconds(duration);
		canMove = false;
	}

	private void FixedUpdate()
	{
		if (!StatMaster.isClient && StatMaster.levelSimulating && canMove)
		{
			objToLerp.MovePosition(objToLerp.position + moveDir * Time.deltaTime);
		}
	}
}
