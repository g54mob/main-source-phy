using System;
using UnityEngine.UI;

namespace TFBGames
{
	public class NavigableUGUITextInput : NavigableTextInputBase<InputField>
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
			}
		}

		protected override void Start()
		{
			base.Start();
			if (m_InputField != null)
			{
				switch (m_InputType)
				{
				case KeyboardType.Default:
					m_InputField.inputType = InputField.InputType.Standard;
					break;
				case KeyboardType.Numeric:
					m_InputField.contentType = InputField.ContentType.IntegerNumber;
					m_InputField.characterValidation = InputField.CharacterValidation.Integer;
					break;
				case KeyboardType.Password:
					m_InputField.inputType = InputField.InputType.Password;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				m_InputField.interactable = m_InlineText;
				m_InputField.targetGraphic.raycastTarget = false;
				m_InputField.placeholder.raycastTarget = false;
				m_InputField.textComponent.raycastTarget = false;
			}
		}

		public override void EnableTextInput()
		{
			m_InputField.ActivateInputField();
			m_InputField.Select();
			m_InputField.targetGraphic.raycastTarget = true;
			base.targetGraphic.raycastTarget = false;
			base.EnableTextInput();
		}

		public override void DisableTextInput()
		{
			m_InputField.targetGraphic.raycastTarget = false;
			base.targetGraphic.raycastTarget = true;
			m_InputField.DeactivateInputField();
			base.DisableTextInput();
		}
	}
}
