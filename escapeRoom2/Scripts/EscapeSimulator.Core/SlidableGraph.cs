using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Pine/SlidableGraph")]
public class SlidableGraph : MonoBehaviour, ISaveable
{
	public delegate bool IsEdgeBlockedPredicate(GameObject node, GameObject neighbourNode, SlidableGraphPiece piece);

	[Serializable]
	public class Edge
	{
		[Tooltip("Node that defines edge's starting point.")]
		public GameObject startNode;

		[Tooltip("Node that defines edge's ending point.")]
		public GameObject endNode;

		[Tooltip("If true, you can modify control points to create a Bézier curved path between start and end nodes.")]
		[DontSave]
		public bool isCurved;

		[Tooltip("If true, piece moved on the path will be rotated to match curve's tangent.")]
		[DontSave]
		public bool isTangent;

		[Tooltip("Position relative to the start node that defines the control point of the Bézier curve.")]
		[DontSave]
		public Vector3 startControlLocalPosition;

		[Tooltip("Position relative to the end node that defines the control point of the Bézier curve.")]
		[DontSave]
		public Vector3 endControlLocalPosition;

		public Vector3 startPoint => startNode.transform.position;

		public Vector3 endPoint => endNode.transform.position;

		public Vector3 direction => endPoint - startPoint;

		public Vector3 startControlPosition => startNode.transform.TransformPoint(startControlLocalPosition);

		public Vector3 endControlPosition => endNode.transform.TransformPoint(endControlLocalPosition);

		public Vector3 getPoint(float t, bool isInverted = false)
		{
			Vector3 vector = (isInverted ? endPoint : startPoint);
			Vector3 vector2 = (isInverted ? startPoint : endPoint);
			if (!isCurved)
			{
				return vector + Mathf.Clamp01(t) * (vector2 - vector);
			}
			Vector3 p = (isInverted ? endControlPosition : startControlPosition);
			Vector3 p2 = (isInverted ? startControlPosition : endControlPosition);
			return Maths.cubicBezier(vector, p, p2, vector2, t);
		}

		public Vector3 getTangent(float t)
		{
			if (!isCurved)
			{
				return direction;
			}
			Vector3 p = startPoint;
			Vector3 p2 = startControlPosition;
			Vector3 p3 = endControlPosition;
			Vector3 p4 = endPoint;
			return Maths.cubicBezierDerivative(p, p2, p3, p4, t);
		}

		public override string ToString()
		{
			return startNode.name + " - " + endNode.name;
		}
	}

	public readonly struct ToNode
	{
		public readonly SlidableGraph graph;

		public readonly SlidableGraphPiece piece;

		public readonly Edge fromEdge;

		public readonly GameObject toNode;

		public ToNode(SlidableGraph graph, SlidableGraphPiece piece, Edge fromEdge, GameObject toNode)
		{
			this.graph = graph;
			this.piece = piece;
			this.fromEdge = fromEdge;
			this.toNode = toNode;
		}
	}

	public readonly struct ToEdge
	{
		public readonly SlidableGraph graph;

		public readonly SlidableGraphPiece piece;

		public readonly GameObject fromNode;

		public readonly Edge toEdge;

		public ToEdge(SlidableGraph graph, SlidableGraphPiece piece, GameObject fromNode, Edge toEdge)
		{
			this.graph = graph;
			this.piece = piece;
			this.fromNode = fromNode;
			this.toEdge = toEdge;
		}
	}

	public readonly struct OnEdge
	{
		public readonly SlidableGraph graph;

		public readonly SlidableGraphPiece piece;

		public readonly Edge edge;

		public readonly float percent;

		public OnEdge(SlidableGraph graph, SlidableGraphPiece piece, Edge edge, float percent)
		{
			this.graph = graph;
			this.piece = piece;
			this.edge = edge;
			this.percent = percent;
		}
	}

	public readonly struct OnPieceInteraction
	{
		public readonly SlidableGraph graph;

		public readonly SlidableGraphPiece piece;

		public OnPieceInteraction(SlidableGraph graph, SlidableGraphPiece piece)
		{
			this.graph = graph;
			this.piece = piece;
		}
	}

