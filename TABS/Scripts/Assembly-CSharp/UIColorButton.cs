using System;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIColorButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[SerializeField]
	private Image m_colorImage;

	private ColorPaletteData m_color;

	private TeamColorPaletteData m_teamColor;

	private bool m_isTeamColor;

	private CharacterItem.RendererMaterialWrapper m_renderer;

	public void SetCallback(Action<ColorPaletteData> callback)
	{
		GetComponent<Button>().onClick.AddListener(delegate
		{
			if (!m_isTeamColor)
			{
				callback(m_color);
				m_renderer.SetMaterial(m_color.m_material, m_color.ColorIndex, teamColor: false);
			}
		});
	}

	public void SetCallback(Action<TeamColorPaletteData> callback)
	{
		GetComponent<Button>().onClick.AddListener(delegate
		{
			if (m_isTeamColor)
			{
				callback(m_teamColor);
				m_renderer.SetMaterial(m_teamColor.GetMaterial(UnitEditorTeamButtons._CurrentTeam), m_teamColor.ColorIndex, teamColor: true);
			}
		});
		UnitEditorTeamButtons._OnTeamChanged = (Action<Team>)Delegate.Combine(UnitEditorTeamButtons._OnTeamChanged, new Action<Team>(OnUpdateTeam));
	}

	private void OnUpdateTeam(Team team)
	{
		if (m_isTeamColor)
		{
			m_colorImage.color = m_teamColor.GetColor(team);
		}
	}

	public void SetColor(ColorPaletteData c)
	{
		m_color = c;
		m_colorImage.color = c.m_color;
		m_isTeamColor = false;
	}

	public void SetColor(TeamColorPaletteData c)
	{
		m_teamColor = c;
		m_colorImage.color = c.GetColor(UnitEditorTeamButtons._CurrentTeam);
		m_isTeamColor = true;
	}

	public void SetRenderer(CharacterItem.RendererMaterialWrapper renderer)
	{
		m_renderer = renderer;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	private void OnDestroy()
	{
		UnitEditorTeamButtons._OnTeamChanged = (Action<Team>)Delegate.Remove(UnitEditorTeamButtons._OnTeamChanged, new Action<Team>(OnUpdateTeam));
	}
}
