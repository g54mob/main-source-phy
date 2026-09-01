using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vectrosity;

public class ArcTracer : MonoBehaviour
{
	public GraphicRaycaster m_Raycaster;

	public VectorObject2D arcTracerVectorLine;

	public CurvyUISpline arcTracerSpline;

	public GameObject m_HandleNotVisible;

	public ArcTracerHandle handleA;

	public ArcTracerHandle handleB;

	public bool mirrorHandles = true;

	private bool dragging;

	private ArcShape m_ArcShape;

	private float m_LastShownArcLength = float.MaxValue;

	private VectorLine m_VectorLine;

	private static PointerEventData m_PointerEventData;

	private static List<RaycastResult> m_ArcTracerRaycastResults = new List<RaycastResult>();

	[NonSerialized]
	public readonly float MIN_ARC_DISTANCE = 0.5f;

	private readonly float TRACE_LINE_WIDTH = 3f;

	private void Start()
	{
		m_PointerEventData = new PointerEventData(EventSystem.current);
		arcTracerSpline.gameObject.SetActive(value: false);
		arcTracerVectorLine.enabled = false;
	}

	private void Update()
	{
		UpdateArcLengthInfoBox();
		UpdateVectorLine();
		m_HandleNotVisible.transform.position = Utils.GetWorldPosAtCenterOfScreen();
		ArcTracerHandle arcTracerHandle = PointerOverArcHandle(GameInput.GetMousePosition());
		if (arcTracerHandle != null)
		{
			if (TangentsLocked())
			{
				handleA.UpdateColor(hover: true);
				handleB.UpdateColor(hover: true);
			}
			else
			{
				handleA.UpdateColor(hover: false);
				handleB.UpdateColor(hover: false);
				arcTracerHandle.UpdateColor(hover: true);
			}
		}
		else
		{
			handleA.UpdateColor(hover: false);
			handleB.UpdateColor(hover: false);
		}
	}

	public void Clear()
	{
		arcTracerSpline.gameObject.SetActive(value: false);
		arcTracerVectorLine.enabled = false;
		if (m_VectorLine != null)
		{
			m_VectorLine.active = false;
		}
	}

	public void StartDrawingFrom(Vector2 p)
	{
		mirrorHandles = true;
		Vector3 position = new Vector3(p.x, p.y, -20f);
		handleA.transform.position = position;
		handleA.SetHandleVisible(value: false);
		handleB.SetHandleVisible(value: false);
	}

	public void RepositionHandles()
	{
		Vector2 normalized = (handleB.GetHandlePositionWorld() - handleA.GetHandlePositionWorld()).normalized;
		handleA.MoveHandleBy(-normalized * 0.5f);
		handleB.MoveHandleBy(normalized * 0.5f);
	}

	public void ShowHandles()
	{
		handleA.SetHandleVisible(value: true);
		handleB.SetHandleVisible(value: true);
	}

	public void HideHandles()
	{
		handleA.SetHandleVisible(value: false);
		handleB.SetHandleVisible(value: false);
	}

	public ArcTracerHandle PointerOverArcHandle(Vector2 pointerScreenPos)
	{
		m_PointerEventData.position = pointerScreenPos;
		m_ArcTracerRaycastResults.Clear();
		BridgeTrace.m_ArcTracer.m_Raycaster.Raycast(m_PointerEventData, m_ArcTracerRaycastResults);
		foreach (RaycastResult arcTracerRaycastResult in m_ArcTracerRaycastResults)
		{
			ArcTracerHandle componentInParent = arcTracerRaycastResult.gameObject.GetComponentInParent<ArcTracerHandle>();
			if (componentInParent != null)
			{
				return componentInParent;
			}
		}
		return null;
	}

	public bool HandlesVisible()
	{
		return handleA.IsEnabled();
	}

	public bool IsTracerVisible()
	{
		return arcTracerSpline.gameObject.activeSelf;
	}

	public bool ActiveBetweenPoints(Vector2 a, Vector2 b)
	{
		if (Utils.ApproximatelyEquals(Utils.V3toV2(handleA.transform.position), a) && Utils.ApproximatelyEquals(Utils.V3toV2(handleB.transform.position), b))
		{
			return true;
		}
		if (Utils.ApproximatelyEquals(Utils.V3toV2(handleB.transform.position), a) && Utils.ApproximatelyEquals(Utils.V3toV2(handleA.transform.position), b))
		{
			return true;
		}
		return false;
	}

	public void ResetDraggingHandles()
	{
		dragging = false;
	}

	public bool IsDraggingHandles()
	{
		if (dragging && HandlesVisible())
		{
			return Input.GetMouseButton(0);
		}
		return false;
	}

	public float GetArcDistance()
	{
		return Vector2.Distance(handleA.transform.position, handleB.transform.position);
	}

	public bool Finish()
	{
		float arcDistance = GetArcDistance();
		if (arcDistance < MIN_ARC_DISTANCE)
		{
			Clear();
			return false;
		}
		handleA.SetABDistance(arcDistance);
		handleB.SetABDistance(arcDistance);
		TraceNodes();
		return true;
	}

