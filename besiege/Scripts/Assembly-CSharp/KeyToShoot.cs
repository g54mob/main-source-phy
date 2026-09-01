using UnityEngine;

public class KeyToShoot : MonoBehaviour
{
	public KeyCode shootKey;

	public ParticleSystem particles;

	public AudioSource audio;

	public GameObject projectile;

	public Transform spawn;

	public float power = 10000f;

	private void Update()
	{
		if (Input.GetKeyDown(shootKey))
		{
			particles.Play();
			audio.Play();
			GameObject gameObject = Object.Instantiate(projectile, spawn.position, spawn.rotation, ReferenceMaster.physicsGoalInstance) as GameObject;
			Rigidbody component = gameObject.GetComponent<Rigidbody>();
			component.AddForce(spawn.forward * power);
		}
	}
}
