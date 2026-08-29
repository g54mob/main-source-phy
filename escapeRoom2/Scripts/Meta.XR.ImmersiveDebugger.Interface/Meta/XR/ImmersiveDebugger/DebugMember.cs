using System;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.XR.ImmersiveDebugger
{
	[Serializable]
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	public class DebugMember : Attribute
	{
		public const string DisplayNameTooltip = "Optional name override to be used in the Inspector Panel";

		private static readonly Dictionary<DebugColor, Color> ParsedColors = new Dictionary<DebugColor, Color>
		{
			{
				DebugColor.Red,
				Color.red
			},
			{
				DebugColor.Gray,
				Color.gray
			}
		};

		public DebugGizmoType GizmoType;

		public bool ShowGizmoByDefault;

		public Color Color = Color.gray;

		public bool Tweakable = true;

		public float Min;

		public float Max = 1f;

		public string Category;

		public string Description;

		[Tooltip("Optional name override to be used in the Inspector Panel")]
		public string DisplayName;

		public DebugMember(DebugColor color = DebugColor.Gray)
		{
			ParsedColors.TryGetValue(color, out Color);
		}
	}
}
