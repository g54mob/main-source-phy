using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class JointSearchSystem : MonoBehaviour
{
	[SerializeField]
	private float jointSearchRadius = 0.5f;

	public List<GameObject> Joints = new List<GameObject>();

	private List<Color> gizmoColors = new List<Color>();

	public List<Collider> hitColliders;

	public List<Collider> thisObjectColliders;

	public List<Rigidbody> connectedBodies = new List<Rigidbody>();

	private bool canDrawGizmo;
}
