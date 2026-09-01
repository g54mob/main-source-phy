using System;
using System.Collections.Generic;
using System.Linq;
using Poly.Base;
using Poly.Game.Segmentation;
using Poly.Physics;
using UnityEngine;

namespace Poly.Game
{
	public class EdgeLinkScoreViewer : ListenerBase, IWorldListener, IHydraulicListener, IEdgeBreakListener
	{
		public enum Mode
		{
			NodeLinksLocal = 0,
			Cluster = 1,
			DynamicAngleMonitoring = 2
		}

		public const bool trackAnglesForNonRoads = false;

		public Action<string> descriptionUpdate;

		[Header("Display settings")]
		public bool showGui;

		public Font font;

		[Range(1f, 24f)]
		public int fontSize = 24;

		public Color backgroundColor = new Color32(34, 140, 34, 77);

		[Range(-1f, 1f)]
		public float labelVerticalOffset = 0.3f;

		[Range(0.5f, 2f)]
		public float desiredLabelWidthInMeters = 1f;

		[NonSerialized]
		public bool roadLinkToAnchorBoostStrength;

		[NonSerialized]
		public float maxStrengthDeductionFraction = 0.4f;

		[NonSerialized]
		public float fullStrengthLength = 4f;

		private List<EdgeHandle> nerfedEdges = new List<EdgeHandle>();

		private List<EdgeHandle> roads = new List<EdgeHandle>();

		private Dictionary<WorldObjectImpl, float> data = new Dictionary<WorldObjectImpl, float>();

		private bool forceReset;

		private const float anchorValue = 10000f;

		[Space(10f)]
		public Mode mode;

		[Header("Clusters idea")]
		public BridgeSegmentation segmentation = new BridgeSegmentation();

		[Header("Dynamic angle tracking")]
		public EdgeLinkAngleMonitor roadLinkAngleMonitor = new EdgeLinkAngleMonitor();

		[Header("New Cluster Debug")]
		public bool debugManual_forceImproveCalculation;

		public bool allowPromotion = true;

		public bool allowSprings = true;

		public bool allowRopes = true;

		public string description
		{
			get
			{
				if (mode != Mode.Cluster)
				{
					return null;
				}
				return "";
			}
		}

		private void RemoveInfoForRoadsWithNewNodes()
		{
			if (mode != Mode.NodeLinksLocal && mode != Mode.DynamicAngleMonitoring)
			{
				return;
			}
			int num = roads.Count - 1;
			while (0 <= num)
			{
				EdgeHandle edgeHandle = roads[num];
				if (!data.ContainsKey(edgeHandle.node0) || !data.ContainsKey(edgeHandle.node1))
				{
					roads.RemoveAt(num);
				}
				num--;
			}
		}

		private void DrawStrengthLabelsForRoadsAndRoadNodes()
		{
			DrawGuiTextUtil.InitGuiStyle(font, backgroundColor, fontSize);
			foreach (EdgeHandle road in roads)
			{
				Vec2 vec = 0.5f * (road.node0.pos + road.node1.pos) + labelVerticalOffset * Vec2.up;
				float num = data[road.node0];
				float num2 = data[road.node1];
				bool num3 = 10000f <= num;
				bool flag = 10000f <= num2;
				if (num3)
				{
					num -= 10000f;
				}
				if (flag)
				{
					num2 -= 10000f;
				}
				int num4 = (num3 ? 100 : Mathf.RoundToInt(Mathf.Min(100f, num / fullStrengthLength * 100f)));
				int num5 = (flag ? 100 : Mathf.RoundToInt(Mathf.Min(100f, num2 / fullStrengthLength * 100f)));
				int num6 = Mathf.RoundToInt((1f - maxStrengthDeductionFraction) * 100f + maxStrengthDeductionFraction * ((float)num4 / 2f + (float)num5 / 2f));
				DrawGuiTextUtil.DisplayGuiLabel_Slow(text: $"str: {num6:0}", posInWorld: vec - 1.5f * Vector3.forward, desiredLabelWidthInMeters: desiredLabelWidthInMeters);
			}
			HashSet<NodeHandle> roadNodes = new HashSet<NodeHandle>();
			roads.ForEach(delegate(EdgeHandle r)
			{
				roadNodes.AddTwo(in r.node0, in r.node1);
			});
			foreach (NodeHandle item in roadNodes)
			{
				Vec2 vec2 = item.pos + labelVerticalOffset * Vec2.up;
				float num7 = data[item];
				bool flag2 = 10000f <= num7;
				if (flag2)
				{
					num7 -= 10000f;
				}
				DrawGuiTextUtil.DisplayGuiLabel_Slow(text: string.Format("len: {0:0.0} {2}\r\nstr: {1:0}%", num7, flag2 ? 100 : Mathf.RoundToInt(Mathf.Min(100f, num7 / fullStrengthLength * 100f)), flag2 ? "(A)" : ""), posInWorld: vec2 - 1.5f * Vector3.forward, desiredLabelWidthInMeters: desiredLabelWidthInMeters);
			}
		}

