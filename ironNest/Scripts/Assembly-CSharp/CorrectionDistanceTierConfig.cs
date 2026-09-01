using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[DisallowMultipleComponent]
public class CorrectionDistanceTierConfig : MonoBehaviour
{
	public enum Mode
	{
		Bracketed = 0,
		Exact = 1,
		Ranged = 2
	}

	[Serializable]
	public struct Bracket
	{
		[Tooltip("Maximum raw distance (inclusive) that maps to this label.\nNote: This compares against RAW distance (before Unit Scale).")]
		public float maxDistance;

		[Tooltip("Text label shown when the miss distance falls within this bracket.")]
		public string label;
	}

	[Header("General")]
	[Tooltip("Select how this tier expresses distance:\n- Bracketed: qualitative labels based on brackets (thresholds).\n- Exact: single numeric value using Exact Mode Settings.\n- Ranged: quantized [low–high] numeric range using Ranged Mode Settings.\nTip: Ranged applies its quantization on the scaled distance (after Unit Scale).")]
	public Mode mode;

	[Header("Exact Mode Settings")]
	[Tooltip("Format string applied to the (scaled) miss distance if Mode = Exact.\nUses standard .NET composite formatting with one argument: the scaled distance.\nExamples: '{0:0.0} m' => 12.3 m, '{0:0.00} km' => 1.23 km")]
	public string exactFormat;

	[Tooltip("Multiplier applied to raw distance to convert into display units.\nExamples: 1 = meters, 0.001 = kilometers, 3.28084 = feet.\nAffects both Exact and Ranged modes; Bracketed uses raw distance for threshold tests.")]
	public float unitScale;

	[Header("Bracketed Mode Settings")]
	[Tooltip("Ordered list of brackets (ascending by maxDistance). The first bracket whose maxDistance >= raw distance is selected.\nOnly used if Mode = Bracketed.")]
	public List<Bracket> brackets;

	[Header("Ranged (Quantized) Mode Settings")]
	[Tooltip("Fixed step size (in SCALED units) that determines the lower and upper bounds of the range.\nGiven scaled distance D and step S:\n- Low  = floor(D / S) * S\n- High = ceil(D / S) * S\nSupports fractional values (e.g., 0.5 → 12.0–12.5; 2.5 → 5.0–7.5).\nExamples:\n- D=127, S=10  → 120–130\n- D=3.7, S=0.5 → 3.5–4.0")]
	public float rangeStep;

	[Tooltip("If enabled, the lower bound is clamped to 0 (never negative). Useful if distance cannot be negative.")]
	public bool clampRangeLowToZero;

	[Tooltip("Format string for Ranged output. Supports the following tokens:\n- {low}  : lower bound (scaled), default format '0.0' if none provided.\n- {high} : upper bound (scaled), default format '0.0' if none provided.\nYou may optionally supply a numeric format: {low:0}, {low:0.0}, {high:0.00}, etc.\nTokens are replaced after quantization. Include units directly if desired.\nExamples:\n- \"{low:0}-{high:0} m\"   → 120-130 m\n- \"{low:0.0}–{high:0.0} km\" → 1.2–1.3 km")]
	public string rangeFormat;

	private static readonly Regex RangeTokenRegex;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public string FormatDistance(float rawDistance)
	{
		return null;
	}

	private static float SafeScale(float value, float scale)
	{
		return 0f;
	}

	private static (float, float) QuantizeRange(float scaledValue, float step, bool clampLowToZero)
	{
		return default((float, float));
	}

	private static string SafeFormatExact(string format, float value)
	{
		return null;
	}

	private static string FormatRange(string template, float low, float high)
	{
		return null;
	}
}
