using UnityEngine;

[AddComponentMenu("Water/Controllers/Water Follow Cam")]
public class WaterFollowCam : WaveGridMover
{
	public bool setRotation;

	public Camera target;

	protected override void LateUpdate()
	{
		base.LateUpdate();
		Rotate();
	}

	protected void Rotate()
	{
		if (StatMaster.isHeadless)
		{
			return;
		}
		if (setRotation)
		{
			Vector3 forward = cam.forward;
			forward.y = 0f;
			base.transform.forward = forward;
		}
		if (moveInIntervals)
		{
			bool flag = Vector3.Dot(cam.forward, base.transform.forward) < 0.5f;
			float y = 0f;
			if (flag)
			{
				y = ((!(Vector3.Dot(cam.forward, base.transform.right) > 0f)) ? (-90f) : 90f);
			}
			if (flag && setRotation)
			{
				base.transform.Rotate(new Vector3(0f, y, 0f));
			}
		}
	}

	protected void OnDrawGizmos()
	{
		if (base.enabled || (bool)target)
		{
			Color color = new Color(0.3f, 0.4f, 1f, 0.75f);
			if (!Application.isPlaying && !camera)
			{
				camera = ((!target) ? Camera.main : target);
				cam = camera.transform;
			}
			Debug.DrawLine(RaycastOnPlane(new Vector2(0f, 1f)), RaycastOnPlane(new Vector2(1f, 1f)), color);
			Debug.DrawLine(RaycastOnPlane(new Vector2(1f, 1f)), RaycastOnPlane(new Vector2(1f, 0f)), color);
			Debug.DrawLine(RaycastOnPlane(new Vector2(1f, 0f)), RaycastOnPlane(new Vector2(0f, 0f)), color);
			Debug.DrawLine(RaycastOnPlane(new Vector2(0f, 0f)), RaycastOnPlane(new Vector2(0f, 1f)), color);
		}
	}
}
