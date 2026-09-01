using System.Collections;
using System.ComponentModel;

namespace Ookii.Dialogs
{
	public class TaskDialogRadioButton : TaskDialogItem
	{
		private bool _checked;

		[Category("Appearance")]
		[Description("Indicates whether the radio button is checked.")]
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return _checked;
			}
			set
			{
				_checked = value;
				if (!value || base.Owner == null)
				{
					return;
				}
				foreach (TaskDialogRadioButton radioButton in base.Owner.RadioButtons)
				{
					if (radioButton != this)
					{
						radioButton.Checked = false;
					}
				}
			}
		}

		protected override IEnumerable ItemCollection
		{
			get
			{
				if (base.Owner != null)
				{
					return base.Owner.RadioButtons;
				}
				return null;
			}
		}

		public TaskDialogRadioButton()
		{
		}

		public TaskDialogRadioButton(IContainer container)
			: base(container)
		{
		}
	}
}
