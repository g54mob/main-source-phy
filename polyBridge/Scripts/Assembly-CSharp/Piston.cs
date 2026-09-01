using System;
using DarkTonic.MasterAudio;
using UnityEngine;

public class Piston : MonoBehaviour
{
	[Header("UI")]
	public PistonSlider m_Slider;

	public SpriteRenderer m_Extender;

	public SpriteRenderer m_ExtenderLimits;

	[Header("Meshes")]
	public GameObject m_PinionA;

	public GameObject m_PinionB;

	private static readonly float TOOLTIP_LINGER_TIME_SECONDS = 0.5f;

	private float m_TooltipLingerUntil;

	[NonSerialized]
	public string m_Guid;

	[NonSerialized]
	public BridgeJoint m_JointA;

	[NonSerialized]
	public BridgeJoint m_JointB;

	[NonSerialized]
	public BridgeEdge m_Edge;

	[NonSerialized]
	public BoxCollider m_EdgeCollider;

	[NonSerialized]
	public ToolTip m_ToolTip;

	[NonSerialized]
	public float m_HoverEdgeActiveUntil;

	private SoundGroupVariation m_ActivatingAudioLoop;

	private void OnDestroy()
	{
		if ((bool)m_ToolTip)
		{
			UnityEngine.Object.Destroy(m_ToolTip.gameObject);
		}
		HydraulicsController.RemovePistonFromAllPhases(this);
		if (Pistons.m_Pistons.Contains(this))
		{
			Pistons.m_Pistons.Remove(this);
		}
		StopLoopingAudio(skipTail: true);
	}

	private void OnDisable()
	{
		if (m_ToolTip != null)
		{
			m_ToolTip.gameObject.SetActive(value: false);
		}
	}

	public void UpdateManual()
	{
		UpdatePinions();
		UpdateSlider();
		UpdateExtenderLimits();
	}

	public void LateUpdate()
	{
		UpdateToolTip();
	}

	public float CalculatePistonAngle()
	{
		Vector3 normalized = (m_JointB.transform.position - m_JointA.transform.position).normalized;
		float num = Vector3.Angle(Vector3.right, normalized);
		if (Vector3.Dot(Vector3.up, normalized) < 0f)
		{
			num *= -1f;
		}
		return num;
	}

	public float GetTargetLengthScale()
	{
		float normalizedValue = m_Slider.GetNormalizedValue();
		if (normalizedValue > 0.5f)
		{
			return Mathf.Lerp(1f, 1.5f, Mathf.Clamp01((normalizedValue - 0.5f) / 0.5f));
		}
		return Mathf.Lerp(0.5f, 1f, Mathf.Clamp01(normalizedValue / 0.5f));
	}

	public int GetTargetLengthPercentage()
	{
		float targetLengthScale = GetTargetLengthScale();
		int num = 100;
		if (targetLengthScale > 1f)
		{
			return Mathf.RoundToInt((targetLengthScale - 1f) * 100f);
		}
		return -Mathf.RoundToInt((1f - targetLengthScale) * 100f);
	}

