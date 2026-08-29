using System;
using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.RTHandles
{
	public class PrefabSpawnPoint : MonoBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField]
		private GameObject m_prefab;

		[SerializeField]
		private Image m_preview;

		[SerializeField]
		private Text m_prefabName;

		[SerializeField]
		private Vector3 m_prefabScale = Vector3.one;

		private Texture2D m_texture;

		private Sprite m_sprite;

		private GameObject m_prefabInstance;

		private HashSet<Transform> m_prefabInstanceTransforms;

		private Plane m_dragPlane;

		private IRTE m_editor;

		private RuntimeWindow m_scene;

		protected Vector3 PrefabScale => m_prefabScale;

		protected GameObject PrefabInstance
		{
			get
			{
				return m_prefabInstance;
			}
			set
			{
				m_prefabInstance = value;
			}
		}

		protected RuntimeWindow Scene => m_scene;

		protected virtual void Start()
		{
			if (m_prefab == null)
			{
				Debug.LogWarning("m_prefab is not set");
				return;
			}
			m_editor = IOC.Resolve<IRTE>();
			m_scene = m_editor.GetWindow(RuntimeWindowType.Scene);
			IResourcePreviewUtility resourcePreviewUtility = IOC.Resolve<IResourcePreviewUtility>();
			m_texture = resourcePreviewUtility.CreatePreview(m_prefab);
			if (m_preview != null)
			{
				m_preview.sprite = Sprite.Create(m_texture, new Rect(0f, 0f, m_texture.width, m_texture.height), new Vector2(0.5f, 0.5f));
				m_preview.color = Color.white;
			}
			if (m_prefabName != null)
			{
				m_prefabName.text = m_prefab.name;
			}
		}

		protected virtual void OnDestroy()
		{
			if (m_texture != null)
			{
				UnityEngine.Object.Destroy(m_texture);
				m_texture = null;
			}
		}

		protected virtual Plane GetDragPlane(Camera camera, Pointer pointer, Vector3 scenePivot)
		{
			Vector3 up = Vector3.up;
			up = ((!(Mathf.Abs(Vector3.Dot(camera.transform.up, Vector3.up)) > Mathf.Cos(MathF.PI / 180f))) ? Vector3.up : Vector3.Cross(camera.transform.right, Vector3.up));
			return new Plane(up, scenePivot);
		}

		protected virtual bool GetPointOnDragPlane(Camera camera, Pointer pointer, out Vector3 point, out Quaternion rotation)
		{
			Ray ray = pointer;
			if (m_dragPlane.Raycast(ray, out var enter))
			{
				point = ray.GetPoint(enter);
				rotation = Quaternion.identity;
				return true;
			}
			point = Vector3.zero;
			rotation = Quaternion.identity;
			return false;
		}

		protected virtual GameObject InstantiatePrefab(GameObject prefab, Vector3 point, Quaternion rotation)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(prefab, point, rotation);
			gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, PrefabScale);
			return gameObject;
		}

		protected virtual ExposeToEditor ExposeToEditor(GameObject prefabInstance)
		{
			ExposeToEditor exposeToEditor = prefabInstance.GetComponent<ExposeToEditor>();
			if (exposeToEditor == null)
			{
				exposeToEditor = prefabInstance.AddComponent<ExposeToEditor>();
			}
			return exposeToEditor;
		}

		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!(m_prefab == null))
			{
				IScenePivot scenePivot = m_scene.IOCContainer.Resolve<IScenePivot>();
				m_dragPlane = GetDragPlane(m_scene.Camera, m_scene.Pointer, scenePivot.SecondaryPivot);
				bool activeSelf = m_prefab.activeSelf;
				m_prefab.SetActive(value: false);
				if (GetPointOnDragPlane(m_scene.Camera, m_scene.Pointer, out var point, out var rotation))
				{
					m_prefabInstance = InstantiatePrefab(m_prefab, point, rotation);
				}
				else
				{
					m_prefabInstance = InstantiatePrefab(m_prefab, Vector3.zero, Quaternion.identity);
				}
				m_prefabInstanceTransforms = new HashSet<Transform>(m_prefabInstance.GetComponentsInChildren<Transform>(includeInactive: true));
				m_prefab.SetActive(activeSelf);
				ExposeToEditor(m_prefabInstance).SetName(m_prefab.name);
				m_prefabInstance.SetActive(value: true);
			}
		}

		public virtual void OnDrag(PointerEventData eventData)
		{
			if (GetPointOnDragPlane(m_scene.Camera, m_scene.Pointer, out var point, out var rotation) && m_prefabInstance != null)
			{
				m_prefabInstance.transform.position = point;
				m_prefabInstance.transform.rotation = rotation;
				RaycastHit raycastHit = (from h in Physics.RaycastAll(m_scene.Pointer)
					where !m_prefabInstanceTransforms.Contains(h.transform)
					select h).FirstOrDefault();
				if (raycastHit.transform != null)
				{
					m_prefabInstance.transform.position = raycastHit.point;
				}
			}
		}

		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (m_prefabInstance != null)
			{
				ExposeToEditor component = m_prefabInstance.GetComponent<ExposeToEditor>();
				m_editor.Undo.BeginRecord();
				m_editor.Undo.RegisterCreatedObjects(new ExposeToEditor[1] { component });
				m_editor.Selection.activeObject = m_prefabInstance;
				m_editor.Undo.EndRecord();
			}
			m_prefabInstance = null;
			m_prefabInstanceTransforms = null;
		}
	}
}
