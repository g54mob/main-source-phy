using System;
using UnityEngine;

public class BridgeJointSelector : MonoBehaviour
{
	public SpriteRenderer m_Num1;

	public SpriteRenderer m_Num2;

	public SpriteRenderer m_Num3;

	public SpriteRenderer m_Lock;

	public SpriteRenderer m_Highlight;

	[NonSerialized]
	public BridgeEdge m_Edge;

	[NonSerialized]
	public BridgeJointSelectorSide m_Side;

	[NonSerialized]
	public bool m_OverlapResolved;

	private readonly int TOP_SORT_ORDER = 6;

	private readonly int DEFAULT_SORT_ORDER = 5;

	private void OnDestroy()
	{
		if (BridgeJointSelectors.m_Selectors.Contains(this))
		{
			BridgeJointSelectors.m_Selectors.Remove(this);
		}
	}

	public void RefreshVisibility()
	{
		bool activeInHierarchy = base.gameObject.activeInHierarchy;
		bool flag = IsVisible();
		base.gameObject.SetActive(flag);
		if (!activeInHierarchy && flag)
		{
			ResolveOverlap();
			m_OverlapResolved = true;
		}
	}

	public void RefreshNumber()
	{
		SplitJointPart splitJointPart = ((m_Side == BridgeJointSelectorSide.A) ? m_Edge.m_JointAPart : m_Edge.m_JointBPart);
		m_Num1.gameObject.SetActive(splitJointPart == SplitJointPart.A);
		m_Num2.gameObject.SetActive(splitJointPart == SplitJointPart.B);
		m_Num3.gameObject.SetActive(splitJointPart == SplitJointPart.C);
		m_Lock.gameObject.SetActive(value: false);
		if (GetAssociatedJoint().m_IsAnchor && m_Num1.gameObject.activeSelf)
		{
			m_Lock.gameObject.SetActive(value: true);
			m_Num1.gameObject.SetActive(value: false);
			m_Num2.gameObject.SetActive(value: false);
			m_Num3.gameObject.SetActive(value: false);
		}
	}

	public void UpdateHighlightState()
	{
		BridgeJoint bridgeJointWithFocus = GetBridgeJointWithFocus();
		if (!bridgeJointWithFocus || !bridgeJointWithFocus.m_IsSplit)
		{
			HighlightOff();
			UnDuck();
		}
		else if (GetAssociatedJoint() == bridgeJointWithFocus)
		{
			HighlightOn();
			UnDuck();
		}
		else
		{
			HighlightOff();
			Duck();
		}
	}

	public void UpdateTransform()
	{
		BridgeJoint associatedJoint = GetAssociatedJoint();
		base.transform.position = CalculatePosition(associatedJoint);
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = CalculateLocalScale();
	}

	public BridgeJoint GetAssociatedJoint()
	{
		if (m_Side != BridgeJointSelectorSide.A)
		{
			return m_Edge.m_JointB;
		}
		return m_Edge.m_JointA;
	}

	public bool IsVisible()
	{
		bool flag = GameStateManager.GetState() == GameState.BUILD && !GameStateBuild.m_CameraInTransition && Cameras.GetOrthographicSize() < GameSettings.MaxOrthographicSizeToShowSplitJointNumbers();
		if (BridgeJointSelectors.m_DebugSplitJointHoverTest)
		{
			flag = base.gameObject.activeInHierarchy;
		}
		BridgeJoint associatedJoint = GetAssociatedJoint();
		if (flag && (bool)associatedJoint && associatedJoint.gameObject.activeInHierarchy)
		{
			return associatedJoint.m_IsSplit;
		}
		return false;
	}

	public Vector3 CalculatePosition(BridgeJoint joint)
	{
		Vector3 vector = CalculateDirection(joint);
		float num = 0.5f;
		float length = m_Edge.GetLength();
		float num2 = length / 2f;
		float num3 = base.transform.localScale.x / 4f;
		if (m_Edge.GetNumActiveJointSelectors() == 1)
		{
			if (length - GameSettings.NodeDiameter() < 0.5f + num3)
			{
				num = num2;
			}
		}
		else if (length < 1.3f)
		{
			num = num2 - num3;
		}
		Vector3 vector2 = joint.transform.position + vector * num;
		return new Vector3(vector2.x, vector2.y, -2f);
	}

	public void ResolveOverlap()
	{
		if (!BridgeJointSelectors.SelectorOverlapsOtherSelectors(this, base.transform.localScale.x / 2f))
		{
			return;
		}
		float num = base.transform.localScale.x / 2f;
		float length = m_Edge.GetLength();
		float num2 = length / 2f;
		if (!(num > length - GameSettings.NodeDiameter()))
		{
			BridgeJoint associatedJoint = GetAssociatedJoint();
			float num3 = Vector2.Distance(associatedJoint.transform.position, base.transform.position);
			float num4;
			do
			{
				num4 = ((m_Edge.GetNumActiveJointSelectors() == 1) ? (length - 0.05f) : (num2 - 0.05f));
				num3 = Mathf.Clamp(num3 + 0.01f, 0f, num4);
				Vector3 vector = associatedJoint.transform.position + CalculateDirection(associatedJoint) * num3;
				base.transform.position = new Vector3(vector.x, vector.y, base.transform.position.z);
			}
			while (BridgeJointSelectors.SelectorOverlapsOtherSelectors(this, num) && !Mathf.Approximately(num3, num4));
		}
	}

