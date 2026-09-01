using System;
using UnityEngine;

public class PulseCookieSize : MonoBehaviour
{
	public Light lightObj;

	public float duration = 1f;

	public float minSize;

	public float multiplier = 1f;

	private float offset;

	private void Start()
	{
		offset = UnityEngine.Random.value * 10f;
	}

	private void Update()
	{
		Pulse();
	}

	private void Pulse()
	{
		float f = (Time.time + offset) / duration * 2f * (float)Math.PI;
		float num = Mathf.Cos(f) * 0.5f + 0.5f;
		lightObj.cookieSize = minSize + num * multiplier;
	}
}
