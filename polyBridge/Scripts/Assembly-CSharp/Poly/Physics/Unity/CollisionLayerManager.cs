using System.Collections.Generic;
using System.Linq;
using Poly.Base;

namespace Poly.Physics.Unity
{
	public class CollisionLayerManager : ListenerBase, IShapeListener, INodeListener, IWorldListener
	{
		private HashSet<NodeHandle> splittableHandles = new HashSet<NodeHandle>();

		private List<ShapeHandleIndex> newShapesAdded = new List<ShapeHandleIndex>();

		private List<ShapeHandleIndex> newShapesModified = new List<ShapeHandleIndex>();

		private bool anyShapesRemoved;

		public NodeHandle[] splittableNodesCopy => splittableHandles.ToArray();

		private void Awake()
		{
		}

		private new void OnEnable()
		{
			base.OnEnable();
			SingletonBehaviour<World>.instance.layerManager = this;
		}

		private new void OnDisable()
		{
			base.OnDisable();
			if ((bool)SingletonBehaviour<World>.instance)
			{
				SingletonBehaviour<World>.instance.layerManager = null;
			}
		}

		public void Clear()
		{
			splittableHandles.Clear();
		}

		public void RegisterSplittableNode(NodeHandle n)
		{
			splittableHandles.Add(n);
		}

		public void UpdateLayerForShapes(List<ShapeHandleIndex> shapes)
		{
			foreach (ShapeHandleIndex shape in shapes)
			{
				ref ShapeHandle reference = ref shape.Get();
				if (reference.entityHandle is NodeHandle)
				{
					NodeHandle node = reference.entityHandle as NodeHandle;
					reference.layer = CalcNodeLayer(node, reference.layer);
				}
				else if (reference.entityHandle is EdgeHandle)
				{
					EdgeHandle edge = reference.entityHandle as EdgeHandle;
					reference.layer = CalcEdgeLayer(edge, reference.layer);
				}
			}
		}

		public void OnShapeAdded(ShapeHandleIndex s)
		{
			newShapesAdded.Add(s);
		}

		public void OnShapeModified(ShapeHandleIndex s)
		{
			newShapesModified.Add(s);
		}

		public void OnShapeRemoved(ShapeHandleIndex s)
		{
			anyShapesRemoved = true;
		}

		public void OnNodeAdded(NodeHandle n)
		{
		}

		public void OnNodeRemoved(NodeHandle n)
		{
			splittableHandles.Remove(n);
		}

		public void BeforeStep()
		{
			if (newShapesAdded.Count > 0 || newShapesModified.Count > 0)
			{
				UpdateLayerForShapes(newShapesAdded);
				newShapesAdded.Clear();
				UpdateLayerForShapes(newShapesModified);
				newShapesModified.Clear();
			}
			anyShapesRemoved = false;
		}

		public void AfterWorldCleared()
		{
			anyShapesRemoved = false;
		}

		public void AfterWorldFrameUpdate()
		{
		}

		public void AfterWorldFixedUpdate()
		{
		}

		private Layer CalcNodeLayer(NodeHandle node, Layer currentLayer)
		{
			Layer layer = currentLayer;
			if ((int)layer >= 9 && (int)layer < 17)
			{
				if (node.isAnchor)
				{
					layer = Layer.AnchorNode;
				}
				else
				{
					if (splittableHandles.Contains(node))
					{
						layer = Layer.SplitNode;
						foreach (EdgeHandle edge in node.edges)
						{
							if (edge.material.enableCollision)
							{
								layer = Layer.SplitRoadNode;
								break;
							}
						}
					}
					else
					{
						layer = Layer.NonRoadNode;
						foreach (EdgeHandle edge2 in node.edges)
						{
							if (edge2.material.enableCollision)
							{
								layer = Layer.RoadNode;
								break;
							}
						}
					}
					bool flag = false;
					foreach (EdgeHandle edge3 in node.edges)
					{
						if (!edge3.material.isDebris && !edge3.material.isPin)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						if (layer == Layer.NonRoadNode || layer == Layer.SplitNode)
						{
							layer = Layer.DebrisNonRoadNode;
						}
						if (layer == Layer.RoadNode || layer == Layer.SplitRoadNode)
						{
							layer = Layer.DebrisRoadNode;
						}
					}
				}
			}
			else if (layer != Layer.DebrisRoadNode)
			{
				_ = 18;
			}
			return layer;
		}

		private Layer CalcEdgeLayer(EdgeHandle edge, Layer currentLayer)
		{
			Layer layer = currentLayer;
			switch (layer)
			{
			case Layer.Bridge:
			case Layer.RoadEdge:
			case Layer.RoadEdgeConnectedToSplitNode:
				layer = ((splittableHandles.Contains(edge.node0) || splittableHandles.Contains(edge.node1)) ? Layer.RoadEdgeConnectedToSplitNode : Layer.RoadEdge);
				break;
			case Layer.DebrisRoadEdge:
				if (splittableHandles.Contains(edge.node0) || splittableHandles.Contains(edge.node1))
				{
					layer = Layer.CollideNothing;
				}
				break;
			}
			return layer;
		}
	}
}
