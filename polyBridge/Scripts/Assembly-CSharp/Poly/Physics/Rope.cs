using System;
using System.Collections.Generic;
using System.Linq;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	public class Rope : Action
	{
		public Edge edge;

		[Range(0.1f, 1f)]
		public float maxSegmentLength = 0.25f;

		[Range(1f, 100f)]
		public int numIterations = 4;

		public bool firstNodeKeyframed = true;

		public bool lastNodeKeyframed = true;

		public Material lineMaterial;

		public bool drawGizmos;

		[Range(0f, 2f)]
		public float gravityScale = 1f;

		private RopeNode[] nodes;

		private float segmentLength;

		[NonSerialized]
		public EdgeMaterial originalMaterial;

		[NonSerialized]
		public bool visualize = true;

		private LineRenderer lineRenderer;

		private float cachedFractionLength = 1f;

		private Vector3[] nodePositions_buffer;

		public override void OnAddedToWorld()
		{
			base.OnAddedToWorld();
			if (!edge)
			{
				edge = GetComponentInParent<Edge>();
			}
			if (nodes == null)
			{
				float length = edge.handle.solverEdge.length;
				int num = Mathf.CeilToInt(length / maxSegmentLength);
				float num2 = 1f / (float)num;
				int num3 = num + 1;
				nodes = new RopeNode[num3];
				for (int i = 0; i < num3; i++)
				{
					nodes[i].pos = Vec2.LerpUnclamped(edge.node0.pos, edge.node1.pos, (float)i * num2);
					nodes[i].posOld = nodes[i].pos;
				}
				segmentLength = num2 * length;
			}
			if ((bool)lineMaterial)
			{
				lineRenderer = base.gameObject.AddComponent<LineRenderer>();
				lineRenderer.sharedMaterial = lineMaterial;
				lineRenderer.widthMultiplier = 2f * edge.material.collisionRadius;
			}
		}

		public override void LateExecute()
		{
			base.LateExecute();
			if ((bool)edge && (bool)edge.handle && !edge.handle.solverEdge.isBroken)
			{
				Integrate();
				Solve();
				ResetEndpoints();
				Visualize();
			}
		}

		private void Integrate(float deltaTime = 0f)
		{
			SolverSettings settings = edge.handle.world.settings;
			Vec2 vec = settings.scaledGravity * gravityScale;
			if (deltaTime == 0f)
			{
				deltaTime = settings.frameDeltaTime;
			}
			float num = Mathf.Pow(1f - settings.nodeVelocityDrag, deltaTime);
			for (int i = 0; i < nodes.Length; i++)
			{
				ref RopeNode reference = ref nodes[i];
				Vec2 pos = reference.pos;
				reference.pos += num * (reference.pos - reference.posOld);
				reference.posOld = pos;
				reference.pos += vec * deltaTime * deltaTime;
			}
		}

		private void ResetEndpoints()
		{
			if (firstNodeKeyframed)
			{
				nodes[0].pos = edge.node0.pos;
				nodes[0].posOld = edge.node0.handle.oldPos;
			}
			if (lastNodeKeyframed)
			{
				nodes[nodes.Length - 1].pos = edge.node1.pos;
				nodes[nodes.Length - 1].posOld = edge.node1.handle.oldPos;
			}
		}

		private void Solve()
		{
			int num = nodes.Length - 1;
			for (int i = 0; i < numIterations; i++)
			{
				for (int j = 0; j < num; j++)
				{
					ref Vec2 pos = ref nodes[j].pos;
					ref Vec2 pos2 = ref nodes[j + 1].pos;
					Vec2 vec = pos2 - pos;
					float magnitude = vec.magnitude;
					Vec2 vec2 = vec * (1f - segmentLength / (magnitude + 1E-09f));
					float num2 = 0.5f;
					float num3 = -0.5f;
					if (j == 0 && firstNodeKeyframed)
					{
						num2 = 0f;
						num3 *= 2f;
					}
					if (j + 1 == num && lastNodeKeyframed)
					{
						num2 *= 2f;
						num3 = 0f;
					}
					pos += num2 * vec2;
					pos2 += num3 * vec2;
				}
			}
		}

		private void Visualize()
		{
			if (visualize && (bool)lineRenderer)
			{
				Vector3[] array = ComputeNodePositions();
				lineRenderer.positionCount = array.Length;
				lineRenderer.SetPositions(array);
			}
		}

		public Vector3[] ComputeNodePositions()
		{
			float num = cachedFractionLength;
			Vec2 a = edge.node0.smoothPos;
			Vec2 b = edge.node1.smoothPos;
			if ((bool)edge && (bool)edge.handle)
			{
				num = (cachedFractionLength = Vec2.Distance(in a, in b) / edge.handle.solverEdge.length);
			}
			float num2 = Mathf.Clamp01((num - 0.5f) / 0.48f);
			float currentFractionOfFixedFrame = base.world.currentFractionOfFixedFrame;
			if (nodePositions_buffer == null || nodePositions_buffer.Length != nodes.Length)
			{
				nodePositions_buffer = new Vector3[nodes.Length];
			}
			Vector3[] array = nodePositions_buffer;
			for (int i = 0; i < nodes.Length; i++)
			{
				ref RopeNode reference = ref nodes[i];
				array[i] = (1f - currentFractionOfFixedFrame) * reference.posOld + currentFractionOfFixedFrame * reference.pos;
			}
			if (num2 > 0f && firstNodeKeyframed && lastNodeKeyframed)
			{
				Vec2 vec = a;
				Vec2 vec2 = (b - a) / (nodes.Length - 1);
				for (int j = 0; j < nodes.Length; j++)
				{
					Vec2 b2 = vec + j * vec2;
					array[j] = Vec2.LerpUnclamped((Vec2)array[j], in b2, num2);
				}
			}
			return array;
		}

		private void OnDrawGizmos()
		{
			if (drawGizmos && nodes != null)
			{
				Gizmos.color = Color.gray;
				RopeNode[] array = nodes;
				for (int i = 0; i < array.Length; i++)
				{
					Gizmos.DrawWireSphere(array[i].pos, 0.1f);
				}
				Vector3[] array2 = ComputeNodePositions();
				Gizmos.color = Color.white;
				Vector3[] array3 = array2;
				for (int i = 0; i < array3.Length; i++)
				{
					Gizmos.DrawWireSphere(array3[i], 0.1f);
				}
			}
		}

		public Rope CopyRopeFromTill(float t0, float t1, Edge parent, RopeNode[] srcNodes = null)
		{
			Rope rope = UnityEngine.Object.Instantiate(this, parent.transform);
			UnityEngine.Object.DestroyImmediate(rope.GetComponent<LineRenderer>());
			rope.edge = parent;
			rope.segmentLength = segmentLength;
			int num = Mathf.CeilToInt(t0 * (float)nodes.Length);
			int num2 = Mathf.CeilToInt(t1 * (float)nodes.Length);
			int num3 = 1;
			if (num2 < num)
			{
				num--;
				num2--;
				num3 = -1;
				num2--;
				if (num2 < 0)
				{
					num2 = -1;
				}
			}
			List<RopeNode> list = new List<RopeNode>();
			RopeNode[] array = srcNodes ?? nodes;
			for (int i = num; i != num2; i += num3)
			{
				list.Add(array[i]);
			}
			rope.nodes = list.ToArray();
			if (t0 != 0f && t0 != 1f)
			{
				rope.firstNodeKeyframed = false;
			}
			if (t1 != 0f && t1 != 1f)
			{
				rope.lastNodeKeyframed = false;
			}
			return rope;
		}

		public RopeNode[] CalcNodesForCopying()
		{
			RopeNode[] array = nodes.ToArray();
			float num = cachedFractionLength;
			Vec2 a = edge.node0.pos;
			Vec2 b = edge.node1.pos;
			if ((bool)edge && (bool)edge.handle)
			{
				num = (cachedFractionLength = Vec2.Distance(in a, in b) / edge.handle.solverEdge.length);
			}
			float num2 = (num - 0.5f) / 0.48f;
			num2 = ((!(0f <= num2)) ? 0f : ((num2 <= 1f) ? num2 : 1f));
			if (num2 > 0f && firstNodeKeyframed && lastNodeKeyframed)
			{
				Vec2 vec = a;
				Vec2 vec2 = (b - a) / (nodes.Length - 1);
				Vec2 oldPos = edge.node0.handle.oldPos;
				Vec2 vec3 = (edge.node1.handle.oldPos - edge.node0.handle.oldPos) / (nodes.Length - 1);
				for (int i = 0; i < nodes.Length; i++)
				{
					Vec2 b2 = vec + i * vec2;
					Vec2 b3 = oldPos + i * vec3;
					array[i].pos = Vec2.LerpUnclamped(in array[i].pos, in b2, num2);
					array[i].posOld = Vec2.LerpUnclamped(in array[i].posOld, in b3, num2);
				}
			}
			return array;
		}
	}
}
