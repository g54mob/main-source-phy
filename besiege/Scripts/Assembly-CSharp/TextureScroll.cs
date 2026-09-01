using UnityEngine;

public class TextureScroll : MonoBehaviour
{
	public Renderer[] scrollers;

	public Vector2 uvAnimationRate = new Vector2(1f, 0f);

	private Vector2 uvOffset = Vector2.zero;

	private Material[] scrollMaterials;

	private void Start()
	{
		uvOffset = new Vector2(Random.value, Random.value);
		scrollMaterials = new Material[scrollers.Length];
		for (int i = 0; i < scrollers.Length; i++)
		{
			scrollMaterials[i] = scrollers[i].materials[0];
		}
	}

	private void Update()
	{
		uvOffset += uvAnimationRate * Time.deltaTime;
		for (int i = 0; i < scrollMaterials.Length; i++)
		{
			scrollMaterials[i].SetTextureOffset("_MainTex", uvOffset);
		}
	}
}
