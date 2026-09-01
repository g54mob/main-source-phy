using UnityEngine;

public class BridgePillarTooltip
{
	private ToolTip m_ToolTip;

	public BridgePillarTooltip()
	{
		CreateToolTip();
	}

	public void Hide()
	{
		if (m_ToolTip != null)
		{
			m_ToolTip.gameObject.SetActive(value: false);
		}
	}

	public void UpdateManual(Vector3 pos, float height, float cost, bool moving)
	{
		if (!m_ToolTip)
		{
			return;
		}
		if (!ShouldShowToolTip(moving))
		{
			Hide();
			return;
		}
		m_ToolTip.gameObject.SetActive(value: true);
		m_ToolTip.Set($"{height:F2}m\n${cost:F0}", null);
		float num = 0f;
		num = Mathf.Clamp(height / 2f, 1.5f, height - 0.5f);
		Vector3 position = pos + new Vector3(0f, num, 0f);
		Vector2 vector = Cameras.MainCamera().WorldToScreenPoint(position);
		if (Utils.PointIsOffscreen(vector))
		{
			m_ToolTip.gameObject.SetActive(value: false);
		}
		else
		{
			GameUI.SetScreenPosClamped(m_ToolTip.gameObject, vector, 0f, 0f);
		}
	}

	public void Destroy()
	{
		if (m_ToolTip != null)
		{
			Object.Destroy(m_ToolTip.gameObject);
		}
	}

	private void CreateToolTip()
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_ToolTip, GameUI.m_Instance.transform);
		if ((bool)gameObject)
		{
			m_ToolTip = gameObject.GetComponent<ToolTip>();
			if ((bool)m_ToolTip)
			{
				m_ToolTip.gameObject.SetActive(value: false);
				m_ToolTip.name = "Foundation ToolTip";
				m_ToolTip.m_RectTransform.pivot = new Vector2(0.5f, 0.5f);
			}
		}
	}

	private bool ShouldShowToolTip(bool moving)
	{
		if (GameStateManager.GetState() != GameState.BUILD || GameStateBuild.m_CameraInTransition)
		{
			return false;
		}
		if (GameUI.SaveLoadPanelIsActive() || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (!GameStateCommonInput.IgnoreKeyboardInput() && GameInput.IsDown(BindingType.SHOW_ALL_TOOLTIPS))
		{
			return true;
		}
		if (moving)
		{
			return true;
		}
		return false;
	}
}
