using Landfall.TABS.UnitPlacement;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class UIGlyphsManager : MonoBehaviour
{
	[SerializeField]
	private GameObject clearBlueGlyph;

	[SerializeField]
	private GameObject clearRedGlyph;

	[SerializeField]
	private GameObject startBattleGlyph;

	[Tooltip("Glyphs to show when CUSTOM_CONTENT is enabled and hide when disabled.")]
	public GameObject[] customContentGlyphs;

	private InputService inputService;

	private SettingsInstance m_borderColorSetting;

	private PlacementCursorState cursorState;

	private void Awake()
	{
		inputService = ServiceLocator.GetService<InputService>();
	}

	private void Start()
	{
		if (inputService != null)
		{
			cursorState = inputService.placementCursorState;
			SwapClearSideGlyph(cursorState);
		}
	}

	private void Update()
	{
		if (inputService != null)
		{
			cursorState = inputService.placementCursorState;
			SwapClearSideGlyph(cursorState);
		}
	}

	public void ShowStartBattleGlyph()
	{
		startBattleGlyph.SetActive(value: true);
	}

	public void HideStartBattleGlyph()
	{
		startBattleGlyph.SetActive(value: false);
	}

	public void TurnCustomContentOff()
	{
		if (customContentGlyphs == null || customContentGlyphs.Length == 0)
		{
			return;
		}
		GameObject[] array = customContentGlyphs;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: false);
			}
		}
	}

	private void SwapClearSideGlyph(PlacementCursorState state)
	{
		switch (state)
		{
		case PlacementCursorState.DenyPlacement:
		case PlacementCursorState.InvalidPlacement:
			if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Campaign)
			{
				SetSideClearIcon(0);
			}
			break;
		case PlacementCursorState.AllowRedPlacement:
			SetSideClearIcon(0);
			break;
		case PlacementCursorState.AllowBluePlacement:
			SetSideClearIcon(1);
			break;
		case PlacementCursorState.Hide:
			SetSideClearIcon(-1);
			break;
		}
	}

	private void SetSideClearIcon(int side)
	{
		if (!(clearBlueGlyph == null) && !(clearRedGlyph == null) && PlayerActions.Instance.InputType == InputType.Controller)
		{
			switch (side)
			{
			case 0:
				clearRedGlyph.SetActive(value: true);
				clearBlueGlyph.SetActive(value: false);
				break;
			case 1:
				clearRedGlyph.SetActive(value: false);
				clearBlueGlyph.SetActive(value: true);
				break;
			default:
				clearRedGlyph.SetActive(value: false);
				clearBlueGlyph.SetActive(value: false);
				break;
			}
		}
	}
}
