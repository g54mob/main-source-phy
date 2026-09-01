using System;
using UnityEngine;
using UnityEngine.UI;

public class ProfileAvatarSwatch : MonoBehaviour
{
	public Image m_SwatchOutline;

	public Image m_SwatchImage;

	public Button m_SwatchButton;

	public Color m_DefaultOutlineColor;

	[NonSerialized]
	public VehicleSkin m_VehicleSkin;

	private Action<ProfileAvatarSwatch> m_OnPressedAction;

	private void Start()
	{
		m_SwatchButton.onClick.AddListener(OnSwatch);
	}

	public void Init(VehicleSkin skin, Action<ProfileAvatarSwatch> action)
	{
		m_VehicleSkin = skin;
		m_OnPressedAction = action;
		m_SwatchOutline.color = m_DefaultOutlineColor;
		m_SwatchImage.color = skin.GetColorForUI();
	}

	public void Highlight(bool on)
	{
		m_SwatchOutline.color = (on ? GameUI.m_Instance.m_GoldColor : m_DefaultOutlineColor);
	}

	private void OnSwatch()
	{
		m_OnPressedAction?.Invoke(this);
		InterfaceAudio.Play("ui_menubar_gen_on");
	}
}
