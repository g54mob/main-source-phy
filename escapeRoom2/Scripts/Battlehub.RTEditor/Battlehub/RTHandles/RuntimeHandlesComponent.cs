using System;
using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTHandles
{
	[DefaultExecutionOrder(-90)]
	public class RuntimeHandlesComponent : MonoBehaviour, IRuntimeHandlesComponent
	{
		[SerializeField]
		private RTHColors m_colors = new RTHColors();

		[SerializeField]
		private float m_handleScale = 1f;

		[SerializeField]
		private float m_selectionMargin = 1f;

		[SerializeField]
		public float m_selectionMarginPixels = 10f;

		[SerializeField]
		private bool m_invertZAxis;

		[SerializeField]
		private bool m_positionHandleArrowOnly;

		[SerializeField]
		private float m_sceneGizmoScale = 1f;

		protected Mesh Axes;

		protected Mesh Arrows;

		protected Mesh ArrowY;

		protected Mesh ArrowX;

		protected Mesh ArrowZ;

		protected Mesh SelectionArrowY;

		protected Mesh SelectionArrowX;

		protected Mesh SelectionArrowZ;

		protected Mesh DisabledArrowY;

		protected Mesh DisabledArrowX;

		protected Mesh DisabledArrowZ;

		protected Mesh Quads;

		protected Mesh WireQuads;

		protected Mesh Quad;

		protected Mesh SelectionCube;

		protected Mesh DisabledCube;

		protected Mesh CubeX;

		protected Mesh CubeY;

		protected Mesh CubeZ;

		protected Mesh CubeUniform;

		protected Mesh WireCircle;

		protected Mesh WireCircle11;

		protected Mesh SceneGizmoSelectedAxis;

		protected Mesh SceneGizmoXAxis;

		protected Mesh SceneGizmoYAxis;

		protected Mesh SceneGizmoZAxis;

		protected Mesh SceneGizmoCube;

		protected Mesh SceneGizmoSelectedCube;

		protected Mesh SceneGizmoQuad;

		protected Material m_shapesMaterialZTest;

		protected Material m_shapesMaterialZTest2;

		protected Material m_shapesMaterialZTest3;

		protected Material m_shapesMaterialZTest4;

		protected Material m_shapesMaterialZTestOffset;

		protected Material m_linesMaterial;

		protected Material m_linesClipMaterial;

		protected Material m_linesClipUsingClipPlaneMaterial;

		protected Material m_linesBillboardMaterial;

		protected Material m_xMaterial;

		protected Material m_yMaterial;

		protected Material m_zMaterial;

		protected Material m_unlitColorMaterial;

		private static RuntimeHandlesComponent s_instance;

		private float m_oldHandleScale;

		private bool m_oldInvertZAxis;

		private const float innerRadius = 1f;

		private const float outerRadius = 1.2f;

		private static RTECommandBuffer s_commandBufferWrapper = new RTECommandBuffer(null);

		public RTHColors Colors
		{
			get
			{
				return m_colors;
			}
			set
			{
				m_colors = value;
			}
		}

		public float HandleScale
		{
			get
			{
				return m_handleScale;
			}
			set
			{
				m_handleScale = value;
			}
		}

		public float SelectionMargin
		{
			get
			{
				return m_selectionMargin * m_handleScale;
			}
			set
			{
				m_selectionMargin = value;
			}
		}

		public float SelectionMarginPixels
		{
			get
			{
				return m_selectionMarginPixels;
			}
			set
			{
				m_selectionMarginPixels = value;
			}
		}

		public bool InvertZAxis
		{
			get
			{
				return m_invertZAxis;
			}
			set
			{
				m_invertZAxis = value;
			}
		}

		public bool PositionHandleArrowOnly
		{
			get
			{
				return m_positionHandleArrowOnly;
			}
			set
			{
				m_positionHandleArrowOnly = value;
			}
		}

		public float SceneGizmoScale
		{
			get
			{
				return m_sceneGizmoScale;
			}
			set
			{
				m_sceneGizmoScale = value;
			}
		}

		public Vector3 Forward
		{
			get
			{
				if (!m_invertZAxis)
				{
					return Vector3.forward;
				}
				return Vector3.back;
			}
		}

		public static void InitializeIfRequired(ref RuntimeHandlesComponent handles)
		{
			if (!(handles == null))
			{
				return;
			}
			if (s_instance == null)
			{
				s_instance = UnityObjectExt.FindAnyObjectByType<RuntimeHandlesComponent>();
				if (s_instance == null)
				{
					IRTE iRTE = IOC.Resolve<IRTE>();
					GameObject obj = new GameObject("RuntimeHandlesComponent");
					obj.transform.SetParent(iRTE.Root.transform, worldPositionStays: false);
					s_instance = obj.AddComponent<RuntimeHandlesComponent>();
				}
			}
			handles = s_instance;
		}

		protected virtual void Awake()
		{
			m_oldHandleScale = m_handleScale;
			m_oldInvertZAxis = m_invertZAxis;
			Initialize();
		}

		protected virtual void OnDestroy()
		{
			if (s_instance == this)
			{
				s_instance = null;
			}
			Cleanup();
		}

		protected virtual void Update()
		{
			if (m_oldHandleScale != m_handleScale || m_oldInvertZAxis != m_invertZAxis)
			{
				m_oldHandleScale = m_handleScale;
				m_oldInvertZAxis = m_invertZAxis;
				ApplySettings();
			}
		}

		public void ApplySettings()
		{
			Cleanup();
			Initialize();
		}

		private void Initialize()
		{
			m_linesMaterial = new Material(Shader.Find("Battlehub/RTCommon/LineBillboard"));
			m_linesMaterial.color = Color.white;
			m_linesMaterial.SetFloat("_Scale", m_handleScale);
			m_linesClipMaterial = new Material(Shader.Find("Battlehub/RTHandles/LineBillboardClip"));
			m_linesClipMaterial.color = Color.white;
			m_linesClipMaterial.SetFloat("_Scale", m_handleScale);
			m_linesClipUsingClipPlaneMaterial = new Material(Shader.Find("Battlehub/RTHandles/VertexColorClipUsingClipPlane"));
			m_linesClipUsingClipPlaneMaterial.color = Color.white;
			m_linesBillboardMaterial = new Material(Shader.Find("Battlehub/RTHandles/LineBillboard"));
			m_linesBillboardMaterial.color = Color.white;
			m_linesBillboardMaterial.SetFloat("_Scale", m_handleScale);
			m_shapesMaterialZTest = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
			m_shapesMaterialZTest.color = new Color(1f, 1f, 1f, 1f);
			m_shapesMaterialZTest.SetFloat("_ZTest", 4f);
			m_shapesMaterialZTest.SetFloat("_ZWrite", 1f);
			m_shapesMaterialZTestOffset = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
			m_shapesMaterialZTestOffset.color = new Color(1f, 1f, 1f, 1f);
			m_shapesMaterialZTestOffset.SetFloat("_ZTest", 4f);
			m_shapesMaterialZTestOffset.SetFloat("_ZWrite", 1f);
			m_shapesMaterialZTestOffset.SetFloat("_OFactors", -1f);
			m_shapesMaterialZTestOffset.SetFloat("_OUnits", -1f);
			m_shapesMaterialZTest2 = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
			m_shapesMaterialZTest2.color = new Color(1f, 1f, 1f, 0f);
			m_shapesMaterialZTest2.SetFloat("_ZTest", 4f);
			m_shapesMaterialZTest2.SetFloat("_ZWrite", 1f);
			m_shapesMaterialZTest3 = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
			m_shapesMaterialZTest3.color = new Color(1f, 1f, 1f, 0f);
			m_shapesMaterialZTest3.SetFloat("_ZTest", 4f);
			m_shapesMaterialZTest3.SetFloat("_ZWrite", 1f);
			m_shapesMaterialZTest4 = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
			m_shapesMaterialZTest4.color = new Color(1f, 1f, 1f, 0f);
			m_shapesMaterialZTest4.SetFloat("_ZTest", 4f);
			m_shapesMaterialZTest4.SetFloat("_ZWrite", 1f);
			m_xMaterial = new Material(Shader.Find("Battlehub/RTCommon/Billboard"));
			m_xMaterial.color = Color.white;
			m_xMaterial.mainTexture = Resources.Load<Texture>("Battlehub.RuntimeHandles.x");
			m_xMaterial.enableInstancing = true;
			m_yMaterial = new Material(Shader.Find("Battlehub/RTCommon/Billboard"));
			m_yMaterial.color = Color.white;
			m_yMaterial.mainTexture = Resources.Load<Texture>("Battlehub.RuntimeHandles.y");
			m_yMaterial.enableInstancing = true;
			m_zMaterial = new Material(Shader.Find("Battlehub/RTCommon/Billboard"));
			m_zMaterial.color = Color.white;
			m_zMaterial.mainTexture = Resources.Load<Texture>("Battlehub.RuntimeHandles.z");
			m_unlitColorMaterial = new Material(Shader.Find("Hidden/RTCommon/UnlitColor"));
			Axes = CreateAxes();
			Mesh mesh = GraphicsUtility.CreateCone(m_colors.SelectionColor, m_handleScale);
			Mesh mesh2 = GraphicsUtility.CreateCone(m_colors.DisabledColor, m_handleScale);
			CombineInstance combineInstance = new CombineInstance
			{
				mesh = mesh,
				transform = Matrix4x4.TRS(Vector3.up * m_handleScale, Quaternion.identity, Vector3.one)
			};
			SelectionArrowY = new Mesh();
			SelectionArrowY.CombineMeshes(new CombineInstance[1] { combineInstance }, mergeSubMeshes: true);
			SelectionArrowY.RecalculateNormals();
			combineInstance.mesh = mesh2;
			combineInstance.transform = Matrix4x4.TRS(Vector3.up * m_handleScale, Quaternion.identity, Vector3.one);
			DisabledArrowY = new Mesh();
			DisabledArrowY.CombineMeshes(new CombineInstance[1] { combineInstance }, mergeSubMeshes: true);
			DisabledArrowY.RecalculateNormals();
			combineInstance.mesh = GraphicsUtility.CreateCone(m_colors.YColor, m_handleScale);
			combineInstance.transform = Matrix4x4.TRS(Vector3.up * m_handleScale, Quaternion.identity, Vector3.one);
			ArrowY = new Mesh();
			ArrowY.CombineMeshes(new CombineInstance[1] { combineInstance }, mergeSubMeshes: true);
			ArrowY.RecalculateNormals();
			CombineInstance combineInstance2 = new CombineInstance
			{
				mesh = mesh,
				transform = Matrix4x4.TRS(Vector3.right * m_handleScale, Quaternion.AngleAxis(-90f, Vector3.forward), Vector3.one)
			};
			SelectionArrowX = new Mesh();
			SelectionArrowX.CombineMeshes(new CombineInstance[1] { combineInstance2 }, mergeSubMeshes: true);
			SelectionArrowX.RecalculateNormals();
			combineInstance2.mesh = mesh2;
			combineInstance2.transform = Matrix4x4.TRS(Vector3.right * m_handleScale, Quaternion.AngleAxis(-90f, Vector3.forward), Vector3.one);
			DisabledArrowX = new Mesh();
			DisabledArrowX.CombineMeshes(new CombineInstance[1] { combineInstance2 }, mergeSubMeshes: true);
			DisabledArrowX.RecalculateNormals();
			combineInstance2.mesh = GraphicsUtility.CreateCone(m_colors.XColor, m_handleScale);
			combineInstance2.transform = Matrix4x4.TRS(Vector3.right * m_handleScale, Quaternion.AngleAxis(-90f, Vector3.forward), Vector3.one);
			ArrowX = new Mesh();
			ArrowX.CombineMeshes(new CombineInstance[1] { combineInstance2 }, mergeSubMeshes: true);
			ArrowX.RecalculateNormals();
			Vector3 pos = Forward * m_handleScale;
			Quaternion quaternion = (m_invertZAxis ? Quaternion.AngleAxis(-90f, Vector3.right) : Quaternion.AngleAxis(90f, Vector3.right));
			CombineInstance combineInstance3 = new CombineInstance
			{
				mesh = mesh,
				transform = Matrix4x4.TRS(pos, quaternion, Vector3.one)
			};
			SelectionArrowZ = new Mesh();
			SelectionArrowZ.CombineMeshes(new CombineInstance[1] { combineInstance3 }, mergeSubMeshes: true);
			SelectionArrowZ.RecalculateNormals();
			combineInstance3.mesh = mesh2;
			combineInstance3.transform = Matrix4x4.TRS(pos, quaternion, Vector3.one);
			DisabledArrowZ = new Mesh();
			DisabledArrowZ.CombineMeshes(new CombineInstance[1] { combineInstance3 }, mergeSubMeshes: true);
			DisabledArrowZ.RecalculateNormals();
			combineInstance3.mesh = GraphicsUtility.CreateCone(m_colors.ZColor, m_handleScale);
			combineInstance3.transform = Matrix4x4.TRS(pos, quaternion, Vector3.one);
			ArrowZ = new Mesh();
			ArrowZ.CombineMeshes(new CombineInstance[1] { combineInstance3 }, mergeSubMeshes: true);
			ArrowZ.RecalculateNormals();
			combineInstance.mesh = GraphicsUtility.CreateCone(m_colors.YColor, m_handleScale);
			combineInstance2.mesh = GraphicsUtility.CreateCone(m_colors.XColor, m_handleScale);
			combineInstance3.mesh = GraphicsUtility.CreateCone(m_colors.ZColor, m_handleScale);
			Arrows = new Mesh();
			Arrows.CombineMeshes(new CombineInstance[3] { combineInstance, combineInstance2, combineInstance3 }, mergeSubMeshes: true);
			Arrows.RecalculateNormals();
			Quad = GraphicsUtility.CreateWireQuad(0.2f * m_handleScale, 0.2f * m_handleScale);
			Quads = CreatePositionHandleQuads();
			WireQuads = CreatePositionHandleWireQuads();
			SelectionCube = GraphicsUtility.CreateCube(m_colors.SelectionColor, Vector3.zero, m_handleScale, 0.1f, 0.1f, 0.1f);
			DisabledCube = GraphicsUtility.CreateCube(m_colors.DisabledColor, Vector3.zero, m_handleScale, 0.1f, 0.1f, 0.1f);
			CubeX = GraphicsUtility.CreateCube(m_colors.XColor, Vector3.zero, m_handleScale, 0.1f, 0.1f, 0.1f);
			CubeY = GraphicsUtility.CreateCube(m_colors.YColor, Vector3.zero, m_handleScale, 0.1f, 0.1f, 0.1f);
			CubeZ = GraphicsUtility.CreateCube(m_colors.ZColor, Vector3.zero, m_handleScale, 0.1f, 0.1f, 0.1f);
			CubeUniform = GraphicsUtility.CreateCube(m_colors.AltColor, Vector3.zero, m_handleScale, 0.1f, 0.1f, 0.1f);
			WireCircle = GraphicsUtility.CreateWireCircle();
			WireCircle11 = GraphicsUtility.CreateWireCircle(1.1f);
			SceneGizmoSelectedAxis = CreateSceneGizmoHalfAxis(m_colors.SelectionColor, Quaternion.AngleAxis(90f, Vector3.right));
			SceneGizmoXAxis = CreateSceneGizmoAxis(m_colors.XColor, m_colors.AltColor, Quaternion.AngleAxis(-90f, Vector3.forward));
			SceneGizmoYAxis = CreateSceneGizmoAxis(m_colors.YColor, m_colors.AltColor, Quaternion.identity);
			SceneGizmoZAxis = CreateSceneGizmoAxis(m_colors.ZColor, m_colors.AltColor, quaternion);
			SceneGizmoCube = GraphicsUtility.CreateCube(m_colors.AltColor, Vector3.zero, 1f);
			SceneGizmoSelectedCube = GraphicsUtility.CreateCube(m_colors.SelectionColor, Vector3.zero, 1f);
			SceneGizmoQuad = GraphicsUtility.CreateQuad();
		}

		private void Cleanup()
		{
			if (Axes != null)
			{
				UnityEngine.Object.Destroy(Axes);
			}
			if (Arrows != null)
			{
				UnityEngine.Object.Destroy(Arrows);
			}
			if (ArrowY != null)
			{
				UnityEngine.Object.Destroy(ArrowY);
			}
			if (ArrowZ != null)
			{
				UnityEngine.Object.Destroy(ArrowZ);
			}
			if (SelectionArrowY != null)
			{
				UnityEngine.Object.Destroy(SelectionArrowY);
			}
			if (SelectionArrowX != null)
			{
				UnityEngine.Object.Destroy(SelectionArrowX);
			}
			if (SelectionArrowZ != null)
			{
				UnityEngine.Object.Destroy(SelectionArrowZ);
			}
			if (DisabledArrowY != null)
			{
				UnityEngine.Object.Destroy(DisabledArrowY);
			}
			if (DisabledArrowX != null)
			{
				UnityEngine.Object.Destroy(DisabledArrowX);
			}
			if (DisabledArrowZ != null)
			{
				UnityEngine.Object.Destroy(DisabledArrowZ);
			}
			if (Quad != null)
			{
				UnityEngine.Object.Destroy(Quad);
			}
			if (WireQuads != null)
			{
				UnityEngine.Object.Destroy(WireQuads);
			}
			if (Quads != null)
			{
				UnityEngine.Object.Destroy(Quads);
			}
			if (SelectionCube != null)
			{
				UnityEngine.Object.Destroy(SelectionCube);
			}
			if (DisabledCube != null)
			{
				UnityEngine.Object.Destroy(DisabledCube);
			}
			if (CubeX != null)
			{
				UnityEngine.Object.Destroy(CubeX);
			}
			if (CubeY != null)
			{
				UnityEngine.Object.Destroy(CubeY);
			}
			if (CubeZ != null)
			{
				UnityEngine.Object.Destroy(CubeZ);
			}
			if (CubeUniform != null)
			{
				UnityEngine.Object.Destroy(CubeUniform);
			}
			if (WireCircle != null)
			{
				UnityEngine.Object.Destroy(WireCircle);
			}
			if (WireCircle11 != null)
			{
				UnityEngine.Object.Destroy(WireCircle11);
			}
			if (SceneGizmoSelectedAxis != null)
			{
				UnityEngine.Object.Destroy(SceneGizmoSelectedAxis);
			}
			if (SceneGizmoXAxis != null)
			{
				UnityEngine.Object.Destroy(SceneGizmoXAxis);
			}
			if (SceneGizmoYAxis != null)
			{
				UnityEngine.Object.Destroy(SceneGizmoYAxis);
			}
			if (SceneGizmoZAxis != null)
			{
				UnityEngine.Object.Destroy(SceneGizmoZAxis);
			}
			if (SceneGizmoCube != null)
			{
				UnityEngine.Object.Destroy(SceneGizmoCube);
			}
			if (SceneGizmoSelectedCube != null)
			{
				UnityEngine.Object.Destroy(SceneGizmoSelectedCube);
			}
			if (SceneGizmoQuad != null)
			{
				UnityEngine.Object.Destroy(SceneGizmoQuad);
			}
			if (m_shapesMaterialZTest != null)
			{
				UnityEngine.Object.Destroy(m_shapesMaterialZTest);
			}
			if (m_shapesMaterialZTest2 != null)
			{
				UnityEngine.Object.Destroy(m_shapesMaterialZTest2);
			}
			if (m_shapesMaterialZTest3 != null)
			{
				UnityEngine.Object.Destroy(m_shapesMaterialZTest3);
			}
			if (m_shapesMaterialZTest4 != null)
			{
				UnityEngine.Object.Destroy(m_shapesMaterialZTest4);
			}
			if (m_shapesMaterialZTestOffset != null)
			{
				UnityEngine.Object.Destroy(m_shapesMaterialZTestOffset);
			}
			if (m_linesMaterial != null)
			{
				UnityEngine.Object.Destroy(m_linesMaterial);
			}
			if (m_linesClipMaterial != null)
			{
				UnityEngine.Object.Destroy(m_linesClipMaterial);
			}
			if (m_linesClipUsingClipPlaneMaterial != null)
			{
				UnityEngine.Object.Destroy(m_linesClipUsingClipPlaneMaterial);
			}
			if (m_linesBillboardMaterial != null)
			{
				UnityEngine.Object.Destroy(m_linesBillboardMaterial);
			}
			if (m_xMaterial != null)
			{
				UnityEngine.Object.Destroy(m_xMaterial);
			}
			if (m_yMaterial != null)
			{
				UnityEngine.Object.Destroy(m_yMaterial);
			}
			if (m_zMaterial != null)
			{
				UnityEngine.Object.Destroy(m_zMaterial);
			}
			if (m_unlitColorMaterial != null)
			{
				UnityEngine.Object.Destroy(m_unlitColorMaterial);
			}
		}

		private static Mesh CreateSceneGizmoHalfAxis(Color color, Quaternion rotation)
		{
			Mesh mesh = GraphicsUtility.CreateCone(color, 1f);
			CombineInstance combineInstance = new CombineInstance
			{
				mesh = mesh,
				transform = Matrix4x4.TRS(Vector3.up * 0.1f, Quaternion.AngleAxis(180f, Vector3.right), Vector3.one)
			};
			Mesh mesh2 = new Mesh();
			mesh2.CombineMeshes(new CombineInstance[1] { combineInstance }, mergeSubMeshes: true);
			CombineInstance combineInstance2 = new CombineInstance
			{
				mesh = mesh2,
				transform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one)
			};
			mesh2 = new Mesh();
			mesh2.CombineMeshes(new CombineInstance[1] { combineInstance2 }, mergeSubMeshes: true);
			mesh2.RecalculateNormals();
			return mesh2;
		}

		private static Mesh CreateSceneGizmoAxis(Color axisColor, Color altColor, Quaternion rotation)
		{
			Mesh mesh = GraphicsUtility.CreateCone(axisColor, 1f);
			Mesh mesh2 = GraphicsUtility.CreateCone(altColor, 1f);
			CombineInstance combineInstance = new CombineInstance
			{
				mesh = mesh,
				transform = Matrix4x4.TRS(Vector3.up * 0.1f, Quaternion.AngleAxis(180f, Vector3.right), Vector3.one)
			};
			CombineInstance combineInstance2 = new CombineInstance
			{
				mesh = mesh2,
				transform = Matrix4x4.TRS(Vector3.down * 0.1f, Quaternion.identity, Vector3.one)
			};
			Mesh mesh3 = new Mesh();
			mesh3.CombineMeshes(new CombineInstance[2] { combineInstance, combineInstance2 }, mergeSubMeshes: true);
			CombineInstance combineInstance3 = new CombineInstance
			{
				mesh = mesh3,
				transform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one)
			};
			mesh3 = new Mesh();
			mesh3.CombineMeshes(new CombineInstance[1] { combineInstance3 }, mergeSubMeshes: true);
			mesh3.RecalculateNormals();
			return mesh3;
		}

		private Mesh CreateAxes()
		{
			Vector3 vector = Vector3.right * 0.95f;
			Vector3 vector2 = Vector3.up * 0.95f;
			Vector3 vector3 = Forward * 0.95f;
			Mesh mesh = new Mesh();
			mesh.subMeshCount = 3;
			mesh.vertices = new Vector3[6]
			{
				Vector3.zero,
				vector,
				Vector3.zero,
				vector2,
				Vector3.zero,
				vector3
			};
			mesh.SetIndices(new int[2] { 0, 1 }, MeshTopology.Lines, 0);
			mesh.SetIndices(new int[2] { 2, 3 }, MeshTopology.Lines, 1);
			mesh.SetIndices(new int[2] { 4, 5 }, MeshTopology.Lines, 2);
			return mesh;
		}

		private Mesh CreatePositionHandleWireQuads()
		{
			Vector3 right = Vector3.right;
			Vector3 up = Vector3.up;
			Vector3 forward = Vector3.forward;
			Vector3 vector = right + up;
			Vector3 vector2 = right + forward;
			Vector3 vector3 = up + forward;
			Mesh mesh = new Mesh();
			mesh.subMeshCount = 3;
			mesh.vertices = new Vector3[7]
			{
				Vector3.zero,
				right,
				up,
				forward,
				vector,
				vector2,
				vector3
			};
			mesh.SetIndices(new int[8] { 0, 2, 2, 4, 4, 1, 1, 0 }, MeshTopology.Lines, 0);
			mesh.SetIndices(new int[8] { 0, 1, 1, 5, 5, 3, 3, 0 }, MeshTopology.Lines, 1);
			mesh.SetIndices(new int[8] { 0, 3, 3, 6, 6, 2, 2, 0 }, MeshTopology.Lines, 2);
			return mesh;
		}

		private Mesh CreatePositionHandleQuads()
		{
			Vector3 right = Vector3.right;
			Vector3 up = Vector3.up;
			Vector3 forward = Vector3.forward;
			Vector3 vector = right + up;
			Vector3 vector2 = right + forward;
			Vector3 vector3 = up + forward;
			Mesh mesh = new Mesh();
			mesh.subMeshCount = 3;
			mesh.vertices = new Vector3[7]
			{
				Vector3.zero,
				right,
				up,
				forward,
				vector,
				vector2,
				vector3
			};
			mesh.SetIndices(new int[4] { 0, 2, 4, 1 }, MeshTopology.Quads, 0);
			mesh.SetIndices(new int[4] { 0, 1, 5, 3 }, MeshTopology.Quads, 1);
			mesh.SetIndices(new int[4] { 0, 3, 6, 2 }, MeshTopology.Quads, 2);
			return mesh;
		}

		public static float GetScreenScale(Vector3 position, Camera camera)
		{
			return GraphicsUtility.GetScreenScale(position, camera);
		}

		private void DoAxes(IRTECommandBuffer commandBuffer, MaterialPropertyBlock[] propertyBlocks, Matrix4x4 transform, RuntimeHandleAxis selectedAxis, bool xLocked, bool yLocked, bool zLocked, bool drawLocked)
		{
			if (xLocked)
			{
				if (drawLocked && m_colors.DisabledColor.a > 0)
				{
					propertyBlocks[0].SetColor("_Color", m_colors.DisabledColor);
					commandBuffer.DrawMesh(Axes, transform, m_linesMaterial, 0, 0, propertyBlocks[0]);
				}
			}
			else
			{
				propertyBlocks[0].SetColor("_Color", ((selectedAxis & RuntimeHandleAxis.X) == 0) ? m_colors.XColor : m_colors.SelectionColor);
				commandBuffer.DrawMesh(Axes, transform, m_linesMaterial, 0, 0, propertyBlocks[0]);
			}
			if (yLocked)
			{
				if (drawLocked && m_colors.DisabledColor.a > 0)
				{
					propertyBlocks[1].SetColor("_Color", m_colors.DisabledColor);
					commandBuffer.DrawMesh(Axes, transform, m_linesMaterial, 1, 0, propertyBlocks[1]);
				}
			}
			else
			{
				propertyBlocks[1].SetColor("_Color", ((selectedAxis & RuntimeHandleAxis.Y) == 0) ? m_colors.YColor : m_colors.SelectionColor);
				commandBuffer.DrawMesh(Axes, transform, m_linesMaterial, 1, 0, propertyBlocks[1]);
			}
			if (zLocked)
			{
				if (drawLocked && m_colors.DisabledColor.a > 0)
				{
					propertyBlocks[2].SetColor("_Color", m_colors.DisabledColor);
					commandBuffer.DrawMesh(Axes, transform, m_linesMaterial, 2, 0, propertyBlocks[2]);
				}
			}
			else
			{
				propertyBlocks[2].SetColor("_Color", ((selectedAxis & RuntimeHandleAxis.Z) == 0) ? m_colors.ZColor : m_colors.SelectionColor);
				commandBuffer.DrawMesh(Axes, transform, m_linesMaterial, 2, 0, propertyBlocks[2]);
			}
		}

		public void DoPositionHandle(IRTECommandBuffer commandBuffer, Camera camera, RTHDrawingSettings settings, bool snapMode = false)
		{
			settings.Init(11);
			MaterialPropertyBlock[] propertyBlocks = settings.PropertyBlocks;
			LockObject lockObject = settings.LockObject;
			RuntimeHandleAxis selectedAxis = settings.SelectedAxis;
			bool drawLocked = settings.DrawLocked;
			bool flag = lockObject?.PositionX ?? false;
			bool flag2 = lockObject?.PositionY ?? false;
			bool flag3 = lockObject?.PositionZ ?? false;
			float screenScale = GetScreenScale(settings.Position, camera);
			Matrix4x4 matrix4x = Matrix4x4.TRS(settings.Position, settings.Rotation, new Vector3(screenScale, screenScale, screenScale) * m_handleScale);
			DoAxes(commandBuffer, propertyBlocks, matrix4x, selectedAxis, flag, flag2, flag3, drawLocked);
			Matrix4x4 matrix = Matrix4x4.TRS(settings.Position, settings.Rotation, new Vector3(screenScale, screenScale, screenScale));
			if (snapMode)
			{
				if (selectedAxis == RuntimeHandleAxis.Snap)
				{
					propertyBlocks[4].SetColor("_Color", m_colors.SelectionColor);
				}
				else
				{
					propertyBlocks[4].SetColor("_Color", m_colors.AltColor);
				}
				commandBuffer.DrawMesh(Quad, matrix, m_linesBillboardMaterial, 0, 0, propertyBlocks[4]);
			}
			else if (!PositionHandleArrowOnly)
			{
				Vector3 lhs = matrix.inverse.MultiplyVector(camera.transform.position - settings.Position);
				Matrix4x4 matrix4x2 = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Mathf.Sign(Vector3.Dot(lhs, Vector3.right)) * 0.2f, Mathf.Sign(Vector3.Dot(lhs, Vector3.up)) * 0.2f, Mathf.Sign(Vector3.Dot(lhs, Vector3.forward)) * 0.2f));
				Matrix4x4 matrix2 = matrix4x * matrix4x2;
				if (!flag && !flag2)
				{
					Color32 zColor = m_colors.ZColor;
					zColor.a = 128;
					propertyBlocks[5].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.XY) ? zColor : m_colors.SelectionColor);
					commandBuffer.DrawMesh(Quads, matrix2, m_unlitColorMaterial, 0, 0, propertyBlocks[5]);
					propertyBlocks[6].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.XY) ? m_colors.ZColor : m_colors.SelectionColor);
					commandBuffer.DrawMesh(WireQuads, matrix2, m_linesMaterial, 0, 0, propertyBlocks[6]);
				}
				if (!flag && !flag3)
				{
					Color32 yColor = m_colors.YColor;
					yColor.a = 128;
					propertyBlocks[7].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.XZ) ? yColor : m_colors.SelectionColor);
					commandBuffer.DrawMesh(Quads, matrix2, m_unlitColorMaterial, 1, 0, propertyBlocks[7]);
					propertyBlocks[8].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.XZ) ? m_colors.YColor : m_colors.SelectionColor);
					commandBuffer.DrawMesh(WireQuads, matrix2, m_linesMaterial, 1, 0, propertyBlocks[8]);
				}
				if (!flag2 && !flag3)
				{
					Color32 xColor = m_colors.XColor;
					xColor.a = 128;
					propertyBlocks[9].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.YZ) ? xColor : m_colors.SelectionColor);
					commandBuffer.DrawMesh(Quads, matrix2, m_unlitColorMaterial, 2, 0, propertyBlocks[9]);
					propertyBlocks[10].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.YZ) ? m_colors.XColor : m_colors.SelectionColor);
					commandBuffer.DrawMesh(WireQuads, matrix2, m_linesMaterial, 2, 0, propertyBlocks[10]);
				}
			}
			if (!flag && !flag2 && !flag3)
			{
				commandBuffer.DrawMesh(Arrows, matrix, m_shapesMaterialZTest, 0, 0);
				if ((selectedAxis & RuntimeHandleAxis.X) != RuntimeHandleAxis.None)
				{
					commandBuffer.DrawMesh(SelectionArrowX, matrix, m_shapesMaterialZTest, 0, 0);
				}
				if ((selectedAxis & RuntimeHandleAxis.Y) != RuntimeHandleAxis.None)
				{
					commandBuffer.DrawMesh(SelectionArrowY, matrix, m_shapesMaterialZTest, 0, 0);
				}
				if ((selectedAxis & RuntimeHandleAxis.Z) != RuntimeHandleAxis.None)
				{
					commandBuffer.DrawMesh(SelectionArrowZ, matrix, m_shapesMaterialZTest, 0, 0);
				}
				return;
			}
			if (flag)
			{
				if (drawLocked)
				{
					commandBuffer.DrawMesh(DisabledArrowX, matrix, m_shapesMaterialZTest, 0, 0);
				}
			}
			else if ((selectedAxis & RuntimeHandleAxis.X) != RuntimeHandleAxis.None)
			{
				commandBuffer.DrawMesh(SelectionArrowX, matrix, m_shapesMaterialZTest, 0, 0);
			}
			else
			{
				commandBuffer.DrawMesh(ArrowX, matrix, m_shapesMaterialZTest, 0, 0);
			}
			if (flag2)
			{
				if (drawLocked)
				{
					commandBuffer.DrawMesh(DisabledArrowY, matrix, m_shapesMaterialZTest, 0, 0);
				}
			}
			else if ((selectedAxis & RuntimeHandleAxis.Y) != RuntimeHandleAxis.None)
			{
				commandBuffer.DrawMesh(SelectionArrowY, matrix, m_shapesMaterialZTest, 0, 0);
			}
			else
			{
				commandBuffer.DrawMesh(ArrowY, matrix, m_shapesMaterialZTest, 0, 0);
			}
			if (flag3)
			{
				if (drawLocked)
				{
					commandBuffer.DrawMesh(DisabledArrowZ, matrix, m_shapesMaterialZTest, 0, 0);
				}
			}
			else if ((selectedAxis & RuntimeHandleAxis.Z) != RuntimeHandleAxis.None)
			{
				commandBuffer.DrawMesh(SelectionArrowZ, matrix, m_shapesMaterialZTest, 0, 0);
			}
			else
			{
				commandBuffer.DrawMesh(ArrowZ, matrix, m_shapesMaterialZTest, 0, 0);
			}
		}

		public void DoRotationHandle(IRTECommandBuffer commandBuffer, Camera camera, RTHDrawingSettings settings, bool cameraFacingBillboardMode = true)
		{
			settings.Init(5);
			MaterialPropertyBlock[] propertyBlocks = settings.PropertyBlocks;
			LockObject lockObject = settings.LockObject;
			RuntimeHandleAxis selectedAxis = settings.SelectedAxis;
			float screenScale = GetScreenScale(settings.Position, camera);
			float handleScale = m_handleScale;
			Vector3 s = Vector3.Scale(new Vector3(screenScale, screenScale, screenScale) * handleScale, settings.Scale);
			Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.zero, settings.Rotation * Quaternion.AngleAxis(-90f, Vector3.up), Vector3.one);
			Matrix4x4 matrix4x2 = Matrix4x4.TRS(Vector3.zero, settings.Rotation * Quaternion.AngleAxis(-90f, Vector3.right), Vector3.one);
			Matrix4x4 matrix4x3 = Matrix4x4.TRS(Vector3.zero, settings.Rotation, Vector3.one);
			Matrix4x4 matrix4x4 = Matrix4x4.TRS(settings.Position, Quaternion.identity, s);
			bool drawLocked = settings.DrawLocked;
			bool num = lockObject?.RotationX ?? false;
			bool flag = lockObject?.RotationY ?? false;
			bool flag2 = lockObject?.RotationZ ?? false;
			bool num2 = lockObject?.RotationFree ?? false;
			bool flag3 = lockObject?.RotationScreen ?? false;
			Matrix4x4 matrix4x5;
			Material material;
			if (cameraFacingBillboardMode)
			{
				material = m_linesMaterial;
				matrix4x5 = Matrix4x4.TRS(settings.Position, Quaternion.LookRotation(camera.transform.position - settings.Position), s);
			}
			else
			{
				material = m_linesBillboardMaterial;
				matrix4x5 = matrix4x4;
			}
			if (num2)
			{
				if (drawLocked)
				{
					propertyBlocks[0].SetColor("_Color", m_colors.DisabledColor);
				}
			}
			else
			{
				propertyBlocks[0].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.Free) ? m_colors.AltColor : m_colors.SelectionColor);
			}
			GraphicsUtility.DrawMesh(commandBuffer, WireCircle, matrix4x5, material, propertyBlocks[0]);
			if (flag3)
			{
				if (drawLocked)
				{
					propertyBlocks[1].SetColor("_Color", m_colors.DisabledColor);
				}
			}
			else
			{
				propertyBlocks[1].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.Screen) ? m_colors.AltColor : m_colors.SelectionColor);
			}
			GraphicsUtility.DrawMesh(commandBuffer, WireCircle11, matrix4x5, material, propertyBlocks[1]);
			material = ((!cameraFacingBillboardMode) ? m_linesClipMaterial : m_linesClipUsingClipPlaneMaterial);
			if (num)
			{
				if (drawLocked)
				{
					propertyBlocks[2].SetColor("_Color", m_colors.DisabledColor);
				}
			}
			else
			{
				propertyBlocks[2].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.X) ? m_colors.XColor : m_colors.SelectionColor);
			}
			GraphicsUtility.DrawMesh(commandBuffer, WireCircle, matrix4x4 * matrix4x, material, propertyBlocks[2]);
			if (flag)
			{
				if (drawLocked)
				{
					propertyBlocks[3].SetColor("_Color", m_colors.DisabledColor);
				}
			}
			else
			{
				propertyBlocks[3].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.Y) ? m_colors.YColor : m_colors.SelectionColor);
			}
			GraphicsUtility.DrawMesh(commandBuffer, WireCircle, matrix4x4 * matrix4x2, material, propertyBlocks[3]);
			if (flag2)
			{
				if (drawLocked)
				{
					propertyBlocks[4].SetColor("_Color", m_colors.DisabledColor);
				}
			}
			else
			{
				propertyBlocks[4].SetColor("_Color", (selectedAxis != RuntimeHandleAxis.Z) ? m_colors.ZColor : m_colors.SelectionColor);
			}
			GraphicsUtility.DrawMesh(commandBuffer, WireCircle, matrix4x4 * matrix4x3, material, propertyBlocks[4]);
		}

		public void DoScaleHandle(IRTECommandBuffer commandBuffer, Camera camera, RTHDrawingSettings settings, bool isUniform = false)
		{
			settings.Init(3);
			MaterialPropertyBlock[] propertyBlocks = settings.PropertyBlocks;
			LockObject lockObject = settings.LockObject;
			RuntimeHandleAxis selectedAxis = settings.SelectedAxis;
			Vector3 position = settings.Position;
			Quaternion rotation = settings.Rotation;
			Vector3 scale = settings.Scale;
			float screenScale = GetScreenScale(position, camera);
			Matrix4x4 matrix4x = Matrix4x4.TRS(position, rotation, scale * screenScale * m_handleScale);
			bool drawLocked = settings.DrawLocked;
			bool flag = lockObject?.ScaleX ?? false;
			bool flag2 = lockObject?.ScaleY ?? false;
			bool flag3 = lockObject?.ScaleZ ?? false;
			bool flag4 = flag && flag2 && flag3;
			if (isUniform)
			{
				RuntimeHandleAxis selectedAxis2 = ((selectedAxis != RuntimeHandleAxis.None) ? RuntimeHandleAxis.XYZ : RuntimeHandleAxis.None);
				DoAxes(commandBuffer, propertyBlocks, matrix4x, selectedAxis2, flag, flag2, flag3, drawLocked);
			}
			else
			{
				DoAxes(commandBuffer, propertyBlocks, matrix4x, selectedAxis, flag, flag2, flag3, drawLocked);
			}
			Matrix4x4 matrix4x2 = Matrix4x4.TRS(Vector3.zero, rotation, scale);
			Vector3 vector = new Vector3(screenScale, screenScale, screenScale);
			Vector3 vector2 = matrix4x2.MultiplyVector(Vector3.right) * screenScale * m_handleScale;
			Vector3 vector3 = matrix4x2.MultiplyVector(Vector3.up) * screenScale * m_handleScale;
			Vector3 vector4 = matrix4x2.MultiplyPoint(Forward) * screenScale * m_handleScale;
			drawLocked = drawLocked && m_colors.DisabledColor.a > 0;
			if (isUniform && selectedAxis != RuntimeHandleAxis.None)
			{
				DrawMesh(commandBuffer, drawLocked || !flag, flag ? DisabledCube : SelectionCube, Matrix4x4.TRS(position + vector2, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag2, flag2 ? DisabledCube : SelectionCube, Matrix4x4.TRS(position + vector3, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag3, flag3 ? DisabledCube : SelectionCube, Matrix4x4.TRS(position + vector4, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag4, flag4 ? DisabledCube : SelectionCube, Matrix4x4.TRS(position, rotation, vector * 1.35f), m_shapesMaterialZTest);
				return;
			}
			switch (selectedAxis)
			{
			case RuntimeHandleAxis.X:
				DrawMesh(commandBuffer, drawLocked || !flag, flag ? DisabledCube : SelectionCube, Matrix4x4.TRS(position + vector2, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag2, flag2 ? DisabledCube : CubeY, Matrix4x4.TRS(position + vector3, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag3, flag3 ? DisabledCube : CubeZ, Matrix4x4.TRS(position + vector4, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag4, flag4 ? DisabledCube : CubeUniform, Matrix4x4.TRS(position, rotation, vector * 1.35f), m_shapesMaterialZTest);
				break;
			case RuntimeHandleAxis.Y:
				DrawMesh(commandBuffer, drawLocked || !flag, flag ? DisabledCube : CubeX, Matrix4x4.TRS(position + vector2, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag2, flag2 ? DisabledCube : SelectionCube, Matrix4x4.TRS(position + vector3, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag3, flag3 ? DisabledCube : CubeZ, Matrix4x4.TRS(position + vector4, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag4, flag4 ? DisabledCube : CubeUniform, Matrix4x4.TRS(position, rotation, vector * 1.35f), m_shapesMaterialZTest);
				break;
			case RuntimeHandleAxis.Z:
				DrawMesh(commandBuffer, drawLocked || !flag, flag ? DisabledCube : CubeX, Matrix4x4.TRS(position + vector2, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag2, flag2 ? DisabledCube : CubeY, Matrix4x4.TRS(position + vector3, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag3, flag3 ? DisabledCube : SelectionCube, Matrix4x4.TRS(position + vector4, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag4, flag4 ? DisabledCube : CubeUniform, Matrix4x4.TRS(position, rotation, vector * 1.35f), m_shapesMaterialZTest);
				break;
			case RuntimeHandleAxis.Free:
				DrawMesh(commandBuffer, drawLocked || !flag, flag ? DisabledCube : CubeX, Matrix4x4.TRS(position + vector2, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag2, flag2 ? DisabledCube : CubeY, Matrix4x4.TRS(position + vector3, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag3, flag3 ? DisabledCube : CubeZ, Matrix4x4.TRS(position + vector4, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag4, flag4 ? DisabledCube : SelectionCube, Matrix4x4.TRS(position, rotation, vector * 1.35f), m_shapesMaterialZTest);
				break;
			default:
				DrawMesh(commandBuffer, drawLocked || !flag, flag ? DisabledCube : CubeX, Matrix4x4.TRS(position + vector2, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag2, flag2 ? DisabledCube : CubeY, Matrix4x4.TRS(position + vector3, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag3, flag3 ? DisabledCube : CubeZ, Matrix4x4.TRS(position + vector4, rotation, vector), m_shapesMaterialZTest);
				DrawMesh(commandBuffer, drawLocked || !flag4, flag4 ? DisabledCube : CubeUniform, Matrix4x4.TRS(position, rotation, vector * 1.35f), m_shapesMaterialZTest);
				break;
			}
		}

		private void DrawMesh(IRTECommandBuffer commandBuffer, bool draw, Mesh mesh, Matrix4x4 matrix, Material material)
		{
			if (draw)
			{
				commandBuffer.DrawMesh(mesh, matrix, material, 0, 0);
			}
		}

		public void DoSceneGizmo(IRTECommandBuffer commandBuffer, MaterialPropertyBlock[] propertyBlocks, Camera camera, Vector3 position, Quaternion rotation, Vector3 selection, float gizmoScale, Color textColor, float xAlpha = 1f, float yAlpha = 1f, float zAlpha = 1f)
		{
			float num = GetScreenScale(position, camera) * gizmoScale;
			Vector3 vector = new Vector3(num, num, num);
			float billboardOffset = 0.4f;
			if (camera.orthographic)
			{
				billboardOffset = 0.42f;
			}
			if (selection != Vector3.zero)
			{
				if (selection == Vector3.one)
				{
					commandBuffer.DrawMesh(SceneGizmoSelectedCube, Matrix4x4.TRS(position, rotation, vector * 0.15f), m_shapesMaterialZTestOffset, 0, -1);
				}
				else if ((xAlpha == 1f || xAlpha == 0f) && (yAlpha == 1f || yAlpha == 0f) && (zAlpha == 1f || zAlpha == 0f))
				{
					commandBuffer.DrawMesh(SceneGizmoSelectedAxis, Matrix4x4.TRS(position, rotation * Quaternion.LookRotation(selection, Vector3.up), vector), m_shapesMaterialZTestOffset, 0, -1);
				}
			}
			m_shapesMaterialZTest.color = Color.white;
			commandBuffer.DrawMesh(SceneGizmoCube, Matrix4x4.TRS(position, rotation, vector * 0.15f), m_shapesMaterialZTest, 0, -1);
			if (xAlpha == 1f && yAlpha == 1f && zAlpha == 1f)
			{
				propertyBlocks[0].SetColor("_Color", new Color(1f, 1f, 1f, 1f));
				commandBuffer.DrawMesh(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest3, 0, -1, propertyBlocks[0]);
				propertyBlocks[1].SetColor("_Color", new Color(1f, 1f, 1f, 1f));
				commandBuffer.DrawMesh(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest4, 0, -1, propertyBlocks[1]);
				propertyBlocks[2].SetColor("_Color", new Color(1f, 1f, 1f, 1f));
				commandBuffer.DrawMesh(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest2, 0, -1, propertyBlocks[2]);
			}
			else if (xAlpha < 1f)
			{
				propertyBlocks[0].SetColor("_Color", new Color(1f, 1f, 1f, yAlpha));
				commandBuffer.DrawMesh(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest3, 0, -1, propertyBlocks[0]);
				propertyBlocks[1].SetColor("_Color", new Color(1f, 1f, 1f, zAlpha));
				commandBuffer.DrawMesh(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest4, 0, -1, propertyBlocks[1]);
				propertyBlocks[2].SetColor("_Color", new Color(1f, 1f, 1f, xAlpha));
				commandBuffer.DrawMesh(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest2, 0, -1, propertyBlocks[2]);
			}
			else if (yAlpha < 1f)
			{
				propertyBlocks[0].SetColor("_Color", new Color(1f, 1f, 1f, zAlpha));
				commandBuffer.DrawMesh(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest4, 0, -1, propertyBlocks[0]);
				propertyBlocks[1].SetColor("_Color", new Color(1f, 1f, 1f, xAlpha));
				commandBuffer.DrawMesh(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest2, 0, -1, propertyBlocks[1]);
				propertyBlocks[2].SetColor("_Color", new Color(1f, 1f, 1f, yAlpha));
				commandBuffer.DrawMesh(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest3, 0, -1, propertyBlocks[2]);
			}
			else
			{
				propertyBlocks[0].SetColor("_Color", new Color(1f, 1f, 1f, xAlpha));
				commandBuffer.DrawMesh(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest2, 0, -1, propertyBlocks[0]);
				propertyBlocks[1].SetColor("_Color", new Color(1f, 1f, 1f, yAlpha));
				commandBuffer.DrawMesh(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest3, 0, -1, propertyBlocks[1]);
				propertyBlocks[2].SetColor("_Color", new Color(1f, 1f, 1f, zAlpha));
				commandBuffer.DrawMesh(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, vector), m_shapesMaterialZTest4, 0, -1, propertyBlocks[2]);
			}
			Color color = textColor;
			propertyBlocks[3].SetColor("_Color", new Color(color.r, color.b, color.g, xAlpha));
			DragSceneGizmoAxis(commandBuffer, propertyBlocks[3], m_xMaterial, camera, position, rotation, Vector3.right, gizmoScale, 0.125f, billboardOffset, num);
			propertyBlocks[4].SetColor("_Color", new Color(color.r, color.b, color.g, yAlpha));
			DragSceneGizmoAxis(commandBuffer, propertyBlocks[4], m_yMaterial, camera, position, rotation, Vector3.up, gizmoScale, 0.125f, billboardOffset, num);
			propertyBlocks[5].SetColor("_Color", new Color(color.r, color.b, color.g, zAlpha));
			DragSceneGizmoAxis(commandBuffer, propertyBlocks[5], m_zMaterial, camera, position, rotation, Forward, gizmoScale, 0.125f, billboardOffset, num);
		}

		private void DragSceneGizmoAxis(IRTECommandBuffer cmdBuffer, MaterialPropertyBlock propertyBlock, Material material, Camera camera, Vector3 position, Quaternion rotation, Vector3 axis, float gizmoScale, float billboardScale, float billboardOffset, float sScale)
		{
			Vector3 vector = Vector3.Reflect(camera.transform.forward, axis) * 0.1f;
			float num = Vector3.Dot(camera.transform.forward, axis);
			if (num > 0f)
			{
				if (camera.orthographic)
				{
					vector += axis * num * 0.4f;
				}
				else
				{
					vector = axis * num * 0.7f;
				}
			}
			else if (camera.orthographic)
			{
				vector -= axis * num * 0.1f;
			}
			else
			{
				vector = Vector3.zero;
			}
			Vector3 vector2 = position + (axis + vector) * billboardOffset * sScale;
			float num2 = GetScreenScale(vector2, camera) * gizmoScale;
			cmdBuffer.DrawMesh(matrix: Matrix4x4.TRS(vector2, rotation, new Vector3(num2, num2, num2) * billboardScale), mesh: SceneGizmoQuad, material: material, submeshIndex: 0, shaderPass: -1, properties: propertyBlock);
		}

		public Mesh CreateGridMesh(Color color, float spacing, int linesCount = 150)
		{
			int num = linesCount / 2;
			Mesh mesh = new Mesh();
			mesh.name = "Grid " + spacing;
			int num2 = 0;
			int[] array = new int[num * 8];
			Vector3[] array2 = new Vector3[num * 8];
			Color[] array3 = new Color[num * 8];
			for (int i = -num; i < num; i++)
			{
				array2[num2] = new Vector3((float)i * spacing, 0f, (float)(-num) * spacing);
				array2[num2 + 1] = new Vector3((float)i * spacing, 0f, (float)num * spacing);
				array2[num2 + 2] = new Vector3((float)(-num) * spacing, 0f, (float)i * spacing);
				array2[num2 + 3] = new Vector3((float)num * spacing, 0f, (float)i * spacing);
				array[num2] = num2;
				array[num2 + 1] = num2 + 1;
				array[num2 + 2] = num2 + 2;
				array[num2 + 3] = num2 + 3;
				array3[num2] = (array3[num2 + 1] = (array3[num2 + 2] = (array3[num2 + 3] = color)));
				num2 += 4;
			}
			mesh.vertices = array2;
			mesh.SetIndices(array, MeshTopology.Lines, 0);
			mesh.colors = array3;
			return mesh;
		}

		public Mesh CreateRawImageMesh()
		{
			Mesh mesh = new Mesh();
			mesh.name = "Quad";
			mesh.vertices = new Vector3[4]
			{
				new Vector3(-0.5f, -0.5f, 0f),
				new Vector3(-0.5f, 0.5f, 0f),
				new Vector3(0.5f, 0.5f, 0f),
				new Vector3(0.5f, -0.5f, 0f)
			};
			mesh.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
			mesh.uv = new Vector2[4]
			{
				Vector2.zero,
				Vector2.up,
				Vector2.up + Vector2.right,
				Vector2.right
			};
			return mesh;
		}

		public RuntimeHandleAxis HitTestPositionHandle(Camera camera, Ray ray, RTHDrawingSettings settings, out float distance)
		{
			LockObject lockObject = settings.LockObject;
			Vector3 position = settings.Position;
			Quaternion rotation = settings.Rotation;
			Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, InvertZAxis ? new Vector3(1f, 1f, -1f) : Vector3.one);
			float screenScale = GetScreenScale(position, camera);
			if (!PositionHandleArrowOnly)
			{
				float num = 0.23f * screenScale;
				if ((lockObject == null || (!lockObject.PositionX && !lockObject.PositionZ)) && HitQuad(camera, ray, position, Vector3.up, matrix, num * HandleScale, out distance))
				{
					return RuntimeHandleAxis.XZ;
				}
				if ((lockObject == null || (!lockObject.PositionY && !lockObject.PositionZ)) && HitQuad(camera, ray, position, Vector3.right, matrix, num * HandleScale, out distance))
				{
					return RuntimeHandleAxis.YZ;
				}
				if ((lockObject == null || (!lockObject.PositionX && !lockObject.PositionY)) && HitQuad(camera, ray, position, Vector3.forward, matrix, num * HandleScale, out distance))
				{
					return RuntimeHandleAxis.XY;
				}
			}
			Matrix4x4 matrix2 = Matrix4x4.TRS(position, rotation, new Vector3(screenScale, screenScale, screenScale));
			float distanceToAxis = float.MaxValue;
			float distanceToAxis2 = float.MaxValue;
			float distanceToAxis3 = float.MaxValue;
			if (((lockObject == null || !lockObject.PositionY) && HitAxis(camera, ray, Vector3.up * HandleScale, matrix2, out distanceToAxis)) | ((lockObject == null || !lockObject.PositionZ) && HitAxis(camera, ray, Forward * HandleScale, matrix2, out distanceToAxis2)) | ((lockObject == null || !lockObject.PositionX) && HitAxis(camera, ray, Vector3.right * HandleScale, matrix2, out distanceToAxis3)))
			{
				if (distanceToAxis <= distanceToAxis2 && distanceToAxis <= distanceToAxis3)
				{
					distance = distanceToAxis;
					return RuntimeHandleAxis.Y;
				}
				if (distanceToAxis3 <= distanceToAxis && distanceToAxis3 <= distanceToAxis2)
				{
					distance = distanceToAxis3;
					return RuntimeHandleAxis.X;
				}
				distance = distanceToAxis2;
				return RuntimeHandleAxis.Z;
			}
			distance = float.PositiveInfinity;
			return RuntimeHandleAxis.None;
		}

		private bool HitQuad(Camera camera, Ray ray, Vector3 position, Vector3 axis, Matrix4x4 matrix, float size, out float distance)
		{
			Plane plane = new Plane(matrix.MultiplyVector(axis).normalized, matrix.MultiplyPoint(Vector3.zero));
			if (!plane.Raycast(ray, out distance))
			{
				return false;
			}
			Vector3 point = ray.GetPoint(distance);
			point = matrix.inverse.MultiplyPoint(point);
			Vector3 lhs = matrix.inverse.MultiplyVector(camera.transform.position - position);
			float num = Mathf.Sign(Vector3.Dot(lhs, Vector3.right));
			float num2 = Mathf.Sign(Vector3.Dot(lhs, Vector3.up));
			float num3 = Mathf.Sign(Vector3.Dot(lhs, Vector3.forward));
			point.x *= num;
			point.y *= num2;
			point.z *= num3;
			float num4 = -0.01f;
			if (point.x >= num4 && point.x <= size && point.y >= num4 && point.y <= size && point.z >= num4)
			{
				return point.z <= size;
			}
			return false;
		}

		private bool GetScreenPosition(Camera camera, Ray ray, Vector3 position, out Vector2 screenPosition)
		{
			if (!new Plane(-camera.transform.forward, position).Raycast(ray, out var enter))
			{
				screenPosition = Vector2.zero;
				return false;
			}
			screenPosition = camera.WorldToScreenPoint(ray.GetPoint(enter));
			return true;
		}

		private bool HitAxis(Camera camera, Ray ray, Vector3 axis, Matrix4x4 matrix, out float distanceToAxis)
		{
			Vector3 vector = matrix.GetColumn(3);
			axis = matrix.MultiplyVector(axis);
			Vector2 vector2 = camera.WorldToScreenPoint(vector);
			Vector3 vector3 = (Vector2)camera.WorldToScreenPoint(axis + vector) - vector2;
			float magnitude = vector3.magnitude;
			vector3.Normalize();
			if (!GetScreenPosition(camera, ray, vector, out var screenPosition))
			{
				distanceToAxis = float.PositiveInfinity;
				return false;
			}
			if (vector3 != Vector3.zero)
			{
				return HitScreenAxis(screenPosition, vector2, vector3, magnitude, out distanceToAxis);
			}
			distanceToAxis = (vector2 - screenPosition).magnitude;
			bool num = distanceToAxis <= SelectionMargin * m_selectionMarginPixels;
			if (!num)
			{
				distanceToAxis = float.PositiveInfinity;
				return num;
			}
			distanceToAxis = 0f;
			return num;
		}

		private bool HitScreenAxis(Vector2 screenPosition, Vector2 screenVectorBegin, Vector3 screenVector, float screenVectorMag, out float distanceToAxis)
		{
			Vector2 normalized = PerpendicularClockwise(screenVector).normalized;
			Vector2 vector = screenPosition - screenVectorBegin;
			distanceToAxis = Mathf.Abs(Vector2.Dot(normalized, vector));
			Vector2 rhs = vector - normalized * distanceToAxis;
			float num = Vector2.Dot(screenVector, rhs);
			float num2 = SelectionMargin * m_selectionMarginPixels;
			int num3;
			if (num <= screenVectorMag + num2 && num >= 0f - num2)
			{
				num3 = ((distanceToAxis <= num2) ? 1 : 0);
				if (num3 != 0)
				{
					if (screenVectorMag < num2)
					{
						distanceToAxis = 0f;
					}
					return (byte)num3 != 0;
				}
			}
			else
			{
				num3 = 0;
			}
			distanceToAxis = float.PositiveInfinity;
			return (byte)num3 != 0;
		}

		private static Vector2 PerpendicularClockwise(Vector2 vector2)
		{
			return new Vector2(0f - vector2.y, vector2.x);
		}

		public RuntimeHandleAxis HitTestRotationHandle(Camera camera, Ray ray, RTHDrawingSettings settings, out float distance)
		{
			Vector3 position = settings.Position;
			Quaternion identity = Quaternion.identity;
			float num = GetScreenScale(position, camera) * HandleScale;
			if (Intersect(ray, position, 1.2f * num, out var _, out var _))
			{
				RuntimeHandleAxis runtimeHandleAxis = HitAxis(camera, ray, settings, identity, out distance);
				Vector3 axis = Vector3.zero;
				switch (runtimeHandleAxis)
				{
				case RuntimeHandleAxis.X:
					axis = Vector3.right;
					break;
				case RuntimeHandleAxis.Y:
					axis = Vector3.up;
					break;
				case RuntimeHandleAxis.Z:
					axis = Vector3.forward;
					break;
				}
				GetPointOnDragPlane(GetDragPlane(camera, axis, settings), ray, out var point);
				if (runtimeHandleAxis != RuntimeHandleAxis.None)
				{
					return runtimeHandleAxis;
				}
				if ((point - position).magnitude <= 1f * num)
				{
					return RuntimeHandleAxis.Free;
				}
				return RuntimeHandleAxis.Screen;
			}
			distance = float.MaxValue;
			return RuntimeHandleAxis.None;
		}

		protected Plane GetDragPlane(Camera camera, Vector3 axis, RTHDrawingSettings settings)
		{
			return new Plane(((!Mathf.Approximately(Mathf.Abs(Vector3.Dot(camera.transform.forward, settings.Rotation * axis)), 1f)) ? camera.cameraToWorldMatrix.MultiplyVector(Vector3.forward) : (camera.transform.position - base.transform.position)).normalized, base.transform.position);
		}

		private bool GetPointOnDragPlane(Plane dragPlane, Ray ray, out Vector3 point)
		{
			if (dragPlane.Raycast(ray, out var enter))
			{
				point = ray.GetPoint(enter);
				return true;
			}
			point = Vector3.zero;
			return false;
		}

		private bool Intersect(Ray r, Vector3 sphereCenter, float sphereRadius, out float hit1Distance, out float hit2Distance)
		{
			hit1Distance = 0f;
			hit2Distance = 0f;
			Vector3 vector = sphereCenter - r.origin;
			float num = Vector3.Dot(vector, r.direction);
			if ((double)num < 0.0)
			{
				return false;
			}
			float num2 = Vector3.Dot(vector, vector) - num * num;
			float num3 = sphereRadius * sphereRadius;
			if (num2 > num3)
			{
				return false;
			}
			float num4 = Mathf.Sqrt(num3 - num2);
			hit1Distance = num - num4;
			hit2Distance = num + num4;
			return true;
		}

		private RuntimeHandleAxis HitAxis(Camera camera, Ray ray, RTHDrawingSettings settings, Quaternion startingRotationInv, out float distance)
		{
			Vector3 position = settings.Position;
			Quaternion rotation = settings.Rotation;
			float num = GetScreenScale(position, camera) * HandleScale;
			Vector3 s = new Vector3(num, num, num);
			Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.zero, rotation * startingRotationInv * Quaternion.AngleAxis(-90f, Vector3.up), Vector3.one);
			Matrix4x4 matrix4x2 = Matrix4x4.TRS(Vector3.zero, rotation * startingRotationInv * Quaternion.AngleAxis(-90f, Vector3.right), Vector3.one);
			Matrix4x4 matrix4x3 = Matrix4x4.TRS(Vector3.zero, rotation * startingRotationInv, Vector3.one);
			Matrix4x4 objToWorld = Matrix4x4.TRS(position, Quaternion.identity, s);
			float minDistance;
			bool num2 = HitAxis(camera, ray, matrix4x, objToWorld, out minDistance);
			float minDistance2;
			bool flag = HitAxis(camera, ray, matrix4x2, objToWorld, out minDistance2);
			float minDistance3;
			bool flag2 = HitAxis(camera, ray, matrix4x3, objToWorld, out minDistance3);
			if (num2 && minDistance < minDistance2 && minDistance < minDistance3)
			{
				distance = minDistance;
				return RuntimeHandleAxis.X;
			}
			if (flag && minDistance2 < minDistance && minDistance2 < minDistance3)
			{
				distance = minDistance2;
				return RuntimeHandleAxis.Y;
			}
			if (flag2 && minDistance3 < minDistance && minDistance3 < minDistance2)
			{
				distance = minDistance3;
				return RuntimeHandleAxis.Z;
			}
			distance = float.MaxValue;
			return RuntimeHandleAxis.None;
		}

		private bool HitAxis(Camera camera, Ray ray, Matrix4x4 transform, Matrix4x4 objToWorld, out float minDistance)
		{
			bool result = false;
			minDistance = float.PositiveInfinity;
			float num = 0f;
			float z = 0f;
			Vector3 point = transform.MultiplyPoint(Vector3.zero);
			point = objToWorld.MultiplyPoint(point);
			point = camera.worldToCameraMatrix.MultiplyPoint(point);
			Vector3 point2 = transform.MultiplyPoint(new Vector3(1f, 0f, z));
			point2 = objToWorld.MultiplyPoint(point2);
			for (int i = 0; i < 32; i++)
			{
				num += MathF.PI / 16f;
				float x = 1f * Mathf.Cos(num);
				float y = 1f * Mathf.Sin(num);
				Vector3 point3 = transform.MultiplyPoint(new Vector3(x, y, z));
				point3 = objToWorld.MultiplyPoint(point3);
				if (camera.worldToCameraMatrix.MultiplyPoint(point3).z >= point.z)
				{
					Vector3 vector = camera.WorldToScreenPoint(point3) - camera.WorldToScreenPoint(point2);
					float magnitude = vector.magnitude;
					vector.Normalize();
					if (vector != Vector3.zero)
					{
						Vector3 position = objToWorld.GetColumn(3);
						if (!GetScreenPosition(camera, ray, position, out var screenPosition))
						{
							return false;
						}
						if (HitScreenAxis(screenPosition, camera.WorldToScreenPoint(point2), vector, magnitude, out var distanceToAxis) && distanceToAxis < minDistance)
						{
							minDistance = distanceToAxis;
							result = true;
							break;
						}
					}
				}
				point2 = point3;
			}
			return result;
		}

		public RuntimeHandleAxis HitTestScaleHandle(Camera camera, Ray ray, RTHDrawingSettings settings, out float distance)
		{
			Vector3 position = settings.Position;
			float num = GetScreenScale(position, camera) * HandleScale;
			Matrix4x4 matrix = Matrix4x4.TRS(position, settings.Rotation, new Vector3(num, num, num));
			if (HitCenter(camera, ray, position, out distance))
			{
				return RuntimeHandleAxis.Free;
			}
			if (HitAxis(camera, ray, Vector3.up, matrix, out var distanceToAxis) | HitAxis(camera, ray, Forward, matrix, out var distanceToAxis2) | HitAxis(camera, ray, Vector3.right, matrix, out var distanceToAxis3))
			{
				if (distanceToAxis <= distanceToAxis2 && distanceToAxis <= distanceToAxis3)
				{
					distance = distanceToAxis;
					return RuntimeHandleAxis.Y;
				}
				if (distanceToAxis3 <= distanceToAxis && distanceToAxis3 <= distanceToAxis2)
				{
					distance = distanceToAxis3;
					return RuntimeHandleAxis.X;
				}
				distance = distanceToAxis2;
				return RuntimeHandleAxis.Z;
			}
			distance = float.PositiveInfinity;
			return RuntimeHandleAxis.None;
		}

		protected virtual bool HitCenter(Camera camera, Ray ray, Vector3 position, out float distance)
		{
			Vector2 vector = camera.WorldToScreenPoint(position);
			if (!GetScreenPosition(camera, ray, position, out var screenPosition))
			{
				distance = float.PositiveInfinity;
				return false;
			}
			distance = (screenPosition - vector).magnitude;
			return distance <= SelectionMargin * m_selectionMarginPixels;
		}

		public void DoPositionHandle(CommandBuffer commandBuffer, Camera camera, RTHDrawingSettings settings, bool snapMode = false)
		{
			s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
			DoPositionHandle(s_commandBufferWrapper, camera, settings, snapMode);
		}

		public void DoRotationHandle(CommandBuffer commandBuffer, Camera camera, RTHDrawingSettings settings, bool cameraFacingBillboardMode = true)
		{
			s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
			DoRotationHandle(s_commandBufferWrapper, camera, settings, cameraFacingBillboardMode);
		}

		public void DoScaleHandle(CommandBuffer commandBuffer, Camera camera, RTHDrawingSettings settings, bool isUniform = false)
		{
			s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
			DoScaleHandle(s_commandBufferWrapper, camera, settings, isUniform);
		}

		public void DoSceneGizmo(CommandBuffer commandBuffer, MaterialPropertyBlock[] propertyBlocks, Camera camera, Vector3 position, Quaternion rotation, Vector3 selection, float gizmoScale, Color textColor, float xAlpha = 1f, float yAlpha = 1f, float zAlpha = 1f)
		{
			s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
			DoSceneGizmo(s_commandBufferWrapper, propertyBlocks, camera, position, rotation, selection, gizmoScale, textColor, xAlpha, yAlpha, zAlpha);
		}
	}
}
