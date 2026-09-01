using UnityEngine;

public class StretchMesh : MonoBehaviour
{
	public Transform target;

	public Transform mesh;

	private Vector3 objectScale;

	private float distance;

	private Vector3 newScale;

	private void Update()
	{
		objectScale = mesh.localScale;
		distance = Vector3.Distance(target.position, mesh.position);
		newScale = new Vector3(objectScale.x, objectScale.y, distance / 2f);
		mesh.localScale = newScale;
		base.transform.LookAt(target);
	}
}
