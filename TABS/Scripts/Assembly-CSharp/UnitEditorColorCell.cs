using Landfall.TABS;
using Landfall.TABS.UnitEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEditorColorCell : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image m_Image;

	private UnitEditorColorPicker ColorPicker;

	private ColorPaletteData colorPaletteData;

	public void OnPointerClick(PointerEventData eventData)
	{
		ColorPicker.Color(colorPaletteData);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ColorPicker.OnColorPreviewEnter(colorPaletteData);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ColorPicker.OnColorPreviewExit(colorPaletteData);
	}

	public void Setup(ColorPaletteData colorData, UnitEditorColorPicker colorPicker)
	{
		m_Image.color = colorData.m_color;
		colorPaletteData = colorData;
		ColorPicker = colorPicker;
	}

	public void Setup(TeamColorPaletteData colorData, UnitEditorColorPicker colorPicker)
	{
		m_Image.color = colorData.GetColor(Team.Red);
		ColorPicker = colorPicker;
	}
}
