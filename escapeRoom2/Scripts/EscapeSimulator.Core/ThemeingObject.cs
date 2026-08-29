using System;
using System.Collections.Generic;
using UnityEngine;

public class ThemeingObject : MonoBehaviour
{
	public enum ThemingObjectType
	{
		None = 0,
		Button_Hoverable = 1,
		Button_Clickable = 2,
		Button_Tab = 3,
		Panel_Primary = 4,
		Panel_Secondary = 5,
		Panel_BackgroundPrimary = 6,
		Panel_BackgroundSecondary = 12,
		Text_Primary = 7,
		Text_Secondary = 8,
		Text_Clickable = 13,
		Text_Clickable_Disabled = 18,
		Other_SelectionFrame = 9,
		Other_Error = 10,
		Other_Logo = 14,
		Other_Fade = 15,
		Scrollbar = 16,
		Slider = 17
	}

	[Serializable]
	public class ThemeOverride
	{
		public Themes.ColorScheme scheme;

		public ThemingObjectType overrideColor;
	}

	public ThemingObjectType themingType;

	public List<ThemeOverride> overrides = new List<ThemeOverride>();
}
