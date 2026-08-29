using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class ScaleHandleModel : BaseHandleModel
	{
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
		private int m_xyzMatIndex = 6;

		[SerializeField]
		private Transform m_armature;

		[SerializeField]
		private Transform m_model;

		private Transform m_b1x;

		private Transform m_b2x;

		private Transform m_b3x;

		private Transform m_b1y;

		private Transform m_b2y;

		private Transform m_b3y;

		private Transform m_b1z;

		private Transform m_b2z;

		private Transform m_b3z;

		private Transform m_b0;

		[SerializeField]
		private float m_radius = 0.05f;

		[SerializeField]
		private float m_length = 1f;

		[SerializeField]
		private float m_arrowRadius = 0.1f;

		private const float DefaultRadius = 0.05f;

		private const float DefaultLength = 1f;

		private const float DefaultArrowRadius = 0.1f;

		private Material[] m_materials;

		private Vector3 m_scale = Vector3.one;

		private readonly bool m_useColliders = true;

		private BoxCollider m_xCollider;

		private BoxCollider m_yCollider;

		private BoxCollider m_zCollider;

		private BoxCollider m_xyzCollider;

		private Collider[] m_colliders;

		private float m_prevRadius;

		private float m_prevLength;

		private float m_prevArrowRadius;

		public bool IsUniform { get; set; }

		protected override void Awake()
		{
			base.Awake();
			m_b1x = m_armature.GetChild(0);
			m_b1y = m_armature.GetChild(1);
			m_b1z = m_armature.GetChild(2);
			m_b2x = m_armature.GetChild(3);
			m_b2y = m_armature.GetChild(4);
			m_b2z = m_armature.GetChild(5);
			m_b3x = m_armature.GetChild(6);
			m_b3y = m_armature.GetChild(7);
			m_b3z = m_armature.GetChild(8);
			m_b0 = m_armature.GetChild(9);
			Renderer component = m_model.GetComponent<Renderer>();
			m_materials = component.materials;
			component.sharedMaterials = m_materials;
			if (m_useColliders)
			{
				GameObject gameObject = new GameObject("Colliders");
				gameObject.transform.SetParent(base.transform, worldPositionStays: false);
				gameObject.layer = base.Editor.CameraLayerSettings.RuntimeGraphicsLayer + Window.Index;
				m_xyzCollider = gameObject.AddComponent<BoxCollider>();
				m_xCollider = gameObject.AddComponent<BoxCollider>();
				m_yCollider = gameObject.AddComponent<BoxCollider>();
				m_zCollider = gameObject.AddComponent<BoxCollider>();
				m_colliders = new Collider[4] { m_xyzCollider, m_xCollider, m_yCollider, m_zCollider };
				for (int i = 0; i < m_colliders.Length; i++)
				{
					m_colliders[i].isTrigger = true;
				}
				m_xCollider.gameObject.SetActive(value: false);
			}
		}

		protected override void Start()
		{
			base.Start();
			SetColors();
		}

		protected override void OnWindowActivating()
		{
			base.OnWindowActivating();
			if (m_useColliders)
			{
				m_xCollider.gameObject.SetActive(value: true);
			}
		}

		protected override void OnWindowDeactivating()
		{
			base.OnWindowDeactivating();
			if (m_xCollider != null)
			{
				m_xCollider.gameObject.SetActive(value: false);
			}
		}

		public override void SetLock(LockObject lockObj)
		{
			base.SetLock(lockObj);
			SetColors();
		}

		public override void Select(RuntimeHandleAxis axis)
		{
			base.Select(axis);
			SetColors();
		}

		private void SetDefaultColors()
		{
			if (m_lockObj.ScaleX)
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
			if (m_lockObj.ScaleY)
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
			if (m_lockObj.ScaleZ)
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
			if (m_lockObj.IsScaleLocked)
			{
				m_materials[m_xyzMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_materials[m_xyzMatIndex].SetFloat("_ZWrite", 0f);
				}
			}
			else
			{
				m_materials[m_xyzMatIndex].color = base.Colors.AltColor;
				m_materials[m_xyzMatIndex].SetFloat("_ZWrite", 1f);
			}
		}

		private void SetColors()
		{
			SetDefaultColors();
			switch (m_selectedAxis)
			{
			case RuntimeHandleAxis.X:
				if (IsUniform)
				{
					SetSelectedColors();
				}
				else if (!m_lockObj.ScaleX)
				{
					SetSelectedXColors();
				}
				break;
			case RuntimeHandleAxis.Y:
				if (IsUniform)
				{
					SetSelectedColors();
				}
				else if (!m_lockObj.ScaleY)
				{
					SetSelectedYColors();
				}
				break;
			case RuntimeHandleAxis.Z:
				if (IsUniform)
				{
					SetSelectedColors();
				}
				else if (!m_lockObj.ScaleZ)
				{
					SetSelectedZColors();
				}
				break;
			case RuntimeHandleAxis.Free:
				if (IsUniform)
				{
					SetSelectedColors();
				}
				else if (!m_lockObj.ScaleX || !m_lockObj.ScaleY || !m_lockObj.ScaleZ)
				{
					SetSelectedFreeColors();
				}
				break;
			}
		}

		private void SetSelectedColors()
		{
			if (!m_lockObj.ScaleX || !m_lockObj.ScaleY || !m_lockObj.ScaleZ)
			{
				SetSelectedFreeColors();
			}
			if (!m_lockObj.ScaleX)
			{
				SetSelectedXColors();
			}
			if (!m_lockObj.ScaleY)
			{
				SetSelectedYColors();
			}
			if (!m_lockObj.ScaleZ)
			{
				SetSelectedZColors();
			}
		}

		private void SetSelectedXColors()
		{
			m_materials[m_xArrowMatIndex].color = base.Colors.SelectionColor;
			m_materials[m_xMatIndex].color = base.Colors.SelectionColor;
		}

		private void SetSelectedYColors()
		{
			m_materials[m_yArrowMatIndex].color = base.Colors.SelectionColor;
			m_materials[m_yMatIndex].color = base.Colors.SelectionColor;
		}

		private void SetSelectedZColors()
		{
			m_materials[m_zArrowMatIndex].color = base.Colors.SelectionColor;
			m_materials[m_zMatIndex].color = base.Colors.SelectionColor;
		}

		private void SetSelectedFreeColors()
		{
			m_materials[m_xyzMatIndex].color = base.Colors.SelectionColor;
		}

		public override void SetScale(Vector3 scale)
		{
			base.SetScale(scale);
			m_scale = scale;
			if (base.enabled)
			{
				UpdateModel();
			}
		}

		public override void UpdateModel()
		{
			m_radius = Mathf.Max(0.001f, m_radius);
			float num = m_radius * base.ModelScale;
			float num2 = m_arrowRadius * base.ModelScale;
			float num3 = m_length * base.ModelScale;
			float num4 = num / 0.05f;
			float num5 = num2 / 0.1f;
			m_b0.localScale = Vector3.one * num5;
			Transform b3z = m_b3z;
			Transform b3y = m_b3y;
			Vector3 vector = (m_b3x.localScale = Vector3.one * num5);
			Vector3 localScale = (b3y.localScale = vector);
			b3z.localScale = localScale;
			m_b1x.position = base.transform.TransformPoint(Vector3.right * num2);
			m_b1y.position = base.transform.TransformPoint(Vector3.up * num2);
			m_b1z.position = base.transform.TransformPoint(Vector3.forward * num2);
			m_b2x.position = base.transform.TransformPoint(Vector3.right * (num3 * m_scale.x - num2));
			m_b2y.position = base.transform.TransformPoint(Vector3.up * (num3 * m_scale.y - num2));
			m_b2z.position = base.transform.TransformPoint(Vector3.forward * (num3 * m_scale.z - num2));
			Transform b2x = m_b2x;
			localScale = (m_b1x.localScale = new Vector3(1f, num4, num4));
			b2x.localScale = localScale;
			Transform b2y = m_b2y;
			localScale = (m_b1y.localScale = new Vector3(num4, num4, 1f));
			b2y.localScale = localScale;
			Transform b2z = m_b2z;
			localScale = (m_b1z.localScale = new Vector3(num4, 1f, num4));
			b2z.localScale = localScale;
			m_b3x.position = base.transform.TransformPoint(Vector3.right * num3 * m_scale.x);
			m_b3y.position = base.transform.TransformPoint(Vector3.up * num3 * m_scale.y);
			m_b3z.position = base.transform.TransformPoint(Vector3.forward * num3 * m_scale.z);
			UpdateColliders();
			base.UpdateModel();
		}

		private void UpdateColliders()
		{
			if (m_useColliders && !(m_scale.x <= 0f) && !(m_scale.y <= 0f) && !(m_scale.z <= 0f))
			{
				float num = 2f * m_arrowRadius * base.SelectionMargin;
				Transform parent = m_xCollider.transform.parent;
				Vector3 vector = parent.InverseTransformPoint(m_b3x.position);
				Vector3 vector2 = parent.InverseTransformPoint(m_b1x.position);
				m_xCollider.size = new Vector3(vector.x - vector2.x, num, num);
				m_xCollider.center = new Vector3(vector2.x + m_xCollider.size.x / 2f, vector.y, vector.z);
				Vector3 vector3 = parent.InverseTransformPoint(m_b3y.position);
				Vector3 vector4 = parent.InverseTransformPoint(m_b1y.position);
				m_yCollider.size = new Vector3(num, vector3.y - vector4.y, num);
				m_yCollider.center = new Vector3(vector3.x, vector4.y + m_yCollider.size.y / 2f, vector3.z);
				Vector3 vector5 = parent.InverseTransformPoint(m_b3z.position);
				Vector3 vector6 = parent.InverseTransformPoint(m_b1z.position);
				m_zCollider.size = new Vector3(num, num, vector5.z - vector6.z);
				m_zCollider.center = new Vector3(vector5.x, vector5.y, vector6.z + m_zCollider.size.z / 2f);
				m_xyzCollider.size = new Vector3(num * 2f, num * 2f, num * 2f);
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
			for (int i = 0; i < m_colliders.Length; i++)
			{
				if (m_colliders[i].Raycast(ray, out var hitInfo, Window.Camera.farClipPlane) && (!m_lockObj.ScaleX || !m_lockObj.ScaleY || !m_lockObj.ScaleZ || !(hitInfo.collider == m_xyzCollider)) && (!m_lockObj.ScaleX || !(hitInfo.collider == m_xCollider)) && (!m_lockObj.ScaleY || !(hitInfo.collider == m_yCollider)) && (!m_lockObj.ScaleZ || !(hitInfo.collider == m_zCollider)) && hitInfo.distance < num)
				{
					collider = hitInfo.collider;
					num = hitInfo.distance;
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
			if (collider == m_xyzCollider)
			{
				return RuntimeHandleAxis.Free;
			}
			distance = float.PositiveInfinity;
			return RuntimeHandleAxis.None;
		}

		protected override void Update()
		{
			if (m_prevRadius != m_radius || m_prevLength != m_length || m_prevArrowRadius != m_arrowRadius)
			{
				m_prevRadius = m_radius;
				m_prevLength = m_length;
				m_prevArrowRadius = m_arrowRadius;
				UpdateModel();
			}
			base.Update();
		}
	}
}
