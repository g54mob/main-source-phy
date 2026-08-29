using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Theme", menuName = "Escape Simulator/Theme")]
public class Themes : ScriptableObject
{
	public enum ColorScheme
	{
		Dark = 0
	}

	[Serializable]
	public class ColorsButtonHoverable
	{
		public Color selectedColorText;

		public Color unselectedColorText;

		public Color hoverColorText;
	}

	[Serializable]
	public class ColorsButtonClickable
	{
		public Color colorBG;

		public Color colorText;
	}

	[Serializable]
	public class ColorsTabButton
	{
		public Color selectedColorText;

		public Color unselectedColorText;
	}

	[Serializable]
	public class ColorsPanel
	{
		public Color primary;

		public Color secondary;

		public Color backgroundPrimary;

		public Color backgroundSecondary;
	}

	[Serializable]
	public class ColorsText
	{
		public Color primary;

		public Color secondary;

		public Color clickable;

		public Color clickableDisabled;
	}

	[Serializable]
	public class ColorOthers
	{
		public Color selectionFrame;

		public Color error;

		public Color logo;

		public Color fade;
	}

	[Serializable]
	public class ColorScrollbar
	{
		public Color background;

		public Color handle;
	}

	[Serializable]
	public class ColorSlider
	{
		public Color background;

		public Color fill;
	}

	[NonSerialized]
	public ColorScheme scheme;

	public ColorsButtonHoverable buttonHoverableColors;

	public ColorsButtonClickable buttonClickableColors;

	public ColorsTabButton tabButtonColors;

	public ColorsPanel panelColors;

	public ColorsText textColors;

	public ColorOthers otherColors;

	public ColorScrollbar scrollbarColors;

	public ColorSlider sliderColors;

	public string musicOverride;

	public static Themes debugOverrideTheme;

	private static Dictionary<ColorScheme, Themes> themesCache = new Dictionary<ColorScheme, Themes>();

	private static ColorScheme MAX_THEME = ColorScheme.Dark;

	public static Themes get(ColorScheme colorScheme)
	{
		if (colorScheme > MAX_THEME)
		{
			PlayerSave.getSettings().theme = MAX_THEME;
			colorScheme = MAX_THEME;
		}
		if (debugOverrideTheme != null)
		{
			return debugOverrideTheme;
		}
		if (!themesCache.ContainsKey(colorScheme))
		{
			Themes asset = AssetBundleLoader.getAsset<Themes>(AssetBundleType.Misc, $"Assets/_Misc/Themes/{colorScheme}.asset");
			asset.scheme = colorScheme;
			themesCache[colorScheme] = asset;
		}
		return themesCache[colorScheme];
	}

	public void themeWithChildren(GameObject gameObject)
	{
		ThemeingObject[] componentsInChildren = gameObject.GetComponentsInChildren<ThemeingObject>(includeInactive: true);
		foreach (ThemeingObject themeingObject in componentsInChildren)
		{
			theme(themeingObject.gameObject);
		}
	}

	public void theme(GameObject gameObject)
	{
		if (!gameObject.TryGetComponent<ThemeingObject>(out var component))
		{
			return;
		}
		ThemeingObject.ThemingObjectType themingObjectType = component.themingType;
		ThemeingObject.ThemeOverride themeOverride = component.overrides.Find((ThemeingObject.ThemeOverride x) => x.scheme == scheme);
		if (themeOverride != null)
		{
			themingObjectType = themeOverride.overrideColor;
		}
		switch (themingObjectType)
		{
		case ThemeingObject.ThemingObjectType.Button_Hoverable:
		{
			if (gameObject.TryGetComponent<Button>(out var _) && gameObject.TryGetComponentInChildren<Text>(out var component19) && gameObject.TryGetComponent<EventButton>(out var component20))
			{
				component20.hoverTextColor = getColorWithoutAlpha(buttonHoverableColors.hoverColorText);
				component20.normalTextColor = getColorWithoutAlpha(buttonHoverableColors.unselectedColorText);
				component19.color = getColorWithoutAlpha(buttonHoverableColors.unselectedColorText);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Button_Clickable:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component9))
			{
				setColorWithoutAlpha(component9, buttonClickableColors.colorBG);
			}
			if (gameObject.TryGetComponentInChildren<Text>(out var component10))
			{
				setColorWithoutAlpha(component10, buttonClickableColors.colorText);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Panel_Primary:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component14))
			{
				setColorWithoutAlpha(component14, panelColors.primary);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Panel_Secondary:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component5))
			{
				setColorWithoutAlpha(component5, panelColors.secondary);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Panel_BackgroundPrimary:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component16))
			{
				setColorWithoutAlpha(component16, panelColors.backgroundPrimary);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Panel_BackgroundSecondary:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component12))
			{
				setColorWithoutAlpha(component12, panelColors.backgroundSecondary);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Other_SelectionFrame:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component7))
			{
				setColorWithoutAlpha(component7, otherColors.selectionFrame);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Other_Error:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component3))
			{
				setColorWithoutAlpha(component3, otherColors.error);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Other_Logo:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component17))
			{
				setColorWithoutAlpha(component17, otherColors.logo);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Other_Fade:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component15))
			{
				setColorWithoutAlpha(component15, otherColors.fade);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Text_Primary:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component13))
			{
				setColorWithoutAlpha(component13, textColors.primary);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Text_Secondary:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component11))
			{
				setColorWithoutAlpha(component11, textColors.secondary);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Text_Clickable:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component8))
			{
				setColorWithoutAlpha(component8, textColors.clickable);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Text_Clickable_Disabled:
		{
			if (gameObject.TryGetComponent<Graphic>(out var component6))
			{
				setColorWithoutAlpha(component6, textColors.clickableDisabled);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Scrollbar:
		{
			if (gameObject.TryGetComponent<Scrollbar>(out var component4))
			{
				setColorWithoutAlpha(gameObject.transform.GetChild(0).GetComponent<Image>(), scrollbarColors.background);
				setColorWithoutAlpha(component4.targetGraphic.transform.GetChild(0).GetComponent<Image>(), scrollbarColors.handle);
			}
			break;
		}
		case ThemeingObject.ThemingObjectType.Slider:
		{
			if (gameObject.TryGetComponent<Slider>(out var component2))
			{
				setColorWithoutAlpha(gameObject.transform.GetChild(0).GetComponent<Image>(), sliderColors.background);
				setColorWithoutAlpha(component2.transform.findRecursively("Fill").GetComponent<Image>(), sliderColors.fill);
			}
			break;
		}
		}
	}

	public static void setColorWithoutAlpha(Graphic image, Color color)
	{
		color.a = image.color.a;
		image.color = color;
	}

	public static Color getColorWithoutAlpha(Color color)
	{
		color.a = 1f;
		return color;
	}
}
