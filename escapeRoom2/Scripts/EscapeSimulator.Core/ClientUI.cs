using UnityEngine;
using UnityEngine.UI;

public class ClientUI : PineUIComponent
{
	public Canvas root;

	public Text LobbyInfo;

	public Transform PlayerList;

	public Text LevelTitle;

	public Image Spinner_Spinner;

	public LevelUI Level;

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
