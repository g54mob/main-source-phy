using UnityEngine;

public class BlackHoleForce : MonoBehaviour
{
	public float radius = 7f;

	public float power = 1800f;

	public float torquePower = 100000f;

	public float upPower = 3f;

	public AddPiece addPieceCode;

	public Vector3 explosionPos;

	public Transform ExitPos;

	private Rigidbody myAttachedRigidbody;

	private Rigidbody prevRigidbody;

	private Collider[] colliders;

	private Transform myTransform;

	private bool executeFrame;

	private void Start()
	{
		InvokeRepeating("GetColliders", 0.1f, 0.1f);
		myTransform = base.transform;
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating)
		{
			explosionPos = myTransform.position;
			Explodey();
		}
	}

	private void GetColliders()
	{
		colliders = Physics.OverlapSphere(explosionPos, radius);
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((bool)other.attachedRigidbody && !other.attachedRigidbody.isKinematic)
		{
			other.attachedRigidbody.position = ExitPos.position;
		}
	}

	private void Explodey()
	{
		if (!executeFrame)
		{
			executeFrame = true;
			return;
		}
		executeFrame = false;
		Collider[] array = colliders;
		foreach (Collider collider in array)
		{
			if (collider == null)
			{
				continue;
			}
			if ((bool)collider.attachedRigidbody)
			{
				myAttachedRigidbody = collider.attachedRigidbody;
			}
			if (myAttachedRigidbody != null && myAttachedRigidbody != prevRigidbody && myAttachedRigidbody != GetComponent<Rigidbody>() && myAttachedRigidbody.gameObject.layer != 22 && myAttachedRigidbody.tag != "KeepConstraintsAlways")
			{
				myAttachedRigidbody.WakeUp();
				myAttachedRigidbody.constraints = RigidbodyConstraints.None;
				myAttachedRigidbody.AddForce((explosionPos - myAttachedRigidbody.position).normalized * (0f - power));
				myAttachedRigidbody.AddTorque((Vector3.up + Vector3.right) * torquePower);
				if (myAttachedRigidbody.gameObject.GetComponent<SimpleBirdAI>() != null)
				{
					myAttachedRigidbody.gameObject.GetComponent<SimpleBirdAI>().Explode();
				}
				if (myAttachedRigidbody.gameObject.GetComponent<BlockHealthBar>() != null)
				{
					myAttachedRigidbody.gameObject.GetComponent<BlockHealthBar>().DamageBlock(1f);
				}
				if (myAttachedRigidbody.gameObject.GetComponent<BreakOnForce>() != null)
				{
					myAttachedRigidbody.gameObject.GetComponent<BreakOnForce>().BreakExplosion(power, explosionPos, radius, upPower);
				}
				if (myAttachedRigidbody.gameObject.GetComponent<BreakOnForceNoSpawn>() != null)
				{
					myAttachedRigidbody.gameObject.GetComponent<BreakOnForceNoSpawn>().BreakExplosion(power, explosionPos, radius, upPower);
				}
				prevRigidbody = myAttachedRigidbody;
			}
		}
	}
}
