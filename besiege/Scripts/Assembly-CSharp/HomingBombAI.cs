using UnityEngine;

[AddComponentMenu("Physics/AI/HomingBombAI")]
public class HomingBombAI : MonoBehaviour
{
	public Transform target;

	public Rigidbody myRigidbody;

	public float moveSpeed = 1000f;

	private Vector3 explosionPos;

	private Collider[] colliders;

	public float radius = 5f;

	public float power = 10f;

	public float torquePower = 1000f;

	public float upPower = 6f;

	private Rigidbody prevRigidbody;

	private Rigidbody myAttachedRigidbody;

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			myRigidbody = GetComponent<Rigidbody>();
			target = Machine.Active().GetRandomBlock().transform;
			myRigidbody.isKinematic = false;
		}
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating)
		{
			myRigidbody.AddForce((myRigidbody.position - target.position).normalized * moveSpeed);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		Rigidbody attachedRigidbody = other.collider.attachedRigidbody;
		if ((bool)attachedRigidbody && (bool)attachedRigidbody.GetComponent<BlockBehaviour>())
		{
			Explode();
		}
	}

	private void Explode()
	{
		explosionPos = base.transform.position;
		colliders = Physics.OverlapSphere(explosionPos, radius);
		Collider[] array = colliders;
		foreach (Collider collider in array)
		{
			if (!(collider == null))
			{
				if (collider.attachedRigidbody != null)
				{
					myAttachedRigidbody = collider.attachedRigidbody;
				}
				if (myAttachedRigidbody != null && myAttachedRigidbody != prevRigidbody && myAttachedRigidbody != GetComponent<Rigidbody>() && myAttachedRigidbody.gameObject.layer != 22 && myAttachedRigidbody.tag != "KeepConstraintsAlways")
				{
					myAttachedRigidbody.WakeUp();
					myAttachedRigidbody.constraints = RigidbodyConstraints.None;
					myAttachedRigidbody.AddExplosionForce(power, explosionPos, radius, upPower);
					myAttachedRigidbody.AddRelativeTorque(Random.insideUnitSphere.normalized * torquePower);
					prevRigidbody = myAttachedRigidbody;
				}
			}
		}
		if (!StatMaster.isMP)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}
}
