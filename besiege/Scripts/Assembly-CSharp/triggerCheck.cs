using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Levels/Trigger Check")]
public class triggerCheck : MonoBehaviour
{
	[Header("ForceBlast")]
	public Transform forceSpawnPoint;

	public float forceRadius = 5f;

	public float appliedForce = 100f;

	public List<Rigidbody> hitRigidbodies = new List<Rigidbody>();

	public GameObject target;

	public GameObject Safe;

	public GameObject Geyser;

	public GameObject Raindrops;

	public GameObject Steam;

	[Header("Delays")]
	public float WinDelay;

	public float GeyserDelay;

	public float forceDelay = 0.3f;

	private bool payoff;

	public Rigidbody rb;

	public RandomSoundController audioGeyser;

	private void Start()
	{
		GeyserDelay = WinDelay - GeyserDelay;
	}

	private void Update()
	{
		if (WinCondition.hasWon && !payoff)
		{
			StartCoroutine(SafeAnimation());
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent.name == target.name)
		{
			WinCondition.currentObjsCompleted++;
			rb = other.attachedRigidbody;
		}
	}

	private IEnumerator SafeAnimation()
	{
		payoff = true;
		Geyser.SetActive(true);
		Animator anim = Safe.GetComponent<Animator>();
		anim.enabled = true;
		anim.speed = 0.65f;
		RandomSoundController audioSafe = Safe.GetComponent<RandomSoundController>();
		audioSafe.Stop();
		audioSafe.Play();
		yield return new WaitForSeconds(GeyserDelay);
		audioGeyser.Play();
		Geyser.GetComponent<Animator>().enabled = true;
		Raindrops.SetActive(true);
		Steam.SetActive(true);
		yield return new WaitForSeconds(forceDelay);
		audioSafe.Stop();
		Collider[] hitColliders = Physics.OverlapSphere(forceSpawnPoint.position, forceRadius);
		for (int i = 0; i < hitColliders.Length; i++)
		{
			if (!(hitColliders[i].transform.parent.name == target.name) && (bool)hitColliders[i].attachedRigidbody && !hitRigidbodies.Contains(hitColliders[i].attachedRigidbody))
			{
				hitRigidbodies.Add(hitColliders[i].attachedRigidbody);
			}
		}
		if (hitRigidbodies.Count > 0)
		{
			for (int j = 0; j < hitRigidbodies.Count; j++)
			{
				Vector3 direction = hitRigidbodies[j].transform.position - forceSpawnPoint.position;
				hitRigidbodies[j].AddForce(direction * appliedForce, ForceMode.Impulse);
			}
		}
	}
}
