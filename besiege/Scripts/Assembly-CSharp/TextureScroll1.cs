using UnityEngine;

public class TextureScroll1 : MonoBehaviour
{
	public Renderer[] scrollers;

	public Vector2 uvAnimationRate = new Vector2(1f, 0f);

	public string texName = "_MainTex";

	private Vector2 uvOffset = Vector2.zero;

	private void Update()
	{
		uvOffset += uvAnimationRate * Time.deltaTime;
		for (int i = 0; i < scrollers.Length; i++)
		{
			scrollers[i].materials[0].SetTextureOffset(texName, uvOffset);
		}
	}
}
