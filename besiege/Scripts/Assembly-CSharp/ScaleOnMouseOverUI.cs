using UnityEngine;

public class ScaleOnMouseOverUI : MonoBehaviour
{
	public RectTransform objToScale;

	public float sizeScaler = 1.4f;

	public float mousePressedScale = 0.85f;

	private Vector3 startScale;

	private bool isMouseOver;

	private Vector3 overScale;

	private Vector3 pressedScale;

	public void MouseOver()
	{
		if (base.enabled)
		{
			isMouseOver = true;
			SetScale(overScale);
		}
	}

	public void MouseExit()
	{
		isMouseOver = false;
		SetScale(startScale);
	}

	public void MouseDown()
	{
		if (isMouseOver)
		{
			SetScale(pressedScale);
		}
	}

	public void MouseUp()
	{
		if (isMouseOver)
		{
			SetScale(overScale);
		}
	}

	private void Awake()
	{
		startScale = objToScale.localScale;
		pressedScale = startScale * mousePressedScale;
		overScale = startScale * sizeScaler;
	}

	private void SetScale(Vector3 newScale)
	{
		objToScale.localScale = newScale;
	}

	private void OnDisable()
	{
		isMouseOver = false;
		SetScale(startScale);
	}
}
