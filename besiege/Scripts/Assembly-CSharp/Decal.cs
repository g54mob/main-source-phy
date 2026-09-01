using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Decal : MonoBehaviour
{
	public Material material;

	public Sprite sprite;

	public float maxAngle;

	public float pushDistance = 0.009f;

	private List<Material> materials;

	private Matrix4x4 oldMatrix;

	private Vector3 oldScale;

	protected static bool showAffectedObject;

	private Renderer[] affectedObjects;

	private bool buildingDecal;

	private void Start()
	{
		BuildDecal(this);
	}

	private void OnDrawGizmosSelected()
	{
		Color red = Color.red;
		red.a = 0.25f;
		Gizmos.color = red;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
	}

	public Bounds GetBounds()
	{
		Vector3 lossyScale = base.transform.lossyScale;
		Vector3 vector = -lossyScale / 2f;
		Vector3 vector2 = lossyScale / 2f;
		Vector3[] array = new Vector3[8]
		{
			new Vector3(vector.x, vector.y, vector.z),
			new Vector3(vector2.x, vector.y, vector.z),
			new Vector3(vector.x, vector2.y, vector.z),
			new Vector3(vector2.x, vector2.y, vector.z),
			new Vector3(vector.x, vector.y, vector2.z),
			new Vector3(vector2.x, vector.y, vector2.z),
			new Vector3(vector.x, vector2.y, vector2.z),
			new Vector3(vector2.x, vector2.y, vector2.z)
		};
		for (int i = 0; i < 8; i++)
		{
			array[i] = base.transform.TransformDirection(array[i]);
		}
		vector = (vector2 = array[0]);
		Vector3[] array2 = array;
		foreach (Vector3 rhs in array2)
		{
			vector = Vector3.Min(vector, rhs);
			vector2 = Vector3.Max(vector2, rhs);
		}
		return new Bounds(base.transform.position, vector2 - vector);
	}

	private void BuildDecal(Decal decal)
	{
		MeshFilter meshFilter = decal.GetComponent<MeshFilter>();
		if (object.ReferenceEquals(meshFilter, null))
		{
			meshFilter = decal.gameObject.AddComponent<MeshFilter>();
		}
		Renderer renderer = decal.GetComponent<Renderer>();
		if (object.ReferenceEquals(renderer, null))
		{
			renderer = decal.gameObject.AddComponent<MeshRenderer>();
		}
		renderer.material = decal.material;
		if (object.ReferenceEquals(decal.material, null) || object.ReferenceEquals(decal.sprite, null))
		{
			meshFilter.sharedMesh = null;
			return;
		}
		affectedObjects = DecalHandler.GetAffectedObjects();
		Bounds bounds = decal.GetBounds();
		for (int i = 0; i < affectedObjects.Length; i++)
		{
			Renderer renderer2 = affectedObjects[i];
			if (!(renderer2 == null) && renderer2.gameObject.activeInHierarchy && bounds.Intersects(renderer2.bounds) && DecalHandler.BuildDecalForObject(decal, renderer2.gameObject))
			{
				buildingDecal = true;
			}
		}
		if (!buildingDecal)
		{
			renderer.enabled = false;
		}
		DecalHandler.Push(decal.pushDistance);
		Mesh mesh = DecalHandler.CreateMesh();
		if (!object.ReferenceEquals(mesh, null))
		{
			mesh.name = "DecalMesh";
			meshFilter.sharedMesh = mesh;
		}
	}
}
