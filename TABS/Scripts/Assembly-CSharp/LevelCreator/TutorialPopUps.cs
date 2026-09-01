using System;
using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

namespace LevelCreator
{
	public class TutorialPopUps
	{
		private const float MovementPopupFontSize = 18f;

		private const float ToolBarPopUpXPosition = -0.45f;

		private const float ToolBarPopUpYPosition = 0.6f;

		private const float TooltipPopUpXPosition = -0.55f;

		private const float TooltipPopUpYPosition = -0.5f;

		private static List<string> m_createdTutorials = new List<string>();

		private static List<string> m_shownTutorials = new List<string>();

		private static PopUp m_previousTooltipPopUp = null;

		private static bool HasCreatedPopUp(string message)
		{
			if (!string.IsNullOrEmpty(message) && !m_createdTutorials.Contains(message))
			{
				m_createdTutorials.Add(message);
				return false;
			}
			return true;
		}

		public static void LoadShownPopups()
		{
			DMIOWrapper.File.Exists(Paths.ShownTutorialPopupsPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (exists)
				{
					DMIOWrapper.File.ReadAllLines(Paths.ShownTutorialPopupsPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] lines)
					{
						m_shownTutorials.Clear();
						m_shownTutorials.AddRange(lines);
					});
				}
			});
		}

		private static void SaveShownPopup(string message)
		{
			if (!m_shownTutorials.Contains(message))
			{
				m_shownTutorials.Add(message);
			}
			DMIOWrapper.File.WriteAllLines(Paths.ShownTutorialPopupsPath, m_shownTutorials, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
		}

		private static bool HasShownPopup(string message)
		{
			return m_shownTutorials.Contains(message);
		}

		private static void ShowTutorialPopUp(PopUp popUp, float delay, UnityEngine.Object sender, bool neverShowAgain)
		{
			if (HasShownPopup(popUp.message) || !(DMEditor.Instance != null))
			{
				return;
			}
			LeanTween.delayedCall(delay, (System.Action)delegate
			{
				Utility.DelayUntil(DMEditor.Instance, () => !DMUIManager.Instance.IsOpen && Time.timeSinceLevelLoad > 5f, delegate
				{
					if (neverShowAgain)
					{
						SaveShownPopup(popUp.message);
					}
					if (popUp != null)
					{
						popUp.Show(0f, sender);
					}
				});
			});
		}

		public static void TooltipPopUp(ToolTableRow toolRow, UnityEngine.Object sender)
		{
			string tutorialMessage = toolRow.tutorialMessage;
			float lifeTime = 10f;
			float delay = 0f;
			if (!HasCreatedPopUp(tutorialMessage))
			{
				if (m_previousTooltipPopUp != null)
				{
					UnityEngine.Object.Destroy(m_previousTooltipPopUp);
				}
				ShowTutorialPopUp(m_previousTooltipPopUp = PopUp.CreatePopUp(new Vector3(-0.55f, -0.5f), tutorialMessage, demandFocus: false, lifeTime, 18f), delay, sender, neverShowAgain: true);
			}
		}

		public static void SlidersHintPopUp(UnityEngine.Object sender)
		{
			string message = "LC_SLIDERS_TOOLTIP";
			float lifeTime = 10f;
			float delay = 0f;
			if (!HasCreatedPopUp(message))
			{
				ShowTutorialPopUp(PopUp.CreatePopUp(new Vector3(0.65f, 0.3f), message, demandFocus: false, lifeTime, 18f, isContinuePopUp: false, PopUp.PopupArrowMode.DownRight), delay, sender, neverShowAgain: true);
			}
		}

		public static void MenubarPopUp(UnityEngine.Object sender)
		{
			string message = "LC_MENUBAR_TOOLTIP";
			float lifeTime = 10f;
			float delay = 30f;
			if (!HasCreatedPopUp(message))
			{
				ShowTutorialPopUp(PopUp.CreatePopUp(new Vector3(-0.45f, 0.6f), message, demandFocus: false, lifeTime, 18f, isContinuePopUp: false, PopUp.PopupArrowMode.UpLeft), delay, sender, neverShowAgain: true);
			}
		}

		public static PopUp MovementPopUp(UnityEngine.Object sender)
		{
			string message = "LC_MOVEMENT_TOOLTIP";
			float lifeTime = 10f;
			float delay = 2f;
			GlyphService service = ServiceLocator.GetService<GlyphService>();
			PlayerActions instance = PlayerActions.Instance;
			bool forceText = instance.InputType == InputType.Keyboard;
			if (PlayerActions.Instance.InputType == InputType.Keyboard)
			{
				string actionGlyph = service.GetActionGlyph(instance.m_moveLeft, instance.InputType, InputDeviceStyle.Unknown, forceText);
				string actionGlyph2 = service.GetActionGlyph(instance.m_moveRight, instance.InputType, InputDeviceStyle.Unknown, forceText);
				string actionGlyph3 = service.GetActionGlyph(instance.m_moveForward, instance.InputType, InputDeviceStyle.Unknown, forceText);
				string actionGlyph4 = service.GetActionGlyph(instance.m_moveBackward, instance.InputType, InputDeviceStyle.Unknown, forceText);
				string text = actionGlyph + ", " + actionGlyph2 + ", " + actionGlyph3 + ", " + actionGlyph4;
				string actionGlyph5 = service.GetActionGlyph(instance.m_flyDown, instance.InputType, InputDeviceStyle.Unknown, forceText);
				string actionGlyph6 = service.GetActionGlyph(instance.m_flyUp, instance.InputType, InputDeviceStyle.Unknown, forceText);
				string text2 = actionGlyph5 + ", " + actionGlyph6;
				message = Localizer.GetSinglePhrase(message, text, text2);
			}
			else
			{
				string actionGlyph7 = service.GetActionGlyph(instance.m_moveAnyDirection, instance.InputType, InputDeviceStyle.Unknown, forceText);
				string actionGlyph8 = service.GetActionGlyph(instance.m_flyDown, instance.InputType, InputDeviceStyle.Unknown, forceText);
				string actionGlyph9 = service.GetActionGlyph(instance.m_flyUp, instance.InputType, InputDeviceStyle.Unknown, forceText);
				string text3 = actionGlyph8 + ", " + actionGlyph9;
				message = Localizer.GetSinglePhrase(message, actionGlyph7, text3);
			}
			if (HasCreatedPopUp(message))
			{
				return null;
			}
			PopUp popUp = PopUp.CreatePopUp(new Vector3(0f, -0.4f), message, demandFocus: false, lifeTime, 18f);
			popUp.onHideComplete.AddListener(delegate
			{
				m_createdTutorials.Remove(message);
			});
			ShowTutorialPopUp(popUp, delay, sender, neverShowAgain: false);
			return popUp;
		}

		public static void ReturnToEditorPopUp(UnityEngine.Object sender)
		{
			string message = "LC_EXIT_PLAMODE_POPUP";
			float lifeTime = 10f;
			float delay = 5f;
			if (!HasShownPopup(message))
			{
				PopUp popUp = PopUp.CreatePopUp(new Vector3(0.55f, 0.5f), message, demandFocus: false, lifeTime, 18f, isContinuePopUp: false, PopUp.PopupArrowMode.UpRight);
				if (popUp != null)
				{
					popUp.Show(delay, sender);
				}
				SaveShownPopup(message);
			}
		}
	}
}
