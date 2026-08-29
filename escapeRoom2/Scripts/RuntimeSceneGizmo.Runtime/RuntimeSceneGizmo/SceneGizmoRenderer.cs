using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeSceneGizmo
{
	public class SceneGizmoRenderer : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField]
		private RawImage imageHolder;

		private RectTransform imageHolderTR;

		[SerializeField]
		public SceneGizmoController controller;

		[SerializeField]
		[Tooltip("Should gizmo's hovered components turn yellow")]
		private bool highlightHoveredComponents = true;

		private PointerEventData hoveringPointer;

		[SerializeField]
		[Tooltip("(Optional) Gizmo will match the reference Transform's rotation")]
		private Transform m_referenceTransform;

		[SerializeField]
		private ComponentClickedEvent m_onComponentClicked;

		public Transform ReferenceTransform
		{
			get
			{
				return m_referenceTransform;
			}
			set
			{
				m_referenceTransform = value;
				controller.ReferenceTransform = value;
			}
		}

		public ComponentClickedEvent OnComponentClicked => m_onComponentClicked;

		private void Awake()
		{
			imageHolderTR = (RectTransform)imageHolder.transform;
			controller = Object.Instantiate(controller);
			imageHolder.texture = controller.TargetTextureBlack;
			imageHolder.material.SetTexture("_WhiteTex", controller.TargetTextureWhite);
		}

		private void Start()
		{
			if (m_referenceTransform != null && !m_referenceTransform.Equals(null))
			{
				controller.ReferenceTransform = m_referenceTransform;
			}
		}

		private void OnEnable()
		{
			if (controller != null && !controller.Equals(null))
			{
				controller.gameObject.SetActive(value: true);
			}
		}

		private void OnDisable()
		{
			if (controller != null && !controller.Equals(null))
			{
				controller.gameObject.SetActive(value: false);
			}
		}

		private void Update()
		{
			if (hoveringPointer != null)
			{
				controller.OnPointerHover(GetNormalizedPointerPosition(hoveringPointer));
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!eventData.dragging)
			{
				GizmoComponent gizmoComponent = controller.Raycast(GetNormalizedPointerPosition(eventData));
				if (gizmoComponent != GizmoComponent.None)
				{
					m_onComponentClicked.Invoke(gizmoComponent);
				}
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
		}

		private Vector3 GetNormalizedPointerPosition(PointerEventData eventData)
		{
			Vector2 size = imageHolderTR.rect.size;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(imageHolderTR, eventData.position, null, out var localPoint);
			return new Vector3(1f + localPoint.x / size.x, 1f + localPoint.y / size.y, 0f);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (highlightHoveredComponents)
			{
				hoveringPointer = eventData;
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (hoveringPointer != null)
			{
				controller.OnPointerHover(new Vector3(-10f, -10f, 0f));
				hoveringPointer = null;
			}
		}
	}
}
