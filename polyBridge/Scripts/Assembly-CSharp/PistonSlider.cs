using UnityEngine;

public class PistonSlider : MonoBehaviour
{
	[Header("Parent")]
	public Piston m_Piston;

	[Header("UI")]
	public SpriteRenderer m_Handle;

	private float m_NormalizedValue;

	private readonly float SLIDER_VISIBLE_LINGER_SECONDS;

	private float m_VisibilityExpiresTime;

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

	public void SetVisibilityExpireTime()
	{
		m_VisibilityExpiresTime = Time.unscaledTime + SLIDER_VISIBLE_LINGER_SECONDS;
	}

	public float GetNormalizedValue()
	{
		return m_NormalizedValue;
	}

	private void UpdateVisibility()
	{
		bool flag = Pistons.m_SliderFollowingMouse == this || GameStateBuild.m_HoverPistonSlider == this || (GameStateBuild.m_HoverEdge == m_Piston.m_Edge && GameStateBuild.m_HoverEdgeSeconds > 0.1f) || m_Piston.PointerOverExtenderLimits();
		if (flag)
		{
			SetVisibilityExpireTime();
		}
		if (Pistons.m_SliderFollowingMouse != null && Pistons.m_SliderFollowingMouse != this)
		{
			flag = false;
		}
		if (BridgeJointMovement.m_SelectedJoint != null || GameToolMode.GetMode() == GameToolModeType.MOVE)
		{
			flag = false;
		}
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			flag = false;
		}
		if (Time.unscaledTime < m_VisibilityExpiresTime)
		{
			flag = true;
		}
		if (CampaignTutorial.ForceShowPistonSlider(this))
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
		if (ActivePanels.m_Panels.Count > 0)
		{
			flag = false;
		}
		if (GameStateManager.GetState() != GameState.BUILD || GameStateBuild.m_CameraInTransition)
		{
			flag = false;
		}
		if (m_Piston.m_Edge.IsLocked())
		{
			flag = false;
		}
		m_Handle.gameObject.SetActive(flag);
	}

	private bool ShouldBeHidden()
	{
		CampaignTutorial.ForceShowPistonSlider(this);
		return false;
	}

	private void UpdatePosition()
	{
		Vector3 normalized = (m_Piston.m_JointB.transform.position - m_Piston.m_JointA.transform.position).normalized;
		float length = m_Piston.m_Edge.GetLength();
		Vector3 vector = (m_Piston.m_JointA.transform.position + m_Piston.m_JointB.transform.position) / 2f;
		base.transform.position = vector + normalized * (length * m_NormalizedValue);
	}

	private void UpdateRotation()
	{
		float angle = m_Piston.CalculatePistonAngle();
		base.transform.rotation = Quaternion.identity;
		base.transform.Rotate(Vector3.forward, angle, Space.World);
	}
}
