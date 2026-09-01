using UnityEngine;
using UnityEngine.UI;

public class Panel_TraceTool : MonoBehaviour
{
	[Header("Buttons")]
	public Button m_Fill;

	public Button m_TangentsLocked;

	public Button m_TangentsFree;

	public Button m_Clear;

	[Header("Panels")]
	public GameObject m_RolloutPanel;

	public GameObject m_FillPanel;

	[Header("Sliders")]
	public Slider m_FillSlider;

	[Header("Shapes")]
	public Button m_Shape;

	public Sprite m_StraightLineSprite;

	public Sprite m_CurvedLineSprite;

	[Header("Grid")]
	public Button m_Grid;

	public Sprite m_GridOn;

	public Sprite m_GridOff;

	private bool m_FillSliderScrolling;

	private static readonly float FILL_LENGTH_MIN = 0.5f;

	private static readonly float FILL_LENGTH_MAX = 4f;

	private void Awake()
	{
		m_Shape.onClick.AddListener(OnShape);
		m_Grid.onClick.AddListener(OnGrid);
		m_Fill.onClick.AddListener(OnFill);
		m_TangentsLocked.onClick.AddListener(OnTangentsLocked);
		m_TangentsFree.onClick.AddListener(OnTangentsFree);
		m_Clear.onClick.AddListener(OnClear);
	}

	private void OnEnable()
	{
		m_FillSliderScrolling = false;
		UpdateShape();
		UpdateGrid();
		UpdateButtons();
	}

	private void OnDisable()
	{
		m_FillSliderScrolling = false;
	}

	private void Update()
	{
		if (m_FillSlider.gameObject.activeInHierarchy)
		{
			TrackFillSliderScrolling();
			UpdateFillSliderToolTip();
		}
		UpdateShape();
		UpdateGrid();
		UpdateButtons();
	}

	public bool IsFillSliderScrolling()
	{
		return m_FillSliderScrolling;
	}

	public void OnEnterBuildMode()
	{
		m_FillSliderScrolling = false;
	}

