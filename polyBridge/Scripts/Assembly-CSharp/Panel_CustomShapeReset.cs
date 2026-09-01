using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CustomShapeReset : MonoBehaviour
{
	[Header("Num Sides")]
	public TMP_InputField m_NumSidesInputField;

	public Button m_NumSidesInputFieldGamepadButton;

	[Header("Radius")]
	public TMP_InputField m_RadiusInputField;

	public Button m_RadiusInputFieldGamepadButton;

	[Header("Buttons")]
	public Button m_CancelButton;

	public Button m_OkButton;

	[NonSerialized]
	public CustomShape m_CustomShape;

	private void Awake()
	{
		m_CancelButton.onClick.AddListener(Close);
		m_OkButton.onClick.AddListener(OnOk);
		m_NumSidesInputFieldGamepadButton.onClick.AddListener(OnNumSidesInputFieldGamepadButton);
		m_RadiusInputFieldGamepadButton.onClick.AddListener(OnRadiusInputFieldGamepadButton);
	}

	private void Update()
	{
		ProcessInput();
	}

	private void OnEnable()
	{
		m_NumSidesInputField.characterLimit = 3;
		m_NumSidesInputField.caretWidth = 1;
		m_NumSidesInputField.selectionColor = GameUI.m_Instance.m_InputFieldSelectColor;
		m_NumSidesInputField.text = "5";
		m_RadiusInputField.characterLimit = 6;
		m_RadiusInputField.caretWidth = 1;
		m_RadiusInputField.selectionColor = GameUI.m_Instance.m_InputFieldSelectColor;
		m_RadiusInputField.text = "1";
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	public void UpdateForCurrentDevice()
	{
		m_NumSidesInputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_RadiusInputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_NumSidesInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
		m_RadiusInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
	}

	public void Close()
	{
		if (GameUI.m_Instance.m_CustomShapeReset.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_window_close");
		}
		GameUI.m_Instance.m_CustomShapeReset.gameObject.SetActive(value: false);
	}

	private void OnOk()
	{
		if (TryResetShape())
		{
			InterfaceAudio.Play("ui_menu_accept");
			int result = CustomShapes.NGON_DEFAULT_NUM_EDGES;
			if (int.TryParse(m_NumSidesInputField.text.Trim(), out result) && result != 5)
			{
				SandboxUndo.SnapShot();
			}
			Close();
		}
		else if (!GameUI.m_Instance.m_PopUpMessage.gameObject.activeInHierarchy)
		{
			PopUpMessage.DisplayErrorOkOnly(Localize.Get("WARN_FAILED_SHAPE_CREATE"));
		}
	}

	private bool TryResetShape()
	{
		if (!int.TryParse(m_NumSidesInputField.text.Trim(), out var result))
		{
			return false;
		}
		if (result < CustomShapes.NGON_MIN_NUM_EDGES)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_SHAPE_MIN_SIDES"));
			return false;
		}
		if (result > CustomShapes.NGON_MAX_NUM_EDGES)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_SHAPE_MAX_SIDES", CustomShapes.NGON_MAX_NUM_EDGES.ToString()));
			return false;
		}
		float result2 = CustomShapes.NGON_DEFAULT_RADIUS;
		if (!float.TryParse(m_RadiusInputField.text.Trim().Replace(',', '.'), out result2))
		{
			return false;
		}
		if (result2 < (float)CustomShapes.NGON_MIN_RADIUS)
		{
			result2 = CustomShapes.NGON_MIN_RADIUS;
		}
		if (result2 > (float)CustomShapes.NGON_MAX_RADIUS)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_SHAPE_MAX_RADIUS", CustomShapes.NGON_MAX_RADIUS.ToString()));
			return false;
		}
		if (!m_CustomShape)
		{
			return false;
		}
		m_CustomShape.DestroyVertsAndEdges();
		m_CustomShape.InitializeFromParameters(result, result2, m_CustomShape.m_Color);
		m_CustomShape.UpdateVisualScale();
		m_CustomShape.m_SandboxItem.SetOutlineDirty(dirty: true);
		return true;
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && Input.GetKeyDown(KeyCode.Escape))
		{
			Close();
		}
	}

	private void OnNumSidesInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_NumSidesInputField.text, m_NumSidesInputField.characterLimit, Localize.Get("UI_RESETSHAPE_NUM_SIDES_COLON").Replace(":", string.Empty), multiline: false, OnNumSidesEntered);
	}

	private void OnRadiusInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_RadiusInputField.text, m_RadiusInputField.characterLimit, Localize.Get("UI_RESETSHAPE_RADIUS_COLON").Replace(":", string.Empty), multiline: true, OnRadiusEntered);
	}

	private void OnNumSidesEntered(string text)
	{
		if (text != null)
		{
			m_NumSidesInputField.text = text;
		}
	}

	private void OnRadiusEntered(string text)
	{
		if (text != null)
		{
			m_RadiusInputField.text = text;
		}
	}
}
