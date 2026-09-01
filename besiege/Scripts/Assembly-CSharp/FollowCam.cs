using UnityEngine;

public class FollowCam : MonoBehaviour
{
	public bool parentInHeadless;

	public bool centerOnEnable;

	public Vector3 localPosition = Vector3.zero;

	public Vector3 localRotation = Vector3.zero;

	public Vector3 localScale = Vector3.one;

	protected void OnEnable()
	{
		if (!StatMaster.isHeadless || parentInHeadless)
		{
			base.transform.parent = Camera.main.transform;
			if (centerOnEnable)
			{
				base.transform.localPosition = localPosition;
				base.transform.localRotation = Quaternion.Euler(localRotation);
				base.transform.localScale = localScale;
			}
		}
	}
}
