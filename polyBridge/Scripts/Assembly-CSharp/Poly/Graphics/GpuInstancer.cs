using System.Collections.Generic;
using Poly.Base;
using Poly.Physics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Poly.Graphics
{
	public class GpuInstancer : SingletonBehaviour<GpuInstancer>
	{
		private struct Batch
		{
			public Matrix4x4[] matrices;

			public Vector4[] colors;

			public float[] tiling;

			public MaterialPropertyBlock propertyBlock;

			public ComputeBuffer colorsBuffer;

			public ComputeBuffer tilingBuffer;

			public MaterialPropertyBlock noStressPropertyBlock;

			public ComputeBuffer noStressBuffer;

			public Batch(int size)
			{
				int num = (size + 3) & -4;
				matrices = new Matrix4x4[size];
				colors = new Vector4[size];
				tiling = new float[num];
				propertyBlock = new MaterialPropertyBlock();
				colorsBuffer = new ComputeBuffer(size, 16);
				tilingBuffer = new ComputeBuffer(num, 4);
				noStressPropertyBlock = new MaterialPropertyBlock();
				noStressBuffer = new ComputeBuffer(size, 16);
			}
		}

		private const int maximumBatchSizeForNonIndirectMeshInstancing = 256;

		public Mesh mesh;

		public Material material;

		public float visualScaleMultiplier = 1f;

		public Camera gameCamera;

		public Camera replayCamera;

		public Camera todo_snapshotCamera;

		public Camera[] camerasToDisableDuringSim;

		private List<Camera> actuallyDisabledCameras = new List<Camera>();

		private int numScannedBridgeEdges;

		private List<BridgeEdge> instancedEdges = new List<BridgeEdge>();

		private List<Edge> physicsEdges = new List<Edge>();

		private const int numEdgesInBatch = 256;

		private List<Batch> batches = new List<Batch>();

		private const string edgeLayerName = "Edge";

		private const string nodeLayerName = "Joint";

		private static int edgeLayer;

		private static int nodeLayer;

		public List<BridgeEdge> notInstancedEdges { get; private set; }

		public static bool isInstancingSupported { get; private set; }

		public static bool isActivatedAndInstancing { get; private set; }

		private static bool stressViewEnabled => Profiles.m_ActiveProfile.m_StressViewEnabled;

		public void Activate()
		{
			if (!isInstancingSupported || !base.isActiveAndEnabled)
			{
				return;
			}
			isActivatedAndInstancing = true;
			EnablePerObjectMeshRenderers(isVisible: false);
			Camera[] array = camerasToDisableDuringSim;
			foreach (Camera camera in array)
			{
				if (camera.isActiveAndEnabled)
				{
					camera.enabled = false;
					actuallyDisabledCameras.Add(camera);
				}
			}
		}

		public void Reset()
		{
			if (!isInstancingSupported)
			{
				return;
			}
			isActivatedAndInstancing = false;
			instancedEdges.Clear();
			physicsEdges.Clear();
			ClearBatchesAndReleaseBuffers();
			notInstancedEdges.Clear();
			numScannedBridgeEdges = 0;
			foreach (Camera actuallyDisabledCamera in actuallyDisabledCameras)
			{
				actuallyDisabledCamera.enabled = true;
			}
			actuallyDisabledCameras.Clear();
		}

		public void ScanForNewEdges()
		{
			if (isInstancingSupported && base.isActiveAndEnabled)
			{
				for (int i = numScannedBridgeEdges; i < BridgeEdges.m_Edges.Count; i++)
				{
					notInstancedEdges.Add(BridgeEdges.m_Edges[i]);
				}
				numScannedBridgeEdges = BridgeEdges.m_Edges.Count;
			}
		}

		private new void Awake()
		{
			base.Awake();
			edgeLayer = LayerMask.NameToLayer("Edge");
			nodeLayer = LayerMask.NameToLayer("Joint");
			isInstancingSupported = SystemInfo.supportsComputeShaders;
			notInstancedEdges = new List<BridgeEdge>();
		}

		private void OnDisable()
		{
			if (isInstancingSupported && isActivatedAndInstancing && !World.isQuitting)
			{
				EnablePerObjectMeshRenderers(isVisible: true);
				Reset();
			}
		}

		private new void OnDestroy()
		{
			ClearBatchesAndReleaseBuffers();
			base.OnDestroy();
		}

		private void Update()
		{
			if (isInstancingSupported && base.isActiveAndEnabled)
			{
				if (isActivatedAndInstancing ^ isActivatedAndInstancing)
				{
					EnablePerObjectMeshRenderers(!isActivatedAndInstancing);
				}
				if (isActivatedAndInstancing)
				{
					UpdateEdgeMatrices();
					InstanceMeshes();
				}
			}
		}

		private void EnablePerObjectMeshRenderers(bool isVisible)
		{
			instancedEdges.Clear();
			physicsEdges.Clear();
			ClearBatchesAndReleaseBuffers();
			notInstancedEdges.Clear();
			numScannedBridgeEdges = 0;
			foreach (BridgeEdge edge in BridgeEdges.m_Edges)
			{
				if (edge.isActiveAndEnabled && (bool)edge.m_MeshFilter && edge.m_MeshFilter.sharedMesh == mesh)
				{
					edge.m_MeshRenderer.enabled = isVisible;
					instancedEdges.Add(edge);
					edge.m_BoxCollider.enabled = isVisible;
					physicsEdges.Add(edge.m_PhysicsEdge);
				}
				else if (edge.isActiveAndEnabled)
				{
					notInstancedEdges.Add(edge);
				}
			}
			numScannedBridgeEdges = BridgeEdges.m_Edges.Count;
			if (isVisible)
			{
				instancedEdges.Clear();
				physicsEdges.Clear();
				numScannedBridgeEdges = 0;
				return;
			}
			int count = instancedEdges.Count;
			for (int i = 0; i < count; i += 256)
			{
				int num = Mathf.Min(256, count - i);
				Batch item = new Batch(num);
				for (int j = 0; j < num; j++)
				{
					item.matrices[j] = Matrix4x4.identity;
					item.colors[j] = Color.black;
					item.tiling[j] = instancedEdges[i + j].cachedTiling;
				}
				item.colorsBuffer.SetData(item.colors);
				item.tilingBuffer.SetData(item.tiling);
				item.propertyBlock.SetBuffer("_EdgeColors", item.colorsBuffer);
				item.propertyBlock.SetBuffer("_EdgeTiling", item.tilingBuffer);
				item.noStressBuffer.SetData(item.colors);
				item.noStressPropertyBlock.SetBuffer("_EdgeColors", item.noStressBuffer);
				item.noStressPropertyBlock.SetBuffer("_EdgeTiling", item.tilingBuffer);
				batches.Add(item);
			}
		}

		private void UpdateEdgeMatrices()
		{
			int num = 0;
			foreach (Batch batch in batches)
			{
				int num2 = num;
				Matrix4x4[] matrices = batch.matrices;
				int num3 = 0;
				while (num3 < matrices.Length)
				{
					Edge edge = physicsEdges[num];
					Vec2 cachedSmoothPos = edge.node0.cachedSmoothPos;
					Vec2 cachedSmoothPos2 = edge.node1.cachedSmoothPos;
					Vec2 vec = cachedSmoothPos2 - cachedSmoothPos;
					Vec2 vec2 = 0.5f * (cachedSmoothPos + cachedSmoothPos2);
					float num4 = vec.magnitude + 5.877472E-39f;
					float num5 = num4 * visualScaleMultiplier;
					num4 *= edge.nodeDirectionMultiplier;
					float num6 = vec.x / num4;
					float num7 = vec.y / num4;
					ref Matrix4x4 reference = ref matrices[num3];
					reference.m03 = vec2.x;
					reference.m13 = vec2.y;
					reference.m00 = num6 * num5;
					reference.m01 = 0f - num7;
					reference.m10 = num7 * num5;
					reference.m11 = num6;
					num3++;
					num++;
				}
				if (stressViewEnabled)
				{
					Vector4[] colors = batch.colors;
					num = num2;
					int num8 = 0;
					while (num8 < colors.Length)
					{
						float smoothedStressNormalized = physicsEdges[num].smoothedStressNormalized;
						smoothedStressNormalized = ((0.01f <= smoothedStressNormalized) ? ((smoothedStressNormalized <= 1f) ? smoothedStressNormalized : 1f) : 0.01f);
						batch.colors[num8] = BridgeEdge.GetColorForStress(smoothedStressNormalized);
						num8++;
						num++;
					}
					batch.colorsBuffer.SetData(batch.colors);
				}
			}
		}

		private void InstanceMeshes()
		{
			foreach (Batch batch in batches)
			{
				if (stressViewEnabled)
				{
					UnityEngine.Graphics.DrawMeshInstanced(mesh, 0, material, batch.matrices, batch.matrices.Length, batch.propertyBlock, ShadowCastingMode.On, receiveShadows: true, edgeLayer, gameCamera);
					if ((bool)gameCamera && (bool)replayCamera)
					{
						UnityEngine.Graphics.DrawMeshInstanced(mesh, 0, material, batch.matrices, batch.matrices.Length, batch.noStressPropertyBlock, ShadowCastingMode.On, receiveShadows: true, edgeLayer, replayCamera);
					}
				}
				else
				{
					UnityEngine.Graphics.DrawMeshInstanced(mesh, 0, material, batch.matrices, batch.matrices.Length, batch.noStressPropertyBlock, ShadowCastingMode.On, receiveShadows: true, edgeLayer, null);
				}
			}
		}

		private void ClearBatchesAndReleaseBuffers()
		{
			foreach (Batch batch in batches)
			{
				batch.colorsBuffer?.Release();
				batch.tilingBuffer?.Release();
				batch.noStressBuffer?.Release();
			}
			batches.Clear();
		}
	}
}
