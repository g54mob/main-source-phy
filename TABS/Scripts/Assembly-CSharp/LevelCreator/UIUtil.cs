using Landfall.TABS;
using UnityEngine.Events;

namespace LevelCreator
{
	public class UIUtil
	{
		public static void AskForConfirmationFirstIfEditorHasDirtyUserLevel(UnityAction okEvent)
		{
			if (DMEditor.Instance != null && DMEditor.Instance.HasDirtyLevelData())
			{
				ServiceLocator.GetService<ModalPanel>().Choice("MAINMENU_EXIT_DISCARD_CHANGES_POPUP_HEADER", "MAINMENU_EXIT_DISCARD_CHANGES_POPUP_QUESTION", okEvent, null);
			}
			else if (DMEditor.Instance == null && SpawnLevel.IsCustomLevelScene && SpawnLevel.IsCustomLevelTestRun && DMEditor.LevelWasDirtyWhenEnteredPlayMode)
			{
				ServiceLocator.GetService<ModalPanel>().Choice("MAINMENU_EXIT_DISCARD_CHANGES_POPUP_HEADER", "MAINMENU_EXIT_DISCARD_CHANGES_POPUP_QUESTION", okEvent, null);
			}
			else
			{
				okEvent();
			}
		}

		public static void ClearEditorLevel()
		{
			if (DMEditor.Instance != null)
			{
				DMEditor.Instance.ClearLevel();
			}
		}
	}
}
