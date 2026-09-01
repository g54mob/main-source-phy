using System;
using System.Collections.Generic;
using System.Linq;
using Pb;
using Poly.Base;
using Poly.Game.Segmentation;
using Poly.Physics;
using UnityEngine;

namespace Poly.Game
{
	public class EdgeLinkAngleMonitor
	{
		public struct MidNode
		{
			public float nodeConnectivityFactorA;

			public float nodeConnectivityFactorB;

			public float connectivityWeightA;

			public float connectivityWeightB;

			public float lastAngle;

			public float totalAngleTraveled;

			public float numDirectionChangesRaw;

			public float flipCooldownFreezeLeft;

			public float min;

			public float max;

			public float currentDir;

			public bool notNerfed;

			public static float threshold = MathF.PI / 180f;

			private uint _key;

			public MergedNode node { get; private set; }

			public EdgeHandle edgeA { get; private set; }

			public EdgeHandle edgeB { get; private set; }

			public bool hasBrokenEdge => 0f == nodeConnectivityFactorA;

			public bool isValid
			{
				get
				{
					bool flag = node.nodes.Contains(edgeA.node1);
					bool num = node.nodes.Contains(edgeA.node0);
					bool flag2 = node.nodes.Contains(edgeB.node1);
					bool flag3 = node.nodes.Contains(edgeB.node0);
					if (num ^ flag)
					{
						return flag3 ^ flag2;
					}
					return false;
				}
			}

			public uint key
			{
				get
				{
					if (_key != 0)
					{
						return _key;
					}
					bool flag = node.nodes.Contains(edgeA.node1);
					bool flag2 = node.nodes.Contains(edgeA.node0);
					bool flag3 = node.nodes.Contains(edgeB.node1);
					bool flag4 = node.nodes.Contains(edgeB.node0);
					bool flag5 = edgeA.worldIdx < edgeB.worldIdx;
					bool flag6 = flag5;
					if (flag && flag4)
					{
						flag6 = true;
					}
					else if (flag3 && flag2)
					{
						flag6 = false;
					}
					else if (flag && flag3)
					{
						flag6 = flag5;
					}
					else if (flag2 && flag4)
					{
						flag6 = !flag5;
					}
					uint num;
					uint worldIdx;
					if (flag6)
					{
						num = (uint)(edgeA.worldIdx << 16);
						worldIdx = (uint)edgeB.worldIdx;
					}
					else
					{
						num = (uint)(edgeB.worldIdx << 16);
						worldIdx = (uint)edgeA.worldIdx;
					}
					_key = num | worldIdx;
					return _key;
				}
			}

			public void CopyStateFrom(in MidNode other)
			{
				totalAngleTraveled = other.totalAngleTraveled;
				numDirectionChangesRaw = other.numDirectionChangesRaw;
				flipCooldownFreezeLeft = other.flipCooldownFreezeLeft;
				notNerfed = other.notNerfed;
				currentDir = 0f;
				Init();
			}

			public MidNode(MergedNode node, EdgeHandle roadA, EdgeHandle roadB, float nodeConnectivityFactorA, float nodeConnectivityFactorB, float connectivityWeightA, float connectivityWeightB)
			{
				this.node = node;
				edgeA = roadA;
				edgeB = roadB;
				this.nodeConnectivityFactorA = nodeConnectivityFactorA;
				this.nodeConnectivityFactorB = nodeConnectivityFactorB;
				this.connectivityWeightA = connectivityWeightA;
				this.connectivityWeightB = connectivityWeightB;
				lastAngle = 0f;
				totalAngleTraveled = 0f;
				numDirectionChangesRaw = 0f;
				flipCooldownFreezeLeft = 0f;
				min = (max = (currentDir = 0f));
				notNerfed = false;
				_key = 0u;
				Init();
			}

			private float CalcAngle()
			{
				Vec2 vec = edgeA.node1.pos - edgeA.node0.pos;
				Vec2 vec2 = edgeB.node1.pos - edgeB.node0.pos;
				float num = (float)System.Math.Atan2(vec.y, vec.x);
				return Pb.Mathf.WrapAngleOnceToOnePi((float)System.Math.Atan2(vec2.y, vec2.x) - num);
			}

			public void Init()
			{
				min = (max = (lastAngle = CalcAngle()));
			}

