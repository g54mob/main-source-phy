using UnityEngine;

public class GroundCaster : MonoBehaviour
{
	public float changeWhenInAir;

	private CharacterData data;

	public LayerMask mask;

	private void Start()
	{
		data = GetComponentInParent<CharacterData>();
	}

	private void FixedUpdate()
	{
		if ((bool)data.move && data.move.movementIsControlledElseWhere)
		{
			return;
		}
		RaycastHit hitInfo = default(RaycastHit);
		Physics.SphereCast(new Ray(base.transform.position, Vector3.down), 0.2f, out hitInfo, 2.3f + (data.isGrounded ? 0f : changeWhenInAir) - (data.isCrouching ? data.crouchSize : 0f), mask);
		if ((bool)hitInfo.transform)
		{
			if (Vector3.Angle(hitInfo.normal, Vector3.up) < 70f)
			{
				data.TouchGround(hitInfo);
			}
			Debug.DrawLine(base.transform.position, hitInfo.point);
		}
	}
}
