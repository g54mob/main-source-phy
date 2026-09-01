using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	public class TaskDialogButton : TaskDialogItem
	{
		private ButtonType _type;

		private bool _elevationRequired;

		private bool _default;

		private string _commandLinkNote;

		[Category("Appearance")]
		[Description("The type of the button.")]
		[DefaultValue(ButtonType.Custom)]
		public ButtonType ButtonType
		{
			get
			{
				return _type;
			}
			set
			{
				if (value != ButtonType.Custom)
				{
					CheckDuplicateButton(value, null);
					_type = value;
					base.Id = (int)value;
				}
				else
				{
					_type = value;
					AutoAssignId();
					UpdateOwner();
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text of the note associated with a command link button.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string CommandLinkNote
		{
			get
			{
				return _commandLinkNote ?? string.Empty;
			}
			set
			{
				_commandLinkNote = value;
				UpdateOwner();
			}
		}

		[Category("Behavior")]
		[Description("Indicates if the button is the default button on the dialog.")]
		[DefaultValue(false)]
		public bool Default
		{
			get
			{
				return _default;
			}
			set
			{
				_default = value;
				if (value && base.Owner != null)
				{
					foreach (TaskDialogButton button in base.Owner.Buttons)
					{
						if (button != this)
						{
							button.Default = false;
						}
					}
				}
				UpdateOwner();
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether the Task Dialog button or command link should have a User Account Control (UAC) shield icon (in other words, whether the action invoked by the button requires elevation).")]
		[DefaultValue(false)]
		public bool ElevationRequired
		{
			get
			{
				return _elevationRequired;
			}
			set
			{
				_elevationRequired = value;
				if (base.Owner != null)
				{
					base.Owner.SetButtonElevationRequired(this);
				}
			}
		}

		internal override int Id
		{
			get
			{
				return base.Id;
			}
			set
			{
				if (base.Id != value)
				{
					if (_type != ButtonType.Custom)
					{
						throw new InvalidOperationException(Resources.NonCustomTaskDialogButtonIdError);
					}
					base.Id = value;
				}
			}
		}

		internal NativeMethods.TaskDialogCommonButtonFlags ButtonFlag
		{
			get
			{
				switch (_type)
				{
				case ButtonType.Ok:
					return NativeMethods.TaskDialogCommonButtonFlags.OkButton;
				case ButtonType.Yes:
					return NativeMethods.TaskDialogCommonButtonFlags.YesButton;
				case ButtonType.No:
					return NativeMethods.TaskDialogCommonButtonFlags.NoButton;
				case ButtonType.Cancel:
					return NativeMethods.TaskDialogCommonButtonFlags.CancelButton;
				case ButtonType.Retry:
					return NativeMethods.TaskDialogCommonButtonFlags.RetryButton;
				case ButtonType.Close:
					return NativeMethods.TaskDialogCommonButtonFlags.CloseButton;
				default:
					return (NativeMethods.TaskDialogCommonButtonFlags)0;
				}
			}
		}

		protected override IEnumerable ItemCollection
		{
			get
			{
				if (base.Owner != null)
				{
					return base.Owner.Buttons;
				}
				return null;
			}
		}

		public TaskDialogButton()
		{
		}

		public TaskDialogButton(ButtonType type)
			: base((int)type)
		{
			_type = type;
		}

		public TaskDialogButton(IContainer container)
			: base(container)
		{
		}

		public TaskDialogButton(string text)
		{
			base.Text = text;
		}

		internal override void AutoAssignId()
		{
			if (_type == ButtonType.Custom)
			{
				base.AutoAssignId();
			}
		}

		internal override void CheckDuplicate(TaskDialogItem itemToExclude)
		{
			CheckDuplicateButton(_type, itemToExclude);
			base.CheckDuplicate(itemToExclude);
		}

		private void CheckDuplicateButton(ButtonType type, TaskDialogItem itemToExclude)
		{
			if (type == ButtonType.Custom || base.Owner == null)
			{
				return;
			}
			foreach (TaskDialogButton button in base.Owner.Buttons)
			{
				if (button != this && button != itemToExclude && button.ButtonType == type)
				{
					throw new InvalidOperationException(Resources.DuplicateButtonTypeError);
				}
			}
		}
	}
}
