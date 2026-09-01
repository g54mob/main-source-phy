using TMPro;
using UnityEngine;

public class SandboxItemLabel : MonoBehaviour
{
	public SpriteRenderer m_Background;

	public SpriteRenderer m_BackgroundOutline;

	public TextMeshPro m_Text;

	public SpriteRenderer m_InvisibleIcon;

	public BoxCollider m_BackgroundBoxCollider;

	public void Awake()
	{
		Utils.SetLayerRecursively(base.gameObject, Utils.BUILD_ZONE_LAYER);
	}

	public void UpdateManual(string text)
	{
		if (GameStateManager.GetPrevState() == GameState.SIM && (GameStateBuild.m_CameraInTransition || GameStateSandbox.m_CameraInTransition))
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		if (string.IsNullOrEmpty(text))
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		base.gameObject.SetActive(value: true);
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, SandboxItems.DEFAULT_FLOATING_TEXT_Z);
		base.transform.rotation = Quaternion.identity;
		m_Background.gameObject.SetActive(GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SANDBOX);
		if (m_BackgroundOutline != null)
		{
			m_BackgroundOutline.gameObject.SetActive(GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SANDBOX);
		}
		m_InvisibleIcon.gameObject.SetActive(ShowInvisibleIcon());
		if (text != m_Text.text)
		{
			m_Text.text = text;
			m_Text.ForceMeshUpdate();
			m_Background.size = new Vector2(m_Text.preferredWidth + 0.4f, m_Background.size.y);
			if (m_BackgroundOutline != null)
			{
				m_BackgroundOutline.size = m_Background.size;
			}
			if (m_BackgroundBoxCollider != null)
			{
				m_BackgroundBoxCollider.size = m_Background.size;
			}
		}
		if (base.gameObject.layer != Utils.BUILD_ZONE_LAYER)
		{
			Utils.SetLayerRecursively(base.gameObject, Utils.BUILD_ZONE_LAYER);
		}
	}

	private bool ShowInvisibleIcon()
	{
		VehicleStopTrigger component = base.transform.parent.GetComponent<VehicleStopTrigger>();
		if ((bool)component && component.m_InvisibleInSim)
		{
			return true;
		}
		Checkpoint component2 = base.transform.parent.GetComponent<Checkpoint>();
		if ((bool)component2 && component2.m_InvisibleInSim)
		{
			return true;
		}
		return false;
	}
}
