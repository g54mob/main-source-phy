using System;
using TMPro;

namespace TFBGames
{
	public class NavigableTMPTextInput : NavigableTextInputBase<TMP_InputField>
	{
		public override string text
		{
			get
			{
				return m_InputField.text;
			}
			set
			{
				m_InputField.text = value;
				m_InputField.onEndEdit?.Invoke(value);
			}
		}

		public TMP_InputField.OnChangeEvent onValueChanged => m_InputField.onValueChanged;

		protected override void Start()
		{
			base.Start();
			if (m_InputField != null)
			{
				switch (m_InputType)
				{
				case KeyboardType.Default:
					m_InputField.inputType = TMP_InputField.InputType.Standard;
					break;
				case KeyboardType.Numeric:
					m_InputField.contentType = TMP_InputField.ContentType.IntegerNumber;
					m_InputField.characterValidation = TMP_InputField.CharacterValidation.Integer;
					break;
				case KeyboardType.Password:
					m_InputField.inputType = TMP_InputField.InputType.Password;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				m_InputField.interactable = m_InlineText;
				m_InputField.targetGraphic.raycastTarget = false;
				m_InputField.placeholder.raycastTarget = false;
				m_InputField.textComponent.raycastTarget = false;
				TMP_SelectionCaret componentInChildren = m_InputField.GetComponentInChildren<TMP_SelectionCaret>();
				if (componentInChildren != null)
				{
					componentInChildren.raycastTarget = false;
				}
			}
		}

		public override void EnableTextInput()
		{
			base.EnableTextInput();
			m_InputField.ActivateInputField();
			m_InputField.targetGraphic.raycastTarget = true;
			base.targetGraphic.raycastTarget = false;
			m_InputField.Select();
		}

		public override void DisableTextInput()
		{
			m_InputField.targetGraphic.raycastTarget = false;
			base.targetGraphic.raycastTarget = true;
			m_InputField.DeactivateInputField();
			base.DisableTextInput();
		}

		public void SetTextNoNotify(string t)
		{
			m_InputField.SetTextWithoutNotify(t);
		}

		public void OnSubmit()
		{
			base.OnSubmit(null);
		}
	}
}
