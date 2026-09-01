using System;
using System.Collections;
using UnityEngine;

public class ShiftyOnMouseOver : ClickBehaviour
{
	public Transform objToShift;

	public Vector3 lerpPosDirection = -Vector3.right;

	public float lerpSpeed = 0.1f;

	public float timeToProc;

	public float lerpAmount = 1f;

	public int mask = -1;

	private Vector3 startPos;

	private bool isMouseOver;

	protected bool active = true;

	protected Coroutine lerper;

	protected float cTime;

	protected void Awake()
	{
		startPos = objToShift.position;
	}

	public override void OnClicked()
	{
		if (UIMask.InsideMask(mask, base.transform.position) && active && isMouseOver)
		{
			if (lerper != null)
			{
				StopCoroutine(lerper);
			}
			lerper = StartCoroutine(LerpPos(1.5f));
		}
	}

	protected void OnMouseEnter()
	{
		if (!UIMask.InsideMask(mask, base.transform.position))
		{
			isMouseOver = false;
		}
		else if (active)
		{
			isMouseOver = true;
			if (lerper != null)
			{
				StopCoroutine(lerper);
			}
			lerper = StartCoroutine(LerpPos());
		}
	}

	private void OnMouseExit()
	{
		if (active)
		{
			isMouseOver = false;
			objToShift.position = startPos;
			cTime = 0f;
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		isMouseOver = false;
		objToShift.position = startPos;
		cTime = 0f;
	}

	protected IEnumerator LerpPos(float rateScale = 1f)
	{
		float rate = 1f / lerpSpeed;
		while (cTime < 1f)
		{
			cTime += Time.unscaledDeltaTime * rate * rateScale;
			objToShift.position = startPos + lerpPosDirection * Mathf.Sin((float)Math.PI * cTime) * lerpAmount;
			yield return null;
		}
		cTime = 0f;
	}

	public void SetEnabledMsg(bool enabled)
	{
		OnMouseExit();
		active = enabled;
	}
}
