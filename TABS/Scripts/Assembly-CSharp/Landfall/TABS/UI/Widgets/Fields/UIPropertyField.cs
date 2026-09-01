using System;
using System.Reflection;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Landfall.TABS.UI.Widgets.Fields
{
	public class UIPropertyField : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text m_label;

		[SerializeField]
		private bool m_OverrideLabel;

		[SerializeField]
		private WinConditionsSurviveForTime m_WinconditionDisplayInspector;

		[SerializeField]
		private TMP_InputField m_inputField;

		private object m_propertyOwner;

		private FieldInfo m_propertyField;

		public TMP_Text Label
		{
			get
			{
				return m_label;
			}
			set
			{
				m_label = value;
			}
		}

		public TMP_InputField InputField => m_inputField;

		protected object PropertyOwner
		{
			get
			{
				return m_propertyOwner;
			}
			set
			{
				m_propertyOwner = value;
			}
		}

		public FieldInfo PropertyField
		{
			get
			{
				return m_propertyField;
			}
			set
			{
				m_propertyField = value;
			}
		}

		protected virtual void Awake()
		{
			if (m_label == null)
			{
				m_label = base.transform.Find("Label").GetComponent<TMP_Text>();
			}
			if (m_inputField == null)
			{
				m_inputField = base.transform.Find("InputField").GetComponent<TMP_InputField>();
			}
		}

		public virtual void BindObject(object propertyOwner, FieldInfo propertyField)
		{
			m_propertyOwner = propertyOwner;
			m_propertyField = propertyField;
			m_inputField.onValueChanged.AddListener(OnPropertyChanged);
			object value = propertyField.GetValue(propertyOwner);
			m_WinconditionDisplayInspector.InitTimer(value.ToString());
			SetValue(value.ToString());
		}

		public virtual void SetCallback(UnityAction<string> call)
		{
			m_inputField.onValueChanged.AddListener(call);
		}

		protected virtual void OnPropertyChanged(string value)
		{
			object value2 = Convert.ChangeType(value, m_propertyField.FieldType);
			m_propertyField.SetValue(m_propertyOwner, value2);
			m_propertyField.SetValue(m_propertyOwner, value2);
		}

		protected void SetValueOnOwner<T>(T value)
		{
			m_propertyField.SetValue(m_propertyOwner, value);
		}

		public T ConvertFromStringTo<T>(string value)
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}

		public virtual void SetPropertyLabel(string labelText)
		{
			if (m_OverrideLabel)
			{
				m_label.text = labelText;
			}
		}

		public virtual void SetValue(string value)
		{
			m_inputField.text = value;
		}
	}
}
