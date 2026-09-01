using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using Poly.Physics;
using UnityEngine;

public class Ramp : MonoBehaviour
{
	public SplineComputer m_SplineComputer;

	public GameObject m_PolePrefab;

	public Transform m_PlanksParent;

	public Transform m_PolesParent;

	public GameObject[] m_PlankVariantPrefabs;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public float m_Height;

	[NonSerialized]
	public bool m_FlippedVertical;

	[NonSerialized]
	public bool m_FlippedHorizontal;

	[NonSerialized]
	public bool m_FlippedLegs;

	[NonSerialized]
	public bool m_HideLegs;

	[NonSerialized]
	public int m_NumSegments;

	[NonSerialized]
	public Dreamteck.Splines.Spline.Type m_SplineType;

	[NonSerialized]
	public List<SplineControlPoint> m_ControlPoints = new List<SplineControlPoint>();

	[NonSerialized]
	public List<MeshRenderer> m_Planks = new List<MeshRenderer>();

	[NonSerialized]
	public List<MeshRenderer> m_Poles = new List<MeshRenderer>();

	[NonSerialized]
	public bool m_UpdateSplineNextFrame;

	[NonSerialized]
	public Bounds m_Bounds;

	[NonSerialized]
	public BoxCollider m_BoxCollider;

	private Outline m_RampOutline;

	private bool m_HasCreatedOutline;

	private List<SplineResult> m_EvaulatedPoints = new List<SplineResult>();

	private List<Vector3> m_OutlinePoints = new List<Vector3>();

	private List<Vector2> m_LinePoints = new List<Vector2>();

	private Outline m_LeftStiltOutline;

	private Outline m_RightStiltOutline;

	private List<Outline> m_MiddleStiltOutlines = new List<Outline>();

	private readonly float PLANK_WIDTH = 0.56f;

	private readonly float PLANK_YSCALE = 2.1f;

	private readonly float PLANK_ZSCALE = 1.2f;

	private readonly float POLE_SCALE_X = 0.7f;

	private readonly float POLE_WIDTH = 0.25f;

