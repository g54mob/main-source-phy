using UnityEngine;

[RequireComponent(typeof(Camera))]
public class VRFlash : MonoBehaviour
{
	private static readonly int FlashColor = Shader.PropertyToID("_Color");

	public Material flashMaterial;

	public float flashAlpha;

	private Material usedMaterial;

	private void Awake()
	{
		usedMaterial = new Material(flashMaterial);
	}

	private void Update()
	{
		usedMaterial.SetColor(FlashColor, new Color(1f, 1f, 1f, flashAlpha));
	}

	private void OnPostRender()
	{
		if (flashAlpha != 0f)
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
