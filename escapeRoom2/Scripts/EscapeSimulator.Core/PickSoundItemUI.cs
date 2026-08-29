using UnityEngine;
using UnityEngine.UI;

public class PickSoundItemUI : PineUIComponent
{
	public Button root;

	public Image Selection;

	public RectTransform Tab0;

	public RectTransform Tab1;

	public RectTransform Tab2;

	public RectTransform Tab3;

	public Image SoundIcon;

	public Image FolderIcon;

	public Text Title;

	public RectTransform[] Tab_List;

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
