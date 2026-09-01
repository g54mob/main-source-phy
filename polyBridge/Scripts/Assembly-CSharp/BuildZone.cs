using System;
using System.Collections.Generic;
using Poly;
using Poly.Collide;
using UnityEngine;

public class BuildZone : MonoBehaviour
{
	public BuildZoneType m_Type;

	[Header("Box")]
	public BoxCollider m_BoxCollider;

	[Header("Triangle Mesh")]
	public MeshRenderer m_MeshRenderer;

	public MeshFilter m_MeshFilter;

	public Color m_MeshColor;

	[Header("Control Points")]
	public Transform m_ControlPointsParent;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public bool m_LockPosition;

	[NonSerialized]
	public float m_RotationDegrees;

	[NonSerialized]
	public Vector3[] m_VertsLocalSpace;

	[NonSerialized]
	public Vector3 m_GridOffset;

	[NonSerialized]
	public List<BuildZoneControlPoint> m_ControlPoints = new List<BuildZoneControlPoint>();

	private bool m_HasCreatedOutlineMesh;

	private Vector2 m_Size;

	private Outline m_Outline;

	private bool m_HasCreatedOutline;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private Color m_TransparentColor = new Color(0f, 0f, 0f, 0f);

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_ControlPointsParent.gameObject.SetActive(value: false);
	}

	private void Start()
	{
		if (!BuildZones.m_BuildZones.Contains(this))
		{
			BuildZones.m_BuildZones.Add(this);
		}
	}

	private void Update()
	{
		m_BoxCollider.size = m_Size;
	}

	private void OnDestroy()
	{
		DestroyControlPoints();
		if (BuildZones.m_BuildZones.Contains(this))
		{
			BuildZones.m_BuildZones.Remove(this);
		}
	}

	public Vector3 GetPosition()
	{
		return base.transform.position;
	}

	public Vector2 GetSize()
	{
		return m_Size;
	}

	public void SetSize(float x, float y)
	{
		m_Size.x = x;
		m_Size.y = y;
	}

	public void SetBounds(Vector2 position, Vector2 size)
	{
		base.transform.position = position;
		m_Size = size;
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public void DisableOutline()
	{
		m_SandboxItem.m_OutlineGroup.DisableOutline();
	}

	public void EnableSpriteRendering(bool enabled)
	{
		m_MeshRenderer.enabled = enabled;
	}

	public void UpdateOutline()
	{
		if (!m_HasCreatedOutline)
		{
			m_Outline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureBuildMode, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_Outline.SetLayer(Utils.RENDER_LAST_LAYER);
			m_Outline.SetTextureScale(GameUI.m_Instance.m_OutlineTextureScale);
			m_Outline.SetActive(GameStateManager.GetState() == GameState.SANDBOX);
			m_HasCreatedOutline = true;
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		if (!m_HasCreatedOutlineMesh)
		{
			if (m_Type == BuildZoneType.RECTANGLE && (m_VertsLocalSpace == null || m_VertsLocalSpace.Length == 0))
			{
				GenerateRectangleVerts(m_Size);
			}
			if (m_Type == BuildZoneType.TRIANGLE && (m_VertsLocalSpace == null || m_VertsLocalSpace.Length == 0))
			{
				GenerateTriangleVerts(4f);
			}
			m_MeshFilter.mesh = CreateOutlineMesh(m_VertsLocalSpace);
			m_MeshRenderer.transform.localPosition = Vector3.zero;
			m_HasCreatedOutlineMesh = true;
		}
		if (!m_SandboxItem.IsOutlineDirty())
		{
			return;
		}
		if (m_Type == BuildZoneType.RECTANGLE)
		{
			m_SandboxItem.UpdateOutlineFromBounds(m_Outline, base.transform, new Bounds(GetPosition(), GetSize()));
		}
		else
		{
			List<Vector3> list = new List<Vector3>();
			Vector3[] vertsLocalSpace = m_VertsLocalSpace;
			foreach (Vector3 position in vertsLocalSpace)
			{
				list.Add(base.transform.TransformPoint(position));
			}
			list.Add(base.transform.TransformPoint(m_VertsLocalSpace[0]));
			m_SandboxItem.UpdateOutlinePoints(m_Outline, list);
		}
		m_SandboxItem.SetOutlineDirty(dirty: false);
		if (m_MeshFilter.mesh != null)
		{
			if (m_Type == BuildZoneType.RECTANGLE)
			{
				GenerateRectangleVerts(m_Size);
			}
			m_MeshFilter.mesh.vertices = m_VertsLocalSpace;
			m_MeshFilter.mesh.RecalculateNormals();
			m_MeshFilter.mesh.RecalculateBounds();
		}
	}

	public void GenerateRectangleVerts(Vector2 size)
	{
		m_VertsLocalSpace = new Vector3[4];
		m_VertsLocalSpace[0] = new Vector3(size.x / 2f, size.y / 2f, 0f);
		m_VertsLocalSpace[1] = new Vector3(size.x / 2f, (0f - size.y) / 2f, 0f);
		m_VertsLocalSpace[2] = new Vector3((0f - size.x) / 2f, (0f - size.y) / 2f, 0f);
		m_VertsLocalSpace[3] = new Vector3((0f - size.x) / 2f, size.y / 2f, 0f);
	}

	public void GenerateTriangleVerts(float sideLength)
	{
		float num = sideLength / 2f;
		m_VertsLocalSpace = new Vector3[3];
		m_VertsLocalSpace[0] = new Vector3(0f, Mathf.Sqrt(sideLength * sideLength - num * num));
		m_VertsLocalSpace[1] = new Vector3(num, 0f, 0f);
		m_VertsLocalSpace[2] = new Vector3(0f - num, 0f, 0f);
	}

	public void EnterBuildMode()
	{
		if (m_Outline == null)
		{
			UpdateOutline();
		}
		if (m_Outline != null)
		{
			m_Outline.SetTexture(GameUI.m_Instance.m_OutlineTextureBuildMode);
			m_Outline.SetActive(active: false);
		}
		EnableSpriteRendering(enabled: true);
		ExitEditMode();
		m_MeshRenderer.GetPropertyBlock(m_MaterialPropertyBlock);
		m_MaterialPropertyBlock.SetColor(ShaderVariables_Common.ALBEDO_COLOR_SHADER_ID, m_TransparentColor);
		m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
	}

	public void EnterSandboxMode()
	{
		if (m_Outline != null)
		{
			m_Outline.SetColor(GameUI.m_Instance.m_OutlineBuildZoneColor);
			m_Outline.SetTexture(GameUI.m_Instance.m_OutlineTextureSandbox);
			m_Outline.SetActive(active: true);
		}
		EnableSpriteRendering(enabled: true);
		m_MeshRenderer.GetPropertyBlock(m_MaterialPropertyBlock);
		m_MaterialPropertyBlock.SetColor(ShaderVariables_Common.ALBEDO_COLOR_SHADER_ID, m_MeshColor);
		m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
	}

	public bool OverlapsPolygonShape(PolygonShape shape)
	{
		if (m_Type == BuildZoneType.RECTANGLE)
		{
			PolygonShape shape2 = PolygonShape.FromRect((Vec2)base.transform.position, m_Size);
			return Utils.PolygonShapeOverlapsShape(shape, shape2);
		}
		Vec2[] array = new Vec2[m_VertsLocalSpace.Length];
		for (int i = 0; i < m_VertsLocalSpace.Length; i++)
		{
			Vector3 vector = base.transform.TransformPoint(m_VertsLocalSpace[i]);
			array[i] = new Vec2(vector.x, vector.y);
		}
		PolygonShape shape3 = PolygonShape.FromPoints(array);
		return Utils.PolygonShapeOverlapsShape(shape, shape3);
	}

	public BuildZone Duplicate(GameObject prefab, Vector3 offset)
	{
		BuildZone buildZone = BuildZones.CreateBuildZoneFromProxy(new BuildZoneProxy(this));
		if (buildZone == null)
		{
			return null;
		}
		buildZone.transform.position += offset;
		return buildZone;
	}

	public bool Contains(float x, float y)
	{
		if (m_Type == BuildZoneType.RECTANGLE)
		{
			return RectangleContains(m_Size, x, y);
		}
		if (m_Type == BuildZoneType.TRIANGLE && m_VertsLocalSpace != null && m_VertsLocalSpace.Length == 3)
		{
			return TriangleContains(m_VertsLocalSpace[0].x, m_VertsLocalSpace[0].y, m_VertsLocalSpace[1].x, m_VertsLocalSpace[1].y, m_VertsLocalSpace[2].x, m_VertsLocalSpace[2].y, x, y);
		}
		return false;
	}

	public void RecalculateGridOffset()
	{
		m_GridOffset = base.transform.position - GameGrid.SnapPosToGridForced(base.transform.position);
	}

	public void UpdateBuildZoneFromControlPoints()
	{
		if (m_Type == BuildZoneType.TRIANGLE)
		{
			for (int i = 0; i < m_ControlPoints.Count; i++)
			{
				m_VertsLocalSpace[i] = base.transform.InverseTransformPoint(m_ControlPoints[i].transform.position);
			}
			return;
		}
		Vector3 position = GetControlPoint(BuildZoneRectHandleType.TOP).transform.position;
		Vector3 position2 = GetControlPoint(BuildZoneRectHandleType.BOTTOM).transform.position;
		Vector3 position3 = GetControlPoint(BuildZoneRectHandleType.LEFT).transform.position;
		Vector3 position4 = GetControlPoint(BuildZoneRectHandleType.RIGHT).transform.position;
		Vector2 vector = base.transform.InverseTransformPoint(position);
		Vector2 vector2 = base.transform.InverseTransformPoint(position2);
		Vector2 vector3 = base.transform.InverseTransformPoint(position3);
		Vector2 vector4 = base.transform.InverseTransformPoint(position4);
		float x = Mathf.Abs(vector3.x - vector4.x);
		float y = Mathf.Abs(vector.y - vector2.y);
		SetSize(x, y);
		Vector3 position5 = new Vector3((vector3.x + vector4.x) / 2f, (vector.y + vector2.y) / 2f, 0f);
		base.transform.position = base.transform.TransformPoint(position5);
		Vector2 vector5 = GetSize() / 2f;
		m_ControlPoints[0].transform.localPosition = new Vector3(0f, vector5.y, 0f);
		m_ControlPoints[1].transform.localPosition = new Vector3(vector5.x, 0f, 0f);
		m_ControlPoints[2].transform.localPosition = new Vector3(0f, 0f - vector5.y, 0f);
		m_ControlPoints[3].transform.localPosition = new Vector3(0f - vector5.x, 0f, 0f);
	}

	public void PutControlPointsInClockwiseOrder()
	{
		Vector3 position = m_ControlPoints[0].transform.position;
		Vector3 position2 = m_ControlPoints[1].transform.position;
		Vector3 position3 = m_ControlPoints[2].transform.position;
		Vector3 lhs = position2 - position;
		Vector3 rhs = position3 - position;
		if (Vector3.Cross(lhs, rhs).z > 0f)
		{
			BuildZoneControlPoint value = m_ControlPoints[1];
			m_ControlPoints[1] = m_ControlPoints[2];
			m_ControlPoints[2] = value;
		}
	}

	public BuildZoneControlPoint GetControlPoint(BuildZoneRectHandleType rectHandleType)
	{
		foreach (BuildZoneControlPoint controlPoint in m_ControlPoints)
		{
			if (controlPoint.m_RectHandleType == rectHandleType)
			{
				return controlPoint;
			}
		}
		return null;
	}

	public void PositionControlPoints()
	{
		if (m_Type == BuildZoneType.RECTANGLE)
		{
			Vector2 vector = GetSize() / 2f;
			m_ControlPoints[0].transform.localPosition = new Vector3(0f, vector.y, 0f);
			m_ControlPoints[1].transform.localPosition = new Vector3(vector.x, 0f, 0f);
			m_ControlPoints[2].transform.localPosition = new Vector3(0f, 0f - vector.y, 0f);
			m_ControlPoints[3].transform.localPosition = new Vector3(0f - vector.x, 0f, 0f);
			m_ControlPoints[0].m_Restriction = BuildZoneControlPointRestriction.LOCAL_YAXIS;
			m_ControlPoints[1].m_Restriction = BuildZoneControlPointRestriction.LOCAL_XAXIS;
			m_ControlPoints[2].m_Restriction = BuildZoneControlPointRestriction.LOCAL_YAXIS;
			m_ControlPoints[3].m_Restriction = BuildZoneControlPointRestriction.LOCAL_XAXIS;
			m_ControlPoints[0].m_RectHandleType = BuildZoneRectHandleType.TOP;
			m_ControlPoints[1].m_RectHandleType = BuildZoneRectHandleType.RIGHT;
			m_ControlPoints[2].m_RectHandleType = BuildZoneRectHandleType.BOTTOM;
			m_ControlPoints[3].m_RectHandleType = BuildZoneRectHandleType.LEFT;
		}
		else
		{
			m_ControlPoints[0].transform.localPosition = m_VertsLocalSpace[0];
			m_ControlPoints[1].transform.localPosition = m_VertsLocalSpace[1];
			m_ControlPoints[2].transform.localPosition = m_VertsLocalSpace[2];
			m_ControlPoints[0].m_Restriction = BuildZoneControlPointRestriction.NONE;
			m_ControlPoints[1].m_Restriction = BuildZoneControlPointRestriction.NONE;
			m_ControlPoints[2].m_Restriction = BuildZoneControlPointRestriction.NONE;
		}
	}

	public void CreateControlPoints()
	{
		if (m_Type == BuildZoneType.TRIANGLE)
		{
			CreateTriangleControlPoints();
		}
		else
		{
			CreateRectControlPoints();
		}
	}

	public void RecalculatePivot()
	{
		Vector3 position = base.gameObject.transform.position;
		Vector3 zero = Vector3.zero;
		Quaternion rotation = base.gameObject.transform.rotation;
		base.gameObject.transform.rotation = Quaternion.identity;
		Bounds bounds = new Bounds(m_ControlPoints[0].transform.position, Vector3.zero);
		foreach (BuildZoneControlPoint controlPoint in m_ControlPoints)
		{
			bounds.Encapsulate(controlPoint.transform.position);
		}
		base.gameObject.transform.rotation = rotation;
		Vector3 vector = new Vector3(bounds.center.x, bounds.center.y, 0f);
		zero = rotation * (vector - position) + position;
		if (Mathf.Approximately((base.transform.position - zero).magnitude, 0f))
		{
			return;
		}
		foreach (BuildZoneControlPoint controlPoint2 in m_ControlPoints)
		{
			controlPoint2.transform.localPosition = base.gameObject.transform.InverseTransformVector(controlPoint2.transform.position - zero);
		}
		base.gameObject.transform.position = zero;
	}

	public void EnterEditMode()
	{
		m_ControlPointsParent.gameObject.SetActive(value: true);
	}

	public void ExitEditMode()
	{
		m_ControlPointsParent.gameObject.SetActive(value: false);
	}

	public void DestroyControlPoints()
	{
		foreach (BuildZoneControlPoint controlPoint in m_ControlPoints)
		{
			if (controlPoint != null)
			{
				UnityEngine.Object.Destroy(controlPoint.gameObject);
			}
		}
		m_ControlPoints.Clear();
	}

	private Mesh CreateOutlineMesh(Vector3[] points)
	{
		Vector2[] array = new Vector2[points.Length];
		for (int i = 0; i < points.Length; i++)
		{
			array[i] = new Vector2(points[i].x, points[i].y);
		}
		int[] triangles = new TriangulatorBridges(array).Triangulate();
		Vector3[] array2 = new Vector3[array.Length];
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j] = new Vector3(array[j].x, array[j].y, 0f);
		}
		Mesh mesh = new Mesh();
		mesh.vertices = array2;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}

	private bool RectangleContains(Vector2 size, float x, float y)
	{
		float num = 0.0001f;
		float num2 = size.x / 2f + num;
		float num3 = size.y / 2f + num;
		Vector3 position = new Vector3(x, y, 0f);
		Vector3 vector = base.transform.InverseTransformPoint(position);
		if (vector.x > num2)
		{
			return false;
		}
		if (vector.x < 0f - num2)
		{
			return false;
		}
		if (vector.y > num3)
		{
			return false;
		}
		if (vector.y < 0f - num3)
		{
			return false;
		}
		return true;
	}

	private bool TriangleContains(float x1, float y1, float x2, float y2, float x3, float y3, float x, float y)
	{
		Vector3 position = new Vector3(x, y, 0f);
		Vector3 vector = base.transform.InverseTransformPoint(position);
		float num = ((y2 - y3) * (vector.x - x3) + (x3 - x2) * (vector.y - y3)) / ((y2 - y3) * (x1 - x3) + (x3 - x2) * (y1 - y3));
		float num2 = ((y3 - y1) * (vector.x - x3) + (x1 - x3) * (vector.y - y3)) / ((y2 - y3) * (x1 - x3) + (x3 - x2) * (y1 - y3));
		float num3 = 1f - num - num2;
		if (num >= 0f && num2 >= 0f)
		{
			return num3 >= 0f;
		}
		return false;
	}

	private void CreateTriangleControlPoints()
	{
		for (int i = 0; i < 3; i++)
		{
			BuildZoneControlPoint item = CreateControlPoint(m_Type, m_ControlPointsParent);
			m_ControlPoints.Add(item);
		}
	}

	private void CreateRectControlPoints()
	{
		for (int i = 0; i < 4; i++)
		{
			BuildZoneControlPoint item = CreateControlPoint(m_Type, m_ControlPointsParent);
			m_ControlPoints.Add(item);
		}
	}

	private BuildZoneControlPoint CreateControlPoint(BuildZoneType buildZoneType, Transform parent)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate((buildZoneType == BuildZoneType.RECTANGLE) ? Prefabs.m_Instance.m_BuildZoneRectControlPoint : Prefabs.m_Instance.m_BuildZoneTriangleControlPoint, parent);
		if (!gameObject)
		{
			return null;
		}
		return gameObject.GetComponent<BuildZoneControlPoint>();
	}
}