	private List<Vector3> m_TempPointsBuffer = new List<Vector3>();

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private bool m_isRenderingEnabled = true;

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
		m_BoxCollider = m_SandboxItem.m_Colliders[0].GetComponent<BoxCollider>();
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
	}

	private void Update()
	{
		if (m_UpdateSplineNextFrame)
		{
			m_SandboxItem.SetOutlineDirty(dirty: true);
			m_UpdateSplineNextFrame = false;
		}
	}

	private void OnDestroy()
	{
		if (Ramps.m_Ramps.Contains(this))
		{
			Ramps.m_Ramps.Remove(this);
		}
		foreach (MeshRenderer plank in m_Planks)
		{
			UnityEngine.Object.Destroy(plank.gameObject);
		}
		foreach (MeshRenderer pole in m_Poles)
		{
			UnityEngine.Object.Destroy(pole.gameObject);
		}
		m_Planks.Clear();
		m_Poles.Clear();
	}

	public void RefreshMesh()
	{
		m_SplineComputer.type = m_SplineType;
		SetNumSegments(m_NumSegments);
		SyncControlPoints();
		m_SplineComputer.RebuildImmediate();
		EvaluatePoints();
		PositionPlanks();
		PositionPoles();
		RefreshCollider();
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public void RefreshLegs()
	{
		PositionPoles();
		RefreshCollider();
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public void RefreshCollider()
	{
		m_Bounds.SetMinMax(EvaluateSpline(0.0).position, EvaluateSpline(0.0).position);
		for (double num = 0.0; num <= 1.0; num += 0.009999999776482582)
		{
			m_Bounds.Encapsulate(EvaluateSpline(num).position);
		}
		m_Bounds.Expand(0.15f);
		m_BoxCollider.center = m_Bounds.center - base.transform.position;
		m_BoxCollider.size = m_Bounds.size;
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public Ramp Duplicate(Vector3 offset)
	{
		Ramp ramp = Ramps.CreateRamp(base.transform.position, base.transform.rotation);
		if ((bool)ramp)
		{
			ramp.m_Height = m_Height;
			ramp.m_NumSegments = m_NumSegments;
			ramp.m_SplineType = m_SplineType;
			ramp.m_FlippedVertical = m_FlippedVertical;
			ramp.m_FlippedHorizontal = m_FlippedHorizontal;
			ramp.m_FlippedLegs = m_FlippedLegs;
			ramp.m_HideLegs = m_HideLegs;
			ramp.transform.position += offset;
			ramp.SetControlPoints(GetControlPointPositions());
			ramp.RefreshMesh();
		}
		return ramp;
	}

	public List<Vector2> GetControlPointPositions()
	{
		List<Vector2> list = new List<Vector2>();
		SplinePoint[] points = m_SplineComputer.GetPoints(SplineComputer.Space.Local);
		for (int i = 0; i < points.Length; i++)
		{
			SplinePoint splinePoint = points[i];
			list.Add(new Vector2(splinePoint.position.x, splinePoint.position.y));
		}
		return list;
	}

	public void SetControlPoints(List<Vector2> controlPoints)
	{
		if (controlPoints != null && controlPoints.Count != 0)
		{
			SetSplineComputerControlPoints(controlPoints);
			SetRampControlPoints(controlPoints);
		}
	}

	public void SetSplineType(Dreamteck.Splines.Spline.Type splineType)
	{
		m_SplineType = splineType;
		m_SplineComputer.type = splineType;
	}

	public void SetNumSegments(int numSegments)
	{
		m_NumSegments = numSegments;
	}

	public void ActivateControlPoints()
	{
		foreach (SplineControlPoint controlPoint in m_ControlPoints)
		{
			controlPoint.gameObject.SetActive(value: true);
		}
	}

	public void DeActivateControlPoints()
	{
		foreach (SplineControlPoint controlPoint in m_ControlPoints)
		{
			controlPoint.gameObject.SetActive(value: false);
		}
	}

	public void FlipHorizontal()
	{
		foreach (SplineControlPoint controlPoint in m_ControlPoints)
		{
			controlPoint.transform.localPosition = new Vector3(0f - controlPoint.transform.localPosition.x, controlPoint.transform.localPosition.y, controlPoint.transform.localPosition.z);
		}
		m_ControlPoints.Reverse();
		SyncControlPoints();
		RefreshMesh();
	}

	public void FlipVertical()
	{
		foreach (SplineControlPoint controlPoint in m_ControlPoints)
		{
			controlPoint.transform.localPosition = new Vector3(controlPoint.transform.localPosition.x, 0f - controlPoint.transform.localPosition.y, controlPoint.transform.localPosition.z);
		}
		SyncControlPoints();
		RefreshMesh();
	}

	public void AddToSimulation()
	{
		float num = 1f / (float)m_NumSegments;
		List<Poly.Physics.Node> list = new List<Poly.Physics.Node>();
		list.Add(AddPhysicsNode(0.0));
		for (int i = 1; i < m_NumSegments; i++)
		{
			list.Add(AddPhysicsNode(Mathf.Clamp01(num * (float)i)));
		}
		list.Add(AddPhysicsNode(1.0));
		for (int j = 0; j < list.Count - 1; j++)
		{
			Spline.AddPhysicsEdge(list[j], list[j + 1]);
		}
	}

	public void SetSplineComputerControlPoints(List<Vector2> points)
	{
		List<SplinePoint> list = new List<SplinePoint>();
		for (int i = 0; i < points.Count; i++)
		{
			SplinePoint item = new SplinePoint(points[i]);
			item.normal = Vector3.up;
			list.Add(item);
		}
		m_SplineComputer.SetPoints(list.ToArray(), SplineComputer.Space.Local);
	}

	public void SyncControlPoints()
	{
		List<Vector2> list = new List<Vector2>();
		foreach (SplineControlPoint controlPoint in m_ControlPoints)
		{
			list.Add(Utils.V3toV2(controlPoint.transform.position - base.transform.position));
		}
		SetSplineComputerControlPoints(list);
	}

	public void DisableOutline()
	{
		m_SandboxItem.m_OutlineGroup.DisableOutline();
	}

	public void EnableMeshRendering()
	{
		m_PlanksParent.gameObject.SetActive(value: true);
		m_PolesParent.gameObject.SetActive(!m_HideLegs);
	}

	public void DisableMeshRendering()
	{
		m_PlanksParent.gameObject.SetActive(value: false);
		m_PolesParent.gameObject.SetActive(value: false);
	}

	public void UpdateOutline()
	{
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			if (m_isRenderingEnabled)
			{
				DisableMeshRendering();
			}
		}
		else if (!m_isRenderingEnabled)
		{
			EnableMeshRendering();
		}
		if (!m_HasCreatedOutline)
		{
			m_RampOutline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_RampOutline.SetTextureScale(GameUI.m_Instance.m_OutlineTextureScale);
			m_RampOutline.SetTexture((GameStateManager.GetState() == GameState.BUILD) ? GameUI.m_Instance.m_OutlineTextureDashedBuildMode : GameUI.m_Instance.m_OutlineTextureSandbox);
			m_LeftStiltOutline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_RightStiltOutline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_HasCreatedOutline = true;
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		if (!m_SandboxItem.IsOutlineDirty())
		{
			return;
		}
		EvaluatePoints();
		m_OutlinePoints.Clear();
		foreach (SplineResult evaulatedPoint in m_EvaulatedPoints)
		{
			Vector3 vector = ComputeNormal(evaulatedPoint);
			m_OutlinePoints.Add(evaulatedPoint.position + vector * 0.1f);
		}
		m_EvaulatedPoints.Reverse();
		foreach (SplineResult evaulatedPoint2 in m_EvaulatedPoints)
		{
			Vector3 vector2 = ComputeNormal(evaulatedPoint2);
			m_OutlinePoints.Add(evaulatedPoint2.position - vector2 * 0.1f);
		}
		m_OutlinePoints.Add(m_OutlinePoints[0]);
		m_SandboxItem.UpdateOutlinePoints(m_RampOutline, m_OutlinePoints);
		int numMiddlePoles = GetNumMiddlePoles();
		if (m_MiddleStiltOutlines.Count < numMiddlePoles)
		{
			int num = numMiddlePoles - m_MiddleStiltOutlines.Count;
			for (int i = 0; i < num; i++)
			{
				Outline item = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
				m_MiddleStiltOutlines.Add(item);
			}
		}
		if (GameStateManager.GetState() == GameState.SANDBOX && !m_HideLegs)
		{
			UpdateStiltsOutline();
		}
		else
		{
			DisableStiltsOutline();
		}
		m_SandboxItem.SetOutlineDirty(dirty: false);
	}

	public void EnterBuildMode()
	{
		EnableMeshRendering();
		if (m_RampOutline != null)
		{
			m_RampOutline.SetTexture(GameUI.m_Instance.m_OutlineTextureDashedBuildMode);
		}
		DisableStiltsOutline();
	}

	public void EnterSandboxMode()
	{
		DisableMeshRendering();
		if (m_RampOutline != null)
		{
			m_RampOutline.SetTexture(GameUI.m_Instance.m_OutlineTextureSandbox);
		}
		if (m_HideLegs)
		{
			DisableStiltsOutline();
		}
		else
		{
			UpdateStiltsOutline();
		}
	}

	public void UpdateShaderProperties(bool buildMode)
	{
		m_MaterialPropertyBlock.SetFloat(ShaderVariables_SimpleLitCollidable.BUILD_MODE_SHADER_ID, buildMode ? 1f : 0f);
		m_MaterialPropertyBlock.SetColor(ShaderVariables_SimpleLitCollidable.BUILD_MODE_TINT_SHADER_ID, PostFX.m_Instance.m_BuildModeCollideTint);
		foreach (MeshRenderer plank in m_Planks)
		{
			m_MaterialPropertyBlock.SetColor(ShaderVariables_SimpleLitCollidable.BUILD_MODE_TINT_SHADER_ID, PostFX.m_Instance.m_BuildModeCollideTint);
			plank.SetPropertyBlock(m_MaterialPropertyBlock);
		}
		foreach (MeshRenderer pole in m_Poles)
		{
			m_MaterialPropertyBlock.SetColor(ShaderVariables_SimpleLitCollidable.BUILD_MODE_TINT_SHADER_ID, PostFX.m_Instance.m_BuildModeSupportCollideTint);
			pole.SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	private void DisableStiltsOutline()
	{
		if (m_LeftStiltOutline != null)
		{
			m_LeftStiltOutline.SetActive(active: false);
		}
		if (m_RightStiltOutline != null)
		{
			m_RightStiltOutline.SetActive(active: false);
		}
		foreach (Outline middleStiltOutline in m_MiddleStiltOutlines)
		{
			middleStiltOutline.SetActive(active: false);
		}
	}

	private void UpdateStiltsOutline()
	{
		if (m_LeftStiltOutline != null && m_Poles.Count > 0)
		{
			UpdateStiltOutline(m_LeftStiltOutline, m_Poles[0]);
		}
		if (m_RightStiltOutline != null && m_Poles.Count > 1)
		{
			UpdateStiltOutline(m_RightStiltOutline, m_Poles[1]);
		}
		for (int i = 0; i < m_MiddleStiltOutlines.Count; i++)
		{
			Outline outline = m_MiddleStiltOutlines[i];
			outline.SetActive(active: false);
			if (i < GetNumMiddlePoles() && 2 + i < m_Poles.Count)
			{
				UpdateStiltOutline(outline, m_Poles[2 + i]);
			}
		}
	}

	private void UpdateStiltOutline(Outline outline, MeshRenderer pole)
	{
		float num = CalculateWidth();
		_ = num / (float)(GetNumMiddlePoles() + 1);
		_ = num / 2f;
		float num2 = POLE_WIDTH * POLE_SCALE_X;
		float num3 = Platforms.THICKNESS / 2f;
		Vector3 vector = pole.transform.position + new Vector3((0f - num2) / 2f, m_FlippedVertical ? num3 : 0f, 0f);
		Vector3 vector2 = vector + new Vector3(num2, pole.transform.localScale.y - num3, 0f);
		m_TempPointsBuffer.Clear();
		Vector3 item = vector2 - new Vector3(num2, 0f, 0f);
		Vector3 item2 = vector2;
		Vector3 item3 = vector;
		Vector3 item4 = vector + new Vector3(num2, 0f, 0f);
		m_TempPointsBuffer.Add(item);
		m_TempPointsBuffer.Add(item3);
		m_TempPointsBuffer.Add(item4);
		m_TempPointsBuffer.Add(item2);
		m_TempPointsBuffer.Add(item);
		m_SandboxItem.UpdateOutlinePoints(outline, m_TempPointsBuffer);
		if (!pole.enabled)
		{
			outline.SetActive(active: false);
		}
	}

	public bool PointOnRampSurface(Vector3 point)
	{
		double percent = m_SplineComputer.Project(point);
		SplineResult splineResult = m_SplineComputer.Evaluate(percent);
		return Vector3.Distance(point, splineResult.position) < Ramps.THICKNESS;
	}

	public List<Vector2> GetLinePoints()
	{
		m_LinePoints.Clear();
		for (int i = 0; i < m_EvaulatedPoints.Count; i++)
		{
			m_LinePoints.Add(m_EvaulatedPoints[i].position);
		}
		return m_LinePoints;
	}

	public void SetLinePoints(List<Vector2> linePoints)
	{
		m_LinePoints = linePoints;
	}

	public bool OverlapsRect(Rect rect)
	{
		new Bounds(rect.center, rect.size);
		SplineResult splineResult = new SplineResult();
		SplineResult splineResult2 = new SplineResult();
		for (int i = 0; i < m_NumSegments; i++)
		{
			m_SplineComputer.Evaluate(splineResult, (float)i / (float)m_NumSegments);
			m_SplineComputer.Evaluate(splineResult2, (float)(i + 1) / (float)m_NumSegments);
			if (Utils.LineIntersectsRect(splineResult.position, splineResult2.position, rect))
			{
				return true;
			}
		}
		return false;
	}

	public void RecalulateNumSegments()
	{
		m_NumSegments = Mathf.Clamp(Mathf.RoundToInt(m_SplineComputer.CalculateLength() * 2f), 10, 1000);
	}

	private void SetRampControlPoints(List<Vector2> points)
	{
		foreach (SplineControlPoint controlPoint in m_ControlPoints)
		{
			UnityEngine.Object.Destroy(controlPoint.gameObject);
		}
		m_ControlPoints.Clear();
		foreach (Vector2 point in points)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_SplineControlPoint, new Vector3(point.x, point.y, 0f) + base.transform.position, Quaternion.identity);
			if ((bool)gameObject)
			{
				gameObject.name = Prefabs.m_Instance.m_SplineControlPoint.name;
				gameObject.transform.SetParent(base.transform);
				gameObject.SetActive(value: false);
				SplineControlPoint component = gameObject.GetComponent<SplineControlPoint>();
				if ((bool)component)
				{
					component.m_SpriteRenderer.transform.localPosition = new Vector3(0f, 0f, -2f);
					m_ControlPoints.Add(component);
				}
			}
		}
	}

	private Poly.Physics.Node AddPhysicsNode(double percent)
	{
		return Spline.AddPhysicsNode(EvaluateSpline(percent).position);
	}

	private SplineResult EvaluateSpline(double percent)
	{
		SplineResult result = new SplineResult();
		m_SplineComputer.Evaluate(result, percent);
		return result;
	}

	private Vector3 ComputeNormal(SplineResult splineResult)
	{
		return Vector3.Cross(splineResult.direction, splineResult.right).normalized;
	}

	private void SetPivotToCenter()
	{
		m_Bounds.SetMinMax(EvaluateSpline(0.0).position, EvaluateSpline(0.0).position);
		for (double num = 0.0; num <= 1.0; num += 0.009999999776482582)
		{
			m_Bounds.Encapsulate(EvaluateSpline(num).position);
		}
		m_Bounds.Expand(0.15f);
		_ = m_Bounds.center - base.transform.position;
		foreach (SplineControlPoint controlPoint in m_ControlPoints)
		{
			controlPoint.transform.localPosition = controlPoint.transform.position - m_Bounds.center;
		}
		base.transform.position = m_Bounds.center;
		foreach (SplineControlPoint controlPoint2 in m_ControlPoints)
		{
			m_SplineComputer.SetPointPosition(m_ControlPoints.IndexOf(controlPoint2), controlPoint2.transform.position);
		}
	}

	private float CalculateRampLength()
	{
		float num = 0f;
		for (int i = 0; i < m_EvaulatedPoints.Count - 1; i++)
		{
			num += Vector2.Distance(m_EvaulatedPoints[i].position, m_EvaulatedPoints[i + 1].position);
		}
		return num;
	}

	private void PositionPlanks()
	{
		int numSegments = m_NumSegments;
		for (int i = m_Planks.Count; i < numSegments; i++)
		{
			GameObject gameObject = InstantiatePlank(m_PlanksParent);
			if (gameObject != null)
			{
				m_Planks.Add(gameObject.GetComponent<MeshRenderer>());
			}
		}
		for (int j = 0; j < m_Planks.Count; j++)
		{
			m_Planks[j].gameObject.SetActive(j < numSegments);
		}
		for (int k = 0; k < m_EvaulatedPoints.Count - 1; k++)
		{
			Vector3 position = m_EvaulatedPoints[k].position;
			Vector3 position2 = m_EvaulatedPoints[k + 1].position;
			float num = Vector2.Distance(position, position2);
			m_Planks[k].transform.position = (position + position2) / 2f;
			m_Planks[k].transform.localScale = new Vector3(num / PLANK_WIDTH, PLANK_YSCALE, PLANK_ZSCALE);
			Vector3 vector = position2 - position;
			float num2 = 0.5f * (float)Math.Atan2(vector.y, vector.x);
			float w = (float)Math.Cos(num2);
			float z = (float)Math.Sin(num2);
			m_Planks[k].transform.rotation = new Quaternion(0f, 0f, z, w);
		}
	}

	private void PositionPoles()
	{
		float num = CalculateWidth();
		float num2 = CalulateLowestY() - m_Height - Ramps.THICKNESS / 2f;
		float num3 = CalulateHighestY() + m_Height + Ramps.THICKNESS / 2f;
		float num4 = (m_FlippedVertical ? 0f : (Ramps.THICKNESS / 2f));
		float num5 = (m_FlippedVertical ? (Ramps.THICKNESS / 2f) : 0f);
		int num6 = 2 + Mathf.FloorToInt(num / 6f);
		for (int i = m_Poles.Count; i < num6; i++)
		{
			GameObject gameObject = InstantiatePole(m_PolesParent);
			if (gameObject != null)
			{
				m_Poles.Add(gameObject.GetComponent<MeshRenderer>());
			}
		}
		for (int j = 0; j < m_Poles.Count; j++)
		{
			m_Poles[j].gameObject.SetActive(j < num6);
			m_Poles[j].enabled = true;
		}
		SplineResult splineResult = EvaluateSpline(0.0);
		Vector3 lhs = ComputeNormal(splineResult);
		float num7 = POLE_WIDTH * POLE_SCALE_X / 2f * Vector3.Dot(lhs, Vector3.up);
		float num8 = 0.05f * Vector3.Dot(lhs, Vector3.right);
		if (m_FlippedLegs)
		{
			m_Poles[0].transform.position = new Vector3(splineResult.position.x + num7 + 0.001f, splineResult.position.y + num8 + num4, 0f);
			m_Poles[0].transform.localScale = new Vector3(POLE_SCALE_X, num3 - splineResult.position.y - num8, 1f);
		}
		else
		{
			m_Poles[0].transform.position = new Vector3(splineResult.position.x + num7 + 0.001f, num2 - num5, 0f);
			m_Poles[0].transform.localScale = new Vector3(POLE_SCALE_X, splineResult.position.y - num2 - num8, 1f);
		}
		SplineResult splineResult2 = EvaluateSpline(1.0);
		lhs = ComputeNormal(splineResult2);
		num7 = POLE_WIDTH * POLE_SCALE_X / 2f * Vector3.Dot(lhs, Vector3.up);
		num8 = 0.05f * Vector3.Dot(lhs, Vector3.right);
		if (m_FlippedLegs)
		{
			m_Poles[1].transform.position = new Vector3(splineResult2.position.x - num7 - 0.001f, splineResult2.position.y + num8 + num4, 0f);
			m_Poles[1].transform.localScale = new Vector3(POLE_SCALE_X, num3 - splineResult2.position.y - num8, 1f);
		}
		else
		{
			m_Poles[1].transform.position = new Vector3(splineResult2.position.x - num7 - 0.001f, num2 - num5, 0f);
			m_Poles[1].transform.localScale = new Vector3(POLE_SCALE_X, splineResult2.position.y - num2 - num8, 1f);
		}
		int num9 = num6 - 2;
		if (num9 > 0)
		{
			float num10 = num / (float)(num9 + 1);
			for (int k = 0; k < num9; k++)
			{
				float value = num10 * (float)(k + 1) / num;
				EvaluateSpline(Mathf.Clamp01(value));
				float num11 = splineResult.position.x + num10 * (float)(k + 1);
				float splineY = GetSplineY(num11);
				if (m_FlippedLegs)
				{
					m_Poles[2 + k].transform.position = new Vector3(num11, splineY + 0.05f + num4, 0f);
					m_Poles[2 + k].transform.localScale = new Vector3(POLE_SCALE_X, num3 - splineY, 1f);
				}
				else
				{
					m_Poles[2 + k].transform.position = new Vector3(num11, num2 - num5, 0f);
					m_Poles[2 + k].transform.localScale = new Vector3(POLE_SCALE_X, splineY - num2, 1f);
				}
			}
		}
		HidePolesThatAreTooShort();
	}

	private void HidePolesThatAreTooShort()
	{
		for (int i = 0; i < m_Poles.Count; i++)
		{
			if (m_Poles[i].transform.localScale.y < Ramps.THICKNESS / 2f + 0.05f)
			{
				m_Poles[i].enabled = false;
			}
		}
	}

	private float GetSplineY(float xGoal)
	{
		float num = 0f;
		float num2 = 1f;
		float result = 0f;
		for (int i = 0; i < 100; i++)
		{
			float num3 = num + (num2 - num) / 2f;
			SplineResult splineResult = EvaluateSpline(num3);
			if (splineResult.position.x > xGoal)
			{
				num2 = num3;
			}
			else
			{
				num = num3;
			}
			if (Mathf.Abs(splineResult.position.x - xGoal) < 0.001f)
			{
				return splineResult.position.y;
			}
			result = splineResult.position.y;
		}
		return result;
	}

	private GameObject InstantiatePole(Transform parent)
	{
		return UnityEngine.Object.Instantiate(m_PolePrefab, parent);
	}

	private int GetNumMiddlePoles()
	{
		int num = 0;
		for (int i = 0; i < m_Poles.Count; i++)
		{
			if (m_Poles[i].gameObject.activeSelf)
			{
				num++;
			}
		}
		return num - 2;
	}

	private float CalculateWidth()
	{
		return Mathf.Abs(m_ControlPoints[m_ControlPoints.Count - 1].transform.position.x - m_ControlPoints[0].transform.position.x);
	}

	private float CalulateLowestY()
	{
		float num = float.MaxValue;
		for (double num2 = 0.0; num2 <= 1.0; num2 += 0.009999999776482582)
		{
			SplineResult splineResult = EvaluateSpline(num2);
			if (splineResult.position.y < num)
			{
				num = splineResult.position.y;
			}
		}
		return num;
	}

	private float CalulateHighestY()
	{
		float num = float.MinValue;
		for (double num2 = 0.0; num2 <= 1.0; num2 += 0.009999999776482582)
		{
			SplineResult splineResult = EvaluateSpline(num2);
			if (splineResult.position.y > num)
			{
				num = splineResult.position.y;
			}
		}
		return num;
	}

	private GameObject InstantiatePlank(Transform parent)
	{
		return UnityEngine.Object.Instantiate(m_PlankVariantPrefabs[UnityEngine.Random.Range(0, m_PlankVariantPrefabs.Length)], parent);
	}

	private void EvaluatePoints()
	{
		m_EvaulatedPoints.Clear();
		m_EvaulatedPoints.Add(EvaluateSpline(0.0));
		float num = 1f / (float)m_NumSegments;
		for (int i = 1; i < m_NumSegments; i++)
		{
			m_EvaulatedPoints.Add(EvaluateSpline(Mathf.Clamp01((float)i * num)));
		}
		m_EvaulatedPoints.Add(EvaluateSpline(1.0));
	}
}
