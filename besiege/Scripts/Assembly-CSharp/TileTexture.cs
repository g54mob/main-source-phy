using UnityEngine;

[ExecuteInEditMode]
public class TileTexture : MonoBehaviour
{
	public Texture texture;

	public float textureToMeshZ = 2f;

	private MeshRenderer render;

	private MaterialPropertyBlock tiler;

	private float prevTextureToMeshZ = -1f;

	private Vector3 prevScale = Vector3.one;

	private Vector3 prevPos = Vector3.zero;

	private void Assign()
	{
		render = base.gameObject.GetComponent<MeshRenderer>();
		tiler = new MaterialPropertyBlock();
	}

	private void Start()
	{
		Assign();
		prevPos = base.transform.position;
		prevScale = base.transform.lossyScale;
		prevTextureToMeshZ = textureToMeshZ;
		UpdateTiling();
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			if (base.transform.lossyScale != prevScale || base.transform.position != prevPos || !Mathf.Approximately(textureToMeshZ, prevTextureToMeshZ))
			{
				UpdateTiling();
			}
			prevPos = base.transform.position;
			prevScale = base.transform.lossyScale;
			prevTextureToMeshZ = textureToMeshZ;
		}
	}

	[ContextMenu("UpdateTiling")]
	private void UpdateTiling()
	{
		Vector2 vector = new Vector2(10f, 10f);
		float num = (float)texture.width / (float)texture.height * textureToMeshZ;
		Vector4 value = new Vector2(vector.x * base.transform.lossyScale.x / num, vector.y * base.transform.lossyScale.z / textureToMeshZ);
		value.z = (0f - base.transform.position.x) / num;
		value.w = (0f - base.transform.position.z) / textureToMeshZ;
		value.z -= value.x * 0.5f;
		value.w -= value.y * 0.5f;
		if (tiler == null)
		{
			Assign();
		}
		tiler.SetVector("_MainTex_ST", value);
		tiler.SetVector("_BumpMap_ST", value);
		render.SetPropertyBlock(tiler);
	}
}
