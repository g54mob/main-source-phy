using System;
using UnityEngine;

namespace TFBGames
{
	[Serializable]
	public struct InputBindingTextOverride
	{
		[Tooltip("Override the text with this value.")]
		public string text;

		[Tooltip("Override when this type of condition is met.")]
		public InputBindingTextCondition condition;

		[Space(10f)]
		[Tooltip("Global setting key.")]
		public string settingKey;

		[Tooltip("Override when the setting has this value.")]
		public int value;

		[Tooltip("Override when the setting has this slider value.")]
		public float sliderValue;
	}
}
