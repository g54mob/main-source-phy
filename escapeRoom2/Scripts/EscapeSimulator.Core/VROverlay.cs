using UnityEngine;

[RequireComponent(typeof(Camera))]
public class VROverlay : MonoBehaviour
{
	private static readonly int ColorPropertyId = Shader.PropertyToID("_Color");

	public Color color;

	public Material material;

	private Material materialCopy;

	private void Awake()
	{
		materialCopy = new Material(material);
	}

	private void Update()
	{
		materialCopy.SetColor(ColorPropertyId, color);
	}

	private void OnPostRender()
	{
		if (color.a != 0f)
		{
			GL.PushMatrix();
			materialCopy.SetPass(0);
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
