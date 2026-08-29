using Battlehub.RTCommon;
using Battlehub.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTHandles
{
	public class SceneGrid : RTEComponent
	{
		public RuntimeHandlesComponent Appearance;

		private Mesh m_grid0Mesh;

		private Mesh m_grid1Mesh;

		private Material m_grid0Material;

		private Material m_grid1Material;

		[SerializeField]
		private Vector3 m_gridOffset = new Vector3(0f, 0.01f, 0f);

		private RTECamera m_rteCamera;

		private float m_gridSize = 0.5f;

		private bool m_zTest = true;

		[SerializeField]
		private float m_alpha = 1f;

		public float SizeOfGrid
		{
			get
			{
				return m_gridSize;
			}
			set
			{
				if (m_gridSize != value)
				{
					m_gridSize = value;
					Rebuild();
				}
			}
		}

		public bool ZTest
		{
			get
			{
				return m_zTest;
			}
			set
			{
				if (m_zTest != value)
				{
					m_zTest = value;
					Rebuild();
				}
			}
		}

		public float Alpha
		{
			get
			{
				return m_alpha;
			}
			set
			{
				m_alpha = Mathf.Clamp01(value);
			}
		}

		protected override void Awake()
		{
			base.Awake();
			RuntimeHandlesComponent.InitializeIfRequired(ref Appearance);
		}

		protected override void Start()
		{
			base.Start();
			Init();
		}

		protected virtual void OnEnable()
		{
			if (base.IsStarted)
			{
				Init();
			}
		}

		protected virtual void OnDisable()
		{
			if (m_rteCamera != null)
			{
				m_rteCamera.CommandBufferRefresh -= OnCommandBufferRefresh;
			}
			Object.Destroy(m_rteCamera);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Cleanup();
		}

		private void Update()
		{
			if (m_rteCamera.RTECommandBufferOverride == null)
			{
				m_rteCamera.RefreshCommandBuffer();
			}
		}

		private void Init()
		{
			m_rteCamera = Window.Camera.gameObject.AddComponent<RTECamera>();
			m_rteCamera.Event = CameraEvent.AfterForwardAlpha;
			m_rteCamera.CommandBufferRefresh += OnCommandBufferRefresh;
			Rebuild();
			m_rteCamera.RefreshCommandBuffer();
		}

		private void Cleanup()
		{
			if (m_grid0Material != null)
			{
				Object.Destroy(m_grid0Material);
			}
			if (m_grid1Material != null)
			{
				Object.Destroy(m_grid1Material);
			}
			if (m_grid0Mesh != null)
			{
				Object.Destroy(m_grid0Mesh);
			}
			if (m_grid1Mesh != null)
			{
				Object.Destroy(m_grid1Mesh);
			}
		}

		private void Rebuild()
		{
			Cleanup();
			if (!(Appearance == null))
			{
				m_grid0Material = CreateGridMaterial(0.5f, m_zTest);
				m_grid1Material = CreateGridMaterial(0.5f, m_zTest);
				m_grid0Mesh = Appearance.CreateGridMesh(Appearance.Colors.GridColor, m_gridSize);
				m_grid1Mesh = Appearance.CreateGridMesh(Appearance.Colors.GridColor, m_gridSize);
			}
		}

		private void OnCommandBufferRefresh(IRTECamera obj)
		{
			float cameraOffset = GetCameraOffset();
			cameraOffset = Mathf.Abs(cameraOffset);
			cameraOffset = Mathf.Max(1f, cameraOffset);
			float num = MathHelper.CountOfDigits(cameraOffset);
			float fadeDistance = cameraOffset * 10f;
			float alpha = GetAlpha(0, cameraOffset, num);
			float alpha2 = GetAlpha(1, cameraOffset, num);
			SetGridAlpha(m_grid0Material, alpha * m_alpha, fadeDistance);
			SetGridAlpha(m_grid1Material, alpha2 * m_alpha, fadeDistance);
			float num2 = Mathf.Pow(10f, num - 1f);
			float num3 = Mathf.Pow(10f, num);
			Matrix4x4 matrix = base.transform.localToWorldMatrix * Matrix4x4.TRS(GetGridPostion(num2), Quaternion.identity, Vector3.one * num2);
			Matrix4x4 matrix2 = base.transform.localToWorldMatrix * Matrix4x4.TRS(GetGridPostion(num3), Quaternion.identity, Vector3.one * num3);
			IRTECommandBuffer rTECommandBuffer = m_rteCamera.RTECommandBuffer;
			rTECommandBuffer.DrawMesh(m_grid0Mesh, matrix, m_grid0Material, 0, 0);
			rTECommandBuffer.DrawMesh(m_grid1Mesh, matrix2, m_grid1Material, 0, 0);
		}

		private Vector3 GetGridPostion(float spacing)
		{
			Vector3 position = Window.Camera.transform.position;
			position = base.transform.InverseTransformPoint(position);
			spacing *= m_gridSize;
			position.x = Mathf.Floor(position.x / spacing) * spacing;
			position.z = Mathf.Floor(position.z / spacing) * spacing;
			position.y = 0f;
			return position + m_gridOffset;
		}

		private void SetGridAlpha(Material gridMaterial, float alpha, float fadeDistance)
		{
			Color color = gridMaterial.GetColor("_GridColor");
			color.a = alpha;
			gridMaterial.SetColor("_GridColor", color);
			gridMaterial.SetFloat("_FadeDistance", fadeDistance);
			if (Window.Camera.orthographic)
			{
				gridMaterial.SetFloat("_CameraSize", Window.Camera.orthographicSize);
			}
		}

		private Material CreateGridMaterial(float scale, bool zTest)
		{
			Material material = new Material(Shader.Find("Battlehub/RTHandles/Grid"));
			Color value = Appearance.Colors.GridColor;
			material.SetColor("_GridColor", value);
			material.SetFloat("_ZTest", zTest ? 4f : 8f);
			return material;
		}

		private float GetCameraOffset()
		{
			if (Window.Camera.orthographic)
			{
				return Window.Camera.orthographicSize;
			}
			Vector3 position = Window.Camera.transform.position;
			return base.transform.InverseTransformPoint(position).y;
		}

		private float GetAlpha(int grid, float h, float scale)
		{
			float num = Mathf.Pow(10f, scale);
			if (grid == 0)
			{
				float num2 = Mathf.Pow(10f, scale - 1f);
				return 1f - (h - num2) / (num - num2);
			}
			float num3 = Mathf.Pow(10f, scale + 1f);
			return (h * 10f - num) / (num3 - num);
		}
	}
}
