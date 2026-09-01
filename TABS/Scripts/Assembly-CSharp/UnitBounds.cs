using UnityEngine;

public class UnitBounds : MonoBehaviour
{
	public Bounds bounds;

	public void CalculateBounds()
	{
		MeshFilter[] componentsInChildren = base.gameObject.GetComponentsInChildren<MeshFilter>();
		if (componentsInChildren.Length != 0)
		{
			bounds = componentsInChildren[0].sharedMesh.bounds;
			MeshFilter[] array = componentsInChildren;
			foreach (MeshFilter meshFilter in array)
			{
				bounds.Encapsulate(meshFilter.sharedMesh.bounds);
			}
		}
		else
		{
			bounds = new Bounds(Vector3.zero, Vector3.zero);
		}
		GetComponent<BoxCollider>().size = bounds.extents;
		GetComponent<BoxCollider>().center = bounds.center;
	}

	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(base.transform.position + bounds.center, bounds.extents);
	}
}
