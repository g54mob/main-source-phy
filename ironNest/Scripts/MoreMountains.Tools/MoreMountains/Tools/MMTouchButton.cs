using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(Rect))]
	[RequireComponent(typeof(CanvasGroup))]
	[AddComponentMenu("More Mountains/Tools/Controls/MMTouchButton")]
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

		[Header("Interaction")]
		public bool Interactable;

		[Header("Binding")]
		[Tooltip("The method(s) to call when the button gets pressed down")]
		public UnityEvent ButtonPressedFirstTime;

		[Tooltip("The method(s) to call when the button gets released")]
		public UnityEvent ButtonReleased;

		[Tooltip("The method(s) to call while the button is being pressed")]
		public UnityEvent ButtonPressed;

		[Header("Sprite Swap")]
		[MMInformation("Here you can define, for disabled and pressed states, if you want a different sprite, and a different color.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the sprite to use on the button when it's in the disabled state")]
		public Sprite DisabledSprite;

		[Tooltip("whether or not to change color when the button is disabled")]
		public bool DisabledChangeColor;

		[Tooltip("the color to use when the button is disabled")]
		[MMCondition("DisabledChangeColor", true)]
		public Color DisabledColor;

		[Tooltip("the sprite to use on the button when it's in the pressed state")]
		public Sprite PressedSprite;

		[Tooltip("whether or not to change the button color on press")]
		public bool PressedChangeColor;

		[Tooltip("the color to use when the button is pressed")]
		[MMCondition("PressedChangeColor", true)]
		public Color PressedColor;

		[Tooltip("the sprite to use on the button when it's in the highlighted state")]
		public Sprite HighlightedSprite;

		[Tooltip("whether or not to change color when highlighting the button")]
		public bool HighlightedChangeColor;

		[Tooltip("the color to use when the button is highlighted")]
		[MMCondition("HighlightedChangeColor", true)]
		public Color HighlightedColor;

		[Header("Opacity")]
		[MMInformation("Here you can set different opacities for the button when it's pressed, idle, or disabled. Useful for visual feedback.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the opacity to apply to the canvas group when the button is pressed")]
		public float PressedOpacity;

		[Tooltip("the new opacity to apply to the canvas group when the button is idle")]
		public float IdleOpacity;

		[Tooltip("the new opacity to apply to the canvas group when the button is disabled")]
		public float DisabledOpacity;

		[Header("Delays")]
		[MMInformation("Specify here the delays to apply when the button is pressed initially, and when it gets released. Usually you'll keep them at 0.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the delay to apply to events when the button gets pressed for the first time")]
		public float PressedFirstTimeDelay;

		[Tooltip("the delay to apply to events when the button gets released")]
		public float ReleasedDelay;

		[Header("Buffer")]
		[Tooltip("the duration (in seconds) after a press during which the button can't be pressed again")]
		public float BufferDuration;

		[Header("Animation")]
		[MMInformation("Here you can bind an animator, and specify animation parameter names for the various states.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("an animator you can bind to this button to have its states updated to reflect the button's states")]
		public Animator Animator;

		[Tooltip("the name of the animation parameter to turn true when the button is idle")]
		public string IdleAnimationParameterName;

		[Tooltip("the name of the animation parameter to turn true when the button is disabled")]
		public string DisabledAnimationParameterName;

		[Tooltip("the name of the animation parameter to turn true when the button is pressed")]
		public string PressedAnimationParameterName;

		[Header("Mouse Mode")]
		[MMInformation("If you set this to true, you'll need to actually press the button for it to be triggered, otherwise a simple hover will trigger it (better to leave it unchecked if you're going for touch input).", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("If you set this to true, you'll need to actually press the button for it to be triggered, otherwise a simple hover will trigger it (better for touch input).")]
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

		public virtual bool ReturnToInitialSpriteAutomatically { get; set; }

		public virtual ButtonStates CurrentState { get; protected set; }

		public event Action<PointerEventData.FramePressState, PointerEventData> ButtonStateChange
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

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

		public virtual void InvokeButtonStateChange(PointerEventData.FramePressState newState, PointerEventData data)
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

		private void OnDisable()
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
	}
}
