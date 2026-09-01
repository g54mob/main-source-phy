using UnityEngine;

public class ResistScaling : MonoBehaviour
{
	[Range(0f, 1f)]
	public float scaleFollow = 0.5f;

	public bool keepRatio;

	private Vector3 startScale;

	private Vector3 ratio;

	private Vector3 lastScale;

	private void Start()
	{
		if (!StatMaster.levelSimulating)
		{
			Vector3 lossyScale = base.transform.parent.lossyScale;
			ratio = new Vector3(base.transform.localScale.x / lossyScale.x, base.transform.localScale.y / lossyScale.y, base.transform.localScale.z / lossyScale.z);
			startScale = base.transform.localScale;
		}
	}

	private void LateUpdate()
	{
		if (StatMaster.levelSimulating || !(lastScale != base.transform.lossyScale))
		{
			return;
		}
		Vector3 lossyScale = base.transform.parent.lossyScale;
		if (keepRatio)
		{
			if (lossyScale.x != 0f && lossyScale.y != 0f && lossyScale.z != 0f)
			{
				Vector3 vector = new Vector3(1f / lossyScale.x / ratio.x, 1f / lossyScale.y / ratio.y, 1f / lossyScale.z / ratio.z);
				vector += vector * (lossyScale.x * ratio.x + lossyScale.y * ratio.y + lossyScale.z * ratio.z) / 3f;
				base.transform.localScale = Vector3.Lerp(vector, startScale, scaleFollow);
			}
		}
		else
		{
			Vector3 a = new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z);
			base.transform.localScale = Vector3.Lerp(a, startScale, scaleFollow);
		}
		lastScale = base.transform.lossyScale;
	}
}
