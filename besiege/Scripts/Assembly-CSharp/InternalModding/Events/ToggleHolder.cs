using Selectors;

namespace InternalModding.Events
{
	public class ToggleHolder : BaseHolder
	{
		public UIButtonExtended Toggle;

		public bool Value;

		public event ValueChangeHandler ValueChanged;

		public void Awake()
		{
			Toggle.Click += OnToggle;
		}

		private void OnToggle()
		{
			SetValue(!Value);
		}

		public void SetValue(bool newValue)
		{
			SetValueNoEvent(newValue);
			if (this.ValueChanged != null)
			{
				this.ValueChanged(Value);
			}
		}

		public void SetValueNoEvent(bool newValue)
		{
			Value = newValue;
			Toggle.ToggleBG(Value);
		}
	}
}