	private void UpdateButtons()
	{
		m_TangentsFree.gameObject.SetActive(!BridgeTrace.TangentsLocked());
		m_TangentsLocked.gameObject.SetActive(BridgeTrace.TangentsLocked());
		if (BridgeTrace.IsTracingActive())
		{
			GameUI.m_Instance.m_BuildToolBar.m_TraceButton.image.color = GameUI.m_Instance.m_GoldColor;
			bool flag = BridgeTrace.IsTraceLinePlaced();
			m_RolloutPanel.SetActive(value: true);
			m_Fill.interactable = flag;
			m_TangentsLocked.interactable = true;
			m_TangentsFree.interactable = true;
			m_Clear.interactable = true;
			m_Shape.interactable = true;
			m_Grid.interactable = true;
			m_FillPanel.SetActive(flag);
			return;
		}
		GameUI.m_Instance.m_BuildToolBar.m_TraceButton.image.color = Color.white;
		bool interactable = BridgeTrace.IsTraceLinePlaced();
		m_RolloutPanel.SetActive(BridgeTrace.IsVisible());
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			m_Fill.interactable = false;
			m_TangentsLocked.interactable = false;
			m_TangentsFree.interactable = false;
		}
		else
		{
			m_Fill.interactable = interactable;
			m_TangentsLocked.interactable = true;
			m_TangentsFree.interactable = true;
		}
		m_Shape.interactable = true;
		m_Grid.interactable = true;
		m_Clear.interactable = true;
		m_FillPanel.SetActive(value: false);
	}

	private void UpdateShape()
	{
		m_Shape.image.sprite = GetSpriteForShape(BridgeTrace.m_Shape);
	}

	private void UpdateGrid()
	{
		m_Grid.image.sprite = (BridgeTrace.m_SnapToGrid ? m_GridOn : m_GridOff);
	}

	private Sprite GetSpriteForShape(ArcShape shape)
	{
		switch (shape)
		{
		case ArcShape.CURVED:
			return m_CurvedLineSprite;
		case ArcShape.FLAT:
			return m_StraightLineSprite;
		default:
			Debug.LogWarningFormat("Unhandled Bridge Trace Shape {0} in GetSpriteForShape", shape.ToString());
			return m_Shape.image.sprite;
		}
	}

	public void OnFill()
	{
		if (GameStateManager.GetPendingState() == GameState.SIM)
		{
			return;
		}
		if (Bridge.m_BuildMaterialType == BridgeMaterialType.PILLAR || BridgeTrace.IsFilling())
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		BridgeJointMovement.CancelSelection();
		if (BridgeTrace.Fill(GetFillSegmentLength()))
		{
			InterfaceAudio.Play("ui_build_tracetool_fill");
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	public void OnShape()
	{
		if (BridgeTrace.m_Shape == ArcShape.CURVED)
		{
			BridgeTrace.m_Shape = ArcShape.FLAT;
		}
		else
		{
			BridgeTrace.m_Shape = ArcShape.CURVED;
		}
		BridgeTrace.m_ArcTracer.SetShape(BridgeTrace.m_Shape);
		if (Profiles.m_ActiveProfile.m_ArcShape != BridgeTrace.m_Shape)
		{
			Profiles.m_ActiveProfile.m_ArcShape = BridgeTrace.m_Shape;
			Profiles.SaveActiveProfile();
		}
		InterfaceAudio.Play("ui_build_tracetool_shape_toggle");
	}

	public void OnGrid()
	{
		BridgeTrace.m_SnapToGrid = !BridgeTrace.m_SnapToGrid;
		if (Profiles.m_ActiveProfile.m_ArcSnapToGrid != BridgeTrace.m_SnapToGrid)
		{
			Profiles.m_ActiveProfile.m_ArcSnapToGrid = BridgeTrace.m_SnapToGrid;
			Profiles.SaveActiveProfile();
		}
		InterfaceAudio.Play("ui_settings_toggle");
	}

	public void OnTangentsLocked()
	{
		m_TangentsFree.gameObject.SetActive(value: true);
		m_TangentsLocked.gameObject.SetActive(value: false);
		BridgeTrace.m_ArcTracer.UnLockTangents();
		InterfaceAudio.Play("ui_build_tracetool_tangent_toggle");
	}

	public void OnTangentsFree()
	{
		m_TangentsLocked.gameObject.SetActive(value: true);
		m_TangentsFree.gameObject.SetActive(value: false);
		BridgeTrace.m_ArcTracer.LockTangents();
		InterfaceAudio.Play("ui_build_tracetool_tangent_toggle");
	}

	public void OnFlip()
	{
		BridgeTrace.m_ArcTracer.Flip();
		InterfaceAudio.Play("ui_build_flip");
	}

	public void OnClear()
	{
		BridgeTrace.ClearTraceLine();
		BridgeTrace.TurnOffTracing();
		InterfaceAudio.Play("ui_build_tracetool_clear_select");
	}

	public float GetFillSegmentLength()
	{
		BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(Bridge.m_BuildMaterialType);
		if (bridgeMaterial == null)
		{
			return 1f;
		}
		float b = Mathf.Min(FILL_LENGTH_MAX, bridgeMaterial.m_MaxLength);
		return Mathf.Lerp(FILL_LENGTH_MIN, b, Mathf.Clamp01(m_FillSlider.value / 100f));
	}

	private void TrackFillSliderScrolling()
	{
		if (GameInput.GetMouseButtonJustPressed(0) && PointerOverSliderFill())
		{
			m_FillSliderScrolling = true;
		}
		if (GameInput.GetMouseButtonJustReleased(0))
		{
			m_FillSliderScrolling = false;
		}
	}

	private void UpdateFillSliderToolTip()
	{
		if (PointerOverPanelFill() || m_FillSliderScrolling)
		{
			float fillSegmentLength = GetFillSegmentLength();
			GameUI.ToolTipForceEnable($"Max Length: {Utils.FormatDistanceOneDecimalPlace(fillSegmentLength)}");
		}
	}

	private bool PointerOverSliderFill()
	{
		return GameUI.PointerOver(typeof(Slider_Fill));
	}

	private bool PointerOverPanelFill()
	{
		return GameUI.PointerOver(typeof(Panel_Fill));
	}
}
