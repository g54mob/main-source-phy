using UnityEngine;

public class KeepScaleRatio : MonoBehaviour
{
	private Vector3 ratio;

	private Vector3 lastScale;

	private void Awake()
	{
		Vector3 lossyScale = base.transform.parent.lossyScale;
		ratio = new Vector3(base.transform.localScale.x / lossyScale.x, base.transform.localScale.y / lossyScale.y, base.transform.localScale.z / lossyScale.z);
	}

	private void LateUpdate()
	{
		if (lastScale != base.transform.lossyScale)
		{
			Vector3 lossyScale = base.transform.parent.lossyScale;
			if (lossyScale.x != 0f && lossyScale.y != 0f && lossyScale.z != 0f)
			{
				Vector3 vector = new Vector3(1f / lossyScale.x / ratio.x, 1f / lossyScale.y / ratio.y, 1f / lossyScale.z / ratio.z);
				base.transform.localScale = vector + vector * (lossyScale.x * ratio.x + lossyScale.y * ratio.y + lossyScale.z * ratio.z) / 30f;
			}
			lastScale = base.transform.lossyScale;
		}
	}
}
