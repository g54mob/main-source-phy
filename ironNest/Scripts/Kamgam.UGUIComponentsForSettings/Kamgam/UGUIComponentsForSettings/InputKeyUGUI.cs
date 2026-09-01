using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	[SelectionBase]
	public class InputKeyUGUI : MonoBehaviour
	{
		public delegate void OnChangedDelegate(UniversalKeyCode key, UniversalKeyCode modifierKey);

		public Func<UniversalKeyCode, string> KeyCodeToKeyNameFunc;

		[SerializeField]
		protected UniversalKeyCode _key;

		[SerializeField]
		protected UniversalKeyCode _modifierKey;

		public bool AllowMouseButtons;

		public bool AllowKeyCombinations;

		public bool AllowAbortWithCancelButton;

		public UnityEvent<UniversalKeyCode, UniversalKeyCode> OnChangedEvent;

		public OnChangedDelegate OnChanged;

		public Button Button;

		public GameObject Normal;

		public GameObject Active;

		public TextMeshProUGUI TextTf;

		public TextMeshProUGUI KeyNameTf;

		public TextMeshProUGUI ActiveTextTf;

		protected bool waitForKeyRelease;

		protected UniversalKeyCode _modifierKeyWhileActive;

		protected UniversalKeyCode _keyWhileActive;

		protected bool _aKeyWasPressedWhileActive;

		public UniversalKeyCode Key
		{
			get
			{
				return default(UniversalKeyCode);
			}
			set
			{
			}
		}

		public UniversalKeyCode ModifierKey
		{
			get
			{
				return default(UniversalKeyCode);
			}
			set
			{
			}
		}

		public bool IsActive => false;

		public string Text
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string KeyName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string ActiveText
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public void SetActive(bool active)
		{
		}

		public void UpdateKeyName()
		{
		}

		public bool IsCancelKeyPressed()
		{
			return false;
		}

		public void OnEnable()
		{
		}

		public void OnDisable()
		{
		}

		public void Refresh()
		{
		}

		public void Update()
		{
		}
	}
}
