using UnityEngine;

[ExecuteAlways]
public class TakeSizeOfOther : MonoBehaviour
{
	public RectTransform other;

	public bool useWidth;

	public bool useHeight;

	private void Update()
	{
		if (!(other == null))
		{
			RectTransform obj = (RectTransform)base.transform;
			Vector2 sizeDelta = obj.sizeDelta;
			if (useWidth)
			{
				sizeDelta.x = other.sizeDelta.x;
			}
			if (useHeight)
			{
				sizeDelta.y = other.sizeDelta.y;
			}
			obj.sizeDelta = sizeDelta;
		}
	}
}
