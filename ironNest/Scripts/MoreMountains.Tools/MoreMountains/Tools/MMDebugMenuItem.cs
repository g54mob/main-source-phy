using System;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMDebugMenuItem
	{
		public enum MMDebugMenuItemTypes
		{
			Title = 0,
			Spacer = 1,
			Button = 2,
			Checkbox = 3,
			Slider = 4,
			Text = 5,
			Value = 6,
			Choices = 7
		}

		public enum MMDebugMenuItemTextTypes
		{
			Tiny = 0,
			Small = 1,
			Long = 2
		}

		public enum MMDebugMenuItemChoicesTypes
		{
			TwoChoices = 0,
			ThreeChoices = 1
		}

		public enum MMDebugMenuItemButtonTypes
		{
			Border = 0,
			Full = 1
		}

		public enum MMDebugMenuItemSpacerTypes
		{
			Small = 0,
			Big = 1
		}

		public string Name;

		public bool Active;

		public MMDebugMenuItemTypes Type;

		[MMEnumCondition("Type", new int[] { 0 })]
		public string TitleText;

		[MMEnumCondition("Type", new int[] { 5 })]
		public MMDebugMenuItemTextTypes TextType;

		[MMEnumCondition("Type", new int[] { 5 })]
		public string TextContents;

		[MMEnumCondition("Type", new int[] { 7 })]
		public MMDebugMenuItemChoicesTypes ChoicesType;

		[MMEnumCondition("Type", new int[] { 7 })]
		public string ChoiceOneText;

		[MMEnumCondition("Type", new int[] { 7 })]
		public string ChoiceOneEventName;

		[MMEnumCondition("Type", new int[] { 7 })]
		public string ChoiceTwoText;

		[MMEnumCondition("Type", new int[] { 7 })]
		public string ChoiceTwoEventName;

		[MMEnumCondition("Type", new int[] { 7 })]
		public string ChoiceThreeText;

		[MMEnumCondition("Type", new int[] { 7 })]
		public string ChoiceThreeEventName;

		[MMEnumCondition("Type", new int[] { 7 })]
		public int SelectedChoice;

		[MMEnumCondition("Type", new int[] { 6 })]
		public string ValueLabel;

		[MMEnumCondition("Type", new int[] { 6 })]
		public string ValueInitialValue;

		[MMEnumCondition("Type", new int[] { 6 })]
		public int ValueMMRadioReceiverChannel;

		[MMEnumCondition("Type", new int[] { 2 })]
		public string ButtonText;

		[MMEnumCondition("Type", new int[] { 2 })]
		public MMDebugMenuItemButtonTypes ButtonType;

		[MMEnumCondition("Type", new int[] { 2 })]
		public string ButtonEventName;

		[MMEnumCondition("Type", new int[] { 1 })]
		public MMDebugMenuItemSpacerTypes SpacerType;

		[MMEnumCondition("Type", new int[] { 3 })]
		public string CheckboxText;

		[MMEnumCondition("Type", new int[] { 3 })]
		public bool CheckboxInitialState;

		[MMEnumCondition("Type", new int[] { 3 })]
		public string CheckboxEventName;

		[MMEnumCondition("Type", new int[] { 4 })]
		public MMDebugMenuItemSlider.Modes SliderMode;

		[MMEnumCondition("Type", new int[] { 4 })]
		public string SliderText;

		[MMEnumCondition("Type", new int[] { 4 })]
		public float SliderRemapZero;

		[MMEnumCondition("Type", new int[] { 4 })]
		public float SliderRemapOne;

		[MMEnumCondition("Type", new int[] { 4 })]
		public float SliderInitialValue;

		[MMEnumCondition("Type", new int[] { 4 })]
		public string SliderEventName;

		[MMHidden]
		public MMDebugMenuItemSlider TargetSlider;

		[MMHidden]
		public MMDebugMenuItemButton TargetButton;

		[MMHidden]
		public MMDebugMenuItemCheckbox TargetCheckbox;
	}
}
