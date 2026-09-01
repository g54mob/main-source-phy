using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ClipboardToolbar : MonoBehaviour
{
	public RectTransform m_Root;

	public GameObject m_RotationTextContainer;

	public TextMeshProUGUI m_RotationText;

	public Button m_FlipHoriz;

	public Button m_FlipVert;

	public Button m_RotateLeft;

	public Button m_RotateRight;

	public PointerEvents m_RotateLeftHover;

	public PointerEvents m_RotateRightHover;

	public PointerToolTip m_PointerToolTip;

	public GameObject[] m_HideForGamepad;

	private static readonly int ANCHOR_Y_DEFAULT = 65;

	private static readonly int ANCHOR_Y_DEFAULT_GAMEPAD = 45;

	[NonSerialized]
	public bool m_DisplayRotationText;

	private void Awake()
	{
		m_FlipHoriz.onClick.AddListener(OnFlipHoriz);
		m_FlipVert.onClick.AddListener(OnFlipVert);
		m_RotateLeft.onClick.AddListener(OnRotateLeft);
		m_RotateRight.onClick.AddListener(OnRotateRight);
		m_PointerToolTip.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		m_RotationText.text = (m_DisplayRotationText ? GetRotationText() : string.Empty);
		m_RotationTextContainer.SetActive(m_DisplayRotationText);
	}

	private void OnEnable()
	{
		m_DisplayRotationText = false;
		m_PointerToolTip.Disable();
		UpdateForCurrentDevice();
	}

	public void UpdateForCurrentDevice()
	{
		m_Root.anchoredPosition = new Vector2(0f, (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? ANCHOR_Y_DEFAULT_GAMEPAD : ANCHOR_Y_DEFAULT);
		GameObject[] hideForGamepad = m_HideForGamepad;
		for (int i = 0; i < hideForGamepad.Length; i++)
		{
			hideForGamepad[i].SetActive(GameInput.GetActiveGameDevice() != GameDevice.Gamepad);
		}
	}

	public void OnClose()
	{
		InterfaceAudio.Play("ui_menu_cancel");
		if (ClipboardManager.ReadyToPaste())
		{
			ClipboardManager.ClearClipboard();
		}
		GameUI.m_Instance.m_Clipboard.gameObject.SetActive(value: false);
	}

	public void OnFlipHoriz()
	{
		ClipboardManager.FlipHorizontal();
		InterfaceAudio.Play("ui_build_flip");
	}

	public void OnFlipVert()
	{
		ClipboardManager.FlipVertical();
		InterfaceAudio.Play("ui_build_flip");
	}

	public void OnRotateLeft()
	{
		ClipboardManager.StartRotate(1f);
		InterfaceAudio.Play("ui_build_rotate");
	}

	public void OnRotateRight()
	{
		ClipboardManager.StartRotate(-1f);
		InterfaceAudio.Play("ui_build_rotate");
	}

	private string GetRotationText()
	{
		float rotationDegrees = ClipboardManager.GetRotationDegrees();
		return Utils.FormatAngle((rotationDegrees > 180f) ? (rotationDegrees - 360f) : rotationDegrees);
	}
}
