using UnityEngine;

public class TerrainParticleCollision : MonoBehaviour
{
	protected ParticleSystem.EmitParams emitter = default(ParticleSystem.EmitParams);

	public Vector3 offset = Vector3.zero;

	private void OnCollisionEnter(Collision collision)
	{
		emitter.applyShapeToPosition = true;
		emitter.position = collision.contacts[0].point + offset;
		GlobalParticles.EmitParticleBursts(10, emitter);
	}
}
