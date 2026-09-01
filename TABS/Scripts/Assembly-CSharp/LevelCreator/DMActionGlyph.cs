using InControl;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;

namespace LevelCreator
{
	public class DMActionGlyph : MonoBehaviour
	{
		[SerializeField]
		protected string actionName = string.Empty;

		[SerializeField]
		private bool forceText;

		protected TextMeshProUGUI textMesh;

		protected PlayerAction action;

		protected DMActionGlyphPlatformSpecificOverride[] platformOverrides;

		public PlayerAction Action => action;

		public string ActionName => actionName;

		protected virtual void Start()
		{
			if (DMEditor.Instance == null)
			{
				Debug.LogError("DMActionGlyph.cs used outside of the DMEditor! Removing component...");
				Object.Destroy(this);
				return;
			}
			textMesh = GetComponentInChildren<TextMeshProUGUI>();
			SetAction(actionName);
			PlayerActions.Instance.OnLastInputTypeChanged += OnInputSourceChanged;
			OnInputSourceChanged(BindingSourceType.None);
		}

		public void SetAction(PlayerAction action)
		{
			if (action != null)
			{
				actionName = action.Name;
				textMesh = GetComponentInChildren<TextMeshProUGUI>();
				OnInputSourceChanged(BindingSourceType.None);
			}
		}

		public void SetAction(string actionName)
		{
			action = PlayerActions.Instance.GetPlayerActionByName(actionName);
			SetAction(action);
		}

		public void SetPlatformOverrides(DMActionGlyphPlatformSpecificOverride[] overrides)
		{
			platformOverrides = overrides;
		}

		private void OnInputSourceChanged(BindingSourceType inputType)
		{
			if (!(textMesh == null))
			{
				SetGlyphText(PlayerActions.Instance.InputType, PlayerActions.Instance.LastDeviceStyle);
			}
		}

		protected virtual void SetGlyphText(InputType inputType, InputDeviceStyle deviceStyle)
		{
			GlyphServiceExtraInfo glyphServiceExtraInfo = new GlyphServiceExtraInfo();
			string text = DMEditor.Instance.glyphService.GetActionGlyph(action, inputType, deviceStyle, forceText, glyphServiceExtraInfo);
			if (!string.IsNullOrEmpty(text))
			{
				char c = text[text.Length - 1];
				if (char.IsDigit(c))
				{
					text = c.ToString();
				}
			}
			float defaultSize = 100f;
			if (inputType == InputType.Controller)
			{
				defaultSize = 140f;
			}
			defaultSize = GetOverrideSize(defaultSize, glyphServiceExtraInfo);
			textMesh.text = $"<size={defaultSize}%>{text}</size>";
		}

		private float GetOverrideSize(float defaultSize, GlyphServiceExtraInfo extraInfo)
		{
			if (extraInfo == null || platformOverrides == null || platformOverrides.Length == 0)
			{
				return defaultSize;
			}
			int i = 0;
			for (int num = platformOverrides.Length; i < num; i++)
			{
				DMActionGlyphPlatformSpecificOverride dMActionGlyphPlatformSpecificOverride = platformOverrides[i];
				if (!(dMActionGlyphPlatformSpecificOverride == null))
				{
					float? glyphSizePercent = dMActionGlyphPlatformSpecificOverride.GetGlyphSizePercent(extraInfo);
					if (glyphSizePercent.HasValue)
					{
						return glyphSizePercent.Value;
					}
				}
			}
			return defaultSize;
		}

		private void OnDestroy()
		{
			if (PlayerActions.Instance != null)
			{
				PlayerActions.Instance.OnLastInputTypeChanged -= OnInputSourceChanged;
			}
		}
	}
}
