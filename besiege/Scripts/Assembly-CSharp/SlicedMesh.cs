using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class SlicedMesh : MonoBehaviour
{
	private Mesh mesh;

	private MeshFilter filter;

	public float _b = 0.1f;

	public float _w = 1f;

	public float _h = 1f;

	public float _m = 0.4f;

	private float bAspectRatio;

	public void SetMeshSize(float border, float width, float hight, float margin)
	{
		_b = border;
		_w = width;
		_h = hight;
		_m = margin;
		CreateSlicedMesh();
	}

	public void SetMeshSizeAspect(float width)
	{
		_w = width;
		_b = bAspectRatio * width;
		_h = 2f * _b;
		CreateSlicedMesh();
	}

	private void Awake()
	{
		bAspectRatio = _b / _w;
		filter = GetComponent<MeshFilter>();
		mesh = new Mesh();
		CreateSlicedMesh();
	}

	private void CreateSlicedMesh()
	{
		mesh.vertices = new Vector3[16]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(_b, 0f, 0f),
			new Vector3(_w - _b, 0f, 0f),
			new Vector3(_w, 0f, 0f),
			new Vector3(0f, _b, 0f),
			new Vector3(_b, _b, 0f),
			new Vector3(_w - _b, _b, 0f),
			new Vector3(_w, _b, 0f),
			new Vector3(0f, _h - _b, 0f),
			new Vector3(_b, _h - _b, 0f),
			new Vector3(_w - _b, _h - _b, 0f),
			new Vector3(_w, _h - _b, 0f),
			new Vector3(0f, _h, 0f),
			new Vector3(_b, _h, 0f),
			new Vector3(_w - _b, _h, 0f),
			new Vector3(_w, _h, 0f)
		};
		mesh.uv = new Vector2[16]
		{
			new Vector2(0f, 0f),
			new Vector2(_m, 0f),
			new Vector2(1f - _m, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, _m),
			new Vector2(_m, _m),
			new Vector2(1f - _m, _m),
			new Vector2(1f, _m),
			new Vector2(0f, 1f - _m),
			new Vector2(_m, 1f - _m),
			new Vector2(1f - _m, 1f - _m),
			new Vector2(1f, 1f - _m),
			new Vector2(0f, 1f),
			new Vector2(_m, 1f),
			new Vector2(1f - _m, 1f),
			new Vector2(1f, 1f)
		};
		mesh.triangles = new int[54]
		{
			0, 4, 5, 0, 5, 1, 1, 5, 6, 1,
			6, 2, 2, 6, 7, 2, 7, 3, 4, 8,
			9, 4, 9, 5, 5, 9, 10, 5, 10, 6,
			6, 10, 11, 6, 11, 7, 8, 12, 13, 8,
			13, 9, 9, 13, 14, 9, 14, 10, 10, 14,
			15, 10, 15, 11
		};
		mesh.RecalculateBounds();
		filter.mesh = mesh;
	}
}
