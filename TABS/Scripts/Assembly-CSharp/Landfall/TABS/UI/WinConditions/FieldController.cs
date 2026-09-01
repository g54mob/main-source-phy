using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Landfall.TABS.UI.UIGroups.Attributes;
using Landfall.TABS.UI.Widgets.Fields;
using Landfall.TABS.Utils;
using Landfall.TABS.WinConditions;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UI.WinConditions
{
	public class FieldController : MonoBehaviour
	{
		public delegate void OnDirtyDelegate();

		[SerializeField]
		private GameObject m_intFieldPrefab;

		[SerializeField]
		private GameObject m_floatFieldPrefab;

		[SerializeField]
		private GameObject m_enumFieldPrefab;

		[SerializeField]
		private GameObject m_stringFieldPrefab;

		[SerializeField]
		private GameObject m_referenceField;

		[SerializeField]
		private Button m_EditButton;

		[SerializeField]
		private Button m_BackButton;

		[SerializeField]
		private WinConditionsBrowser m_WinConditionBrowser;

		private List<GameObject> m_currentFields = new List<GameObject>();

		private RectTransform m_rectTransform;

		private InspectorPanel m_inspectorPanel;

		public OnDirtyDelegate OnDirtyCallback { get; set; }

		private void Start()
		{
			m_rectTransform = GetComponent<RectTransform>();
			m_inspectorPanel = GetComponentInParent<InspectorPanel>();
		}

		public void BindObject(object obj)
		{
			RemoveAllFields();
			foreach (FieldInfo unitySerializableField in TypeUtils.GetUnitySerializableFields(obj.GetType()))
			{
				Type fieldType = unitySerializableField.FieldType;
				string propertyLabel = ConvertToDisplayName(unitySerializableField.Name);
				GameObject gameObject = null;
				if (fieldType == typeof(int))
				{
					gameObject = UnityEngine.Object.Instantiate(m_intFieldPrefab);
				}
				else if (fieldType == typeof(float))
				{
					gameObject = UnityEngine.Object.Instantiate(m_floatFieldPrefab);
					WinConditionsSurviveForTime componentInChildren = gameObject.GetComponentInChildren<WinConditionsSurviveForTime>();
					if ((bool)componentInChildren)
					{
						componentInChildren.SetBrowser(m_WinConditionBrowser);
					}
				}
				else if (fieldType == typeof(string))
				{
					gameObject = UnityEngine.Object.Instantiate(m_stringFieldPrefab);
				}
				else if (fieldType.IsEnum)
				{
					gameObject = UnityEngine.Object.Instantiate(m_enumFieldPrefab);
				}
				else if (fieldType.GetGenericTypeDefinition() == typeof(ReferenceRequest<>))
				{
					gameObject = UnityEngine.Object.Instantiate(m_referenceField);
					UIReferenceField component = gameObject.GetComponent<UIReferenceField>();
					component.SetButtonReference(m_EditButton, m_BackButton);
					component.SetReferenceType(fieldType.GetGenericArguments()[0]);
					LockReferenceToTeamAttribute customAttribute = unitySerializableField.GetCustomAttribute<LockReferenceToTeamAttribute>();
					if (customAttribute != null)
					{
						component.LockToTeam(m_inspectorPanel.Team, customAttribute.TeamLock);
					}
					else
					{
						Debug.LogError("Missing Team Lock attribute!");
					}
				}
				if (gameObject == null)
				{
					Debug.LogError("Field type: " + unitySerializableField.FieldType.ToString() + " not supported!");
					continue;
				}
				gameObject.transform.SetParent(base.transform, worldPositionStays: false);
				UIPropertyField component2 = gameObject.GetComponent<UIPropertyField>();
				component2.SetPropertyLabel(propertyLabel);
				component2.BindObject(obj, unitySerializableField);
				m_currentFields.Add(gameObject);
				component2.SetCallback(OnValueChanged);
			}
		}

		public void UpdateContentHeight()
		{
			float num = 0f;
			for (int i = 0; i < m_currentFields.Count; i++)
			{
				RectTransform component = m_currentFields[i].GetComponent<RectTransform>();
				num += component.rect.height;
			}
			m_rectTransform.sizeDelta = new Vector2(m_rectTransform.sizeDelta.x, num);
		}

		private void OnValueChanged(string newValue)
		{
			OnDirtyCallback?.Invoke();
		}

		private void RemoveAllFields()
		{
			foreach (GameObject currentField in m_currentFields)
			{
				UnityEngine.Object.Destroy(currentField);
			}
			m_currentFields.Clear();
		}

		private string ConvertToDisplayName(string fieldName)
		{
			string text = string.Empty;
			if (fieldName.StartsWith("m_"))
			{
				text = fieldName.Substring(2);
			}
			else if (fieldName.StartsWith("_"))
			{
				text = fieldName.Substring(1);
			}
			return text.First().ToString().ToUpper() + text.Substring(1);
		}

		public void TurnOnEditButton()
		{
			if (m_EditButton != null)
			{
				m_EditButton.gameObject.SetActive(value: true);
			}
		}

		public void TurnOffEditButton()
		{
			if (m_EditButton != null)
			{
				m_EditButton.gameObject.SetActive(value: false);
			}
		}
	}
}
