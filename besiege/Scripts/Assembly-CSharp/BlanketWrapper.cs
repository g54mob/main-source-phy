using UnityEngine;

public class BlanketWrapper : MonoBehaviour
{
	public Mesh lowPoly;

	public MeshRenderer source;

	public Transform[] objects = new Transform[0];

	private int posID;

	private int heightID;

	private int countID;

	private Vector4[] v;

	private bool visible;

	private void Start()
	{
		posID = Shader.PropertyToID("Positions");
		heightID = Shader.PropertyToID("BaseHeight");
		countID = Shader.PropertyToID("ArrayCount");
		v = new Vector4[objects.Length];
		source.material.SetFloat(heightID, base.transform.position.y);
		source.material.SetInt(countID, objects.Length);
		MeshFilter component = source.GetComponent<MeshFilter>();
		if (SystemInfo.graphicsShaderLevel <= 30)
		{
			component.mesh = lowPoly;
		}
		Mesh mesh = component.mesh;
		mesh.bounds = new Bounds(new Vector3(0f, 20f, 0f), new Vector3(mesh.bounds.size.x, 6f, mesh.bounds.size.z));
	}

	private void Update()
	{
		if (visible)
		{
			for (int i = 0; i < objects.Length; i++)
			{
				v[i] = objects[i].position;
			}
			UpdateMat(source.material);
		}
	}

	private void UpdateMat(Material m)
	{
		m.SetVectorArray(posID, v);
	}

	private void OnBecameVisible()
	{
		visible = true;
	}

	private void OnBecameInvisible()
	{
		visible = false;
	}
}
