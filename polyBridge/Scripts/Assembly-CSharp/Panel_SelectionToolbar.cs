using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SelectionToolbar : MonoBehaviour
{
	public RectTransform m_Root;

	public Button m_Delete;

	public Button m_Copy;

	public Button m_Cut;

	public Button m_LockSoft;

	public Button m_Lock;

	public Button m_UnLock;

	public TextMeshProUGUI m_CostAndMass;

	public GameObject m_CopyTutorialArrow;

	public GameObject[] m_HideForGamepad;

	private static readonly int ANCHOR_Y_DEFAULT_SANDBOX = 5;

	private static readonly int ANCHOR_Y_DEFAULT_BUILD = 65;

	private static readonly int ANCHOR_Y_DEFAULT_SANDBOX_GAMEPAD = -15;

	private static readonly int ANCHOR_Y_DEFAULT_BUILD_GAMEPAD = 45;

	private void Awake()
	{
		m_Delete.onClick.AddListener(OnDelete);
		m_Copy.onClick.AddListener(OnCopy);
		m_Cut.onClick.AddListener(OnCut);
		m_LockSoft.onClick.AddListener(OnLockSoft);
		m_Lock.onClick.AddListener(OnLock);
		m_UnLock.onClick.AddListener(OnUnLock);
		m_CopyTutorialArrow.SetActive(value: false);
	}

	private void OnEnable()
	{
		m_Delete.gameObject.SetActive(GameStateManager.GetState() == GameState.BUILD);
		m_Copy.gameObject.SetActive(GameStateManager.GetState() == GameState.BUILD);
		m_Cut.gameObject.SetActive(GameStateManager.GetState() == GameState.BUILD);
		m_LockSoft.gameObject.SetActive(GameStateManager.GetState() == GameState.SANDBOX);
		m_Lock.gameObject.SetActive(GameStateManager.GetState() == GameState.SANDBOX);
		m_UnLock.gameObject.SetActive(GameStateManager.GetState() == GameState.SANDBOX);
		SetCostAndMass();
		UpdateForCurrentDevice();
	}

	public void UpdateForCurrentDevice()
	{
		GameObject[] hideForGamepad = m_HideForGamepad;
		for (int i = 0; i < hideForGamepad.Length; i++)
		{
			hideForGamepad[i].SetActive(GameInput.GetActiveGameDevice() != GameDevice.Gamepad);
		}
	}

	private void Update()
	{
		m_Copy.interactable = !BridgeSelectionSet.OnlyContainsJoints() && !CampaignTutorial.BlockCopy();
		m_Cut.interactable = !BridgeSelectionSet.OnlyContainsJoints() && !Game.IsCurrentLevelTutorial();
		SetCostAndMass();
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			m_Root.anchoredPosition = new Vector2(0f, GameUI.m_Instance.m_EventEditor.m_RootRectTransform.anchoredPosition.y + (float)((GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? ANCHOR_Y_DEFAULT_SANDBOX_GAMEPAD : ANCHOR_Y_DEFAULT_SANDBOX));
		}
		else
		{
			m_Root.anchoredPosition = new Vector2(0f, (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? ANCHOR_Y_DEFAULT_BUILD_GAMEPAD : ANCHOR_Y_DEFAULT_BUILD);
		}
	}

	public void OnClose()
	{
		InterfaceAudio.Play("ui_menu_cancel");
		BridgeSelectionSet.CancelSelection();
	}

	public void OnDelete()
	{
		BridgePillarMovement.CancelMovement();
		BridgeSelectionSet.DeleteSelectionSet();
		BridgeActions.FlushRecording();
		InterfaceAudio.Play("ui_build_delete");
	}

	public void OnCopy()
	{
		BridgeSelectionSet.CopySelectionSet();
		BridgeSelectionSet.CancelSelection();
		BridgeTrace.TurnOffTracing();
		InterfaceAudio.Play("ui_build_copy");
	}

	public void OnCut()
	{
		BridgeSelectionSet.CutSelectionSet();
		InterfaceAudio.Play("ui_build_cut");
	}

	public void OnLockSoft()
	{
		SetPrebuildState(PrebuiltState.SOFT_LOCKED);
		InterfaceAudio.Play("ui_build_select");
	}

	public void OnLock()
	{
		SetPrebuildState(PrebuiltState.HARD_LOCKED);
		InterfaceAudio.Play("ui_build_select");
	}

	public void OnUnLock()
	{
		SetPrebuildState(PrebuiltState.NONE);
		InterfaceAudio.Play("ui_build_select");
	}

	public void ShowCopyTutorialArrow(bool show)
	{
		m_CopyTutorialArrow.SetActive(show);
	}

	private void SetPrebuildState(PrebuiltState prebuildState)
	{
		foreach (BridgeEdge edge in BridgeSelectionSet.m_Edges)
		{
			edge.SetPrebuiltState(prebuildState);
		}
		foreach (BridgePillar bridgePillar in BridgeSelectionSet.m_BridgePillars)
		{
			bridgePillar.SetPrebuiltState(prebuildState);
		}
	}

	private void SetCostAndMass()
	{
		string text = Utils.FormatCash(Mathf.RoundToInt(BridgeSelectionSet.GetCost()));
		string text2 = Utils.FormatMass(BridgeSelectionSet.GetMass() * BridgePhysics.KgToPg);
		m_CostAndMass.text = text + "   " + text2;
	}
}
