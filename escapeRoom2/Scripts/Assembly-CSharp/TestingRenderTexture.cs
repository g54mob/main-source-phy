using UnityEngine;

public class TestingRenderTexture : MonoBehaviour
{
	public RenderTexture renderTexture;

	private void Start()
	{
		renderTexture = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGB32);
		GetComponent<Camera>();
	}

	private void Update()
	{
		Camera component = GetComponent<Camera>();
		component.targetTexture = renderTexture;
		component.Render();
	}
}
