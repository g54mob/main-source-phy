using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class MMTouchButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler, ISubmitHandler
{
	public enum ButtonStates
	{
		Off,
		ButtonDown,
		ButtonPressed,
		ButtonUp,
		Disabled
	}

	public UnityEvent ButtonPressedFirstTime;

	public UnityEvent ButtonReleased;

	public UnityEvent ButtonPressed;

	public Sprite DisabledSprite;

	public Sprite PressedSprite;

	public Sprite HighlightedSprite;

	public bool PressedChangeColor;

	public Color PressedColor;

	public bool LerpColor;

	public float LerpColorDuration;

	public AnimationCurve LerpColorCurve;

	public float PressedOpacity;

	public float IdleOpacity;

	public float DisabledOpacity;

	public float PressedFirstTimeDelay;

	public float ReleasedDelay;

	public float BufferDuration;

	public Animator Animator;

	public string IdleAnimationParameterName;

	public string DisabledAnimationParameterName;

	public string PressedAnimationParameterName;

	public bool MouseMode;

	private bool _003CReturnToInitialSpriteAutomatically_003Ek__BackingField;

	private ButtonStates _003CCurrentState_003Ek__BackingField;

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

	public bool ReturnToInitialSpriteAutomatically
	{
		get
		{
			return _003CReturnToInitialSpriteAutomatically_003Ek__BackingField;
		}
		set
		{
			_003CReturnToInitialSpriteAutomatically_003Ek__BackingField = value;
		}
	}

	public ButtonStates CurrentState
	{
		get
		{
			return _003CCurrentState_003Ek__BackingField;
		}
		protected set
		{
			_003CCurrentState_003Ek__BackingField = value;
		}
	}

	protected virtual void Awake()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+1D8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+1E0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected virtual void Initialization()
	{
		//IL_0072: Expected O, but got F4
		//IL_0125: Expected O, but got I4
		//IL_01b4: Expected I, but got O
		//IL_017c: Expected O, but got I4
		_003CReturnToInitialSpriteAutomatically_003Ek__BackingField = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Selectable selectable = default(Selectable);
		_selectable = selectable;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Image image = default(Image);
		_image = image;
		if (_image != null)
		{
			Color color = _image.color;
			Image image2 = _image;
			float r = color.r;
			_initialColor = (Color)color.r;
			_initialSprite = image2.m_Sprite;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Animator animator = default(Animator);
		_animator = animator;
		if (Animator != null)
		{
			_animator = Animator;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		CanvasGroup canvasGroup = default(CanvasGroup);
		_canvasGroup = canvasGroup;
		bool flag = _canvasGroup != null;
		bool flag2 = !flag;
		object obj = 0;
		if (!flag2)
		{
			_initialOpacity = IdleOpacity;
			_canvasGroup.alpha = IdleOpacity;
			float r = _canvasGroup.alpha;
			_initialOpacity = r;
			obj = 0;
		}
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v296 @ rdx_v16 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+258] (should have been resolved before IL gen)");
		Cpp2ILHelpers.NoteDecompilerIssue("Warning: Method ends with non empty stack (-38), the output could be wrong!");
		/*Error: End of method reached without returning.*/;
	}

	protected unsafe virtual void Update()
	{
		//IL_0015: Expected O, but got I4
		//IL_0390: Expected O, but got I4
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0518: Expected I, but got O
		//IL_0528: Expected O, but got I
		//IL_0538: Expected O, but got I
		//IL_03b8: Expected O, but got I4
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_04d2: Expected I, but got O
		//IL_04e2: Expected O, but got I
		//IL_04ef: Expected O, but got Ref
		//IL_03fc: Expected O, but got I4
		//IL_0472: Invalid comparison between I4 and F4
		//IL_04c5: Expected F4, but got I4
		//IL_0202: Expected O, but got Ref
		//IL_0216: Expected F4, but got O
		bool flag = _003CCurrentState_003Ek__BackingField == ButtonStates.Off;
		Color pressedColor = default(Color);
		if (!flag)
		{
			object obj = _003CCurrentState_003Ek__BackingField - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					object obj3 = obj2 - 1;
					if (!flag && (nint)obj3 == 1)
					{
						float disabledOpacity = DisabledOpacity;
						SetOpacity(DisabledOpacity);
						if (_image != null && DisabledSprite != null)
						{
							_image.sprite = DisabledSprite;
						}
						if (_selectable != null)
						{
							_selectable.interactable = false;
						}
					}
				}
				else
				{
					float disabledOpacity = PressedOpacity;
					SetOpacity(PressedOpacity);
					OnPointerPressed();
					if (_image != null)
					{
						if (PressedSprite != null)
						{
							_image.sprite = PressedSprite;
						}
						if (PressedChangeColor)
						{
							_image.color = (Color)(&pressedColor);
							pressedColor = PressedColor;
							float num = (float)PressedColor;
						}
					}
				}
			}
		}
		else
		{
			float disabledOpacity = IdleOpacity;
			SetOpacity(IdleOpacity);
			if (_image != null && _003CReturnToInitialSpriteAutomatically_003Ek__BackingField)
			{
				_image.sprite = _initialSprite;
			}
			if (_selectable != null)
			{
				_selectable.interactable = true;
				EventSystem current = EventSystem.current;
				GameObject gameObject = base.gameObject;
				if (current.m_CurrentSelected == gameObject && HighlightedSprite != null)
				{
					_image.sprite = HighlightedSprite;
				}
			}
		}
		bool flag2 = _image != null;
		bool flag3 = !flag2;
		object obj4 = 0;
		Image image = default(Image);
		float num5 = default(float);
		if (!flag3)
		{
			bool flag4 = !PressedChangeColor;
			obj4 = 0;
			if (!flag4)
			{
				float time = Time.time;
				float num = time - _lastStateChangeAt;
				bool flag5 = !(LerpColorDuration > num);
				obj4 = 0;
				if (!flag5)
				{
					float time2 = Time.time;
					float num2 = time2 - _lastStateChangeAt;
					float num3 = Remap(num2, 0f, LerpColorDuration, 0f, 0f);
					float num4 = LerpColorCurve.Evaluate(num2);
					image = _image;
					if (!(0f > num4))
					{
						bool flag6 = !(num4 > 1f);
						num5 = num4;
						if (!flag6)
						{
							num5 = 1f;
						}
					}
					else
					{
						num5 = 0f;
					}
					goto IL_0542;
				}
			}
		}
		goto IL_0513;
		IL_0513:
		nint num6 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v479 @ rdx_v5 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+2C8]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v479 @ rdx_v5 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+2D0]");
		object obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v481 @ rax_v8 (should have been resolved before IL gen)");
		goto IL_0542;
		IL_0542:
		float num8 = default(float);
		float num7 = num8 - num8;
		float num9 = num7 * num5;
		float num10 = num9 + num8;
		nint num11 = (nint)image;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v878 @ r8_v6 (Il2CppClass<UnityEngine.UI.Image>)+2B0]");
		obj4 = 0;
		image.color = (Color)(&pressedColor);
		goto IL_0513;
	}

	protected virtual void LateUpdate()
	{
		if (_003CCurrentState_003Ek__BackingField == ButtonStates.ButtonUp)
		{
			float time = Time.time;
			_lastStateChangeAt = time;
			_003CCurrentState_003Ek__BackingField = ButtonStates.Off;
			_fromColor = PressedColor;
			_toColor = _initialColor;
		}
		if (_003CCurrentState_003Ek__BackingField == ButtonStates.ButtonDown)
		{
			float time2 = Time.time;
			_lastStateChangeAt = time2;
			_003CCurrentState_003Ek__BackingField = ButtonStates.ButtonPressed;
			_fromColor = _initialColor;
			_toColor = PressedColor;
		}
	}

	public virtual void OnPointerDown(PointerEventData data)
	{
		//IL_008a: Invalid comparison between F4 and I4
		//IL_00b3: Invalid comparison between F4 and I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F553]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		float time = Time.time;
		float num = time - _lastClickTimestamp;
		if (!(BufferDuration > num) && _003CCurrentState_003Ek__BackingField == ButtonStates.Off)
		{
			_003CCurrentState_003Ek__BackingField = ButtonStates.ButtonDown;
			float time2 = Time.time;
			_lastClickTimestamp = time2;
			float timeScale = Time.timeScale;
			bool flag = timeScale == 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A8C5C4h\"");
			if (!flag && PressedFirstTimeDelay > 0f)
			{
				Invoke("InvokePressedFirstTime", PressedFirstTimeDelay);
			}
			else
			{
				ButtonPressedFirstTime.Invoke();
			}
		}
	}

	protected virtual void InvokePressedFirstTime()
	{
		if (ButtonPressedFirstTime != null)
		{
			ButtonPressedFirstTime.Invoke();
		}
	}

	public virtual void OnPointerUp(PointerEventData data)
	{
		//IL_0072: Invalid comparison between F4 and I4
		//IL_009b: Invalid comparison between F4 and I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F554]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (_003CCurrentState_003Ek__BackingField == ButtonStates.ButtonPressed || _003CCurrentState_003Ek__BackingField == ButtonStates.ButtonDown)
		{
			_003CCurrentState_003Ek__BackingField = ButtonStates.ButtonUp;
			float timeScale = Time.timeScale;
			bool flag = timeScale == 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A8C6C2h\"");
			if (!flag && ReleasedDelay > 0f)
			{
				Invoke("InvokeReleased", ReleasedDelay);
			}
			else
			{
				ButtonReleased.Invoke();
			}
		}
	}

	protected virtual void InvokeReleased()
	{
		if (ButtonReleased != null)
		{
			ButtonReleased.Invoke();
		}
	}

	public virtual void OnPointerPressed()
	{
		bool flag = ButtonPressed == null;
		_003CCurrentState_003Ek__BackingField = ButtonStates.ButtonPressed;
		if (!flag)
		{
			ButtonPressed.Invoke();
		}
	}

	protected virtual void ResetButton()
	{
		SetOpacity(_initialOpacity);
		_003CCurrentState_003Ek__BackingField = ButtonStates.Off;
	}

	public virtual void OnPointerEnter(PointerEventData data)
	{
		//IL_0027: Expected I, but got O
		//IL_0037: Expected O, but got I
		//IL_0047: Expected O, but got I
		if (!MouseMode)
		{
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+208]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+210]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v13 @ rax_v1 (should have been resolved before IL gen)");
		}
	}

	public virtual void OnPointerExit(PointerEventData data)
	{
		//IL_0027: Expected I, but got O
		//IL_0037: Expected O, but got I
		//IL_0047: Expected O, but got I
		if (!MouseMode)
		{
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+228]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+230]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v13 @ rax_v1 (should have been resolved before IL gen)");
		}
	}

	protected virtual void OnEnable()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+258]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.MMTouchButton>)+260]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public virtual void DisableButton()
	{
		_003CCurrentState_003Ek__BackingField = ButtonStates.Disabled;
	}

	public virtual void EnableButton()
	{
		if (_003CCurrentState_003Ek__BackingField == ButtonStates.Disabled)
		{
			_003CCurrentState_003Ek__BackingField = ButtonStates.Off;
		}
	}

	protected virtual void SetOpacity(float newOpacity)
	{
		if (_canvasGroup != null)
		{
			_canvasGroup.alpha = newOpacity;
		}
	}

	protected virtual void UpdateAnimatorStates()
	{
		//IL_0052: Expected O, but got I4
		//IL_00aa: Expected O, but got I4
		if (_animator != null)
		{
			if (DisabledAnimationParameterName != null)
			{
				object obj = _003CCurrentState_003Ek__BackingField - 4;
				bool value = obj == null;
				_animator.SetBool(DisabledAnimationParameterName, value);
			}
			if (PressedAnimationParameterName != null)
			{
				object obj2 = _003CCurrentState_003Ek__BackingField - 2;
				bool value2 = obj2 == null;
				_animator.SetBool(PressedAnimationParameterName, value2);
			}
			if (IdleAnimationParameterName != null)
			{
				bool value3 = _003CCurrentState_003Ek__BackingField == ButtonStates.Off;
				_animator.SetBool(IdleAnimationParameterName, value3);
			}
		}
	}

	public virtual void OnSubmit(BaseEventData eventData)
	{
		if (ButtonPressedFirstTime != null)
		{
			ButtonPressedFirstTime.Invoke();
		}
		if (ButtonReleased != null)
		{
			ButtonReleased.Invoke();
		}
	}

	protected virtual float Remap(float x, float A, float B, float C, float D)
	{
		float num = x - A;
		object obj2 = default(object);
		object obj3 = default(object);
		object obj = obj2 - obj3;
		float num2 = B - A;
		float num3 = num / num2;
		float num4 = num3 * (float)obj;
		return num4 + (float)obj3;
	}

	public MMTouchButton()
	{
		//IL_0057: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F557]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		PressedColor = (Color)0;
		LerpColor = true;
		LerpColorDuration = 0.2f;
		PressedOpacity = 1f;
		IdleOpacity = 1f;
		DisabledOpacity = 1f;
		IdleAnimationParameterName = "Idle";
		DisabledAnimationParameterName = "Disabled";
		PressedAnimationParameterName = "Pressed";
		_lastStateChangeAt = -50f;
		base._002Ector();
	}
}
