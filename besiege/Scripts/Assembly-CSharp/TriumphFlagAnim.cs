using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("UI/Win Screen/Triumph Bars Flag Animation")]
public class TriumphFlagAnim : MonoBehaviour
{
	public float LerpSpeed = 4f;

	public float hideDistance = 5f;

	public bool shouldWave;

	public float waveAmount = 10f;

	public float waveDuration = 1f;

	public Renderer flagRendy;

	protected float myTime;

	protected bool lastWinCondition;

	protected bool posIsNotCorrect;

	protected Vector3 posToBe;

	protected Transform myTransform;

	protected Vector3 startPos;

	protected float startRotX;

	protected Coroutine coanimation;

	protected float _DeltaTime
	{
		get
		{
			return TimeSlider.Instance.deltaTime;
		}
	}

	protected float _Time
	{
		get
		{
			return TimeSlider.Instance.time;
		}
	}

	private void Awake()
	{
		myTransform = base.transform;
		startPos = base.transform.localPosition;
		myTransform.localPosition = new Vector3(myTransform.localPosition.x - hideDistance, myTransform.localPosition.y, myTransform.localPosition.z);
		startRotX = myTransform.localEulerAngles.x;
	}

	public IEnumerator AnimateTo(Vector3 pos, bool wave)
	{
		posToBe = pos;
		posIsNotCorrect = true;
		while (posIsNotCorrect)
		{
			Vector3 localPosition = myTransform.localPosition;
			myTransform.localPosition = Vector3.Lerp(localPosition, posToBe, _DeltaTime * LerpSpeed);
			posIsNotCorrect = localPosition != posToBe;
			if (wave)
			{
				Wave();
			}
			yield return null;
		}
		while (wave)
		{
			Wave();
			yield return null;
		}
	}

	public void Display()
	{
		if (!lastWinCondition)
		{
			lastWinCondition = true;
			if (coanimation != null)
			{
				StopCoroutine(coanimation);
			}
			coanimation = StartCoroutine(AnimateTo(startPos, shouldWave));
		}
	}

	public void Disable()
	{
		if (lastWinCondition)
		{
			lastWinCondition = false;
			if (coanimation != null)
			{
				StopCoroutine(coanimation);
			}
			coanimation = StartCoroutine(AnimateTo(startPos - new Vector3(hideDistance, 0f, 0f), false));
		}
	}

	private void Wave()
	{
		float f = _Time / waveDuration * (float)Math.PI * 2f;
		float num = Mathf.Cos(f) * 0.5f;
		myTransform.localEulerAngles = new Vector3(startRotX + num * waveAmount, myTransform.localEulerAngles.y, myTransform.localEulerAngles.z);
	}
}
