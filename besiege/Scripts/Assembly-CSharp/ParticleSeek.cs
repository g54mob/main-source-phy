using UnityEngine;

public class ParticleSeek : MonoBehaviour
{
	public Transform target;

	public float force = 10f;

	public ParticleSystem particleSystem;

	private void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
	}

	private void LateUpdate()
	{
		ParticleSystem.Particle[] array = new ParticleSystem.Particle[particleSystem.particleCount];
		particleSystem.GetParticles(array);
		float num = force * Time.deltaTime;
		Vector3 position = target.position;
		for (int i = 0; i < array.Length; i++)
		{
			Vector3 vector = ((particleSystem.simulationSpace != ParticleSystemSimulationSpace.Local) ? array[i].position : base.transform.TransformPoint(array[i].position));
			Vector3 vector2 = Vector3.Normalize(position - vector);
			Vector3 vector3 = vector2 * num;
			array[i].velocity += vector3;
		}
		particleSystem.SetParticles(array, array.Length);
	}
}
