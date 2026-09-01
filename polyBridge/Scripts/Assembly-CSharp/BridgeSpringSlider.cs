using UnityEngine;

public class BridgeSpringSlider : MonoBehaviour
{
	[Header("Parent")]
	public BridgeSpring m_BridgeSpring;

	[Header("UI")]
	public SpriteRenderer m_Handle;

	private float m_NormalizedValue;

	private readonly float SLIDER_VISIBLE_LINGER_SECONDS;

	private float m_VisibilityExpiresTime;

	public void Awake()
	{
		m_Handle.gameObject.SetActive(value: false);
	}

	public void UpdateManual()
	{
		UpdateVisibility();
		UpdatePosition();
		UpdateRotation();
	}

	public void SetNormalizedValue(float value)
	{
		m_NormalizedValue = value;
		UpdatePosition();
	}

	public float GetNormalizedValue()
	{
		return m_NormalizedValue;
	}

	public void SetVisibilityExpireTime()
	{
		m_VisibilityExpiresTime = Time.unscaledTime + SLIDER_VISIBLE_LINGER_SECONDS;
	}

	public Vector3 GetAdjustedStartPos(Vector3 dir, float len)
	{
		float num = ((len > 0.75001f) ? GameSettings.NodeDiameter() : 0f);
		return m_BridgeSpring.m_ParentEdge.m_JointA.transform.position + dir * num;
	}

	public float GetAdjustedLen(float len)
	{
		if (len > 0.75001f)
		{
			return Mathf.Max(GameSettings.NodeRadius() * 2f, len - 4f * GameSettings.NodeRadius());
		}
		return len;
	}

	private void UpdateVisibility()
	{
		bool flag = BridgeSprings.m_SliderFollowingMouse == this || GameStateBuild.m_HoverBridgeSpringSlider == this || (GameStateBuild.m_HoverEdge == m_BridgeSpring.m_ParentEdge && GameStateBuild.m_HoverEdgeSeconds > GameStateBuild.HOVER_EDGE_DELAY_SECONDS);
		if (flag)
		{
			SetVisibilityExpireTime();
		}
		if (BridgeSprings.m_SliderFollowingMouse != null && BridgeSprings.m_SliderFollowingMouse != this)
		{
			flag = false;
		}
		if (BridgeJointMovement.m_SelectedJoint != null || GameToolMode.GetMode() == GameToolModeType.MOVE)
		{
			flag = false;
		}
		if (Time.unscaledTime < m_VisibilityExpiresTime)
		{
			flag = true;
		}
		if (BridgeJointPlacement.InPlacementMode() || (bool)BridgeJointMovement.m_SelectedJoint)
		{
			flag = false;
		}
		if (!GameStateCommonInput.IgnoreKeyboardInput() && (GameInput.IsDown(BindingType.SHOW_ALL_TOOLTIPS) || GameInput.IsDown(BindingType.SHOW_ALL_SPLIT_JOINT_NUMBERS)))
		{
			flag = true;
		}
		if (GameStateManager.GetState() != GameState.BUILD || GameStateBuild.m_CameraInTransition)
		{
			flag = false;
		}
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			flag = false;
		}
		if (ActivePanels.m_Panels.Count > 0)
		{
			flag = false;
		}
		if (m_BridgeSpring.m_ParentEdge.IsLocked() || !SandboxSettings.m_SpringAdjustmentsAllowed)
		{
			flag = false;
		}
		m_Handle.gameObject.SetActive(flag);
	}

	private void UpdatePosition()
	{
		Vector3 normalized = (m_BridgeSpring.m_ParentEdge.m_JointB.transform.position - m_BridgeSpring.m_ParentEdge.m_JointA.transform.position).normalized;
		float length = m_BridgeSpring.m_ParentEdge.GetLength();
		Vector3 adjustedStartPos = GetAdjustedStartPos(normalized, length);
		float adjustedLen = GetAdjustedLen(length);
		base.transform.position = adjustedStartPos + normalized * adjustedLen * m_NormalizedValue;
	}

	private void UpdateRotation()
	{
		float angle = m_BridgeSpring.m_ParentEdge.CalculateAngle();
		base.transform.rotation = Quaternion.identity;
		base.transform.Rotate(Vector3.forward, angle, Space.World);
	}
}
