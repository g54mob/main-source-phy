using Landfall.TABS;
using Landfall.TABS.UnitEditor;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorEquipedColorCell : MonoBehaviour
{
	public Image m_Image;

	public Image m_TeamColorImage;

	public int submeshIndex;

	public UnitEditorManager.EquipedWrapper equipedItem;

	private UnitEditorHighlightingManager unitEditorHighlightingManager;

	private UnitEditorColorWheel colorWheel;

	public void Setup(ColorPaletteData colorData, int index, UnitEditorManager.EquipedWrapper clothingWrapper, UnitEditorColorWheel wheel)
	{
		m_Image.color = colorData.m_color;
		submeshIndex = index;
		equipedItem = clothingWrapper;
		unitEditorHighlightingManager = Object.FindObjectOfType<UnitEditorHighlightingManager>();
		colorWheel = wheel;
		m_TeamColorImage.gameObject.SetActive(value: false);
	}

	public void Setup(TeamColorPaletteData colorData, int index, UnitEditorManager.EquipedWrapper clothingWrapper, UnitEditorColorWheel wheel)
	{
		UnitEditorManager unitEditorManager = Object.FindObjectOfType<UnitEditorManager>();
		m_Image.color = colorData.GetColor(unitEditorManager.currentTeam);
		submeshIndex = index;
		equipedItem = clothingWrapper;
		unitEditorHighlightingManager = Object.FindObjectOfType<UnitEditorHighlightingManager>();
		colorWheel = wheel;
		m_TeamColorImage.gameObject.SetActive(value: true);
		m_TeamColorImage.color = colorData.GetColor(TeamUtlity.GetOtherTeam(unitEditorManager.currentTeam));
		m_TeamColorImage.fillAmount = m_Image.fillAmount;
	}

	public void Setup(CharacterItem.RendererMaterialWrapper wrapper, int index, UnitEditorManager.EquipedWrapper clothingWrapper, UnitEditorColorWheel wheel)
	{
		m_Image.color = wrapper.m_material.SafeColor();
		submeshIndex = index;
		equipedItem = clothingWrapper;
		unitEditorHighlightingManager = Object.FindObjectOfType<UnitEditorHighlightingManager>();
		colorWheel = wheel;
		m_TeamColorImage.gameObject.SetActive(value: false);
	}

	public void Click()
	{
		colorWheel.StartColorPicking(submeshIndex);
	}

	public void Enter()
	{
		unitEditorHighlightingManager.BlinkClothes(equipedItem, submeshIndex);
	}

	public void Exit()
	{
		unitEditorHighlightingManager.StopBlinking(equipedItem, submeshIndex);
	}
}