	public void CreateToolTip()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_ToolTip, GameUI.m_Instance.transform);
		if ((bool)gameObject)
		{
			m_ToolTip = gameObject.GetComponent<ToolTip>();
			if ((bool)m_ToolTip)
			{
				m_ToolTip.gameObject.SetActive(value: false);
				m_ToolTip.name = "Piston ToolTip";
			}
		}
	}

	public void UpdatePinions()
	{
		m_PinionA.transform.position = m_JointA.transform.position;
		m_PinionB.transform.position = m_JointB.transform.position;
	}

	public void PlayLoopingAudio()
	{
		StopLoopingAudio(skipTail: true);
		m_ActivatingAudioLoop = SimAudio.Loop("sfx_simulation_hydraulic_lp", (m_PinionA.transform.position + m_PinionB.transform.position) / 2f, 1f, AudioMixerManager.Pitch);
	}

	public void StopLoopingAudio(bool skipTail = false)
	{
		if (m_ActivatingAudioLoop != null)
		{
			SimAudio.StopLoop(m_ActivatingAudioLoop, skipTail);
			m_ActivatingAudioLoop = null;
		}
	}

	public bool PointerOverExtenderLimits()
	{
		if (!m_ExtenderLimits.gameObject.activeInHierarchy || BridgeSave.m_IsDeserializing)
		{
			return false;
		}
		Ray ray = Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition());
		Piston piston = null;
		if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, Utils.PISTON_LAYER_MASK))
		{
			piston = hitInfo.transform.parent.GetComponent<Piston>();
		}
		return piston == this;
	}

	private void UpdateSlider()
	{
		m_Slider.UpdateManual();
	}

	private void UpdateExtenderLimits()
	{
		bool flag = ExtenderLimitsShouldBeVisible();
		m_ExtenderLimits.gameObject.SetActive(flag);
		if (flag)
		{
			float angle = CalculatePistonAngle();
			Vector3 normalized = (m_JointB.transform.position - m_JointA.transform.position).normalized;
			float num = m_Edge.GetLength() / 2f;
			if (Pistons.m_SliderFollowingMouse == m_Slider)
			{
				Vector3 vector = m_JointB.transform.position + normalized * (num / 2f);
				m_ExtenderLimits.transform.position = new Vector3(vector.x, vector.y, m_ExtenderLimits.transform.position.z);
				m_ExtenderLimits.transform.rotation = Quaternion.identity;
				m_ExtenderLimits.transform.Rotate(Vector3.forward, angle, Space.World);
				m_ExtenderLimits.transform.localScale = new Vector3(num, m_ExtenderLimits.transform.localScale.y, m_ExtenderLimits.transform.localScale.z);
			}
			else
			{
				float num2 = (m_Slider.GetNormalizedValue() - 0.5f) / 0.5f;
				Vector3 vector2 = m_JointB.transform.position + normalized * (num2 * num / 2f);
				m_ExtenderLimits.transform.position = new Vector3(vector2.x, vector2.y, m_ExtenderLimits.transform.position.z);
				m_ExtenderLimits.transform.rotation = Quaternion.identity;
				m_ExtenderLimits.transform.Rotate(Vector3.forward, angle, Space.World);
				m_ExtenderLimits.transform.localScale = new Vector3(num2 * num, m_ExtenderLimits.transform.localScale.y, m_ExtenderLimits.transform.localScale.z);
			}
		}
	}

	private bool ExtenderLimitsShouldBeVisible()
	{
		if (!ShouldDisplayPistionUI())
		{
			return false;
		}
		if ((bool)Pistons.m_SliderFollowingMouse && Pistons.m_SliderFollowingMouse == m_Slider)
		{
			return true;
		}
		if (Pistons.m_SliderFollowingMouse != m_Slider && m_Slider.GetNormalizedValue() < 0.5f)
		{
			return false;
		}
		return m_Slider.GetNormalizedValue() > 0.5f;
	}

	private void UpdateToolTip()
	{
		if (!m_ToolTip)
		{
			return;
		}
		bool flag = Time.unscaledTime < m_TooltipLingerUntil;
		if ((GameStateManager.GetState() != GameState.BUILD || GameStateCommonInput.IgnoreKeyboardInput() || (!flag && !GameInput.IsDown(BindingType.SHOW_ALL_TOOLTIPS)) || m_Edge.IsLocked()) && (!ShouldShowToolTip() || !ShouldDisplayPistionUI()))
		{
			m_ToolTip.gameObject.SetActive(value: false);
			return;
		}
		m_ToolTip.gameObject.SetActive(value: true);
		int targetLengthPercentage = GetTargetLengthPercentage();
		if (targetLengthPercentage == 0)
		{
			m_ToolTip.Set(Localize.Get("PISTON_HOLD_LENGTH"), null);
		}
		else if (targetLengthPercentage > 0)
		{
			string arg = string.Format(Localize.Get("PISTON_EXPAND"), targetLengthPercentage);
			m_ToolTip.Set($"{arg} (+{(float)targetLengthPercentage / 100f * m_Edge.GetLength():F2}m)", null);
		}
		else
		{
			string arg2 = string.Format(Localize.Get("PISTON_CONTRACT"), targetLengthPercentage);
			m_ToolTip.Set($"{arg2} ({(float)targetLengthPercentage / 100f * m_Edge.GetLength():F2}m)", null);
		}
		Vector2 vector = Cameras.MainCamera().WorldToScreenPoint(m_Slider.m_Handle.transform.position);
		if (Utils.PointIsOffscreen(vector))
		{
			m_ToolTip.gameObject.SetActive(value: false);
		}
		else
		{
			GameUI.SetScreenPosClamped(m_ToolTip.gameObject, vector, 20f, (0f - m_ToolTip.m_RectTransform.sizeDelta.y) / 2f);
		}
	}

	private bool ShouldShowToolTip()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameStateManager.GetState() != GameState.BUILD || GameStateBuild.m_CameraInTransition)
		{
			return false;
		}
		if (GameUI.SaveLoadPanelIsActive() || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (!Profiles.m_ActiveProfile.m_DisableBuildHelpTooltips && GameInput.GetActiveGameDevice() != GameDevice.Gamepad && !GameUI.m_Instance.m_PointerToolTip.gameObject.activeInHierarchy && !BridgeSelectionSet.ContainsEdge(m_Edge))
		{
			return false;
		}
		if ((bool)Pistons.m_SliderFollowingMouse && Pistons.m_SliderFollowingMouse.m_Piston == this && Pistons.m_SliderFollowingMouse.m_Handle.gameObject.activeInHierarchy)
		{
			return true;
		}
		if (GameStateBuild.m_HoverPistonSlider == m_Slider || GameStateBuild.m_HoverLockedEdge == m_Edge)
		{
			return true;
		}
		if ((bool)BridgeJointMovement.m_SelectedJoint && IsConnectedToJoint(BridgeJointMovement.m_SelectedJoint))
		{
			return true;
		}
		if (m_Slider.m_Handle.gameObject.activeInHierarchy && Pistons.GetPistonSliderUnderMouse() == this)
		{
			return true;
		}
		if ((GameInput.IsDown(BindingType.NUDGE_HYDRO_UP) || GameInput.IsDown(BindingType.NUDGE_HYDRO_DOWN)) && (BridgeSelectionSet.ContainsEdge(m_Edge) || GameStateBuild.m_HoverEdge == m_Edge))
		{
			m_TooltipLingerUntil = Time.unscaledTime + TOOLTIP_LINGER_TIME_SECONDS;
			return true;
		}
		if (Time.unscaledTime < m_TooltipLingerUntil)
		{
			return true;
		}
		return false;
	}

	private bool IsConnectedToJoint(BridgeJoint joint)
	{
		if (!(m_JointA == joint))
		{
			return m_JointB == joint;
		}
		return true;
	}

	private bool ShouldDisplayPistionUI()
	{
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			return !GameStateBuild.m_CameraInTransition;
		}
		return false;
	}
}