	public Vector3 CalculateLocalScale()
	{
		if (!m_Edge.IsRoad())
		{
			return BridgeJointSelectors.SCALE_NONROAD;
		}
		return BridgeJointSelectors.SCALE_ROAD;
	}

	public void Cycle(bool forward)
	{
		BridgeActions.StartRecording();
		SplitJointPart prevSplitJointPart = ((m_Side == BridgeJointSelectorSide.A) ? m_Edge.m_JointAPart : m_Edge.m_JointBPart);
		if (forward)
		{
			CycleToNext();
		}
		else
		{
			CycleToPrev();
		}
		SplitJointPart splitJointPart = ((m_Side == BridgeJointSelectorSide.A) ? m_Edge.m_JointAPart : m_Edge.m_JointBPart);
		BridgeActions.CycleSplitJointSelector(m_Edge, prevSplitJointPart, splitJointPart, m_Side);
		BridgeActions.FlushRecording();
	}

	public void CycleToNext()
	{
		if (m_Side == BridgeJointSelectorSide.A)
		{
			m_Edge.m_JointAPart = GetNextSplitJointPart(m_Edge.m_JointAPart);
		}
		else
		{
			m_Edge.m_JointBPart = GetNextSplitJointPart(m_Edge.m_JointBPart);
		}
		RefreshNumber();
		PlaySplitJointSelectorChangedAudio();
	}

	public void CycleToPrev()
	{
		if (m_Side == BridgeJointSelectorSide.A)
		{
			m_Edge.m_JointAPart = GetPrevSplitJointPart(m_Edge.m_JointAPart);
		}
		else
		{
			m_Edge.m_JointBPart = GetPrevSplitJointPart(m_Edge.m_JointBPart);
		}
		RefreshNumber();
		PlaySplitJointSelectorChangedAudio();
	}

	public bool Ducked()
	{
		return m_Num1.color.a < 1f;
	}

	public void DrawOnTop()
	{
		foreach (BridgeJointSelector selector in BridgeJointSelectors.m_Selectors)
		{
			selector.SetSortOrder((selector == this) ? TOP_SORT_ORDER : DEFAULT_SORT_ORDER);
		}
	}

	public void SetSortOrder(int sortOrder)
	{
		m_Num1.sortingOrder = sortOrder;
		m_Num2.sortingOrder = sortOrder;
		m_Num3.sortingOrder = sortOrder;
	}

	private SplitJointPart GetNextSplitJointPart(SplitJointPart current)
	{
		switch (current)
		{
		case SplitJointPart.A:
			return SplitJointPart.B;
		case SplitJointPart.B:
			if (!SandboxSettings.m_HydraulicControllerEnabled || !SandboxSettings.m_ThreeWaySplitJointsEnabled)
			{
				return SplitJointPart.A;
			}
			return SplitJointPart.C;
		case SplitJointPart.C:
			return SplitJointPart.A;
		default:
			Debug.LogWarningFormat("Unexpected SplitJointPart");
			return current;
		}
	}

	private SplitJointPart GetPrevSplitJointPart(SplitJointPart current)
	{
		switch (current)
		{
		case SplitJointPart.A:
			if (!SandboxSettings.m_HydraulicControllerEnabled || !SandboxSettings.m_ThreeWaySplitJointsEnabled)
			{
				return SplitJointPart.B;
			}
			return SplitJointPart.C;
		case SplitJointPart.B:
			return SplitJointPart.A;
		case SplitJointPart.C:
			return SplitJointPart.B;
		default:
			Debug.LogWarningFormat("Unexpected SplitJointPart");
			return current;
		}
	}

	private Vector3 CalculateDirection(BridgeJoint joint)
	{
		return (((m_Side == BridgeJointSelectorSide.A) ? m_Edge.m_JointB : m_Edge.m_JointA).transform.position - joint.transform.position).normalized;
	}

	private void HighlightOn()
	{
		m_Highlight.gameObject.SetActive(value: true);
	}

	private void HighlightOff()
	{
		m_Highlight.gameObject.SetActive(value: false);
	}

	private void Duck()
	{
		SetIconsAlpha(0.25f);
	}

	private void UnDuck()
	{
		SetIconsAlpha(1f);
	}

	private void SetIconsAlpha(float alpha)
	{
		m_Num1.color = new Color(m_Num1.color.r, m_Num1.color.g, m_Num1.color.b, alpha);
		m_Num2.color = new Color(m_Num2.color.r, m_Num2.color.g, m_Num2.color.b, alpha);
		m_Num3.color = new Color(m_Num3.color.r, m_Num3.color.g, m_Num3.color.b, alpha);
		m_Lock.color = new Color(m_Lock.color.r, m_Lock.color.g, m_Lock.color.b, alpha);
	}

	private BridgeJoint GetBridgeJointWithFocus()
	{
		if ((bool)BridgeJointPlacement.m_HoverJoint)
		{
			return BridgeJointPlacement.m_HoverJoint;
		}
		if ((bool)BridgeJointPlacement.m_SelectedJoint)
		{
			return BridgeJointPlacement.m_SelectedJoint;
		}
		if ((bool)BridgeJointMovement.m_SelectedJoint)
		{
			return BridgeJointMovement.m_SelectedJoint;
		}
		return null;
	}

	private void PlaySplitJointSelectorChangedAudio()
	{
		if (m_Lock.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_build_splitJoint_change_locked");
		}
		else
		{
			InterfaceAudio.Play("ui_build_splitJoint_change");
		}
	}
}
