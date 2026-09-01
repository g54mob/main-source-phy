using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxWorkshopSubmit : MonoBehaviour
{
	[Header("Buttons")]
	public Button m_SubmitToWorkshopButton;

	private void Start()
	{
		m_SubmitToWorkshopButton.onClick.AddListener(OnSubmitToWorkshop);
	}

	private void OnSubmitToWorkshop()
	{
		if (GameManager.IsSteamOffline())
		{
			PopUpMessage.DisplayErrorOkOnly(Localize.Get("UI_STEAM_OFFLINE"));
			return;
		}
		if (EventEditor.IsIconMoving())
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		SandboxSelectionSet.CancelSelection();
		if (!WorkshopSubmit.VehiclesInLevel())
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_WORKSHOP_MIN_VEHICLES"));
			return;
		}
		if (Mods.IsUsingLocalUGC())
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_WORKSHOP_LOCAL_UGC"));
			return;
		}
		int num = Mathf.RoundToInt(Budget.m_BridgeCost);
		if (num > Budget.m_CashBudget)
		{
			int cashBudget = Budget.m_CashBudget;
			PopUpMessage.DisplayWarningOkOnly(string.Format(Localize.Get("WARN_WORKSHOP_BUDGET_EXCEEDED"), Utils.FormatCash(num), Utils.FormatCash(cashBudget)));
			return;
		}
		BridgeMaterialType firstNegativeMaterial = Budget.GetFirstNegativeMaterial();
		if (firstNegativeMaterial != BridgeMaterialType.INVALID)
		{
			PopUpMessage.DisplayWarningOkOnly(string.Format(Localize.Get("WARN_WORKSHOP_MATERIALS_EXCEEDED"), BridgeMaterials.GetLocalizedMaterialDisplayName(firstNegativeMaterial)));
			return;
		}
		if (WorkshopSubmit.BridgeHasIllegalNodePlacement())
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_WORKSHOP_ILLEGAL_NODES"));
			return;
		}
		if (BridgeJoints.GetNumSplitJoints() > 0 && HydraulicsPhases.m_Phases.Count == 0)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_ILLEGAL_SPLIT_NODES"));
			return;
		}
		if (Mods.m_IsUsingGameplayMod)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_WORKSHOP_WILL_EMBED_MODS"));
		}
		if (!GameUI.m_Instance.m_WorkshopSubmit.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_window_open");
			GameUI.m_Instance.m_WorkshopSubmit.Open(Game.GetLevelId());
		}
	}
}
