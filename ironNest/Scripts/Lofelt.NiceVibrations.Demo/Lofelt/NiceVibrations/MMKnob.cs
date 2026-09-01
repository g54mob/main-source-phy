using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations
{
	[RequireComponent(typeof(Rect))]
	public class MMKnob : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		[Header("Bindings")]
		public Camera TargetCamera;

		[Header("Settings")]
		public float MinimumAngle;

		public float MaximumAngle;

		public float MaximumDistance;

		public Color ActiveColor;

		public Color InactiveColor;

		[Header("Output")]
		public bool Dragging;

		public float Value;

		public bool Active;

		public Image _image;

		protected PointerEventData _pointerEventData;

		protected float _distance;

		public RectTransform _rectTransform;

		protected Vector3 _rotation;

		protected Canvas _canvas;

		protected Vector2 _workPosition;

		public RenderMode ParentCanvasRenderMode { get; protected set; }

		protected virtual void Awake()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void SetRotation(float angle)
		{
		}

		public virtual void SetActive(bool status)
		{
		}

		public virtual void SetValue(float value)
		{
		}

		public void OnPointerDown(PointerEventData eventData)
		{
		}

		public void OnPointerUp(PointerEventData eventData)
		{
		}

		protected virtual Vector3 GetWorldPosition(Vector3 testPosition)
		{
			return default(Vector3);
		}
	}
}