	public enum Event : byte
	{
		MovedOnEdge = 0,
		ArrivedToNode = 1,
		ArrivedToEdge = 2,
		PieceReleased = 3
	}

	[Tooltip("Objects that can be moved by the player.")]
	[DontSave]
	public List<SlidableGraphPiece> piecesList = new List<SlidableGraphPiece>();

	[Tooltip("All of the nodes that this graph consists of.")]
	[DontSave]
	public List<GameObject> nodes = new List<GameObject>();

	[Tooltip("All of the edges that this graph consists of.")]
	[DontSave]
	public List<Edge> edges = new List<Edge>();

	[Header("Graph configuration")]
	[Tooltip("If true, edge direction matters, otherwise it does not.")]
	[DontSave]
	public bool isDirectedGraph;

	[Tooltip("If true, each node that has a piece on it will disallow moving pieces on neighbouring edges.")]
	[DontSave]
	public bool occupiedNodeBlocksEdges;

	[Tooltip("If piece is within this distance of a node, it is considered to be in that node.")]
	[Min(0f)]
	[DontSave]
	public float nodeRadius = 0.02f;

	[Header("Snapping configuration")]
	[Tooltip("If true, piece will be moved to the nearest node on release, otherwise it won't move on release.")]
	[DontSave]
	public bool useSnapping;

	[Tooltip("Snap animation duration in seconds. If zero, snapping will not be animated.")]
	[Min(0f)]
	[DontSave]
	public float snapAnimationDuration = 0.1f;

	[Tooltip("Maps each start and end node pair to corresponding graph edge.")]
	[DontSave]
	private readonly Dictionary<(GameObject startNode, GameObject endNode), Edge> nodesToEdge = new Dictionary<(GameObject, GameObject), Edge>();

	[Tooltip("Maps each node to list of nodes that are connected to it (neighbouring nodes).")]
	[DontSave]
	private readonly Dictionary<GameObject, List<GameObject>> nodeToNeighbours = new Dictionary<GameObject, List<GameObject>>();

	[Header("Collision configuration")]
	[Tooltip("If true, pieces will not be able to pass through other pieces, otherwise they will.")]
	[DontSave]
	public bool useCollision = true;

	[Tooltip("Sound played when interaction with this SlidableGraph starts.")]
	[DontSave]
	[HideInInspector]
	public EventReference startSound;

	[Tooltip("Sound played when piece of this SlidableGraph is being moved/dragged.")]
	[DontSave]
	[HideInInspector]
	public EventReference moveSound;

	[Tooltip("Sound played when interaction with this SlidableGraph ends.")]
	[DontSave]
	[HideInInspector]
	public EventReference endSound;

	[Tooltip("Move sound will be stopped if no movement is detected for this many frames. If negative, it will always be played while holding.")]
	[DontSave]
	[HideInInspector]
	public int idleFramesToStopMoveSound = 10;

	[Header("Other configuration")]
	[Tooltip("If true, player can rotate camera while interacting with this SlidableGraph.")]
	[DontSave]
	public bool isFreeCamera;

	[Tooltip("Controller specific setting that defines how fast pieces of this SlidableGraph are moving.")]
	[Min(0f)]
	[DontSave]
	public float controllerSpeedMultiplier = 1f;

	[Tooltip("VR specific setting that defines how fast pieces of this SlidableGraph are moving.")]
	[Min(0f)]
	[DontSave]
	public float vrSpeedMultiplier = 1f;

	[Tooltip("Camera which will be used for drawing gizmos in play-mode.")]
	[DontSave]
	public Camera gizmoCamera;

	[DontSave]
	public GameObject soundEmitterParentOverride;

	internal const float COLLISION_BUFFER = 0.001f;

	internal const float FREE_CAMERA_MAX_OFFSET = 0.05f;

	[NonSerialized]
	[DontSave]
	internal Vector3 nodeScreenMovement;

