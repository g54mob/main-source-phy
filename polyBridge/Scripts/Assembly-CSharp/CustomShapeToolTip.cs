using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomShapeToolTip : MonoBehaviour
{
	public RectTransform m_RectTransform;

	public LabelValueResizer m_LabelValueResizer;

	public HorizontalLayoutGroup m_HorizontalLayoutGroup;

	public TextMeshProUGUI m_BehaviorLabel;

	public TextMeshProUGUI m_MassLabel;

	public TextMeshProUGUI m_MotorStrengthLabel;

	public TextMeshProUGUI m_MotorSpeedLabel;

	public TextMeshProUGUI m_MotorAccelLabel;

	public TextMeshProUGUI m_Behavior;

	public TextMeshProUGUI m_Mass;

	public TextMeshProUGUI m_MotorStrength;

	public TextMeshProUGUI m_MotorSpeed;

	public TextMeshProUGUI m_MotorAccel;

	public GameObject m_NothingIcon;

	public GameObject m_CarIcon;

	public GameObject m_RoadIcon;

	public GameObject m_JointIcon;

	public GameObject m_RampsIcon;

	public GameObject m_SplitJointIcon;

	private readonly float PANEL_HEIGHT_DYNAMIC = 65f;

	private readonly float PANEL_HEIGHT_STATIC = 50f;

	private readonly float PANEL_HEIGHT_MOTORIZED = 105f;

	public void Enable(CustomShape shape)
	{
		base.gameObject.SetActive(value: true);
		Vector3 position = Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition()) + new Vector3(0f, 0.5f, 0f);
		Vector2 screenPos = Cameras.MainCamera().WorldToScreenPoint(position);
		GameUI.SetScreenPosClamped(base.gameObject, screenPos, 0f, 0f);
		m_BehaviorLabel.text = Localize.Get("UI_CUSTOM_SHAPE_BEHAVIOR") + ":";
		if (shape.IsMotorized())
		{
			InitMotorized(shape);
		}
		else if (shape.IsDynamic())
		{
			InitDynamic(shape);
		}
		else
		{
			InitStatic(shape);
		}
		InitCollidesWithIcons(shape);
		m_RectTransform.localScale = (Game.IsRunningOnSteamDeck() ? new Vector3(1.3f, 1.3f, 1f) : new Vector3(1f, 1f, 1f));
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_HorizontalLayoutGroup.GetComponent<RectTransform>());
		m_LabelValueResizer.ForceUpdate();
	}

	public void Disable()
	{
		base.gameObject.SetActive(value: false);
	}

	private void InitMotorized(CustomShape shape)
	{
		m_RectTransform.sizeDelta = new Vector2(m_RectTransform.sizeDelta.x, PANEL_HEIGHT_MOTORIZED);
		m_Behavior.text = Localize.Get("UI_CUSTOM_SHAPE_MOTORIZED");
		MakeMassValueActive(active: true);
		m_Mass.text = Utils.FormatWeight(shape.m_Mass * BridgePhysics.KgToPg);
		MakeMotorizedValuesActive(active: true);
		m_MotorStrengthLabel.text = Localize.Get("UI_SANDBOX_MOTOR_STRENGTH") + ":";
		m_MotorSpeedLabel.text = Localize.Get("UI_CUSTOM_SHAPE_MOTOR_SPEED") + ":";
		m_MotorAccelLabel.text = Localize.Get("UI_CUSTOM_SHAPE_MOTOR_ACCELERATION") + ":";
		m_MotorStrength.text = Utils.FormatOneDecimalPlace(shape.m_PinMotorStrength);
		m_MotorAccel.text = Utils.FormatSeconds(Mathf.Abs(shape.m_PinTargetAccelerationSeconds));
		m_MotorSpeed.text = Utils.FormatOneDecimalPlace(Mathf.Abs(shape.m_PinTargetVelocity));
		if (!Mathf.Approximately(shape.m_PinTargetVelocity, 0f))
		{
			if (shape.m_PinTargetVelocity > 0f)
			{
				m_MotorSpeed.text += " <sprite name=undo>";
			}
			else
			{
				m_MotorSpeed.text += " <sprite name=redo>";
			}
		}
	}

	private void InitDynamic(CustomShape shape)
	{
		m_RectTransform.sizeDelta = new Vector2(m_RectTransform.sizeDelta.x, PANEL_HEIGHT_DYNAMIC);
		m_Behavior.text = Localize.Get("UI_CUSTOM_SHAPE_DYNAMIC");
		MakeMassValueActive(active: true);
		m_Mass.text = Utils.FormatWeight(shape.m_Mass * BridgePhysics.KgToPg);
		MakeMotorizedValuesActive(active: false);
	}

	private void InitStatic(CustomShape shape)
	{
		m_RectTransform.sizeDelta = new Vector2(m_RectTransform.sizeDelta.x, PANEL_HEIGHT_STATIC);
		m_Behavior.text = Localize.Get("UI_CUSTOM_SHAPE_STATIC");
		MakeMassValueActive(active: false);
		MakeMotorizedValuesActive(active: false);
	}

	private void InitCollidesWithIcons(CustomShape shape)
	{
		m_CarIcon.SetActive(shape.m_CollidesWithVehicles);
		m_RoadIcon.SetActive(shape.m_CollidesWithRoad);
		m_JointIcon.SetActive(shape.m_CollidesWithNodes);
		m_RampsIcon.SetActive(shape.m_CollidesWithRamps);
		m_SplitJointIcon.SetActive(value: false);
		m_NothingIcon.SetActive(!shape.m_CollidesWithVehicles && !shape.m_CollidesWithRoad && !shape.m_CollidesWithNodes && !shape.m_CollidesWithRamps);
	}

	private void MakeMassValueActive(bool active)
	{
		m_MassLabel.gameObject.SetActive(active);
		m_Mass.gameObject.SetActive(active);
	}

	private void MakeMotorizedValuesActive(bool active)
	{
		m_MotorStrengthLabel.gameObject.SetActive(active);
		m_MotorSpeedLabel.gameObject.SetActive(active);
		m_MotorAccelLabel.gameObject.SetActive(active);
		m_MotorStrength.gameObject.SetActive(active);
		m_MotorSpeed.gameObject.SetActive(active);
		m_MotorAccel.gameObject.SetActive(active);
	}
}
