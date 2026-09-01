using System;
using SRF;
using UnityEngine.UI;

namespace SRDebugger.UI.Controls.Data
{
	public class StringControl : DataBoundControl
	{
		[RequiredField]
		public InputField InputField;

		[RequiredField]
		public Text Title;

		protected override void Start()
		{
			base.Start();
			InputField.onValueChanged.AddListener(OnValueChanged);
		}

		private void OnValueChanged(string newValue)
		{
			UpdateValue(newValue);
		}

		protected override void OnBind(string propertyName, Type t)
		{
			base.OnBind(propertyName, t);
			Title.text = propertyName;
			InputField.text = string.Empty;
			InputField.interactable = !base.IsReadOnly;
		}

		protected override void OnValueUpdated(object newValue)
		{
			string text = ((newValue != null) ? ((string)newValue) : string.Empty);
			InputField.text = text;
		}

		public override bool CanBind(Type type, bool isReadOnly)
		{
			return type == typeof(string) && !isReadOnly;
		}
	}
}
