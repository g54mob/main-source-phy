using UnityEngine;

public class ScaleJiggleHideShowAnimation : MonoBehaviour
{
	public bool open;

	private ScaleJiggle scaleJiggle;

	private void Awake()
	{
		scaleJiggle = GetComponent<ScaleJiggle>();
		if (!open)
		{
			base.transform.localScale = Vector3.zero;
		}
	}

	private void LateUpdate()
	{
		if (!open)
		{
			scaleJiggle.extraScale = scaleJiggle.targetScale * -1f;
		}
	}

	public void Open()
	{
		open = false;
		scaleJiggle.extraScale = 0f;
	}

	public void Close()
	{
		open = true;
	}
}
