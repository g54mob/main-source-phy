using TMPro;
using UnityEngine;

public class BudgetToolTip : MonoBehaviour
{
	public RectTransform m_Background;

	public TextMeshProUGUI m_BudgetLabel;

	public TextMeshProUGUI m_CashLeftLabel;

	public TextMeshProUGUI m_BudgetValue;

	public TextMeshProUGUI m_CashLeftValue;

	public void Enable()
	{
		Populate();
		GameUI.SetScreenPosClamped(base.gameObject, GameInput.GetMousePosition(), Mathf.Abs(m_Background.offsetMin.x) + 20f, -50f);
		base.gameObject.SetActive(value: true);
	}

	public void Disable()
	{
		base.gameObject.SetActive(value: false);
	}

	private void Populate()
	{
		int num = Budget.m_CashBudget - Mathf.RoundToInt(Budget.m_BridgeCost);
		if (num >= 0)
		{
			m_BudgetLabel.text = Localize.Get("TOOLTIP_UNDER_BUDGET_BY");
			m_BudgetValue.text = Utils.FormatCash(num);
		}
		else
		{
			m_BudgetLabel.text = Localize.Get("TOOLTIP_OVER_BUDGET_BY");
			m_BudgetValue.text = Utils.FormatCash(Mathf.Abs(num));
		}
		m_CashLeftLabel.text = Localize.Get("TOOLTIP_CASH_LEFT");
		m_CashLeftValue.text = Utils.FormatCash(Budget.GetRemainingFromHardBudget());
	}
}
