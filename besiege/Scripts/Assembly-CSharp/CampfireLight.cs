using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class CampfireLight : MonoBehaviour
{
	private const double Tolerance = 0.0001;

	public float MinLightIntensity = 0.6f;

	public float MaxLightIntensity = 1f;

	public float AccelerateTime = 0.15f;

	public float DouseLightSpeed = 10f;

	private float _targetIntensity = 1f;

	private float _lastIntensity = 1f;

	private float _timePassed;

	private Light _lt;

	private void Start()
	{
		_lt = GetComponent<Light>();
		_lastIntensity = _lt.intensity;
		FixedUpdate();
	}

	private void FixedUpdate()
	{
		_timePassed += Time.deltaTime;
		_lt.intensity = Mathf.Lerp(_lastIntensity, _targetIntensity, _timePassed / AccelerateTime);
		if ((double)Math.Abs(_lt.intensity - _targetIntensity) < 0.0001)
		{
			_lastIntensity = _lt.intensity;
			_targetIntensity = UnityEngine.Random.Range(MinLightIntensity, MaxLightIntensity);
			_timePassed = 0f;
		}
	}

	public IEnumerator Doused()
	{
		while (_lt.intensity > 0f)
		{
			MinLightIntensity = Mathf.Clamp(MinLightIntensity - Time.deltaTime * DouseLightSpeed, 0f, 8f);
			MaxLightIntensity = Mathf.Clamp(MaxLightIntensity - Time.deltaTime * DouseLightSpeed, 0f, 8f);
			yield return null;
		}
		_lt.enabled = false;
		base.enabled = false;
	}
}
