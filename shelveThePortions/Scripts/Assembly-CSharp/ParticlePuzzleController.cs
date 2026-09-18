using UnityEngine;

public class ParticlePuzzleController : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem pSystem;

	public void SetController(Mesh particleMesh, int orderInSet)
	{
		orderInSet++;
		pSystem.transform.localScale = Vector3.one;
		pSystem.transform.localPosition = new Vector3(0f, 0.2f, 0f);
		pSystem.transform.localEulerAngles = new Vector3(-30f, 0f, 0f);
		ParticleSystem.MainModule main = pSystem.main;
		main.maxParticles = orderInSet;
		pSystem.GetComponent<ParticleSystemRenderer>().mesh = particleMesh;
	}

	public void SetControllerLayer(int layer)
	{
		base.gameObject.layer = layer;
	}
}
