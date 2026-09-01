using UnityEngine;

public class WaterSplash_OrderedTexturePriority : MonoBehaviour
{
	private static float lastOffset = -1f;

	private static float offsetStep = 0.01f;

	private static float minOffset = -1f;

	private static float maxOffset = 0f;

	private void Start()
	{
		float num = lastOffset + offsetStep;
		if (maxOffset < num)
		{
			num += minOffset - maxOffset;
		}
		base.transform.localPosition += Vector3.up * num;
		lastOffset = num;
	}
}