		public void Reset()
		{
			forceReset = true;
		}

		public void CalcNodeStrengths()
		{
			World instance = SingletonBehaviour<World>.instance;
			roads = instance.edgeHandles.FindAll((EdgeHandle e) => e.isDynamic && e.material.enableCollision);
			data.Clear();
			foreach (EdgeHandle road in roads)
			{
				road.shapeHandleIndex.Get();
			}
			HashSet<NodeHandle> roadNodes = new HashSet<NodeHandle>();
			roads.ForEach(delegate(EdgeHandle r)
			{
				roadNodes.AddTwo(in r.node0, in r.node1);
			});
			List<EdgeHandle> list = new List<EdgeHandle>();
			HashSet<EdgeHandle> neighbors2nd = new HashSet<EdgeHandle>();
			foreach (NodeHandle item in roadNodes)
			{
				list.AddRange(item.edges.Where((EdgeHandle e) => e.isDynamic && !e.material.enableCollision));
				list.ForEach(delegate(EdgeHandle e1)
				{
					neighbors2nd.AddRange(e1.node0.edges.Where((EdgeHandle edgeHandle) => edgeHandle.isDynamic && !edgeHandle.material.enableCollision));
				});
				list.ForEach(delegate(EdgeHandle e1)
				{
					neighbors2nd.AddRange(e1.node1.edges.Where((EdgeHandle edgeHandle) => edgeHandle.isDynamic && !edgeHandle.material.enableCollision));
				});
				neighbors2nd.RemoveRange(list);
				float num = list.Sum((EdgeHandle e) => e.length) + neighbors2nd.Sum((EdgeHandle e) => e.length);
				bool isKinematic = item.isKinematic;
				bool flag = item.edges.Any((EdgeHandle e) => (roadLinkToAnchorBoostStrength || !e.material.enableCollision) && e.isDynamic && (e.node0.isKinematic || e.node1.isKinematic));
				if (isKinematic || flag)
				{
					num += 10000f;
				}
				data.Add(item, num);
				list.Clear();
				neighbors2nd.Clear();
			}
		}

		private void CalcRoadStrengths()
		{
			foreach (EdgeHandle road in roads)
			{
				if (data.ContainsKey(road.node0) && data.ContainsKey(road.node1))
				{
					float num = data[road.node0];
					float num2 = data[road.node1];
					bool num3 = 10000f <= num;
					bool flag = 10000f <= num2;
					if (num3)
					{
						num -= 10000f;
					}
					if (flag)
					{
						num2 -= 10000f;
					}
					int num4 = (num3 ? 100 : Mathf.RoundToInt(Mathf.Min(100f, num / fullStrengthLength * 100f)));
					int num5 = (flag ? 100 : Mathf.RoundToInt(Mathf.Min(100f, num2 / fullStrengthLength * 100f)));
					int num6 = Mathf.RoundToInt((1f - maxStrengthDeductionFraction) * 100f + maxStrengthDeductionFraction * ((float)num4 / 2f + (float)num5 / 2f));
					data[road] = (float)num6 / 100f;
				}
			}
		}

		private void AssignRoadStrengths()
		{
			foreach (EdgeHandle road in roads)
			{
				if (data.ContainsKey(road))
				{
					road.maxForce_ActualFraction = data[road];
				}
			}
		}

