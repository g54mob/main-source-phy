using UnityEngine;

public class ErasingV2 : MonoBehaviour
{
	public Texture2D original;

	public Texture2D modified;

	public Texture2D brush;

	public float brushScale = 1f;

	public Renderer target;

	public int targetMaterialIndex;

	public MeshCollider targetMeshCollider;

	public Shader blitAdd;

	public Shader blitCombine;

	private RenderTexture mask;

	private RenderTexture result;

	private Material blitAddMaterial;

	private Material blitCombineMaterial;

	private void Start()
	{
		Material obj = target.materials[targetMaterialIndex];
		RenderTextureFormat format = RenderTextureFormat.Default;
		if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32) && SystemInfo.SupportsBlendingOnRenderTextureFormat(RenderTextureFormat.ARGB32))
		{
			format = RenderTextureFormat.ARGB32;
		}
		result = new RenderTexture(original.width, original.height, 0, format);
		result.filterMode = original.filterMode;
		mask = new RenderTexture(original.width, original.height, 0, format);
		mask.filterMode = original.filterMode;
		mask.wrapMode = TextureWrapMode.Clamp;
		Graphics.Blit(original, result);
		obj.SetTexture("_MainTex", result);
		blitAddMaterial = new Material(blitAdd);
		blitCombineMaterial = new Material(blitCombine);
		blitCombineMaterial.SetTexture("_Mask", mask);
		blitCombineMaterial.SetTexture("_Overlay", modified);
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (targetMeshCollider.Raycast(ray, out var hitInfo, 10000f))
			{
				Vector2 vector = new Vector2((float)brush.width * brushScale, (float)brush.height * brushScale);
				Vector2 vector2 = new Vector2((float)original.width * hitInfo.textureCoord.x, (float)original.height * (1f - hitInfo.textureCoord.y));
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = mask;
				GL.PushMatrix();
				GL.LoadPixelMatrix(0f, mask.width, mask.height, 0f);
				Graphics.DrawTexture(new Rect(vector2.x - vector.x * 0.5f, vector2.y - vector.y * 0.5f, vector.x, vector.y), brush, blitAddMaterial);
				GL.PopMatrix();
				RenderTexture.active = result;
				GL.PushMatrix();
				GL.LoadPixelMatrix(0f, result.width, result.height, 0f);
				Graphics.DrawTexture(new Rect(0f, 0f, result.width, result.height), original, blitCombineMaterial);
				GL.PopMatrix();
				RenderTexture.active = active;
			}
		}
	}
}
