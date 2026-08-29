using UnityEngine;

public class SlidableSettings : ScriptableObject
{
	[Header("Gizmos - Handle")]
	public bool showHandleGizmos = true;

	public Color handleGizmoColor = new Color(0f, 0.5f, 1f, 0.75f);

	[Header("Gizmos - Nodes")]
	public bool showNodeGizmos = true;

	public Color nodeAsChildGizmoColor = new Color(0f, 0.5f, 1f, 0.75f);

	public Color nodeAsSiblingGizmoColor = new Color(1f, 0.5f, 0f, 0.75f);

	[Range(0f, 0.1f)]
	public float nodeGizmoRadius = 0.02f;

	[Header("Gizmos - Edge")]
	public bool showEdgeGizmos = true;

	public Color edgeGizmoColor = new Color(1f, 1f, 1f, 0.75f);

	[Range(0f, 100f)]
	public float lineThickness = 10f;

	[Header("Gizmos - Snap points")]
	public bool showSnapPointGizmos = true;

	public Color snapPointGizmoColor = new Color(1f, 0.5f, 0f, 0.75f);

	[Range(0f, 0.1f)]
	public float snapPointGizmoRadius = 0.01f;

	[Header("Gizmos - Snap point indices")]
	public bool showSnapPointIndices = true;

	public int snapPointIndicesFontSize = 20;

	public float snapPointIndicesHeightOffset = 0.08f;

	[Header("Gizmos - Draw mode")]
	public bool drawGizmosOnlyIfSelected = true;
}
