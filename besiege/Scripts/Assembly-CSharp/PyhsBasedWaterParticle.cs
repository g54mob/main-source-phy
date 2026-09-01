using UnityEngine;

public class PyhsBasedWaterParticle : MonoBehaviour
{
	public Transform sphereParent;

	public Transform spawnPos;

	public int currentIndex;

	public float power = 100f;

	public Rigidbody parentRigidbody;

	public bool isActive;

	public float spawnRate = 0.1f;

	public float randomAngle = 0.2f;

	private float timer;

	private Machine machine;

	private Rigidbody[] spheres;

	private ParticleSystem[] sphereParticle;

	private void Start()
	{
		machine = GetComponentInParent<Machine>();
		spheres = new Rigidbody[sphereParent.childCount];
		sphereParticle = new ParticleSystem[sphereParent.childCount];
		for (int i = 0; i < sphereParent.childCount; i++)
		{
			spheres[i] = sphereParent.GetChild(i).GetComponent<Rigidbody>();
			sphereParticle[i] = sphereParent.GetChild(i).FindChild("MainParticle").GetComponent<ParticleSystem>();
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown("o"))
		{
			isActive = !isActive;
		}
		if (isActive)
		{
			timer += Time.deltaTime;
			if (timer >= spawnRate)
			{
				timer = 0f;
				SpawnSphere();
			}
		}
	}

	private void SpawnSphere()
	{
		if ((bool)machine && machine.SimPhysics)
		{
			Rigidbody rigidbody = spheres[spheres.Length - 1];
			rigidbody.isKinematic = true;
			rigidbody.position = spawnPos.position;
			rigidbody.GetComponent<Collider>().enabled = false;
			Rigidbody rigidbody2 = spheres[currentIndex];
			rigidbody2.position = spawnPos.position;
			rigidbody2.isKinematic = false;
			rigidbody2.GetComponent<Collider>().enabled = true;
			rigidbody2.velocity = spawnPos.forward * power + parentRigidbody.velocity + randomAngle * Random.insideUnitSphere;
		}
		sphereParticle[currentIndex].Play();
		currentIndex++;
		if (currentIndex >= spheres.Length)
		{
			currentIndex = 0;
		}
	}
}
