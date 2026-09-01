using System.Collections;
using UnityEngine;

public class ShrapnelCannon : MonoBehaviour
{
	public string searchTag = "Respawn";

	public float scanFrequency = 1f;

	public Transform boltObject;

	public Transform boltSpawnPos;

	public float boltSpeed = 100f;

	public float knockbackSpeed = 100f;

	public Rigidbody parentRigidbody;

	public LayerMask layerMasky;

	public ParticleSystem particles;

	public bool hasShot;

	public float fuseDelay = 1.5f;

	public ParticleSystem fuseParticles;

	public bool grappleHook;

	public Transform ropeJointPos;

	public float randomDelay = 0.1f;

	public bool shootInstantlyOnCommand;

	private RaycastHit hit;

	private Ray ray;

	private AudioSource sfx;

	private float lastScan;

	private Machine machine;

	private void Start()
	{
		sfx = GetComponent<AudioSource>();
		machine = GetComponentInParent<Machine>();
	}

	private void Update()
	{
		if (machine.SimPhysics)
		{
			lastScan += Time.deltaTime;
			if (lastScan > scanFrequency)
			{
				lastScan = 0f;
			}
		}
	}

	private Transform GetNearestTaggedObject()
	{
		float num = float.PositiveInfinity;
		GameObject[] array = GameObject.FindGameObjectsWithTag(searchTag);
		Transform result = null;
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			Vector3 position = gameObject.transform.position;
			float sqrMagnitude = (position - base.transform.position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				result = gameObject.transform;
				num = sqrMagnitude;
			}
		}
		return result;
	}

	public IEnumerator Shoot()
	{
		if (!hasShot || machine.InfiniteAmmoMode)
		{
			hasShot = true;
			float delay = 0f;
			delay = (shootInstantlyOnCommand ? Random.Range(0f, 0.1f) : (fuseDelay + Random.Range(0f - randomDelay, randomDelay)));
			yield return new WaitForSeconds(delay);
			fuseParticles.Play();
			particles.Play();
			sfx.pitch = 1f + Random.Range(-0.1f, 0.1f);
			sfx.Play();
			Object.Instantiate(boltObject, boltSpawnPos.position, boltSpawnPos.rotation);
			if (parentRigidbody != null)
			{
				parentRigidbody.AddForce(base.transform.forward * (0f - knockbackSpeed));
			}
		}
	}
}
