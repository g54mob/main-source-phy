using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	public class TaskDialogItemCollection<T> : Collection<T> where T : TaskDialogItem
	{
		private TaskDialog _owner;

		internal TaskDialogItemCollection(TaskDialog owner)
		{
			_owner = owner;
		}

		protected override void ClearItems()
		{
			using (IEnumerator<T> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T current = enumerator.Current;
					current.Owner = null;
				}
			}
			base.ClearItems();
			_owner.UpdateDialog();
		}

		protected override void InsertItem(int index, T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (item.Owner != null)
			{
				throw new ArgumentException(Resources.TaskDialogItemHasOwnerError);
			}
			item.Owner = _owner;
			try
			{
				item.CheckDuplicate(null);
			}
			catch (InvalidOperationException)
			{
				item.Owner = null;
				throw;
			}
			base.InsertItem(index, item);
			_owner.UpdateDialog();
		}

		protected override void RemoveItem(int index)
		{
			base[index].Owner = null;
			base.RemoveItem(index);
			_owner.UpdateDialog();
		}

		protected override void SetItem(int index, T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (base[index] != item)
			{
				if (item.Owner != null)
				{
					throw new ArgumentException(Resources.TaskDialogItemHasOwnerError);
				}
				item.Owner = _owner;
				try
				{
					item.CheckDuplicate(base[index]);
				}
				catch (InvalidOperationException)
				{
					item.Owner = null;
					throw;
				}
				base[index].Owner = null;
				base.SetItem(index, item);
				_owner.UpdateDialog();
			}
		}
	}
}
