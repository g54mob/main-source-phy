using System;
using UnityEngine;

public static class InterpolationExtensions
{
	private static readonly float c1 = 1.70158f;

	private static readonly float c2 = c1 * 1.525f;

	private static readonly float c3 = c1 + 1f;

	private static readonly float c4 = MathF.PI * 2f / 3f;

	private static readonly float c5 = MathF.PI * 4f / 9f;

	private static readonly float n1 = 7.5625f;

	private static readonly float d1 = 2.75f;

	private static readonly Func<float, float> inSine = (float x) => 1f - Mathf.Cos(x * MathF.PI / 2f);

	private static readonly Func<float, float> outSine = (float x) => Mathf.Sin(x * MathF.PI / 2f);

	private static readonly Func<float, float> inOutSine = (float x) => (0f - (Mathf.Cos(MathF.PI * x) - 1f)) / 2f;

	private static readonly Func<float, float> inQuad = (float x) => x * x;

	private static readonly Func<float, float> outQuad = (float x) => 1f - (1f - x) * (1f - x);

	private static readonly Func<float, float> inOutQuad = (float x) => (!(x < 0.5f)) ? (1f - Mathf.Pow(-2f * x + 2f, 2f) / 2f) : (2f * x * x);

	private static readonly Func<float, float> inCubic = (float x) => x * x * x;

	private static readonly Func<float, float> outCubic = (float x) => 1f - Mathf.Pow(1f - x, 3f);

	private static readonly Func<float, float> inOutCubic = (float x) => (!(x < 0.5f)) ? (1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f) : (4f * x * x * x);

	private static readonly Func<float, float> inQuart = (float x) => x * x * x * x;

	private static readonly Func<float, float> outQuart = (float x) => 1f - Mathf.Pow(1f - x, 4f);

	private static readonly Func<float, float> inOutQuart = (float x) => (!(x < 0.5f)) ? (1f - Mathf.Pow(-2f * x + 2f, 4f) / 2f) : (8f * x * x * x * x);

	private static readonly Func<float, float> inQuint = (float x) => x * x * x * x * x;

	private static readonly Func<float, float> outQuint = (float x) => 1f - Mathf.Pow(1f - x, 5f);

	private static readonly Func<float, float> inOutQuint = (float x) => (!(x < 0.5f)) ? (1f - Mathf.Pow(-2f * x + 2f, 5f) / 2f) : (16f * x * x * x * x * x);

	private static readonly Func<float, float> inExpo = (float x) => (x != 0f) ? Mathf.Pow(2f, 10f * x - 10f) : 0f;

	private static readonly Func<float, float> outExpo = (float x) => (x != 1f) ? (1f - Mathf.Pow(2f, -10f * x)) : 1f;

	private static readonly Func<float, float> inOutExpo = (float x) => (x != 0f) ? ((x != 1f) ? ((!(x < 0.5f)) ? ((2f - Mathf.Pow(2f, -20f * x + 10f)) / 2f) : (Mathf.Pow(2f, 20f * x - 10f) / 2f)) : 1f) : 0f;

	private static readonly Func<float, float> inCirc = (float x) => 1f - Mathf.Sqrt(1f - Mathf.Pow(x, 2f));

	private static readonly Func<float, float> outCirc = (float x) => Mathf.Sqrt(1f - Mathf.Pow(x - 1f, 2f));

	private static readonly Func<float, float> inOutCirc = (float x) => (!(x < 0.5f)) ? ((Mathf.Sqrt(1f - Mathf.Pow(-2f * x + 2f, 2f)) + 1f) / 2f) : ((1f - Mathf.Sqrt(1f - Mathf.Pow(2f * x, 2f))) / 2f);

	private static readonly Func<float, float> inBack = (float x) => c3 * x * x * x - c1 * x * x;

	private static readonly Func<float, float> outBack = (float x) => 1f + c3 * Mathf.Pow(x - 1f, 3f) + c1 * Mathf.Pow(x - 1f, 2f);

