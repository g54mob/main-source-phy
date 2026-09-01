using System;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorPopup : UIComponentMainMenu
	{
		[SerializeField]
		private CanvasGroup panel;

		public UnitEditorPopupSubMenu overwriteConfirmMenu;

		public UnitEditorPopupSubMenu discardConfirmMenu;

		public UnitEditorPopupSubMenu saveSubUnitMenu;

		public LocalizeText overwriteText;

		public LocalizeText saveSubText;

		private Action overwriteEvent;

		private Action saveNewEvent;

		private Action cancelEvent;

		private Action discardEvent;

		public void AskOverwrite(UnitBlueprint unit, Action onOverwriteEvent, Action onSaveNewEvent)
		{
			OpenSubMenu(overwriteConfirmMenu);
			overwriteEvent = onOverwriteEvent;
			saveNewEvent = onSaveNewEvent;
			overwriteText.Args = new string[2]
			{
				unit.Entity.Name,
				"\n"
			};
			overwriteText.LocaleID = "POPUP_OVERWRITECHECK";
			panel.interactable = true;
			panel.blocksRaycasts = true;
		}

		public void AskDiscard(Action onDiscardEvent)
		{
			OpenSubMenu(discardConfirmMenu);
			discardEvent = onDiscardEvent;
			panel.interactable = true;
			panel.blocksRaycasts = true;
		}

		public void AskSaveSubunit(string unitName, Action onDiscardEvent, Action onSaveNewEvent, Action onCancel)
		{
			OpenSubMenu(saveSubUnitMenu);
			discardEvent = onDiscardEvent;
			saveNewEvent = onSaveNewEvent;
			cancelEvent = onCancel;
			saveSubText.Args = new string[2] { unitName, "\n" };
			saveSubText.LocaleID = "POPUP_SAVECHECK";
			panel.interactable = true;
			panel.blocksRaycasts = true;
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			if (canvasToggle != null)
			{
				canvasToggle.FadeIn();
			}
		}

		protected override void OnClose()
		{
			base.OnClose();
			if (currentSubMenu != null)
			{
				currentSubMenu.gameObject.SetActive(value: false);
				CloseSubMenu(currentSubMenu);
			}
		}

		public void Discard()
		{
			discardEvent?.Invoke();
			base.Close?.Invoke();
		}

		public void Overwrite(bool yes)
		{
			if (yes)
			{
				overwriteEvent?.Invoke();
			}
			else
			{
				saveNewEvent?.Invoke();
			}
			base.Close?.Invoke();
		}

		public void Cancel()
		{
			base.Close?.Invoke();
			cancelEvent?.Invoke();
			cancelEvent = null;
		}
	}
}