	public Vector3 ClosestPointOnLineTo(Vector3 pos, Vector3 restrictOrigin, float restrictRadius)
	{
		Vector3 localPosition = arcTracerSpline.transform.InverseTransformPoint(pos);
		float nearestPointTF = arcTracerSpline.GetNearestPointTF(localPosition);
		Vector3 vector = arcTracerSpline.transform.TransformPoint(arcTracerSpline.Interpolate(nearestPointTF));
		float num = Vector2.Distance(vector, restrictOrigin);
		if (num < restrictRadius || Mathf.Approximately(num, restrictRadius))
		{
			return vector;
		}
		float num2 = 0.001f;
		float num3 = nearestPointTF;
		for (num3 -= num2; num3 >= 0f; num3 -= num2)
		{
			vector = arcTracerSpline.transform.TransformPoint(arcTracerSpline.Interpolate(Mathf.Clamp01(num3)));
			float num4 = Vector2.Distance(vector, restrictOrigin);
			if (num4 > num)
			{
				break;
			}
			if (num4 < restrictRadius || Mathf.Approximately(num4, restrictRadius))
			{
				return vector;
			}
		}
		float num5 = nearestPointTF;
		for (num5 += num2; num5 <= 1f; num5 += num2)
		{
			vector = arcTracerSpline.transform.TransformPoint(arcTracerSpline.Interpolate(Mathf.Clamp01(num5)));
			float num6 = Vector2.Distance(vector, restrictOrigin);
			if (num6 > num)
			{
				break;
			}
			if (num6 < restrictRadius || Mathf.Approximately(num6, restrictRadius))
			{
				return vector;
			}
		}
		return vector;
	}

	private bool PosWithinRadiusOfOrigin(Vector3 pos, Vector3 origin, float radius)
	{
		float num = Vector2.Distance(pos, origin);
		if (!(num < radius))
		{
			return Mathf.Approximately(num, radius);
		}
		return true;
	}

	public void ContinueDrawingTo(Vector2 p)
	{
		CalculateHandlePositions();
		Vector3 position = new Vector3(p.x, p.y, -20f);
		handleB.transform.position = position;
		Vector2 vector = ComputeNormal();
		Vector2 pos = Vector2.Reflect(handleA.GetHandlePosition(), vector.normalized);
		Vector2 pos2 = Vector2.Reflect(handleB.GetHandlePosition(), vector.normalized);
		handleA.MoveHandleTo(pos, worldPos: false);
		handleB.MoveHandleTo(pos2, worldPos: false);
		float aBDistance = Vector2.Distance(handleA.transform.position, handleB.transform.position);
		handleA.SetABDistance(aBDistance);
		handleB.SetABDistance(aBDistance);
		arcTracerSpline.gameObject.SetActive(value: true);
		m_VectorLine.active = true;
		arcTracerSpline.Refresh();
		UpdateArcTracerLine();
	}

	private bool IsStraightLine()
	{
		if (handleA.GetHandlePosition() == Vector2.zero)
		{
			return handleB.GetHandlePosition() == Vector2.zero;
		}
		return false;
	}

	private void CalculateHandlePositions()
	{
		float defaultSag = GetDefaultSag();
		float defaultTension = GetDefaultTension();
		Vector2 vector = (handleB.transform.position - handleA.transform.position) * 0.5f + handleA.transform.position;
		Vector2 vector2 = handleB.transform.position - handleA.transform.position;
		int num = 90;
		if (vector2.x > 0f)
		{
			num = -90;
		}
		Vector2 vector3 = Utils.V3toV2(Quaternion.Euler(0f, 0f, num) * vector2) * defaultSag + vector;
		Vector2 pos = (vector3 - Utils.V3toV2(handleA.transform.position)) * defaultTension;
		Vector2 pos2 = (vector3 - Utils.V3toV2(handleB.transform.position)) * defaultTension;
		handleA.MoveHandleTo(pos, worldPos: false);
		handleB.MoveHandleTo(pos2, worldPos: false);
	}

	private void TraceNodes()
	{
		arcTracerSpline.Refresh();
		UpdateArcTracerLine();
		handleA.SetHandleVisible(value: true);
		handleB.SetHandleVisible(value: true);
	}

	public void SetShape(ArcShape shape)
	{
		m_ArcShape = shape;
		CalculateHandlePositions();
		if (shape == ArcShape.CURVED)
		{
			Flip();
		}
		TraceNodes();
		if (shape == ArcShape.FLAT)
		{
			RepositionHandles();
		}
	}

	public void SetShapeSilent(ArcShape shape)
	{
		m_ArcShape = shape;
	}

	public Vector2 ComputeNormal()
	{
		Vector2 vector = handleB.transform.position - handleA.transform.position;
		return Quaternion.Euler(0f, 0f, 90f) * vector.normalized;
	}

	private float GetDefaultSag()
	{
		return m_ArcShape switch
		{
			ArcShape.FLAT => 0f, 
			ArcShape.CURVED => 0.4f, 
			_ => 0.3f, 
		};
	}

