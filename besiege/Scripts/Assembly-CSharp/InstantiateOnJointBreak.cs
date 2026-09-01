using UnityEngine;

public class InstantiateOnJointBreak : BreakBase
{
	public Transform objToSpawn;

	[SerializeField]
	private Transform sendBrokenMessage;

	[SerializeField]
	private bool alsoOnCollision;

	[SerializeField]
	private float force = 200f;

	private float forceSqr = 200f;

	protected override void Awake()
	{
		base.Awake();
		forceSqr = force * force;
	}

	private void OnJointBreak()
	{
		Break();
	}

	private void AddToPercentageBar()
	{
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
		}
	}

	private void Break()
	{
		AddToPercentageBar();
		Transform transform = Object.Instantiate(objToSpawn, base.transform.position, base.transform.rotation) as Transform;
		transform.parent = base.transform.parent;
		transform.localScale = base.transform.localScale;
		if (sendBrokenMessage != null)
		{
			sendBrokenMessage.SendMessage("Break");
		}
		OnBreak();
		Object.Destroy(base.gameObject);
	}

	protected virtual void OnCollisionEnter(Collision collision)
	{
		if (alsoOnCollision && base.enabled && StatMaster.levelSimulating && collision.relativeVelocity.sqrMagnitude > forceSqr)
		{
			Break();
		}
	}
}
