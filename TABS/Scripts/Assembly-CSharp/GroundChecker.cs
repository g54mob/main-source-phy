using Landfall.MonoBatch;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : BatchedMonobehaviour
{
	public bool isActive = true;

	public float sinceGrounded;

	public Vector3 groundPos;

	public bool reportToDataHandler = true;

	public UnityEvent footDownEvent;

	public bool ignoreRigidbodies;

	private UnitSounds unitSounds;

	private DataHandler data;

	private Transform transformRoot;

	private void Awake()
	{
		transformRoot = base.transform.root;
		unitSounds = transformRoot.GetComponentInChildren<UnitSounds>();
		data = GetComponentInParent<DataHandler>();
	}

	public override void BatchedUpdate()
	{
		sinceGrounded += Time.deltaTime;
	}

	public void OnCollisionEnter(Collision collision)
	{
		Collide(collision);
	}

	public void OnCollisionStay(Collision collision)
	{
		Collide(collision);
	}

	private void Collide(Collision collision)
	{
		if (!isActive || (object)collision.transform.root == transformRoot)
		{
			return;
		}
		Rigidbody rigidbody = collision.rigidbody;
		bool flag = rigidbody != null;
		if (ignoreRigidbodies && flag)
		{
			return;
		}
		ContactPoint contact = collision.GetContact(0);
		float num = Vector3.Angle(Vector3.up, contact.normal);
		if (num > 80f || (flag && num > 40f && rigidbody.mass < 100f && rigidbody.gameObject.layer != 9))
		{
			return;
		}
		groundPos = contact.point;
		if (sinceGrounded > 0.15f)
		{
			if ((bool)unitSounds)
			{
				unitSounds.PlayFootSound(groundPos, SoundEffectVariations.GetMaterialTypeFromCollision(collision));
			}
			footDownEvent.Invoke();
		}
		sinceGrounded = 0f;
		if (reportToDataHandler)
		{
			data.TouchGround(groundPos, contact.normal, collision.rigidbody);
		}
	}
}
