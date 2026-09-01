using System.Collections.Generic;
using UnityEngine;

public class GooglyEye : MonoBehaviour
{
	public enum EyeState
	{
		Open = 0,
		Blink = 1,
		Dead = 2,
		Startle = 3
	}

	public List<GooglyEye> blinkBuddies = new List<GooglyEye>();

	public float spring;

	public float drag;

	public float size;

	public AnimationCurve randomCurve;

	public float inheritedMovement;

	public GameObject open;

	public GameObject blink;

	public GameObject dead;

	public GameObject startle;

	public Transform pupil;

	[HideInInspector]
	public Vector3 velocity;

	[HideInInspector]
	public Vector3 eyeTarget;

	[HideInInspector]
	public Vector3 lastPos;

	public bool isAnimated;

	public EyeState currentEyeState;

	public float nextBlink;

	private void Start()
	{
		spring *= randomCurve.Evaluate(Random.value);
		drag *= randomCurve.Evaluate(Random.value);
		lastPos = base.transform.position;
		if ((bool)GooglyEyes.instance)
		{
			GooglyEyes.instance.AddEye(this);
		}
		else
		{
			Debug.LogError("You need a googly eyes manager");
		}
		HealthHandler componentInParent = GetComponentInParent<HealthHandler>();
		if ((bool)componentInParent)
		{
			componentInParent.AddDieAction(Die);
		}
		SetNextBlink();
		if (base.transform.parent.localPosition.x < 0f)
		{
			startle.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}

	public void SetNextBlink()
	{
		nextBlink = Time.time + Random.Range(0.5f, 5f);
	}

	public void SetState(EyeState eyeState)
	{
		if (currentEyeState != EyeState.Dead)
		{
			ToggleEye(currentEyeState, on: false);
			ToggleEye(eyeState, on: true);
			currentEyeState = eyeState;
		}
	}

	private void ToggleEye(EyeState eyeState, bool on)
	{
		if (!(open.gameObject == null))
		{
			if (eyeState == EyeState.Open)
			{
				open.gameObject.SetActive(on);
			}
			if (eyeState == EyeState.Blink)
			{
				blink.gameObject.SetActive(on);
			}
			if (eyeState == EyeState.Dead)
			{
				dead.gameObject.SetActive(on);
			}
			if (eyeState == EyeState.Startle)
			{
				startle.gameObject.SetActive(on);
			}
		}
	}

	public void Die()
	{
		if ((bool)GooglyEyes.instance)
		{
			GooglyEyes.instance.RemoveEye(this);
		}
		SetState(EyeState.Dead);
	}

	private void OnDestroy()
	{
		if (currentEyeState != EyeState.Dead && (bool)GooglyEyes.instance)
		{
			GooglyEyes.instance.RemoveEye(this);
		}
	}
}
