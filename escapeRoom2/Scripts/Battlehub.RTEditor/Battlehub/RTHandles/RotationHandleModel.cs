using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class RotationHandleModel : BaseHandleModel
	{
		private const float DefaultMinorRadius = 0.0075f;

		private const float DefaultMajorRadius = 1f;

		private const float DefaultOuterRadius = 1.11f;

		[SerializeField]
		private float m_minorRadius = 0.0075f;

		[SerializeField]
		private float m_majorRadius = 1f;

		[SerializeField]
		private float m_outerRadius = 1.11f;

		[SerializeField]
		private MeshFilter m_xyz;

		[SerializeField]
		private MeshFilter m_innerCircle;

		[SerializeField]
		private MeshFilter m_outerCircle;

		[SerializeField]
		private MeshFilter m_innerCircleVR;

		[SerializeField]
		private MeshFilter m_outerCircleVR;

		private MeshFilter m_inner;

		private MeshFilter m_outer;

		private Mesh m_xyzMesh;

		private Mesh m_innerCircleMesh;

		private Mesh m_outerCircleMesh;

		[SerializeField]
		private int m_xMatIndex;

		[SerializeField]
		private int m_yMatIndex = 1;

		[SerializeField]
		private int m_zMatIndex = 2;

		[SerializeField]
		private int m_innerCircleBorderMatIndex;

		[SerializeField]
		private int m_innerCircleFillMatIndex = 1;

		private Material[] m_xyzMaterials;

		private Material[] m_innerCircleMaterials;

		private Material m_outerCircleMaterial;

		private readonly bool m_useColliders = true;

		[SerializeField]
		private Mesh m_axisColliderMesh;

		[SerializeField]
		private Mesh m_ssMesh;

		private MeshCollider m_xCollider;

		private MeshCollider m_yCollider;

		private MeshCollider m_zCollider;

		private MeshCollider m_innerCollider;

		private MeshCollider m_outerCollider;

		private Collider[] m_colliders;

		private float m_prevMinorRadius = 0.0075f;

		private float m_prevMajorRadius = 1f;

		private float m_prevOuterRadius = 1.11f;

		protected override void Awake()
		{
			base.Awake();
			m_xyzMesh = m_xyz.sharedMesh;
			if (base.Editor.IsVR)
			{
				Object.Destroy(m_innerCircle.gameObject);
				Object.Destroy(m_outerCircle.gameObject);
				m_inner = m_innerCircleVR;
				m_outer = m_outerCircleVR;
			}
			else
			{
				Object.Destroy(m_innerCircleVR.gameObject);
				Object.Destroy(m_outerCircleVR.gameObject);
				m_inner = m_innerCircle;
				m_outer = m_outerCircle;
			}
			m_innerCircleMesh = m_inner.sharedMesh;
			m_outerCircleMesh = m_outer.sharedMesh;
			Renderer component = m_xyz.GetComponent<Renderer>();
			component.sharedMaterials = component.materials;
			m_xyzMaterials = component.sharedMaterials;
			component = m_inner.GetComponent<Renderer>();
			component.sharedMaterials = component.materials;
			m_innerCircleMaterials = component.sharedMaterials;
			component = m_outer.GetComponent<Renderer>();
			component.sharedMaterials = component.materials;
			m_outerCircleMaterial = component.sharedMaterial;
			Mesh mesh = m_xyz.mesh;
			m_xyz.sharedMesh = mesh;
			mesh = m_inner.mesh;
			m_inner.sharedMesh = mesh;
			mesh = m_outer.mesh;
			m_outer.sharedMesh = mesh;
			if (m_useColliders)
			{
				GameObject gameObject = new GameObject("Colliders");
				gameObject.transform.SetParent(base.transform, worldPositionStays: false);
				gameObject.layer = base.Editor.CameraLayerSettings.RuntimeGraphicsLayer + Window.Index;
				GameObject gameObject2 = new GameObject("XAxis");
				gameObject2.transform.SetParent(gameObject.transform, worldPositionStays: false);
				gameObject2.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
				m_xCollider = gameObject2.AddComponent<MeshCollider>();
				GameObject gameObject3 = new GameObject("YAxis");
				gameObject3.transform.SetParent(gameObject.transform, worldPositionStays: false);
				gameObject3.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
				m_yCollider = gameObject3.AddComponent<MeshCollider>();
				GameObject gameObject4 = new GameObject("ZAxis");
				gameObject4.transform.SetParent(gameObject.transform, worldPositionStays: false);
				m_zCollider = gameObject4.AddComponent<MeshCollider>();
				GameObject gameObject5 = new GameObject("InnerAxis");
				gameObject5.transform.SetParent(gameObject.transform, worldPositionStays: false);
				m_innerCollider = gameObject5.AddComponent<MeshCollider>();
				GameObject gameObject6 = new GameObject("OuterCollider");
				gameObject6.transform.SetParent(gameObject.transform, worldPositionStays: false);
				m_outerCollider = gameObject6.AddComponent<MeshCollider>();
				Collider[] colliders = new MeshCollider[5] { m_xCollider, m_yCollider, m_zCollider, m_innerCollider, m_outerCollider };
				m_colliders = colliders;
				colliders = m_colliders;
				foreach (Collider obj in colliders)
				{
					obj.gameObject.SetActive(value: false);
					obj.gameObject.layer = base.Editor.CameraLayerSettings.RuntimeGraphicsLayer + Window.Index;
				}
			}
		}

		protected override void Start()
		{
			base.Start();
			SetColors();
		}

		public override void Select(RuntimeHandleAxis axis)
		{
			base.Select(axis);
			SetColors();
		}

		public override void SetLock(LockObject lockObj)
		{
			base.SetLock(lockObj);
			SetColors();
		}

		private void SetDefaultColors()
		{
			if (m_lockObj.RotationX)
			{
				m_xyzMaterials[m_xMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_xyzMaterials[m_xMatIndex].SetFloat("_ZWrite", 0f);
				}
			}
			else
			{
				m_xyzMaterials[m_xMatIndex].color = base.Colors.XColor;
				m_xyzMaterials[m_xMatIndex].SetFloat("_ZWrite", 1f);
			}
			if (m_lockObj.RotationY)
			{
				m_xyzMaterials[m_yMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_xyzMaterials[m_yMatIndex].SetFloat("_ZWrite", 0f);
				}
			}
			else
			{
				m_xyzMaterials[m_yMatIndex].color = base.Colors.YColor;
				m_xyzMaterials[m_yMatIndex].SetFloat("_ZWrite", 1f);
			}
			if (m_lockObj.RotationZ)
			{
				m_xyzMaterials[m_zMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_xyzMaterials[m_zMatIndex].SetFloat("_ZWrite", 0f);
				}
			}
			else
			{
				m_xyzMaterials[m_zMatIndex].color = base.Colors.ZColor;
				m_xyzMaterials[m_zMatIndex].SetFloat("_ZWrite", 1f);
			}
			if (m_lockObj.RotationScreen)
			{
				m_outerCircleMaterial.color = base.Colors.DisabledColor;
			}
			else
			{
				m_outerCircleMaterial.color = base.Colors.AltColor;
			}
			m_outerCircleMaterial.SetInt("_ZTest", 2);
			if (m_lockObj.RotationFree)
			{
				m_innerCircleMaterials[m_innerCircleBorderMatIndex].color = base.Colors.DisabledColor;
				if (Mathf.Approximately((int)base.Colors.DisabledColor.a, 0f))
				{
					m_innerCircleMaterials[m_innerCircleBorderMatIndex].SetFloat("_ZWrite", 0f);
					if ((m_lockObj.RotationX && m_lockObj.RotationY) || (m_lockObj.RotationY && m_lockObj.RotationZ) || (m_lockObj.RotationX && m_lockObj.RotationZ))
					{
						m_innerCircle.gameObject.SetActive(value: false);
						Renderer component = m_innerCircle.GetComponent<Renderer>();
						if (component != null)
						{
							component.forceRenderingOff = true;
						}
					}
					else
					{
						m_innerCircle.gameObject.SetActive(value: true);
						Renderer component2 = m_innerCircle.GetComponent<Renderer>();
						if (component2 != null)
						{
							component2.forceRenderingOff = false;
						}
					}
				}
			}
			else
			{
				m_innerCircleMaterials[m_innerCircleBorderMatIndex].color = base.Colors.AltColor2;
				m_innerCircleMaterials[m_innerCircleBorderMatIndex].SetFloat("_ZWrite", 1f);
				Renderer component3 = m_innerCircle.GetComponent<Renderer>();
				if (component3 != null)
				{
					component3.forceRenderingOff = false;
				}
				m_inner.gameObject.SetActive(value: true);
			}
			m_innerCircleMaterials[m_innerCircleFillMatIndex].color = new Color(0f, 0f, 0f, 0f);
		}

		private void SetColors()
		{
			SetDefaultColors();
			switch (m_selectedAxis)
			{
			case RuntimeHandleAxis.X:
				if (!m_lockObj.RotationX)
				{
					m_xyzMaterials[m_xMatIndex].color = base.Colors.SelectionColor;
				}
				break;
			case RuntimeHandleAxis.Y:
				if (!m_lockObj.RotationY)
				{
					m_xyzMaterials[m_yMatIndex].color = base.Colors.SelectionColor;
				}
				break;
			case RuntimeHandleAxis.Z:
				if (!m_lockObj.RotationZ)
				{
					m_xyzMaterials[m_zMatIndex].color = base.Colors.SelectionColor;
				}
				break;
			case RuntimeHandleAxis.Free:
				if (!m_lockObj.RotationFree)
				{
					m_innerCircleMaterials[m_innerCircleFillMatIndex].color = base.Colors.SelectionAltColor;
				}
				break;
			case RuntimeHandleAxis.Screen:
				if (!m_lockObj.RotationScreen)
				{
					m_outerCircleMaterial.color = base.Colors.SelectionColor;
					m_outerCircleMaterial.SetInt("_ZTest", 0);
				}
				break;
			}
			PushUpdatesToGraphicLayer();
		}

		private void UpdateXYZ(Mesh mesh, float majorRadius, float minorRadius)
		{
			m_xyz.transform.localScale = Vector3.one * majorRadius;
			minorRadius /= Mathf.Max(0.01f, majorRadius);
			Vector3[] vertices = m_xyzMesh.vertices;
			for (int i = 0; i < m_xyzMesh.subMeshCount; i++)
			{
				int[] triangles = mesh.GetTriangles(i);
				foreach (int num in triangles)
				{
					Vector3 vector = vertices[num];
					Vector3 vector2 = vector;
					switch (i)
					{
					case 0:
						vector2.x = 0f;
						break;
					case 1:
						vector2.y = 0f;
						break;
					case 2:
						vector2.z = 0f;
						break;
					}
					vector2.Normalize();
					vertices[num] = vector2 + (vector - vector2).normalized * minorRadius;
				}
			}
			mesh.vertices = vertices;
		}

		private void UpdateCircle(Mesh mesh, Mesh originalMesh, Transform circleTransform, float majorRadius, float minorRadius)
		{
			circleTransform.localScale = ((base.transform.localScale.z < 0f) ? (new Vector3(1f, 1f, -1f) * majorRadius) : (Vector3.one * majorRadius));
			minorRadius /= Mathf.Max(0.01f, majorRadius);
			Vector3[] vertices = originalMesh.vertices;
			int[] triangles = mesh.GetTriangles(0);
			foreach (int num in triangles)
			{
				Vector3 vector = vertices[num];
				Vector3 vector2 = vector;
				vector2.z = 0f;
				vector2.Normalize();
				vertices[num] = vector2 + (vector - vector2).normalized * minorRadius;
			}
			if (mesh.subMeshCount > 1)
			{
				triangles = mesh.GetTriangles(1);
				foreach (int num2 in triangles)
				{
					Vector3 vector3 = vertices[num2];
					vector3.Normalize();
					vertices[num2] = vector3 * (1f - minorRadius);
				}
			}
			mesh.vertices = vertices;
		}

		private void UpdateColliders()
		{
			if (m_useColliders)
			{
				float majorRadius = m_majorRadius * base.ModelScale;
				float minorRadius = m_minorRadius * base.SelectionMargin * 10f;
				float majorRadius2 = m_outerRadius * base.ModelScale;
				Mesh mesh = Object.Instantiate(m_axisColliderMesh);
				UpdateCircle(mesh, m_axisColliderMesh, m_xCollider.transform, majorRadius, minorRadius);
				m_xCollider.sharedMesh = null;
				m_xCollider.sharedMesh = mesh;
				UpdateCircle(mesh, m_axisColliderMesh, m_yCollider.transform, majorRadius, minorRadius);
				m_yCollider.sharedMesh = null;
				m_yCollider.sharedMesh = mesh;
				UpdateCircle(mesh, m_axisColliderMesh, m_zCollider.transform, majorRadius, minorRadius);
				m_zCollider.sharedMesh = null;
				m_zCollider.sharedMesh = mesh;
				Mesh mesh2 = Object.Instantiate(m_ssMesh);
				UpdateCircle(mesh2, m_ssMesh, m_innerCollider.transform, majorRadius, minorRadius);
				m_innerCollider.sharedMesh = null;
				m_innerCollider.sharedMesh = mesh2;
				Mesh mesh3 = Object.Instantiate(m_axisColliderMesh);
				UpdateCircle(mesh3, m_ssMesh, m_outerCollider.transform, majorRadius2, minorRadius);
				m_outerCollider.sharedMesh = null;
				m_outerCollider.sharedMesh = mesh3;
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
			Camera camera = Window.Camera;
			if (base.Editor.IsVR)
			{
				m_innerCollider.transform.LookAt(m_innerCollider.transform.position - (camera.transform.position - m_innerCollider.transform.position), Vector3.up);
				m_outerCollider.transform.LookAt(m_outerCollider.transform.position - (camera.transform.position - m_outerCollider.transform.position), Vector3.up);
			}
			else
			{
				m_innerCollider.transform.LookAt(m_innerCollider.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
				m_outerCollider.transform.LookAt(m_outerCollider.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
			}
			for (int i = 0; i < m_colliders.Length; i++)
			{
				Collider collider2 = m_colliders[i];
				if (collider2 == m_innerCollider)
				{
					if (m_lockObj.RotationFree)
					{
						continue;
					}
				}
				else if (collider2 == m_xCollider)
				{
					if (m_lockObj.RotationX)
					{
						continue;
					}
				}
				else if (collider2 == m_yCollider)
				{
					if (m_lockObj.RotationY)
					{
						continue;
					}
				}
				else if (collider2 == m_zCollider)
				{
					if (m_lockObj.RotationZ)
					{
						continue;
					}
				}
				else if (collider2 == m_outerCollider && m_lockObj.RotationScreen)
				{
					continue;
				}
				m_colliders[i].gameObject.SetActive(value: true);
				if (m_colliders[i].Raycast(ray, out var hitInfo, camera.farClipPlane) && hitInfo.distance < num)
				{
					collider = hitInfo.collider;
					num = hitInfo.distance;
				}
				m_colliders[i].gameObject.SetActive(value: false);
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
			if (collider == m_innerCollider)
			{
				return RuntimeHandleAxis.Free;
			}
			if (collider == m_outerCollider)
			{
				return RuntimeHandleAxis.Screen;
			}
			distance = float.PositiveInfinity;
			return RuntimeHandleAxis.None;
		}

		protected override void Update()
		{
			base.Update();
			if (m_prevMinorRadius != m_minorRadius || m_prevMajorRadius != m_majorRadius || m_prevOuterRadius != m_outerRadius)
			{
				m_prevMinorRadius = m_minorRadius;
				m_prevMajorRadius = m_majorRadius;
				m_prevOuterRadius = m_outerRadius;
				UpdateModel();
			}
		}

		public override void UpdateModel()
		{
			float majorRadius = m_majorRadius * base.ModelScale;
			float minorRadius = m_minorRadius * base.ModelScale;
			float majorRadius2 = m_outerRadius * base.ModelScale;
			UpdateXYZ(m_xyz.sharedMesh, majorRadius, minorRadius);
			UpdateCircle(m_inner.sharedMesh, m_innerCircleMesh, m_inner.transform, majorRadius, minorRadius);
			UpdateCircle(m_outer.sharedMesh, m_outerCircleMesh, m_outer.transform, majorRadius2, minorRadius);
			UpdateColliders();
			base.UpdateModel();
		}
	}
}
