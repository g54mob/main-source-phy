using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PolygonTool : MonoBehaviour
{
	public delegate void PolygonChanged(Polygon polygon, List<Vector2> previousVertices, List<Hole> previousHoles);

	public delegate void FloorEdgePressed(Floor floor, int edgeIndex);

	[Serializable]
	public class VertexIndex
	{
		public int hole = -1;

		public int vertex = -1;

		public bool isNone()
		{
			bool num = hole == -1;
			bool flag = vertex == -1;
			return num && flag;
		}

		public void setNone()
		{
			hole = -1;
			vertex = -1;
		}

		public override string ToString()
		{
			return $"Hole: {hole}, Index: {vertex}";
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(hole, vertex);
		}

		public static bool operator ==(VertexIndex left, VertexIndex right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(VertexIndex left, VertexIndex right)
		{
			return !object.Equals(left, right);
		}

		private bool Equals(VertexIndex other)
		{
			if (hole == other.hole)
			{
				return vertex == other.vertex;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((VertexIndex)obj);
		}
	}

	[Header("Dependencies")]
	public Camera camera;

	public Polygon polygon;

	public LineRenderer polygonOutline;

	public LineRenderer edgeOutline;

	public LineRenderer holeOutlineTemplate;

	private List<LineRenderer> holeOutlines = new List<LineRenderer>();

	[Header("Tooltip")]
	public Canvas tooltipCanvas;

	private Text tooltipText;

	[Header("Editing")]
	public bool isEditingEnabled = true;

	[Header("Grid & Snap")]
	public bool alwaysSnapToGrid = true;

	public Vector2 snapStep = new Vector2(1f, 1f);

	[Header("Vertices")]
	public Transform vertexPrefab;

	public Transform vertexPreview;

	public Transform polygonVertexContainer;

	public Transform holeVertexContainer;

	[Range(0.01f, 1f)]
	public float vertexSize = 0.2f;

	[Header("Debug")]
	public VertexIndex hoveredVertexIndex = new VertexIndex();

	public VertexIndex selectedVertexIndex = new VertexIndex();

	public VertexIndex addVertexIndexA = new VertexIndex();

	public VertexIndex addVertexIndexB = new VertexIndex();

	public float addVertexPercent = -1f;

	public VertexIndex selectedEdgeVertexA = new VertexIndex();

	public VertexIndex selectedEdgeVertexB = new VertexIndex();

	private Vector3 edgeDragVertexAStartPosition;

	private Vector3 edgeDragVertexBStartPosition;

	private Polygon polygonLastFrame;

	private Plane plane;

	private Ray ray;

	private Vector3 hitPoint;

	private Vector3 lastValidHitPoint;

	private RaycastHit[] hits = new RaycastHit[32];

	public PolygonChanged onPolygonChanged;

	public Action onPolygonCancel;

	public FloorEdgePressed onFloorEdgePressed;

	private List<Vector2> editStartPolygonVertices;

	private List<Hole> editStartPolygonHoles;

	private Vector3 editStartHitPoint;

	private readonly List<Vector2> dragStartPolygonVertices = new List<Vector2>();

	private bool isDraggingHoleCopy;

	private static Color Red => Color.softRed;

	private static Color Green => Color.limeGreen;

	private static Color Blue => Color.dodgerBlue;

	private static Color Orange => Color.darkOrange;

	private static Color Yellow => Color.yellowNice;

	private bool isSnapToggled => Input.GetKey(KeyCode.LeftShift);

	private bool isAddVertexPressed => Input.GetKey(KeyCode.LeftAlt);

	[ContextMenu("addHole")]
	public void addHole()
	{
		if (polygon == null)
		{
			return;
		}
		List<Vector2> previousVertices = polygon.copyVertices();
		List<Hole> previousHoles = polygon.copyHoles();
		Hole hole = newHole();
		if (polygon is Wall wall && wall.tryGetFloor(out var floor))
		{
			for (int i = 0; i < hole.vertices.Count; i++)
			{
				hole.vertices[i] += Vector2.up * floor.wallHeight / 2f;
			}
		}
		polygon.holes.Add(hole);
		onPolygonChanged?.Invoke(polygon, previousVertices, previousHoles);
	}

	public Hole newHole()
	{
		return new Hole
		{
			vertices = 
			{
				new Vector2(0f - snapStep.x, 0f - snapStep.y),
				new Vector2(0f - snapStep.x, snapStep.y),
				new Vector2(snapStep.x, snapStep.y),
				new Vector2(snapStep.x, 0f - snapStep.y)
			}
		};
	}

	public bool isInteractingWithVertexOrEdge(bool includeHover)
	{
		if (!hoveredVertexIndex.isNone() && includeHover)
		{
			return true;
		}
		if (!selectedVertexIndex.isNone())
		{
			return true;
		}
		if (!selectedEdgeVertexA.isNone())
		{
			return true;
		}
		if (addVertexPercent >= 0f && includeHover)
		{
			return true;
		}
		return false;
	}

	public void init()
	{
		changeVertexColor(vertexPreview, Color.white);
		vertexPreview.gameObject.SetActive(value: false);
		tooltipCanvas.enabled = false;
		tooltipText = tooltipCanvas.GetComponentInChildren<Text>();
	}

	public void update()
	{
		updatePolygonChange();
		if (!(polygon == null))
		{
			updateHoleContainers();
			updateMouseInput();
			updateVertexAddition();
			updateVertexDeletion();
			updateFloorEdgePress();
			updateOutlines();
			updateVertices();
			updateTooltip();
		}
	}

	private void updatePolygonChange()
	{
		if (polygon == polygonLastFrame)
		{
			return;
		}
		if (polygonLastFrame != null)
		{
			foreach (Transform item in polygonVertexContainer)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
			foreach (Transform item2 in holeVertexContainer)
			{
				UnityEngine.Object.Destroy(item2.gameObject);
			}
		}
		if (polygon == null)
		{
			polygonOutline.positionCount = 0;
			edgeOutline.positionCount = 0;
			tooltipCanvas.enabled = false;
			vertexPreview.gameObject.SetActive(value: false);
			foreach (LineRenderer holeOutline in holeOutlines)
			{
				holeOutline.positionCount = 0;
			}
		}
		polygonLastFrame = polygon;
	}

	private void updateHoleContainers()
	{
		int count = polygon.holes.Count;
		int childCount = holeVertexContainer.childCount;
		if (count == childCount)
		{
			return;
		}
		foreach (Transform item in holeVertexContainer)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		for (int i = 0; i < count; i++)
		{
			GameObject gameObject = new GameObject("Vertices");
			gameObject.transform.SetParent(holeVertexContainer);
			int count2 = polygon.holes[i].vertices.Count;
			for (int j = 0; j < count2; j++)
			{
				UnityEngine.Object.Instantiate(vertexPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
			}
		}
	}

	private void updateMouseInput()
	{
		plane = new Plane(polygon.planeWorldNormal, polygon.transform.position);
		ray = camera.ScreenPointToRay(Input.mousePosition);
		plane.Raycast(ray, out var enter);
		hitPoint = ((enter > 0f) ? ray.GetPoint(enter) : lastValidHitPoint);
		lastValidHitPoint = hitPoint;
		int num = Physics.RaycastNonAlloc(ray, hits);
		changeVertexColor(hoveredVertexIndex, Color.white);
		hoveredVertexIndex.setNone();
		for (int i = 0; i < num; i++)
		{
			RaycastHit raycastHit = hits[i];
			Transform parent = raycastHit.transform.parent;
			if (!(parent == null))
			{
				if (raycastHit.transform.IsChildOf(polygonVertexContainer))
				{
					hoveredVertexIndex.vertex = parent.GetSiblingIndex();
					hoveredVertexIndex.hole = -1;
					changeVertexColor(hoveredVertexIndex, Yellow);
					break;
				}
				if (raycastHit.transform.IsChildOf(holeVertexContainer))
				{
					hoveredVertexIndex.vertex = parent.GetSiblingIndex();
					hoveredVertexIndex.hole = parent.parent.GetSiblingIndex();
					changeVertexColor(hoveredVertexIndex, Yellow);
					break;
				}
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			handleMousePress();
		}
		else if (Input.GetMouseButton(0))
		{
			handleMouseDrag();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			handleMouseRelease(isCancel: false);
		}
	}

	private void handleMousePress()
	{
		if (!hoveredVertexIndex.isNone())
		{
			handleVertexPress(hoveredVertexIndex);
		}
		else if (addVertexPercent >= 0f)
		{
			if (isAddVertexPressed)
			{
				placeNewEdgeVertex();
			}
			else if (Input.GetKey(KeyCode.LeftControl))
			{
				startExtrudingEdge();
			}
			else
			{
				startDraggingEdge();
			}
		}
	}

	private void startExtrudingEdge()
	{
		startEditingPolygon();
		List<Vector2> polygonForVertex = getPolygonForVertex(addVertexIndexA);
		edgeDragVertexAStartPosition = polygon.polygonToWorldPosition(polygonForVertex[addVertexIndexA.vertex]);
		edgeDragVertexBStartPosition = polygon.polygonToWorldPosition(polygonForVertex[addVertexIndexB.vertex]);
		List<Vector2> list = new List<Vector2>(polygonForVertex);
		polygonForVertex.Insert(addVertexIndexB.vertex, list[addVertexIndexB.vertex]);
		polygonForVertex.Insert(addVertexIndexB.vertex, list[addVertexIndexA.vertex]);
		selectedEdgeVertexA.hole = addVertexIndexA.hole;
		selectedEdgeVertexA.vertex = addVertexIndexB.vertex;
		selectedEdgeVertexB.hole = addVertexIndexB.hole;
		selectedEdgeVertexB.vertex = addVertexIndexB.vertex + 1;
	}

	private void startDraggingEdge()
	{
		startEditingPolygon();
		selectedEdgeVertexA.hole = addVertexIndexA.hole;
		selectedEdgeVertexA.vertex = addVertexIndexA.vertex;
		selectedEdgeVertexB.hole = addVertexIndexB.hole;
		selectedEdgeVertexB.vertex = addVertexIndexB.vertex;
		List<Vector2> polygonForVertex = getPolygonForVertex(addVertexIndexA);
		edgeDragVertexAStartPosition = polygon.polygonToWorldPosition(polygonForVertex[addVertexIndexA.vertex]);
		edgeDragVertexBStartPosition = polygon.polygonToWorldPosition(polygonForVertex[addVertexIndexB.vertex]);
	}

	private void handleVertexPress(VertexIndex vertexIndex)
	{
		if (selectedVertexIndex == vertexIndex)
		{
			selectedVertexIndex.setNone();
		}
		else
		{
			selectVertex(vertexIndex, null);
		}
	}

	private void placeNewEdgeVertex()
	{
		List<Vector2> polygonForVertex = getPolygonForVertex(addVertexIndexA);
		Vector2 vector = polygonForVertex[addVertexIndexA.vertex];
		Vector2 vector2 = polygonForVertex[addVertexIndexB.vertex];
		Vector2 vector3 = vector + (vector2 - vector) * addVertexPercent;
		selectVertex(addVertexIndexB, vector3);
		vertexPreview.gameObject.SetActive(value: false);
		addVertexPercent = -1f;
	}

	private void handleMouseDrag()
	{
		if ((!selectedEdgeVertexA.isNone() || !selectedVertexIndex.isNone()) && Input.GetMouseButtonDown(1))
		{
			handleMouseRelease(isCancel: true);
			return;
		}
		bool flag = (alwaysSnapToGrid && !isSnapToggled) || (!alwaysSnapToGrid && isSnapToggled);
		if (!selectedEdgeVertexA.isNone())
		{
			Vector2 vector = polygon.worldToPolygonPosition(hitPoint);
			Vector2 vector2 = polygon.worldToPolygonPosition(editStartHitPoint);
			Vector2 vector3 = vector - vector2;
			if (Mathf.Abs(vector3.x) > Mathf.Abs(vector3.y))
			{
				vector3.y = 0f;
			}
			else
			{
				vector3.x = 0f;
			}
			Vector3 vector4 = polygon.polygonToWorldPosition(polygon.worldToPolygonPosition(edgeDragVertexAStartPosition) + vector3) - edgeDragVertexAStartPosition;
			Vector2 vector5 = polygon.worldToPolygonPosition(edgeDragVertexAStartPosition + vector4);
			if (flag)
			{
				vector5 = getSnappedPosition(vector5);
			}
			Vector3 vector6 = polygon.polygonToWorldPosition(vector5);
			Vector3 vector7 = edgeDragVertexBStartPosition - edgeDragVertexAStartPosition;
			Vector2 value = polygon.worldToPolygonPosition(vector6 + vector7);
			List<Vector2> polygonForVertex = getPolygonForVertex(selectedEdgeVertexA);
			polygonForVertex[selectedEdgeVertexA.vertex] = vector5;
			polygonForVertex[selectedEdgeVertexB.vertex] = value;
		}
		else
		{
			if (selectedVertexIndex.isNone())
			{
				return;
			}
			changeVertexColor(selectedVertexIndex, Yellow);
			Vector2 vector8 = polygon.worldToPolygonPosition(hitPoint);
			if (flag)
			{
				vector8 = getSnappedPosition(vector8);
			}
			List<Vector2> polygonForVertex2 = getPolygonForVertex(selectedVertexIndex);
			Vector2 vector9 = vector8 - dragStartPolygonVertices[selectedVertexIndex.vertex];
			bool flag2 = Input.GetKey(KeyCode.LeftControl) || isDraggingHoleCopy;
			for (int i = 0; i < polygonForVertex2.Count; i++)
			{
				if (flag2)
				{
					polygonForVertex2[i] = dragStartPolygonVertices[i] + vector9;
				}
				else if (i == selectedVertexIndex.vertex)
				{
					polygonForVertex2[i] = vector8;
				}
				else
				{
					polygonForVertex2[i] = dragStartPolygonVertices[i];
				}
			}
		}
		Vector3 getSnappedPosition(Vector3 toSnap)
		{
			Vector2 vector10 = snapStep;
			Vector3 localScale = polygon.transform.localScale;
			if (polygon.planeSystem == PlaneSystem.XY)
			{
				vector10.x /= localScale.x;
				vector10.y /= localScale.y;
			}
			else if (polygon.planeSystem == PlaneSystem.XZ)
			{
				vector10.x /= localScale.x;
				vector10.y /= localScale.z;
			}
			else if (polygon.planeSystem == PlaneSystem.YZ)
			{
				vector10.x /= localScale.y;
				vector10.y /= localScale.z;
			}
			toSnap.x = Mathf.Round(toSnap.x / vector10.x) * vector10.x;
			toSnap.y = Mathf.Round(toSnap.y / vector10.y) * vector10.y;
			return toSnap;
		}
	}

	private void handleMouseRelease(bool isCancel)
	{
		if (!isCancel)
		{
			isCancel = Maths.isPolygonSelfIntersecting(polygon.vertices);
		}
		if (!isCancel)
		{
			foreach (Hole hole in polygon.holes)
			{
				if (Maths.isPolygonSelfIntersecting(hole.vertices))
				{
					isCancel = true;
					break;
				}
			}
		}
		if (isCancel)
		{
			polygon.vertices = editStartPolygonVertices;
			polygon.holes = editStartPolygonHoles;
		}
		if (!selectedEdgeVertexA.isNone())
		{
			selectedEdgeVertexA.setNone();
			selectedEdgeVertexB.setNone();
			invokePolygonChangedIfNeeded();
		}
		else if (!selectedVertexIndex.isNone())
		{
			changeVertexColor(selectedVertexIndex, Color.white);
			isDraggingHoleCopy = false;
			selectedVertexIndex.setNone();
			invokePolygonChangedIfNeeded();
		}
		void invokePolygonChangedIfNeeded()
		{
			if (isCancel)
			{
				onPolygonCancel?.Invoke();
			}
			else
			{
				bool flag = !Polygon.compareVertices(polygon.vertices, editStartPolygonVertices);
				if (!flag)
				{
					flag = !Polygon.compareHoles(polygon.holes, editStartPolygonHoles);
				}
				if (flag)
				{
					onPolygonChanged?.Invoke(polygon, editStartPolygonVertices, editStartPolygonHoles);
				}
			}
		}
	}

	private void updateOutlines()
	{
		edgeOutline.positionCount = 0;
		updateOutline(polygonOutline, polygon.vertices, -1);
		int count = polygon.holes.Count;
		int count2 = holeOutlines.Count;
		if (count > count2)
		{
			int num = count - count2;
			for (int i = 0; i < num; i++)
			{
				holeOutlines.Add(UnityEngine.Object.Instantiate(holeOutlineTemplate, base.transform));
			}
		}
		else if (count < count2)
		{
			int num2 = count2 - count;
			int num3 = holeOutlines.Count - num2;
			for (int num4 = holeOutlines.Count - 1; num4 >= num3; num4--)
			{
				UnityEngine.Object.Destroy(holeOutlines[num4].gameObject);
				holeOutlines.RemoveAt(num4);
			}
		}
		for (int j = 0; j < polygon.holes.Count; j++)
		{
			updateOutline(holeOutlines[j], polygon.holes[j].vertices, j);
		}
		void updateOutline(LineRenderer outline, List<Vector2> vertices, int holeIndex)
		{
			int count3 = vertices.Count;
			bool flag = !selectedEdgeVertexA.isNone() && selectedEdgeVertexA.hole == holeIndex;
			bool flag2 = addVertexPercent >= 0f && addVertexIndexA.hole == holeIndex;
			bool flag3 = !selectedVertexIndex.isNone() && selectedVertexIndex.hole == holeIndex;
			bool flag4 = !hoveredVertexIndex.isNone() && hoveredVertexIndex.hole == holeIndex;
			Vector3[] array = new Vector3[count3];
			if (flag3 || flag4)
			{
				for (int k = 0; k < count3; k++)
				{
					array[k] = polygon.polygonToWorldPosition(vertices[k]);
				}
			}
			else if (flag || flag2)
			{
				int num5 = (flag ? selectedEdgeVertexA.vertex : addVertexIndexA.vertex);
				for (int l = 0; l < count3; l++)
				{
					array[l] = polygon.polygonToWorldPosition(vertices[(l + num5 + 1) % count3]);
				}
				edgeOutline.positionCount = 2;
				edgeOutline.SetPosition(0, polygon.polygonToWorldPosition(vertices[num5]));
				edgeOutline.SetPosition(1, polygon.polygonToWorldPosition(vertices[(num5 + 1) % count3]));
				edgeOutline.material.SetColor("_UnlitColor", (flag2 && !flag && Input.GetKey(KeyCode.LeftControl)) ? Green : Yellow);
			}
			else
			{
				for (int m = 0; m < count3; m++)
				{
					array[m] = polygon.polygonToWorldPosition(vertices[m]);
				}
			}
			outline.positionCount = count3;
			outline.SetPositions(array);
			outline.loop = flag3 || flag4 || (!flag && !flag2);
			Color value = ((holeIndex != -1) ? Orange : (isEditingEnabled ? Blue : Color.gray));
			if (Maths.isPolygonSelfIntersecting(vertices))
			{
				value = Red;
			}
			outline.material.SetColor("_UnlitColor", value);
		}
	}

	private void updateVertices()
	{
		float closestVertexDistance = float.PositiveInfinity;
		updateVertexPositions(polygon.vertices, polygonVertexContainer);
		for (int i = 0; i < polygon.holes.Count; i++)
		{
			updateVertexPositions(polygon.holes[i].vertices, holeVertexContainer.GetChild(i));
		}
		closestVertexDistance = Mathf.Clamp(closestVertexDistance, 1f, 16f);
		updateVertexSizes(polygon.vertices, polygonVertexContainer, -1);
		for (int j = 0; j < polygon.holes.Count; j++)
		{
			updateVertexSizes(polygon.holes[j].vertices, holeVertexContainer.GetChild(j), j);
		}
		vertexPreview.transform.localScale = Vector3.one * (closestVertexDistance * vertexSize);
		Physics.SyncTransforms();
		void updateVertexPositions(List<Vector2> vertices, Transform vertexContainer)
		{
			int count = vertices.Count;
			int childCount = vertexContainer.childCount;
			if (count > childCount)
			{
				int num = count - childCount;
				for (int k = 0; k < num; k++)
				{
					UnityEngine.Object.Instantiate(vertexPrefab, Vector3.zero, Quaternion.identity, vertexContainer);
				}
				recalculateVertexNames();
			}
			else if (count < childCount)
			{
				int num2 = childCount - count;
				for (int l = 0; l < num2; l++)
				{
					UnityEngine.Object.Destroy(vertexContainer.GetChild(childCount - 1 - l).gameObject);
				}
				recalculateVertexNames();
			}
			for (int m = 0; m < count; m++)
			{
				Transform child = vertexContainer.GetChild(m);
				child.position = polygon.polygonToWorldPosition(vertices[m]);
				closestVertexDistance = Mathf.Min(closestVertexDistance, (camera.transform.position - child.position).magnitude);
			}
			void recalculateVertexNames()
			{
				for (int n = 0; n < vertexContainer.childCount; n++)
				{
					vertexContainer.GetChild(n).name = $"Vertex #{n}";
				}
			}
		}
		void updateVertexSizes(List<Vector2> vertices, Transform vertexContainer, int holeIndex)
		{
			int count = vertices.Count;
			for (int k = 0; k < count; k++)
			{
				Transform child = vertexContainer.GetChild(k);
				Renderer componentInChildren = child.GetComponentInChildren<Renderer>();
				if (holeIndex == -1 && !isEditingEnabled)
				{
					child.gameObject.SetActive(value: false);
				}
				else
				{
					child.gameObject.SetActive(value: true);
					bool flag = hoveredVertexIndex.hole == holeIndex && hoveredVertexIndex.vertex == k;
					bool flag2 = selectedVertexIndex.hole == holeIndex && selectedVertexIndex.vertex == k;
					bool flag3 = false;
					if (hoveredVertexIndex.isNone() && selectedVertexIndex.isNone())
					{
						flag3 = addVertexPercent >= 0f && addVertexIndexA.hole == holeIndex && addVertexIndexA.vertex == k;
						flag3 |= addVertexPercent >= 0f && addVertexIndexB.hole == holeIndex && addVertexIndexB.vertex == k;
					}
					if (!selectedEdgeVertexA.isNone())
					{
						flag = false;
						flag3 = false;
						flag2 = selectedEdgeVertexA.hole == holeIndex && selectedEdgeVertexA.vertex == k;
						flag2 |= selectedEdgeVertexB.hole == holeIndex && selectedEdgeVertexB.vertex == k;
					}
					float num = closestVertexDistance * vertexSize;
					float b = num * 1.5f;
					float num2 = Mathf.InverseLerp(num, b, componentInChildren.transform.localScale.x);
					int num3 = ((flag || flag2 || flag3) ? 1 : (-1));
					float num4 = Mathf.Lerp(num, b, Mathf.Clamp01(num2 + (float)num3 * Time.deltaTime / 0.1f));
					componentInChildren.transform.localScale = Vector3.one * num4;
				}
			}
		}
	}

	private void updateTooltip()
	{
		if (!selectedEdgeVertexA.isNone())
		{
			Transform vertex = getVertex(selectedEdgeVertexA);
			Transform vertex2 = getVertex(selectedEdgeVertexB);
			StringBuilder stringBuilder = new StringBuilder();
			appendXYZ(stringBuilder, vertex.transform.position);
			stringBuilder.AppendLine();
			appendXYZ(stringBuilder, vertex2.transform.position);
			stringBuilder.AppendLine().AppendLine();
			stringBuilder.AppendLine("Press <b>RMB</b> to cancel.");
			if (!isSnapToggled)
			{
				stringBuilder.AppendLine().AppendLine("Hold <b>Shift</b> to " + (alwaysSnapToGrid ? "not " : "") + "snap to grid.");
			}
			stringBuilder.Length -= Environment.NewLine.Length;
			tooltipText.text = stringBuilder.ToString();
			tooltipCanvas.enabled = true;
		}
		else if (!selectedVertexIndex.isNone())
		{
			Transform vertex3 = getVertex(selectedVertexIndex);
			StringBuilder stringBuilder2 = new StringBuilder();
			appendXYZ(stringBuilder2, vertex3.transform.position);
			stringBuilder2.AppendLine();
			stringBuilder2.AppendLine("Angle: " + getVertexAngle(selectedVertexIndex).ToString("0", CultureInfo.InvariantCulture) + "°");
			stringBuilder2.AppendLine();
			stringBuilder2.AppendLine("Press <b>RMB</b> to cancel.");
			if (!isSnapToggled)
			{
				stringBuilder2.AppendLine().AppendLine("Hold <b>Shift</b> to " + (alwaysSnapToGrid ? "not " : "") + "snap to grid.");
			}
			if (!Input.GetKey(KeyCode.LeftControl))
			{
				stringBuilder2.AppendLine().AppendLine("Hold <b>CTRL</b> to move all vertices.");
			}
			stringBuilder2.Length -= Environment.NewLine.Length;
			tooltipText.text = stringBuilder2.ToString();
			tooltipCanvas.enabled = true;
		}
		else if (!hoveredVertexIndex.isNone())
		{
			Transform vertex4 = getVertex(hoveredVertexIndex);
			bool flag = hoveredVertexIndex.hole >= 0;
			StringBuilder stringBuilder3 = new StringBuilder();
			appendXYZ(stringBuilder3, vertex4.transform.position);
			stringBuilder3.AppendLine();
			stringBuilder3.AppendLine("Angle: " + getVertexAngle(hoveredVertexIndex).ToString("0", CultureInfo.InvariantCulture) + "°");
			stringBuilder3.AppendLine();
			stringBuilder3.AppendLine("Hold <b>LMB</b> - Move Vertex");
			stringBuilder3.AppendLine("<b>Alt</b> + <b>LMB</b> - " + (flag ? "Copy Hole" : "Create Hole").Colored(Orange));
			List<Vector2> polygonForVertex = getPolygonForVertex(hoveredVertexIndex);
			if (hoveredVertexIndex.hole == -1)
			{
				if (polygonForVertex != null && polygonForVertex.Count > 3)
				{
					stringBuilder3.AppendLine();
					stringBuilder3.Append("<b>MMB</b> - " + "Remove Vertex".Colored(Red));
				}
				else
				{
					stringBuilder3.Length -= Environment.NewLine.Length;
				}
			}
			else
			{
				stringBuilder3.AppendLine();
				string text = ((polygonForVertex != null && polygonForVertex.Count > 3) ? "Vertex" : "Hole");
				stringBuilder3.Append("<b>MMB</b> - " + ("Remove " + text).Colored(Red));
			}
			if (flag)
			{
				stringBuilder3.AppendLine().Append("<b>Alt</b> + <b>MMB</b> - " + "Remove Hole".Colored(Red));
			}
			tooltipText.text = stringBuilder3.ToString();
			tooltipCanvas.enabled = true;
		}
		else if (addVertexPercent >= 0f)
		{
			Transform vertex5 = getVertex(addVertexIndexA);
			Transform vertex6 = getVertex(addVertexIndexB);
			StringBuilder stringBuilder4 = new StringBuilder();
			stringBuilder4.AppendLine("Edge Length: " + Vector3.Distance(vertex5.position, vertex6.position).ToString("0.###", CultureInfo.InvariantCulture));
			stringBuilder4.AppendLine();
			stringBuilder4.AppendLine("Hold <b>LMB</b> - Move Edge");
			stringBuilder4.AppendLine("<b>CTRL</b> + <b>LMB</b> - " + "Extrude Edge".Colored(Green));
			stringBuilder4.Append("<b>Alt</b> + <b>LMB</b> - " + "Add Vertex".Colored(Green));
			if (polygon is Floor floor && addVertexIndexA.hole == -1)
			{
				bool flag2 = floor.getWall(addVertexIndexA.vertex) != null;
				stringBuilder4.AppendLine().AppendLine().Append("<b>MMB</b> - " + (flag2 ? "Remove Wall" : "Add Wall").Colored(flag2 ? Red : Green));
			}
			tooltipText.text = stringBuilder4.ToString();
			tooltipCanvas.enabled = true;
		}
		else
		{
			tooltipCanvas.enabled = false;
		}
		static void appendXYZ(StringBuilder sb, Vector3 position)
		{
			sb.Append(("<b>X:</b> " + position.x.ToString("0.###", CultureInfo.InvariantCulture) + "  ").Colored(Red));
			sb.Append(("<b>Y:</b> " + position.y.ToString("0.###", CultureInfo.InvariantCulture) + "  ").Colored(Green));
			sb.Append(("<b>Z:</b> " + position.z.ToString("0.###", CultureInfo.InvariantCulture)).Colored(Blue));
		}
		float getVertexAngle(VertexIndex vertexIndex)
		{
			List<Vector2> polygonForVertex2 = getPolygonForVertex(vertexIndex);
			if (polygonForVertex2 == null)
			{
				return 0f;
			}
			int vertex7 = vertexIndex.vertex;
			int count = polygonForVertex2.Count;
			Vector2 vector = polygonForVertex2[vertex7];
			Vector2 vector2 = polygonForVertex2[(vertex7 + 1) % count];
			Vector2 vector3 = polygonForVertex2[(vertex7 + count - 1) % count];
			if (!Maths.isPolygonClockwise(polygonForVertex2))
			{
				Vector2 vector4 = vector3;
				Vector2 vector5 = vector2;
				vector2 = vector4;
				vector3 = vector5;
			}
			return (Vector2.SignedAngle(vector3 - vector, vector2 - vector) + 360f) % 360f;
		}
	}

	private void selectVertex(VertexIndex vertexIndex, Vector3? newVertexPosition)
	{
		List<Vector2> list = getPolygonForVertex(vertexIndex);
		if (list == null)
		{
			return;
		}
		startEditingPolygon();
		if (!newVertexPosition.HasValue && Input.GetKey(KeyCode.LeftAlt))
		{
			Hole hole = new Hole();
			if (hoveredVertexIndex.hole >= 0)
			{
				hole.vertices.AddRange(list);
			}
			else
			{
				Vector2 vector = list[vertexIndex.vertex];
				hole.vertices.Add(vector);
				hole.vertices.Add(vector + new Vector2(0f, 1f));
				hole.vertices.Add(vector + new Vector2(1f, 1f));
				hole.vertices.Add(vector + new Vector2(1f, 0f));
				list = hole.vertices;
			}
			polygon.holes.Add(hole);
			selectedVertexIndex.hole = polygon.holes.Count - 1;
			selectedVertexIndex.vertex = ((hoveredVertexIndex.hole >= 0) ? vertexIndex.vertex : 0);
			isDraggingHoleCopy = true;
			updateHoleContainers();
		}
		else
		{
			selectedVertexIndex.hole = vertexIndex.hole;
			selectedVertexIndex.vertex = vertexIndex.vertex;
		}
		if (newVertexPosition.HasValue)
		{
			list.Insert(addVertexIndexB.vertex, newVertexPosition.Value);
		}
		dragStartPolygonVertices.Clear();
		dragStartPolygonVertices.AddRange(list);
	}

	private void startEditingPolygon()
	{
		editStartHitPoint = hitPoint;
		editStartPolygonVertices = polygon.copyVertices();
		editStartPolygonHoles = polygon.copyHoles();
	}

	private List<Vector2> getPolygonForVertex(VertexIndex vertexIndex)
	{
		if (vertexIndex.vertex < 0)
		{
			return null;
		}
		if (vertexIndex.hole == -1)
		{
			if (vertexIndex.vertex >= polygon.vertices.Count)
			{
				return null;
			}
			return polygon.vertices;
		}
		if (vertexIndex.hole >= polygon.holes.Count)
		{
			return null;
		}
		Hole hole = polygon.holes[vertexIndex.hole];
		if (vertexIndex.vertex >= hole.vertices.Count)
		{
			return null;
		}
		return hole.vertices;
	}

	private void changeVertexColor(VertexIndex vertexIndex, Color color)
	{
		Transform vertex = getVertex(vertexIndex);
		changeVertexColor(vertex, color);
	}

	private void changeVertexColor(Transform vertex, Color color)
	{
		if (!(vertex == null))
		{
			vertex.GetComponentInChildren<Renderer>().material.SetColor("_UnlitColor", color);
		}
	}

	private Transform getVertex(VertexIndex index)
	{
		if (index.vertex == -1)
		{
			return null;
		}
		if (index.hole == -1)
		{
			if (index.vertex < polygonVertexContainer.childCount)
			{
				return polygonVertexContainer.GetChild(index.vertex);
			}
			return null;
		}
		if (index.hole >= holeVertexContainer.childCount)
		{
			return null;
		}
		Transform child = holeVertexContainer.GetChild(index.hole);
		if (index.vertex < child.childCount)
		{
			return child.GetChild(index.vertex);
		}
		return null;
	}

	private void updateVertexAddition()
	{
		vertexPreview.gameObject.SetActive(value: false);
		float closestDistance;
		if (hoveredVertexIndex.isNone() && selectedVertexIndex.isNone())
		{
			closestDistance = 0.3f;
			addVertexPercent = -1f;
			if (isEditingEnabled)
			{
				findClosestEdgeForPolygon(polygon.vertices, -1);
			}
			for (int i = 0; i < polygon.holes.Count; i++)
			{
				findClosestEdgeForPolygon(polygon.holes[i].vertices, i);
			}
		}
		void findClosestEdgeForPolygon(List<Vector2> polygonVertices, int holeIndex)
		{
			int count = polygonVertices.Count;
			for (int j = 0; j < count; j++)
			{
				Vector3 vector = hitPoint;
				Vector3 vector2 = polygon.polygonToWorldPosition(polygonVertices[j]);
				Vector3 vector3 = polygon.polygonToWorldPosition(polygonVertices[(j + 1) % count]);
				float num = Maths.vectorProjectionPercent(vector - vector2, vector3 - vector2);
				if (!(num < 0f) && !(num > 1f))
				{
					float num2 = Maths.distanceFromPointToSegment(vector, vector2, vector3);
					if (!(num2 > closestDistance))
					{
						closestDistance = num2;
						addVertexIndexA.hole = holeIndex;
						addVertexIndexB.hole = holeIndex;
						addVertexIndexA.vertex = j;
						addVertexIndexB.vertex = (j + 1) % count;
						addVertexPercent = num;
						if (isAddVertexPressed)
						{
							vertexPreview.gameObject.SetActive(value: true);
						}
						vertexPreview.position = vector2 + (vector3 - vector2) * num;
					}
				}
			}
		}
	}

	private void updateVertexDeletion()
	{
		if (Input.GetMouseButtonDown(2) && !hoveredVertexIndex.isNone() && (hoveredVertexIndex.hole != -1 || polygon.vertices.Count > 3))
		{
			List<Vector2> previousVertices = polygon.copyVertices();
			List<Hole> previousHoles = polygon.copyHoles();
			List<Vector2> polygonForVertex = getPolygonForVertex(hoveredVertexIndex);
			if (hoveredVertexIndex.hole >= 0 && Input.GetKey(KeyCode.LeftAlt))
			{
				polygonForVertex.Clear();
			}
			else
			{
				polygonForVertex.RemoveAt(hoveredVertexIndex.vertex);
			}
			if (polygonForVertex.Count < 3)
			{
				polygon.holes.RemoveAt(hoveredVertexIndex.hole);
			}
			onPolygonChanged?.Invoke(polygon, previousVertices, previousHoles);
		}
	}

	private void updateFloorEdgePress()
	{
		if (Input.GetMouseButtonDown(2) && polygon is Floor floor && hoveredVertexIndex.isNone() && selectedVertexIndex.isNone() && selectedEdgeVertexA.isNone() && addVertexIndexA.hole == -1 && !(addVertexPercent < 0f))
		{
			onFloorEdgePressed?.Invoke(floor, addVertexIndexA.vertex);
		}
	}
}
