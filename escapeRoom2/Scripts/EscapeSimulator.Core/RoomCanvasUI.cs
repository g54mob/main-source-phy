using UnityEngine;
using UnityEngine.UI;

public class RoomCanvasUI : PineUIComponent
{
	public Canvas root;

	public Image pcCrosshair;

	public Transform VoiceChatCanvas;

	public EmotesPickerUI EmotesPicker;

	public CanvasGroup HintTutorialPopup;

	public Text HintTutorialPopup_HintTutorialText;

	public Image HintTutorialPopup_HintTutorialShortcutKeyboard;

	public Image HintTutorialPopup_HintTutorialShortcutController;

	public object data;

	private bool isInitialized;

	protected override void Awake()
	{
		init();
	}

	public void init()
	{
		if (!isInitialized)
		{
			isInitialized = true;
		}
	}
}
