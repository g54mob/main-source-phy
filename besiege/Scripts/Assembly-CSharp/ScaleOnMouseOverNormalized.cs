using UnityEngine;

public class ScaleOnMouseOverNormalized : ClickBehaviour
{
	public float sizeScaler = 1.4f;

	public Transform objToScale;

	public float mousePressedScale = 0.85f;

	public float lerpSpeed = 0.1f;

	private bool isMouseOver;

	private Vector3 normalScale;

	private Vector3 overScale;

	private Vector3 pressedScale;

	private bool active = true;

	private void Start()
	{
		pressedScale = Vector3.one * mousePressedScale;
		overScale = Vector3.one * sizeScaler;
		releaseOnlyOver = true;
	}

	public override void OnClicked()
	{
		if (active && isMouseOver)
		{
			objToScale.localScale = pressedScale;
		}
	}

	public override void OnClickReleased()
	{
		if (active && isMouseOver)
		{
			objToScale.localScale = overScale;
		}
	}

	private void OnMouseEnter()
	{
		if (active)
		{
			isMouseOver = true;
			objToScale.localScale = overScale;
		}
	}

	private void OnMouseExit()
	{
		if (active)
		{
			isMouseOver = false;
			objToScale.localScale = Vector3.one;
		}
	}

	private void SetEnabledMsg(bool enabled)
	{
		OnMouseExit();
		active = enabled;
	}
}
