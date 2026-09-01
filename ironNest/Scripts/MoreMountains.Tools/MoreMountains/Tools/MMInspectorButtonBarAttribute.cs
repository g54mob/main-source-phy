using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AttributeUsage(AttributeTargets.Field)]
	public class MMInspectorButtonBarAttribute : PropertyAttribute
	{
		public string[] Labels { get; set; }

		public string[] Methods { get; set; }

		public bool[] OnlyWhenPlaying { get; set; }

		public string[] UssClass { get; set; }

		public MMInspectorButtonBarAttribute(string[] labels, string[] methods, bool[] onlyWhenPlaying, string[] ussClass)
		{
		}
	}
}
