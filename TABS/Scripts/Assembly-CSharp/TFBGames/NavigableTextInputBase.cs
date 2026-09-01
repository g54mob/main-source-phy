using System;
using System.Collections;
using BitCode.Extensions;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	[RequireComponent(typeof(Image))]
	public abstract class NavigableTextInputBase<T> : Selectable, ISubmitHandler, IEventSystemHandler
	{
		[SerializeField]
		protected KeyboardType m_InputType;

		[SerializeField]
		protected T m_InputField;

		[SerializeField]
		protected int m_MaxTextLength = 32;

		[SerializeField]
		[Header("Custom Messages to Display")]
		protected string m_DefaultTextToDisplay = "";

		[SerializeField]
		protected string m_TitleToDisplay = "";

		[SerializeField]
		[Multiline]
		protected string m_DescriptionForUser = "";

		protected Graphic m_RaycastGraphic;

		protected bool m_EnableInput;

		protected bool m_InlineText;

		protected bool m_SubmitGuard;

		protected ISystemKeyboard m_Keyboard;

		public abstract string text { get; set; }

		public string DefaultText => m_DefaultTextToDisplay;

		public bool IsTextInputEnabled => m_EnableInput;

		public event Action InputEnabled;

		public event Action InputDisabled;

		protected override void Awake()
		{
			base.Awake();
			m_RaycastGraphic = base.gameObject.GetOrAddComponent<Image>();
			if (m_RaycastGraphic == null)
			{
				m_RaycastGraphic.color = Color.clear;
			}
		}

		protected override void Start()
		{
			base.Start();
			if (m_InputField != null && !string.IsNullOrWhiteSpace(m_DefaultTextToDisplay))
			{
				text = m_DefaultTextToDisplay;
			}
			m_InlineText = true;
			if (base.image != null)
			{
				base.image.raycastTarget = m_InlineText;
			}
			if (m_RaycastGraphic != null)
			{
				m_RaycastGraphic.raycastTarget = m_InlineText;
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			UnsubscribeFromKeyboardEvents();
		}

		protected virtual void SubscribeToKeyboardEvents()
		{
			m_Keyboard = ServiceLocator.GetService<SystemKeyboardProvider>().Keyboard;
			if (m_Keyboard != null)
			{
				m_Keyboard.InputStarted += InputStarted;
				m_Keyboard.InputCompleted += InputCompleted;
				m_Keyboard.InputCancelled += InputCancelled;
				m_Keyboard.InputError += InputError;
			}
		}

		protected virtual void UnsubscribeFromKeyboardEvents()
		{
			if (m_Keyboard != null)
			{
				m_Keyboard.InputStarted -= InputStarted;
				m_Keyboard.InputCompleted -= InputCompleted;
				m_Keyboard.InputCancelled -= InputCancelled;
				m_Keyboard.InputError -= InputError;
			}
		}

		protected virtual void InputStarted(string result)
		{
		}

		protected virtual void InputCompleted(string result)
		{
			text = result;
			DisableTextInput();
		}

		protected virtual void InputCancelled(string result)
		{
			DisableTextInput();
		}

		protected virtual void InputError(string result)
		{
			DisableTextInput();
		}

		public virtual void EnableTextInput()
		{
			SubscribeToKeyboardEvents();
			m_EnableInput = true;
			this.InputEnabled?.Invoke();
			ShowNativeKeyboard();
		}

		public virtual void DisableTextInput()
		{
			m_EnableInput = false;
			m_SubmitGuard = true;
			Select();
			this.InputDisabled?.Invoke();
			UnsubscribeFromKeyboardEvents();
			StartCoroutine(Delay());
			IEnumerator Delay()
			{
				yield return null;
				m_SubmitGuard = false;
			}
		}

		public virtual void OnSubmit(BaseEventData eventData)
		{
			if (!m_SubmitGuard)
			{
				EnableTextInput();
			}
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			EnableTextInput();
		}

		protected virtual void Update()
		{
			if (m_EnableInput && PlayerActions.Instance.m_accept.WasPressed && !PlayerActions.Instance.m_newLineModifier.IsPressed)
			{
				DisableTextInput();
			}
		}

		protected virtual void ShowNativeKeyboard()
		{
			string defaultText = ((text.Length > 0) ? text : m_DefaultTextToDisplay);
			m_Keyboard?.Show(m_InputType, defaultText, m_TitleToDisplay, m_DescriptionForUser, m_MaxTextLength);
		}
	}
}
