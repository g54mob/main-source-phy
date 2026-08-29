using UnityEngine;
using UnityEngine.UI;

public class EditorConfigUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public Toggle ConfigInfo_LegacyLightsToggle;

	public Toggle ConfigInfo_LegacyFloorCollidersToggle;

	public Toggle ConfigInfo_ForcePostProcessingToggle;

	public Toggle ConfigInfo_RemoveAllEditorPuzzleComponents;

	public Toggle ConfigInfo_PlayerNameplateVisible;

	public Toggle ConfigInfo_ItemNameplatesVisible;

	public Toggle ConfigInfo_UseProximityChat;

	public Image ConfigInfo_Spacer;

	public Toggle ConfigInfo_UseOnStartPostProcessing;

	public Toggle ConfigInfo_UseOnStartSkybox;

	public Toggle ConfigInfo_UseOnStartWater;

	public Toggle ConfigInfo_EnableScriptingProp;

	public Toggle ConfigInfo_CamToLinkedProp;

	public Button ConfigInfo_CloseButton;

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
			PineUI.addButtonListeners(Background);
			PineUI.addToggleListeners(ConfigInfo_LegacyLightsToggle);
			PineUI.addToggleListeners(ConfigInfo_LegacyFloorCollidersToggle);
			PineUI.addToggleListeners(ConfigInfo_ForcePostProcessingToggle);
			PineUI.addToggleListeners(ConfigInfo_RemoveAllEditorPuzzleComponents);
			PineUI.addToggleListeners(ConfigInfo_PlayerNameplateVisible);
			PineUI.addToggleListeners(ConfigInfo_ItemNameplatesVisible);
			PineUI.addToggleListeners(ConfigInfo_UseProximityChat);
			PineUI.addToggleListeners(ConfigInfo_UseOnStartPostProcessing);
			PineUI.addToggleListeners(ConfigInfo_UseOnStartSkybox);
			PineUI.addToggleListeners(ConfigInfo_UseOnStartWater);
			PineUI.addToggleListeners(ConfigInfo_EnableScriptingProp);
			PineUI.addToggleListeners(ConfigInfo_CamToLinkedProp);
			PineUI.addButtonListeners(ConfigInfo_CloseButton);
		}
	}
}