		public virtual void BeforeStep()
		{
			if (forceReset)
			{
				forceReset = false;
				roads.Clear();
				nerfedEdges.Clear();
				data.Clear();
				roadLinkAngleMonitor?.StoreSimulationAngleData_AndClear();
			}
			if (roads.Count == 0)
			{
				switch (mode)
				{
				case Mode.NodeLinksLocal:
					CalcNodeStrengths();
					CalcRoadStrengths();
					AssignRoadStrengths();
					break;
				case Mode.Cluster:
					BridgeSegmentation.allowPromotion = allowPromotion;
					BridgeSegmentation.allowSprings = allowSprings;
					BridgeSegmentation.allowRopes = allowRopes;
					segmentation.Clear();
					segmentation.CalcOrImproveSegmentation();
					debugManual_forceImproveCalculation = false;
					roads = SingletonBehaviour<World>.instance.edgeHandles.FindAll((EdgeHandle e) => e.isDynamic && e.material.enableCollision);
					break;
				case Mode.DynamicAngleMonitoring:
					CalcNodeStrengths();
					CalcRoadStrengths();
					if (roadLinkAngleMonitor.combineWithMethodOne)
					{
						AssignRoadStrengths();
					}
					BridgeSegmentation.allowPromotion = allowPromotion;
					BridgeSegmentation.allowSprings = allowSprings;
					BridgeSegmentation.allowRopes = allowRopes;
					segmentation.Clear();
					segmentation.CalcOrImproveSegmentation();
					debugManual_forceImproveCalculation = false;
					roadLinkAngleMonitor.Clear(clearHistory: false);
					if (!roadLinkAngleMonitor.isInitialized || roadLinkAngleMonitor.midNodes.Length == 0)
					{
						roadLinkAngleMonitor.Init(data, segmentation.mergedNodes, segmentation.rigidChunks);
					}
					break;
				}
				if (descriptionUpdate != null)
				{
					descriptionUpdate(description);
				}
			}
			if (mode == Mode.Cluster && debugManual_forceImproveCalculation)
			{
				debugManual_forceImproveCalculation = false;
				BridgeSegmentation.allowPromotion = allowPromotion;
				BridgeSegmentation.allowSprings = allowSprings;
				BridgeSegmentation.allowRopes = allowRopes;
				segmentation.CalcOrImproveSegmentation();
			}
			if (mode == Mode.DynamicAngleMonitoring)
			{
				roadLinkAngleMonitor.UpdateStep(SingletonBehaviour<World>.instance.settings.frameDeltaTime, segmentation);
				roadLinkAngleMonitor.ApplyStrenghts();
			}
		}

		public virtual void AfterWorldFrameUpdate()
		{
		}

		public virtual void AfterWorldFixedUpdate()
		{
		}

		public virtual void AfterWorldCleared()
		{
			roads.Clear();
			nerfedEdges.Clear();
			data.Clear();
			segmentation?.Clear();
			roadLinkAngleMonitor?.Clear();
		}

		public virtual void OnNodeSplit(Node originalNode, Node additionalNewNode)
		{
		}

		public virtual void OnNodeJoint__NeverTriggered(Node nodeAboutToBeRemoved, Node existingJointNode)
		{
		}

		public virtual void OnEdgeReattached(Edge edge, Node oldEndpoint, Node newEndpoint)
		{
		}

		public virtual void OnPhaseStart()
		{
			roads.Clear();
			nerfedEdges.Clear();
			data.Clear();
			roadLinkAngleMonitor?.StoreSimulationAngleData_AndClear();
		}

		public virtual void OnPhaseComplete(Node[] mergedNodes_duringLastPhaseOnly)
		{
			roads.Clear();
			nerfedEdges.Clear();
			data.Clear();
			roadLinkAngleMonitor?.StoreSimulationAngleData_AndClear();
		}

		public virtual void OnNodesMergedEarly(Node a, Node b)
		{
		}

		public virtual void ClearAndReset()
		{
		}

		public virtual bool OnEdgeBroken(EdgeHandle e)
		{
			roadLinkAngleMonitor?.OnEdgeBroken(e);
			return true;
		}
	}
}
