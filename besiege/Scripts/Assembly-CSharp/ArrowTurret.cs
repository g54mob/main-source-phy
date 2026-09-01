using System.Collections;
using UnityEngine;

public class ArrowTurret : MonoBehaviour
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

	public float force = 1f;

	public AudioSource audioSource;

	public float lastScan;

	private RaycastHit hit;

	private Ray ray;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (StatMaster.levelSimulating)
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
		if (hasShot && !StatMaster.GodTools.InfiniteAmmoMode)
		{
			yield break;
		}
		hasShot = true;
		fuseParticles.Play();
		float delay = 0f;
		delay = (shootInstantlyOnCommand ? Random.Range(0f, 0.1f) : (fuseDelay + Random.Range(0f - randomDelay, randomDelay)));
		yield return new WaitForSeconds(delay);
		fuseParticles.Stop();
		particles.Play();
		audioSource.pitch = 1f + Random.Range(-0.1f, 0.1f);
		audioSource.Play();
		if (!StatMaster.Rules.DisableProjectiles)
		{
			Transform bolt = null;
			if (StatMaster.isHosting)
			{
				byte[] spawnInfo = new byte[13];
				int offset = 0;
				NetworkCompression.CompressPosition(boltSpawnPos.position, spawnInfo, offset);
				offset += 6;
				NetworkCompression.CompressRotation(boltSpawnPos.rotation, spawnInfo, offset);
				NetworkAddPiece addPiece = NetworkAddPiece.Instance;
				bolt = ProjectileManager.Instance.Spawn(NetworkProjectileType.Cannon, addPiece.frame, ushort.MaxValue, spawnInfo);
			}
			else if (!StatMaster.isMP)
			{
				bolt = (Object.Instantiate(boltObject, boltSpawnPos.position, boltSpawnPos.rotation) as GameObject).transform;
			}
			if (bolt != null)
			{
				PostShoot(bolt);
			}
		}
	}

	private void PostShoot(Transform bolt)
	{
		Rigidbody component = bolt.GetComponent<Rigidbody>();
		component.velocity = parentRigidbody.velocity;
		Vector3 forward = base.transform.forward;
		component.AddForce(forward * (boltSpeed * force + (float)Random.Range(-700, 0)));
		parentRigidbody.AddForce(forward * (0f - knockbackSpeed) * force);
		if (grappleHook)
		{
			bolt.GetComponent<ConfigurableJoint>().connectedBody = parentRigidbody;
			bolt.GetComponent<DrawLineRender>().targetObj = ropeJointPos;
		}
	}
}
