using System;
using System.Diagnostics;
using UnityEngine;

namespace Sirenix.OdinInspector
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ProgressBarAttribute : Attribute
	{
		public double Min;

		public double Max;

		public string MinMember;

		public string MaxMember;

		public float R;

		public float G;

		public float B;

		public int Height;

		public string ColorMember;

		public string BackgroundColorMember;

		public bool Segmented;

		public string CustomValueStringMember;

		private bool drawValueLabel;

		private TextAlignment valueLabelAlignment;

		public bool DrawValueLabel
		{
			get
			{
				return drawValueLabel;
			}
			set
			{
				drawValueLabel = value;
				DrawValueLabelHasValue = true;
			}
		}

		public bool DrawValueLabelHasValue { get; private set; }

		public TextAlignment ValueLabelAlignment
		{
			get
			{
				return valueLabelAlignment;
			}
			set
			{
				valueLabelAlignment = value;
				ValueLabelAlignmentHasValue = true;
			}
		}

		public bool ValueLabelAlignmentHasValue { get; private set; }

		public ProgressBarAttribute(double min, double max, float r = 0.15f, float g = 0.47f, float b = 0.74f)
		{
			Min = min;
			Max = max;
			R = r;
			G = g;
			B = b;
			Height = 12;
			Segmented = false;
			drawValueLabel = true;
			DrawValueLabelHasValue = false;
			valueLabelAlignment = TextAlignment.Center;
			ValueLabelAlignmentHasValue = false;
		}

		public ProgressBarAttribute(string minMember, double max, float r = 0.15f, float g = 0.47f, float b = 0.74f)
		{
			MinMember = minMember;
			Max = max;
			R = r;
			G = g;
			B = b;
			Height = 12;
			Segmented = false;
			drawValueLabel = true;
			DrawValueLabelHasValue = false;
			valueLabelAlignment = TextAlignment.Center;
			ValueLabelAlignmentHasValue = false;
		}

		public ProgressBarAttribute(double min, string maxMember, float r = 0.15f, float g = 0.47f, float b = 0.74f)
		{
			Min = min;
			MaxMember = maxMember;
			R = r;
			G = g;
			B = b;
			Height = 12;
			Segmented = false;
			drawValueLabel = true;
			DrawValueLabelHasValue = false;
			valueLabelAlignment = TextAlignment.Center;
			ValueLabelAlignmentHasValue = false;
		}

		public ProgressBarAttribute(string minMember, string maxMember, float r = 0.15f, float g = 0.47f, float b = 0.74f)
		{
			MinMember = minMember;
			MaxMember = maxMember;
			R = r;
			G = g;
			B = b;
			Height = 12;
			Segmented = false;
			drawValueLabel = true;
			DrawValueLabelHasValue = false;
			valueLabelAlignment = TextAlignment.Center;
			ValueLabelAlignmentHasValue = false;
		}
	}
}
