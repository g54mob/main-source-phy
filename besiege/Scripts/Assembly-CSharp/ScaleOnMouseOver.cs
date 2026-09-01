using UnityEngine;

public class ScaleOnMouseOver : ClickBehaviour
{
	public bool ignoreTextFocus;

	public float sizeScaler = 1.4f;

	public Transform objToScale;

	public float mousePressedScale = 0.85f;

	public float lerpSpeed = 0.1f;

	public int mask = -1;

	private Vector3 startScale;

	private bool isMouseOver;

	private Vector3 normalScale;

	private Vector3 overScale;

	private Vector3 pressedScale;

	private bool active = true;

	private void Awake()
	{
		startScale = objToScale.localScale.Absolute();
		pressedScale = (startScale * mousePressedScale).Absolute();
		overScale = (startScale * sizeScaler).Absolute();
		releaseOnlyOver = true;
	}

	public override void OnClicked()
	{
		if (base.enabled && UIMask.InsideMask(mask, base.transform.position) && active && isMouseOver)
		{
			ScaleObject(pressedScale);
		}
	}

	public override void OnClickReleased()
	{
		if (!base.enabled || !UIMask.InsideMask(mask, base.transform.position))
		{
			OnMouseExit();
		}
		else if (active && isMouseOver)
		{
			ScaleObject(overScale);
		}
	}

	private void OnMouseEnter()
	{
		if (!base.enabled || !UIMask.InsideMask(mask, base.transform.position))
		{
			isMouseOver = false;
		}
		else if (active && (!ignoreTextFocus || !StatMaster.textFieldSelected))
		{
			isMouseOver = true;
			ScaleObject(overScale);
		}
	}

	private void OnMouseExit()
	{
		if (active)
		{
			isMouseOver = false;
			ScaleObject(startScale);
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		isMouseOver = false;
		objToScale.localScale = startScale;
	}

	private void ScaleObject(Vector3 newScale)
	{
		objToScale.localScale = newScale;
	}

	private void SetEnabledMsg(bool enabled)
	{
		OnMouseExit();
		active = enabled;
	}
}