			public void UpdateDirection(float deltaTime, float cooldownRate, float flipCooldownFreezeDuration)
			{
				float num = CalcAngle();
				if (currentDir < 0f)
				{
					if (num < min)
					{
						min = num;
						max = min;
					}
					else if (max < num)
					{
						max = num;
					}
				}
				else if (0f < currentDir)
				{
					if (max < num)
					{
						max = num;
						min = max;
					}
					else if (num < min)
					{
						min = num;
					}
				}
				else
				{
					if (num < min)
					{
						min = num;
					}
					if (max < num)
					{
						max = num;
					}
				}
				flipCooldownFreezeLeft = Pb.Mathf.Max(0f, flipCooldownFreezeLeft - deltaTime);
				if (0f == flipCooldownFreezeLeft)
				{
					numDirectionChangesRaw = Pb.Mathf.Max(0f, numDirectionChangesRaw - deltaTime * cooldownRate);
				}
				if (threshold < max - min)
				{
					currentDir = ((num == max) ? 1f : (-1f));
					min = (max = num);
					numDirectionChangesRaw += 1f;
					flipCooldownFreezeLeft = flipCooldownFreezeDuration;
				}
				float f = Pb.Mathf.WrapAngleOnceToOnePi(num - lastAngle);
				totalAngleTraveled += UnityEngine.Mathf.Abs(f);
				lastAngle = num;
			}
		}

		public const bool overrideAvgEndpointNerfWithMax = false;

		public const bool suppressNerfForVerticalAndDiagonalEdgesAbove45Deg = true;

		private const float connectivityWeight_Road = 1f;

		private const float connectivityWeight_Truss = 1E-05f;

		private const float minSumWeightValue = 1E-05f;

		public bool isInitialized;

		private World world;

		public MidNode[] midNodes;

		public Dictionary<EdgeHandle, float> edgeToStrength = new Dictionary<EdgeHandle, float>();

		public Dictionary<EdgeHandle, float> baseStrengthCombined = new Dictionary<EdgeHandle, float>();

		public Dictionary<EdgeHandle, float> edgeToNerfA = new Dictionary<EdgeHandle, float>();

		public Dictionary<EdgeHandle, float> edgeToNerfB = new Dictionary<EdgeHandle, float>();

		private Dictionary<NodeHandle, MergedNode> mergedNodes = new Dictionary<NodeHandle, MergedNode>();

		private Dictionary<EdgeHandle, RigidChunk> unused_rigidChunks = new Dictionary<EdgeHandle, RigidChunk>();

		private Dictionary<MergedNode, List<EdgeHandle>> nodeToEdges = new Dictionary<MergedNode, List<EdgeHandle>>();

		private MidNode[] storedSimulationState;

		public bool mustBeNerfedOnBothSides;

		public bool combineWithMethodOne = true;

		public int numDirectionChangesOk = 5;

		public float deductionPerDirChange = 0.12f;

		public float maxDeduction = 0.36f;

		public float flipCountCooldownRate = 0.5f;

		public float flipCooldownFreezeDuration = 0.5f;

		public float desiredLabelWidthInMeters = 1f;

		private bool wasEdgeBroken { get; set; }

		public void Init(Dictionary<WorldObjectImpl, float> baseRoadStrengths, Dictionary<NodeHandle, MergedNode> mergedNodesIn, Dictionary<EdgeHandle, RigidChunk> rigidChunksIn)
		{
			if (isInitialized)
			{
				Clear();
			}
			world = SingletonBehaviour<World>.instance;
			List<EdgeHandle> nerfedEdges = world.edgeHandles.FindAll((EdgeHandle e) => e.isDynamic && !e.material.isPin && !e.material.isDebris).FindAll((EdgeHandle e) => e.material.enableCollision);
			mergedNodes = mergedNodesIn;
			unused_rigidChunks = rigidChunksIn;
			nodeToEdges = MapMergedNodesToNerfedEdges(in nerfedEdges, in mergedNodesIn);
			edgeToStrength.Clear();
			baseStrengthCombined.Clear();
			midNodes = CreateMidNodes(in nodeToEdges, edgeToStrength).ToArray();
			EdgeHandle[] array = edgeToStrength.Keys.ToArray();
			foreach (EdgeHandle key in array)
			{
				if (baseRoadStrengths.TryGetValue(key, out var value))
				{
					baseStrengthCombined[key] = value;
				}
				else
				{
					baseStrengthCombined[key] = 1f;
				}
			}
			RestoreSimulationAngleData_Once();
			isInitialized = true;
		}

		private static int edgeComparison(EdgeHandle a, EdgeHandle b)
		{
			Vec2 vec = 0.5f * (a.node0.pos + b.node1.pos);
			Vec2 other = 0.5f * (a.node0.pos + b.node1.pos);
			return vec.CompareTo(other);
		}

