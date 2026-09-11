using System;
using System.Diagnostics;
using UnityEngine;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class GUIColorAttribute : Attribute
	{
		public Color Color { get; }

		public string GetColor { get; }

		public GUIColorAttribute(float r, float g, float b, float a = 1f)
		{
			Color = new Color(r, g, b, a);
		}

		public GUIColorAttribute(byte r, byte g, byte b, byte a = byte.MaxValue)
		{
			Color = new Color32(r, g, b, a);
		}

		public GUIColorAttribute(string value)
		{
			if (value.StartsWith("$"))
			{
				GetColor = value;
				return;
			}
			if (!ColorUtility.TryParseHtmlString(value, out var color) && !ColorUtility.TryParseHtmlString("#" + value, out color))
			{
				color = Color.white;
			}
			Color = color;
		}
	}
}
