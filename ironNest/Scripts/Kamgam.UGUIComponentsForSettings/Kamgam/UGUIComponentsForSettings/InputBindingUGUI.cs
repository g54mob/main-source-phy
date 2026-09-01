using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	[SelectionBase]
	public class InputBindingUGUI : MonoBehaviour
	{
		public delegate void OnChangedDelegate(string bindingPath);

		public Func<string, string> PathToDisplayNameFunc;

		public InputBindingForInputSystem InputBinding;

		public UnityEvent<string> OnChangedEvent;

		public OnChangedDelegate OnChanged;

		public Button Button;

		public GameObject Normal;

		public GameObject Active;

		public TextMeshProUGUI TextTf;

		public TextMeshProUGUI DisplayNameTf;

		public TextMeshProUGUI ActiveTextTf;

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

		public virtual string DisplayName
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

		public void CopyFrom(InputBindingUGUI other)
		{
		}

		public virtual void SetActive(bool active)
		{
		}

		protected void onBindingComplete()
		{
		}

		protected virtual void onBindingCanceled()
		{
		}

		public virtual void UpdateDisplayName()
		{
		}

		private string localizeString(string path)
		{
			return null;
		}

		public virtual bool IsCancelKeyPressed()
		{
			return false;
		}

		public void OnEnable()
		{
		}

		public void OnDisable()
		{
		}

		public virtual void Refresh()
		{
		}
	}
}
