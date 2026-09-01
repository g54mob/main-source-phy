using System;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.UI;

public class EquipSettingsButton : EditorSettingsButton
{
	[SerializeField]
	private Toggle m_toggleLeft;

	[SerializeField]
	private Toggle m_toggleRight;

	private bool m_leftOn;

	private bool m_rightOn;

	private Action<UnitRig.EquipType> m_toggleChangedCallback;

	protected override void Awake()
	{
		base.Awake();
	}

	public void SetToggles(UnitRig.EquipType equipType)
	{
		switch (equipType)
		{
		case UnitRig.EquipType.BOTH:
			m_leftOn = true;
			m_rightOn = true;
			break;
		case UnitRig.EquipType.LEFT:
			m_leftOn = true;
			m_rightOn = false;
			break;
		case UnitRig.EquipType.RIGHT:
			m_leftOn = false;
			m_rightOn = true;
			break;
		}
		m_toggleLeft.isOn = m_leftOn;
		m_toggleRight.isOn = m_rightOn;
		m_toggleLeft.onValueChanged.AddListener(OnLeftToggleChanged);
		m_toggleRight.onValueChanged.AddListener(OnRightToggleChanged);
	}

	public void RegisterToggleCallback(Action<UnitRig.EquipType> callback)
	{
		m_toggleChangedCallback = callback;
	}

	private void OnLeftToggleChanged(bool value)
	{
		if (!value && !m_rightOn)
		{
			m_toggleLeft.isOn = true;
			return;
		}
		m_leftOn = value;
		OnToggleChanged();
	}

	private void OnRightToggleChanged(bool value)
	{
		if (!value && !m_leftOn)
		{
			m_toggleRight.isOn = true;
			return;
		}
		m_rightOn = value;
		OnToggleChanged();
	}

	private void OnToggleChanged()
	{
		if (m_toggleChangedCallback != null)
		{
			UnitRig.EquipType obj = UnitRig.EquipType.BOTH;
			if (!m_leftOn)
			{
				obj = UnitRig.EquipType.RIGHT;
			}
			if (!m_rightOn)
			{
				obj = UnitRig.EquipType.LEFT;
			}
			m_toggleChangedCallback(obj);
		}
	}
}
