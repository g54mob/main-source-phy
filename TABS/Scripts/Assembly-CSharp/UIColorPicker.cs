using System;
using DM;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.UI;

public class UIColorPicker : MonoBehaviour
{
	[SerializeField]
	private UIColorButton m_colorButtonTemplate;

	[SerializeField]
	private UIColorButton m_teamColorButtonTemplate;

	[SerializeField]
	private Button m_closeButton;

	[SerializeField]
	private RectTransform m_selected;

	[SerializeField]
	private RectTransform m_DefaultPosition;

	private CanvasGroup m_canvasGroup;

	private CharacterItem m_currentProp;

	private int m_currentColorIndex = -1;

	private Action<Color> m_newColorCallback;

	private UIColorButton m_defaultColor;

	private UIColorButton[] m_colorButtons;

	private UIColorButton[] m_teamColorButtons;

	public event Action<UIColorButton> Opened;

	public event Action Closed;

	private void Awake()
	{
		m_canvasGroup = GetComponent<CanvasGroup>();
		m_colorButtonTemplate.gameObject.SetActive(value: false);
		m_teamColorButtonTemplate.gameObject.SetActive(value: false);
		m_defaultColor = UnityEngine.Object.Instantiate(m_colorButtonTemplate, m_colorButtonTemplate.transform.parent);
		m_defaultColor.gameObject.SetActive(value: true);
		m_defaultColor.SetCallback(delegate(ColorPaletteData newColor)
		{
			m_newColorCallback(newColor.m_color);
			CloseColorPicker();
		});
		m_defaultColor.SetCallback(delegate(TeamColorPaletteData newColor)
		{
			m_newColorCallback(newColor.GetColor(UnitEditorTeamButtons._CurrentTeam));
			CloseColorPicker();
		});
		ColorPaletteData[] colors = ContentDatabase.Instance().GetUnitEditorColorPalette().Colors;
		m_colorButtons = new UIColorButton[colors.Length];
		for (int num = 0; num < colors.Length; num++)
		{
			UIColorButton uIColorButton = UnityEngine.Object.Instantiate(m_colorButtonTemplate, m_colorButtonTemplate.transform.parent);
			uIColorButton.gameObject.SetActive(value: true);
			uIColorButton.SetColor(colors[num]);
			uIColorButton.SetCallback(delegate(ColorPaletteData newColor)
			{
				m_newColorCallback(newColor.m_color);
				CloseColorPicker();
			});
			m_colorButtons[num] = uIColorButton;
		}
		TeamColorPaletteData[] teamColors = ContentDatabase.Instance().GetUnitEditorColorPalette().TeamColors;
		m_teamColorButtons = new UIColorButton[teamColors.Length];
		for (int num2 = 0; num2 < teamColors.Length; num2++)
		{
			UIColorButton uIColorButton2 = UnityEngine.Object.Instantiate(m_teamColorButtonTemplate, m_teamColorButtonTemplate.transform.parent);
			uIColorButton2.gameObject.SetActive(value: true);
			uIColorButton2.SetColor(teamColors[num2]);
			uIColorButton2.SetCallback(delegate(TeamColorPaletteData newColor)
			{
				m_newColorCallback(newColor.GetColor(UnitEditorTeamButtons._CurrentTeam));
				CloseColorPicker();
			});
			m_teamColorButtons[num2] = uIColorButton2;
		}
		m_closeButton.onClick.AddListener(CloseColorPicker);
		CloseColorPicker();
	}

	public void OpenColorPicker(CharacterItem prop, int colorIndex, Action<Color> newColorCallback)
	{
		if (!(prop == null))
		{
			m_canvasGroup.alpha = 1f;
			m_canvasGroup.interactable = true;
			m_canvasGroup.blocksRaycasts = true;
			m_currentProp = prop;
			m_currentColorIndex = colorIndex;
			m_newColorCallback = newColorCallback;
			CharacterItem.RendererMaterialWrapper rendererMaterialWrapper = prop.SharedMaterials[colorIndex];
			int paletteIndex = rendererMaterialWrapper.m_paletteIndex;
			bool hasTeamColor = rendererMaterialWrapper.m_hasTeamColor;
			UIColorButton uIColorButton = ((paletteIndex >= 0) ? (hasTeamColor ? m_teamColorButtons[paletteIndex] : m_colorButtons[paletteIndex]) : m_defaultColor);
			m_selected.position = uIColorButton.transform.position;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out var localPoint);
			(base.transform as RectTransform).anchoredPosition = localPoint;
			CharacterItem.RendererMaterialWrapper rendererMaterialWrapper2 = prop.DefaultColors[colorIndex];
			if (!rendererMaterialWrapper2.m_hasTeamColor)
			{
				ColorPaletteData color = new ColorPaletteData
				{
					m_material = rendererMaterialWrapper2.m_material,
					m_color = rendererMaterialWrapper2.m_material.color,
					ColorIndex = -1
				};
				m_defaultColor.SetColor(color);
			}
			else
			{
				TeamColorPaletteData[] teamColors = ContentDatabase.Instance().GetUnitEditorColorPalette().TeamColors;
				m_defaultColor.SetColor(teamColors[rendererMaterialWrapper2.m_paletteIndex]);
			}
			m_defaultColor.SetRenderer(rendererMaterialWrapper);
			for (int i = 0; i < m_colorButtons.Length; i++)
			{
				m_colorButtons[i].SetRenderer(rendererMaterialWrapper);
			}
			for (int j = 0; j < m_teamColorButtons.Length; j++)
			{
				m_teamColorButtons[j].SetRenderer(rendererMaterialWrapper);
			}
			this.Opened?.Invoke(uIColorButton);
		}
	}

	public void CloseColorPicker()
	{
		m_canvasGroup.alpha = 0f;
		m_canvasGroup.interactable = false;
		m_canvasGroup.blocksRaycasts = false;
		m_currentProp = null;
		m_currentColorIndex = -1;
		m_newColorCallback = null;
		this.Closed?.Invoke();
	}
}
