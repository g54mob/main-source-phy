using System;
using UnityEngine;

namespace Battlehub.RTHandles
{
	[Serializable]
	public class RTHColors
	{
		public Color32 DisabledColor = new Color32(128, 128, 128, 128);

		public Color32 XColor = new Color32(187, 70, 45, byte.MaxValue);

		public Color32 YColor = new Color32(139, 206, 74, byte.MaxValue);

		public Color32 ZColor = new Color32(55, 115, 244, byte.MaxValue);

		public Color32 AltColor = new Color32(192, 192, 192, 224);

		public Color32 AltColor2 = new Color32(56, 56, 56, 224);

		public Color32 SelectionColor = new Color32(239, 238, 64, byte.MaxValue);

		public Color32 SelectionAltColor = new Color(0f, 0f, 0f, 0.1f);

		public Color32 BoundsColor = Color.green;

		public Color32 GridColor = new Color(1f, 1f, 1f, 0.1f);

		public RTHColors()
		{
		}

		public RTHColors(RTHColors colors)
		{
			DisabledColor = colors.DisabledColor;
			XColor = colors.XColor;
			YColor = colors.YColor;
			ZColor = colors.ZColor;
			AltColor = colors.AltColor;
			AltColor2 = colors.AltColor2;
			SelectionColor = colors.SelectionColor;
			SelectionAltColor = colors.SelectionAltColor;
			BoundsColor = colors.BoundsColor;
			GridColor = colors.GridColor;
		}
	}
}