	private static readonly Func<float, float> inOutBack = (float x) => (!(x < 0.5f)) ? ((Mathf.Pow(2f * x - 2f, 2f) * ((c2 + 1f) * (x * 2f - 2f) + c2) + 2f) / 2f) : (Mathf.Pow(2f * x, 2f) * ((c2 + 1f) * 2f * x - c2) / 2f);

	private static readonly Func<float, float> inElastic = (float x) => (x != 0f) ? ((x != 1f) ? ((0f - Mathf.Pow(2f, 10f * x - 10f)) * Mathf.Sin((x * 10f - 10.75f) * c4)) : 1f) : 0f;

	private static readonly Func<float, float> outElastic = (float x) => (x != 0f) ? ((x != 1f) ? (Mathf.Pow(2f, -10f * x) * Mathf.Sin((x * 10f - 0.75f) * c4) + 1f) : 1f) : 0f;

	private static readonly Func<float, float> inOutElastic = (float x) => (x != 0f) ? ((x != 1f) ? ((!(x < 0.5f)) ? (Mathf.Pow(2f, -20f * x + 10f) * Mathf.Sin((20f * x - 11.125f) * c5) / 2f + 1f) : ((0f - Mathf.Pow(2f, 20f * x - 10f) * Mathf.Sin((20f * x - 11.125f) * c5)) / 2f)) : 1f) : 0f;

	private static readonly Func<float, float> inBounce = (float x) => 1f - outBounce(1f - x);

	private static readonly Func<float, float> outBounce = delegate(float x)
	{
		if (x < 1f / d1)
		{
			return n1 * x * x;
		}
		if (x < 2f / d1)
		{
			return n1 * (x -= 1.5f / d1) * x + 0.75f;
		}
		return (x < 2.5f / d1) ? (n1 * (x -= 2.25f / d1) * x + 0.9375f) : (n1 * (x -= 2.625f / d1) * x + 63f / 64f);
	};

	private static readonly Func<float, float> inOutBounce = (float x) => (!(x < 0.5f)) ? ((1f + outBounce(2f * x - 1f)) / 2f) : ((1f - outBounce(1f - 2f * x)) / 2f);

	public static float evaluate(this Interpolation interpolation, float t)
	{
		return interpolation switch
		{
			Interpolation.Linear => t, 
			Interpolation.SmoothStep => Mathf.SmoothStep(0f, 1f, t), 
			Interpolation.SmootherStep => UnityUtils.smootherStep(0f, 1f, t), 
			Interpolation.InSine => inSine(t), 
			Interpolation.OutSine => outSine(t), 
			Interpolation.InOutSine => inOutSine(t), 
			Interpolation.InQuad => inQuad(t), 
			Interpolation.OutQuad => outQuad(t), 
			Interpolation.InOutQuad => inOutQuad(t), 
			Interpolation.InCubic => inCubic(t), 
			Interpolation.OutCubic => outCubic(t), 
			Interpolation.InOutCubic => inOutCubic(t), 
			Interpolation.InQuart => inQuart(t), 
			Interpolation.OutQuart => outQuart(t), 
			Interpolation.InOutQuart => inOutQuart(t), 
			Interpolation.InQuint => inQuint(t), 
			Interpolation.OutQuint => outQuint(t), 
			Interpolation.InOutQuint => inOutQuint(t), 
			Interpolation.InExpo => inExpo(t), 
			Interpolation.OutExpo => outExpo(t), 
			Interpolation.InOutExpo => inOutExpo(t), 
			Interpolation.InCirc => inCirc(t), 
			Interpolation.OutCirc => outCirc(t), 
			Interpolation.InOutCirc => inOutCirc(t), 
			Interpolation.InBack => inBack(t), 
			Interpolation.OutBack => outBack(t), 
			Interpolation.InOutBack => inOutBack(t), 
			Interpolation.InElastic => inElastic(t), 
			Interpolation.OutElastic => outElastic(t), 
			Interpolation.InOutElastic => inOutElastic(t), 
			Interpolation.InBounce => inBounce(t), 
			Interpolation.OutBounce => outBounce(t), 
			Interpolation.InOutBounce => inOutBounce(t), 
			_ => t, 
		};
	}
}
