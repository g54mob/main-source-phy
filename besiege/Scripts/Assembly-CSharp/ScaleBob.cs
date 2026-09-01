using System;
using UnityEngine;

public class ScaleBob : MonoBehaviour
{
	public float amount = 0.01f;

	public float bobTime = 1f;

	private Transform t;

	private Vector3 startScale;

	private float phi;

	private float amplitude;

	private float time;

	private void Start()
	{
		t = base.transform;
		startScale = t.localScale;
	}

	private void Update()
	{
		time += Time.unscaledDeltaTime;
		phi = time / bobTime * 2f * (float)Math.PI;
		amplitude = Mathf.Cos(phi) * amount;
		base.transform.localScale = startScale + startScale * amplitude;
	}

	private void OnDisable()
	{
		base.transform.localScale = startScale;
	}
}
