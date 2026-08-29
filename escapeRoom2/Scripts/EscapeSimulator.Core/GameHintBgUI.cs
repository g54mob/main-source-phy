using UnityEngine.UI;

public class GameHintBgUI : PineUIComponent
{
	public Image root;

	public Text Title;

	public Text Explanation;

	public Text ExplanationTutorial;

	public Image Image;

	public Button ButtonHint;

	public Image ButtonHint_Fill;

	public Text ButtonHint_Label;

	public Text ButtonHint_HintCooldownText;

	public Image ButtonHint_RequestHintController;

	public Image ButtonHint_SelectionIndicator;

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
			PineUI.addButtonListeners(ButtonHint);
		}
	}
}
