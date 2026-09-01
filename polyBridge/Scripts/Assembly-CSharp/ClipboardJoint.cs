using System;
using UnityEngine;

public class ClipboardJoint : MonoBehaviour
{
	public GameObject m_AnchorIcon;

	public GameObject m_Icon;

	public SpriteRenderer m_IconRight;

	public GameObject m_BadIcon;

	public GameObject m_MergeIcon;

	public bool m_IsSplit;

	[NonSerialized]
	public BridgeJoint m_SourceBridgeJoint;

	[NonSerialized]
	public BridgeJoint m_PastedBridgeJoint;

	[NonSerialized]
	public BridgeJoint m_MergeBridgeJoint;

	[NonSerialized]
	public bool m_ResetJointSelectorsAfterPaste;

	public void DrawAsSplitJoint()
	{
		m_IconRight.color = new Color(m_IconRight.color.r, m_IconRight.color.g, m_IconRight.color.b, 1f);
	}

	public void DrawAsNonSplitJoint()
	{
		m_IconRight.color = new Color(m_IconRight.color.r, m_IconRight.color.g, m_IconRight.color.b, 0.5f);
	}

	public void SetNormal()
	{
		if (m_SourceBridgeJoint != null)
		{
			SetBaseIcon(m_SourceBridgeJoint);
		}
		if (m_BadIcon != null)
		{
			m_BadIcon.SetActive(value: false);
		}
		if (m_MergeIcon != null)
		{
			m_MergeIcon.SetActive(value: false);
		}
	}

	public void SetBad()
	{
		if (m_SourceBridgeJoint != null)
		{
			SetBaseIcon(m_SourceBridgeJoint);
		}
		if (m_BadIcon != null)
		{
			m_BadIcon.SetActive(value: true);
		}
		if (m_MergeIcon != null)
		{
			m_MergeIcon.SetActive(value: false);
		}
	}

	public void SetMerge(BridgeJoint mergeJoint)
	{
		if (m_SourceBridgeJoint != null)
		{
			SetBaseIcon(m_SourceBridgeJoint);
		}
		if (m_BadIcon != null)
		{
			m_BadIcon.SetActive(value: false);
		}
		if (m_MergeIcon != null)
		{
			m_MergeIcon.SetActive(value: true);
			m_MergeBridgeJoint = mergeJoint;
		}
	}

	public bool IsBad()
	{
		return m_BadIcon.activeSelf;
	}

	public bool WillMerge()
	{
		return m_MergeIcon.activeSelf;
	}

	public ClipboardBridgePillar GetClipboardBridgePillar()
	{
		BridgePillar bridgePillarWithAnchor = BridgePillars.GetBridgePillarWithAnchor(m_SourceBridgeJoint.m_Guid);
		if (bridgePillarWithAnchor == null)
		{
			return null;
		}
		return ClipboardManager.FindClipboardBridgePillarMatchingSource(bridgePillarWithAnchor.m_Guid);
	}

	private void SetBaseIcon(BridgeJoint joint)
	{
		BridgePillar bridgePillarWithAnchor = BridgePillars.GetBridgePillarWithAnchor(joint.m_Guid);
		bool flag = bridgePillarWithAnchor != null && ClipboardManager.ContainsBridgePillarSource(bridgePillarWithAnchor);
		m_Icon.gameObject.SetActive(!flag);
		m_AnchorIcon.gameObject.SetActive(flag);
	}
}
