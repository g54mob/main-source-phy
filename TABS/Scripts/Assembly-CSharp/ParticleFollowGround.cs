using UnityEngine;

public class ParticleFollowGround : MonoBehaviour
{
	public LayerMask mask;

	private Vector3 startPos;

	private void Start()
	{
		startPos = base.transform.parent.InverseTransformPoint(base.transform.position);
	}

	private void Update()
	{
		Vector3 vector = base.transform.parent.TransformPoint(startPos);
		Physics.Raycast(new Ray(vector + Vector3.up, Vector3.down), out var hitInfo, 10f, mask);
		if ((bool)hitInfo.transform)
		{
			base.transform.position = hitInfo.point;
		}
	}
}