	[NonSerialized]
	[DontSave]
	internal Vector3 lastSyncedPosition;

	[NonSerialized]
	[DontSave]
	internal int idleFrames;

	[DontSave]
	public IsEdgeBlockedPredicate isEdgeBlockedPredicate;

	[Tooltip("Emitter that is responsible for playing sounds of this SlidableGraph instance.")]
	public StudioEventEmitter soundEmitter { get; private set; }

	public void init(Camera gizmoCamera)
	{
		initSoundEmitter();
		initGizmoCamera(gizmoCamera);
		initPieces();
		buildEdgeGraph();
		buildNodeGraph();
	}

	private void initSoundEmitter()
	{
		GameObject gameObject = ((soundEmitterParentOverride == null) ? base.gameObject : soundEmitterParentOverride);
		soundEmitter = PineFmod.addSoundEmitter(gameObject, moveSound);
	}

	private void initGizmoCamera(Camera gizmoCamera)
	{
		if (!(this.gizmoCamera != null))
		{
			this.gizmoCamera = gizmoCamera;
		}
	}

	[ContextMenu("initPieces")]
	public void initPieces()
	{
		Interactive.linkInteractives(new List<Interactive>(piecesList).ToArray());
		foreach (SlidableGraphPiece pieces in piecesList)
		{
			pieces.graph = this;
			if (!tryFindCorrespondingEdge(pieces, isLinearPosition: false, out var correspondingEdge))
			{
				Debug.LogError("Piece '" + pieces.name + "' is not on any edge (graph: " + base.name + ")!", pieces);
				continue;
			}
			pieces.currentEdgeStartNode = (pieces.initialEdgeStartNode = correspondingEdge.startNode);
			pieces.currentEdgeEndNode = (pieces.initialEdgeEndNode = correspondingEdge.endNode);
			if (!correspondingEdge.isCurved)
			{
				pieces.currentEdgePercent = (pieces.initialEdgePercent = UnityUtils.inverseLerp(correspondingEdge.startPoint, correspondingEdge.endPoint, pieces.transform.position));
				continue;
			}
			Vector3 startPoint = correspondingEdge.startPoint;
			Vector3 startControlPosition = correspondingEdge.startControlPosition;
			Vector3 endControlPosition = correspondingEdge.endControlPosition;
			Vector3 endPoint = correspondingEdge.endPoint;
			pieces.currentEdgePercent = (pieces.initialEdgePercent = Maths.closestCubicBezierPercent(startPoint, startControlPosition, endControlPosition, endPoint, pieces.transform.position));
		}
	}

	private bool tryFindCorrespondingEdge(SlidableGraphPiece piece, bool isLinearPosition, out Edge correspondingEdge)
	{
		float num = 0.01f;
		Edge edge = null;
		foreach (Edge edge2 in edges)
		{
			float num2;
			if (!isLinearPosition && edge2.isCurved)
			{
				Vector3 startPoint = edge2.startPoint;
				Vector3 startControlPosition = edge2.startControlPosition;
				Vector3 endControlPosition = edge2.endControlPosition;
				Vector3 endPoint = edge2.endPoint;
				float t = Maths.closestCubicBezierPercent(startPoint, startControlPosition, endControlPosition, endPoint, piece.transform.position);
				Vector3 b = Maths.cubicBezier(startPoint, startControlPosition, endControlPosition, endPoint, t);
				num2 = Vector3.Distance(piece.transform.position, b);
			}
			else
			{
				Vector3 vector = edge2.endPoint - edge2.startPoint;
				Vector3 vector2 = (isLinearPosition ? piece.currentLinearPosition : piece.transform.position) - edge2.startPoint;
				float num3 = Maths.vectorProjectionPercent(vector2, vector);
				if (num3 < -0.01f || num3 > 1.01f)
				{
					continue;
				}
				num2 = Vector3.Cross(vector2, vector).magnitude / vector.magnitude;
			}
			if (num2 < num)
			{
				num = num2;
				edge = edge2;
			}
		}
		correspondingEdge = edge;
		return correspondingEdge != null;
	}

