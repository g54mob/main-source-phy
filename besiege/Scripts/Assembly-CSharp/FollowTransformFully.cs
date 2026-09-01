using UnityEngine;

public class FollowTransformFully : MonoBehaviour
{
	public Transform target;

	public Transform myTransform;

	public bool followRotation = true;

	public bool lateUpdate = true;

	public bool fixedUpdate;

	public bool onlyInBuild;

	public Collider[] collidersToEnable = new Collider[0];

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			for (int i = 0; i < collidersToEnable.Length; i++)
			{
				collidersToEnable[i].enabled = true;
				collidersToEnable[i].attachedRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			}
		}
	}

	private void Update()
	{
		if (!lateUpdate && !fixedUpdate)
		{
			SetTransform();
		}
	}

	private void LateUpdate()
	{
		if (lateUpdate)
		{
			SetTransform();
		}
	}

	private void FixedUpdate()
	{
		if (fixedUpdate)
		{
			SetTransform();
		}
	}

	private void SetTransform()
	{
		if (!StatMaster.levelSimulating || !onlyInBuild)
		{
			myTransform.position = target.position;
			if (followRotation)
			{
				myTransform.rotation = target.rotation;
			}
		}
	}
}
