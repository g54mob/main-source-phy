using System;
using UnityEngine;

public sealed class MFilter : MonoBehaviour
{
	public enum FilterType
	{
		Allpass = 0,
		LowPass = 1,
		Notch = 2,
		LowShelf = 3,
		HighShelf = 4
	}

	private struct DelayedSamples
	{
		public double za1;

		public double za2;

		public double zb1;

		public double zb2;
	}

	public FilterType Type = FilterType.HighShelf;

	[Range(0.3f, 0.8f)]
	public double Q = 0.6;

	[Range(100f, 22000f)]
	public double Frequency = 250.0;

	[Range(-30f, 30f)]
	public double Gain;

	[Range(0f, 1f)]
	public float Vol = 1f;

	private double _a0 = 1.0;

	private double _b0 = 1.0;

	private double _b1;

	private double _b2;

	private double _a1;

	private double _a2;

	private double _w0;

	private double _alpha;

	private double _A;

	private double _sampleRate;

	private double _cosW0;

	private double _sqrtAAlpha;

	private DelayedSamples[] _delayedSamples;

	private void Start()
	{
		_sampleRate = AudioSettings.outputSampleRate;
	}

	private void LateUpdate()
	{
		QandFrequencyCalculation();
		GainCalculation(Type);
		CoefficientCalculation(Type);
	}

	private void QandFrequencyCalculation()
	{
		_w0 = Math.PI * 2.0 * (Frequency / _sampleRate);
		_cosW0 = Math.Cos(_w0);
		_alpha = Math.Sin(_w0) / (2.0 * Q);
	}

	private void GainCalculation(FilterType type)
	{
		if ((uint)type > 2u && (uint)(type - 3) <= 1u)
		{
			_A = Math.Pow(10.0, Gain / 40.0);
			_sqrtAAlpha = Math.Sqrt(_A) * _alpha;
		}
	}

	private void CoefficientCalculation(FilterType type)
	{
		switch (type)
		{
		case FilterType.Allpass:
			_b0 = 1.0 - _alpha;
			_b1 = -2.0 * _cosW0;
			_b2 = 1.0 + _alpha;
			_a0 = 1.0 + _alpha;
			_a1 = -2.0 * _cosW0;
			_a2 = 1.0 - _alpha;
			break;
		case FilterType.LowPass:
			_b0 = (1.0 - _cosW0) / 2.0;
			_b1 = 1.0 - _cosW0;
			_b2 = (1.0 - _cosW0) / 2.0;
			_a0 = 1.0 + _alpha;
			_a1 = -2.0 * _cosW0;
			_a2 = 1.0 - _alpha;
			break;
		case FilterType.Notch:
			_b0 = 1.0;
			_b1 = -2.0 * _cosW0;
			_b2 = 1.0;
			_a0 = 1.0 + _alpha;
			_a1 = -2.0 * _cosW0;
			_a2 = 1.0 - _alpha;
			break;
		case FilterType.LowShelf:
			_b0 = _A * (_A + 1.0 - (_A - 1.0) * _cosW0 + 2.0 * _sqrtAAlpha);
			_b1 = 2.0 * _A * (_A - 1.0 - (_A + 1.0) * _cosW0);
			_b2 = _A * (_A + 1.0 - (_A - 1.0) * _cosW0 - 2.0 * _sqrtAAlpha);
			_a0 = _A + 1.0 + (_A - 1.0) * _cosW0 + 2.0 * _sqrtAAlpha;
			_a1 = -2.0 * (_A - 1.0 + (_A + 1.0) * _cosW0);
			_a2 = _A + 1.0 + (_A - 1.0) * _cosW0 - 2.0 * _sqrtAAlpha;
			break;
		case FilterType.HighShelf:
			_b0 = _A * (_A + 1.0 + (_A - 1.0) * _cosW0 + 2.0 * _sqrtAAlpha);
			_b1 = -2.0 * _A * (_A - 1.0 + (_A + 1.0) * _cosW0);
			_b2 = _A * (_A + 1.0 + (_A - 1.0) * _cosW0 - 2.0 * _sqrtAAlpha);
			_a0 = _A + 1.0 - (_A - 1.0) * _cosW0 + 2.0 * _sqrtAAlpha;
			_a1 = 2.0 * (_A - 1.0 - (_A + 1.0) * _cosW0);
			_a2 = _A + 1.0 - (_A - 1.0) * _cosW0 - 2.0 * _sqrtAAlpha;
			break;
		}
	}

	private void OnAudioFilterRead(float[] data, int channels)
	{
		if (_delayedSamples == null)
		{
			_delayedSamples = new DelayedSamples[channels];
		}
		for (int i = 0; i < data.Length; i++)
		{
			int num = i % channels;
			if (num < _delayedSamples.Length)
			{
				DelayedSamples delayedSamples = _delayedSamples[num];
				double num2 = data[i];
				if (AudioUtilites.CheckDouble(num2))
				{
					num2 = 0.0;
				}
				double num3 = (num2 * _b0 + delayedSamples.zb1 * _b1 + delayedSamples.zb2 * _b2 - delayedSamples.za1 * _a1 - delayedSamples.za2 * _a2) / _a0;
				if (AudioUtilites.CheckDouble(num3))
				{
					num3 = 0.0;
				}
				float num4 = (float)num3 * Vol;
				if (num4 > 1f || num4 < -1f)
				{
					num4 = Mathf.Clamp(num4, -1f, 1f);
				}
				data[i] = num4;
				_delayedSamples[num].zb2 = delayedSamples.zb1;
				_delayedSamples[num].zb1 = num2;
				_delayedSamples[num].za2 = delayedSamples.za1;
				_delayedSamples[num].za1 = num3;
			}
		}
	}
}
