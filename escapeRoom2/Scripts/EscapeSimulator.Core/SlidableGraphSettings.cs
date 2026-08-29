using UnityEngine;

public class SlidableGraphSettings : ScriptableObject
{
	[Header("Gizmos - Pieces")]
	public bool showPieceGizmos = true;

	public bool showPieceColliders = true;

	public Color pieceGizmoColor = new Color(1f, 0f, 1f, 0.75f);

	public Color pieceColliderColor = new Color(0f, 0.75f, 0f, 0.75f);

	[Header("Gizmos - Nodes")]
	public bool showNodeGizmos = true;

	public Color nodeRegularGizmoColor = new Color(0f, 0.5f, 1f, 0.75f);

	public Color nodeSelectedGizmoColor = new Color(1f, 0f, 1f, 0.75f);

	[Header("Gizmos - Edges")]
	public bool showEdgeGizmos = true;

	[Range(0f, 100f)]
	public float lineThickness;

	[Range(0f, 0.5f)]
	public float edgeEndLength = 0.25f;

	public Color edgeLineGizmoColor = new Color(1f, 1f, 1f, 0.75f);

	public Color edgeEndGizmoColor = new Color(0f, 0.5f, 1f, 0.75f);

	[Header("Gizmos - Piece indices")]
	public bool showPieceIndices = true;

	public int pieceIndicesFontSize = 20;

	[Header("Gizmos - Node indices")]
	public bool showNodeIndices = true;

	public int nodeIndicesFontSize = 20;

	[Header("Gizmos - Edge indices")]
	public bool showEdgeIndices = true;

	public int edgeIndicesFontSize = 20;
}