	private bool tryFindCorrespondingNode(SlidableGraphPiece piece, bool isLinearPosition, out GameObject correspondingNode)
	{
		float num = float.PositiveInfinity;
		correspondingNode = null;
		foreach (GameObject node in nodes)
		{
			float num2 = Vector3.Distance(isLinearPosition ? piece.currentLinearPosition : piece.transform.position, node.transform.position);
			if (!(num2 >= num))
			{
				num = num2;
				correspondingNode = node;
			}
		}
		return num < nodeRadius;
	}

	private void buildEdgeGraph()
	{
		for (int i = 0; i < edges.Count; i++)
		{
			Edge edge = edges[i];
			(GameObject, GameObject) key = (edge.startNode, edge.endNode);
			if (!nodesToEdge.TryAdd(key, edge))
			{
				Debug.LogError($"Edge {edge} at index {i} is duplicate", this);
			}
		}
		if (isDirectedGraph)
		{
			return;
		}
		foreach (Edge edge2 in edges)
		{
			(GameObject, GameObject) key2 = (edge2.endNode, edge2.startNode);
			if (nodesToEdge.TryGetValue(key2, out var value))
			{
				Debug.LogError($"Reversed edge {value} is already contained in the edge list", this);
				continue;
			}
			nodesToEdge[key2] = new Edge
			{
				startNode = edge2.endNode,
				endNode = edge2.startNode
			};
		}
	}

	private void buildNodeGraph()
	{
		nodeToNeighbours.Clear();
		foreach (Edge edge in edges)
		{
			addNeighbour(edge.startNode, edge.endNode);
			if (!isDirectedGraph)
			{
				addNeighbour(edge.endNode, edge.startNode);
			}
		}
	}

	private void addNeighbour(GameObject toNode, GameObject neighbourNode)
	{
		if (!nodeToNeighbours.TryGetValue(toNode, out var value))
		{
			value = new List<GameObject>();
			nodeToNeighbours[toNode] = value;
		}
		value.Add(neighbourNode);
	}

	public bool tryGetPiece(GameObject node, out SlidableGraphPiece piece)
	{
		foreach (SlidableGraphPiece pieces in piecesList)
		{
			if (Vector3.Distance(pieces.currentLinearPosition, node.transform.position) < nodeRadius)
			{
				piece = pieces;
				return true;
			}
		}
		piece = null;
		return false;
	}

	public bool tryGetNeighbours(GameObject node, out List<GameObject> neighbours)
	{
		return nodeToNeighbours.TryGetValue(node, out neighbours);
	}

	public bool tryGetNode(SlidableGraphPiece piece, out GameObject node)
	{
		return tryFindCorrespondingNode(piece, isLinearPosition: true, out node);
	}

	public Edge getEdge(SlidableGraphPiece piece)
	{
		if (!tryGetEdge(piece, out var edge))
		{
			return null;
		}
		return edge;
	}

	public bool tryGetEdge(SlidableGraphPiece piece, out Edge edge)
	{
		return tryFindCorrespondingEdge(piece, isLinearPosition: true, out edge);
	}

	public Edge getEdge(GameObject startNode, GameObject endNode)
	{
		if (!tryGetEdge(startNode, endNode, out var edge))
		{
			return null;
		}
		return edge;
	}

	public bool tryGetEdge(GameObject startNode, GameObject endNode, out Edge edge)
	{
		return nodesToEdge.TryGetValue((startNode, endNode), out edge);
	}

	public void reset()
	{
		foreach (SlidableGraphPiece pieces in piecesList)
		{
			pieces.currentEdgeStartNode = pieces.initialEdgeStartNode;
			pieces.currentEdgeEndNode = pieces.initialEdgeEndNode;
			pieces.currentEdgePercent = pieces.initialEdgePercent;
			pieces.transform.position = getEdge(pieces.initialEdgeStartNode, pieces.initialEdgeEndNode).getPoint(pieces.initialEdgePercent);
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
	}

	public virtual void load(FastBinaryReader reader)
	{
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
	}
}
