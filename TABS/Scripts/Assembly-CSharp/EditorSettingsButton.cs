using System;
using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditorSettingsButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public TextMeshProUGUI m_title;

	public Action<bool> m_hoverCallback;

	private Action<Team> m_teamColorCallback;

	protected virtual void Awake()
	{
	}

	public void SetText(string txt)
	{
		if ((bool)m_title)
		{
			m_title.text = txt.ToUpper();
		}
	}

	public void SetTeamColorCallback(Action<Team> teamCallback)
	{
		m_teamColorCallback = teamCallback;
		UnitEditorTeamButtons._OnTeamChanged = (Action<Team>)Delegate.Combine(UnitEditorTeamButtons._OnTeamChanged, teamCallback);
	}

	public void AddHoverCallback(Action<bool> callback)
	{
		m_hoverCallback = callback;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_hoverCallback?.Invoke(obj: true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_hoverCallback?.Invoke(obj: false);
	}

	private void OnDestroy()
	{
		if (m_teamColorCallback != null)
		{
			UnitEditorTeamButtons._OnTeamChanged = (Action<Team>)Delegate.Remove(UnitEditorTeamButtons._OnTeamChanged, m_teamColorCallback);
		}
	}
}
