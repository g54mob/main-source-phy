using System;
using System.Collections;
using System.ComponentModel;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	[DefaultEvent("Click")]
	public abstract class TaskDialogItem : Component
	{
		private TaskDialog _owner;

		private int _id;

		private bool _enabled = true;

		private string _text;

		private IContainer components = null;

		[Browsable(false)]
		public TaskDialog Owner
		{
			get
			{
				return _owner;
			}
			internal set
			{
				_owner = value;
				AutoAssignId();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text of the item.")]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return _text ?? string.Empty;
			}
			set
			{
				_text = value;
				UpdateOwner();
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether the item is enabled.")]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				_enabled = value;
				if (Owner != null)
				{
					Owner.SetItemEnabled(this);
				}
			}
		}

		[Category("Data")]
		[Description("The id of the item.")]
		[DefaultValue(0)]
		internal virtual int Id
		{
			get
			{
				return _id;
			}
			set
			{
				CheckDuplicateId(null, value);
				_id = value;
				UpdateOwner();
			}
		}

		protected abstract IEnumerable ItemCollection { get; }

		protected TaskDialogItem()
		{
			InitializeComponent();
		}

		protected TaskDialogItem(IContainer container)
		{
			if (container != null)
			{
				container.Add(this);
			}
			InitializeComponent();
		}

		internal TaskDialogItem(int id)
		{
			InitializeComponent();
			_id = id;
		}

		public void Click()
		{
			if (Owner == null)
			{
				throw new InvalidOperationException(Resources.NoAssociatedTaskDialogError);
			}
			Owner.ClickItem(this);
		}

		protected void UpdateOwner()
		{
			if (Owner != null)
			{
				Owner.UpdateDialog();
			}
		}

		internal virtual void CheckDuplicate(TaskDialogItem itemToExclude)
		{
			CheckDuplicateId(itemToExclude, _id);
		}

		internal virtual void AutoAssignId()
		{
			if (ItemCollection == null)
			{
				return;
			}
			int num = 9;
			foreach (TaskDialogItem item in ItemCollection)
			{
				if (item.Id > num)
				{
					num = item.Id;
				}
			}
			Id = num + 1;
		}

		private void CheckDuplicateId(TaskDialogItem itemToExclude, int id)
		{
			if (id == 0)
			{
				return;
			}
			IEnumerable itemCollection = ItemCollection;
			if (itemCollection == null)
			{
				return;
			}
			foreach (TaskDialogItem item in itemCollection)
			{
				if (item != this && item != itemToExclude && item.Id == id)
				{
					throw new InvalidOperationException(Resources.DuplicateItemIdError);
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		private void InitializeComponent()
		{
			components = new Container();
		}
	}
}
