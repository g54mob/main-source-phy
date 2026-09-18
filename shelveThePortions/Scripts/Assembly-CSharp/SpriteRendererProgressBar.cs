using UnityEngine;

public class SpriteRendererProgressBar : MonoBehaviour
{
	[SerializeField]
	private bool isVertical = true;

	[SerializeField]
	private float minPos;

	[SerializeField]
	private float maxPos;

	[SerializeField]
	private float minSize;

	[SerializeField]
	private float maxSize;

	[SerializeField]
	private Transform trans;

	public void SetPercent(float percent)
	{
		percent = Mathf.Clamp01(percent);
		Vector3 localPosition = trans.localPosition;
		Vector2 vector = trans.localScale;
		if (isVertical)
		{
			localPosition.y = Mathf.Lerp(minPos, maxPos, percent);
			vector.y = Mathf.Lerp(minSize, maxSize, percent);
		}
		else
		{
			localPosition.x = Mathf.Lerp(minPos, maxPos, percent);
			vector.x = Mathf.Lerp(minSize, maxSize, percent);
		}
		trans.localPosition = localPosition;
		trans.localScale = vector;
	}
}
