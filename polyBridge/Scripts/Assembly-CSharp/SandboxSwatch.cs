using System;
using UnityEngine;
using UnityEngine.UI;

public class SandboxSwatch : MonoBehaviour
{
	public Image m_SwatchImage;

	public GameObject m_InnerSwatch;

	public Image m_InnerSwatchImage;

	public Button m_SwatchButton;

	[NonSerialized]
	public VehicleSkin m_VehicleSkin;

	private Action<SandboxSwatch, bool> m_OnPressedAction;

	private void Start()
	{
		m_SwatchButton.onClick.AddListener(OnSwatch);
	}

	public void Init(VehicleSkin skin, Action<SandboxSwatch, bool> action)
	{
		m_VehicleSkin = skin;
		m_OnPressedAction = action;
		m_SwatchImage.color = skin.GetColorForUI();
		m_InnerSwatchImage.color = skin.GetColorForUI();
	}

	public void Highlight(bool on)
	{
		m_InnerSwatch.SetActive(on);
		m_SwatchImage.color = (on ? Color.white : m_VehicleSkin.GetColorForUI());
	}

	private void OnSwatch()
	{
		m_OnPressedAction?.Invoke(this, arg2: false);
	}
}
