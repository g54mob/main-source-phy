using System;
using UnityEngine.Events;

[Serializable]
public class MainMenuState
{
	public MenuState state;

	public bool mainIsOut;

	public bool campaignSelectorIsOut;

	public bool campaignMenuIsOut;

	public bool settingsIsOut;

	public bool levelSelectIsOut;

	public bool thanksIsOut;

	public bool roadmapIsOut;

	public bool workshopIsOut;

	public bool screenSizeIsOut;

	public bool multiplayerIsOut;

	public bool isBlurry;

	public bool isDark;

	public UnityEvent enterMenuEvent;
}