		private static Dictionary<MergedNode, List<EdgeHandle>> MapMergedNodesToNerfedEdges(in List<EdgeHandle> nerfedEdges, in Dictionary<NodeHandle, MergedNode> mergedNodes)
		{
			Dictionary<MergedNode, List<EdgeHandle>> dictionary = new Dictionary<MergedNode, List<EdgeHandle>>();
			foreach (EdgeHandle nerfedEdge in nerfedEdges)
			{
				if (!dictionary.TryGetValue(mergedNodes[nerfedEdge.node0], out var value))
				{
					value = new List<EdgeHandle>();
					dictionary.Add(mergedNodes[nerfedEdge.node0], value);
				}
				value.Add(nerfedEdge);
				if (!dictionary.TryGetValue(mergedNodes[nerfedEdge.node1], out var value2))
				{
					value2 = new List<EdgeHandle>();
					dictionary.Add(mergedNodes[nerfedEdge.node1], value2);
				}
				value2.Add(nerfedEdge);
			}
			return dictionary;
		}

		private static List<MidNode> CreateMidNodes(in Dictionary<MergedNode, List<EdgeHandle>> nodeToEdges, Dictionary<EdgeHandle, float> edgeToStrength)
		{
			List<MidNode> list = new List<MidNode>();
			foreach (KeyValuePair<MergedNode, List<EdgeHandle>> nodeToEdge in nodeToEdges)
			{
				MergedNode key = nodeToEdge.Key;
				List<EdgeHandle> value = nodeToEdge.Value;
				value.Sort(edgeComparison);
				if (2 > value.Count)
				{
					continue;
				}
				float num = 1f / (float)(value.Count - 1);
				int num2 = 0;
				foreach (EdgeHandle item in value)
				{
					if (!item.material.enableCollision)
					{
						num2++;
					}
				}
				int num3 = value.Count - num2;
				float num4 = 1f / (float)UnityEngine.Mathf.Max(1, num3);
				num = 1f / UnityEngine.Mathf.Max(1E-05f, (float)(num3 - 1) * 1f + (float)num2 * 1E-05f);
				num4 = 1f / UnityEngine.Mathf.Max(1f, (float)num3 * 1f);
				for (int i = 0; i < value.Count; i++)
				{
					for (int j = i + 1; j < value.Count; j++)
					{
						if (value[i].material.enableCollision || value[j].material.enableCollision)
						{
							float nodeConnectivityFactorA = (value[i].material.enableCollision ? num : num4);
							float nodeConnectivityFactorB = (value[j].material.enableCollision ? num : num4);
							float connectivityWeightA = (value[i].material.enableCollision ? 1f : 1E-05f);
							float connectivityWeightB = (value[j].material.enableCollision ? 1f : 1E-05f);
							list.Add(new MidNode(key, value[i], value[j], nodeConnectivityFactorA, nodeConnectivityFactorB, connectivityWeightA, connectivityWeightB));
							edgeToStrength[value[i]] = 0f;
							edgeToStrength[value[j]] = 0f;
						}
					}
				}
			}
			return list;
		}

		private static void UpdateMidNodesConnectivityFactor(in Dictionary<MergedNode, List<EdgeHandle>> nodeToEdges, MidNode[] midNodes)
		{
			foreach (KeyValuePair<MergedNode, List<EdgeHandle>> nodeToEdge in nodeToEdges)
			{
				MergedNode key = nodeToEdge.Key;
				List<EdgeHandle> value = nodeToEdge.Value;
				int num = 0;
				foreach (EdgeHandle item in value)
				{
					if (!item.material.isDebris)
					{
						num++;
					}
				}
				int num2 = 0;
				foreach (EdgeHandle item2 in value)
				{
					if (!item2.material.isDebris && !item2.material.enableCollision)
					{
						num2++;
					}
				}
				key.debug_numMidNodes = num * (num - 1) / 2;
				key.debug_numMidNodes -= num2 * (num2 - 1) / 2;
				int num3 = num - num2;
				key.cached_connectivityFactorForRoad = 1f / UnityEngine.Mathf.Max(1E-05f, (float)(num3 - 1) * 1f + (float)num2 * 1E-05f);
				key.cached_connectivityFactorForNonRoad = 1f / UnityEngine.Mathf.Max(1f, (float)num3 * 1f);
			}
			for (int i = 0; i < midNodes.Length; i++)
			{
				ref MidNode reference = ref midNodes[i];
				if (!reference.edgeA.material.isDebris && !reference.edgeB.material.isDebris)
				{
					float cached_connectivityFactorForRoad = reference.node.cached_connectivityFactorForRoad;
					reference.nodeConnectivityFactorA = (reference.edgeA.material.enableCollision ? cached_connectivityFactorForRoad : reference.node.cached_connectivityFactorForNonRoad);
					reference.nodeConnectivityFactorB = (reference.edgeB.material.enableCollision ? cached_connectivityFactorForRoad : reference.node.cached_connectivityFactorForNonRoad);
					reference.connectivityWeightA = (reference.edgeA.material.enableCollision ? 1f : 1E-05f);
					reference.connectivityWeightB = (reference.edgeB.material.enableCollision ? 1f : 1E-05f);
					reference.node.debug_numMidNodes--;
				}
				else
				{
					reference.nodeConnectivityFactorA = 0f;
					reference.nodeConnectivityFactorB = 0f;
					reference.connectivityWeightA = 0f;
					reference.connectivityWeightB = 0f;
				}
			}
			foreach (KeyValuePair<MergedNode, List<EdgeHandle>> nodeToEdge2 in nodeToEdges)
			{
				MergedNode key2 = nodeToEdge2.Key;
				key2.cached_connectivityFactorForRoad = 0f;
				key2.cached_connectivityFactorForNonRoad = 0f;
			}
		}

