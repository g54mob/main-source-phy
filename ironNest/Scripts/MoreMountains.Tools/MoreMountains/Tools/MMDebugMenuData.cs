using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MoreMountains.Tools
{
	[CreateAssetMenu(fileName = "MMDebugMenuData", menuName = "MoreMountains/MMDebugMenu/MMDebugMenuData")]
	public class MMDebugMenuData : ScriptableObject
	{
		[Header("Prefabs")]
		public MMDebugMenuItemTitle TitlePrefab;

		public MMDebugMenuItemButton ButtonPrefab;

		public MMDebugMenuItemButton ButtonBorderPrefab;

		public MMDebugMenuItemCheckbox CheckboxPrefab;

		public MMDebugMenuItemSlider SliderPrefab;

		public GameObject SpacerSmallPrefab;

		public GameObject SpacerBigPrefab;

		public MMDebugMenuItemText TextTinyPrefab;

		public MMDebugMenuItemText TextSmallPrefab;

		public MMDebugMenuItemText TextLongPrefab;

		public MMDebugMenuItemValue ValuePrefab;

		public MMDebugMenuItemChoices TwoChoicesPrefab;

		public MMDebugMenuItemChoices ThreeChoicesPrefab;

		public MMDebugMenuTab TabPrefab;

		public MMDebugMenuTabContents TabContentsPrefab;

		public RectTransform TabSpacerPrefab;

		public MMDebugMenuDebugTab DebugTabPrefab;

		public string DebugTabName;

		[Header("Tabs")]
		public List<MMDebugMenuTabData> Tabs;

		public bool DisplayDebugTab;

		public int MaxTabs;

		public int InitialActiveTabIndex;

		[Header("Toggle")]
		public MMDebugMenu.ToggleDirections ToggleDirection;

		public float ToggleDuration;

		public MMTween.MMTweenCurve ToggleCurve;

		public Key ToggleKey;

		[Header("Style")]
		public Font RegularFont;

		public Font BoldFont;

		public Color BackgroundColor;

		public Color AccentColor;

		public Color TextColor;
	}
}
