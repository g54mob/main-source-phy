using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.UGUIComponentsForSettings
{
	public class TextfieldUGUI : MonoBehaviour
	{
		public delegate void OnTextChangedDelegate(string text);

		public TMP_InputField InputTf;

		public UnityEvent<string> OnTextChangedEvent;

		public OnTextChangedDelegate OnTextChanged;

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

		public void Start()
		{
		}

		private void onTextChanged(string text)
		{
		}
	}
}
