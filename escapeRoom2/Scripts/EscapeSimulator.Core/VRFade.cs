using UnityEngine;

[RequireComponent(typeof(Camera))]
public class VRFade : MonoBehaviour
{
	private static readonly int FadeColor = Shader.PropertyToID("_Color");

	public Material fadeMaterial;

	public float fadeAlpha;

	private Material usedMaterial;

	private void Awake()
	{
		usedMaterial = new Material(fadeMaterial);
	}

	private void Update()
	{
		usedMaterial.SetColor(FadeColor, new Color(0f, 0f, 0f, fadeAlpha));
	}

	private void OnPostRender()
	{
		if (fadeAlpha != 0f)
		{
			GL.PushMatrix();
			usedMaterial.SetPass(0);
			GL.LoadOrtho();
			GL.Begin(7);
			GL.Color(Color.white);
			GL.Vertex3(0f, 0f, 0f);
			GL.Vertex3(0f, 1f, 0f);
			GL.Vertex3(1f, 1f, 0f);
			GL.Vertex3(1f, 0f, 0f);
			GL.End();
			GL.PopMatrix();
		}
	}
}
