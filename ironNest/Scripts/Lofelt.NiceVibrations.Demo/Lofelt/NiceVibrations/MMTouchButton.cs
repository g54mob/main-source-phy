using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations
{
	[RequireComponent(typeof(Rect))]
	[RequireComponent(typeof(CanvasGroup))]
	public class MMTouchButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler, ISubmitHandler
	{
		public enum ButtonStates
		{
			Off = 0,
			ButtonDown = 1,
			ButtonPressed = 2,
			ButtonUp = 3,
			Disabled = 4
		}

		[Header("Binding")]
		public UnityEvent ButtonPressedFirstTime;

		public UnityEvent ButtonReleased;

		public UnityEvent ButtonPressed;

		[Header("Sprite Swap")]
		public Sprite DisabledSprite;

		public Sprite PressedSprite;

		public Sprite HighlightedSprite;

		[Header("Color Changes")]
		public bool PressedChangeColor;

		public Color PressedColor;

		public bool LerpColor;

		public float LerpColorDuration;

		public AnimationCurve LerpColorCurve;

		[Header("Opacity")]
		public float PressedOpacity;

		public float IdleOpacity;

		public float DisabledOpacity;

		[Header("Delays")]
		public float PressedFirstTimeDelay;

		public float ReleasedDelay;

		[Header("Buffer")]
		public float BufferDuration;

		[Header("Animation")]
		public Animator Animator;

		public string IdleAnimationParameterName;

		public string DisabledAnimationParameterName;

		public string PressedAnimationParameterName;

		[Header("Mouse Mode")]
		public bool MouseMode;

		protected bool _zonePressed;

		protected CanvasGroup _canvasGroup;

		protected float _initialOpacity;

		protected Animator _animator;

		protected Image _image;

		protected Sprite _initialSprite;

		protected Color _initialColor;

		protected float _lastClickTimestamp;

		protected Selectable _selectable;

		protected float _lastStateChangeAt;

		protected Color _imageColor;

		protected Color _fromColor;

		protected Color _toColor;

		public bool ReturnToInitialSpriteAutomatically { get; set; }

		public ButtonStates CurrentState { get; protected set; }

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		public virtual void OnPointerDown(PointerEventData data)
		{
		}

		protected virtual void InvokePressedFirstTime()
		{
		}

		public virtual void OnPointerUp(PointerEventData data)
		{
		}

		protected virtual void InvokeReleased()
		{
		}

		public virtual void OnPointerPressed()
		{
		}

		protected virtual void ResetButton()
		{
		}

		public virtual void OnPointerEnter(PointerEventData data)
		{
		}

		public virtual void OnPointerExit(PointerEventData data)
		{
		}

		protected virtual void OnEnable()
		{
		}

		public virtual void DisableButton()
		{
		}

		public virtual void EnableButton()
		{
		}

		protected virtual void SetOpacity(float newOpacity)
		{
		}

		protected virtual void UpdateAnimatorStates()
		{
		}

		public virtual void OnSubmit(BaseEventData eventData)
		{
		}

		protected virtual float Remap(float x, float A, float B, float C, float D)
		{
			return 0f;
		}
	}
}
