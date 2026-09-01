using UnityEngine;

public class RupeeController : MonoBehaviour
{
	public Renderer rupeeRenderer;

	public Material[] materials;

	public float chanceOfEnabling = 0.3f;

	private void Start()
	{
		rupeeRenderer.enabled = false;
		if (Random.value <= chanceOfEnabling)
		{
			EnableRupee();
		}
	}

	private void EnableRupee()
	{
		rupeeRenderer.enabled = true;
		GetComponent<AudioSource>().Play();
		rupeeRenderer.material = materials[Random.Range(0, materials.Length)];
	}
}