	private float GetDefaultTension()
	{
		return m_ArcShape switch
		{
			ArcShape.FLAT => 0f, 
			ArcShape.CURVED => 0.6f, 
			_ => 0.5f, 
		};
	}

	public void UpdateArcTracerLine()
	{
		arcTracerSpline.Refresh();
		int num = Mathf.RoundToInt(arcTracerSpline.Length) * 8;
		if (num < 2)
		{
			num = 2;
		}
		float num2 = 1f / ((float)num - 1f);
		m_VectorLine.Resize(num);
		for (int i = 0; i < num; i++)
		{
			m_VectorLine.points3[i] = arcTracerSpline.Interpolate((float)i * num2);
		}
	}

	public void Flip()
	{
		Vector2 vector = ComputeNormal();
		Vector2 pos = Vector2.Reflect(handleA.GetHandlePosition(), vector.normalized);
		Vector2 pos2 = Vector2.Reflect(handleB.GetHandlePosition(), vector.normalized);
		handleA.MoveHandleTo(pos, worldPos: false);
		handleB.MoveHandleTo(pos2, worldPos: false);
		UpdateArcTracerLine();
	}

	public void HandleBeingMovedBy(Vector2 delta, ArcTracerHandle handle)
	{
		if (mirrorHandles)
		{
			if (handle == handleA)
			{
				Vector2 vector = handleB.GetHandlePositionWorld() - handleA.GetHandlePositionWorld();
				delta = handleA.MoveHandleBy(delta);
				Vector2 delta2 = Vector2.Reflect(delta, vector.normalized);
				handleB.MoveHandleBy(delta2);
			}
			else
			{
				Vector2 vector2 = handleB.GetHandlePositionWorld() - handleA.GetHandlePositionWorld();
				delta = handleB.MoveHandleBy(delta);
				Vector2 delta3 = Vector2.Reflect(delta, vector2.normalized);
				handleA.MoveHandleBy(delta3);
			}
		}
		else
		{
			handle.MoveHandleBy(delta);
		}
		UpdateArcTracerLine();
	}

	public void BeginDragHandle(BaseEventData data)
	{
		if (!data.used)
		{
			data.Use();
			dragging = true;
		}
	}

	public void EndDragHandle(BaseEventData data)
	{
		if (!data.used)
		{
			data.Use();
			dragging = false;
		}
	}

	public void LockTangents()
	{
		mirrorHandles = true;
	}

	public void UnLockTangents()
	{
		mirrorHandles = false;
	}

	public bool TangentsLocked()
	{
		return mirrorHandles;
	}

	public void Hide(bool hide)
	{
		base.gameObject.SetActive(!hide);
		m_VectorLine.layer = (hide ? Utils.NO_RENDER_LAYER : Utils.RENDER_LAST_LAYER);
	}

	public bool IsVectorLineActive()
	{
		return m_VectorLine.active;
	}

	public void OnLayoutLoaded()
	{
		if (m_VectorLine != null)
		{
			VectorLine.Destroy(ref m_VectorLine);
		}
		m_VectorLine = CreateTraceVectorLine();
	}

	public void SetArcLengthInfoBoxVisibility(bool value)
	{
		if (value)
		{
			GameUI.m_Instance.m_TraceLineToolTip.ForceEnable();
		}
		else
		{
			GameUI.m_Instance.m_TraceLineToolTip.Disable();
		}
	}

	private void UpdateArcLengthInfoBox()
	{
		if (GameUI.m_Instance.m_TraceLineToolTip.gameObject.activeInHierarchy)
		{
			float length = arcTracerSpline.Length;
			if (!Mathf.Approximately(m_LastShownArcLength, length))
			{
				GameUI.m_Instance.m_TraceLineToolTip.Set(Utils.FormatDistance(length), null);
				m_LastShownArcLength = length;
			}
		}
		Vector3 position = arcTracerSpline.Interpolate(0.5f);
		Vector2 screenPos = Cameras.MainCamera().WorldToScreenPoint(position);
		GameUI.SetScreenPos(GameUI.m_Instance.m_TraceLineToolTip.gameObject, screenPos, 0f, 0f);
		Vector2 vector = ComputeNormal();
		GameUI.m_Instance.m_TraceLineToolTip.m_RectTransform.anchoredPosition += vector * 20f;
	}

	private void UpdateVectorLine()
	{
		if (m_VectorLine != null)
		{
			Outlines.UpdateWidthForOrthographicChange(m_VectorLine, TRACE_LINE_WIDTH);
		}
	}

	private VectorLine CreateTraceVectorLine()
	{
		VectorLine vectorLine = new VectorLine("ArcLine", new List<Vector3>(), null, TRACE_LINE_WIDTH, LineType.Continuous, Joins.Weld);
		vectorLine.rectTransform.gameObject.hideFlags = HideFlags.HideInHierarchy;
		vectorLine.Draw3DAuto();
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.layer = Utils.RENDER_LAST_LAYER;
		vectorLine.textureScale = 1f;
		vectorLine.color = new Color(4f / 51f, 4f / 51f, 4f / 51f, 26f / 51f);
		vectorLine.active = false;
		vectorLine.AddNormals();
		return vectorLine;
	}
}
