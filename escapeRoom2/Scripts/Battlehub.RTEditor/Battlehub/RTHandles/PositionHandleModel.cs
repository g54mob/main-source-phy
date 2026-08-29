using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class PositionHandleModel : BaseHandleModel
	{
		[SerializeField]
		private GameObject[] m_models;

		private Renderer[] m_renderers;

		private Renderer m_ssQuadRenderer;

		[SerializeField]
		private GameObject m_screenSpaceQuad;

		[SerializeField]
		private GameObject m_normalModeArrows;

		[SerializeField]
		private GameObject m_vertexSnappingModeArrows;

		[SerializeField]
		private Transform[] m_armatures;

		[SerializeField]
		private Transform m_ssQuadArmature;

		[SerializeField]
		private int m_xMatIndex;

		[SerializeField]
		private int m_yMatIndex = 1;

		[SerializeField]
		private int m_zMatIndex = 2;

		[SerializeField]
		private int m_xArrowMatIndex = 3;

		[SerializeField]
		private int m_yArrowMatIndex = 4;

		[SerializeField]
		private int m_zArrowMatIndex = 5;

		[SerializeField]
		private int m_xQMatIndex = 6;

		[SerializeField]
		private int m_yQMatIndex = 7;

		[SerializeField]
		private int m_zQMatIndex = 8;

		[SerializeField]
		private int m_xQuadMatIndex = 9;

		[SerializeField]
		private int m_yQuadMatIndex = 10;

		[SerializeField]
		private int m_zQuadMatIndex = 11;

		[SerializeField]
		private float m_quadTransparency = 0.5f;

		[SerializeField]
		private float m_radius = 0.05f;

		[SerializeField]
		private float m_length = 1f;

		[SerializeField]
		private float m_arrowRadius = 0.1f;

		[SerializeField]
		private float m_arrowLength = 0.2f;

		[SerializeField]
		private float m_quadLength = 0.2f;

		[SerializeField]
		private bool m_isVertexSnapping;

		private readonly bool m_useColliders = true;

		private Material[] m_materials;

		private Material m_ssQuadMaterial;

		private Transform[] m_b0;

		private Transform[] m_b1x;

		private Transform[] m_b2x;

		private Transform[] m_b3x;

		private Transform[] m_bSx;

		private Transform[] m_b1y;

		private Transform[] m_b2y;

		private Transform[] m_b3y;

		private Transform[] m_bSy;

		private Transform[] m_b1z;

		private Transform[] m_b2z;

		private Transform[] m_b3z;

		private Transform[] m_bSz;

		private Transform m_b1ss;

		private Transform m_b2ss;

		private Transform m_b3ss;

		private Transform m_b4ss;

		private Vector3[] m_defaultArmaturesScale;

		private Vector3[] m_defaultB3XScale;

		private Vector3[] m_defaultB3YScale;

		private Vector3[] m_defaultB3ZScale;

		private Vector3[] m_defaultSigns = new Vector3[9]
		{
			new Vector3(1f, 1f, 1f),
			new Vector3(-1f, 1f, 1f),
			new Vector3(-1f, -1f, 1f),
			new Vector3(1f, -1f, 1f),
			new Vector3(1f, 1f, -1f),
			new Vector3(-1f, 1f, -1f),
			new Vector3(-1f, -1f, -1f),
			new Vector3(1f, -1f, -1f),
			new Vector3(1f, 1f, 1f)
		};

		private const float DefaultRadius = 0.05f;

		private const float DefaultLength = 1f;

		private const float DefaultArrowRadius = 0.1f;

		private const float DefaultArrowLength = 0.2f;

		private const float DefaultQuadLength = 0.2f;

		private BoxCollider m_xCollider;

		private BoxCollider m_yCollider;

		private BoxCollider m_zCollider;

		private BoxCollider m_xyCollider;

		private BoxCollider m_xzCollider;

		private BoxCollider m_yzCollider;

		private SphereCollider m_snappingCollider;

		private Collider[] m_colliders;

		private float m_prevRadius;

		private float m_prevLength;

		private float m_prevArrowRadius;

		private float m_prevArrowLength;

		private float m_prevQuadLength;

		private Vector3 m_prevCameraPosition = new Vector3(float.MinValue, float.MinValue, float.MinValue);

		private Vector3 m_prevPosition;

		private Quaternion m_prevRotation;

		private int m_prevIndex = -1;

		public float Radius
		{
			get
			{
				return m_radius;
			}
			set
			{
				m_radius = value;
			}
		}

		public float Length
		{
			get
			{
				return m_length;
			}
			set
			{
				m_length = value;
			}
		}

		public float ArrowRadius
		{
			get
			{
				return m_arrowRadius;
			}
			set
			{
				m_arrowRadius = value;
			}
		}

		public float ArrowLength
		{
			get
			{
				return m_arrowLength;
			}
			set
			{
				m_arrowLength = value;
			}
		}

		public float QuadLength
		{
			get
			{
				return m_quadLength;
			}
			set
			{
				m_quadLength = value;
			}
		}

		private float _QuadLength
		{
			get
			{
				if (base.Appearance == null)
				{
					return m_quadLength;
				}
				if (!base.Appearance.PositionHandleArrowOnly)
				{
					return m_quadLength;
				}
				return 0f;
			}
		}

		public bool IsVertexSnapping
		{
			get
			{
				return m_isVertexSnapping;
			}
			set
			{
				if (m_isVertexSnapping != value)
				{
					m_isVertexSnapping = value;
					OnVertexSnappingModeChanged();
					SetColors();
					UpdateColliders();
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			m_defaultArmaturesScale = new Vector3[m_armatures.Length];
			m_defaultB3XScale = new Vector3[m_armatures.Length];
			m_defaultB3YScale = new Vector3[m_armatures.Length];
			m_defaultB3ZScale = new Vector3[m_armatures.Length];
			m_b1x = new Transform[m_armatures.Length];
			m_b1y = new Transform[m_armatures.Length];
			m_b1z = new Transform[m_armatures.Length];
			m_b2x = new Transform[m_armatures.Length];
			m_b2y = new Transform[m_armatures.Length];
			m_b2z = new Transform[m_armatures.Length];
			m_b3x = new Transform[m_armatures.Length];
			m_b3y = new Transform[m_armatures.Length];
			m_b3z = new Transform[m_armatures.Length];
			m_b0 = new Transform[m_armatures.Length];
			m_bSx = new Transform[m_armatures.Length];
			m_bSy = new Transform[m_armatures.Length];
			m_bSz = new Transform[m_armatures.Length];
			for (int i = 0; i < m_armatures.Length; i++)
			{
				m_b1x[i] = m_armatures[i].GetChild(0);
				m_b1y[i] = m_armatures[i].GetChild(1);
				m_b1z[i] = m_armatures[i].GetChild(2);
				m_b2x[i] = m_armatures[i].GetChild(3);
				m_b2y[i] = m_armatures[i].GetChild(4);
				m_b2z[i] = m_armatures[i].GetChild(5);
				m_b3x[i] = m_armatures[i].GetChild(6);
				m_b3y[i] = m_armatures[i].GetChild(7);
				m_b3z[i] = m_armatures[i].GetChild(8);
				m_b0[i] = m_armatures[i].GetChild(9);
				m_bSx[i] = m_armatures[i].GetChild(10);
				m_bSy[i] = m_armatures[i].GetChild(11);
				m_bSz[i] = m_armatures[i].GetChild(12);
				m_defaultArmaturesScale[i] = m_armatures[i].localScale;
				m_defaultB3XScale[i] = base.transform.TransformVector(m_b3x[i].localScale);
				m_defaultB3YScale[i] = base.transform.TransformVector(m_b3y[i].localScale);
				m_defaultB3ZScale[i] = base.transform.TransformVector(m_b3z[i].localScale);
			}
			m_b1ss = m_ssQuadArmature.GetChild(1);
			m_b2ss = m_ssQuadArmature.GetChild(2);
			m_b3ss = m_ssQuadArmature.GetChild(3);
			m_b4ss = m_ssQuadArmature.GetChild(4);
			m_materials = m_models[0].GetComponent<Renderer>().materials;
			m_ssQuadRenderer = m_screenSpaceQuad.GetComponent<Renderer>();
			m_ssQuadRenderer.forceRenderingOff = true;
			m_ssQuadMaterial = m_ssQuadRenderer.sharedMaterial;
			SetDefaultColors();
			m_renderers = new Renderer[m_models.Length];
			for (int j = 0; j < m_models.Length; j++)
			{
				Renderer component = m_models[j].GetComponent<Renderer>();
				component.sharedMaterials = m_materials;
				component.forceRenderingOff = true;
				m_renderers[j] = component;
			}
			OnVertexSnappingModeChanged();
			if (m_useColliders)
			{
				GameObject gameObject = new GameObject("Colliders");
				gameObject.transform.SetParent(base.transform, worldPositionStays: false);
				gameObject.layer = base.Editor.CameraLayerSettings.RuntimeGraphicsLayer + Window.Index;
				m_xCollider = gameObject.AddComponent<BoxCollider>();
				m_yCollider = gameObject.AddComponent<BoxCollider>();
				m_zCollider = gameObject.AddComponent<BoxCollider>();
				m_xzCollider = gameObject.AddComponent<BoxCollider>();
				m_xyCollider = gameObject.AddComponent<BoxCollider>();
				m_yzCollider = gameObject.AddComponent<BoxCollider>();
				m_snappingCollider = gameObject.AddComponent<SphereCollider>();
				m_colliders = new Collider[6] { m_xCollider, m_yCollider, m_zCollider, m_xzCollider, m_xyCollider, m_yzCollider };
				for (int k = 0; k < m_colliders.Length; k++)
				{
					m_colliders[k].isTrigger = true;
				}
				m_snappingCollider.isTrigger = true;
				m_xCollider.transform.gameObject.SetActive(value: false);
			}
		}

		protected override void Start()
		{
			base.Start();
			SetColors();
			UpdateColliders();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (base.IsWindowActive)
			{
				m_prevRotation = base.transform.rotation;
				m_prevPosition = base.transform.position;
				m_prevCameraPosition = Window.Camera.transform.position;
				int num = SetCameraPosition(m_prevCameraPosition);
				if (num >= 0)
				{
					UpdateColliders(num);
				}
			}
		}

		protected override void OnWindowActivating()
		{
			base.OnWindowActivating();
			m_prevRotation = base.transform.rotation;
			m_prevPosition = base.transform.position;
			m_prevCameraPosition = Window.Camera.transform.position;
			int num = SetCameraPosition(m_prevCameraPosition);
			if (num >= 0)
			{
				UpdateColliders(num);
			}
			if (m_useColliders)
			{
				m_xCollider.transform.gameObject.SetActive(value: true);
			}
		}

		protected override void OnWindowDeactivating()
		{
			base.OnWindowDeactivating();
			if (m_xCollider != null)
			{
				m_xCollider.transform.gameObject.SetActive(value: false);
			}
		}

		public override void SetLock(LockObject lockObj)
		{
			base.SetLock(lockObj);
			OnVertexSnappingModeChanged();
			SetColors();
			UpdateColliders();
		}

		public override void Select(RuntimeHandleAxis axis)
		{
			base.Select(axis);
			OnVertexSnappingModeChanged();
			SetColors();
			UpdateColliders();
		}

		private void OnVertexSnappingModeChanged()
		{
			m_normalModeArrows.SetActive(!m_isVertexSnapping);
			m_vertexSnappingModeArrows.SetActive(m_isVertexSnapping && !m_lockObj.IsPositionLocked);
			if (m_vertexSnappingModeArrows.activeSelf)
			{
				for (int i = 0; i < m_renderers.Length; i++)
				{
					m_renderers[i].forceRenderingOff = true;
				}
				m_renderers[m_renderers.Length - 1].forceRenderingOff = false;
			}
			else if (m_xCollider != null)
			{
				m_prevCameraPosition = Window.Camera.transform.position;
				int num = SetCameraPosition(m_prevCameraPosition, force: true);
				if (num >= 0)
				{
					UpdateColliders(num);
					m_prevIndex = num;
				}
			}
			m_ssQuadRenderer.forceRenderingOff = !m_vertexSnappingModeArrows.activeSelf;
			PushUpdatesToGraphicLayer();
		}

		private void SetDefaultColors()
		{
			if (m_lockObj.PositionX)
			{
				m_materials[m_xMatIndex].color = base.Colors.DisabledColor;
				m_materials[m_xArrowMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_materials[m_xMatIndex].SetFloat("_ZWrite", 0f);
					m_materials[m_xArrowMatIndex].SetFloat("_ZWrite", 0f);
				}
			}
			else
			{
				m_materials[m_xMatIndex].color = base.Colors.XColor;
				m_materials[m_xArrowMatIndex].color = base.Colors.XColor;
				m_materials[m_xMatIndex].SetFloat("_ZWrite", 1f);
				m_materials[m_xArrowMatIndex].SetFloat("_ZWrite", 1f);
			}
			if (m_lockObj.PositionY)
			{
				m_materials[m_yMatIndex].color = base.Colors.DisabledColor;
				m_materials[m_yArrowMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_materials[m_yMatIndex].SetFloat("_ZWrite", 0f);
					m_materials[m_yArrowMatIndex].SetFloat("_ZWrite", 0f);
				}
			}
			else
			{
				m_materials[m_yMatIndex].color = base.Colors.YColor;
				m_materials[m_yArrowMatIndex].color = base.Colors.YColor;
				m_materials[m_yMatIndex].SetFloat("_ZWrite", 1f);
				m_materials[m_yArrowMatIndex].SetFloat("_ZWrite", 1f);
			}
			if (m_lockObj.PositionZ)
			{
				m_materials[m_zMatIndex].color = base.Colors.DisabledColor;
				m_materials[m_zArrowMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_materials[m_zMatIndex].SetFloat("_ZWrite", 0f);
					m_materials[m_zArrowMatIndex].SetFloat("_ZWrite", 0f);
				}
			}
			else
			{
				m_materials[m_zMatIndex].color = base.Colors.ZColor;
				m_materials[m_zArrowMatIndex].color = base.Colors.ZColor;
				m_materials[m_zMatIndex].SetFloat("_ZWrite", 1f);
				m_materials[m_zArrowMatIndex].SetFloat("_ZWrite", 1f);
			}
			if (m_lockObj.PositionY || m_lockObj.PositionZ)
			{
				m_materials[m_xQMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_materials[m_xQMatIndex].SetFloat("_ZWrite", 0f);
				}
			}
			else
			{
				m_materials[m_xQMatIndex].color = base.Colors.XColor;
				m_materials[m_xQMatIndex].SetFloat("_ZWrite", 1f);
			}
			if (m_lockObj.PositionX || m_lockObj.PositionZ)
			{
				m_materials[m_yQMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_materials[m_yQMatIndex].SetFloat("_ZWrite", 0f);
				}
			}
			else
			{
				m_materials[m_yQMatIndex].color = base.Colors.YColor;
				m_materials[m_yQMatIndex].SetFloat("_ZWrite", 1f);
			}
			if (m_lockObj.PositionX || m_lockObj.PositionY)
			{
				m_materials[m_zQMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_materials[m_zQMatIndex].SetFloat("_ZWrite", 0f);
				}
			}
			else
			{
				m_materials[m_zQMatIndex].color = base.Colors.ZColor;
				m_materials[m_zQMatIndex].SetFloat("_ZWrite", 1f);
			}
			Color color = ((m_lockObj.PositionY || m_lockObj.PositionZ) ? base.Colors.DisabledColor : base.Colors.XColor);
			color.a = Mathf.Min(m_quadTransparency, color.a);
			m_materials[m_xQuadMatIndex].color = color;
			Color color2 = ((m_lockObj.PositionX || m_lockObj.PositionZ) ? base.Colors.DisabledColor : base.Colors.YColor);
			color2.a = Mathf.Min(m_quadTransparency, color2.a);
			m_materials[m_yQuadMatIndex].color = color2;
			Color color3 = ((m_lockObj.PositionX || m_lockObj.PositionY) ? base.Colors.DisabledColor : base.Colors.ZColor);
			color3.a = Mathf.Min(m_quadTransparency, color3.a);
			m_materials[m_zQuadMatIndex].color = color3;
			m_ssQuadMaterial.color = base.Colors.AltColor;
		}

		private void UpdateColliders()
		{
			m_xCollider.enabled = !m_lockObj.PositionX;
			m_yCollider.enabled = !m_lockObj.PositionY;
			m_zCollider.enabled = !m_lockObj.PositionZ;
			m_xyCollider.enabled = !m_lockObj.PositionX && !m_lockObj.PositionY;
			m_xzCollider.enabled = !m_lockObj.PositionX && !m_lockObj.PositionZ;
			m_yzCollider.enabled = !m_lockObj.PositionY && !m_lockObj.PositionZ;
		}

		private void SetColors()
		{
			SetDefaultColors();
			switch (m_selectedAxis)
			{
			case RuntimeHandleAxis.XY:
				if (!m_lockObj.PositionX && !m_lockObj.PositionY)
				{
					m_materials[m_xArrowMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_yArrowMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_xMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_yMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_zQMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_zQuadMatIndex].color = base.Colors.SelectionColor;
				}
				break;
			case RuntimeHandleAxis.YZ:
				if (!m_lockObj.PositionY && !m_lockObj.PositionZ)
				{
					m_materials[m_yArrowMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_zArrowMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_yMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_zMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_xQMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_xQuadMatIndex].color = base.Colors.SelectionColor;
				}
				break;
			case RuntimeHandleAxis.XZ:
				if (!m_lockObj.PositionX && !m_lockObj.PositionZ)
				{
					m_materials[m_xArrowMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_zArrowMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_xMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_zMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_yQMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_yQuadMatIndex].color = base.Colors.SelectionColor;
				}
				break;
			case RuntimeHandleAxis.X:
				if (!m_lockObj.PositionX)
				{
					m_materials[m_xArrowMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_xMatIndex].color = base.Colors.SelectionColor;
				}
				break;
			case RuntimeHandleAxis.Y:
				if (!m_lockObj.PositionY)
				{
					m_materials[m_yArrowMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_yMatIndex].color = base.Colors.SelectionColor;
				}
				break;
			case RuntimeHandleAxis.Z:
				if (!m_lockObj.PositionZ)
				{
					m_materials[m_zArrowMatIndex].color = base.Colors.SelectionColor;
					m_materials[m_zMatIndex].color = base.Colors.SelectionColor;
				}
				break;
			case RuntimeHandleAxis.Snap:
				m_ssQuadMaterial.color = base.Colors.SelectionColor;
				break;
			}
		}

		public override void UpdateModel()
		{
			float num = Mathf.Abs(_QuadLength);
			m_radius = Mathf.Max(0.01f, m_radius);
			Vector3 position = base.transform.position;
			float num2 = m_radius * base.ModelScale;
			float num3 = m_length * base.ModelScale;
			float num4 = m_arrowRadius * base.ModelScale;
			float num5 = m_arrowLength * base.ModelScale;
			num *= base.ModelScale;
			float num6 = num2 / 0.05f;
			float num7 = num5 / 0.2f / num6;
			float num8 = num4 / 0.1f / num6;
			for (int i = 0; i < m_models.Length; i++)
			{
				m_armatures[i].localScale = m_defaultArmaturesScale[i] * num6;
				m_ssQuadArmature.localScale = Vector3.one * num6;
				m_b3x[i].position = base.transform.TransformPoint(Vector3.right * num3);
				m_b3y[i].position = base.transform.TransformPoint(Vector3.up * num3);
				m_b3z[i].position = base.transform.TransformPoint(Vector3.forward * num3);
				m_b2x[i].position = base.transform.TransformPoint(Vector3.right * (num3 - num5));
				m_b2y[i].position = base.transform.TransformPoint(Vector3.up * (num3 - num5));
				m_b2z[i].position = base.transform.TransformPoint(Vector3.forward * (num3 - num5));
				m_b3x[i].localScale = Vector3.right * num7 + new Vector3(0f, 1f, 1f) * num8;
				m_b3y[i].localScale = Vector3.forward * num7 + new Vector3(1f, 1f, 0f) * num8;
				m_b3z[i].localScale = Vector3.up * num7 + new Vector3(1f, 0f, 1f) * num8;
				m_b1x[i].position = base.transform.TransformPoint(m_defaultSigns[i].x * Vector3.right * num);
				m_b1y[i].position = base.transform.TransformPoint(m_defaultSigns[i].y * Vector3.up * num);
				m_b1z[i].position = base.transform.TransformPoint(m_defaultSigns[i].z * Vector3.forward * num);
				m_bSx[i].position = position + (m_b1y[i].position - position) + (m_b1z[i].position - position);
				m_bSy[i].position = position + (m_b1x[i].position - position) + (m_b1z[i].position - position);
				m_bSz[i].position = position + (m_b1x[i].position - position) + (m_b1y[i].position - position);
			}
			m_b1ss.position = position + base.transform.rotation * new Vector3(1f, 1f, 0f) * num * base.transform.localScale.x * 0.5f;
			m_b2ss.position = position + base.transform.rotation * new Vector3(-1f, -1f, 0f) * num * base.transform.localScale.x * 0.5f;
			m_b3ss.position = position + base.transform.rotation * new Vector3(-1f, 1f, 0f) * num * base.transform.localScale.x * 0.5f;
			m_b4ss.position = position + base.transform.rotation * new Vector3(1f, -1f, 0f) * num * base.transform.localScale.x * 0.5f;
			int num9 = SetCameraPosition(Window.Camera.transform.position);
			if (num9 >= 0)
			{
				UpdateColliders(num9);
			}
			base.UpdateModel();
		}

		private void UpdateColliders(int i)
		{
			if (m_useColliders)
			{
				float num = 2f * m_arrowRadius * base.SelectionMargin;
				float num2 = 2f * m_radius;
				Transform parent = m_xCollider.transform.parent;
				Vector3 position = m_b3x[i].position;
				position = parent.InverseTransformPoint(position);
				Vector3 position2 = m_b1x[i].position;
				position2 = parent.InverseTransformPoint(position2);
				m_xCollider.size = new Vector3(position.x - Mathf.Clamp(position2.x, 0f, position2.x), num, num);
				m_xCollider.center = new Vector3(Mathf.Clamp(position2.x, 0f, position2.x) + m_xCollider.size.x / 2f, position.y, position.z);
				Vector3 position3 = m_b3y[i].position;
				position3 = parent.InverseTransformPoint(position3);
				Vector3 position4 = m_b1y[i].position;
				position4 = parent.InverseTransformPoint(position4);
				m_yCollider.size = new Vector3(num, position3.y - Mathf.Clamp(position4.y, 0f, position4.y), num);
				m_yCollider.center = new Vector3(position3.x, Mathf.Clamp(position4.y, 0f, position4.y) + m_yCollider.size.y / 2f, position3.z);
				Vector3 position5 = m_b3z[i].position;
				position5 = parent.InverseTransformPoint(position5);
				Vector3 position6 = m_b1z[i].position;
				position6 = parent.InverseTransformPoint(position6);
				m_zCollider.size = new Vector3(num, num, position5.z - Mathf.Clamp(position6.z, 0f, position6.z));
				m_zCollider.center = new Vector3(position5.x, position5.y, Mathf.Clamp(position6.z, 0f, position6.z) + m_zCollider.size.z / 2f);
				if (base.ModelScale > 0f)
				{
					position2 /= base.ModelScale;
					position4 /= base.ModelScale;
					position6 /= base.ModelScale;
					m_xyCollider.size = new Vector3(Mathf.Abs(position2.x * base.SelectionMargin), Mathf.Abs(position4.y * base.SelectionMargin), num2);
					m_xyCollider.center = new Vector3(position2.x * base.SelectionMargin / 2f, position4.y * base.SelectionMargin / 2f, 0f);
					m_xzCollider.size = new Vector3(Mathf.Abs(position2.x * base.SelectionMargin), num2, Mathf.Abs(position6.z * base.SelectionMargin));
					m_xzCollider.center = new Vector3(position2.x * base.SelectionMargin / 2f, 0f, position6.z * base.SelectionMargin / 2f);
					m_yzCollider.size = new Vector3(num2, Mathf.Abs(position4.y * base.SelectionMargin), Mathf.Abs(position6.z * base.SelectionMargin));
					m_yzCollider.center = new Vector3(0f, position4.y * base.SelectionMargin / 2f, position6.z * base.SelectionMargin / 2f);
					m_snappingCollider.radius = Mathf.Abs(position2.x * base.SelectionMargin);
				}
				else
				{
					m_xyCollider.size = Vector3.zero;
					m_xyCollider.center = Vector3.zero;
					m_xzCollider.size = Vector3.zero;
					m_xzCollider.center = Vector3.zero;
					m_yzCollider.size = Vector3.zero;
					m_yzCollider.center = Vector3.zero;
					m_snappingCollider.radius = 0f;
				}
			}
		}

		public override RuntimeHandleAxis HitTest(Ray ray, out float distance)
		{
			if (!m_useColliders)
			{
				distance = float.PositiveInfinity;
				return RuntimeHandleAxis.None;
			}
			Collider collider = null;
			float num = float.MaxValue;
			if (m_isVertexSnapping)
			{
				if (m_snappingCollider.Raycast(ray, out var hitInfo, Window.Camera.farClipPlane))
				{
					collider = hitInfo.collider;
					num = hitInfo.distance;
				}
			}
			else
			{
				for (int i = 0; i < m_colliders.Length; i++)
				{
					if (m_colliders[i].Raycast(ray, out var hitInfo2, Window.Camera.farClipPlane) && (!m_lockObj.PositionX || (!(hitInfo2.collider == m_xCollider) && !(hitInfo2.collider == m_xyCollider) && !(hitInfo2.collider == m_xzCollider))) && (!m_lockObj.PositionY || (!(hitInfo2.collider == m_yCollider) && !(hitInfo2.collider == m_yzCollider) && !(hitInfo2.collider == m_xyCollider))) && (!m_lockObj.PositionZ || (!(hitInfo2.collider == m_zCollider) && !(hitInfo2.collider == m_yzCollider) && !(hitInfo2.collider == m_xzCollider))))
					{
						if (hitInfo2.collider == m_xyCollider || hitInfo2.collider == m_xzCollider || hitInfo2.collider == m_yzCollider)
						{
							hitInfo2.distance *= 0.1f;
						}
						if (hitInfo2.distance < num)
						{
							collider = hitInfo2.collider;
							num = hitInfo2.distance;
						}
					}
				}
			}
			distance = num;
			if (collider == m_xCollider)
			{
				return RuntimeHandleAxis.X;
			}
			if (collider == m_yCollider)
			{
				return RuntimeHandleAxis.Y;
			}
			if (collider == m_zCollider)
			{
				return RuntimeHandleAxis.Z;
			}
			if (collider == m_xyCollider)
			{
				return RuntimeHandleAxis.XY;
			}
			if (collider == m_xzCollider)
			{
				return RuntimeHandleAxis.XZ;
			}
			if (collider == m_yzCollider)
			{
				return RuntimeHandleAxis.YZ;
			}
			if (collider == m_snappingCollider)
			{
				return RuntimeHandleAxis.Snap;
			}
			distance = float.PositiveInfinity;
			return RuntimeHandleAxis.None;
		}

		public int SetCameraPosition(Vector3 pos, bool force = false)
		{
			Vector3 normalized = (pos - base.transform.position).normalized;
			normalized = base.transform.InverseTransformDirection(normalized);
			int num = -1;
			num = ((normalized.x >= 0f) ? ((normalized.y >= 0f) ? ((!(normalized.z >= 0f)) ? 4 : 0) : ((!(normalized.z >= 0f)) ? 7 : 3)) : ((normalized.y >= 0f) ? ((normalized.z >= 0f) ? 1 : 5) : ((!(normalized.z >= 0f)) ? 6 : 2)));
			num = (num + ((base.transform.localScale.z < 0f) ? 4 : 0)) % 8;
			if (m_lockObj != null && (m_lockObj.PositionX || m_lockObj.PositionY || m_lockObj.PositionZ))
			{
				num = 0;
			}
			if (m_prevIndex == num && !force)
			{
				return -1;
			}
			if (m_prevIndex >= 0)
			{
				m_models[m_prevIndex].SetActive(value: false);
				m_renderers[m_prevIndex].forceRenderingOff = true;
			}
			if (num >= 0)
			{
				m_models[num].SetActive(value: true);
				m_renderers[num].forceRenderingOff = false;
			}
			PushUpdatesToGraphicLayer();
			m_prevIndex = num;
			return num;
		}

		protected override void Update()
		{
			base.Update();
			if (m_prevCameraPosition != Window.Camera.transform.position || m_prevPosition != base.transform.position || m_prevRotation != base.transform.rotation)
			{
				m_prevRotation = base.transform.rotation;
				m_prevPosition = base.transform.position;
				m_prevCameraPosition = Window.Camera.transform.position;
				int num = SetCameraPosition(m_prevCameraPosition);
				if (num >= 0)
				{
					UpdateColliders(num);
				}
			}
			if (m_prevRadius != m_radius || m_prevLength != m_length || m_prevArrowRadius != m_arrowRadius || m_prevArrowLength != m_arrowLength || m_prevQuadLength != _QuadLength)
			{
				m_prevRadius = m_radius;
				m_prevLength = m_length;
				m_prevArrowRadius = m_arrowRadius;
				m_prevArrowLength = m_arrowLength;
				m_prevQuadLength = _QuadLength;
				UpdateModel();
			}
		}
	}
}
