using UnityEngine;

[AddComponentMenu("VFX/ParticleFollowCamera")]
public class ParticleFollowCamera : MonoBehaviour
{
	protected Camera camera;

	protected Transform cam;

	protected virtual void Start()
	{
		if (!camera)
		{
			camera = Camera.main;
		}
		cam = camera.transform;
	}

	protected virtual void LateUpdate()
	{
		Move();
	}

	protected void Move()
	{
		if (!StatMaster.isHeadless)
		{
			Vector3 position = cam.position;
			position.y = 0f;
			SetPosition(position);
		}
	}

	protected virtual void ResetPos(ref Vector3 pos)
	{
		pos.y = base.transform.position.y;
	}

	protected virtual void SetPosition(Vector3 pos)
	{
		if (base.transform.position != pos)
		{
			base.transform.position = pos;
		}
	}
}
