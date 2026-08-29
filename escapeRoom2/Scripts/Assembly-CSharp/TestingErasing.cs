using UnityEngine;
using UnityEngine.Rendering;

public class TestingErasing : MonoBehaviour
{
	public GameObject board;

	public RenderTexture rt;

	public Texture brush;

	private Mesh quadMesh;

	public Material blendMaterial;

	private void Start()
	{
		Material material = board.GetComponent<Renderer>().material;
		Texture texture = material.GetTexture("_MainTex");
		RenderTextureFormat format = RenderTextureFormat.Default;
		if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32) && SystemInfo.SupportsBlendingOnRenderTextureFormat(RenderTextureFormat.ARGB32))
		{
			format = RenderTextureFormat.ARGB32;
		}
		rt = new RenderTexture(texture.width, texture.height, 24, format);
		rt.name = "BoardTexture";
		rt.filterMode = texture.filterMode;
		Graphics.Blit(texture, rt);
		material.SetTexture("_MainTex", rt);
		quadMesh = new Mesh
		{
			vertices = new Vector3[4]
			{
				Vector3.up,
				new Vector3(1f, 1f, 0f),
				Vector3.right,
				Vector3.zero
			},
			uv = new Vector2[4]
			{
				Vector2.up,
				Vector2.one,
				Vector2.right,
				Vector2.zero
			},
			triangles = new int[6] { 0, 1, 2, 2, 3, 0 },
			colors = new Color[4]
			{
				Color.white,
				Color.white,
				Color.white,
				Color.white
			}
		};
	}

	private void Update()
	{
		Collider componentInChildren = board.GetComponentInChildren<Collider>();
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (componentInChildren.Raycast(ray, out var hitInfo, 100f))
			{
				Vector3 vector = board.transform.InverseTransformPoint(hitInfo.point);
				Vector2 vector2 = new Vector2(vector.x / 10f + 0.5f, vector.z / 10f + 0.5f);
				GL.LoadOrtho();
				CommandBuffer commandBuffer = new CommandBuffer();
				commandBuffer.SetRenderTarget(rt);
				Vector3 s = Vector3.one * 0.1f;
				commandBuffer.DrawMesh(quadMesh, Matrix4x4.TRS(Vector3.one - new Vector3(vector2.x + s.x * 0.5f, vector2.y + s.y * 0.5f, 0f), Quaternion.identity, s), blendMaterial);
				Graphics.ExecuteCommandBuffer(commandBuffer);
			}
		}
	}
}