		public void StoreSimulationAngleData_AndClear()
		{
			if (storedSimulationState != null)
			{
				UnityEngine.Debug.LogWarning("RoadLinkAngleMonitor.StoreSimulationAngleData() not expected to be called twice in one frame.");
			}
			if (midNodes != null)
			{
				storedSimulationState = midNodes;
				midNodes = null;
				edgeToStrength.Clear();
				baseStrengthCombined.Clear();
				mergedNodes.Clear();
				nodeToEdges.Clear();
				world = null;
				isInitialized = false;
			}
		}

		public void Clear(bool clearHistory = true)
		{
			midNodes = null;
			edgeToStrength.Clear();
			baseStrengthCombined.Clear();
			mergedNodes.Clear();
			nodeToEdges.Clear();
			world = null;
			isInitialized = false;
			if (clearHistory)
			{
				storedSimulationState = null;
			}
		}

		private void RestoreSimulationAngleData_Once()
		{
			if (storedSimulationState == null)
			{
				return;
			}
			Dictionary<uint, int> dictionary = new Dictionary<uint, int>();
			for (int i = 0; i < storedSimulationState.Length; i++)
			{
				if (storedSimulationState[i].isValid)
				{
					dictionary.Add(storedSimulationState[i].key, i);
				}
			}
			for (int j = 0; j < midNodes.Length; j++)
			{
				if (dictionary.TryGetValue(midNodes[j].key, out var value))
				{
					ref MidNode other = ref storedSimulationState[value];
					midNodes[j].CopyStateFrom(in other);
				}
			}
			storedSimulationState = null;
		}

		public void UpdateStep(float deltaTime, BridgeSegmentation temp_segmentation)
		{
			if (!isInitialized)
			{
				return;
			}
			if (wasEdgeBroken)
			{
				UpdateMidNodesConnectivityFactor(in nodeToEdges, midNodes);
				wasEdgeBroken = false;
			}
			EdgeHandle[] array = edgeToStrength.Keys.ToArray();
			edgeToNerfA.Clear();
			edgeToNerfB.Clear();
			EdgeHandle[] array2 = array;
			foreach (EdgeHandle key in array2)
			{
				edgeToStrength[key] = 1f;
				edgeToNerfA[key] = 0f;
				edgeToNerfB[key] = 0f;
			}
			HashSet<MergedNode> hashSet = new HashSet<MergedNode>();
			for (int j = 0; j < midNodes.Length; j++)
			{
				ref MidNode reference = ref midNodes[j];
				if (reference.nodeConnectivityFactorA != 0f && reference.nodeConnectivityFactorB != 0f)
				{
					reference.UpdateDirection(deltaTime, flipCountCooldownRate, flipCooldownFreezeDuration);
					float num = CalcNerf(in reference) * 0.5f;
					if (0f < num)
					{
						edgeToStrength[reference.edgeA] -= num * reference.nodeConnectivityFactorA * reference.connectivityWeightB;
						edgeToStrength[reference.edgeB] -= num * reference.nodeConnectivityFactorB * reference.connectivityWeightA;
					}
					else
					{
						hashSet.Add(reference.node);
					}
				}
			}
			array2 = array;
			foreach (EdgeHandle edgeHandle in array2)
			{
				Vec2 a = edgeHandle.node1.pos - edgeHandle.node0.pos;
				if (UnityEngine.Mathf.Abs(a.x) < UnityEngine.Mathf.Abs(a.y))
				{
					float num2 = 1f;
					if (1E-12f < a.sqrMagnitude)
					{
						a.Normalize();
						num2 = Pb.Mathf.Clamp01((Pb.Mathf.Abs(Vec2.Dot(in a, in Vec2.right)) - 0.3827f) / 0.32439998f);
					}
					edgeToStrength[edgeHandle] = 1f - (1f - edgeToStrength[edgeHandle]) * num2;
					edgeToNerfA[edgeHandle] *= num2;
					edgeToNerfB[edgeHandle] *= num2;
				}
			}
			array2 = array;
			foreach (EdgeHandle edgeHandle2 in array2)
			{
				edgeToStrength[edgeHandle2] = UnityEngine.Mathf.Max(edgeToStrength[edgeHandle2], 1f - maxDeduction);
				if (combineWithMethodOne)
				{
					edgeToStrength[edgeHandle2] += baseStrengthCombined[edgeHandle2] - 1f;
				}
				_ = edgeToStrength[edgeHandle2];
				if (mustBeNerfedOnBothSides && (hashSet.Contains(mergedNodes[edgeHandle2.node0]) || hashSet.Contains(mergedNodes[edgeHandle2.node1])))
				{
					edgeToStrength[edgeHandle2] = (combineWithMethodOne ? baseStrengthCombined[edgeHandle2] : 1f);
				}
			}
			edgeToNerfA.Clear();
			edgeToNerfB.Clear();
		}

