using UnityEngine;

public class ClipboardJointProxy
{
	public string m_Guid;

	public bool m_IsSplit;

	public bool m_m_ResetJointSelectorsAfterPaste;

	public Vector3 m_LocalPos;

	public ClipboardJointProxy(ClipboardJoint joint)
	{
		m_Guid = ((joint.m_SourceBridgeJoint != null) ? joint.m_SourceBridgeJoint.m_Guid : string.Empty);
		m_IsSplit = joint.m_IsSplit;
		m_m_ResetJointSelectorsAfterPaste = joint.m_ResetJointSelectorsAfterPaste;
		m_LocalPos = joint.transform.localPosition;
	}
}
