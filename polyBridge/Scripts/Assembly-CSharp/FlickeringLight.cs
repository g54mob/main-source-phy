using System;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
	public enum WaveForm
	{
		sin = 0,
		tri = 1,
		sqr = 2,
		saw = 3,
		inv = 4,
		noise = 5
	}

	public WaveForm waveform;

	public float baseStart;

	public float amplitude = 1f;

	public float phase;

	public float frequency = 0.5f;

	private Color originalColor;

	private Light m_light;

	private void Start()
	{
		m_light = GetComponent<Light>();
		originalColor = m_light.color;
	}

	private void Update()
	{
		m_light.color = originalColor * EvalWave();
	}

	private float EvalWave()
	{
		float num = (Time.time + phase) * frequency;
		num -= Mathf.Floor(num);
		float num2 = ((waveform == WaveForm.sin) ? Mathf.Sin(num * 2f * MathF.PI) : ((waveform == WaveForm.tri) ? ((!(num < 0.5f)) ? (-4f * num + 3f) : (4f * num - 1f)) : ((waveform == WaveForm.sqr) ? ((!(num < 0.5f)) ? (-1f) : 1f) : ((waveform == WaveForm.saw) ? num : ((waveform == WaveForm.inv) ? (1f - num) : ((waveform != WaveForm.noise) ? 1f : (1f - UnityEngine.Random.value * 2f)))))));
		return Mathf.Clamp(num2 * amplitude + baseStart, 0.0001f, float.MaxValue);
	}
}
