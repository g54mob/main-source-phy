using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[AddComponentMenu("ModIO/Inspector/Inspector Editable TextField")]
	public class InspectorEditableTextField : MonoBehaviour, IModViewElement
	{
		public ModProfileFieldDisplay FieldDisplay;

		public GameObject FieldContainer;

		public InputField InputField;

		[MemberReference.DropdownDisplay(typeof(ModProfile), false, false, null, displayEnumerables = false, displayNested = true)]
		public MemberReference OverrideReference = new MemberReference("id");

		public bool UseOverrideReference;

		private ModView m_view;

		private ModProfile m_profile;

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		public void StartEditing()
		{
			ToggleEditField(true);
			InputField.text = GetDisplayString();
			InputField.Select();
		}

		public void SubmitChanges()
		{
			ToggleEditField(false);
			string displayString = GetDisplayString();
			if (InputField.text != displayString)
			{
				ModBrowser.filterMethod(InputField.text, delegate(int res, string str)
				{
					SubmitFieldChanges(str);
				});
			}
		}

		private string GetDisplayString()
		{
			string inputText = ((!UseOverrideReference) ? GetDisplayString(FieldDisplay.reference) : GetDisplayString(OverrideReference));
			return SanitizeForTextField(inputText);
		}

		public string GetDisplayString(MemberReference reference)
		{
			object value = reference.GetValue(m_profile);
			string text = ValueFormatting.FormatValue(value, FieldDisplay.formatting.method, FieldDisplay.formatting.toStringParameter);
			if (FieldDisplay.useUppercase)
			{
				text = text.ToUpper();
			}
			return text;
		}

		private EditableStringField GetEditableStringField(EditableModProfile editableProfile, MemberReference reference)
		{
			switch (reference.MemberPath)
			{
			case "descriptionAsText":
			case "descriptionAsHTML":
				return editableProfile.descriptionAsHTML;
			case "name":
				return editableProfile.name;
			default:
				return null;
			}
		}

		private void SubmitFieldChanges(string inputFieldTextValue)
		{
			EditableModProfile editableModProfile = EditableModProfile.CreateFromProfile(m_profile);
			string text = SanitizeForWebRequest(inputFieldTextValue);
			EditableStringField editableStringField = GetEditableStringField(editableModProfile, (!UseOverrideReference) ? FieldDisplay.reference : OverrideReference);
			if (editableStringField.value == text)
			{
				RestoreFieldDisplay();
				return;
			}
			editableStringField.value = text;
			editableStringField.isDirty = true;
			Action<ModProfile> onSuccess = delegate(ModProfile changedProfile)
			{
				m_profile = changedProfile;
				ViewManager.instance.explorerView.RefreshOnNextFocus();
				FieldDisplay.DisplayProfile(changedProfile);
				ToggleTextFieldInteractables(true);
			};
			Action<WebRequestError> onError = delegate(WebRequestError error)
			{
				if (error.errorReference != 0)
				{
					MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to update mod profile.\n" + error.displayMessage);
				}
				RestoreFieldDisplay();
				ToggleTextFieldInteractables(true);
			};
			ModManager.SubmitModChanges(m_profile.id, editableModProfile, onSuccess, onError);
			PreupdateFieldDisplay(inputFieldTextValue);
			ToggleTextFieldInteractables(false);
		}

		private void PreupdateFieldDisplay(string fieldTextValue)
		{
			FieldDisplay.UpdateDisplay(fieldTextValue);
		}

		private void RestoreFieldDisplay()
		{
			FieldDisplay.DisplayProfile(m_profile);
		}

		private void ToggleEditField(bool toggleOn)
		{
			FieldContainer.SetActive(!toggleOn);
			InputField.gameObject.SetActive(toggleOn);
		}

		private void ToggleTextFieldInteractables(bool toggleOn)
		{
			Selectable[] componentsInChildren = FieldContainer.GetComponentsInChildren<Selectable>();
			foreach (Selectable selectable in componentsInChildren)
			{
				selectable.interactable = toggleOn;
			}
		}

		private string SanitizeForWebRequest(string inputFieldText)
		{
			return inputFieldText.Replace("\n", "<br>");
		}

		private string SanitizeForTextField(string inputText)
		{
			return inputText.Replace("<br>", "\n").Replace("<br />", "\n");
		}

		private void OnEnable()
		{
			SetModView(m_view);
		}

		private void OnDisable()
		{
			ToggleEditField(false);
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(OnProfileChanged);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(OnProfileChanged);
					OnProfileChanged(m_view.profile);
				}
				else
				{
					OnProfileChanged(null);
				}
			}
		}

		public void OnProfileChanged(ModProfile modProfile)
		{
			if (modProfile != m_profile)
			{
				m_profile = modProfile;
			}
		}
	}
}
