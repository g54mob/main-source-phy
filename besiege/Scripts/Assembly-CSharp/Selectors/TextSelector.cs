using UnityEngine;

namespace Selectors
{
	public class TextSelector : Selector
	{
		[SerializeField]
		private DynamicText title;

		[SerializeField]
		private TextHolder valueHolder;

		private bool updateCallback;

		public override MapperType MapperType
		{
			get
			{
				return Text;
			}
			set
			{
				if (updateCallback)
				{
					if (Text != null)
					{
						Text.TextChanged -= OnTextChanged;
					}
					updateCallback = false;
				}
				Text = (MText)value;
				if (Text != null)
				{
					Text.TextChanged += OnTextChanged;
					updateCallback = true;
				}
			}
		}

		public MText Text { get; set; }

		public string Value
		{
			get
			{
				return Text.Value;
			}
			set
			{
				Text.Value = value;
				UpdateText();
			}
		}

		private void Awake()
		{
			valueHolder.TextChanged += OnManualInput;
		}

		private void OnTextChanged(string newText)
		{
			UpdateText();
		}

		private void OnManualInput(string newValue)
		{
			Text.SetValue(newValue);
			OnEdit();
		}

		protected void OnDisable()
		{
			if (updateCallback)
			{
				if (Text != null)
				{
					Text.TextChanged -= OnTextChanged;
				}
				updateCallback = false;
			}
		}

		private void UpdateText()
		{
			if (Text == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(Value))
			{
				valueHolder.SetText(Value);
			}
			else
			{
				WorkshopManager.VerifyString(Value, delegate(WorkshopManager.VerifyStringResult res, string str)
				{
					if (valueHolder != null)
					{
						valueHolder.SetText(str);
					}
				});
			}
			valueHolder.SetConflict(InConflict());
		}

		public override void Init()
		{
			if (Text == null)
			{
				Debug.LogWarning("Colour slider has not been assigned to " + base.transform.name);
			}
			else
			{
				title.SetText(Text.DisplayName.Replace(" ", "\n").ToUpper());
			}
			base.Init();
			UpdateText();
		}
	}
}
