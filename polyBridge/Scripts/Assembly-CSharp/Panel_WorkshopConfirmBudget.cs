using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_WorkshopConfirmBudget : MonoBehaviour
{
	public TextMeshProUGUI m_CurrentCost;

	public SandboxInputField m_CashBudgetInputField;

	public Button m_OKButton;

	private int m_OriginalBudget;

	private void Start()
	{
		m_OKButton.onClick.AddListener(SaveAndClose);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
		if (Budget.m_CashBudget < Mathf.RoundToInt(Budget.m_BridgeCost))
		{
			Budget.m_CashBudget = Mathf.RoundToInt(Budget.m_BridgeCost);
			RefreshInputFields();
		}
	}

	public void Open()
	{
		base.gameObject.SetActive(value: true);
		m_OriginalBudget = Budget.m_CashBudget;
		SetSuggestedBudget();
		RefreshProperties();
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void RefreshProperties()
	{
		RefreshText();
		RefreshInputFields();
	}

	public void Cancel()
	{
		Budget.m_CashBudget = m_OriginalBudget;
		Close();
	}

	public void Close()
	{
		InterfaceAudio.Play("ui_menubar_gen_off");
		base.gameObject.SetActive(value: false);
	}

	private void RefreshText()
	{
		m_CurrentCost.text = string.Format(Localize.Get("UI_WORKSHOP_BUDGET_COST_COLON"), Utils.FormatCash(Mathf.RoundToInt(Budget.m_BridgeCost)));
	}

	private void RefreshInputFields()
	{
		m_CashBudgetInputField.m_InputField.text = Utils.FormatCash(Budget.m_CashBudget);
	}

	private void SaveAndClose()
	{
		if (Budget.m_CashBudget < Mathf.RoundToInt(Budget.m_BridgeCost))
		{
			Budget.m_CashBudget = Mathf.RoundToInt(Budget.m_BridgeCost);
		}
		if (!string.IsNullOrEmpty(Sandbox.m_CurrentLayoutName))
		{
			SandboxLayoutData sandboxLayoutData = SandboxLayout.Save(Sandbox.m_CurrentLayoutName);
			if (sandboxLayoutData != null)
			{
				Sandbox.m_CurrentLayoutData = sandboxLayoutData;
				Sandbox.m_UnsavedChanges = false;
			}
		}
		Close();
		GameUI.m_Instance.m_WorkshopSubmit.Submit();
	}

	private void SetSuggestedBudget()
	{
		Budget.m_CashBudget = Mathf.CeilToInt(Budget.m_BridgeCost * 1.5f / 1000f) * 1000;
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)))
		{
			Cancel();
		}
	}
}
