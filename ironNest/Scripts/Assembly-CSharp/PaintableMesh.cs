using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PaintableMesh : MonoBehaviour
{
	public int TextureSize;

	public string MaterialTextureProperty;

	public float InteractionRange;

	[Header("Brush")]
	public Color BrushColor;

	public float BrushSize;

	public float BrushScaleX;

	public float BrushScaleY;

	[Range(0f, 1f)]
	public float BrushSoftness;

	public Shader DrawShader;

	public Camera PaintCamera;

	public RenderTexture PaintTexture;

	private MeshRenderer meshRenderer;

	private Material runtimeMaterial;

	private Texture2D cpuBrush;

	private Material drawMaterial;

	private Vector2? lastPaintUv;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void TryPaint(Vector2 screenPos)
	{
	}

	private void PaintStroke(Vector2 uv)
	{
	}

	public void PaintAtUV(Vector2 uv)
	{
	}

	private Texture2D CreateCircularBrush(int size)
	{
		return null;
	}

	public void ClearTexture()
	{
	}
}
