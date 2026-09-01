using UnityEngine;

public class KeepWorldScale : MonoBehaviour
{
	public Vector3 worldScale = Vector3.one;

	private Vector3 lastScale;

	private Transform parentTransform;

	protected void Awake()
	{
		parentTransform = base.transform.parent;
	}

	protected void LateUpdate()
	{
		if (!(lastScale == base.transform.lossyScale))
		{
			Vector3 localScale = parentTransform.InverseTransformVector(worldScale).Absolute();
			base.transform.localScale = localScale;
			lastScale = base.transform.lossyScale;
		}
	}
}
