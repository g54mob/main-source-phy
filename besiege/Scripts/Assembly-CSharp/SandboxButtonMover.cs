using System;
using UnityEngine;

public class SandboxButtonMover : MonoBehaviour
{
	[SerializeField]
	private float magnitudeRange = 0.5f;

	[SerializeField]
	private float minMagnitude = 0.5f;

	[SerializeField]
	private float periodRange = 1f;

	[SerializeField]
	private float minPeriod = 2f;

	private Transform myTransform;

	private Vector3 startPosition;

	private float xMagnitude;

	private float yMagnitude;

	private float xDuration;

	private float yDuration;

	private float timeOffset;

	private void Awake()
	{
		myTransform = base.transform;
		startPosition = myTransform.localPosition;
		timeOffset = UnityEngine.Random.Range(0f, (float)Math.PI);
		xMagnitude = minMagnitude + UnityEngine.Random.Range(0f, magnitudeRange);
		yMagnitude = minMagnitude + UnityEngine.Random.Range(0f, magnitudeRange * 2f);
		xDuration = minPeriod + UnityEngine.Random.Range(0f, periodRange);
		yDuration = minPeriod + UnityEngine.Random.Range(0f, periodRange * 2f);
	}

	private void Update()
	{
		Vector3 vector = new Vector3(Mathf.Sin(timeOffset + Time.time / xDuration) * xMagnitude, Mathf.Sin(timeOffset + Time.time / yDuration) * yMagnitude);
		myTransform.localPosition = startPosition + vector;
	}
}