		public void ApplyStrenghts()
		{
			foreach (KeyValuePair<EdgeHandle, float> item in edgeToStrength)
			{
				EdgeHandle key = item.Key;
				float value = item.Value;
				key.maxForce_ActualFraction = value;
			}
		}

		private float CalcNerf(in MidNode info)
		{
			float b = UnityEngine.Mathf.Max(info.numDirectionChangesRaw - (float)numDirectionChangesOk, 0f) * deductionPerDirChange;
			return UnityEngine.Mathf.Max(0f, b);
		}

		public void OnEdgeBroken(EdgeHandle e)
		{
			wasEdgeBroken = true;
		}

		public void DrawAngles(Font font, Color backgroundColor, int fontSize, float labelVerticalOffset, float desiredLabelWidthInMeters = 0f)
		{
			if (!isInitialized)
			{
				return;
			}
			if (desiredLabelWidthInMeters == 0f)
			{
				desiredLabelWidthInMeters = this.desiredLabelWidthInMeters;
			}
			DrawGuiTextUtil.InitGuiStyle(font, backgroundColor, fontSize);
			GUIStyle style = DrawGuiTextUtil.style;
			Color green = Color.green;
			green.a = backgroundColor.a;
			DrawGuiTextUtil.InitGuiStyle(font, green, fontSize);
			GUIStyle style2 = DrawGuiTextUtil.style;
			for (int i = 0; i < midNodes.Length; i++)
			{
				ref MidNode reference = ref midNodes[i];
				if (0f < reference.nodeConnectivityFactorA && 0f < reference.numDirectionChangesRaw)
				{
					float num = CalcNerf(in reference);
					if (num != 0f || reference.numDirectionChangesRaw != 0f)
					{
						Vec2 vec = reference.node.nodes[0].pos + labelVerticalOffset * Vec2.up;
						string text = $"flip# {reference.numDirectionChangesRaw:0.0}\r\n" + $"nerf: {num * 100f:0.}%";
						DrawGuiTextUtil.style = (reference.notNerfed ? style2 : style);
						DrawGuiTextUtil.DisplayGuiLabel_Slow(vec - 1.5f * Vector3.forward, text, desiredLabelWidthInMeters);
					}
				}
			}
			DrawGuiTextUtil.style = style;
			foreach (KeyValuePair<EdgeHandle, float> item in edgeToStrength)
			{
				EdgeHandle key = item.Key;
				if (!key.material.isDebris)
				{
					float value = item.Value;
					if (value != 1f)
					{
						Vec2 vec2 = 0.5f * (key.node0.pos + key.node1.pos) + labelVerticalOffset * Vec2.up;
						DrawGuiTextUtil.DisplayGuiLabel_Slow(text: (!combineWithMethodOne) ? $"st: {UnityEngine.Mathf.RoundToInt(value * 100f)}" : (key.material.enableCollision ? $"st: {UnityEngine.Mathf.RoundToInt(value * 100f)}%\r\nm1: {UnityEngine.Mathf.RoundToInt(baseStrengthCombined[key] * 100f)}%" : $"st: {UnityEngine.Mathf.RoundToInt(value * 100f)}%\r\nm1: ---"), posInWorld: vec2 - 1.5f * Vector3.forward, desiredLabelWidthInMeters: desiredLabelWidthInMeters);
					}
				}
			}
		}
	}
}
