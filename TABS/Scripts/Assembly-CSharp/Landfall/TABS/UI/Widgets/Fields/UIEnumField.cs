using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Landfall.TABS.UI.Widgets.Fields
{
	public class UIEnumField : UIPropertyField
	{
		[SerializeField]
		private TMP_Text m_inputFieldText;

		private List<string> m_enumEntries = new List<string>();

		private int m_currentValue;

		[SerializeField]
		private UnityEvent<string> m_onValueChangedCallback;

		protected override void Awake()
		{
			base.Label = base.transform.Find("Label").GetComponent<TMP_Text>();
			if (m_onValueChangedCallback == null)
			{
				m_onValueChangedCallback = new TMP_InputField.OnChangeEvent();
			}
			m_onValueChangedCallback.AddListener(OnEnumValueChanged);
		}

		private void OnEnumValueChanged(string value)
		{
			object value2 = Enum.Parse(base.PropertyField.FieldType, value);
			base.PropertyField.SetValue(base.PropertyOwner, value2);
		}

		public override void BindObject(object propertyOwner, FieldInfo propertyField)
		{
			if (!propertyField.FieldType.IsEnum)
			{
				Debug.LogError("Could not bind field of type " + propertyField.FieldType.ToString() + ". Type is not an enum!");
			}
			base.PropertyOwner = propertyOwner;
			base.PropertyField = propertyField;
			Debug.LogError($"Enum Type: {base.PropertyField.FieldType}");
			foreach (object value2 in Enum.GetValues(propertyField.FieldType))
			{
				m_enumEntries.Add(value2.ToString());
			}
			object value = base.PropertyField.GetValue(base.PropertyOwner);
			SetValue(value.ToString());
		}

		public override void SetCallback(UnityAction<string> call)
		{
			m_onValueChangedCallback.AddListener(call);
		}

		public void PreviousValue()
		{
			m_currentValue--;
			if (m_currentValue < 0)
			{
				m_currentValue = m_enumEntries.Count - 1;
			}
			SetValue(m_currentValue);
		}

		public void NextValue()
		{
			m_currentValue++;
			if (m_currentValue >= m_enumEntries.Count)
			{
				m_currentValue = 0;
			}
			SetValue(m_currentValue);
		}

		public void SetValue(int index)
		{
			string text = m_enumEntries[index];
			m_inputFieldText.text = text;
			m_onValueChangedCallback?.Invoke(text);
		}

		public override void SetValue(string value)
		{
			m_inputFieldText.text = value;
			UpdateIndexBasedOnInput(value);
		}

		private void UpdateIndexBasedOnInput(string value)
		{
		}
	}
}
