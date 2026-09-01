using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
	public float spawnEffectTime;

	public float pause;

	public AnimationCurve fadeIn;

	private ParticleSystem ps;

	private float timer;

	private Renderer _renderer;

	private int shaderProperty;

	private void Start()
	{
	}

	private void Update()
	{
	}
}
