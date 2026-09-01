using UnityEngine;

public class BoatHitManager : MonoBehaviour
{
	public float uprightTorque = 100f;

	public Transform jointBreakFulcrum;

	public float force;

	[SerializeField]
	private BoatHullController hullController;

	private Rigidbody rb;

	private Collider lastHit;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!(lastHit == collision.collider))
		{
			lastHit = collision.collider;
			if (lastHit.gameObject.GetComponent<ProjectileInfo>() != null)
			{
				collision.collider.gameObject.SetActive(false);
			}
			hullController.HullHit(rb, collision.contacts[0].point);
		}
	}

	private void OnJointBreak(float breakForce)
	{
		rb.AddExplosionForce(force, jointBreakFulcrum.position, 0.5f);
		hullController.BreakHull(rb);
		hullController.StartSinking();
		Debug.Log("Broke Joint");
	}

	private void Update()
	{
		if (!hullController.isSinking && uprightTorque != 0f)
		{
			Quaternion quaternion = Quaternion.FromToRotation(base.transform.up, Vector3.up);
			rb.AddTorque(new Vector3(quaternion.x, quaternion.y, quaternion.z) * uprightTorque);
		}
	}
}
