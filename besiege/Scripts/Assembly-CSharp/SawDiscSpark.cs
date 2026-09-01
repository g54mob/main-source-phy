using UnityEngine;

public class SawDiscSpark : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem sparks;

	private ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);

	[SerializeField]
	private int particles = 30;

	private ContactPoint contact;

	private void Start()
	{
		emitParams.applyShapeToPosition = true;
	}

	private void Update()
	{
	}

	private void OnCollisionEnter(Collision other)
	{
		contact = other.contacts[0];
		emitParams.position = contact.point;
		sparks.transform.rotation = Quaternion.LookRotation(Quaternion.Euler(-90f, 0f, 0f) * contact.normal);
		sparks.Emit(emitParams, particles);
	}
}
