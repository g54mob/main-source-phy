using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("UI/Set Collider To Match Mesh Bounds")]
public class SetColliderToMatchMeshBounds : MonoBehaviour
{
	public Vector2 padding = Vector2.zero;

	private MeshRenderer ren;

	private BoxCollider col;

	private Vector3 lastCenter = Vector3.zero;

	private Vector3 lastSize = Vector3.one;

	private void Awake()
	{
		ren = base.gameObject.GetComponent<MeshRenderer>();
		col = base.gameObject.GetComponent<BoxCollider>();
	}

	private void LateUpdate()
	{
		if (lastCenter != ren.bounds.center || lastSize != ren.bounds.size)
		{
			col.center = base.transform.InverseTransformPoint(ren.bounds.center);
			col.size = base.transform.InverseTransformVector(ren.bounds.size) + (Vector3)padding;
		}
	}
}
